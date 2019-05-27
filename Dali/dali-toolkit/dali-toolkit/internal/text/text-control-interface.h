#ifndef DALI_TOOLKIT_TEXT_CONTROL_INTERFACE_H
#define DALI_TOOLKIT_TEXT_CONTROL_INTERFACE_H

/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd.
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

namespace Dali
{

namespace Toolkit
{

namespace Text
{

/**
 * @brief An interface that the Text::Controller uses to request a text relayout.
 */
class ControlInterface
{
public:

  /**
   * @brief Virtual destructor.
   */
  virtual ~ControlInterface()
  {}

  /**
   * @brief Called to request a text relayout.
   */
  virtual void RequestTextRelayout() = 0;
};

} // namespace Text

} // namespace Toolkit

} // namespace Dali

#endif // DALI_TOOLKIT_TEXT_CONTROL_INTERFACE_H
