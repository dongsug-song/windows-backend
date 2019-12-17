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
using Tizen.NUI.BaseComponents;
using System.ComponentModel;

namespace Tizen.NUI.Components
{
    /// <summary>
    /// A slider lets users select a value from a continuous or discrete range of values by moving the slider thumb.
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    public class Slider : Control
    {
        // the background track image object
        private ImageView bgTrackImage = null;
        // the slided track image object
        private ImageView slidedTrackImage = null;
        // the thumb image object
        private ImageView thumbImage = null;
        // the background thumb image object
        private ImageView bgThumbImage = null;
        // the low indicator image object
        private ImageView lowIndicatorImage = null;
        // the high indicator image object
        private ImageView highIndicatorImage = null;
        // the low indicator text object
        private TextLabel lowIndicatorText = null;
        // the high indicator text object
        private TextLabel highIndicatorText = null;
        // the attributes of the slider
        private SliderStyle sliderStyle = null;
        // the direction type
        private DirectionType direction = DirectionType.Horizontal;
        // the indicator type
        private IndicatorType indicatorType = IndicatorType.None;
        private const float round = 0.5f;
        // the minimum value
        private float? minValue = null;
        // the maximum value
        private float? maxValue = null;
        // the current value
        private float? curValue = null;
        // the size of the low indicator
        private Size lowIndicatorSize = null;
        // the size of the high indicator
        private Size highIndicatorSize = null;
        // the track thickness value
        private uint? trackThickness = null;
        // the value of the space between track and indicator object
        private Extents spaceBetweenTrackAndIndicator = null;
        
        private PanGestureDetector panGestureDetector = null;
        private float currentSlidedOffset;
        private EventHandler<ValueChangedArgs> valueChangedHandler;
        private EventHandler<SlidingFinishedArgs> slidingFinishedHandler;
        private EventHandler<StateChangedArgs> stateChangedHandler;

        bool isFocused = false;
        bool isPressed = false;

        /// <summary>
        /// The constructor of the Slider class.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Slider() 
        {
            Initialize();
        }

        /// <summary>
        /// The constructor of the Slider class with specific style.
        /// </summary>
        /// <param name="style">The string to initialize the Slider</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Slider(string style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// The constructor of the Slider class with specific style.
        /// </summary>
        /// <param name="style">The style object to initialize the Slider</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Slider(SliderStyle style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// The value changed event handler.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public event EventHandler<ValueChangedArgs> ValueChangedEvent
        {
            add
            {
                valueChangedHandler += value;
            }
            remove
            {
                valueChangedHandler -= value;
            }
        }

        /// <summary>
        /// The sliding finished event handler.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public event EventHandler<SlidingFinishedArgs> SlidingFinishedEvent
        {
            add
            {
                slidingFinishedHandler += value;
            }
            remove
            {
                slidingFinishedHandler -= value;
            }
        }

        /// <summary>
        /// The state changed event handler.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public event EventHandler<StateChangedArgs> StateChangedEvent
        {
            add
            {
                stateChangedHandler += value;
            }
            remove
            {
                stateChangedHandler -= value;
            }
        }

        /// <summary>
        /// The direction type of slider.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public enum DirectionType
        {
            /// <summary>
            /// The Horizontal type.
            /// </summary>
            /// <since_tizen> 6 </since_tizen>
            Horizontal,

            /// <summary>
            /// The Vertical type.
            /// </summary>
            /// <since_tizen> 6 </since_tizen>
            Vertical
        }

        /// <summary>
        /// The indicator type of slider.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public enum IndicatorType
        {
            /// <summary> Only contains slider bar.</summary>
            /// <since_tizen> 6 </since_tizen>
            None,

            /// <summary> Contains slider bar, IndicatorImage.</summary>
            /// <since_tizen> 6 </since_tizen>
            Image,

            /// <summary> Contains slider bar, IndicatorText.</summary>
            /// <since_tizen> 6 </since_tizen>
            Text
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public SliderStyle Style => sliderStyle;

        /// <summary>
        /// Gets or sets the direction type of slider.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public DirectionType Direction
        {
            get
            {
                return direction;
            }
            set
            {
                if (direction == value)
                {
                    return;
                }
                direction = value;
                RelayoutBaseComponent(false);
                UpdateBgTrackSize();
                UpdateBgTrackPosition();
                UpdateValue();
            }
        }

        /// <summary>
        /// Gets or sets the indicator type, arrow or sign.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public IndicatorType Indicator
        {
            get
            {
                return indicatorType;
            }
            set
            {
                if (indicatorType == value)
                {
                    return;
                }
                indicatorType = value;
                RelayoutBaseComponent(false);
                UpdateBgTrackSize();
                UpdateBgTrackPosition();
                UpdateValue();
            }
        }

        /// <summary>
        /// Gets or sets the minimum value of slider.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public float MinValue
        {
            get
            {
                return minValue ?? 0;
            }
            set
            {
                minValue = value;
                UpdateValue();
            }
        }

        /// <summary>
        /// Gets or sets the maximum value of slider.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public float MaxValue
        {
            get
            {
                return maxValue ?? 100;
            }
            set
            {
                maxValue = value;
                UpdateValue();
            }
        }

        /// <summary>
        /// Gets or sets the current value of slider.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public float CurrentValue
        {
            get
            {
                return curValue ?? 0;
            }
            set
            {
                curValue = value;
                UpdateValue();
            }
        }

        /// <summary>
        /// Gets or sets the size of the thumb image object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Size ThumbSize
        {
            get
            {
                return sliderStyle.Thumb?.Size;
            }
            set
            {
                //CreateThumbAttributes();
                sliderStyle.Thumb.Size = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets the resource url of the thumb image object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string ThumbImageURL
        {
            get
            {
                return sliderStyle.Thumb?.ResourceUrl?.All;
            }
            set
            {
                //CreateThumbAttributes();
                if (sliderStyle.Thumb.ResourceUrl == null)
                {
                    sliderStyle.Thumb.ResourceUrl = new StringSelector(); 
                }
                sliderStyle.Thumb.ResourceUrl.All = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets the resource url selector of the thumb image object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public StringSelector ThumbImageURLSelector
        {
            get
            {
                return (StringSelector)sliderStyle.Thumb?.ResourceUrl;
            }
            set
            {
                //CreateThumbAttributes();
                if (value != null)
                {
                    sliderStyle.Thumb.ResourceUrl = value.Clone() as StringSelector;
                    //RelayoutRequest();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the background track image object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Color BgTrackColor
        {
            get
            {
                return sliderStyle.Track?.BackgroundColor?.All;
            }
            set
            {
                //CreateBackgroundTrackAttributes();
                if (sliderStyle.Track.BackgroundColor == null)
                {
                    sliderStyle.Track.BackgroundColor = new ColorSelector();
                }
                sliderStyle.Track.BackgroundColor.All = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets the color of the slided track image object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Color SlidedTrackColor
        {
            get
            {
                return sliderStyle.Progress?.BackgroundColor?.All;
            }
            set
            {
                //CreateSlidedTrackAttributes();
                if (sliderStyle.Progress.BackgroundColor == null)
                {
                    sliderStyle.Progress.BackgroundColor = new ColorSelector();
                }
                sliderStyle.Progress.BackgroundColor.All = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets the thickness value of the track.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public uint TrackThickness
        {
            get
            {
                return trackThickness ?? 0;
            }
            set
            {
                trackThickness = value;
                if (bgTrackImage != null)
                {
                    if (direction == DirectionType.Horizontal)
                    {
                        bgTrackImage.SizeHeight = (float)trackThickness.Value;
                    }
                    else if (direction == DirectionType.Vertical)
                    {
                        bgTrackImage.SizeWidth = (float)trackThickness.Value;
                    }
                }
                if (slidedTrackImage != null)
                {
                    if (direction == DirectionType.Horizontal)
                    {
                        slidedTrackImage.SizeHeight = (float)trackThickness.Value;
                    }
                    else if (direction == DirectionType.Vertical)
                    {
                        slidedTrackImage.SizeWidth = (float)trackThickness.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the resource url of the low indicator image object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string LowIndicatorImageURL
        {
            get
            {
                return sliderStyle.LowIndicatorImage?.ResourceUrl?.All;
            }
            set
            {
                //CreateLowIndicatorImageAttributes();
                if (sliderStyle.LowIndicatorImage.ResourceUrl == null)
                {
                    sliderStyle.LowIndicatorImage.ResourceUrl = new StringSelector();
                }
                sliderStyle.LowIndicatorImage.ResourceUrl.All = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets the resource url of the high indicator image object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string HighIndicatorImageURL
        {
            get
            {
                return sliderStyle.HighIndicatorImage?.ResourceUrl?.All;
            }
            set
            {
                //CreateHighIndicatorImageAttributes();
                if (sliderStyle.HighIndicatorImage.ResourceUrl == null)
                {
                    sliderStyle.HighIndicatorImage.ResourceUrl = new StringSelector();
                }
                sliderStyle.HighIndicatorImage.ResourceUrl.All = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets the text content of the low indicator text object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string LowIndicatorTextContent
        {
            get
            {
                return sliderStyle.LowIndicator?.Text?.All;
            }
            set
            {
                //CreateLowIndicatorTextAttributes();
                if (sliderStyle.LowIndicator.Text == null)
                {
                    sliderStyle.LowIndicator.Text = new StringSelector();
                }
                sliderStyle.LowIndicator.Text.All = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets the text content of the high indicator text object.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public string HighIndicatorTextContent
        {
            get
            {
                return sliderStyle.HighIndicator?.Text?.All;
            }
            set
            {
                //CreateHighIndicatorTextAttributes();
                if (sliderStyle.HighIndicator.Text == null)
                {
                    sliderStyle.HighIndicator.Text = new StringSelector();
                }
                sliderStyle.HighIndicator.Text.All = value;
                //RelayoutRequest();
            }
        }

        /// <summary>
        /// Gets or sets the size of the low indicator object(image or text).
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Size LowIndicatorSize
        {
            get
            {
                return lowIndicatorSize;
            }
            set
            {
                lowIndicatorSize = value;
                UpdateLowIndicatorSize();
                UpdateBgTrackSize();
                UpdateBgTrackPosition();
                UpdateValue();
            }
        }

        /// <summary>
        /// Gets or sets the size of the high indicator object(image or text).
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public Size HighIndicatorSize
        {
            get
            {
                return highIndicatorSize;
            }
            set
            {
                highIndicatorSize = value;
                UpdateHighIndicatorSize();
                UpdateBgTrackSize();
                UpdateBgTrackPosition();
                UpdateValue();
            }
        }

        /// <summary>
        /// Gets or sets the value of the space between track and indicator.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public uint SpaceBetweenTrackAndIndicator
        {
            get
            {
                return TrackPadding.Start;
            }
            set
            {
                ushort val = (ushort)value;
                TrackPadding = new Extents(val, val, val, val);
            }
        }

        /// <summary>
        /// Gets or sets the value of the space between track and indicator.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Extents TrackPadding
        {
            get
            {
                return spaceBetweenTrackAndIndicator;
            }
            set
            {
                spaceBetweenTrackAndIndicator = value;
                UpdateComponentByIndicatorTypeChanged();
                UpdateBgTrackSize();
                UpdateBgTrackPosition();
                UpdateValue();
            }
        }

        /// <summary>
        /// Focus gained callback.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void OnFocusGained()
        {
            //State = ControlStates.Focused;
            UpdateState(true, isPressed);
            base.OnFocusGained();
        }

        /// <summary>
        /// Focus Lost callback.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void OnFocusLost()
        {
            //State = ControlStates.Normal;
            UpdateState(false, isPressed);
            base.OnFocusLost();
        }

        /// <summary>
        /// Get Slider attribues.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override ViewStyle GetAttributes()
        {
            return new SliderStyle();
        }

        /// <summary>
        /// Dispose Slider.
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
                if (null != panGestureDetector)
                {
                    if (null != thumbImage)
                    {
                        panGestureDetector.Detach(thumbImage);
                    }
                    panGestureDetector.Detected -= OnPanGestureDetected;
                    panGestureDetector.Dispose();
                    panGestureDetector = null;
                }
                
                if (null != thumbImage)
                {
                    thumbImage.TouchEvent -= OnTouchEventForThumb;
                    Utility.Dispose(thumbImage);
                }
                Utility.Dispose(bgThumbImage);
                Utility.Dispose(slidedTrackImage);
                if (null != bgTrackImage)
                {
                    bgTrackImage.TouchEvent -= OnTouchEventForBgTrack;
                    Utility.Dispose(bgTrackImage);
                }
                Utility.Dispose(lowIndicatorImage);
                Utility.Dispose(highIndicatorImage);
                Utility.Dispose(lowIndicatorText);
                Utility.Dispose(highIndicatorText);
            }

            base.Dispose(type);
        }

        /// <summary>
        /// Update Slider by attributes.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnUpdate()
        {
            if (sliderStyle.Track != null && bgTrackImage == null)
            {
                bgTrackImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true
                };
                this.Add(bgTrackImage);
                bgTrackImage.TouchEvent += OnTouchEventForBgTrack;
            }
            if (sliderStyle.Progress != null && slidedTrackImage == null)
            {
                slidedTrackImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };
                if (bgTrackImage != null)
                {
                    bgTrackImage.Add(slidedTrackImage);
                }
            }
            if (sliderStyle.ThumbBackground != null && bgThumbImage == null)
            {
                bgThumbImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };
                if (slidedTrackImage != null)
                {
                    slidedTrackImage.Add(bgThumbImage);
                }
            }
            if (sliderStyle.Thumb != null && thumbImage == null)
            {
                thumbImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    ParentOrigin = NUI.ParentOrigin.Center,
                    PivotPoint = NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true
                };
                if (bgThumbImage != null)
                {
                    bgThumbImage.Add(thumbImage);
                }
                thumbImage.TouchEvent += OnTouchEventForThumb;

                panGestureDetector = new PanGestureDetector();
                panGestureDetector.Attach(thumbImage);
                panGestureDetector.Detected += OnPanGestureDetected;
            }
            if (sliderStyle.LowIndicatorImage!= null && lowIndicatorImage == null)
            {
                lowIndicatorImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };
                this.Add(lowIndicatorImage);
            }
            if (sliderStyle.HighIndicatorImage!= null && highIndicatorImage == null)
            {
                highIndicatorImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };
                this.Add(highIndicatorImage);
            }
            if (sliderStyle.LowIndicator!= null && lowIndicatorText == null)
            {
                lowIndicatorText = new TextLabel()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };
                this.Add(lowIndicatorText);
            }
            if (sliderStyle.HighIndicator != null && highIndicatorText == null)
            {
                highIndicatorText = new TextLabel()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };
                this.Add(highIndicatorText);
            }

            RelayoutBaseComponent();

            UpdateComponentByIndicatorTypeChanged();
            UpdateBgTrackSize();
            UpdateBgTrackPosition();
            UpdateLowIndicatorSize();
            UpdateHighIndicatorSize();
            UpdateValue();
        }

        /// <summary>
        /// Theme change callback when theme is changed, this callback will be trigger.
        /// </summary>
        /// <param name="sender">serder object</param>
        /// <param name="e">ThemeChangeEventArgs</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            SliderStyle tempAttributes = StyleManager.Instance.GetAttributes(style) as SliderStyle;
            if (tempAttributes != null)
            {
                sliderStyle.CopyFrom(tempAttributes);
                RelayoutRequest();
            }
        }

        private void Initialize()
        {
            sliderStyle = controlStyle as SliderStyle;
            if (null == sliderStyle)
            {
                throw new Exception("Fail to get the slider style.");
            }

            if (null != sliderStyle.Progress)
            {
                CreateSlidedTrackAttributes();
            }

           
            if (null != sliderStyle.LowIndicator)
            {
                CreateLowIndicatorTextAttributes();
            }

            if (null != sliderStyle.HighIndicator)
            {
                CreateHighIndicatorTextAttributes();
            }

            if (null != sliderStyle.Track)
            {
                CreateBackgroundTrackAttributes();
            }

            if (null != sliderStyle.Thumb)
            {
                CreateThumbAttributes();
            }

            if (null != sliderStyle.ThumbBackground)
            {
                CreateThumbBackgroundAttributes();
            }

            currentSlidedOffset = 0;
            isFocused = false;
            isPressed = false;
            LayoutDirectionChanged += OnLayoutDirectionChanged;
        }

        private void OnLayoutDirectionChanged(object sender, LayoutDirectionChangedEventArgs e)
        {
            RelayoutRequest();
        }

        private void CreateSlidedTrackAttributes()
        {
            if (null == slidedTrackImage)
            {
                slidedTrackImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };

                if (bgTrackImage != null)
                {
                    bgTrackImage.Add(slidedTrackImage);
                }

                if (null != bgThumbImage)
                {
                    slidedTrackImage.Add(bgThumbImage);
                }
            }

            if (null == sliderStyle.Progress)
            {
                sliderStyle.Progress = new ImageViewStyle();
            }

            slidedTrackImage.ApplyStyle(sliderStyle.Progress);
        }

        private void CreateLowIndicatorTextAttributes()
        {
            if (null == lowIndicatorText)
            {
                lowIndicatorText = new TextLabel()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };
                this.Add(lowIndicatorText);
            }

            if (null == sliderStyle.LowIndicator)
            {
                sliderStyle.LowIndicator = new TextLabelStyle();
            }

            lowIndicatorText.ApplyStyle(sliderStyle.LowIndicator);
        }

        private void CreateHighIndicatorTextAttributes()
        {
            if (null == highIndicatorText)
            {
                highIndicatorText = new TextLabel()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };
                this.Add(highIndicatorText);
            }

            if (null == sliderStyle.HighIndicator)
            {
                sliderStyle.HighIndicator = new TextLabelStyle();
            }

            highIndicatorText.ApplyStyle(sliderStyle.HighIndicator);
        }

        private void CreateBackgroundTrackAttributes()
        {
            if (null == bgTrackImage)
            {
                bgTrackImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true
                };
                this.Add(bgTrackImage);

                if (null != slidedTrackImage)
                {
                    bgTrackImage.Add(slidedTrackImage);
                }

                bgTrackImage.TouchEvent += OnTouchEventForBgTrack;
            }

            if (null == sliderStyle.Track)
            {
                sliderStyle.Track = new ImageViewStyle();
            }

            bgTrackImage.ApplyStyle(sliderStyle.Track);
        }

        private void CreateThumbAttributes()
        {
            if (null == thumbImage)
            {
                thumbImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    ParentOrigin = NUI.ParentOrigin.Center,
                    PivotPoint = NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true
                };
                if (bgThumbImage != null)
                {
                    bgThumbImage.Add(thumbImage);
                }
                thumbImage.TouchEvent += OnTouchEventForThumb;

                panGestureDetector = new PanGestureDetector();
                panGestureDetector.Attach(thumbImage);
                panGestureDetector.Detected += OnPanGestureDetected;
            }

            if (null == sliderStyle.Thumb)
            {
                sliderStyle.Thumb= new ImageViewStyle();
            }

            thumbImage.ApplyStyle(sliderStyle.Thumb);
        }

        private void CreateThumbBackgroundAttributes()
        {
            if (null == bgThumbImage)
            {
                bgThumbImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed
                };

                if (slidedTrackImage != null)
                {
                    slidedTrackImage.Add(bgThumbImage);
                }

                if (null != thumbImage)
                {
                    bgThumbImage.Add(thumbImage);
                }
            }

            if (null == sliderStyle.ThumbBackground)
            {
                sliderStyle.ThumbBackground= new ImageViewStyle();
            }

            bgThumbImage.ApplyStyle(sliderStyle.ThumbBackground);
        }

        private void OnPanGestureDetected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            if (e.PanGesture.State == Gesture.StateType.Started)
            {
                if (direction == DirectionType.Horizontal)
                {
                    currentSlidedOffset = slidedTrackImage.SizeWidth;
                }
                else if (direction == DirectionType.Vertical)
                {
                    currentSlidedOffset = slidedTrackImage.SizeHeight;
                }
                UpdateState(isFocused, true);
            }

            if (e.PanGesture.State == Gesture.StateType.Continuing || e.PanGesture.State == Gesture.StateType.Started)
            {
                if (direction == DirectionType.Horizontal)
                {
                    CalculateCurrentValueByGesture(e.PanGesture.Displacement.X);
                }
                else if (direction == DirectionType.Vertical)
                {
                    CalculateCurrentValueByGesture(-e.PanGesture.Displacement.Y);
                }
                UpdateValue();
            }

            if (e.PanGesture.State == Gesture.StateType.Finished)
            {
                if (null != slidingFinishedHandler)
                {
                    SlidingFinishedArgs args = new SlidingFinishedArgs();
                    args.CurrentValue = curValue.Value;
                    slidingFinishedHandler(this, args);
                }

                UpdateState(isFocused, false);
            }
        }

        // Relayout basic component: track, thumb and indicator
        private void RelayoutBaseComponent(bool isInitial = true)
        {
            if (direction == DirectionType.Horizontal)
            {
                if (slidedTrackImage != null)
                {
                    slidedTrackImage.ParentOrigin = NUI.ParentOrigin.CenterLeft;
                    slidedTrackImage.PivotPoint = NUI.PivotPoint.CenterLeft;
                    slidedTrackImage.PositionUsesPivotPoint = true;
                }
                if (bgThumbImage != null)
                {
                    bgThumbImage.ParentOrigin = NUI.ParentOrigin.CenterRight;
                    bgThumbImage.PivotPoint = NUI.PivotPoint.Center;
                    bgThumbImage.PositionUsesPivotPoint = true;
                }
                if (lowIndicatorImage != null)
                {
                    lowIndicatorImage.ParentOrigin = NUI.ParentOrigin.CenterLeft;
                    lowIndicatorImage.PivotPoint = NUI.PivotPoint.CenterLeft;
                    lowIndicatorImage.PositionUsesPivotPoint = true;
                }
                if (highIndicatorImage != null)
                {
                    highIndicatorImage.ParentOrigin = NUI.ParentOrigin.CenterRight;
                    highIndicatorImage.PivotPoint = NUI.PivotPoint.CenterRight;
                    highIndicatorImage.PositionUsesPivotPoint = true;
                }
                if (lowIndicatorText != null)
                {
                    lowIndicatorText.ParentOrigin = NUI.ParentOrigin.CenterLeft;
                    lowIndicatorText.PivotPoint = NUI.PivotPoint.CenterLeft;
                    lowIndicatorText.PositionUsesPivotPoint = true;
                }
                if (highIndicatorText != null)
                {
                    highIndicatorText.ParentOrigin = NUI.ParentOrigin.CenterRight;
                    highIndicatorText.PivotPoint = NUI.PivotPoint.CenterRight;
                    highIndicatorText.PositionUsesPivotPoint = true;
                }
                if (panGestureDetector != null)
                {
                    if (!isInitial)
                    {
                        panGestureDetector.RemoveDirection(PanGestureDetector.DirectionVertical);
                    }
                    panGestureDetector.AddDirection(PanGestureDetector.DirectionHorizontal);
                }
            }
            else if (direction == DirectionType.Vertical)
            {
                if (slidedTrackImage != null)
                {
                    slidedTrackImage.ParentOrigin = NUI.ParentOrigin.BottomCenter;
                    slidedTrackImage.PivotPoint = NUI.PivotPoint.BottomCenter;
                    slidedTrackImage.PositionUsesPivotPoint = true;
                }
                if (bgThumbImage != null)
                {
                    bgThumbImage.ParentOrigin = NUI.ParentOrigin.TopCenter;
                    bgThumbImage.PivotPoint = NUI.PivotPoint.Center;
                    bgThumbImage.PositionUsesPivotPoint = true;
                }
                if (lowIndicatorImage != null)
                {
                    lowIndicatorImage.ParentOrigin = NUI.ParentOrigin.BottomCenter;
                    lowIndicatorImage.PivotPoint = NUI.PivotPoint.BottomCenter;
                    lowIndicatorImage.PositionUsesPivotPoint = true;
                }
                if (highIndicatorImage != null)
                {
                    highIndicatorImage.ParentOrigin = NUI.ParentOrigin.TopCenter;
                    highIndicatorImage.PivotPoint = NUI.PivotPoint.TopCenter;
                    highIndicatorImage.PositionUsesPivotPoint = true;
                }
                if (lowIndicatorText != null)
                {
                    lowIndicatorText.ParentOrigin = NUI.ParentOrigin.BottomCenter;
                    lowIndicatorText.PivotPoint = NUI.PivotPoint.BottomCenter;
                    lowIndicatorText.PositionUsesPivotPoint = true;
                }
                if (highIndicatorText != null)
                {
                    highIndicatorText.ParentOrigin = NUI.ParentOrigin.TopCenter;
                    highIndicatorText.PivotPoint = NUI.PivotPoint.TopCenter;
                    highIndicatorText.PositionUsesPivotPoint = true;
                }
                if (panGestureDetector != null)
                {
                    if (!isInitial)
                    {
                        panGestureDetector.RemoveDirection(PanGestureDetector.DirectionHorizontal);
                    }
                    panGestureDetector.AddDirection(PanGestureDetector.DirectionVertical);
                }
            }
        }

        private int BgTrackLength()
        {
            int bgTrackLength = 0;
            IndicatorType type = CurrentIndicatorType();

            if (type == IndicatorType.None)
            {
                if (direction == DirectionType.Horizontal)
                {
                    bgTrackLength = this.Size2D.Width;
                }
                else if (direction == DirectionType.Vertical)
                {
                    bgTrackLength = this.Size2D.Height;
                }
            }
            else if (type == IndicatorType.Image)
            {// <lowIndicatorImage> <spaceBetweenTrackAndIndicator> <bgTrack> <spaceBetweenTrackAndIndicator> <highIndicatorImage>
                Size lowIndicatorImageSize = LowIndicatorImageSize();
                Size highIndicatorImageSize = HighIndicatorImageSize();
                int curSpace = (int)CurrentSpaceBetweenTrackAndIndicator();
                if (direction == DirectionType.Horizontal)
                {
                    int lowIndicatorSpace = ((lowIndicatorImageSize.Width == 0) ? (0) : ((int)(curSpace + lowIndicatorImageSize.Width)));
                    int highIndicatorSpace = ((highIndicatorImageSize.Width == 0) ? (0) : ((int)(curSpace + highIndicatorImageSize.Width)));
                    bgTrackLength = this.Size2D.Width - lowIndicatorSpace - highIndicatorSpace;
                }
                else if (direction == DirectionType.Vertical)
                {
                    int lowIndicatorSpace = ((lowIndicatorImageSize.Height == 0) ? (0) : ((int)(curSpace + lowIndicatorImageSize.Height)));
                    int highIndicatorSpace = ((highIndicatorImageSize.Height == 0) ? (0) : ((int)(curSpace + highIndicatorImageSize.Height)));
                    bgTrackLength = this.Size2D.Height - lowIndicatorSpace - highIndicatorSpace;
                }
            }
            else if (type == IndicatorType.Text)
            {// <lowIndicatorText> <spaceBetweenTrackAndIndicator> <bgTrack> <spaceBetweenTrackAndIndicator> <highIndicatorText>
                Size lowIndicatorTextSize = LowIndicatorTextSize();
                Size highIndicatorTextSize = HighIndicatorTextSize();
                int curSpace = (int)CurrentSpaceBetweenTrackAndIndicator();
                if (direction == DirectionType.Horizontal)
                {
                    int lowIndicatorSpace = ((lowIndicatorTextSize.Width == 0) ? (0) : ((int)(curSpace + lowIndicatorTextSize.Width)));
                    int highIndicatorSpace = ((highIndicatorTextSize.Width == 0) ? (0) : ((int)(curSpace + highIndicatorTextSize.Width)));
                    bgTrackLength = this.Size2D.Width - lowIndicatorSpace - highIndicatorSpace;
                }
                else if (direction == DirectionType.Vertical)
                {
                    int lowIndicatorSpace = ((lowIndicatorTextSize.Height == 0) ? (0) : ((int)(curSpace + lowIndicatorTextSize.Height)));
                    int highIndicatorSpace = ((highIndicatorTextSize.Height == 0) ? (0) : ((int)(curSpace + highIndicatorTextSize.Height)));
                    bgTrackLength = this.Size2D.Height - lowIndicatorSpace - highIndicatorSpace;
                }
            }
            return bgTrackLength;
        }

        private void UpdateLowIndicatorSize()
        {
            if (lowIndicatorSize != null)
            {
                if (lowIndicatorImage != null)
                {
                    lowIndicatorImage.Size = lowIndicatorSize;
                }
                if (lowIndicatorText != null)
                {
                    lowIndicatorText.Size = lowIndicatorSize;
                }
            }
            else
            {
                if (lowIndicatorImage != null && sliderStyle != null && sliderStyle.LowIndicatorImage!= null && sliderStyle.LowIndicatorImage.Size != null)
                {
                    lowIndicatorImage.Size = sliderStyle.LowIndicatorImage.Size;
                }
                if (lowIndicatorText != null && sliderStyle != null && sliderStyle.LowIndicator!= null && sliderStyle.LowIndicator.Size != null)
                {
                    lowIndicatorText.Size = sliderStyle.LowIndicator.Size;
                }
            }
        }

        private void UpdateHighIndicatorSize()
        {
            if (highIndicatorSize != null)
            {
                if (highIndicatorImage != null)
                {
                    highIndicatorImage.Size = highIndicatorSize;
                }
                if (highIndicatorText != null)
                {
                    highIndicatorText.Size = highIndicatorSize;
                }
            }
            else
            {
                if (highIndicatorImage != null && sliderStyle != null && sliderStyle.HighIndicatorImage!= null && sliderStyle.HighIndicatorImage.Size != null)
                {
                    highIndicatorImage.Size = sliderStyle.HighIndicatorImage.Size;
                }
                if (highIndicatorText != null && sliderStyle != null && sliderStyle.HighIndicator!= null && sliderStyle.HighIndicator.Size != null)
                {
                    highIndicatorText.Size = sliderStyle.HighIndicator.Size;
                }
            }
        }

        private void UpdateBgTrackSize()
        {
            if(bgTrackImage == null)
            {
                return;
            }
            int curTrackThickness = (int)CurrentTrackThickness();
            int bgTrackLength = BgTrackLength();
            if (direction == DirectionType.Horizontal)
            {
                bgTrackImage.Size2D = new Size2D(bgTrackLength, curTrackThickness);
            }
            else if (direction == DirectionType.Vertical)
            {
                bgTrackImage.Size2D = new Size2D(curTrackThickness, bgTrackLength);
            }
        }

        private void UpdateBgTrackPosition()
        {
            if (bgTrackImage == null)
            {
                return;
            }
            IndicatorType type = CurrentIndicatorType();

            if (type == IndicatorType.None)
            {
                bgTrackImage.Position2D = new Position2D(0, 0);
            }
            else if (type == IndicatorType.Image)
            {
                Size lowIndicatorImageSize = LowIndicatorImageSize();
                Size highIndicatorImageSize = HighIndicatorImageSize();
                int curSpace = (int)CurrentSpaceBetweenTrackAndIndicator();
                if (direction == DirectionType.Horizontal)
                {
                    int lowIndicatorSpace = ((lowIndicatorImageSize.Width == 0) ? (0) : ((int)(curSpace + lowIndicatorImageSize.Width)));
                    int highIndicatorSpace = ((highIndicatorImageSize.Width == 0) ? (0) : ((int)(curSpace + highIndicatorImageSize.Width)));
                    bgTrackImage.Position2D = new Position2D(lowIndicatorSpace - (lowIndicatorSpace + highIndicatorSpace) / 2, 0);
                }
                else if (direction == DirectionType.Vertical)
                {
                    int lowIndicatorSpace = ((lowIndicatorImageSize.Height == 0) ? (0) : ((int)(curSpace + lowIndicatorImageSize.Height)));
                    int highIndicatorSpace = ((highIndicatorImageSize.Height == 0) ? (0) : ((int)(curSpace + highIndicatorImageSize.Height)));
                    bgTrackImage.Position2D = new Position2D(0, lowIndicatorSpace - (lowIndicatorSpace + highIndicatorSpace) / 2);
                }
            }
            else if (type == IndicatorType.Text)
            {
                Size lowIndicatorTextSize = LowIndicatorTextSize();
                Size highIndicatorTextSize = HighIndicatorTextSize();
                int curSpace = (int)CurrentSpaceBetweenTrackAndIndicator();
                if (direction == DirectionType.Horizontal)
                {
                    int lowIndicatorSpace = ((lowIndicatorTextSize.Width == 0) ? (0) : ((int)(curSpace + lowIndicatorTextSize.Width)));
                    int highIndicatorSpace = ((highIndicatorTextSize.Width == 0) ? (0) : ((int)(curSpace + highIndicatorTextSize.Width)));
                    bgTrackImage.Position2D = new Position2D(lowIndicatorSpace - (lowIndicatorSpace + highIndicatorSpace) / 2, 0);
                }
                else if (direction == DirectionType.Vertical)
                {
                    int lowIndicatorSpace = ((lowIndicatorTextSize.Height == 0) ? (0) : ((int)(curSpace + lowIndicatorTextSize.Height)));
                    int highIndicatorSpace = ((highIndicatorTextSize.Height == 0) ? (0) : ((int)(curSpace + highIndicatorTextSize.Height)));
                    bgTrackImage.Position2D = new Position2D(0, -(lowIndicatorSpace - (lowIndicatorSpace + highIndicatorSpace) / 2));
                }
            }
        }

        private void UpdateValue()
        {
            if (slidedTrackImage == null || curValue == null || minValue == null || maxValue == null)
            {
                return;
            }
            if (curValue < minValue || curValue > maxValue || minValue >= maxValue)
            {
                return;
            }
            
            float ratio = 0;
            ratio = (float)(curValue - minValue) / (float)(maxValue - minValue);

            uint curTrackThickness = CurrentTrackThickness();

            if (direction == DirectionType.Horizontal)
            {
                if (LayoutDirection == ViewLayoutDirectionType.RTL)
                {
                    ratio = 1.0f - ratio;
                }
                float slidedTrackLength = (float)BgTrackLength() * ratio;
                slidedTrackImage.Size2D = new Size2D((int)(slidedTrackLength + round), (int)curTrackThickness); //Add const round to reach Math.Round function.
            }
            else if (direction == DirectionType.Vertical)
            {
                float slidedTrackLength = (float)BgTrackLength() * ratio;
                slidedTrackImage.Size2D = new Size2D((int)(curTrackThickness + round), (int)slidedTrackLength); //Add const round to reach Math.Round function.
            }
        }

        private uint CurrentTrackThickness()
        {
            uint curTrackThickness = 0;
            if (trackThickness != null)
            {
                curTrackThickness = trackThickness.Value;
            }
            else
            {
                if (sliderStyle != null && sliderStyle.TrackThickness != null)
                {
                    curTrackThickness = sliderStyle.TrackThickness.Value;
                }
            }
            return curTrackThickness;
        }

        private uint CurrentSpaceBetweenTrackAndIndicator()
        {
            uint curSpace = 0;
            if (spaceBetweenTrackAndIndicator != null)
            {
                curSpace = spaceBetweenTrackAndIndicator.Start;
            }
            else
            {
                if (sliderStyle != null && sliderStyle.TrackPadding != null)
                {
                    curSpace = sliderStyle.TrackPadding.Start;
                }
            }
            return curSpace;
        }

        private IndicatorType CurrentIndicatorType()
        {
            IndicatorType type = IndicatorType.None;
            if (sliderStyle != null)
            {
                type = sliderStyle.IndicatorType;
            }
            return type;
        }

        private Size LowIndicatorImageSize()
        {
            Size size = new Size(0, 0);
            if (lowIndicatorSize != null)
            {
                size = lowIndicatorSize;
            }
            else
            {
                if (sliderStyle != null && sliderStyle.LowIndicatorImage!= null && sliderStyle.LowIndicatorImage.Size != null)
                {
                    size = sliderStyle.LowIndicatorImage.Size;
                }
            }
            return size;
        }

        private Size HighIndicatorImageSize()
        {
            Size size = new Size(0, 0);
            if (highIndicatorSize != null)
            {
                size = highIndicatorSize;
            }
            else
            {
                if (sliderStyle != null && sliderStyle.HighIndicatorImage!= null && sliderStyle.HighIndicatorImage.Size != null)
                {
                    size = sliderStyle.HighIndicatorImage.Size;
                }
            }
            return size;
        }

        private Size LowIndicatorTextSize()
        {
            Size size = new Size(0, 0);
            if (lowIndicatorSize != null)
            {
                size = lowIndicatorSize;
            }
            else
            {
                if (sliderStyle != null && sliderStyle.LowIndicator!= null && sliderStyle.LowIndicator.Size != null)
                {
                    size = sliderStyle.LowIndicator.Size;
                }
            }
            return size;
        }

        private Size HighIndicatorTextSize()
        {
            Size size = new Size(0, 0);
            if (highIndicatorSize != null)
            {
                size = highIndicatorSize;
            }
            else
            {
                if (sliderStyle != null && sliderStyle.HighIndicator!= null && sliderStyle.HighIndicator.Size != null)
                {
                    size = sliderStyle.HighIndicator.Size;
                }
            }
            return size;
        }

        private void CalculateCurrentValueByGesture(float offset)
        {
            currentSlidedOffset += offset;

            if (currentSlidedOffset <= 0)
            {
                curValue = minValue;
            }
            else if (currentSlidedOffset >= BgTrackLength())
            {
                curValue = maxValue;
            }
            else
            {
                int bgTrackLength = BgTrackLength();
                if (bgTrackLength != 0)
                {
                    curValue = ((currentSlidedOffset / (float)bgTrackLength) * (float)(maxValue - minValue)) + minValue;
                }
            }
            if (valueChangedHandler != null)
            {
                ValueChangedArgs args = new ValueChangedArgs();
                args.CurrentValue = curValue.Value;
                valueChangedHandler(this, args);
            }
        }

        private bool OnTouchEventForBgTrack(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                Vector2 pos = e.Touch.GetLocalPosition(0);
                CalculateCurrentValueByTouch(pos);
                UpdateValue();
                if (null != slidingFinishedHandler)
                {
                    SlidingFinishedArgs args = new SlidingFinishedArgs();
                    args.CurrentValue = curValue.Value;
                    slidingFinishedHandler(this, args);
                }
            }
            return false;
        }

        private bool OnTouchEventForThumb(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                UpdateState(isFocused, true);
            }
            else if (state == PointStateType.Up)
            {
                UpdateState(isFocused, false);
            }
            return true;
        }

        private void CalculateCurrentValueByTouch(Vector2 pos)
        {
            int bgTrackLength = BgTrackLength();
            if (direction == DirectionType.Horizontal)
            {
                currentSlidedOffset = pos.X;
            }
            else if (direction == DirectionType.Vertical)
            {
                currentSlidedOffset = bgTrackLength - pos.Y;
            }
            if (bgTrackLength != 0)
            {
                curValue = ((currentSlidedOffset / (float)bgTrackLength) * (maxValue - minValue)) + minValue;
                if (null != valueChangedHandler)
                {
                    ValueChangedArgs args = new ValueChangedArgs();
                    args.CurrentValue = curValue.Value;
                    valueChangedHandler(this, args);
                }
            }
        }

        private void UpdateState(bool isFocusedNew, bool isPressedNew)
        {
            if (isFocused == isFocusedNew && isPressed == isPressedNew)
            {
                return;
            }
            if (thumbImage == null || sliderStyle == null)
            {
                return;
            }
            isFocused = isFocusedNew;
            isPressed = isPressedNew;

            if (!isFocused && !isPressed)
            {
                ControlState = ControlStates.Normal;
                if (stateChangedHandler != null)
                {
                    StateChangedArgs args = new StateChangedArgs();
                    args.CurrentState = (ControlStates)ControlStates.Normal;
                    stateChangedHandler(this, args);
                }
            }
            else if (isPressed)
            {
                ControlState = ControlStates.Pressed;

                if (stateChangedHandler != null)
                {
                    StateChangedArgs args = new StateChangedArgs();
                    args.CurrentState = (ControlStates)ControlStates.Pressed;
                    stateChangedHandler(this, args);
                }
            }
            else if (!isPressed && isFocused)
            {
                ControlState = ControlStates.Focused;

                if (stateChangedHandler != null)
                {
                    StateChangedArgs args = new StateChangedArgs();
                    args.CurrentState = (ControlStates)ControlStates.Focused;
                    stateChangedHandler(this, args);
                }
            }
        }

        private void UpdateComponentByIndicatorTypeChanged()
        {
            IndicatorType type = CurrentIndicatorType();
            if (type == IndicatorType.None)
            {
                if (lowIndicatorImage != null)
                {
                    lowIndicatorImage.Hide();
                }
                if (highIndicatorImage != null)
                {
                    highIndicatorImage.Hide();
                }
                if (lowIndicatorText != null)
                {
                    lowIndicatorText.Hide();
                }
                if (highIndicatorText != null)
                {
                    highIndicatorText.Hide();
                }
            }
            else if (type == IndicatorType.Image)
            {
                if (lowIndicatorImage != null)
                {
                    lowIndicatorImage.Show();
                }
                if (highIndicatorImage != null)
                {
                    highIndicatorImage.Show();
                }
                if (lowIndicatorText != null)
                {
                    lowIndicatorText.Hide();
                }
                if (highIndicatorText != null)
                {
                    highIndicatorText.Hide();
                }
            }
            else if (type == IndicatorType.Text)
            {
                if (lowIndicatorText != null)
                {
                    lowIndicatorText.Show();
                }
                if (highIndicatorText != null)
                {
                    highIndicatorText.Show();
                }
                if (lowIndicatorImage != null)
                {
                    lowIndicatorImage.Hide();
                }
                if (highIndicatorImage != null)
                {
                    highIndicatorImage.Hide();
                }
            }
        }

        /// <summary>
        /// Value Changed event data.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public class ValueChangedArgs : EventArgs
        {
            /// <summary>
            /// Curren value
            /// </summary>
            /// <since_tizen> 6 </since_tizen>
            public float CurrentValue;
        }

        /// <summary>
        /// Value Changed event data.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public class SlidingFinishedArgs : EventArgs
        {
            /// <summary>
            /// Curren value
            /// </summary>
            /// <since_tizen> 6 </since_tizen>
            public float CurrentValue;
        }

        /// <summary>
        /// State Changed event data.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public class StateChangedArgs : EventArgs
        {
            /// <summary>
            /// Curent state
            /// </summary>
            /// <since_tizen> 6 </since_tizen>
            public ControlStates CurrentState;
        }
    }
}
