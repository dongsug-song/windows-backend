﻿using Tizen.NUI;
using Tizen.NUI.Components;

namespace Tizen.FH.NUI.Controls
{
    internal class BlackConditionNavigationItemStyle : StyleBase
    {
        protected internal override Attributes GetAttributes()
        {
            NavigationItemAttributes attributes = new NavigationItemAttributes
            {
                TextAttributes = new TextAttributes
                {
                    Size = new Size(108, 24),
                    TextColor = new ColorSelector
                    {
                        Pressed = new Color(1, 1, 1, 0.85f),
                        Disabled = new Color(1, 1, 1, 0.4f),
                        Other = new Color(1, 1, 1, 0.85f),
                    },
                    PointSize = new FloatSelector { All = 8 },
                    FontFamily = "SamsungOneUI 500C",
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                },
                SubTextAttributes = new TextAttributes
                {
                    Size = new Size(108, 24),
                    TextColor = new ColorSelector
                    {
                        Pressed = new Color(1, 1, 1, 0.85f),
                        Disabled = new Color(1, 1, 1, 0.4f),
                        Other = new Color(1, 1, 1, 0.85f),
                    },
                    PointSize = new FloatSelector { All = 8 },
                    FontFamily = "SamsungOneUI 500C",
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                IconAttributes = new ImageAttributes
                {
                    Size = new Size(56, 56),
                },
                Space = new Vector4(4, 4, 8, 16),
            };

            return attributes;
        }
    }
}
