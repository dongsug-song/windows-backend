/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

// CLASS HEADER
#include <dali-toolkit/internal/visuals/animated-vector-image/animated-vector-image-visual.h>

// EXTERNAL INCLUDES
#include <dali/public-api/common/stage.h>
#include <dali/devel-api/rendering/renderer-devel.h>
#include <dali/integration-api/debug.h>

// INTERNAL INCLUDES
#include <dali-toolkit/public-api/visuals/image-visual-properties.h>
#include <dali-toolkit/public-api/visuals/visual-properties.h>
#include <dali-toolkit/devel-api/visuals/image-visual-properties-devel.h>
#include <dali-toolkit/devel-api/visuals/animated-vector-image-visual-signals-devel.h>
#include <dali-toolkit/internal/visuals/image-visual-shader-factory.h>
#include <dali-toolkit/internal/visuals/visual-factory-cache.h>
#include <dali-toolkit/internal/visuals/visual-string-constants.h>
#include <dali-toolkit/internal/visuals/visual-base-data-impl.h>

namespace Dali
{

namespace Toolkit
{

namespace Internal
{

namespace
{

constexpr auto LOOP_FOREVER = -1;

const Dali::Vector4 FULL_TEXTURE_RECT( 0.f, 0.f, 1.f, 1.f );

// Flags for re-sending data to the rasterize thread
enum Flags
{
  RESEND_PLAY_RANGE    = 1 << 0,
  RESEND_LOOP_COUNT    = 1 << 1,
  RESEND_STOP_BEHAVIOR = 1 << 2,
  RESEND_LOOPING_MODE  = 1 << 3
};

// stop behavior
DALI_ENUM_TO_STRING_TABLE_BEGIN( STOP_BEHAVIOR )
DALI_ENUM_TO_STRING_WITH_SCOPE( Dali::Toolkit::DevelImageVisual::StopBehavior, CURRENT_FRAME )
DALI_ENUM_TO_STRING_WITH_SCOPE( Dali::Toolkit::DevelImageVisual::StopBehavior, FIRST_FRAME )
DALI_ENUM_TO_STRING_WITH_SCOPE( Dali::Toolkit::DevelImageVisual::StopBehavior, LAST_FRAME )
DALI_ENUM_TO_STRING_TABLE_END( STOP_BEHAVIOR )

// looping mode
DALI_ENUM_TO_STRING_TABLE_BEGIN( LOOPING_MODE )
DALI_ENUM_TO_STRING_WITH_SCOPE( Dali::Toolkit::DevelImageVisual::LoopingMode, RESTART )
DALI_ENUM_TO_STRING_WITH_SCOPE( Dali::Toolkit::DevelImageVisual::LoopingMode, AUTO_REVERSE )
DALI_ENUM_TO_STRING_TABLE_END( LOOPING_MODE )

#if defined(DEBUG_ENABLED)
Debug::Filter* gVectorAnimationLogFilter = Debug::Filter::New( Debug::NoLogging, false, "LOG_VECTOR_ANIMATION" );
#endif

} // unnamed namespace

AnimatedVectorImageVisualPtr AnimatedVectorImageVisual::New( VisualFactoryCache& factoryCache, ImageVisualShaderFactory& shaderFactory, const VisualUrl& imageUrl, const Property::Map& properties )
{
  AnimatedVectorImageVisualPtr visual( new AnimatedVectorImageVisual( factoryCache, shaderFactory, imageUrl ) );
  visual->SetProperties( properties );

  return visual;
}

AnimatedVectorImageVisualPtr AnimatedVectorImageVisual::New( VisualFactoryCache& factoryCache, ImageVisualShaderFactory& shaderFactory, const VisualUrl& imageUrl )
{
  AnimatedVectorImageVisualPtr visual( new AnimatedVectorImageVisual( factoryCache, shaderFactory, imageUrl ) );

  return visual;
}

AnimatedVectorImageVisual::AnimatedVectorImageVisual( VisualFactoryCache& factoryCache, ImageVisualShaderFactory& shaderFactory, const VisualUrl& imageUrl )
: Visual::Base( factoryCache, Visual::FittingMode::FILL ),
  mImageVisualShaderFactory( shaderFactory ),
  mUrl( imageUrl ),
  mVectorRasterizeThread( imageUrl.GetUrl() ),
  mVisualSize(),
  mPlacementActor(),
  mLoopCount( LOOP_FOREVER ),
  mStartFrame( 0 ),
  mEndFrame( 0 ),
  mResendFlag( 0 ),
  mActionStatus( DevelAnimatedVectorImageVisual::Action::STOP ),
  mStopBehavior( DevelImageVisual::StopBehavior::CURRENT_FRAME ),
  mLoopingMode( DevelImageVisual::LoopingMode::RESTART )
{
  // the rasterized image is with pre-multiplied alpha format
  mImpl->mFlags |= Impl::IS_PREMULTIPLIED_ALPHA;

  mVectorRasterizeThread.SetResourceReadyCallback( new EventThreadCallback( MakeCallback( this, &AnimatedVectorImageVisual::OnResourceReady ) ) );
  mVectorRasterizeThread.SetAnimationFinishedCallback( new EventThreadCallback( MakeCallback( this, &AnimatedVectorImageVisual::OnAnimationFinished ) ) );

  mVectorRasterizeThread.Start();
}

AnimatedVectorImageVisual::~AnimatedVectorImageVisual()
{
}

void AnimatedVectorImageVisual::GetNaturalSize( Vector2& naturalSize )
{
  if( mImpl->mRenderer ) // Check if we have a rendered image
  {
    auto textureSet = mImpl->mRenderer.GetTextures();
    if( textureSet )
    {
      if( textureSet.GetTextureCount() > 0 )
      {
        auto texture = textureSet.GetTexture( 0 );
        naturalSize.x = texture.GetWidth();
        naturalSize.y = texture.GetHeight();
        return;
      }
    }
  }

  uint32_t width, height;
  mVectorRasterizeThread.GetDefaultSize( width, height );
  naturalSize.x = width;
  naturalSize.y = height;
}

void AnimatedVectorImageVisual::DoCreatePropertyMap( Property::Map& map ) const
{
  map.Clear();
  map.Insert( Toolkit::Visual::Property::TYPE, Toolkit::DevelVisual::ANIMATED_VECTOR_IMAGE );
  if( mUrl.IsValid() )
  {
    map.Insert( Toolkit::ImageVisual::Property::URL, mUrl.GetUrl() );
  }
  map.Insert( Toolkit::DevelImageVisual::Property::LOOP_COUNT, mLoopCount );

  Property::Array playRange;
  playRange.PushBack( mStartFrame );
  playRange.PushBack( mEndFrame );
  map.Insert( Toolkit::DevelImageVisual::Property::PLAY_RANGE, playRange );

  map.Insert( Toolkit::DevelImageVisual::Property::PLAY_STATE, static_cast< int >( mVectorRasterizeThread.GetPlayState() ) );
  map.Insert( Toolkit::DevelImageVisual::Property::CURRENT_FRAME_NUMBER, static_cast< int32_t >( mVectorRasterizeThread.GetCurrentFrameNumber() ) );
  map.Insert( Toolkit::DevelImageVisual::Property::TOTAL_FRAME_NUMBER, static_cast< int32_t >( mVectorRasterizeThread.GetTotalFrameNumber() ) );

  map.Insert( Toolkit::DevelImageVisual::Property::STOP_BEHAVIOR, mStopBehavior );
  map.Insert( Toolkit::DevelImageVisual::Property::LOOPING_MODE, mLoopingMode );
}

void AnimatedVectorImageVisual::DoCreateInstancePropertyMap( Property::Map& map ) const
{
  // Do nothing
}

void AnimatedVectorImageVisual::DoSetProperties( const Property::Map& propertyMap )
{
  // url already passed in from constructor
  for( Property::Map::SizeType iter = 0; iter < propertyMap.Count(); ++iter )
  {
    KeyValuePair keyValue = propertyMap.GetKeyValue( iter );
    if( keyValue.first.type == Property::Key::INDEX )
    {
      DoSetProperty( keyValue.first.indexKey, keyValue.second );
    }
    else
    {
       if( keyValue.first == LOOP_COUNT_NAME )
       {
          DoSetProperty( Toolkit::DevelImageVisual::Property::LOOP_COUNT, keyValue.second );
       }
       else if( keyValue.first == PLAY_RANGE_NAME )
       {
          DoSetProperty( Toolkit::DevelImageVisual::Property::PLAY_RANGE, keyValue.second );
       }
       else if( keyValue.first == STOP_BEHAVIOR_NAME )
       {
          DoSetProperty( Toolkit::DevelImageVisual::Property::STOP_BEHAVIOR, keyValue.second );
       }
       else if( keyValue.first == LOOPING_MODE_NAME )
       {
          DoSetProperty( Toolkit::DevelImageVisual::Property::LOOPING_MODE, keyValue.second );
       }
    }
  }
}

void AnimatedVectorImageVisual::DoSetProperty( Property::Index index, const Property::Value& value )
{
  switch(index)
  {
    case Toolkit::DevelImageVisual::Property::LOOP_COUNT:
    {
      int32_t loopCount;
      if( value.Get( loopCount ) )
      {
        mLoopCount = loopCount;
        mResendFlag |= RESEND_LOOP_COUNT;
      }
      break;
    }
    case Toolkit::DevelImageVisual::Property::PLAY_RANGE:
    {
      Property::Array* array = value.GetArray();
      if( array )
      {
        size_t count = array->Count();
        if( count >= 2 )
        {
          int startFrame, endFrame;
          int totalFrame = mVectorRasterizeThread.GetTotalFrameNumber();
          array->GetElementAt( 0 ).Get( startFrame );
          array->GetElementAt( 1 ).Get( endFrame );

          if( startFrame >= 0 && startFrame < totalFrame && endFrame >= 0 && endFrame < totalFrame )
          {
            mStartFrame = startFrame;
            mEndFrame = endFrame;
            mResendFlag |= RESEND_PLAY_RANGE;
          }
          else
          {
            DALI_LOG_ERROR( "Invalid play range [%d, %d / %d]\n", startFrame, endFrame, totalFrame );
          }
        }
      }
      break;
    }
    case Toolkit::DevelImageVisual::Property::STOP_BEHAVIOR:
    {
      int32_t stopBehavior;
      Scripting::GetEnumerationProperty( value, STOP_BEHAVIOR_TABLE, STOP_BEHAVIOR_TABLE_COUNT, stopBehavior );
      mStopBehavior = DevelImageVisual::StopBehavior::Type( stopBehavior );
      mResendFlag |= RESEND_STOP_BEHAVIOR;
      break;
    }
    case Toolkit::DevelImageVisual::Property::LOOPING_MODE:
    {
      int32_t loopingMode;
      Scripting::GetEnumerationProperty( value, LOOPING_MODE_TABLE, LOOPING_MODE_TABLE_COUNT, loopingMode );
      mLoopingMode = DevelImageVisual::LoopingMode::Type( loopingMode );
      mResendFlag |= RESEND_LOOPING_MODE;
      break;
    }
  }
}

void AnimatedVectorImageVisual::DoSetOnStage( Actor& actor )
{
  Shader shader;

  if( mImpl->mCustomShader )
  {
    shader = Shader::New( mImpl->mCustomShader->mVertexShader.empty() ? mImageVisualShaderFactory.GetVertexShaderSource() : mImpl->mCustomShader->mVertexShader,
                          mImpl->mCustomShader->mFragmentShader.empty() ? mImageVisualShaderFactory.GetFragmentShaderSource() : mImpl->mCustomShader->mFragmentShader,
                          mImpl->mCustomShader->mHints );

    shader.RegisterProperty( PIXEL_AREA_UNIFORM_NAME, FULL_TEXTURE_RECT );
  }
  else
  {
    shader = mImageVisualShaderFactory.GetShader( mFactoryCache, false, true );
  }

  Geometry geometry = mFactoryCache.GetGeometry( VisualFactoryCache::QUAD_GEOMETRY );

  mImpl->mRenderer = Renderer::New( geometry, shader );

  TextureSet textureSet = TextureSet::New();
  mImpl->mRenderer.SetTextures( textureSet );

  // Register transform properties
  mImpl->mTransform.RegisterUniforms( mImpl->mRenderer, Direction::LEFT_TO_RIGHT );

  // Defer the rasterisation task until we get given a size (by Size Negotiation algorithm)

  // Hold the weak handle of the placement actor and delay the adding of renderer until the rasterization is finished.
  mPlacementActor = actor;

  mVectorRasterizeThread.SetRenderer( mImpl->mRenderer );

  DALI_LOG_INFO( gVectorAnimationLogFilter, Debug::Verbose, "AnimatedVectorImageVisual::DoSetOnStage [%p]\n", this );
}

void AnimatedVectorImageVisual::DoSetOffStage( Actor& actor )
{
  mVectorRasterizeThread.PauseAnimation();

  mActionStatus = DevelAnimatedVectorImageVisual::Action::PAUSE;

  if( mImpl->mRenderer )
  {
    mImpl->mRenderer.SetProperty( DevelRenderer::Property::RENDERING_BEHAVIOR, DevelRenderer::Rendering::IF_REQUIRED );

    actor.RemoveRenderer( mImpl->mRenderer );
    mImpl->mRenderer.Reset();
  }

  mPlacementActor.Reset();

  // Reset the visual size to zero so that when adding the actor back to stage the rasterization is forced
  mVisualSize = Vector2::ZERO;

  DALI_LOG_INFO( gVectorAnimationLogFilter, Debug::Verbose, "AnimatedVectorImageVisual::DoSetOffStage [%p]\n", this );
}

void AnimatedVectorImageVisual::OnSetTransform()
{
  Vector2 visualSize = mImpl->mTransform.GetVisualSize( mImpl->mControlSize );

  if( IsOnStage() )
  {
    DALI_LOG_INFO( gVectorAnimationLogFilter, Debug::Verbose, "AnimatedVectorImageVisual::OnSetTransform: width = %f, height = %f [%p]\n", visualSize.width, visualSize.height, this );

    if( visualSize != mVisualSize )
    {
      mVisualSize = visualSize;

      uint32_t width = static_cast< uint32_t >( visualSize.width );
      uint32_t height = static_cast< uint32_t >( visualSize.height );

      mVectorRasterizeThread.SetSize( width, height );
    }

    SendAnimationData();

    if( mActionStatus == DevelAnimatedVectorImageVisual::Action::PLAY )
    {
      mVectorRasterizeThread.PlayAnimation();

      mImpl->mRenderer.SetProperty( DevelRenderer::Property::RENDERING_BEHAVIOR, DevelRenderer::Rendering::CONTINUOUSLY );
    }
    else
    {
      // Render one frame
      mVectorRasterizeThread.RenderFrame();
    }

    if( mVectorRasterizeThread.IsResourceReady() )
    {
      Actor actor = mPlacementActor.GetHandle();
      if( actor )
      {
        actor.AddRenderer( mImpl->mRenderer );
        mPlacementActor.Reset();

        DALI_LOG_INFO( gVectorAnimationLogFilter, Debug::Verbose, "AnimatedVectorImageVisual::OnSetTransform: Renderer is added [%p]\n", this );
      }

      ResourceReady( Toolkit::Visual::ResourceStatus::READY );
    }
  }
}

void AnimatedVectorImageVisual::OnDoAction( const Property::Index actionId, const Property::Value& attributes )
{
  // Check if action is valid for this visual type and perform action if possible
  switch( actionId )
  {
    case DevelAnimatedVectorImageVisual::Action::PLAY:
    {
      if( IsOnStage() && mVisualSize != Vector2::ZERO )
      {
        mVectorRasterizeThread.PlayAnimation();

        mImpl->mRenderer.SetProperty( DevelRenderer::Property::RENDERING_BEHAVIOR, DevelRenderer::Rendering::CONTINUOUSLY );
      }
      mActionStatus = DevelAnimatedVectorImageVisual::Action::PLAY;
      break;
    }
    case DevelAnimatedVectorImageVisual::Action::PAUSE:
    {
      mVectorRasterizeThread.PauseAnimation();

      if( mImpl->mRenderer )
      {
        mImpl->mRenderer.SetProperty( DevelRenderer::Property::RENDERING_BEHAVIOR, DevelRenderer::Rendering::IF_REQUIRED );
      }

      mActionStatus = DevelAnimatedVectorImageVisual::Action::PAUSE;
      break;
    }
    case DevelAnimatedVectorImageVisual::Action::STOP:
    {
      if( mVectorRasterizeThread.GetPlayState() != DevelImageVisual::PlayState::STOPPED )
      {
        mVectorRasterizeThread.StopAnimation();

        OnAnimationFinished();
      }
      mActionStatus = DevelAnimatedVectorImageVisual::Action::STOP;
      break;
    }
    case DevelAnimatedVectorImageVisual::Action::JUMP_TO:
    {
      int32_t frameNumber;
      if( attributes.Get( frameNumber ) )
      {
        mVectorRasterizeThread.SetCurrentFrameNumber( frameNumber );

        if( IsOnStage() && mVectorRasterizeThread.GetPlayState() != DevelImageVisual::PlayState::PLAYING )
        {
          mVectorRasterizeThread.RenderFrame();
          Stage::GetCurrent().KeepRendering( 0.0f );    // Trigger rendering
        }
      }
      break;
    }
    case DevelAnimatedVectorImageVisual::Action::UPDATE_PROPERTY:
    {
      Property::Map* map = attributes.GetMap();
      if( map )
      {
        DoSetProperties( *map );

        SendAnimationData();
      }
      break;
    }
  }
}

void AnimatedVectorImageVisual::OnResourceReady()
{
  // If weak handle is holding a placement actor, it is the time to add the renderer to actor.
  Actor actor = mPlacementActor.GetHandle();
  if( actor )
  {
    actor.AddRenderer( mImpl->mRenderer );
    // reset the weak handle so that the renderer only get added to actor once
    mPlacementActor.Reset();

    ResourceReady( Toolkit::Visual::ResourceStatus::READY );

    DALI_LOG_INFO( gVectorAnimationLogFilter, Debug::Verbose, "AnimatedVectorImageVisual::OnResourceReady: Renderer is added [%p]\n", this );
  }
}

void AnimatedVectorImageVisual::OnAnimationFinished()
{
  if( mImpl->mEventObserver )
  {
    mImpl->mEventObserver->NotifyVisualEvent( *this, DevelAnimatedVectorImageVisual::Signal::ANIMATION_FINISHED );
  }

  mActionStatus = DevelAnimatedVectorImageVisual::Action::STOP;

  if( mImpl->mRenderer )
  {
    mImpl->mRenderer.SetProperty( DevelRenderer::Property::RENDERING_BEHAVIOR, DevelRenderer::Rendering::IF_REQUIRED );
  }
}

void AnimatedVectorImageVisual::SendAnimationData()
{
  if( mResendFlag )
  {
    bool isPlaying = false;
    if( mVectorRasterizeThread.GetPlayState() == DevelImageVisual::PlayState::PLAYING )
    {
      mVectorRasterizeThread.PauseAnimation();
      isPlaying = true;
    }

    if( mResendFlag & RESEND_LOOP_COUNT )
    {
      mVectorRasterizeThread.SetLoopCount( mLoopCount );
    }

    if( mResendFlag & RESEND_PLAY_RANGE )
    {
      mVectorRasterizeThread.SetPlayRange( mStartFrame, mEndFrame );
    }

    if( mResendFlag & RESEND_STOP_BEHAVIOR )
    {
      mVectorRasterizeThread.SetStopBehavior( mStopBehavior );
    }

    if( mResendFlag & RESEND_LOOPING_MODE )
    {
      mVectorRasterizeThread.SetLoopingMode( mLoopingMode );
    }

    if( IsOnStage() )
    {
      if( isPlaying )
      {
        mVectorRasterizeThread.PlayAnimation();
      }
      else
      {
        mVectorRasterizeThread.RenderFrame();
        Stage::GetCurrent().KeepRendering( 0.0f );
      }
    }

    mResendFlag = 0;
  }
}

} // namespace Internal

} // namespace Toolkit

} // namespace Dali
