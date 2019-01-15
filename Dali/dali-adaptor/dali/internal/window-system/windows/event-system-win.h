#ifndef _WINDOWEVENTSYSTEM_H_
#define _WINDOWEVENTSYSTEM_H_

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

#define EVENT_MOUSE_BUTTON_DOWN   WM_LBUTTONDOWN
#define EVENT_MOUSE_BUTTON_UP     WM_LBUTTONUP
#define EVENT_MOUSE_MOVE          WM_MOUSEMOVE
#define EVENT_MOUSE_WHEEL         WM_MOUSEWHEEL

#define EVENT_KEY_DOWN            WM_KEYDOWN
#define EVENT_KEY_UP              WM_KEYUP

#define WIN_EVENT_WINDOW_FOCUS_IN   WM_SETFOCUS
#define WIN_EVENT_WINDOW_FOCUS_OUT  WM_KILLFOCUS

#define WIN_EVENT_WINDOW_DAMAGE     WM_PAINT

#define DEVICE_MOUSE                    0

// INTERNAL INCLUDES
#include <dali/internal/window-system/windows/platform-implement-win.h>

namespace Dali
{

namespace Internal
{

namespace Adaptor
{

struct _Event_Mouse_Button
{
  WinWindowHandle     window; /**< The main window where event happened */

  unsigned int     timestamp; /**< Time when the event occurred */

  int              x; /**< x coordinate relative to window where event happened */
  int              y; /**< y coordinate relative to window where event happened */

  struct
  {
    int           device; /**< 0 if normal mouse, 1+ for other mouse-devices (eg multi-touch - other fingers) */
    double        radius, radius_x, radius_y; /**< radius of press point - radius_x and y if its an ellipse (radius is the average of the 2) */
    double        pressure; /**< pressure - 1.0 == normal, > 1.0 == more, 0.0 == none */
    double        angle; /**< angle relative to perpendicular (0.0 == perpendicular), in degrees */
    double        x, y; /**< same as x, y, but with sub-pixel precision, if available */
    struct
    {
      double     x, y;
    } root; /**< same as root.x, root.y, but with sub-pixel precision, if available */
  } multi;
};

/**
* @struct _Event_Mouse_Wheel
* Contains information about an Windows mouse wheel event.
*/
struct _Event_Mouse_Wheel
{
  WinWindowHandle     window; /**< The main window where event happened */
  WinWindowHandle     root_window; /**< The root window where event happened */
  WinWindowHandle     event_window; /**< The child window where event happened */

  unsigned int     timestamp; /**< Time when the event occurred */
  unsigned int     modifiers; /**< The combination of modifiers key (SHIFT,CTRL,ALT,..)*/

  int              direction; /**< Orientation of the wheel (horizontal/vertical) */
  int              z; /**< Value of the wheel event (+1/-1) */

  int              x; /**< x coordinate relative to window where event happened */
  int              y; /**< y coordinate relative to window where event happened */
  struct
  {
    int           x;
    int           y;
  } root; /**< Coordinates relative to root window */
};

typedef struct _Event_Mouse_Button Event_Mouse_Button;
typedef struct _Event_Mouse_Wheel  Event_Mouse_Wheel;

struct TWinEventInfo
{
  TWinEventInfo( uint64_t hWnd, uint32_t uMsg, uint64_t wParam, uint64_t lParam)
  {
    this->mWindow = (WinWindowHandle)hWnd;
    this->uMsg = uMsg;
    this->wParam = wParam;
    this->lParam = lParam;
  }

  WinWindowHandle mWindow;
  uint32_t uMsg;
  uint64_t wParam;
  uint64_t lParam;
};

} // namespace Adaptor

} // namespace internal

} // namespace Dali

#endif
