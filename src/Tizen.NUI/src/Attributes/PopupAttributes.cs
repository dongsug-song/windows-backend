﻿using Tizen.NUI.Binding;

[assembly: Tizen.NUI.Xaml.XamlResourceId("Tizen.NUI.res.PopupDefaultAttributes.xaml", "PopupDefaultAttributes.xaml", typeof(Tizen.NUI.Controls.PopupAttributes))]
namespace Tizen.NUI.Controls
{
    public class PopupAttributes : ViewAttributes
    {
        public static readonly BindableProperty ShadowImageAttributesProperty = BindableProperty.Create("ShadowImageAttributes", typeof(ImageAttributes), typeof(PopupAttributes), default(ImageAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (PopupAttributes)bindable;
            if (newValue != null)
            {
                attrs.shadowImageAttrs = (ImageAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (PopupAttributes)bindable;
            return attrs.shadowImageAttrs;
        });

        public static readonly BindableProperty BackgroundImageAttributesProperty = BindableProperty.Create("BackgroundImageAttributes", typeof(ImageAttributes), typeof(PopupAttributes), default(ImageAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (PopupAttributes)bindable;
            if (newValue != null)
            {
                attrs.backgroundImageAttrs = (ImageAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (PopupAttributes)bindable;
            return attrs.backgroundImageAttrs;
        });

        public static readonly BindableProperty TitleTextAttributesProperty = BindableProperty.Create("TitleTextAttributes", typeof(TextAttributes), typeof(PopupAttributes), default(TextAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (PopupAttributes)bindable;
            if (newValue != null)
            {
                attrs.titleTextAttributes = (TextAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (PopupAttributes)bindable;
            return attrs.titleTextAttributes;
        });

        public static readonly BindableProperty ShadowOffsetProperty = BindableProperty.Create("ShadowOffset", typeof(Vector4), typeof(PopupAttributes), default(Vector4), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (PopupAttributes)bindable;
            if (newValue != null)
            {
                attrs.shadowOffset = (Vector4)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (PopupAttributes)bindable;
            return attrs.shadowOffset;
        });

        public static readonly BindableProperty ButtonHeightProperty = BindableProperty.Create("ButtonHeight", typeof(int), typeof(PopupAttributes), 0, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (PopupAttributes)bindable;
            if (newValue != null)
            {
                attrs.buttonHeight = (int)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (PopupAttributes)bindable;
            return attrs.buttonHeight;
        });

        private ImageAttributes shadowImageAttrs;
        private ImageAttributes backgroundImageAttrs;
        private TextAttributes titleTextAttributes;
        private Vector4 shadowOffset;
        private int buttonHeight;

        public PopupAttributes() : base() { }
        public PopupAttributes(PopupAttributes attributes) : base(attributes)
        {
            if (attributes.shadowImageAttrs != null)
            {
                shadowImageAttrs = attributes.shadowImageAttrs.Clone() as ImageAttributes;
            }

            if (attributes.backgroundImageAttrs != null)
            {
                backgroundImageAttrs = attributes.backgroundImageAttrs.Clone() as ImageAttributes;
            }

            if (attributes.titleTextAttributes != null)
            {
                titleTextAttributes = attributes.titleTextAttributes.Clone() as TextAttributes;
            }
            shadowOffset = new Vector4(attributes.shadowOffset.W, attributes.shadowOffset.X, attributes.shadowOffset.Y, attributes.shadowOffset.Z);
            buttonHeight = attributes.buttonHeight;
        }

        public ImageAttributes ShadowImageAttributes
        {
            get
            {
                return (ImageAttributes)GetValue(ShadowImageAttributesProperty);
            }
            set
            {
                SetValue(ShadowImageAttributesProperty, value);
            }
        }

        public ImageAttributes BackgroundImageAttributes
        {
            get
            {
                return (ImageAttributes)GetValue(BackgroundImageAttributesProperty);
            }
            set
            {
                SetValue(BackgroundImageAttributesProperty, value);
            }
        }

        public TextAttributes TitleTextAttributes
        {
            get
            {
                return (TextAttributes)GetValue(TitleTextAttributesProperty);
            }
            set
            {
                SetValue(TitleTextAttributesProperty, value);
            }
        }

        public Vector4 ShadowOffset
        {
            get
            {
                return (Vector4)GetValue(ShadowOffsetProperty);
            }
            set
            {
                SetValue(ShadowOffsetProperty, value);
            }
        }

        public int ButtonHeight
        {
            get
            {
                return (int)GetValue(ButtonHeightProperty);
            }
            set
            {
                SetValue(ButtonHeightProperty, value);
            }
        }

        public override Attributes Clone()
        {
            return new PopupAttributes(this);
        }
    }
}
