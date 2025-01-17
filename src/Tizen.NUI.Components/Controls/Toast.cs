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
using Tizen.NUI.Binding;

namespace Tizen.NUI.Components
{
    /// <summary>
    /// Use a toast to provide simple messages when the user does not need to make an additional action or confirmation.
    /// Unlike other popups, a toast only has the body field as it is just used for providing simple feedback to user actions.
    /// A toast will automatically disappear after a certain time.
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    public class Toast : Control
    {
        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty MessageProperty = BindableProperty.Create("Message", typeof(string), typeof(Toast), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Toast)bindable;
            if (newValue != null)
            {
                instance.strText = (string)(newValue);
                instance.textLabel.Text = instance.strText;
                instance.UpdateText();
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Toast)bindable;
            return instance.strText;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty DurationProperty = BindableProperty.Create("Duration", typeof(uint), typeof(Toast), default(uint), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var instance = (Toast)bindable;
            if (newValue != null)
            {
                instance.toastStyle.Duration = (uint)newValue;
                instance.timer.Interval = (uint)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var instance = (Toast)bindable;
            return instance.toastStyle.Duration ?? instance.duration;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Toast FromText(string text, uint duration) 
        {
            Toast toast = new Toast();
            toast.Message = text;
            toast.Duration = duration;
            return toast;
        }

        private Window window = null;
		protected TextLabel[] textLabels = null;
        protected TextLabel textLabel = null;
        private ToastStyle toastStyle = null;
        private string strText = null;
        private NPatchVisual toastBackground = null;
        private Timer timer = null;
        private string[] textArray = null;

        private readonly int maxTextAreaWidth = 808;
        private readonly int textPaddingLeft = 96;
        private readonly int textPaddingTop = 38;
        private readonly uint duration = 3000;

        /// <summary>
        /// Construct Toast with null.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Toast() : base()
        {
            Initialize();
        }

        /// <summary>
        /// The constructor of the Toast class with specific Style.
        /// </summary>
        /// <param name="Style">Construct Style</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Toast(ToastStyle style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// Constructor of the Toast class with special style.
        /// </summary>
        /// <param name="style"> style name </param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Toast(string style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// Gets or sets the text array of toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string[] TextArray
        {
            get
            {
                return textArray;
            }
            set
            {
                if (null != value)
                {
                    textArray = value;
                    string message = "";
                    foreach (string text in textArray)
                    {
                        message += text + "\n";
                    }
                    Message = message;
                }
            }
        }

        /// <summary>
        /// Gets or sets text point size in toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public float PointSize
        {
            get
            {
                return (float)toastStyle.Text?.PointSize?.All;
            }
            set
            {
                if (null == toastStyle.Text.PointSize)
                {
                    toastStyle.Text.PointSize = new FloatSelector();
                }
                toastStyle.Text.PointSize.All = value;
            }
        }

        /// <summary>
        /// Gets or sets text font family in toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string FontFamily
        {
            get
            {
                return toastStyle.Text?.FontFamily.All;
            }
            set
            {
                toastStyle.Text.FontFamily = value;
            }
        }

        /// <summary>
        /// Gets or sets text color in toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Color TextColor
        {
            get
            {
                return toastStyle.Text?.TextColor?.All;
            }
            set
            {
                //CreateTextAttributes();
                if (null == toastStyle.Text.TextColor)
                {
                    toastStyle.Text.TextColor = new ColorSelector();
                }
                toastStyle.Text.TextColor.All = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets text horizontal alignment in toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public HorizontalAlignment TextAlignment
        {
            get
            {
                return toastStyle.Text?.HorizontalAlignment ?? HorizontalAlignment.Center;
            }
            set
            {
                //CreateTextAttributes();
                toastStyle.Text.HorizontalAlignment = value;
                //RelayoutRequest();
            }
        }

        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ToastStyle Style
        {
            get
            {
                if (null == toastStyle)
                {
                    return new ToastStyle();
                }
                else
                {
                    return toastStyle;
                }
            }
        }

        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Post(Window win)
        {
            window = win;
            window.Add(this);
            this.Position.X = (window.Size.Width - this.Size.Width) / 2;
            this.Position.Y = (window.Size.Height - this.Size.Height) / 2;
        }

        /// <summary>
        /// Gets or sets the text toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets text padding in toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Extents TextPadding
        {
            get
            {
                return toastStyle.Text.Padding;
            }
            set
            {
                if (null != value)
                {
                    //CreateTextAttributes();
                    toastStyle.Text.Padding.CopyFrom(value);

                    //if (null == textPadding)
                    //{
                    //    textPadding = new Extents((ushort start, ushort end, ushort top, ushort bottom) =>
                    //    {
                    //        toastAttributes.TextAttributes.Padding.Start = start;
                    //        toastAttributes.TextAttributes.Padding.End = end;
                    //        toastAttributes.TextAttributes.Padding.Top = top;
                    //        toastAttributes.TextAttributes.Padding.Bottom = bottom;
                    //        RelayoutRequest();
                    //    }, value.Start, value.End, value.Top, value.Bottom);
                    //}
                    //else
                    //{
                    //    textPadding.CopyFrom(value);
                    //}

                    //RelayoutRequest();
                }
            }
        }

        /// <summary>
        /// Gets or sets text line height in toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public uint TextLineHeight { get; set; }

        /// <summary>
        /// Gets or sets text line space in toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public uint TextLineSpace { get; set; }

        /// <summary>
        /// Gets or sets duration of toast.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public uint Duration
        {
            get
            {
                return (uint)GetValue(DurationProperty);
            }
            set
            {
                SetValue(DurationProperty, value);
            }
        }

        /// <summary>
        /// Dispose ToastPopup.
        /// </summary>
        /// <param name="type">dispose types.</param>
        /// <since_tizen> 6 </since_tizen>
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                this.VisibilityChanged -= OnVisibilityChanged;
                if (null != timer)
                {
                    timer.Tick -= OnTick;
                    timer.Dispose();
                    timer = null;
                }

                if (null != textLabel)
                {
                    Utility.Dispose(textLabel);
                }
            }

            base.Dispose(type);
        }

        /// <summary>
        /// Relayout control's elements
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void UpdateText()
        {
            if (window == null)
            {
                //return;
            }

            int _textPaddingLeft = toastStyle.Text?.Padding.Start ?? textPaddingLeft;
            int _textPaddingRight = toastStyle.Text?.Padding.End ?? _textPaddingLeft;
            int _textPaddingTop = toastStyle.Text?.Padding.Top ?? textPaddingTop;
            int _textPaddingBottom = toastStyle.Text?.Padding.Bottom ?? _textPaddingTop;

            int _textAreaWidth = (int)Size.Width - _textPaddingLeft - _textPaddingRight;
            int _textAreaHeight = (int)Size.Height - _textPaddingTop - _textPaddingBottom;
            _textAreaWidth = _textAreaWidth > maxTextAreaWidth ? maxTextAreaWidth : _textAreaWidth;        
            if (textLabel != null)
            {
                textLabel.Position = new Position(_textPaddingLeft, _textPaddingTop);
                textLabel.Size = new Size(_textAreaWidth, _textAreaHeight);
                if (LayoutDirection == ViewLayoutDirectionType.RTL)
                {
                    textLabel.ParentOrigin = Tizen.NUI.ParentOrigin.TopRight;
                    textLabel.PivotPoint = Tizen.NUI.PivotPoint.TopRight;
                    textLabel.PositionUsesPivotPoint = true;
                }
            }
        }

        /// <summary>
        /// Get Toast attribues.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override ViewStyle GetAttributes()
        {
            if (null == toastStyle)
            {
                return new ToastStyle();
            }
            else
            {
                return toastStyle;
            }
        }

        private void Initialize()
        {
            toastStyle = controlStyle as ToastStyle;
            if (toastStyle == null)
            {
                throw new Exception("Toast attribute parse error.");
            }

            toastBackground = new NPatchVisual();
            if (toastBackground == null)
            {
                throw new Exception("Toast background is null.");
            }
            SetToastBackground();

            textLabel = new TextLabel();
            if (textLabel == null)
            {
                throw new Exception("Toast textLabel is null.");
            }
            textLabel.TextColor = Color.White;
            this.Add(textLabel);

            this.VisibilityChanged += OnVisibilityChanged;
            timer = new Timer(toastStyle.Duration ?? duration);
            timer.Tick += OnTick;
            timer.Start();
        }

        private bool OnTick(object sender, EventArgs e)
        {
            Hide();
            return false;
        }

        private void OnVisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (true == e.Visibility)
            {
                timer.Start();
            }
        }

        private void SetToastBackground()
        {
            if (null != toastStyle?.Background?.ResourceUrl)
            {
                toastBackground.URL = toastStyle.Background.ResourceUrl.All;
            }
            if (null != toastStyle?.Background?.Border)
            {
                toastBackground.Border = toastStyle.Background.Border.All;
            }
            this.Background = toastBackground.OutputVisualMap;
        }
    }
}
