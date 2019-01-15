﻿using Tizen.NUI.Binding;

[assembly: Tizen.NUI.Xaml.XamlResourceId("Tizen.NUI.res.SliderAttributes.xaml", "SliderAttributes.xaml", typeof(Tizen.NUI.Controls.SliderAttributes))]
namespace Tizen.NUI.Controls
{
    public class SliderAttributes : ViewAttributes
    {
        private ImageAttributes bgTrackAttrs = null;
        private ImageAttributes slidedTrackAttrs = null;
        private ImageAttributes thumbAttrs = null;
        private ImageAttributes thumbBgAttrs = null;
        private ImageAttributes lowIndicatorImageAttrs = null;
        private ImageAttributes highIndicatorImageAttrs = null;
        private TextAttributes lowIndicatorTextAttrs = null;
        private TextAttributes highIndicatorTextAttrs = null;
        private uint? trackThickness = null;
        private uint? spaceBetweenTrackAndIndicator = null;
        private Tizen.NUI.Controls.Slider.IndicatorType indicatorType = Slider.IndicatorType.None;

        public static readonly BindableProperty BgTrackAttributesProperty = 
            BindableProperty.Create("BgTrackAttributes", typeof(ImageAttributes), typeof(SliderAttributes), default(ImageAttributes),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.bgTrackAttrs = (ImageAttributes)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.bgTrackAttrs;
            });

