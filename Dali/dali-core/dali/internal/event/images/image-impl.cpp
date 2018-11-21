/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd.
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
#include <dali/internal/event/images/image-impl.h>

// EXTERNAL INCLUDES
#include <cstring> // for strcmp

// INTERNAL INCLUDES
#include <dali/public-api/common/dali-common.h>
#include <dali/public-api/object/type-registry.h>

#include <dali/integration-api/debug.h>
#include <dali/internal/event/common/thread-local-storage.h>
#include <dali/internal/event/common/stage-impl.h>

using namespace Dali::Integration;

namespace Dali
{

namespace Internal
{

namespace
{

// Signals

const char* const SIGNAL_IMAGE_UPLOADED = "uploaded";

TypeRegistration mType( typeid( Dali::Image ), typeid( Dali::BaseHandle ), NULL );

Dali::SignalConnectorType signalConnector1( mType, SIGNAL_IMAGE_UPLOADED, &Image::DoConnectSignal );

}

bool Image::DoConnectSignal( BaseObject* object, ConnectionTrackerInterface* tracker, const std::string& signalName, FunctorDelegate* functor )
{
  bool connected( true );
  DALI_ASSERT_DEBUG( dynamic_cast<Image*>( object ) && "Failed to downcast from BaseObject to Image.\n" );
  Image* image = static_cast<Image*>(object);

  if( 0 == strcmp( signalName.c_str(), SIGNAL_IMAGE_UPLOADED ) )
  {
    image->UploadedSignal().Connect( tracker, functor );
  }
  else
  {
    // signalName does not match any signal
    connected = false;
  }

  return connected;
}

unsigned int Image::GetWidth() const
{
  return mWidth;
}

unsigned int Image::GetHeight() const
{
  return mHeight;
}

Vector2 Image::GetNaturalSize() const
{
  return Vector2( static_cast<float>( mWidth ), static_cast<float>( mHeight ) );
}

Image::Image()
: mTexture(),
  mWidth( 0 ),
  mHeight( 0 ),
  mConnectionCount( 0 )
{
}

Image::~Image()
{
  if( Stage::IsInstalled() )
  {
    UnregisterObject();
  }
}

void Image::Initialize()
{
  RegisterObject();
}

} // namespace Internal

} // namespace Dali
