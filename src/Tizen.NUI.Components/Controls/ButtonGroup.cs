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
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.Binding;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.Components
{
    /// <summary>
    /// ButtonGroup is a group of buttons which can be set common property<br />
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class ButtonGroup : BaseHandle
    {
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemSizeProperty = BindableProperty.Create(nameof(ItemSize), typeof(Size), typeof(ButtonGroup), new Size(0, 0), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Size = (Size)newValue;
                }
                btGroup.itemSize = (Size)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            Size ret = new Size(0, 0);
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemSize = ret;
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemheightProperty = BindableProperty.Create(nameof(Itemheight), typeof(float), typeof(ButtonGroup), 0.0f, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.SizeHeight = (float)newValue;
                }
                btGroup.itemheight = (float)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            float ret = 0.0f;
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemheight = ret;
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemPointSizeProperty = BindableProperty.Create(nameof(ItemPointSize), typeof(float), typeof(ButtonGroup), 0.0f, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.PointSize = (float)newValue;
                }
                btGroup.itemPointSize = (float)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            float ret = 0.0f;
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemPointSize = ret;
            return ret;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemFontFamilyProperty = BindableProperty.Create(nameof(ItemFontFamily), typeof(string), typeof(ButtonGroup), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.FontFamily = (string)newValue;
                }
                btGroup.itemFontFamily = (string)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            string ret = "";
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemFontFamily = ret;
            return ret;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemTextColorProperty = BindableProperty.Create(nameof(ItemTextColor), typeof(Color), typeof(ButtonGroup), Color.White, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.Text.TextColor = (Color)newValue;
                }
                btGroup.itemTextColor = (Color)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            Color ret = Color.White;
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemTextColor = ret;
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemBackgroundColorSelectorProperty = BindableProperty.Create(nameof(ItemBackgroundColorSelector), typeof(Selector<Color>), typeof(ButtonGroup), new Selector<Color>(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.Overlay.BackgroundColor = (Selector<Color>)newValue;
                }
                btGroup.itemBackgroundColorSelector = (Selector<Color>)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            Selector<Color> ret = new Selector<Color>();
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemBackgroundColorSelector = ret;
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemTextAlignmentProperty = BindableProperty.Create(nameof(ItemTextAlignment), typeof(HorizontalAlignment), typeof(ButtonGroup), new HorizontalAlignment(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.TextAlignment = (HorizontalAlignment)newValue;
                }
                btGroup.itemTextAlignment = (HorizontalAlignment)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            HorizontalAlignment ret = new HorizontalAlignment();
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemTextAlignment = ret;
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemBackgroundProperty = BindableProperty.Create(nameof(ItemBackground), typeof(string), typeof(ButtonGroup), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    if (btn.Style.Background.ResourceUrl == null)
                    {
                        btn.Style.Background.ResourceUrl = new Selector<string>();
                    }              
                    btn.Style.Background.ResourceUrl.All = (string)newValue;
                    //btn.BackgroundImage = (string)newValue;
                }
                btGroup.itemBackground = (string)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            string ret = "";
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemBackground = ret;
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemBackgroundBorderProperty = BindableProperty.Create(nameof(ItemBackgroundBorder), typeof(Rectangle), typeof(ButtonGroup), new Rectangle(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {                 
                    btn.BackgroundBorder = (Rectangle)newValue;
                }
                btGroup.itemBackgroundBorder = (Rectangle)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            Rectangle ret = new Rectangle();
            ButtonGroup btGroup = (ButtonGroup)bindable;
            btGroup.itemBackgroundBorder = ret;
            return ret;
        });

        public List<Button> itemGroup;
        private View root;
        private Size itemSize;
        private float itemheight;
        private float itemPointSize;
        private string itemFontFamily;
        private Color itemTextColor;
        private Selector<Color> itemBackgroundColorSelector;
        private HorizontalAlignment itemTextAlignment;
        private string itemBackground;
        private Rectangle itemBackgroundBorder;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ButtonGroup(View view) : base()
        {
            itemGroup = new List<Button>();
            if ((root = view) == null)
            {
                throw new Exception("Root view is null.");
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Count
        {
            get
            {
                return itemGroup.Count;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Contains(Button bt)
        {
            return itemGroup.Contains(bt);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int GetIndex(Button bt)
        {
            return itemGroup.IndexOf(bt);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Button GetButton(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new Exception("button index error");
            }
            return itemGroup[index];
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddItem(Button bt)
        {
            if (itemGroup.Contains(bt))
            {
                return;
            }
            itemGroup.Add(bt);
            root.Add(bt);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveItem(Button bt)
        {
            if (!itemGroup.Contains(bt))
            {
                return;
            }
            itemGroup.Remove(bt);
            root.Remove(bt);
            bt.Dispose();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveItem(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new Exception("button index error");
            }
            Button bt = itemGroup[index];
            itemGroup.Remove(bt);
            root.Remove(bt);
            bt.Dispose();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveAll()
        {
            foreach (Button bt in itemGroup)
            {
                root.Remove(bt);
                bt.Dispose();
            }
            itemGroup.Clear();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void UpdateButton()
        {
            if (Count == 0) return;

            int buttonWidth = (int)root.Size.Width / Count;
            int buttonHeight = (int)itemheight;
            foreach (Button btnTemp in itemGroup)
            {
                btnTemp.Size = new Size(buttonWidth, buttonHeight);
            }

            int pos = 0;
            if (root.LayoutDirection == ViewLayoutDirectionType.RTL)
            {
                for (int i = Count - 1; i >= 0; i--)
                {
                    itemGroup[i].PositionX = pos;
                    pos += (int)(itemGroup[i].Size.Width);
                }
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    itemGroup[i].PositionX = pos;
                    pos += (int)(itemGroup[i].Size.Width);
                }
            }
        }


        /// <summary>
        /// Common size for all of the Items
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size ItemSize
        {
            get
            {
                return (Size)GetValue(ItemSizeProperty);
            }
            set
            {
                SetValue(ItemSizeProperty, value);
            }
        }

        /// <summary>
        /// Common height for all of the Items
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public float Itemheight
        {
            get
            {
                return (float)GetValue(ItemheightProperty);
            }
            set
            {
                SetValue(ItemheightProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public float ItemPointSize
        {
            get
            {
                return (float)GetValue(ItemPointSizeProperty);
            }
            set
            {
                SetValue(ItemPointSizeProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ItemFontFamily
        {
            get
            {
                return (string)GetValue(ItemFontFamilyProperty);
            }
            set
            {
                SetValue(ItemFontFamilyProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Color ItemTextColor
        {
            get
            {
                return (Color)GetValue(ItemTextColorProperty);
            }
            set
            {
                SetValue(ItemTextColorProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Selector<Color> ItemBackgroundColorSelector
        {
            get
            {
                return (Selector<Color>)GetValue(ItemBackgroundColorSelectorProperty);
            }
            set
            {
                SetValue(ItemBackgroundColorSelectorProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public HorizontalAlignment ItemTextAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(ItemTextAlignmentProperty);
            }
            set
            {
                SetValue(ItemTextAlignmentProperty, value);
            }
        }

        /// <summary>
        /// The mutually exclusive with "backgroundColor" and "background" type Map.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ItemBackground
        {
            get
            {
                return (string)GetValue(ItemBackgroundProperty);
            }
            set
            {
                SetValue(ItemBackgroundProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle ItemBackgroundBorder
        {
            get
            {
                return (Rectangle)GetValue(ItemBackgroundBorderProperty);
            }
            set
            {
                SetValue(ItemBackgroundBorderProperty, value);
            }
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                if (itemGroup != null)
                {
                    RemoveAll();
                    itemGroup = null;
                }
            }

            base.Dispose(type);
        }
    }
}
