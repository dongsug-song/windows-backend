/*
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
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;
using System.ComponentModel;

namespace Tizen.NUI.Components
{
    /// <summary>
    /// Popup is one kind of common component, it can be used as popup window.
    /// User can handle Popup button count, head title and content area.
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    public class Popup : Control
    {
        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ButtonHeightProperty = BindableProperty.Create("ButtonHeight", typeof(int), typeof(Popup), default(int), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Popup)bindable;
            if (newValue != null)
            {
                instance.popupStyle.Buttons.Size.Height = (int)newValue;
                instance.btGroup.Itemheight = (int)newValue;
                instance.UpdateView();
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Popup)bindable;
            return instance.popupStyle?.Buttons?.Size?.Height ?? 0;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ButtonTextPointSizeProperty = BindableProperty.Create("ButtonTextPointSize", typeof(float), typeof(Popup), default(float), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Popup)bindable;
            if (newValue != null)
            {
                if (instance.popupStyle.Buttons.Text.PointSize == null)
                {
                    instance.popupStyle.Buttons.Text.PointSize = new Selector<float?>();
                }
                instance.popupStyle.Buttons.Text.PointSize.All = (float)newValue;
                instance.btGroup.ItemPointSize = (float)newValue;
                instance.UpdateView();
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Popup)bindable;
            return instance.popupStyle?.Buttons?.Text?.PointSize?.All ?? 0;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ButtonFontFamilyProperty = BindableProperty.Create("ButtonFontFamily", typeof(string), typeof(Popup), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Popup)bindable;
            if (newValue != null)
            {
                instance.popupStyle.Buttons.Text.FontFamily = (string)newValue;
                instance.btGroup.ItemFontFamily = (string)newValue;
                instance.UpdateView();
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Popup)bindable;
            return instance.popupStyle?.Buttons?.Text?.FontFamily;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ButtonTextColorProperty = BindableProperty.Create("ButtonTextColor", typeof(Color), typeof(Popup), Color.Transparent, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Popup)bindable;
            if (newValue != null)
            {  
                if (instance.popupStyle.Buttons.Text.TextColor == null)
                {
                    instance.popupStyle.Buttons.Text.TextColor = new Selector<Color>();
                }
                instance.popupStyle.Buttons.Text.TextColor.All = (Color)newValue;
                instance.btGroup.ItemTextColor = (Color)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Popup)bindable;
            return instance.popupStyle?.Buttons?.Text?.TextColor?.All;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ButtonOverLayBackgroundColorSelectorProperty = BindableProperty.Create("ButtonOverLayBackgroundColorSelector", typeof(Selector<Color>), typeof(Popup), new Selector<Color>(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Popup)bindable;
            if (newValue != null)
            {
                instance.popupStyle.Buttons.Overlay.BackgroundColor = (Selector<Color>)newValue;
                instance.btGroup.ItemBackgroundColorSelector = (Selector<Color>)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Popup)bindable;
            return instance.popupStyle?.Buttons?.Overlay?.BackgroundColor;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ButtonTextAlignmentProperty = BindableProperty.Create("ButtonTextAlignment", typeof(HorizontalAlignment), typeof(Popup), new HorizontalAlignment(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Popup)bindable;
            if (newValue != null)
            {
                instance.popupStyle.Buttons.Text.HorizontalAlignment = (HorizontalAlignment)newValue;
                instance.btGroup.ItemTextAlignment = (HorizontalAlignment)newValue;
                instance.UpdateView();
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Popup)bindable;
            return instance.popupStyle?.Buttons?.Text?.HorizontalAlignment ?? HorizontalAlignment.Center;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ButtonBackgroundProperty = BindableProperty.Create("ButtonBackground", typeof(string), typeof(Popup), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Popup)bindable;
            if (newValue != null)
            {
                if (instance.popupStyle.Buttons.Background.ResourceUrl == null)
                {
                    instance.popupStyle.Buttons.Background.ResourceUrl = new Selector<string>();
                }
                instance.btGroup.ItemBackground = (string)newValue;
                instance.popupStyle.Buttons.Background.ResourceUrl.All = (string)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Popup)bindable;
            return instance.popupStyle?.Buttons?.Background?.ResourceUrl?.All;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ButtonBackgroundBorderProperty = BindableProperty.Create("ButtonBackgroundBorder", typeof(Rectangle), typeof(Popup), new Rectangle(0, 0, 0, 0), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Popup)bindable;
            if (newValue != null)
            {
                if (instance.popupStyle.Buttons.Background.Border == null)
                {
                    instance.popupStyle.Buttons.Background.Border = new Selector<Rectangle>();
                }
                instance.popupStyle.Buttons.Background.Border.All = (Rectangle)newValue;
                instance.btGroup.ItemBackgroundBorder = (Rectangle)newValue;
                instance.UpdateView();
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Popup)bindable;
            return instance.popupStyle?.Buttons?.Background?.Border?.All;
        });

        private TextLabel titleText;
        private PopupStyle popupStyle;
        private ButtonGroup btGroup = null;
        private Window window = null;

        /// <summary>
        /// Creates a new instance of a Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Popup() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a Popup with style.
        /// </summary>
        /// <param name="style">Create Popup by special style defined in UX.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Popup(string style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a Popup with attributes.
        /// </summary>
        /// <param name="attributes">Create Popup by attributes customized by user.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Popup(PopupStyle attributes) : base(attributes)
        {
            Initialize();
        }

        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Post(Window win)
        {
            window = win;
            window.Add(this);
        }

        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddButton(string buttonText)
        {   
            Button btn = new Button(popupStyle.Buttons);
            btn.Style.Text.Text = buttonText;
            btn.ClickEvent += ButtonClickEvent;
            btGroup.AddItem(btn);
            UpdateView();
        }

        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddButton(string buttonText, string style)
        {
            AddButton(buttonText);
        }

        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddButton(string buttonText, ButtonStyle style)
        {
            if (popupStyle.Buttons != null && style != null)
            {
                popupStyle.Buttons.CopyFrom(style);
            }
            AddButton(buttonText);
        }

        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        Button GetButton(int index)
        {
            return btGroup.GetButton(index);
        }

        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        void RemoveButton(int index)
        {
            btGroup.RemoveItem(index);
        }
		
        /// <summary>
        /// An event for the button clicked signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public event EventHandler<ButtonClickEventArgs> PopupButtonClickEvent;

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PopupStyle Style => popupStyle;

        /// <summary>
        /// Title text string in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string TitleText
        {
            get
            {
                return popupStyle?.Title?.Text?.All;
            }
            set
            {
                if (value != null)
                {
                    if (popupStyle.Title.Text == null)
                    {
                        popupStyle.Title.Text = new StringSelector();
                    }
                    popupStyle.Title.Text.All = value;
                }
            }
        }

        /// <summary>
        /// Title text point size in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public float TitlePointSize
        {
            get
            {
                return popupStyle?.Title?.PointSize?.All ?? 0;
            }
            set
            {
                if (popupStyle.Title.PointSize == null)
                {
                    popupStyle.Title.PointSize = new FloatSelector();
                }
                popupStyle.Title.PointSize.All = value;
            }
        }

        /// <summary>
        /// Title text color in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Color TitleTextColor
        {
            get
            {
                return popupStyle?.Title?.TextColor?.All;
            }
            set
            {
                if (popupStyle.Title.TextColor == null)
                {
                    popupStyle.Title.TextColor = new ColorSelector();
                }
                popupStyle.Title.TextColor.All = value;
            }
        }

        /// <summary>
        /// Title text horizontal alignment in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public HorizontalAlignment TitleTextHorizontalAlignment
        {
            get
            {
                return popupStyle?.Title?.HorizontalAlignment ?? HorizontalAlignment.Center;
            }
            set
            {
                popupStyle.Title.HorizontalAlignment = value;
            }
        }

        /// <summary>
        /// Title text's position in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Position TitleTextPosition
        {
            get
            {
                return popupStyle?.Title?.Position ?? new Position(0, 0, 0);
            }
            set
            {
                popupStyle.Title.Position = value;
            }
        }

        /// <summary>
        /// Title text's height in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public int TitleHeight
        {
            get
            {
                return (int)(popupStyle?.Title?.Size?.Height ?? 0);
            }
            set
            {
                popupStyle.Title.Size.Height = value;
            }
        }

        /// <summary>
        /// Content view in Popup, only can be gotten.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public View ContentView
        {
            get;
            private set;
        }

        /// <summary>
        /// Button count in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public int ButtonCount
        {
            get;
            set;
        }

        /// <summary>
        /// Shadow offset in Popup, including left, right, up and bottom offset.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Vector4 ShadowOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Button height in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public int ButtonHeight
        {
            get
            {
                return (int)GetValue(ButtonHeightProperty);
            }
            set
            {
                SetValue(ButtonHeightProperty, value);
            }
        }

        /// <summary>
        /// Button text point size in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public float ButtonTextPointSize
        {
            get
            {
                return (float)GetValue(ButtonTextPointSizeProperty);
            }
            set
            {
                SetValue(ButtonTextPointSizeProperty, value);
            }
        }

        /// <summary>
        /// Button text font family in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string ButtonFontFamily
        {
            get
            {           
                return (string)GetValue(ButtonFontFamilyProperty);
            }
            set
            {
                SetValue(ButtonFontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Button text color in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Color ButtonTextColor
        {
            get
            {
                return (Color)GetValue(ButtonTextColorProperty);
            }
            set
            {
                SetValue(ButtonTextColorProperty, value);
            }
        }

        /// <summary>
        /// Button overlay background color selector in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Selector<Color> ButtonOverLayBackgroundColorSelector
        {
            get
            {
                return (Selector<Color>)GetValue(ButtonOverLayBackgroundColorSelectorProperty);
            }
            set
            {
                SetValue(ButtonOverLayBackgroundColorSelectorProperty, value);
            }
        }

        /// <summary>
        /// Button text horizontal alignment in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public HorizontalAlignment ButtonTextAlignment
        {
            get
            {   
                return (HorizontalAlignment)GetValue(ButtonTextAlignmentProperty);
            }
            set
            {
                SetValue(ButtonTextAlignmentProperty, value);
            }
        }

        /// <summary>
        /// Button background image's resource url in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ButtonBackground
        {
            get
            {     
                return (string)GetValue(ButtonBackgroundProperty);
            }
            set
            {
                SetValue(ButtonBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Button background image's border in Popup.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle ButtonBackgroundBorder
        {
            get
            {
                
                return (Rectangle)GetValue(ButtonBackgroundBorderProperty);
            }
            set
            {
                SetValue(ButtonBackgroundBorderProperty, value);
            }
        }

        /// <summary>
        /// Set button text by index.
        /// </summary>
        /// <param name="index">Button index.</param>
        /// <param name="text">Button text string.</param>
        /// <since_tizen> 6 </since_tizen>
        public void SetButtonText(int index, string text)
        {}

        /// <summary>
        /// Dispose Popup and all children on it.
        /// </summary>
        /// <param name="type">Dispose type.</param>
        /// <since_tizen> 6 </since_tizen>
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                if (titleText != null)
                {
                    Remove(titleText);
                    titleText.Dispose();
                    titleText = null;
                }
                if (ContentView != null)
                {
                    Remove(ContentView);
                    ContentView.Dispose();
                    ContentView = null;
                }

                if (btGroup != null)
                {
                    btGroup.Dispose();
                    btGroup = null;
                }
            }

            base.Dispose(type);
        }

        /// <summary>
        /// Focus gained callback.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void OnFocusGained()
        {
            base.OnFocusGained();
        }

        /// <summary>
        /// Focus lost callback.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void OnFocusLost()
        {
            base.OnFocusLost();
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ApplyStyle(ViewStyle viewStyle)
        {
            base.ApplyStyle(viewStyle);

            PopupStyle popupStyle = viewStyle as PopupStyle;
        }

        /// <summary>
        /// Get Popup attribues.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override ViewStyle GetAttributes()
        {
            if (null == popupStyle)
            {
                return new PopupStyle();
            }
            else
            {
                return popupStyle;
            }
        }

        /// <summary>
        /// Theme change callback when theme is changed, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            PopupStyle tempAttributes = StyleManager.Instance.GetAttributes(style) as PopupStyle;
            if (tempAttributes != null)
            {
                popupStyle.CopyFrom(tempAttributes);
                RelayoutRequest();
            }
        }

        private void Initialize()
        {
            popupStyle = controlStyle as PopupStyle;
            if (popupStyle == null)
            {
                throw new Exception("Popup style parse error.");
            }

            LeaveRequired = true;

            // ContentView
            ContentView = new View()
            {
                ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                PositionUsesPivotPoint = true
            };
            Add(ContentView);
            ContentView.RaiseToTop();

            // Title
            titleText = new TextLabel();
            Add(titleText);
            titleText.ApplyStyle(popupStyle.Title);

            // Button
            btGroup = new ButtonGroup(this);
        }

        private void UpdateView()
        {
            btGroup.UpdateButton();
            UpdateContentView();
        }

        private void ButtonClickEvent(object sender, Button.ClickEventArgs e)
        {
            if (PopupButtonClickEvent != null && btGroup.itemGroup != null)
            {
                Button button = sender as Button;
                for (int i = 0; i < btGroup.Count; i++)
                {
                    if (button == btGroup.itemGroup[i])
                    {
                        ButtonClickEventArgs args = new ButtonClickEventArgs();
                        args.ButtonIndex = i;
                        PopupButtonClickEvent(this, args);
                    }
                }
            }
        }

        private void UpdateContentView()
        {
            int titleX = 0;
            int titleY = 0;
            int titleH = 0;
            if (popupStyle.Title.Size != null)
            {
                titleH = (int)titleText.Size.Height;
            }
            if (popupStyle.Title.Position != null)
            {
                titleX = (int)popupStyle.Title.Position.X;
                titleY = (int)popupStyle.Title.Position.Y;
            }
            int buttonH = (int)popupStyle.Buttons.Size.Height;

            ContentView.Size = new Size(Size.Width - titleX * 2, Size.Height - titleY - titleH - buttonH);
            ContentView.Position = new Position(titleX, titleY + titleH);
            ContentView.RaiseToTop();
        }

        /// <summary>
        /// ButtonClickEventArgs is a class to record button click event arguments which will sent to user.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public class ButtonClickEventArgs : EventArgs
        {
            /// <summary> Button index which is clicked in Popup </summary>
            /// <since_tizen> 6 </since_tizen>
            public int ButtonIndex;
        }
    }
}