        public static readonly BindableProperty SlidedTrackAttributesProperty = 
            BindableProperty.Create("SlidedTrackAttributes", typeof(ImageAttributes), typeof(SliderAttributes), default(ImageAttributes),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.slidedTrackAttrs = (ImageAttributes)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.slidedTrackAttrs;
            });

        public static readonly BindableProperty ThumbAttributesProperty = 
            BindableProperty.Create("ThumbAttributes", typeof(ImageAttributes), typeof(SliderAttributes), default(ImageAttributes),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.thumbAttrs = (ImageAttributes)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.thumbAttrs;
            });

        public static readonly BindableProperty ThumbBgAttributesProperty =
            BindableProperty.Create("ThumbBgAttributes", typeof(ImageAttributes), typeof(SliderAttributes), default(ImageAttributes),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.thumbBgAttrs = (ImageAttributes)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.thumbBgAttrs;
            });

        public static readonly BindableProperty LowIndicatorImageAttributesProperty =
            BindableProperty.Create("LowIndicatorImageAttributes", typeof(ImageAttributes), typeof(SliderAttributes), default(ImageAttributes),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.lowIndicatorImageAttrs = (ImageAttributes)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.lowIndicatorImageAttrs;
            });

        public static readonly BindableProperty HighIndicatorImageAttributesProperty =
            BindableProperty.Create("HighIndicatorImageAttributes", typeof(ImageAttributes), typeof(SliderAttributes), default(ImageAttributes),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.highIndicatorImageAttrs = (ImageAttributes)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.highIndicatorImageAttrs;
            });

        public static readonly BindableProperty LowIndicatorTextAttributesProperty =
            BindableProperty.Create("LowIndicatorTextAttributes", typeof(TextAttributes), typeof(SliderAttributes), default(TextAttributes),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.lowIndicatorTextAttrs = (TextAttributes)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.lowIndicatorTextAttrs;
            });

        public static readonly BindableProperty HighIndicatorTextAttributesProperty =
            BindableProperty.Create("HighIndicatorTextAttributes", typeof(TextAttributes), typeof(SliderAttributes), default(TextAttributes),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.highIndicatorTextAttrs = (TextAttributes)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.highIndicatorTextAttrs;
            });

        public static readonly BindableProperty TrackThicknessProperty =
            BindableProperty.Create("TrackThickness", typeof(uint), typeof(SliderAttributes), default(uint),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.trackThickness = (uint?)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.trackThickness;
            });

        public static readonly BindableProperty SpaceBetweenTrackAndIndicatorProperty =
            BindableProperty.Create("SpaceBetweenTrackAndIndicator", typeof(uint), typeof(SliderAttributes), default(uint),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.spaceBetweenTrackAndIndicator = (uint?)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.spaceBetweenTrackAndIndicator;
            });

        public static readonly BindableProperty IndicatorTypeProperty =
            BindableProperty.Create("IndicatorType", typeof(Tizen.NUI.Controls.Slider.IndicatorType), typeof(SliderAttributes), default(Tizen.NUI.Controls.Slider.IndicatorType),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var attrs = (SliderAttributes)bindable;
                if (newValue != null)
                {
                    attrs.indicatorType = (Tizen.NUI.Controls.Slider.IndicatorType)newValue;
                }
            },
            defaultValueCreator: (bindable) =>
            {
                var attrs = (SliderAttributes)bindable;
                return attrs.indicatorType;
            });

        public static new readonly BindableProperty StateProperty = BindableProperty.Create("State", typeof(States), typeof(SliderAttributes), States.Normal, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (SliderAttributes)bindable;
            if (newValue != null)
            {
                attrs.state = (States)newValue;
                if (attrs.thumbAttrs != null)
                {
                    attrs.thumbBgAttrs.State = attrs.state;
                    attrs.thumbAttrs.State = attrs.state;
                }
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (SliderAttributes)bindable;
            return attrs.state;
        });

        public ImageAttributes BgTrackAttributes
        {
            get { return (ImageAttributes)GetValue(BgTrackAttributesProperty); }
            set { SetValue(BgTrackAttributesProperty, value); }
        }

        public ImageAttributes SlidedTrackAttributes
        {
            get { return (ImageAttributes)GetValue(SlidedTrackAttributesProperty); }
            set { SetValue(SlidedTrackAttributesProperty, value); }
        }

        public ImageAttributes ThumbAttributes
        {
            get { return (ImageAttributes)GetValue(ThumbAttributesProperty); }
            set { SetValue(ThumbAttributesProperty, value); }
        }

        public ImageAttributes ThumbBgAttributes
        {
            get { return (ImageAttributes)GetValue(ThumbBgAttributesProperty); }
            set { SetValue(ThumbBgAttributesProperty, value); }
        }

        public ImageAttributes LowIndicatorImageAttributes
        {
            get { return (ImageAttributes)GetValue(LowIndicatorImageAttributesProperty); }
            set { SetValue(LowIndicatorImageAttributesProperty, value); }
        }

        public ImageAttributes HighIndicatorImageAttributes
        {
            get { return (ImageAttributes)GetValue(HighIndicatorImageAttributesProperty); }
            set { SetValue(HighIndicatorImageAttributesProperty, value); }
        }

        public TextAttributes LowIndicatorTextAttributes
        {
            get { return (TextAttributes)GetValue(LowIndicatorTextAttributesProperty); }
            set { SetValue(LowIndicatorTextAttributesProperty, value); }
        }

        public TextAttributes HighIndicatorTextAttributes
        {
            get { return (TextAttributes)GetValue(HighIndicatorTextAttributesProperty); }
            set { SetValue(HighIndicatorTextAttributesProperty, value); }
        }

        public uint? TrackThickness
        {
            get { return (uint?)GetValue(TrackThicknessProperty); }
            set { SetValue(TrackThicknessProperty, value); }
        }

        public uint? SpaceBetweenTrackAndIndicator
        {
            get { return (uint?)GetValue(SpaceBetweenTrackAndIndicatorProperty); }
            set { SetValue(SpaceBetweenTrackAndIndicatorProperty, value); }
        }

        public Tizen.NUI.Controls.Slider.IndicatorType IndicatorType
        {
            get { return (Tizen.NUI.Controls.Slider.IndicatorType)GetValue(IndicatorTypeProperty); }
            set { SetValue(IndicatorTypeProperty, value); }
        }

        public override States State
        {
            get
            {
                return (States)GetValue(StateProperty);
            }
            set
            {
                SetValue(StateProperty, value);
            }
        }
    }
}
