﻿using Tizen.NUI;
using Tizen.NUI.CommonUI;

namespace Tizen.FH.NUI.Controls
{
    internal class KitchenTextSliderStyle : StyleBase
    {
        protected override Attributes GetAttributes()
        {
            if (Content != null)
            {
                return (Content as Attributes).Clone();
            }
            SliderAttributes attributes = new SliderAttributes
            {
                TrackThickness = 4,
                SpaceBetweenTrackAndIndicator = 48,
                IndicatorType = Slider.IndicatorType.Text,
                BackgroundTrackAttributes = new ImageAttributes
                {
                    BackgroundColor = new ColorSelector
                    {
                        All = new Color(0, 0, 0, 0.1f),
                    }
                },

                SlidedTrackAttributes = new ImageAttributes
                {
                    BackgroundColor = new ColorSelector
                    {
                        All = Utility.Hex2Color(Constants.APP_COLOR_KITCHEN, 1),
                    }
                },

                ThumbAttributes = new ImageAttributes
                {
                    ResourceURL = new StringSelector
                    {
                        Normal = CommonResource.Instance.GetFHResourcePath() + "9. Controller/[Controller] App Primary Color/controller_btn_slide_handler_normal_9762d9.png",
                        Pressed = CommonResource.Instance.GetFHResourcePath() + "9. Controller/[Controller] App Primary Color/controller_btn_slide_handler_press_9762d9.png",
                    }
                },

                ThumbBackgroundAttributes = new ImageAttributes
                {
                    Size2D = new Size2D(60, 60),
                    ResourceURL = new StringSelector
                    {
                        Normal = "",
                        Pressed = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_slide_handler_effect.png",
                    }
                },

                LowIndicatorTextAttributes = new TextAttributes
                {
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = "SamsungOne 400",
                    TextColor = new ColorSelector
                    {
                        All = new Color(0, 0, 0, 0.75f),
                    },
                    PointSize = new FloatSelector
                    {
                        All = 30,
                    }
                }
            };

            return attributes;
        }
    }
}