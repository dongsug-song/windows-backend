﻿using System.ComponentModel;

namespace Tizen.NUI.CommonUI
{
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class SliderAttributes : ViewAttributes
    {
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public SliderAttributes() : base()
        {
            IndicatorType = Slider.IndicatorType.None;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public SliderAttributes(SliderAttributes attributes) : base(attributes)
        {
            if(attributes == null)
            {
                return;
            }
            if (attributes.BackgroundTrackAttributes != null)
            {
                BackgroundTrackAttributes = attributes.BackgroundTrackAttributes.Clone() as ImageAttributes;
            }
            if (attributes.SlidedTrackAttributes != null)
            {
                SlidedTrackAttributes = attributes.SlidedTrackAttributes.Clone() as ImageAttributes;
            }
            if (attributes.ThumbBackgroundAttributes != null)
            {
                ThumbBackgroundAttributes = attributes.ThumbBackgroundAttributes.Clone() as ImageAttributes;
            }
            if (attributes.ThumbAttributes != null)
            {
                ThumbAttributes = attributes.ThumbAttributes.Clone() as ImageAttributes;
            }
            if (attributes.LowIndicatorImageAttributes != null)
            {
                LowIndicatorImageAttributes = attributes.LowIndicatorImageAttributes.Clone() as ImageAttributes;
            }
            if (attributes.HighIndicatorImageAttributes != null)
            {
                HighIndicatorImageAttributes = attributes.HighIndicatorImageAttributes.Clone() as ImageAttributes;
            }
            if (attributes.LowIndicatorTextAttributes != null)
            {
                LowIndicatorTextAttributes = attributes.LowIndicatorTextAttributes.Clone() as TextAttributes;
            }
            if (attributes.HighIndicatorTextAttributes != null)
            {
                HighIndicatorTextAttributes = attributes.HighIndicatorTextAttributes.Clone() as TextAttributes;
            }
            if (attributes.TrackThickness != null)
            {
                TrackThickness = attributes.TrackThickness;
            }
            if (attributes.SpaceBetweenTrackAndIndicator != null)
            {
                SpaceBetweenTrackAndIndicator = attributes.SpaceBetweenTrackAndIndicator;
            }
            IndicatorType = attributes.IndicatorType;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageAttributes BackgroundTrackAttributes
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageAttributes SlidedTrackAttributes
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageAttributes ThumbAttributes
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageAttributes ThumbBackgroundAttributes
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageAttributes LowIndicatorImageAttributes
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageAttributes HighIndicatorImageAttributes
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TextAttributes LowIndicatorTextAttributes
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TextAttributes HighIndicatorTextAttributes
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public uint? TrackThickness
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public uint? SpaceBetweenTrackAndIndicator
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Slider.IndicatorType IndicatorType
        {
            get;
            set;
        }
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Attributes Clone()
        {
            return new SliderAttributes(this);
        }
    }
}
