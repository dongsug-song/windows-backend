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
#include <dali/internal/window-system/windows/window-factory-win.h>

// INTERNAL HEADERS
#include <dali/internal/window-system/windows/window-base-win.h>
#include <dali/internal/window-system/common/display-utils.h>

namespace Dali
{
namespace Internal
{
namespace Adaptor
{

std::unique_ptr< WindowBase > WindowFactoryWin::CreateWindowBase( Dali::PositionSize positionSize, Any surface, bool isTransparent )
{
  return Utils::MakeUnique< WindowBaseWin >( positionSize, surface, isTransparent );
}

std::unique_ptr< IndicatorInterface > WindowFactoryWin::CreateIndicator( Adaptor* adaptor, Dali::Window::WindowOrientation orientation, IndicatorInterface::Observer* observer )
{
  return std::unique_ptr< IndicatorInterface >( nullptr );
}

// this should be created from Window impl
std::unique_ptr< WindowFactory > GetWindowFactory()
{
  // returns Window factory
  return Utils::MakeUnique< WindowFactoryWin >();
}

} // namespace Adaptor
} // namespace Internal
} // namespace Dali
