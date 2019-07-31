/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
#include "emboss-filter.h"

// EXTERNAL INCLUDES
#include <sstream>
#include <dali/public-api/animation/constraints.h>
#include <dali/public-api/common/stage.h>
#include <dali/public-api/object/property-map.h>
#include <dali/public-api/render-tasks/render-task-list.h>
#include <dali/public-api/rendering/renderer.h>

// INTERNAL INCLUDES
#include <dali-toolkit/internal/controls/control/control-renderers.h>

namespace Dali
{

namespace Toolkit
{

namespace Internal
{

namespace
{

const char* EMBOSS_FRAGMENT_SOURCE =
{
 "precision highp float;\n"
 "varying mediump vec2 vTexCoord;\n"
 "uniform sampler2D sTexture;\n"
 "uniform vec2 uTexScale;\n"
 "uniform vec3 uCoefficient;\n"
 "\n"
 "void main()\n"
 "{\n"
 "  vec4 color  = uCoefficient.x * texture2D( sTexture, vTexCoord + vec2(0.0, -uTexScale.y) );\n"
 "  color += uCoefficient.y * texture2D( sTexture, vTexCoord );\n"
 "  color += uCoefficient.z * texture2D( sTexture, vTexCoord + vec2(0.0, uTexScale.y) );\n"
 "  gl_FragColor = color;\n"
 "}\n"
};

const char* const COMPOSITE_FRAGMENT_SOURCE =
{
  "varying mediump vec2 vTexCoord;\n"
  "uniform sampler2D sTexture;\n"
  "uniform lowp vec4 uEffectColor;\n"
  "void main()\n"
  "{\n"
  "  gl_FragColor = uEffectColor;\n"
  "  gl_FragColor.a *= texture2D( sTexture, vTexCoord).a;\n"
  "}\n"
};

const char* const TEX_SCALE_UNIFORM_NAME( "uTexScale" );
const char* const COEFFICIENT_UNIFORM_NAME( "uCoefficient" );
const char* const COLOR_UNIFORM_NAME( "uEffectColor" );

} // namespace

EmbossFilter::EmbossFilter()
: ImageFilter()
{
}

EmbossFilter::~EmbossFilter()
{
}

void EmbossFilter::Enable()
{
  mFrameBufferForEmboss1 = FrameBuffer::New( mTargetSize.width, mTargetSize.height, FrameBuffer::Attachment::NONE );
  Texture texture1 = Texture::New( TextureType::TEXTURE_2D, mPixelFormat, unsigned(mTargetSize.width), unsigned(mTargetSize.height) );
  mFrameBufferForEmboss1.AttachColorTexture( texture1 );

  mFrameBufferForEmboss2 = FrameBuffer::New( mTargetSize.width, mTargetSize.height, FrameBuffer::Attachment::NONE );
  Texture texture2 = Texture::New( TextureType::TEXTURE_2D, mPixelFormat, unsigned(mTargetSize.width), unsigned(mTargetSize.height) );
  mFrameBufferForEmboss2.AttachColorTexture( texture2 );

  // create actor to render input with applied emboss effect
  mActorForInput1 = Actor::New();
  mActorForInput1.SetParentOrigin( ParentOrigin::CENTER );
  mActorForInput1.SetSize(mTargetSize);
  Vector2 textureScale( 1.5f/mTargetSize.width, 1.5f/mTargetSize.height);
  mActorForInput1.RegisterProperty( TEX_SCALE_UNIFORM_NAME, textureScale );
  mActorForInput1.RegisterProperty( COEFFICIENT_UNIFORM_NAME, Vector3( 2.f, -1.f, -1.f ) );
  // set EMBOSS custom shader
  Renderer renderer1 = CreateRenderer( BASIC_VERTEX_SOURCE, EMBOSS_FRAGMENT_SOURCE );
  SetRendererTexture( renderer1, mInputTexture );
  mActorForInput1.AddRenderer( renderer1 );
  mRootActor.Add( mActorForInput1 );

  mActorForInput2 = Actor::New();
  mActorForInput2.SetParentOrigin( ParentOrigin::CENTER );
  mActorForInput2.SetSize(mTargetSize);
  mActorForInput2.RegisterProperty( TEX_SCALE_UNIFORM_NAME, textureScale );
  mActorForInput2.RegisterProperty( COEFFICIENT_UNIFORM_NAME, Vector3( -1.f, -1.f, 2.f ) );
  // set EMBOSS custom shader
  Renderer renderer2 = CreateRenderer( BASIC_VERTEX_SOURCE, EMBOSS_FRAGMENT_SOURCE );
  SetRendererTexture( renderer2, mInputTexture );
  mActorForInput2.AddRenderer( renderer2 );
  mRootActor.Add( mActorForInput2 );

  mActorForComposite = Actor::New();
  mActorForComposite.SetParentOrigin( ParentOrigin::CENTER );
  mActorForComposite.SetSize(mTargetSize);
  mActorForComposite.SetColor( Color::BLACK );

  mRootActor.Add( mActorForComposite );

  mRendererForEmboss1 = CreateRenderer( BASIC_VERTEX_SOURCE, COMPOSITE_FRAGMENT_SOURCE );
  SetRendererTexture( mRendererForEmboss1, mFrameBufferForEmboss1 );
  mRendererForEmboss1.RegisterProperty( COLOR_UNIFORM_NAME, Color::BLACK );
  mActorForComposite.AddRenderer( mRendererForEmboss1 );

  mRendererForEmboss2 = CreateRenderer( BASIC_VERTEX_SOURCE, COMPOSITE_FRAGMENT_SOURCE );
  SetRendererTexture( mRendererForEmboss2, mFrameBufferForEmboss2 );
  mRendererForEmboss2.RegisterProperty( COLOR_UNIFORM_NAME, Color::WHITE );
  mActorForComposite.AddRenderer( mRendererForEmboss2 );

  SetupCamera();
  CreateRenderTasks();
}

void EmbossFilter::Disable()
{
  if( mRootActor )
  {
    if( mCameraActor )
    {
      mRootActor.Remove( mCameraActor );
      mCameraActor.Reset();
    }

    if( mActorForInput1 )
    {
      mRootActor.Remove( mActorForInput1 );
      mActorForInput1.Reset();
    }

    if( mActorForInput2 )
    {
      mRootActor.Remove( mActorForInput2 );
      mActorForInput2.Reset();
    }

    if( mActorForComposite )
    {
      mActorForComposite.RemoveRenderer( mRendererForEmboss1 );
      mRendererForEmboss1.Reset();

      mActorForComposite.RemoveRenderer( mRendererForEmboss2 );
      mRendererForEmboss2.Reset();

      mRootActor.Remove( mActorForComposite );
      mActorForComposite.Reset();
    }

    RenderTaskList taskList = Stage::GetCurrent().GetRenderTaskList();
    if( mRenderTaskForEmboss1 )
    {
      taskList.RemoveTask(mRenderTaskForEmboss1);
    }

    if( mRenderTaskForEmboss2 )
    {
      taskList.RemoveTask(mRenderTaskForEmboss2);
    }

    if( mRenderTaskForOutput )
    {
      taskList.RemoveTask( mRenderTaskForOutput );
    }

    mRootActor.Reset();
  }
}

void EmbossFilter::Refresh()
{
  if( mRenderTaskForEmboss1 )
  {
    mRenderTaskForEmboss1.SetRefreshRate( mRefreshOnDemand ? RenderTask::REFRESH_ONCE : RenderTask::REFRESH_ALWAYS );
  }
  if( mRenderTaskForEmboss2 )
  {
    mRenderTaskForEmboss2.SetRefreshRate( mRefreshOnDemand ? RenderTask::REFRESH_ONCE : RenderTask::REFRESH_ALWAYS );
  }
}

void EmbossFilter::SetSize( const Vector2& size )
{
  mTargetSize = size;
  if( mActorForInput1 )
  {
    mActorForInput1.SetSize(mTargetSize);
  }
  if( mActorForInput2 )
  {
    mActorForInput2.SetSize(mTargetSize);
  }
  if( mActorForComposite )
  {
    mActorForComposite.SetSize(mTargetSize);
  }
}

void EmbossFilter::CreateRenderTasks()
{
  RenderTaskList taskList = Stage::GetCurrent().GetRenderTaskList();

  mRenderTaskForEmboss1 = taskList.CreateTask();
  mRenderTaskForEmboss1.SetRefreshRate( mRefreshOnDemand ? RenderTask::REFRESH_ONCE : RenderTask::REFRESH_ALWAYS );
  mRenderTaskForEmboss1.SetSourceActor( mActorForInput1 );
  mRenderTaskForEmboss1.SetExclusive(true);
  mRenderTaskForEmboss1.SetInputEnabled( false );
  mRenderTaskForEmboss1.SetClearColor( Vector4( 0.0f, 0.0f, 0.0f, 0.0f ) );
  mRenderTaskForEmboss1.SetClearEnabled( true );
  mRenderTaskForEmboss1.SetFrameBuffer( mFrameBufferForEmboss1 );
  mRenderTaskForEmboss1.SetCameraActor( mCameraActor );

  mRenderTaskForEmboss2 = taskList.CreateTask();
  mRenderTaskForEmboss2.SetRefreshRate( mRefreshOnDemand ? RenderTask::REFRESH_ONCE : RenderTask::REFRESH_ALWAYS );
  mRenderTaskForEmboss2.SetSourceActor( mActorForInput2 );
  mRenderTaskForEmboss2.SetExclusive(true);
  mRenderTaskForEmboss2.SetInputEnabled( false );
  mRenderTaskForEmboss2.SetClearColor( Vector4( 1.0f, 1.0f, 1.0f, 0.0f ) );
  mRenderTaskForEmboss2.SetClearEnabled( true );
  mRenderTaskForEmboss2.SetFrameBuffer( mFrameBufferForEmboss2 );
  mRenderTaskForEmboss2.SetCameraActor( mCameraActor );

  mRenderTaskForOutput = taskList.CreateTask();
  mRenderTaskForOutput.SetRefreshRate( mRefreshOnDemand ? RenderTask::REFRESH_ONCE : RenderTask::REFRESH_ALWAYS );
  mRenderTaskForOutput.SetSourceActor( mActorForComposite );
  mRenderTaskForOutput.SetExclusive(true);
  mRenderTaskForOutput.SetInputEnabled( false );
  mRenderTaskForOutput.SetClearColor( Vector4( 0.5f, 0.5f, 0.5f, 0.0f ) );
  mRenderTaskForOutput.SetClearEnabled( true );
  mRenderTaskForOutput.SetFrameBuffer( mOutputFrameBuffer );
  mRenderTaskForOutput.SetCameraActor( mCameraActor );
}

} // namespace Internal

} // namespace Toolkit

} // namespace Dali
