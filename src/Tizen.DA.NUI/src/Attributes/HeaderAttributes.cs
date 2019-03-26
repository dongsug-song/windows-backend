﻿using Tizen.NUI.Binding;
using Tizen.NUI.CommonUI;

namespace Tizen.FH.NUI.Controls
{
    public class HeaderAttributes : ViewAttributes
    {
        public static readonly BindableProperty TextAttributesProperty = BindableProperty.Create("TextAttributes", typeof(TextAttributes), typeof(HeaderAttributes), default(TextAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (HeaderAttributes)bindable;
            if (newValue != null)
            {
                attrs.textAttrs = (TextAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (HeaderAttributes)bindable;
            return attrs.textAttrs;
        });

        public static readonly BindableProperty LineAttributesProperty = BindableProperty.Create("ViewAttributes", typeof(ViewAttributes), typeof(HeaderAttributes), default(TextAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (HeaderAttributes)bindable;
            if (newValue != null)
            {
                attrs.lineAttrs = (ViewAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (HeaderAttributes)bindable;
            return attrs.lineAttrs;
        });

        private TextAttributes textAttrs;
        private ViewAttributes lineAttrs;

        public HeaderAttributes() : base() { }
        public HeaderAttributes(HeaderAttributes attributes) : base(attributes)
        {

        }


        public TextAttributes TextAttributes
        {
            get
            {
                return (TextAttributes)GetValue(TextAttributesProperty);
            }
            set
            {
                SetValue(TextAttributesProperty, value);
            }
        }

        public ViewAttributes LineAttributes
        {
            get
            {
                return (ViewAttributes)GetValue(LineAttributesProperty);
            }
            set
            {
                SetValue(LineAttributesProperty, value);
            }
        }

       
        public override Attributes Clone()
        {
            return new HeaderAttributes(this);
        }
    }
}
