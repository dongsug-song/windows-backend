﻿/*
 * Copyright(c) 2019 Samsung Electronics Co., Ltd.
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
using System;
using Tizen.NUI.BaseComponents;
using System.ComponentModel;

namespace Tizen.NUI.Components
{
    /// <summary>
    /// Switch is one kind of common component, it can be used as selector.
    /// User can handle Navigation by adding/inserting/deleting NavigationItem.
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    public class Switch : Button
    {
        private const int aniTime = 100; // will be defined in const file later
        private ImageView trackImage;
        private ImageView thumbImage;
        private Animation handlerAni = null;
        private SwitchStyle switchStyle;

        /// <summary>
        /// Creates a new instance of a Switch.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Switch() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a Switch with style.
        /// </summary>
        /// <param name="style">Create Switch by special style defined in UX.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Switch(string style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a Switch with style.
        /// </summary>
        /// <param name="style">Create Switch by style customized by user.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Switch(SwitchStyle style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// An event for the item selected signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public event EventHandler<SelectEventArgs> SelectedEvent;

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new SwitchStyle Style => switchStyle;

        /// <summary>
        /// Background image's resource url in Switch.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string SwitchBackgroundImageURL
        {
            get
            {
                return switchStyle?.Track?.ResourceUrl?.All;
            }
            set
            {
                if (value != null)
                {
                    //CreateSwitchBackgroundImageAttributes();
                    if (switchStyle.Track.ResourceUrl == null)
                    {
                        switchStyle.Track.ResourceUrl = new StringSelector();
                    }
                    switchStyle.Track.ResourceUrl.All = value;
                    //RelayoutRequest();
                }
            }
        }

        /// <summary>
        /// Background image's resource url selector in Switch.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public StringSelector SwitchBackgroundImageURLSelector
        {
            get
            {
                return (StringSelector)switchStyle?.Track?.ResourceUrl;
            }
            set
            {
                if (value != null)
                {
                    //CreateSwitchBackgroundImageAttributes();
                    switchStyle.Track.ResourceUrl = value.Clone() as StringSelector;
                    //RelayoutRequest();
                }
            }
        }

        /// <summary>
        /// Handler image's resource url in Switch.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string SwitchHandlerImageURL
        {
            get
            {
                return switchStyle?.Thumb?.ResourceUrl?.All;
            }
            set
            {
                if (value != null)
                {
                    //CreateSwitchHandlerImageAttributes();
                    if (switchStyle.Thumb.ResourceUrl == null)
                    {
                        switchStyle.Thumb.ResourceUrl = new StringSelector();
                    }
                    switchStyle.Thumb.ResourceUrl.All = value;
                    //RelayoutRequest();
                }
            }
        }

        /// <summary>
        /// Handler image's resource url selector in Switch.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public StringSelector SwitchHandlerImageURLSelector
        {
            get
            {
                return (StringSelector)switchStyle?.Thumb?.ResourceUrl;
            }
            set
            {
                if (value != null)
                {
                    //CreateSwitchHandlerImageAttributes();
                    switchStyle.Thumb.ResourceUrl = value.Clone() as StringSelector;
                    //RelayoutRequest();
                }
            }
        }

        /// <summary>
        /// Handler image's size in Switch.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Size SwitchHandlerImageSize
        {
            get
            {
                return switchStyle?.Thumb?.Size ?? new Size(0, 0);
            }
            set
            {
                //CreateSwitchHandlerImageAttributes();
                switchStyle.Thumb.Size = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Dispose Switch and all children on it.
        /// </summary>
        /// <param name="type">Dispose type.</param>
        /// <since_tizen> 6 </since_tizen>
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed) return;

            if (type == DisposeTypes.Explicit)
            {
                if (null != handlerAni)
                {
                    if (handlerAni.State == Animation.States.Playing)
                    {
                        handlerAni.Stop();
                    }
                    handlerAni.Dispose();
                    handlerAni = null;
                }

                Utility.Dispose(thumbImage);
                Utility.Dispose(trackImage);
            }

            base.Dispose(type);
        }

        /// <summary>
        /// Called after a key event is received by the view that has had its focus set.
        /// </summary>
        /// <param name="key">The key event.</param>
        /// <returns>True if the key event should be consumed.</returns>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool OnKey(Key key)
        {
            if (!IsEnabled) return false;

            bool ret = base.OnKey(key);
            if (key.State == Key.StateType.Up)
            {
                if (key.KeyPressedName == "Return")
                {
                    OnSelect();
                }
            }

            return ret;
        }

        /// <summary>
        /// Called after a touch event is received by the owning view.<br />
        /// CustomViewBehaviour.REQUIRES_TOUCH_EVENTS must be enabled during construction. See CustomView(ViewWrapperImpl.CustomViewBehaviour behaviour).<br />
        /// </summary>
        /// <param name="touch">The touch event.</param>
        /// <returns>True if the event should be consumed.</returns>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool OnTouch(Touch touch)
        {
            if(!IsEnabled) return false;

            PointStateType state = touch.GetState(0);
            bool ret = base.OnTouch(touch);
            switch (state)
            {
                case PointStateType.Up:
                    OnSelect();
                    break;
                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Get Switch attribues.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override ViewStyle GetAttributes()
        {
            return new SwitchStyle();
        }

        private void Initialize()
        {
            switchStyle = controlStyle as SwitchStyle;
            if (null == switchStyle)
            {
                throw new Exception("Switch style parse error.");
            }

            switchStyle.IsSelectable = true;
            handlerAni = new Animation(aniTime);
            trackImage = new ImageView()
            {
                ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                PositionUsesPivotPoint = true,
                WidthResizePolicy = ResizePolicyType.FillToParent,
                HeightResizePolicy = ResizePolicyType.FillToParent,
                Name = "SwitchBackgroundImage",
            };
            Add(trackImage);
            trackImage.ApplyStyle(switchStyle.Track);

            thumbImage = new ImageView()
            {
                ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                PositionUsesPivotPoint = true,
                Name = "SwitchHandlerImage",
            };
            trackImage.Add(thumbImage);
            thumbImage.ApplyStyle(switchStyle.Thumb);
        }

        /// <summary>
        /// Theme change callback when theme is changed, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            SwitchStyle tempAttributes = StyleManager.Instance.GetAttributes(style) as SwitchStyle;
            if (null != tempAttributes)
            {
                switchStyle.CopyFrom(tempAttributes);
                controlStyle = switchStyle;
                //RelayoutRequest();
            }
        }

        private void OnSelect()
        {
            if (handlerAni.State == Animation.States.Playing)
            {
                handlerAni.Stop();
            }
            handlerAni.Clear();
            handlerAni.AnimateTo(thumbImage, "PositionX", Size2D.Width - thumbImage.Size2D.Width - thumbImage.Position2D.X);
            trackImage.Opacity = 0.5f; ///////need defined by UX
            handlerAni.AnimateTo(trackImage, "Opacity", 1);
            handlerAni.Play();

            if (SelectedEvent != null)
            {
                SelectEventArgs eventArgs = new SelectEventArgs();
                eventArgs.IsSelected = IsSelected;
                SelectedEvent(this, eventArgs);
            }
        }

        /// <summary>
        /// SelectEventArgs is a class to record item selected arguments which will sent to user.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public class SelectEventArgs : EventArgs
        {
            /// <summary> Select state of Switch </summary>
            /// <since_tizen> 6 </since_tizen>
            public bool IsSelected;
        }
    }
}
