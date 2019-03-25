﻿using System;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.CommonUI
{
    public class InputField : Control
    {
        // the background image
        private ImageView bgImage = null;
        // the textField
        private TextField textField = null;
        // the attributes of the inputField
        private InputFieldAttributes inputFieldAttrs = null;

        // the flag indicate should relayout the textField in base class
        private bool relayoutTextField = true;

        static InputField()
        {
            RegisterStyle("DefaultInputField", typeof(InputFieldAttributes));
        }

        public InputField() : this("DefaultInputField")
        {
            Initialize();
        }

        public InputField(string style) : base(style)
        {
            Initialize();
        }

        public InputField(InputFieldAttributes attributes) : base()
        {
            this.attributes = inputFieldAttrs = attributes.Clone() as InputFieldAttributes;
            Initialize();
        }

        /// <summary>
        /// The property for the enabled state
        /// </summary>
        public bool StateEnabled
        {
            get
            {
                return Sensitive;
            }
            set
            {
                Sensitive = value;
            }
        }

        /// <summary>
        /// The property for the text content
        /// </summary>
        public string Text
        {
            get
            {
                return textField.Text;
            }
            set
            {
                if (textField != null)
                {
                    textField.Text = value;
                }
            }
        }

        /// <summary>
        /// The property for the hint text
        /// </summary>
        public string HintText
        {
            get
            {
                return textField.PlaceholderText;
            }
            set
            {
                if (textField != null)
                {
                    textField.PlaceholderText = value;
                }
            }
        }

        /// <summary>
        /// The property for the color of the input text
        /// </summary>
        public Color TextColor
        {
            get
            {
                return textField.TextColor;
            }
            set
            {
                if (textField != null)
                {
                    textField.TextColor = value;
                }
            }
        }

        protected override Attributes GetAttributes()
        {
            return null;
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }
            if (type == DisposeTypes.Explicit)
            {
                if (bgImage != null)
                {
                    this.Remove(bgImage);
                    bgImage.Dispose();
                    bgImage = null;
                }
                if (textField != null)
                {
                    textField.FocusGained -= OnTextFieldFocusGained;
                    textField.FocusLost -= OnTextFieldFocusLost;
                    textField.TextChanged -= OnTextFieldTextChanged;
                    textField.KeyEvent -= OnTextFieldKeyEvent;
                    //textField.TouchEvent -= OnTextFieldTouchEvent;
                    this.Remove(textField);
                    textField.Dispose();
                    textField = null;
                }
            }

            base.Dispose(type);
        }

        protected override void OnUpdate(Attributes attributtes)
        {
            inputFieldAttrs = attributes as InputFieldAttributes;
            if (inputFieldAttrs == null)
            {
                return;
            }
            ApplyAttributes(this, inputFieldAttrs);
            ApplyAttributes(bgImage, inputFieldAttrs.BackgroundImageAttributes);
            ApplyAttributes(textField, inputFieldAttrs.InputBoxAttributes);
            RelayoutComponent();
        }

        private void Initialize()
        {
            inputFieldAttrs = attributes as InputFieldAttributes;
            if (inputFieldAttrs == null)
            {
                throw new Exception("Fail to get the base inputField attributes.");
            }
            if (inputFieldAttrs.BackgroundImageAttributes != null && bgImage == null)
            {
                bgImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true
                };
                this.Add(bgImage);
            }
            if (inputFieldAttrs.InputBoxAttributes != null && textField == null)
            {
                textField = new TextField()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                    PositionUsesPivotPoint = true
                };
                this.Add(textField);
                textField.FocusGained += OnTextFieldFocusGained;
                textField.FocusLost += OnTextFieldFocusLost;
                textField.TextChanged += OnTextFieldTextChanged;
                textField.KeyEvent += OnTextFieldKeyEvent;
            }
        }
        protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            InputFieldAttributes tempAttributes = StyleManager.Instance.GetAttributes(style) as InputFieldAttributes;
            if (tempAttributes != null)
            {
                attributes = inputFieldAttrs = tempAttributes;
                RelayoutRequest();
            }
        }
        private void RelayoutComponent()
        {
            if (!relayoutTextField)
            {
                return;
            }
            int space = Space();
            if (textField != null)
            {
                textField.Size2D = new Size2D(this.Size2D.Width - space * 2, this.Size2D.Height);
                textField.PositionX = space;
            }
        }

        protected int Space()
        {
            int space = 0;
            if (inputFieldAttrs != null && inputFieldAttrs.Space != null)
            {
                space = inputFieldAttrs.Space.Value;
            }
            return space;
        }

        protected virtual void OnTextFieldFocusGained(object source, EventArgs e)
        {
        }

        protected virtual void OnTextFieldFocusLost(object source, EventArgs e)
        {
        }

        protected virtual void OnTextFieldTextChanged(object sender, TextField.TextChangedEventArgs e)
        {
        }

        protected virtual bool OnTextFieldKeyEvent(object source, KeyEventArgs e)
        {
            return false;
        }

        protected virtual bool OnTextFieldTouchEvent(object sender, View.TouchEventArgs e)
        {
            return false;
        }

        protected void SetTextFieldSize2D(int w, int h)
        {
            if (textField != null)
            {
                textField.Size2D = new Size2D(w, h);
            }
        }

        protected void SetTextFieldPosX(int x)
        {
            if (textField != null)
            {
                textField.PositionX = x;
            }
        }

        protected void SetTextFieldTextColor(Color color)
        {
            if (textField != null)
            {
                textField.TextColor = color;
            }
        }

        protected void RelayoutTextField(bool value)
        {
            relayoutTextField = value;
        }
    }
}
