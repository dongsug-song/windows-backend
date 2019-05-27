/*
 * Copyright(c) 2018 Samsung Electronics Co., Ltd.
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
using System.Runtime.InteropServices;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.UIComponents
{
    /// <summary>
    /// The ProgressBar is a control to give the user an indication of the progress of an operation.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class ProgressBar : View
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        private EventHandler<ValueChangedEventArgs> _progressBarValueChangedEventHandler;
        private ValueChangedCallbackDelegate _progressBarValueChangedCallbackDelegate;

        /// <summary>
        /// Creates the ProgressBar.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public ProgressBar() : this(Interop.ProgressBar.ProgressBar_New(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();

        }

        internal ProgressBar(global::System.IntPtr cPtr, bool cMemoryOwn) : base(Interop.ProgressBar.ProgressBar_SWIGUpcast(cPtr), cMemoryOwn)
        {
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ValueChangedCallbackDelegate(IntPtr progressBar, float progressValue, float secondaryProgressValue);

        /// <summary>
        /// The event is sent when the ProgressBar value changes.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler<ValueChangedEventArgs> ValueChanged
        {
            add
            {
                if (_progressBarValueChangedEventHandler == null)
                {
                    _progressBarValueChangedCallbackDelegate = (OnValueChanged);
                    ValueChangedSignal().Connect(_progressBarValueChangedCallbackDelegate);
                }
                _progressBarValueChangedEventHandler += value;
            }
            remove
            {
                _progressBarValueChangedEventHandler -= value;
                if (_progressBarValueChangedEventHandler == null && ValueChangedSignal().Empty() == false)
                {
                    ValueChangedSignal().Disconnect(_progressBarValueChangedCallbackDelegate);
                }
            }
        }

        /// <summary>
        /// The progress value of the progress bar, the progress runs from 0 to 1.<br />
        /// If the value is set to 0, then the progress bar will be set to beginning.<br />
        /// If the value is set to 1, then the progress bar will be set to end.<br />
        /// Any value outside the range is ignored.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public float ProgressValue
        {
            get
            {
                float temp = 0.0f;
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.PROGRESS_VALUE).Get(out temp);
                return temp;
            }
            set
            {
                Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.PROGRESS_VALUE, new PropertyValue(value));
            }
        }

        /// <summary>
        /// The secondary progress value of the progress bar, the secondary progress runs from 0 to 1.<br />
        /// Optional. If not supplied, the default is 0.<br />
        /// If the value is set to 0, then the progress bar will be set secondary progress to beginning.<br />
        /// If the value is set to 1, then the progress bar will be set secondary progress to end.<br />
        /// Any value outside of the range is ignored.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public float SecondaryProgressValue
        {
            get
            {
                float temp = 0.0f;
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.SECONDARY_PROGRESS_VALUE).Get(out temp);
                return temp;
            }
            set
            {
                Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.SECONDARY_PROGRESS_VALUE, new PropertyValue(value));
            }
        }

        /// <summary>
        /// Sets the progress bar as \e indeterminate state.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool Indeterminate
        {
            get
            {
                bool temp = false;
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.INDETERMINATE).Get(out temp);
                return temp;
            }
            set
            {
                Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.INDETERMINATE, new PropertyValue(value));
            }
        }

        /// <summary>
        /// The track visual value of progress bar, it's full progress area, and it's shown behind the PROGRESS_VISUAL.<br />
        /// Optional. If not supplied, the default track visual will be shown.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap TrackVisual
        {
            get
            {
                PropertyMap temp = new PropertyMap();
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.TRACK_VISUAL).Get(temp);
                return temp;
            }
            set
            {
                if (value != null)
                {
                    Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.TRACK_VISUAL, new PropertyValue((PropertyMap)value));
                }
            }
        }

        /// <summary>
        /// The progress visual value of the progress bar, the size of the progress visual is changed based on the PROGRESS_VALUE.<br />
        /// Optional. If not supplied, then the default progress visual will be shown.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap ProgressVisual
        {
            get
            {
                PropertyMap temp = new PropertyMap();
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.PROGRESS_VISUAL).Get(temp);
                return temp;
            }
            set
            {
                if (value != null)
                {
                    Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.PROGRESS_VISUAL, new PropertyValue((PropertyMap)value));
                }
            }
        }

        /// <summary>
        /// The secondary progress visual of the progress bar, the size of the secondary progress visual is changed based on the SECONDARY_PROGRESS_VALUE.<br />
        /// Optional. If not supplied, then the secondary progress visual will not be shown.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap SecondaryProgressVisual
        {
            get
            {
                PropertyMap temp = new PropertyMap();
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.SECONDARY_PROGRESS_VISUAL).Get(temp);
                return temp;
            }
            set
            {
                if (value != null)
                {
                    Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.SECONDARY_PROGRESS_VISUAL, new PropertyValue((PropertyMap)value));
                }
            }
        }

        /// <summary>
        /// The indeterminate visual of the progress bar.<br />
        /// Optional. If not supplied, then the default indeterminate visual will be shown.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap IndeterminateVisual
        {
            get
            {
                PropertyMap temp = new PropertyMap();
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.INDETERMINATE_VISUAL).Get(temp);
                return temp;
            }
            set
            {
                if (value != null)
                {
                    Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.INDETERMINATE_VISUAL, new PropertyValue((PropertyMap)value));
                }
            }
        }

        /// <summary>
        /// The transition data for the indeterminate visual animation.<br />
        /// Optional. If not supplied, then the default animation will be played.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyArray IndeterminateVisualAnimation
        {
            get
            {
                PropertyArray temp = new PropertyArray();
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.INDETERMINATE_VISUAL_ANIMATION).Get(temp);
                return temp;
            }
            set
            {
                if (value != null)
                {
                    Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.INDETERMINATE_VISUAL_ANIMATION, new PropertyValue((PropertyArray)value));
                }
            }
        }

        /// <summary>
        /// The label visual of the progress bar.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public PropertyMap LabelVisual
        {
            get
            {
                PropertyMap temp = new PropertyMap();
                Tizen.NUI.Object.GetProperty(swigCPtr, ProgressBar.Property.LABEL_VISUAL).Get(temp);
                return temp;
            }
            set
            {
                if (value != null)
                {
                    Tizen.NUI.Object.SetProperty(swigCPtr, ProgressBar.Property.LABEL_VISUAL, new PropertyValue((PropertyMap)value));
                }
            }
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ProgressBar obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        internal ProgressBarValueChangedSignal ValueChangedSignal()
        {
            ProgressBarValueChangedSignal ret = new ProgressBarValueChangedSignal(Interop.ProgressBar.ProgressBar_ValueChangedSignal(swigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// To dispose the ProgressBar instance.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.

            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.

            if (this != null && _progressBarValueChangedCallbackDelegate != null)
            {
                ValueChangedSignal().Disconnect(_progressBarValueChangedCallbackDelegate);
            }

            if (swigCPtr.Handle != global::System.IntPtr.Zero)
            {
                if (swigCMemOwn)
                {
                    swigCMemOwn = false;
                    Interop.ProgressBar.delete_ProgressBar(swigCPtr);
                }
                swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
            }

            base.Dispose(type);
        }

        // Callback for ProgressBar ValueChanged signal
        private void OnValueChanged(IntPtr progressBar, float progressValue, float secondaryProgressValue)
        {
            ValueChangedEventArgs e = new ValueChangedEventArgs();

            // Populate all members of "e" (ValueChangedEventArgs) with real page
            e.ProgressBar = Registry.GetManagedBaseHandleFromNativePtr(progressBar) as ProgressBar;
            e.ProgressValue = progressValue;
            e.SecondaryProgressValue = secondaryProgressValue;

            if (_progressBarValueChangedEventHandler != null)
            {
                _progressBarValueChangedEventHandler(this, e);
            }
        }

        /// <summary>
        /// Event arguments that passed via the ValueChangedEventArgs.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public class ValueChangedEventArgs : EventArgs
        {
            private ProgressBar _progressBar;
            private float _progressValue;
            private float _secondaryProgressValue;

            /// <summary>
            /// ProgressBar
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public ProgressBar ProgressBar
            {
                get
                {
                    return _progressBar;
                }
                set
                {
                    _progressBar = value;
                }
            }

            /// <summary>
            /// The progress value of the progress bar, the progress runs from 0 to 1.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public float ProgressValue
            {
                get
                {
                    return _progressValue;
                }
                set
                {
                    _progressValue = value;
                }
            }

            /// <summary>
            /// The secondary progress value of the progress bar, the secondary progress runs from 0 to 1.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public float SecondaryProgressValue
            {
                get
                {
                    return _secondaryProgressValue;
                }
                set
                {
                    _secondaryProgressValue = value;
                }
            }

        }

        internal new class Property
        {
            internal static readonly int PROGRESS_VALUE = Interop.ProgressBar.ProgressBar_Property_PROGRESS_VALUE_get();
            internal static readonly int SECONDARY_PROGRESS_VALUE = Interop.ProgressBar.ProgressBar_Property_SECONDARY_PROGRESS_VALUE_get();
            internal static readonly int INDETERMINATE = Interop.ProgressBar.ProgressBar_Property_INDETERMINATE_get();
            internal static readonly int TRACK_VISUAL = Interop.ProgressBar.ProgressBar_Property_TRACK_VISUAL_get();
            internal static readonly int PROGRESS_VISUAL = Interop.ProgressBar.ProgressBar_Property_PROGRESS_VISUAL_get();
            internal static readonly int SECONDARY_PROGRESS_VISUAL = Interop.ProgressBar.ProgressBar_Property_SECONDARY_PROGRESS_VISUAL_get();
            internal static readonly int INDETERMINATE_VISUAL = Interop.ProgressBar.ProgressBar_Property_INDETERMINATE_VISUAL_get();
            internal static readonly int INDETERMINATE_VISUAL_ANIMATION = Interop.ProgressBar.ProgressBar_Property_INDETERMINATE_VISUAL_ANIMATION_get();
            internal static readonly int LABEL_VISUAL = Interop.ProgressBar.ProgressBar_Property_LABEL_VISUAL_get();
        }
    }
}