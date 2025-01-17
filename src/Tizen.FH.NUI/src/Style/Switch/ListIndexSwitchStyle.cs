﻿using Tizen.NUI;
using Tizen.NUI.Components;

namespace Tizen.FH.NUI.Controls
{
    internal class ListIndexSwitchStyle : StyleBase
    {
        protected internal override Attributes GetAttributes()
        {
            SwitchAttributes attributes = new SwitchAttributes
            {
                IsSelectable = true,
                SwitchBackgroundImageAttributes = new ImageAttributes
                {
                    Size = new Size(72, 48),
                    ResourceURL = new StringSelector
                    {
                        Normal = CommonResource.Instance.GetFHResourcePath() + "6. List/list_index_swich_bg_off.png",
                        Selected = CommonResource.Instance.GetFHResourcePath() + "6. List/list_index_swich_bg_on.png",
                        Disabled = CommonResource.Instance.GetFHResourcePath() + "6. List/list_index_swich_bg_off_dim.png",
                        DisabledSelected = CommonResource.Instance.GetFHResourcePath() + "6. List/list_index_swich_bg_on_dim.png",
                    },
                },
                SwitchHandlerImageAttributes = new ImageAttributes
                {
                    Size = new Size(48, 48),
                    ResourceURL = new StringSelector
                    {
                        Normal = CommonResource.Instance.GetFHResourcePath() + "6. List/list_index_controller_swich.png",
                        Selected = CommonResource.Instance.GetFHResourcePath() + "6. List/list_index_controller_swich.png",
                        Disabled = CommonResource.Instance.GetFHResourcePath() + "6. List/list_index_controller_swich_dim.png",
                        DisabledSelected = CommonResource.Instance.GetFHResourcePath() + "6. List/list_index_controller_swich_dim.png",
                    },
                },
            };

            return attributes;
        }
    }
}
