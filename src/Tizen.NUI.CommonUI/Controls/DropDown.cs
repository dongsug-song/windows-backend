﻿using System;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.CommonUI
{
    public class DropDown : Control
    {
        #region DropDown
        public enum ListOrientation
        {
            Left,
            Right,
        }

        private Button button = null;
        private TextLabel headerText = null;
        private TextLabel buttonText = null;
        private ImageView listBackgroundImage = null;
        private FlexibleView list = null;
        private DropDownListBridge adapter = new DropDownListBridge();
        private DropDownAttributes dropDownAttributes = null;
        private DropDownItemView touchedView = null;
        private int selectedItemIndex = -1;
        private ClickEventHandler<ItemClickEventArgs> clickEventHandlers;

        public DropDown() : base()
        {
            dropDownAttributes = attributes as DropDownAttributes;
            if (dropDownAttributes == null)
            {
                throw new Exception("DropDown attribute parse error.");
            }
            Initialize();
        }
        public DropDown(string style) : base(style)
        {
            dropDownAttributes = attributes as DropDownAttributes;
            if (dropDownAttributes == null)
            {
                throw new Exception("DropDown attribute parse error.");
            }
            Initialize();
        }
        public DropDown(DropDownAttributes attributes) : base()
        {
            this.attributes = dropDownAttributes = attributes.Clone() as DropDownAttributes;
            if (dropDownAttributes == null)
            {
                throw new Exception("DropDown attribute parse error.");
            }
            Initialize();
        }

        public delegate void ClickEventHandler<ClickEventArgs>(object sender, ClickEventArgs e);
        
        /// <summary>
        /// Item click event.
        /// </summary>
        public event ClickEventHandler<ItemClickEventArgs> ItemClickEvent
        {
            add
            {
                clickEventHandlers += value;
            }

            remove
            {
                clickEventHandlers -= value;
            }
        }

        public string HeaderText
        {
            get
            {
                return dropDownAttributes.HeaderTextAttributes?.Text.All;
            }
            set
            {
                if (value != null)
                {
                    CreateHeaderTextAttributes();
                    if (dropDownAttributes.HeaderTextAttributes.Text == null)
                    {
                        dropDownAttributes.HeaderTextAttributes.Text = new StringSelector();
                    }
                    dropDownAttributes.HeaderTextAttributes.Text.All = value;
                    RelayoutRequest();
                }
            }
        }

        public float HeaderTextPointSize
        {
            get
            {
                return dropDownAttributes.HeaderTextAttributes?.PointSize?.All ?? 0;
            }
            set
            {
                CreateHeaderTextAttributes();
                if (dropDownAttributes.HeaderTextAttributes.PointSize == null)
                {
                    dropDownAttributes.HeaderTextAttributes.PointSize = new FloatSelector();
                }
                dropDownAttributes.HeaderTextAttributes.PointSize.All = value;
                RelayoutRequest();
            }
        }

        public string HeaderTextFontFamily
        {
            get
            {
                return dropDownAttributes.HeaderTextAttributes?.FontFamily;
            }
            set
            {
                CreateHeaderTextAttributes();
                dropDownAttributes.HeaderTextAttributes.FontFamily = value;
                RelayoutRequest();
            }
        }

        public Color HeaderTextColor
        {
            get
            {
                return dropDownAttributes.HeaderTextAttributes?.TextColor?.All;
            }
            set
            {
                CreateHeaderTextAttributes();
                if (dropDownAttributes.HeaderTextAttributes.TextColor == null)
                {
                    dropDownAttributes.HeaderTextAttributes.TextColor = new ColorSelector();
                }
                dropDownAttributes.HeaderTextAttributes.TextColor.All = value;
                RelayoutRequest();
            }
        }

        public ColorSelector HeaderTextColorSelector
        {
            get
            {
                return dropDownAttributes.HeaderTextAttributes?.TextColor;
            }
            set
            {
                CreateHeaderTextAttributes();
                if (value != null)
                {
                    dropDownAttributes.HeaderTextAttributes.TextColor = value.Clone() as ColorSelector;
                    RelayoutRequest();
                }
            }
        }

        public string ButtonText
        {
            get
            {
                return dropDownAttributes.ButtonAttributes?.TextAttributes?.Text.All;
            }
            set
            {
                if (value != null)
                {
                    CreateButtonTextAttributes();
                    if (dropDownAttributes.ButtonAttributes.TextAttributes.Text == null)
                    {
                        dropDownAttributes.ButtonAttributes.TextAttributes.Text = new StringSelector();
                    }
                    dropDownAttributes.ButtonAttributes.TextAttributes.Text.All = value;
                    RelayoutRequest();
                }
            }
        }

        public float ButtonTextPointSize
        {
            get
            {
                return dropDownAttributes.ButtonAttributes?.TextAttributes?.PointSize?.All ?? 0;
            }
            set
            {
                CreateButtonTextAttributes();
                if (dropDownAttributes.ButtonAttributes.TextAttributes.PointSize == null)
                {
                    dropDownAttributes.ButtonAttributes.TextAttributes.PointSize = new FloatSelector();
                }
                dropDownAttributes.ButtonAttributes.TextAttributes.PointSize.All = value;
                RelayoutRequest();
            }
        }

        public string ButtonTextFontFamily
        {
            get
            {
                return dropDownAttributes.ButtonAttributes?.TextAttributes?.FontFamily;
            }
            set
            {
                CreateButtonTextAttributes();
                dropDownAttributes.ButtonAttributes.TextAttributes.FontFamily = value;                
                RelayoutRequest();
            }
        }

        public Color ButtonTextColor
        {
            get
            {
                return dropDownAttributes.ButtonAttributes?.TextAttributes?.TextColor?.All;
            }
            set
            {
                CreateButtonTextAttributes();
                if (dropDownAttributes.ButtonAttributes.TextAttributes.TextColor == null)
                {
                    dropDownAttributes.ButtonAttributes.TextAttributes.TextColor = new ColorSelector();
                }
                dropDownAttributes.ButtonAttributes.TextAttributes.TextColor.All = value;               
                RelayoutRequest();
            }
        }

        public ColorSelector ButtonTextColorSelector
        {
            get
            {
                return dropDownAttributes.ButtonAttributes?.TextAttributes?.TextColor;
            }
            set
            {
                CreateButtonTextAttributes();
                if (value != null)
                {
                    dropDownAttributes.ButtonAttributes.TextAttributes.TextColor = value.Clone() as ColorSelector;                    
                    RelayoutRequest();
                }
            }
        }

        public string ButtonIconImageURL
        {
            get
            {
                return dropDownAttributes.ButtonAttributes?.IconAttributes?.ResourceURL.All;
            }
            set
            {
                if (value != null)
                {
                    CreateButtonIconAttributes();
                    if (dropDownAttributes.ButtonAttributes.IconAttributes.ResourceURL == null)
                    {
                        dropDownAttributes.ButtonAttributes.IconAttributes.ResourceURL = new StringSelector();
                    }
                    dropDownAttributes.ButtonAttributes.IconAttributes.ResourceURL.All = value;
                    RelayoutRequest();
                }
            }
        }

        public Size2D ButtonIconSize2D
        {
            get
            {
                return dropDownAttributes.ButtonAttributes?.IconAttributes?.Size2D;
            }
            set
            {
                if (value != null)
                {
                    CreateButtonIconAttributes();
                    dropDownAttributes.ButtonAttributes.IconAttributes.Size2D = value;
                    RelayoutRequest();
                }
            }
        }
        
        public int SpaceBetweenButtonTextAndIcon
        {
            get
            {
                return dropDownAttributes.SpaceBetweenButtonTextAndIcon;
            }
            set
            {
                dropDownAttributes.SpaceBetweenButtonTextAndIcon = value;
                RelayoutRequest();
            }
        }

        public int LeftSpace
        {
            get
            {
                return (int)dropDownAttributes.Space.X;
            }
            set
            {
                dropDownAttributes.Space.X = value;
                RelayoutRequest();
            }
        }

        public int RightSpace
        {
            get
            {
                return (int)dropDownAttributes.Space.Y;
            }
            set
            {
                dropDownAttributes.Space.Y = value;
                RelayoutRequest();
            }
        }

        public string ListBackgroundImageURL
        {
            get
            {
                return dropDownAttributes.ListBackgroundImageAttributes?.ResourceURL?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateListBackgroundAttributes();
                    if (dropDownAttributes.ListBackgroundImageAttributes.ResourceURL == null)
                    {
                        dropDownAttributes.ListBackgroundImageAttributes.ResourceURL = new StringSelector();
                    }
                    dropDownAttributes.ListBackgroundImageAttributes.ResourceURL.All = value;
                    RelayoutRequest();
                }
            }
        }

        public Rectangle ListBackgroundImageBorder
        {
            get
            {
                return dropDownAttributes.ListBackgroundImageAttributes?.Border?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateListBackgroundAttributes();
                    if (dropDownAttributes.ListBackgroundImageAttributes.Border == null)
                    {
                        dropDownAttributes.ListBackgroundImageAttributes.Border = new RectangleSelector();
                    }
                    dropDownAttributes.ListBackgroundImageAttributes.Border.All = value;
                    RelayoutRequest();
                }
            }
        }

        public ListOrientation ListRelativeOrientation
        {
            get
            {
                return dropDownAttributes.ListRelativeOrientation;
            }
            set
            {
                dropDownAttributes.ListRelativeOrientation = value;
                RelayoutRequest();
            }
        }

        public int ListLeftMargin
        {
            get
            {
                return (int)dropDownAttributes.ListMargin.X;
            }
            set
            {
                dropDownAttributes.ListMargin.X = value;
                RelayoutRequest();
            }
        }

        public int ListRigthMargin
        {
            get
            {
                return (int)dropDownAttributes.ListMargin.Y;
            }
            set
            {
                dropDownAttributes.ListMargin.Y = value;
                RelayoutRequest();
            }
        }

        public int ListTopMargin
        {
            get
            {
                return (int)dropDownAttributes.ListMargin.Z;
            }
            set
            {
                dropDownAttributes.ListMargin.Z = value;
                RelayoutRequest();
            }
        }

        public int FocusedItemIndex
        {
            get
            {
                return dropDownAttributes.FocusedItemIndex;
            }
            set
            {
                dropDownAttributes.FocusedItemIndex = value;
                RelayoutRequest();
            }
        }

        public int SelectedItemIndex
        {
            get
            {
                return selectedItemIndex;
            }
            set
            {
                if (value == selectedItemIndex || adapter == null || value >= adapter.GetItemCount())
                {
                    return;
                }
                UpdateSelectedItem(value);
            }
        }

        public Size2D ListSize2D
        {
            get
            {
                return dropDownAttributes.ListSize2D;
            }
            set
            {
                dropDownAttributes.ListSize2D = value;
                RelayoutRequest();
            }
        }

        public Extents ListPadding
        {
            get
            {
                return dropDownAttributes.ListPadding;
            }
            set
            {
                dropDownAttributes.ListPadding = value;
                RelayoutRequest();
            }
        }

        public void AddItem(DropDownItemData item)
        {
            adapter.InsertData(-1, item);
        }

        public void AttachScrollBar(ScrollBar scrollBar)
        {
            if (list == null)
            {
                return;
            }
            list.AttachScrollBar(scrollBar);
        }

        public void DetachScrollBar()
        {
            if (list == null)
            {
                return;
            }
            list.DetachScrollBar();
        }

        protected override void OnUpdate(Attributes attributes)
        {
            dropDownAttributes = attributes as DropDownAttributes;
            if (dropDownAttributes == null)
            {
                return;
            }

            if (dropDownAttributes.HeaderTextAttributes != null)
            {
                if (headerText == null)
                {
                    CreateHeaderText();
                }
                ApplyAttributes(headerText, dropDownAttributes.HeaderTextAttributes);
            }


            if (dropDownAttributes.ButtonAttributes != null)
            {
                if (button == null)
                {
                    CreateButton();
                }
                if (dropDownAttributes.Space != null)
                {
                    button.Position2D.X = (int)dropDownAttributes.Space.X;
                }

                if (dropDownAttributes.ButtonAttributes.TextAttributes != null)
                {
                    ApplyAttributes(buttonText, dropDownAttributes.ButtonAttributes.TextAttributes);
                    button.TextSelector = dropDownAttributes.ButtonAttributes.TextAttributes.Text;
                    if (dropDownAttributes.ButtonAttributes.TextAttributes.PointSize != null)
                    {
                        button.PointSize = dropDownAttributes.ButtonAttributes.TextAttributes.PointSize.All.Value;
                    }
                    button.FontFamily = dropDownAttributes.ButtonAttributes.TextAttributes.FontFamily;
                    button.TextColorSelector = dropDownAttributes.ButtonAttributes.TextAttributes.TextColor;
                }
                if (dropDownAttributes.ButtonAttributes.IconAttributes != null)
                {
                    button.IconURLSelector = dropDownAttributes.ButtonAttributes.IconAttributes.ResourceURL;
                    int iconWidth = 0;
                    int buttonTextWidth = 0;
                    if (dropDownAttributes.ButtonAttributes.IconAttributes.Size2D != null)
                    {
                        iconWidth = dropDownAttributes.ButtonAttributes.IconAttributes.Size2D.Width;
                    }
                    if (buttonText.NaturalSize2D != null)
                    {
                        buttonTextWidth = buttonText.NaturalSize2D.Width;
                    }
                    button.SizeWidth = iconWidth + dropDownAttributes.SpaceBetweenButtonTextAndIcon + buttonTextWidth;
                }
            }

            if (dropDownAttributes.ListBackgroundImageAttributes != null)
            {
                if (listBackgroundImage == null)
                {
                    CreateListBackgroundImage();
                    CreateList();
                }
                ApplyAttributes(listBackgroundImage, dropDownAttributes.ListBackgroundImageAttributes);
                list.FocusedItemIndex = dropDownAttributes.FocusedItemIndex;
                list.Size2D = dropDownAttributes.ListSize2D;
                list.Padding = dropDownAttributes.ListPadding;

                int listBackgroundImageX = 0;
                int listBackgroundImageY = 0;
                if (dropDownAttributes.ListRelativeOrientation == ListOrientation.Left)
                {
                    if (dropDownAttributes.ListMargin != null)
                    {
                        listBackgroundImageX = (int)dropDownAttributes.ListMargin.X;
                        listBackgroundImageY = (int)dropDownAttributes.ListMargin.Z;
                    }
                }
                else if (dropDownAttributes.ListRelativeOrientation == ListOrientation.Right)
                {
                    if (dropDownAttributes.ListMargin != null)
                    {
                        int listWidth = 0;
                        if (list.Size2D != null)
                        {
                            listWidth = list.Size2D.Width;
                        }
                        listBackgroundImageX = Size2D.Width - listWidth - (int)dropDownAttributes.ListMargin.Y;
                        listBackgroundImageY = (int)dropDownAttributes.ListMargin.Z;
                    }
                }
                listBackgroundImage.Position2D = new Position2D(listBackgroundImageX, listBackgroundImageY);
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
                if (headerText != null)
                {
                    Remove(headerText);
                    headerText.Dispose();
                    headerText = null;
                }

                if (buttonText != null)
                {
                    Remove(buttonText);
                    buttonText.Dispose();
                    buttonText = null;
                }

                if (button != null)
                {
                    Remove(button);
                    button.Dispose();
                    button = null;
                }

                if (list != null)
                {
                    if (listBackgroundImage != null)
                    {
                        list.Remove(listBackgroundImage);
                        listBackgroundImage.Dispose();
                        listBackgroundImage = null;
                    }

                    uint childCount = list.GetChildCount();
                    for (uint i = 0; i < childCount; i++)
                    {
                        DropDownItemView child = list.GetChildAt(i) as DropDownItemView;
                        if (child != null)
                        {
                            child.Dispose();
                        }
                    }

                    Remove(list);
                    list.Dispose();
                    list = null;
                }
            }

            base.Dispose(type);
        }

        protected override Attributes GetAttributes()
        {
            return new DropDownAttributes();
        }

        private void Initialize()
        {
            ApplyAttributes(this, dropDownAttributes);                  
        }

        private void OnClickEvent(object sender, ItemClickEventArgs e)
        {
            clickEventHandlers?.Invoke(sender, e);
        }

        private void CreateHeaderText()
        {
            headerText = new TextLabel();
            headerText.Name = "DropDownHeaderText";
            Add(headerText);
        }

        private void CreateButton()
        {
            button = new Button()
            {
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                HeightResizePolicy = ResizePolicyType.FillToParent,
                IconRelativeOrientation = Button.IconOrientation.Right,
            };
            button.Name = "DropDownButton";
            button.ClickEvent += ButtonClickEvent;
            Add(button);

            buttonText = new TextLabel()
            {
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                WidthResizePolicy = ResizePolicyType.UseNaturalSize,
                HeightResizePolicy = ResizePolicyType.FillToParent,
            };
            buttonText.Name = "DropDownButtonText";
            Add(buttonText);
            buttonText.Hide();
        }

        private void CreateList()
        {
            list = new FlexibleView();
            list.Name = "DropDownList";
            LinearLayoutManager layoutManager = new LinearLayoutManager(LinearLayoutManager.VERTICAL);
            list.SetLayoutManager(layoutManager);
            list.SetAdapter(adapter);
            list.Focusable = true;
            list.ItemTouchEvent += ListItemTouchEvent;
            list.ItemClickEvent += ListItemClickEvent;
            listBackgroundImage.Add(list);
            listBackgroundImage.Hide();
        }

        private void ListItemClickEvent(object sender, FlexibleView.ItemClickEventArgs e)
        {
            if (e.ClickedView != null)
            {
                UpdateSelectedItem(e.ClickedView.AdapterPosition);

                ItemClickEventArgs args = new ItemClickEventArgs();
                args.Index = e.ClickedView.AdapterPosition;
                args.Text = (e.ClickedView.ItemView as DropDownItemView)?.Text;
                OnClickEvent(this, args);
            }

            listBackgroundImage.Hide();
        }

        private void ListItemTouchEvent(object sender, FlexibleView.ItemTouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            switch (state)
            {
                case PointStateType.Down:
                    if (e.TouchedView != null)
                    {
                        touchedView = e.TouchedView.ItemView as DropDownItemView;
                        if (touchedView != null && touchedView.BackgroundColorSelector != null)
                        {
                            touchedView.BackgroundColor = touchedView.BackgroundColorSelector.GetValue(ControlStates.Pressed);
                        }
                    }
                    break;
                case PointStateType.Motion:
                    if (touchedView != null && touchedView.BackgroundColorSelector != null)
                    {
                        touchedView.BackgroundColor = touchedView.BackgroundColorSelector.GetValue(ControlStates.Normal);
                    }
                    break;
                case PointStateType.Up:
                    if (touchedView != null && touchedView.BackgroundColorSelector != null)
                    {
                        touchedView.BackgroundColor = touchedView.BackgroundColorSelector.GetValue(ControlStates.Selected);
                    }
                    break;
                default:
                    break;
            }
        }      

        private void UpdateSelectedItem(int index)
        {
            if (selectedItemIndex != -1)
            {
                DropDownItemData data = adapter.GetData(selectedItemIndex);
                if(data != null)
                {
                    data.IsSelected = false;
                }
                DropDownItemView view = list?.FindViewHolderForAdapterPosition(selectedItemIndex)?.ItemView as DropDownItemView;
                if (view != null)
                {
                    view.IsSelected = false;
                }
            }

            if (index != -1)
            {
                DropDownItemData data = adapter.GetData(index);
                if (data != null)
                {
                    data.IsSelected = true;
                }
                DropDownItemView view = list?.FindViewHolderForAdapterPosition(index)?.ItemView as DropDownItemView;
                if (view != null)
                {
                    view.IsSelected = true;
                    button.Text = view.Text;
                }
            }

            selectedItemIndex = index;
        }

        private void CreateListBackgroundImage()
        {
            listBackgroundImage = new ImageView
            {
                Name = "ListBackgroundImage",
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                WidthResizePolicy = ResizePolicyType.FitToChildren,
                HeightResizePolicy = ResizePolicyType.FitToChildren,
            };
            Add(listBackgroundImage);
        }

        private void ButtonClickEvent(object sender, Button.ClickEventArgs e)
        {
            listBackgroundImage.Show();
        }

        private void CreateHeaderTextAttributes()
        {
            if (dropDownAttributes.HeaderTextAttributes == null)
            {
                dropDownAttributes.HeaderTextAttributes = new TextAttributes()
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
            }
        }

        private void CreateButtonAttributes()
        {
            if (dropDownAttributes.ButtonAttributes == null)
            {
                dropDownAttributes.ButtonAttributes = new ButtonAttributes();
            }
        }

        private void CreateButtonTextAttributes()
        {
            CreateButtonAttributes();

            if (dropDownAttributes.ButtonAttributes.TextAttributes == null)
            {
                dropDownAttributes.ButtonAttributes.TextAttributes = new TextAttributes
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    WidthResizePolicy = ResizePolicyType.UseNaturalSize,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    Position2D = new Position2D(0, 0),
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                };
            }
        }

        private void CreateButtonIconAttributes()
        {
            CreateButtonAttributes();

            if (dropDownAttributes.ButtonAttributes.IconAttributes == null)
            {
                dropDownAttributes.ButtonAttributes.IconAttributes = new ImageAttributes
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                };
            }
        }

        private void CreateListBackgroundAttributes()
        {
            if (dropDownAttributes.ListBackgroundImageAttributes == null)
            {
                dropDownAttributes.ListBackgroundImageAttributes = new ImageAttributes
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                };
            }
        }
        #endregion

        #region ItemClickEventArgs
        public class ItemClickEventArgs : EventArgs
        {
            public int Index;
            public String Text;
        }
        #endregion

        #region DropDownItemData
        public class DropDownItemData : Control
        {
            private DropDownItemAttributes itemDataAttributes = new DropDownItemAttributes();
            public DropDownItemData() : base()
            {
                Initalize();
            }

            public DropDownItemData(string style) : base(style)
            {
                Initalize();
            }

            public DropDownItemData(DropDownItemAttributes attributes) : base()
            {
                this.attributes = attributes.Clone() as DropDownItemAttributes;
                Initalize();
            }

            public new Size2D Size2D
            {
                get
                {
                    return itemDataAttributes.Size2D;
                }
                set
                {
                    itemDataAttributes.Size2D = value;
                }
            }

            public ColorSelector BackgroundColorSelector
            {
                get
                {
                    return itemDataAttributes.BackgroundColor;
                }
                set
                {
                    if (itemDataAttributes.BackgroundColor == null)
                    {
                        itemDataAttributes.BackgroundColor = value.Clone() as ColorSelector;
                    }
                    itemDataAttributes.BackgroundColor = value.Clone();
                }
            }

            public string Text
            {
                get
                {
                    return itemDataAttributes.TextAttributes?.Text?.All;
                }
                set
                {
                    CreateTextAttributes();
                    if (itemDataAttributes.TextAttributes.Text == null)
                    {
                        itemDataAttributes.TextAttributes.Text = new StringSelector { All = value };
                    }
                    else
                    {
                        itemDataAttributes.TextAttributes.Text.All = value;
                    }
                }
            }

            public float PointSize
            {
                get
                {
                    return itemDataAttributes.TextAttributes?.PointSize?.All ?? 0; ;
                }
                set
                {
                    CreateTextAttributes();
                    if (itemDataAttributes.TextAttributes.PointSize == null)
                    {
                        itemDataAttributes.TextAttributes.PointSize = new FloatSelector { All = value };
                    }
                    else
                    {
                        itemDataAttributes.TextAttributes.PointSize.All = value;
                    }
                }
            }

            public string FontFamily
            {
                get
                {
                    return itemDataAttributes.TextAttributes?.FontFamily;
                }
                set
                {
                    CreateTextAttributes();
                    itemDataAttributes.TextAttributes.FontFamily = value;
                }
            }

            public Position2D TextPosition2D
            {
                get
                {
                    return itemDataAttributes.TextAttributes?.Position2D;
                }
                set
                {
                    CreateTextAttributes();
                    itemDataAttributes.TextAttributes.Position2D = value;
                }
            }

            public string IconResourceUrl
            {
                get
                {
                    return itemDataAttributes.IconAttributes?.ResourceURL?.All;
                }
                set
                {
                    CreateIconAttributes();
                    if (itemDataAttributes.IconAttributes.ResourceURL == null)
                    {
                        itemDataAttributes.IconAttributes.ResourceURL = new StringSelector { All = value };
                    }
                    else
                    {
                        itemDataAttributes.IconAttributes.ResourceURL.All = value;
                    }
                }
            }

            public Size2D IconSize2D
            {
                get
                {
                    return itemDataAttributes.IconAttributes?.Size2D;
                }
                set
                {
                    CreateIconAttributes();
                    itemDataAttributes.IconAttributes.Size2D = value;
                }
            }

            public Position2D IconPosition2D
            {
                get
                {
                    return itemDataAttributes.IconAttributes.Position2D;
                }
                set
                {
                    CreateIconAttributes();
                    itemDataAttributes.IconAttributes.Position2D = value;
                }
            }

            public string CheckImageResourceUrl
            {
                get
                {
                    return itemDataAttributes.CheckImageAttributes?.ResourceURL?.All;
                }
                set
                {
                    CreateCheckImageAttributes();
                    if (itemDataAttributes.CheckImageAttributes.ResourceURL == null)
                    {
                        itemDataAttributes.CheckImageAttributes.ResourceURL = new StringSelector { All = value };
                    }
                    else
                    {
                        itemDataAttributes.CheckImageAttributes.ResourceURL.All = value;
                    }
                }
            }

            public Size2D CheckImageSize2D
            {
                get
                {
                    return itemDataAttributes.CheckImageAttributes?.Size2D;
                }
                set
                {
                    CreateCheckImageAttributes();
                    itemDataAttributes.CheckImageAttributes.Size2D = value;
                }
            }

            public int CheckImageRightSpace
            {
                get
                {
                    return itemDataAttributes.CheckImageRightSpace;
                }
                set
                {
                    itemDataAttributes.CheckImageRightSpace = value;
                }
            }

            public bool IsSelected
            {
                get
                {
                    return itemDataAttributes.IsSelected;
                }
                set
                {
                    itemDataAttributes.IsSelected = value;
                }
            }

            protected override Attributes GetAttributes()
            {
                return new DropDownItemAttributes();
            }

            private void Initalize()
            {
                itemDataAttributes = attributes as DropDownItemAttributes;
                if (itemDataAttributes == null)
                {
                    throw new Exception("Button attribute parse error.");
                }
            }

            private void CreateTextAttributes()
            {
                if(itemDataAttributes.TextAttributes == null)
                {
                    itemDataAttributes.TextAttributes = new TextAttributes
                    {
                        PositionUsesPivotPoint = true,
                        ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                        WidthResizePolicy = ResizePolicyType.UseNaturalSize,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Begin,
                    };
                }
            }

            private void CreateIconAttributes()
            {
                if (itemDataAttributes.IconAttributes == null)
                {
                    itemDataAttributes.IconAttributes = new ImageAttributes
                    {
                        PositionUsesPivotPoint = true,
                        ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    };
                }
            }

            private void CreateCheckImageAttributes()
            {
                if (itemDataAttributes.CheckImageAttributes == null)
                {
                    itemDataAttributes.CheckImageAttributes = new ImageAttributes
                    {
                        PositionUsesPivotPoint = true,
                        ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    };
                }
            }
        }
        #endregion

        #region DropDownItemView
        public class DropDownItemView : Control
        {
            private TextLabel mText = null;
            private ImageView mIcon = null;
            private ImageView mCheck = null;

            public DropDownItemView() { }           

            public ColorSelector BackgroundColorSelector
            {
                get;
                set;
            }

            public string Text
            {
                get
                {
                    if(mText == null)
                    {
                        return null;
                    }
                    return mText.Text;
                }
                set
                {
                    CreateText();
                    mText.Text = value;
                }
            }

            public string FontFamily
            {
                get
                {
                    if (mText == null)
                    {
                        return null;
                    }
                    return mText.FontFamily;
                }
                set
                {
                    CreateText();
                    mText.FontFamily = value;
                }
            }

            public float PointSize
            {
                get
                {
                    if (mText == null)
                    {
                        return 0;
                    }
                    return mText.PointSize;
                }
                set
                {
                    CreateText();
                    mText.PointSize = value;
                }
            }

            public Color TextColor
            {
                get
                {
                    if (mText == null)
                    {
                        return null;
                    }
                    return mText.TextColor;
                }
                set
                {
                    CreateText();
                    mText.TextColor = value;
                }
            }

            public Position2D TextPosition2D
            {
                get
                {
                    if (mText == null)
                    {
                        return null;
                    }
                    return mText.Position2D;
                }
                set
                {
                    CreateText();
                    mText.Position2D = value;
                }
            }

            public string IconResourceUrl
            {
                get
                {
                    if (mIcon == null)
                    {
                        return null;
                    }
                    return mIcon.ResourceUrl;
                }
                set
                {
                    CreateIcon();
                    mIcon.ResourceUrl = value;
                }
            }

            public Size2D IconSize2D
            {
                get
                {
                    if (mIcon == null)
                    {
                        return null;
                    }
                    return mIcon.Size2D;
                }
                set
                {
                    CreateIcon();
                    mIcon.Size2D = value;
                }
            }

            public Position2D IconPosition2D
            {
                get
                {
                    if (mIcon == null)
                    {
                        return null;
                    }
                    return mIcon.Position2D;
                }
                set
                {
                    CreateIcon();
                    mIcon.Position2D = value;
                }
            }

            public string CheckResourceUrl
            {
                get
                {
                    if (mCheck == null)
                    {
                        return null;
                    }
                    return mCheck.ResourceUrl;
                }
                set
                {
                    CreateCheckImage();
                    mCheck.ResourceUrl = value;
                }
            }

            public Position2D CheckPosition2D
            {
                get
                {
                    if (mCheck == null)
                    {
                        return null;
                    }
                    return mCheck.Position2D;
                }
                set
                {
                    CreateCheckImage();
                    mCheck.Position2D = value;
                }
            }

            public Size2D CheckImageSize2D
            {
                get
                {
                    if (mCheck == null)
                    {
                        return null;
                    }
                    return mCheck.Size2D;
                }
                set
                {
                    CreateCheckImage();
                    mCheck.Size2D = value;
                }
            }

            public bool IsSelected
            {
                get
                {
                    if (mCheck == null)
                    {
                        return false;
                    }
                    return mCheck.Visibility;
                }
                set
                {
                    CreateCheckImage();
                    if(value)
                    {
                        mCheck.Show();
                    }
                    else
                    {
                        mCheck.Hide();
                    }
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
                    if (mText != null)
                    {
                        Remove(mText);
                        mText.Dispose();
                        mText = null;
                    }

                    if (mIcon != null)
                    {
                        Remove(mIcon);
                        mIcon.Dispose();
                        mIcon = null;
                    }

                    if (mCheck != null)
                    {
                        Remove(mCheck);
                        mCheck.Dispose();
                        mCheck = null;
                    }
                }
                base.Dispose(type);
            }

            protected override Attributes GetAttributes()
            {
                return null;
            }

            private void CreateIcon()
            {
                if(mIcon == null)
                {
                    mIcon = new ImageView()
                    {
                        PositionUsesPivotPoint = true,
                        ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    };
                    Add(mIcon);
                }
            }

            private void CreateText()
            {
                if (mText == null)
                {
                    mText = new TextLabel()
                    {
                        PositionUsesPivotPoint = true,
                        ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                        WidthResizePolicy = ResizePolicyType.UseNaturalSize,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Begin,
                    };
                    Add(mText);
                }
            }

            private void CreateCheckImage()
            {
                if (mCheck == null)
                {
                    mCheck = new ImageView()
                    {
                        PositionUsesPivotPoint = true,
                        ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    };
                    Add(mCheck);
                }
                mCheck.Hide();
            }
        }
        #endregion

        #region DropDownListBridge

        public class DropDownListBridge : FlexibleView.Adapter
        {
            private List<DropDownItemData> mDatas = new List<DropDownItemData>();

            public DropDownListBridge()
            {
            }

            public void InsertData(int position, DropDownItemData data)
            {
                if(position == -1)
                {
                    position = mDatas.Count;
                }
                mDatas.Insert(position, data);
                NotifyItemInserted(position);
            }

            public void RemoveData(int position)
            {
                mDatas.RemoveAt(position);
                NotifyItemRemoved(position);
            }

            public DropDownItemData GetData(int position)
            {
                return mDatas[position];
            }

            public override FlexibleView.ViewHolder OnCreateViewHolder(int viewType)
            {
                FlexibleView.ViewHolder viewHolder = new FlexibleView.ViewHolder(new DropDownItemView());

                return viewHolder;
            }

            public override void OnBindViewHolder(FlexibleView.ViewHolder holder, int position)
            {
                DropDownItemData listItemData = mDatas[position];
                if(listItemData == null)
                {
                    return;
                }
                DropDownItemView listItemView = holder.ItemView as DropDownItemView;
                listItemView.Name = "Item" + position;
                if (listItemData.Size2D != null)
                {
                    holder.ItemView.Size2D = listItemData.Size2D;
                }

                if (listItemView != null)
                {
                    listItemView.BackgroundColorSelector = listItemData.BackgroundColorSelector;
                    if (listItemData.Text != null)
                    {
                        listItemView.Text = listItemData.Text;
                        listItemView.PointSize = listItemData.PointSize;
                        listItemView.FontFamily = listItemData.FontFamily;
                        listItemView.TextPosition2D = listItemData.TextPosition2D;
                    }

                    if (listItemData.IconResourceUrl != null)
                    {
                        listItemView.IconResourceUrl = listItemData.IconResourceUrl;
                        listItemView.IconSize2D = listItemData.IconSize2D;
                        listItemView.IconPosition2D = new Position2D(listItemData.IconPosition2D.X, (listItemView.Size2D.Height - listItemView.IconSize2D.Height) / 2);
                    }

                    if (listItemData.CheckImageResourceUrl != null)
                    {
                        listItemView.CheckResourceUrl = listItemData.CheckImageResourceUrl;
                        listItemView.CheckImageSize2D = listItemData.CheckImageSize2D;
                        listItemView.CheckPosition2D = new Position2D(listItemView.Size2D.Width - listItemData.CheckImageRightSpace - listItemView.CheckImageSize2D.Width, (listItemView.Size2D.Height - listItemView.CheckImageSize2D.Height) / 2);
                    }

                    listItemView.IsSelected = listItemData.IsSelected;
                }              
            }

            public override int GetItemCount()
            {
                return mDatas.Count;
            }        
        }
        #endregion
    }
}
