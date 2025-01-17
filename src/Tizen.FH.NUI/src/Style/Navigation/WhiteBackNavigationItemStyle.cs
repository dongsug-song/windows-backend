﻿using Tizen.NUI;
using Tizen.NUI.Components;

namespace Tizen.FH.NUI.Controls
{
    internal class WhiteBackNavigationItemStyle : StyleBase
    {
        protected internal override Attributes GetAttributes()
        {
            NavigationItemAttributes attributes = new NavigationItemAttributes
            {
                Size = new Size(120, 140),
                IconAttributes = new ImageAttributes()
                {
                    Size = new Size(56, 56),
                    ResourceURL = new StringSelector { All = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_back.png" },
                },
                BackgroundImageAttributes = new ImageAttributes()
                {
                    ResourceURL = new StringSelector { All = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/sidenavi_back_bg.png" },
                },
                OverlayImageAttributes = new ImageAttributes()
                {
                    ResourceURL = new StringSelector
                    {
                        Pressed = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/sidenavi_back_bg_press_overlay.png",
                        Other = "",
                    },
                },
                EnableIconCenter = true
            };

            return attributes;
        }
    }
}
