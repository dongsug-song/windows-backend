﻿using System;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.Controls
{
    public class Button : Control
    {
        public enum IconOrientation
        {
            Top,
            Bottom,
            Left,
            Right,
        }

        private ImageView backgroundImage;
        private ImageView shadowImage;
        private ImageView overlayImage;

        private TextLabel buttonText;
        private ImageView buttonIcon;

        private ButtonAttributes buttonAttributes;
        private EventHandler<StateChangeEventArgs> stateChangeHander;
        private Dictionary<KeyValuePair<States, States>, Action> stateActionTable;

        private bool isSelected = false;
        private bool isEnabled = true;
        private bool isPressed = false;

        public Button() : base()
        {
            Initialize();
        }
        public Button(string style) : base(style)
        {
            Initialize();
        }

        public Button(ButtonAttributes attributes) : base()
        {
            this.attributes = attributes.Clone() as ButtonAttributes;
            Initialize();
        }

        public event EventHandler<ClickEventArgs> ClickEvent;
        public event EventHandler<StateChangeEventArgs> StateChangedEvent
        {
            add
            {
                stateChangeHander += value;
            }
            remove
            {
                stateChangeHander -= value;
            }
        }

        public bool IsSelectable
        {
            get
            {
                return buttonAttributes?.IsSelectable ?? false;
            }
            set
            {
                buttonAttributes.IsSelectable = value;
            }
        }

        public string BackgroundImageURL
        {
            get
            {
                return buttonAttributes?.BackgroundImageAttributes?.ResourceURL?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateBackgroundAttributes();
                    if (buttonAttributes.BackgroundImageAttributes.ResourceURL == null)
                    {
                        buttonAttributes.BackgroundImageAttributes.ResourceURL = new StringSelector();
                    }
                    buttonAttributes.BackgroundImageAttributes.ResourceURL.All = value;
                    RelayoutRequest();
                }
            }
        }

        public Rectangle BackgroundImageBorder
        {
            get
            {
                return buttonAttributes?.BackgroundImageAttributes?.Border?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateBackgroundAttributes();
                    if (buttonAttributes.BackgroundImageAttributes.Border == null)
                    {
                        buttonAttributes.BackgroundImageAttributes.Border = new RectangleSelector();
                    }
                    buttonAttributes.BackgroundImageAttributes.Border.All = value;
                    RelayoutRequest();
                }
            }
        }
        public string ShadowImageURL
        {
            get
            {
                return buttonAttributes?.ShadowImageAttributes?.ResourceURL?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateShadowAttributes();
                    if (buttonAttributes.ShadowImageAttributes.ResourceURL == null)
                    {
                        buttonAttributes.ShadowImageAttributes.ResourceURL = new StringSelector();
                    }
                    buttonAttributes.ShadowImageAttributes.ResourceURL.All = value;
                    RelayoutRequest();
                }
            }
        }
        public Rectangle ShadowImageBorder
        {
            get
            {
                return buttonAttributes?.ShadowImageAttributes?.Border?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateShadowAttributes();
                    if (buttonAttributes.ShadowImageAttributes.Border == null)
                    {
                        buttonAttributes.ShadowImageAttributes.Border = new RectangleSelector();
                    }
                    buttonAttributes.ShadowImageAttributes.Border.All = value;
                    RelayoutRequest();
                }
            }
        }

        public string OverlayImageURL
        {
            get
            {
                return buttonAttributes?.OverlayImageAttributes?.ResourceURL?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateOverlayAttributes();
                    if (buttonAttributes.OverlayImageAttributes.ResourceURL == null)
                    {
                        buttonAttributes.OverlayImageAttributes.ResourceURL = new StringSelector();
                    }
                    buttonAttributes.OverlayImageAttributes.ResourceURL.All = value;
                    RelayoutRequest();
                }
            }
        }

        public Rectangle OverlayImageBorder
        {
            get
            {
                return buttonAttributes?.OverlayImageAttributes?.Border?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateOverlayAttributes();
                    if (buttonAttributes.OverlayImageAttributes.Border == null)
                    {
                        buttonAttributes.OverlayImageAttributes.Border = new RectangleSelector();
                    }
                    buttonAttributes.OverlayImageAttributes.Border.All = value;
                    RelayoutRequest();
                }
            }
        }

        public string Text
        {
            get
            {
                return buttonAttributes?.TextAttributes?.Text?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateTextAttributes();
                    if(buttonAttributes.TextAttributes.Text == null)
                    {
                        buttonAttributes.TextAttributes.Text = new StringSelector();
                    }
                    buttonAttributes.TextAttributes.Text.All = value;

                    RelayoutRequest();
                }
            }
        }

        public string TranslatableText
        {
            get
            {
                return buttonAttributes?.TextAttributes?.TranslatableText?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateTextAttributes();
                    if (buttonAttributes.TextAttributes.TranslatableText == null)
                    {
                        buttonAttributes.TextAttributes.TranslatableText = new StringSelector();
                    }
                    buttonAttributes.TextAttributes.TranslatableText.All = value;

                    RelayoutRequest();
                }
            }
        }

        public float PointSize
        {
            get
            {
                return buttonAttributes?.TextAttributes?.PointSize?.All ?? 0;
            }
            set
            {
                CreateTextAttributes();
                if (buttonAttributes.TextAttributes.PointSize == null)
                {
                    buttonAttributes.TextAttributes.PointSize = new FloatSelector();
                }
                buttonAttributes.TextAttributes.PointSize.All = value;
                RelayoutRequest();
            }
        }

        public string FontFamily
        {
            get
            {
                return buttonAttributes?.TextAttributes?.FontFamily;
            }
            set
            {
                CreateTextAttributes();
                buttonAttributes.TextAttributes.FontFamily = value;
                RelayoutRequest();
            }
        }

        public Color TextColor
        {
            get
            {
                return buttonAttributes?.TextAttributes?.TextColor?.All;
            }
            set
            {
                CreateTextAttributes();
                if (buttonAttributes.TextAttributes.TextColor == null)
                {
                    buttonAttributes.TextAttributes.TextColor = new ColorSelector();
                }
                buttonAttributes.TextAttributes.TextColor.All = value;
                RelayoutRequest();
            }
        }

        public HorizontalAlignment TextAlignment
        {
            get
            {
                return buttonAttributes?.TextAttributes?.HorizontalAlignment ?? HorizontalAlignment.Center;
            }
            set
            {
                CreateTextAttributes();
                buttonAttributes.TextAttributes.HorizontalAlignment = value;
                RelayoutRequest();
            }
        }

        public string IconURL
        {
            get
            {
                return buttonAttributes?.IconAttributes?.ResourceURL?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateIconAttributes();
                    if (buttonAttributes.IconAttributes.ResourceURL == null)
                    {
                        buttonAttributes.IconAttributes.ResourceURL = new StringSelector();
                    }
                    buttonAttributes.IconAttributes.ResourceURL.All = value;
                    RelayoutRequest();
                }
            }
        }

        public StringSelector TextSelector
        {
            get
            {
                return buttonAttributes?.TextAttributes?.Text;
            }
            set
            {
                if (value != null)
                {
                    CreateTextAttributes();
                    buttonAttributes.TextAttributes.Text = value.Clone() as StringSelector;
                    RelayoutRequest();
                }
            }
        }
        public StringSelector TranslatableTextSelector
        {
            get
            {
                return buttonAttributes?.TextAttributes?.TranslatableText;
            }
            set
            {
                if (value != null)
                {
                    CreateTextAttributes();
                    buttonAttributes.TextAttributes.TranslatableText = value.Clone() as StringSelector;
                    RelayoutRequest();
                }
            }
        }

        public ColorSelector TextColorSelector
        {
            get
            {
                return buttonAttributes?.TextAttributes?.TextColor;
            }
            set
            {
                if(value != null)
                {
                    CreateTextAttributes();
                    buttonAttributes.TextAttributes.TextColor = value.Clone() as ColorSelector;
                    RelayoutRequest();
                }
            }
        }

        public FloatSelector FontSizeSelector
        {
            get
            {
                return buttonAttributes?.TextAttributes?.PointSize;
            }
            set
            {
                if (value != null)
                {
                    CreateTextAttributes();
                    buttonAttributes.TextAttributes.PointSize = value.Clone() as FloatSelector;
                    RelayoutRequest();
                }
            }
        }

        public StringSelector IconURLSelector
        {
            get
            {
                return buttonAttributes?.IconAttributes?.ResourceURL;
            }
            set
            {
                if (value != null)
                {
                    CreateIconAttributes();
                    buttonAttributes.IconAttributes.ResourceURL = value.Clone() as StringSelector;
                    RelayoutRequest();
                }
            }
        }

        public StringSelector BackgroundImageURLSelector
        {
            get
            {
                return buttonAttributes?.BackgroundImageAttributes?.ResourceURL;
            }
            set
            {
                if (value != null)
                {
                    CreateBackgroundAttributes();
                    buttonAttributes.BackgroundImageAttributes.ResourceURL = value.Clone() as StringSelector;
                    RelayoutRequest();
                }
            }
        }
        public RectangleSelector BackgroundImageBorderSelector
        {
            get
            {
                return buttonAttributes?.BackgroundImageAttributes?.Border;
            }
            set
            {
                if (value != null)
                {
                    CreateBackgroundAttributes();
                    buttonAttributes.BackgroundImageAttributes.Border = value.Clone() as RectangleSelector;
                    RelayoutRequest();
                }
            }
        }

        public StringSelector ShadowImageURLSelector
        {
            get
            {
                return buttonAttributes?.ShadowImageAttributes?.ResourceURL;
            }
            set
            {
                if (value != null)
                {
                    CreateShadowAttributes();
                    buttonAttributes.ShadowImageAttributes.ResourceURL = value.Clone() as StringSelector;
                    RelayoutRequest();
                }
            }
        }
        public RectangleSelector ShadowImageBorderSelector
        {
            get
            {
                return buttonAttributes?.ShadowImageAttributes?.Border;
            }
            set
            {
                if (value != null)
                {
                    CreateShadowAttributes();
                    buttonAttributes.ShadowImageAttributes.Border = value.Clone() as RectangleSelector;
                    RelayoutRequest();
                }
            }
        }

        public StringSelector OverlayImageURLSelector
        {
            get
            {
                return buttonAttributes?.OverlayImageAttributes?.ResourceURL;
            }
            set
            {
                if (value != null)
                {
                    CreateOverlayAttributes();
                    buttonAttributes.OverlayImageAttributes.ResourceURL = value.Clone() as StringSelector;
                    RelayoutRequest();
                }
            }
        }
        public RectangleSelector OverlayImageBorderSelector
        {
            get
            {
                return buttonAttributes?.OverlayImageAttributes?.Border;
            }
            set
            {
                if (value != null)
                {
                    CreateOverlayAttributes();
                    buttonAttributes.OverlayImageAttributes.Border = value.Clone() as RectangleSelector;
                    RelayoutRequest();
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                UpdateState();
            }
        }

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                UpdateState();
            }
        }

        //Work only when show icon and text 
        public IconOrientation? IconRelativeOrientation
        {
            get
            {
                return buttonAttributes?.IconRelativeOrientation;
            }
            set
            {
                if(buttonAttributes != null && buttonAttributes.IconRelativeOrientation != value)
                {
                    buttonAttributes.IconRelativeOrientation = value;
                    RelayoutRequest();
                }
            }
        }

        //Work only when show icon and text 
        public int IconPaddingLeft
        {
            get
            {
                return buttonAttributes?.IconAttributes?.PaddingLeft ?? 0;
            }
            set
            {
                CreateIconAttributes();
                buttonAttributes.IconAttributes.PaddingLeft = value;
                RelayoutRequest();
            }
        }

        //Work only when show icon and text 
        public int IconPaddingRight
        {
            get
            {
                return buttonAttributes?.IconAttributes?.PaddingRight ?? 0;
            }
            set
            {
                CreateIconAttributes();
                buttonAttributes.IconAttributes.PaddingRight = value;
                RelayoutRequest();
            }
        }

        //Work only when show icon and text 
        public int IconPaddingTop
        {
            get
            {
                return buttonAttributes?.IconAttributes?.PaddingTop ?? 0;
            }
            set
            {
                CreateIconAttributes();
                buttonAttributes.IconAttributes.PaddingTop = value;
                RelayoutRequest();
            }
        }

        //Work only when show icon and text 
        public int IconPaddingBottom
        {
            get
            {
                return buttonAttributes?.IconAttributes?.PaddingBottom ?? 0;
            }
            set
            {
                CreateIconAttributes();
                buttonAttributes.IconAttributes.PaddingBottom = value;
                RelayoutRequest();
            }
        }

        //Work only when show icon and text 
        public int TextPaddingLeft
        {
            get
            {
                return buttonAttributes?.TextAttributes?.PaddingLeft ?? 0;
            }
            set
            {
                CreateIconAttributes();
                buttonAttributes.TextAttributes.PaddingLeft = value;
                RelayoutRequest();
            }
        }

        //Work only when show icon and text 
        public int TextPaddingRight
        {
            get
            {
                return buttonAttributes?.TextAttributes?.PaddingRight ?? 0;
            }
            set
            {
                CreateIconAttributes();
                buttonAttributes.TextAttributes.PaddingRight = value;
                RelayoutRequest();
            }
        }

        //Work only when show icon and text 
        public int TextPaddingTop
        {
            get
            {
                return buttonAttributes?.TextAttributes?.PaddingTop ?? 0;
            }
            set
            {
                CreateIconAttributes();
                buttonAttributes.TextAttributes.PaddingTop = value;
                RelayoutRequest();
            }
        }

        //Work only when show icon and text 
        public int TextPaddingBottom
        {
            get
            {
                return buttonAttributes?.TextAttributes?.PaddingBottom ?? 0;
            }
            set
            {
                CreateIconAttributes();
                buttonAttributes.TextAttributes.PaddingBottom = value;
                RelayoutRequest();
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
                if (buttonIcon != null)
                {
                    buttonIcon.Relayout -= OnIconRelayout;
                    this.Remove(buttonIcon);
                    buttonIcon.Dispose();
                    buttonIcon = null;
                }
                if (buttonText != null)
                {
                    this.Remove(buttonText);
                    buttonText.Dispose();
                    buttonText = null;
                }
                if (overlayImage != null)
                {
                    this.Remove(overlayImage);
                    overlayImage.Dispose();
                    overlayImage = null;
                }
                if (backgroundImage != null)
                {
                    this.Remove(backgroundImage);
                    backgroundImage.Dispose();
                    backgroundImage = null;
                }
                if (shadowImage != null)
                {
                    this.Remove(shadowImage);
                    shadowImage.Dispose();
                    shadowImage = null;
                }
            }

            base.Dispose(type);
        }
     
        protected override bool OnKey(object source, KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Return")
                {
                    isPressed = true;
                    UpdateState();
                    if(isEnabled)
                    {
                        ClickEventArgs eventArgs = new ClickEventArgs();
                        OnClick(eventArgs);
                    }
                }
            }
            else
            {
                if (e.Key.KeyPressedName == "Return")
                {
                    isPressed = false;
                    if (buttonAttributes.IsSelectable != null && buttonAttributes.IsSelectable == true)
                    {
                        isSelected = !isSelected;
                    }
                    UpdateState();
                }
            }
            return base.OnKey(source, e);
        }
        protected override void OnFocusGained(object sender, EventArgs e)
        {
            base.OnFocusGained(sender, e);
            UpdateState();
        }
        protected override void OnFocusLost(object sender, EventArgs e)
        {
            base.OnFocusLost(sender, e);
            UpdateState();
        }
        protected override void OnTapGestureDetected(object source, TapGestureDetector.DetectedEventArgs e)
        {
            if (isEnabled)
            {
                ClickEventArgs eventArgs = new ClickEventArgs();
                OnClick(eventArgs);
                base.OnTapGestureDetected(source, e);
            }
        }
        protected override bool OnTouch(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
      
            switch(state)
            {
                case PointStateType.Down:
                    isPressed = true;
                    UpdateState();
                    return true;
                case PointStateType.Interrupted:
                    isPressed = false;
                    UpdateState();
                    return true;
                case PointStateType.Up:
                    isPressed = false;
                    if (buttonAttributes.IsSelectable != null && buttonAttributes.IsSelectable == true)
                    {
                        isSelected = !isSelected;
                    }
                    UpdateState();
                    return true;
                default:
                    break;
            }
            return base.OnTouch(source, e);
        }
        protected override Attributes GetAttributes()
        {
            return new ButtonAttributes();
        }

        protected override void OnUpdate(Attributes attributtes)
        {
            buttonAttributes = attributes as ButtonAttributes;
            if (buttonAttributes == null)
            {
                return;
            }

            if (buttonAttributes.ShadowImageAttributes != null)
            {
                if(shadowImage == null)
                {
                    shadowImage = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent
                    };
                    this.Add(shadowImage);
                }
                ApplyAttributes(shadowImage, buttonAttributes.ShadowImageAttributes);
            }

            if (buttonAttributes.BackgroundImageAttributes != null)
            {
                if(backgroundImage == null)
                {
                    backgroundImage = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent
                    };
                    this.Add(backgroundImage);
                }
                ApplyAttributes(backgroundImage, buttonAttributes.BackgroundImageAttributes);
            }

            if (buttonAttributes.OverlayImageAttributes != null)
            {
                if(overlayImage == null)
                {
                    overlayImage = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent
                    };
                    this.Add(overlayImage);
                }
                ApplyAttributes(overlayImage, buttonAttributes.OverlayImageAttributes);
            }

            if (buttonAttributes.TextAttributes != null)
            {
                if(buttonText == null)
                {
                    buttonText = new TextLabel();
                    this.Add(buttonText);
                }
                ApplyAttributes(buttonText, buttonAttributes.TextAttributes);
            }

            if (buttonAttributes.IconAttributes != null)
            {
                if(buttonIcon == null)
                {
                    buttonIcon = new ImageView();
                    buttonIcon.Relayout += OnIconRelayout;
                    this.Add(buttonIcon);
                }
                ApplyAttributes(buttonIcon, buttonAttributes.IconAttributes);
            }

            MeasureText();
            LayoutChild();

            Sensitive = isEnabled ? true : false;
        }

        protected void UpdateState()
        {
            States sourceState = State;
            States targetState;

            if(isEnabled)
            {
                targetState = isPressed ? States.Pressed : (IsFocused ? (IsSelected ? States.SelectedFocused : States.Focused) : (IsSelected ? States.Selected : States.Normal));
            }
            else
            {
                targetState = IsSelected ? States.DisabledSelected : (IsFocused ? States.DisabledFocused : States.Disabled);
            }
            if(sourceState != targetState)
            {
                State = targetState;

                OnUpdate(attributes);

                StateChangeEventArgs e = new StateChangeEventArgs
                {
                    PreviousState = sourceState,
                    CurrentState = targetState
                };
                stateChangeHander?.Invoke(this, e);

                PlayMotion(sourceState, targetState);
            }
        }

        protected void Initialize()
        {
            buttonAttributes = attributes as ButtonAttributes;
            if (buttonAttributes == null)
            {
                throw new Exception("Button attribute parse error.");
            }
            ApplyAttributes(this, buttonAttributes);
            LayoutDirectionChanged += OnLayoutDirectionChanged;
        }

        protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            ButtonAttributes tempAttributes = StyleManager.Instance.GetAttributes(style) as ButtonAttributes;
            if(tempAttributes != null)
            {
                attributes = buttonAttributes = tempAttributes;
                RelayoutRequest();
            }
        }

        private void OnLayoutDirectionChanged(object sender, LayoutDirectionChangedEventArgs e)
        {
            MeasureText();
            LayoutChild();
        }

        private void PlayMotion(States sourceState, States targetState)
        {
            KeyValuePair<States, States> command = new KeyValuePair<States, States>(sourceState, targetState);
            if (stateActionTable != null && stateActionTable.ContainsKey(command))
            {
                stateActionTable[command].Invoke();
            }
        }

        private void OnClick(ClickEventArgs eventArgs)
        {
            if (ClickEvent != null)
            {
                ClickEvent(this, eventArgs);
            }
        }

        private void OnIconRelayout(object sender, EventArgs e)
        {
            MeasureText();
            LayoutChild();
        }

        private void CreateBackgroundAttributes()
        {
            if (buttonAttributes.BackgroundImageAttributes == null)
            {
                buttonAttributes.BackgroundImageAttributes = new ImageAttributes()
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
            }
        }

        private void CreateShadowAttributes()
        {
            if (buttonAttributes.ShadowImageAttributes == null)
            {
                buttonAttributes.ShadowImageAttributes = new ImageAttributes()
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
            }
        }

        private void CreateOverlayAttributes()
        {
            if (buttonAttributes.OverlayImageAttributes == null)
            {
                buttonAttributes.OverlayImageAttributes = new ImageAttributes()
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
            }
        }

        private void CreateTextAttributes()
        {
            if (buttonAttributes.TextAttributes == null)
            {
                buttonAttributes.TextAttributes = new TextAttributes()
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
        }

        private void CreateIconAttributes()
        {
            if (buttonAttributes.IconAttributes == null)
            {
                buttonAttributes.IconAttributes = new ImageAttributes()
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    WidthResizePolicy = ResizePolicyType.UseNaturalSize,
                    HeightResizePolicy = ResizePolicyType.UseNaturalSize,
                };
            }
        }

        protected virtual void MeasureText()
        {
            if (buttonAttributes.IconRelativeOrientation == null || buttonIcon == null || buttonText == null)
            {
                return;
            }
            buttonText.WidthResizePolicy = ResizePolicyType.Fixed;
            buttonText.HeightResizePolicy = ResizePolicyType.Fixed;
            int textPaddingLeft = buttonAttributes.TextAttributes.PaddingLeft;
            int textPaddingRight = buttonAttributes.TextAttributes.PaddingRight;
            int textPaddingTop = buttonAttributes.TextAttributes.PaddingTop;
            int textPaddingBottom = buttonAttributes.TextAttributes.PaddingBottom;

            int iconPaddingLeft = buttonAttributes.IconAttributes.PaddingLeft;
            int iconPaddingRight = buttonAttributes.IconAttributes.PaddingRight;
            int iconPaddingTop = buttonAttributes.IconAttributes.PaddingTop;
            int iconPaddingBottom = buttonAttributes.IconAttributes.PaddingBottom;

            if (IconRelativeOrientation == IconOrientation.Top || IconRelativeOrientation == IconOrientation.Bottom)
            {
                buttonText.SizeWidth = SizeWidth - textPaddingLeft - textPaddingRight;
                buttonText.SizeHeight = SizeHeight - textPaddingTop - textPaddingBottom - iconPaddingTop - iconPaddingBottom - buttonIcon.SizeHeight;
            }
            else
            {
                buttonText.SizeWidth = SizeWidth - textPaddingLeft - textPaddingRight - iconPaddingLeft - iconPaddingRight - buttonIcon.SizeWidth;
                buttonText.SizeHeight = SizeHeight - textPaddingTop - textPaddingBottom;
            }
        }

        protected virtual void LayoutChild()
        {
            if (buttonAttributes.IconRelativeOrientation == null || buttonIcon == null || buttonText == null)
            {
                return;
            }

            int textPaddingLeft = buttonAttributes.TextAttributes.PaddingLeft;
            int textPaddingRight = buttonAttributes.TextAttributes.PaddingRight;
            int textPaddingTop = buttonAttributes.TextAttributes.PaddingTop;
            int textPaddingBottom = buttonAttributes.TextAttributes.PaddingBottom;

            int iconPaddingLeft = buttonAttributes.IconAttributes.PaddingLeft;
            int iconPaddingRight = buttonAttributes.IconAttributes.PaddingRight;
            int iconPaddingTop = buttonAttributes.IconAttributes.PaddingTop;
            int iconPaddingBottom = buttonAttributes.IconAttributes.PaddingBottom;

            switch (IconRelativeOrientation)
            {
                case IconOrientation.Top:
                    buttonIcon.PositionUsesPivotPoint = true;
                    buttonIcon.ParentOrigin = NUI.ParentOrigin.TopCenter;
                    buttonIcon.PivotPoint = NUI.PivotPoint.TopCenter;
                    buttonIcon.Position2D = new Position2D(0, iconPaddingTop);

                    buttonText.PositionUsesPivotPoint = true;
                    buttonText.ParentOrigin = NUI.ParentOrigin.BottomCenter;
                    buttonText.PivotPoint = NUI.PivotPoint.BottomCenter;
                    buttonText.Position2D = new Position2D(0, -textPaddingBottom);
                    break;
                case IconOrientation.Bottom:
                    buttonIcon.PositionUsesPivotPoint = true;
                    buttonIcon.ParentOrigin = NUI.ParentOrigin.BottomCenter;
                    buttonIcon.PivotPoint = NUI.PivotPoint.BottomCenter;
                    buttonIcon.Position2D = new Position2D(0, -iconPaddingBottom);

                    buttonText.PositionUsesPivotPoint = true;
                    buttonText.ParentOrigin = NUI.ParentOrigin.TopCenter;
                    buttonText.PivotPoint = NUI.PivotPoint.TopCenter;
                    buttonText.Position2D = new Position2D(0, textPaddingTop);
                    break;
                case IconOrientation.Left:
                    if(LayoutDirection == ViewLayoutDirectionType.LTR)
                    {
                        buttonIcon.PositionUsesPivotPoint = true;
                        buttonIcon.ParentOrigin = NUI.ParentOrigin.CenterLeft;
                        buttonIcon.PivotPoint = NUI.PivotPoint.CenterLeft;
                        buttonIcon.Position2D = new Position2D(iconPaddingLeft, 0);

                        buttonText.PositionUsesPivotPoint = true;
                        buttonText.ParentOrigin = NUI.ParentOrigin.CenterRight;
                        buttonText.PivotPoint = NUI.PivotPoint.CenterRight;
                        buttonText.Position2D = new Position2D(-textPaddingRight, 0);
                    }
                    else
                    {
                        buttonIcon.PositionUsesPivotPoint = true;
                        buttonIcon.ParentOrigin = NUI.ParentOrigin.CenterRight;
                        buttonIcon.PivotPoint = NUI.PivotPoint.CenterRight;
                        buttonIcon.Position2D = new Position2D(-iconPaddingLeft, 0);

                        buttonText.PositionUsesPivotPoint = true;
                        buttonText.ParentOrigin = NUI.ParentOrigin.CenterLeft;
                        buttonText.PivotPoint = NUI.PivotPoint.CenterLeft;
                        buttonText.Position2D = new Position2D(textPaddingRight, 0);
                    }

                    break;
                case IconOrientation.Right:
                    if (LayoutDirection == ViewLayoutDirectionType.RTL)
                    {
                        buttonIcon.PositionUsesPivotPoint = true;
                        buttonIcon.ParentOrigin = NUI.ParentOrigin.CenterLeft;
                        buttonIcon.PivotPoint = NUI.PivotPoint.CenterLeft;
                        buttonIcon.Position2D = new Position2D(iconPaddingRight, 0);

                        buttonText.PositionUsesPivotPoint = true;
                        buttonText.ParentOrigin = NUI.ParentOrigin.CenterRight;
                        buttonText.PivotPoint = NUI.PivotPoint.CenterRight;
                        buttonText.Position2D = new Position2D(-textPaddingLeft, 0);
                    }
                    else
                    {
                        buttonIcon.PositionUsesPivotPoint = true;
                        buttonIcon.ParentOrigin = NUI.ParentOrigin.CenterRight;
                        buttonIcon.PivotPoint = NUI.PivotPoint.CenterRight;
                        buttonIcon.Position2D = new Position2D(-iconPaddingRight, 0);

                        buttonText.PositionUsesPivotPoint = true;
                        buttonText.ParentOrigin = NUI.ParentOrigin.CenterLeft;
                        buttonText.PivotPoint = NUI.PivotPoint.CenterLeft;
                        buttonText.Position2D = new Position2D(textPaddingLeft, 0);
                    }
                    break;
                default:
                    break;
            }
        }

        public class ClickEventArgs : EventArgs
        {

        }

        public class StateChangeEventArgs : EventArgs
        {
            /// <summary> previous state of Button </summary>
            public States PreviousState;
            /// <summary> current state of Button </summary>
            public States CurrentState;
        }

    }
}
