﻿/*
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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using StyleManager = Tizen.NUI.Components.StyleManager;

namespace Tizen.FH.NUI.Controls
{
    /// <summary>
    /// InputField is a editable input compoment with delete button or delete and add button.
    /// After pressing Return key, search button will show
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class InputField : Tizen.NUI.Components.InputField
    {
        // the cancel button
        private ImageView cancelBtn = null;
        // the delete button
        private ImageView deleteBtn = null;
        // the add button background image
        private ImageView addBtnBg = null;
        // the add button overlay image
        private ImageView addBtnOverlay = null;
        // the add button foreground image
        private ImageView addBtnFg = null;
        // the search button
        private ImageView searchBtn = null;
        
        private Style style = Style.None;

        private ControlStates textFieldState = ControlStates.Normal;
        private TextState textState = TextState.Guide;
        private bool isDoneKeyPressed = false;

        // the attributes of the inputField
        private InputFieldAttributes inputFieldAttrs = null;

        private EventHandler<ButtonClickArgs> cancelBtnClickHandler;
        private EventHandler<ButtonClickArgs> deleteBtnClickHandler;
        private EventHandler<ButtonClickArgs> addBtnClickHandler;
        private EventHandler<ButtonClickArgs> searchBtnClickHandler;
        private EventHandler<KeyEventArgs> keyEventHandler;

        /// <summary>
        /// Initializes a new instance of the InputField class.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public InputField() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the InputField class.
        /// </summary>
        /// <param name="style">Create Header by special style defined in UX.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public InputField(string style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// Click Event attached to Cancel Button.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<ButtonClickArgs> CancelButtonClickEvent
        {
            add
            {
                cancelBtnClickHandler += value;
            }
            remove
            {
                cancelBtnClickHandler -= value;
            }
        }

        /// <summary>
        /// Click Event attached to Delete Button
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<ButtonClickArgs> DeleteButtonClickEvent
        {
            add
            {
                deleteBtnClickHandler += value;
            }
            remove
            {
                deleteBtnClickHandler -= value;
            }
        }

        /// <summary>
        /// Click Event attached to Add Button
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<ButtonClickArgs> AddButtonClickEvent
        {
            add
            {
                addBtnClickHandler += value;
            }
            remove
            {
                addBtnClickHandler -= value;
            }
        }

        /// <summary>
        /// Click Event attached to Search Button
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<ButtonClickArgs> SearchButtonClickEvent
        {
            add
            {
                searchBtnClickHandler += value;
            }
            remove
            {
                searchBtnClickHandler -= value;
            }
        }

        /// <summary>
        /// The handler Event of Key
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler<KeyEventArgs> KeyEvent
        {
            add
            {
                keyEventHandler += value;
            }
            remove
            {
                keyEventHandler -= value;
            }
        }

        /// <summary>
        /// The  state of Button Click
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public enum ButtonClickState
        {
            /// <summary> Press down </summary>
            /// <since_tizen> 6 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            [EditorBrowsable(EditorBrowsableState.Never)]
            PressDown,
            /// <summary> Bounce up </summary>
            /// <since_tizen> 6 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            [EditorBrowsable(EditorBrowsableState.Never)]
            BounceUp
        }

        private enum TextState
        {
            Guide,
            Input,
        }

        private enum Style
        {
            None,
            Default,
            StyleB,
            SearchBar
        }

        /// <summary>
        /// Set the status of the Input Field editable or not.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool StateEnabled
        {
            get
            {
                return base.StateEnabled;
            }
            set
            {
                if (base.StateEnabled == value)
                {
                    return;
                }
                UpdateComponentsByStateEnabledChanged(value);
                base.StateEnabled = value;
            }
        }

        /// <summary>
        /// Dispose Input Field and all children on it.
        /// </summary>
        /// <param name="type">Dispose type.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }
            if (type == DisposeTypes.Explicit)
            {
                if (cancelBtn != null)
                {
                    cancelBtn.TouchEvent -= OnCancelBtnTouchEvent;
                    this.Remove(cancelBtn);
                    cancelBtn.Dispose();
                    cancelBtn = null;
                }
                if (deleteBtn != null)
                {
                    deleteBtn.TouchEvent -= OnDeleteBtnTouchEvent;
                    this.Remove(deleteBtn);
                    deleteBtn.Dispose();
                    deleteBtn = null;
                }
                if (searchBtn != null)
                {
                    searchBtn.TouchEvent -= OnSearchBtnTouchEvent;
                    this.Remove(searchBtn);
                    searchBtn.Dispose();
                    searchBtn = null;
                }
                if (addBtnFg != null)
                {
                    addBtnFg.TouchEvent -= OnAddBtnTouchEvent;
                    if (addBtnOverlay != null)
                    {
                        addBtnOverlay.Remove(addBtnFg);
                    }
                    addBtnFg.Dispose();
                    addBtnFg = null;
                }
                if (addBtnOverlay != null)
                {
                    if (addBtnBg != null)
                    {
                        addBtnBg.Remove(addBtnOverlay);
                    }
                    addBtnOverlay.Dispose();
                    addBtnOverlay = null;
                }
                if (addBtnBg != null)
                {
                    this.Remove(addBtnBg);
                    addBtnBg.Dispose();
                    addBtnBg = null;
                }
            }
            base.Dispose(type);
        }

        /// <summary>
        /// Get Input Field attribues.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override Attributes GetAttributes()
        {
            return new InputFieldAttributes();
        }

        /// <summary>
        /// Update Input Field by attributes.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnUpdate()
        {
            RelayoutTextField(false);
            base.OnUpdate();
            ApplyAttributes(cancelBtn, inputFieldAttrs.CancelButtonAttributes);
            ApplyAttributes(deleteBtn, inputFieldAttrs.DeleteButtonAttributes);
            ApplyAttributes(addBtnBg, inputFieldAttrs.AddButtonBgAttributes);
            ApplyAttributes(addBtnOverlay, inputFieldAttrs.AddButtonOverlayAttributes);
            ApplyAttributes(addBtnFg, inputFieldAttrs.AddButtonFgAttributes);
            ApplyAttributes(searchBtn, inputFieldAttrs.SearchButtonAttributes);
            RelayoutComponents();
            UpdateComponentsByStateEnabledChanged(base.StateEnabled);
            OnLayoutDirectionChanged();
        }

        /// <summary>
        /// Theme change callback when theme is changed, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            InputFieldAttributes tempAttributes = StyleManager.Instance.GetAttributes(base.style) as InputFieldAttributes;
            if (tempAttributes != null)
            {
                attributes = inputFieldAttrs = tempAttributes;
                RelayoutRequest();
            }
        }

        /// <summary>
        ///  Text field focus gain callback when focus is getted, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnTextFieldFocusGained(object source, EventArgs e)
        {
            // when press on TextField, it will gain focus
            textFieldState = ControlStates.Selected;
            RelayoutComponents(false, true, true, false);
        }

        /// <summary>
        /// Text field lost gain  callback when focus is lost, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnTextFieldFocusLost(object source, EventArgs e)
        {
            textFieldState = ControlStates.Normal;
            RelayoutComponents(false, true, true, false);
        }

        /// <summary>
        /// Text field change callback when text  is changed, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnTextFieldTextChanged(object sender, TextField.TextChangedEventArgs e)
        {
            if (sender is TextField)
            {
                TextField textField = sender as TextField;
                int textLen = textField.Text.Length;
                if (textLen == 0)
                {
                    textState = TextState.Guide;
                }
                else
                {
                    textState = TextState.Input;
                }
                isDoneKeyPressed = false;
                RelayoutComponents(false, true, true, false);
            }
        }

        /// <summary>
        /// Text field key callback when "Return"  click down, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override bool OnTextFieldKeyEvent(object source, KeyEventArgs e)
        {
            if (keyEventHandler != null)
            {
                keyEventHandler(this, e);
            }

            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Return")
                {
                    // when press "Return" key("Done" key in IME), the searchBtn should show.
                    isDoneKeyPressed = true;
                    RelayoutComponents(false, false, true, false);
                    return true;
                }
            }
            return false;
        }

        private void Initialize()
        {
            inputFieldAttrs = attributes as InputFieldAttributes;
            if (inputFieldAttrs == null)
            {
                throw new Exception("Fail to get the InputField attributes.");
            }
            if (inputFieldAttrs.CancelButtonAttributes != null && cancelBtn == null)
            {
                cancelBtn = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                    PositionUsesPivotPoint = true
                };
                this.Add(cancelBtn);
                cancelBtn.TouchEvent += OnCancelBtnTouchEvent;
            }
            if (inputFieldAttrs.DeleteButtonAttributes != null && deleteBtn == null)
            {
                deleteBtn = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                    PositionUsesPivotPoint = true
                };
                this.Add(deleteBtn);
                deleteBtn.TouchEvent += OnDeleteBtnTouchEvent;
            }
            if (inputFieldAttrs.SearchButtonAttributes != null && searchBtn == null)
            {
                searchBtn = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                    PositionUsesPivotPoint = true
                };
                this.Add(searchBtn);
                searchBtn.TouchEvent += OnSearchBtnTouchEvent;
            }
            if (inputFieldAttrs.AddButtonBgAttributes != null && addBtnBg == null)
            {
                addBtnBg = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                    PositionUsesPivotPoint = true
                };
                this.Add(addBtnBg);
            }
            if (inputFieldAttrs.AddButtonOverlayAttributes != null && addBtnOverlay == null)
            {
                addBtnOverlay = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                };
                addBtnBg.Add(addBtnOverlay);
            }
            if (inputFieldAttrs.AddButtonFgAttributes != null && addBtnFg == null)
            {
                addBtnFg = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent,
                };
                addBtnOverlay.Add(addBtnFg);
                addBtnFg.TouchEvent += OnAddBtnTouchEvent;
            }

            if (cancelBtn != null)
            {
                if (searchBtn == null)
                {
                    style = Style.Default;
                }
                else
                {
                    style = Style.SearchBar;
                }
            }
            else
            {
                if (deleteBtn != null && addBtnBg != null && addBtnOverlay != null && addBtnFg != null)
                {
                    style = Style.StyleB;
                }
            }
        }
        private void OnLayoutDirectionChanged()
        {
            if (inputFieldAttrs == null) return;

            if (LayoutDirection == ViewLayoutDirectionType.LTR)
            {
                if (cancelBtn)
                {
                    if (inputFieldAttrs.CancelButtonAttributes != null)
                    {
                        inputFieldAttrs.CancelButtonAttributes.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        inputFieldAttrs.CancelButtonAttributes.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        inputFieldAttrs.CancelButtonAttributes.PositionUsesPivotPoint = true;
                    }
                    cancelBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    cancelBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    cancelBtn.PositionUsesPivotPoint = true;
                }
                if(addBtnBg)
                {
                    if (inputFieldAttrs.AddButtonBgAttributes != null)
                    {
                        inputFieldAttrs.AddButtonBgAttributes.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        inputFieldAttrs.AddButtonBgAttributes.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        inputFieldAttrs.AddButtonBgAttributes.PositionUsesPivotPoint = true;
                    }
                    addBtnBg.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    addBtnBg.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    addBtnBg.PositionUsesPivotPoint = true;
                }
                if(searchBtn)
                {
                    if (inputFieldAttrs.SearchButtonAttributes != null)
                    {
                        inputFieldAttrs.SearchButtonAttributes.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        inputFieldAttrs.SearchButtonAttributes.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        inputFieldAttrs.SearchButtonAttributes.PositionUsesPivotPoint = true;
                    }
                    searchBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    searchBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    searchBtn.PositionUsesPivotPoint = true;
                }
                if(deleteBtn)
                {
                    if (inputFieldAttrs.DeleteButtonAttributes != null)
                    {
                        inputFieldAttrs.DeleteButtonAttributes.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        inputFieldAttrs.DeleteButtonAttributes.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        inputFieldAttrs.DeleteButtonAttributes.PositionUsesPivotPoint = true;
                    }
                    deleteBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    deleteBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    deleteBtn.PositionUsesPivotPoint = true;
                }
            }
            else
            {
                if (cancelBtn)
                {
                    if (inputFieldAttrs.AddButtonBgAttributes != null)
                    {
                        inputFieldAttrs.AddButtonBgAttributes.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        inputFieldAttrs.AddButtonBgAttributes.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        inputFieldAttrs.AddButtonBgAttributes.PositionUsesPivotPoint = true;
                    }
                    cancelBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    cancelBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    cancelBtn.PositionUsesPivotPoint = true;
                }
                if (addBtnBg)
                {
                    if (inputFieldAttrs.AddButtonBgAttributes != null)
                    {
                        inputFieldAttrs.AddButtonBgAttributes.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        inputFieldAttrs.AddButtonBgAttributes.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        inputFieldAttrs.AddButtonBgAttributes.PositionUsesPivotPoint = true;
                    }
                    addBtnBg.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    addBtnBg.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    addBtnBg.PositionUsesPivotPoint = true;
                }
                if (searchBtn)
                {
                    if (inputFieldAttrs.SearchButtonAttributes != null)
                    {
                        inputFieldAttrs.SearchButtonAttributes.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        inputFieldAttrs.SearchButtonAttributes.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        inputFieldAttrs.SearchButtonAttributes.PositionUsesPivotPoint = true;
                    }
                    searchBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    searchBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    searchBtn.PositionUsesPivotPoint = true;
                }
                if (deleteBtn)
                {
                    if (inputFieldAttrs.DeleteButtonAttributes != null)
                    {
                        inputFieldAttrs.DeleteButtonAttributes.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        inputFieldAttrs.DeleteButtonAttributes.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        inputFieldAttrs.DeleteButtonAttributes.PositionUsesPivotPoint = true;
                    }
                    deleteBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    deleteBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    deleteBtn.PositionUsesPivotPoint = true;
                }
            }
        }

        private void RelayoutComponents(bool shouldUpdate = true, bool enableRelayoutDefault = true, bool enableRelayoutSearchBar = true, bool enableRelayoutStyleB = true)
        {
            switch(style)
            {
                case Style.Default:
                    if (enableRelayoutDefault)
                    {
                        RelayoutComponentsForDefault(shouldUpdate);
                    }
                    break;
                case Style.SearchBar:
                    if (enableRelayoutSearchBar)
                    {
                        RelayoutComponentsForSearchBar(shouldUpdate);
                    }
                    break;
                case Style.StyleB:
                    if (enableRelayoutStyleB)
                    {
                        RelayoutComponentsForStyleB(shouldUpdate);
                    }
                    break;
                default:
                    break;
            }
        }

        private void RelayoutComponentsForDefault(bool shouldUpdate)
        {
            if (cancelBtn == null)
            {
                return;
            }
            // 2 type layouts:
            // #1 TextField                 normal state, text's length == 0;
            // #2 TextField + CancelBtn     except #1.
            int space = inputFieldAttrs.Space ?? 0;
            if (textFieldState == ControlStates.Normal && textState == TextState.Guide)
            {
                SetTextFieldSize2D((int)Size.Width - space * 2, (int)Size.Height);
                cancelBtn.Hide();
            }
            else
            {
                SetTextFieldSize2D((int)(Size.Width - space * 2 - cancelBtn.Size.Width - SpaceBetweenTextFieldAndRightButton()), (int)Size.Height);
                cancelBtn.Show();
            }
            if (shouldUpdate)
            {
                if(this.LayoutDirection == ViewLayoutDirectionType.RTL)
                {
                    SetTextFieldPosX(-space);
                    cancelBtn.PositionX = space;
                }
                else
                {
                    SetTextFieldPosX(space);
                    cancelBtn.PositionX = -space;
                }
            }
        }

        private void RelayoutComponentsForSearchBar(bool shouldUpdate)
        {
            if (searchBtn == null || cancelBtn == null)
            {
                return;
            }
            // 3 type layouts:
            // #1 SearchBtn + TextField                 normal state, text's length == 0;
            // #2 SearchBtn + TextField + CancelBtn     input state, text's length > 0, press "Done" key on IME;
            // #3 TextField + CancelBtn                 excepte #1 & #2.
            int space = inputFieldAttrs.Space ?? 0;
            int textfieldX = 0;
            if (textFieldState == ControlStates.Normal && textState == TextState.Guide)
            {// #1
                int spaceBetweenTextFieldAndLeftButton = SpaceBetweenTextFieldAndLeftButton();
                SetTextFieldSize2D((int)(Size.Width - space * 2 - searchBtn.Size.Width - spaceBetweenTextFieldAndLeftButton), (int)Size.Height);
                textfieldX = (int)(space + searchBtn.Size.Width + spaceBetweenTextFieldAndLeftButton);
                searchBtn.Show();
                cancelBtn.Hide();
            }
            else if (textFieldState == ControlStates.Selected && textState == TextState.Input && isDoneKeyPressed)
            {// #2
                int spaceBetweenTextFieldAndLeftButton = SpaceBetweenTextFieldAndLeftButton();
                int spaceBetweenTextFieldAndRightButton = SpaceBetweenTextFieldAndRightButton();
                SetTextFieldSize2D((int)(Size.Width - space * 2 - searchBtn.Size.Width - spaceBetweenTextFieldAndLeftButton - cancelBtn.Size.Width - spaceBetweenTextFieldAndRightButton), (int)Size.Height);
                textfieldX = (int)(space + searchBtn.Size.Width + spaceBetweenTextFieldAndLeftButton);
                searchBtn.Show();
                cancelBtn.Show();
            }
            else
            {// #3
                int spaceBetweenTextFieldAndRighttButton = SpaceBetweenTextFieldAndRightButton();
                SetTextFieldSize2D((int)(Size.Width - space * 2 - cancelBtn.Size.Width - spaceBetweenTextFieldAndRighttButton), (int)Size.Height);
                textfieldX = space;
                searchBtn.Hide();
                cancelBtn.Show();
            }

            if (this.LayoutDirection == ViewLayoutDirectionType.RTL)
            {
                if (shouldUpdate)
                {
                    searchBtn.PositionX = -space;
                    cancelBtn.PositionX = space;
                }
                SetTextFieldPosX(-textfieldX);
            }
            else
            {
                if (shouldUpdate)
                {
                    searchBtn.PositionX = space;
                    cancelBtn.PositionX = -space;
                }
                SetTextFieldPosX(textfieldX);
            }
        }

        private void RelayoutComponentsForStyleB(bool shouldUpdate)
        {
            if (addBtnBg == null || deleteBtn == null)
            {
                return;
            }
            if (!shouldUpdate)
            {
                return;
            }
            int space = inputFieldAttrs.Space ?? 0;
            int spaceBetweenTextFieldAndRightButton = SpaceBetweenTextFieldAndRightButton();
            SetTextFieldSize2D((int)(Size.Width - space - spaceBetweenTextFieldAndRightButton - deleteBtn.Size.Width - addBtnBg.Size.Width), (int)Size.Height);

            if (this.LayoutDirection == ViewLayoutDirectionType.RTL)
            {
                SetTextFieldPosX(-space);

                addBtnBg.PositionX = 0;
                deleteBtn.PositionX = addBtnBg.Size.Width;
            }
            else
            {
                SetTextFieldPosX(space);

                addBtnBg.PositionX = 0;
                deleteBtn.PositionX = -addBtnBg.Size.Width;
            }

        }

        private int SpaceBetweenTextFieldAndRightButton()
        {
            int space = 0;
            if (inputFieldAttrs != null && inputFieldAttrs.SpaceBetweenTextFieldAndRightButton != null)
            {
                space = inputFieldAttrs.SpaceBetweenTextFieldAndRightButton.Value;
            }
            return space;
        }

        private int SpaceBetweenTextFieldAndLeftButton()
        {
            int space = 0;
            if (inputFieldAttrs != null && inputFieldAttrs.SpaceBetweenTextFieldAndLeftButton != null)
            {
                space = inputFieldAttrs.SpaceBetweenTextFieldAndLeftButton.Value;
            }
            return space;
        }
        
        private void UpdateComponentsByStateEnabledChanged(bool isEnabled)
        {
            if (isEnabled)
            {
                UpdateTextFieldTextColor(ControlStates.Selected);
                UpdateDeleteBtnState(ControlStates.Normal);
                UpdateAddBtnState(ControlStates.Normal);
            }
            else
            {
                UpdateTextFieldTextColor(ControlStates.Disabled);
                UpdateDeleteBtnState(ControlStates.Disabled);
                UpdateAddBtnState(ControlStates.Disabled);
            }
        }
        
        private void UpdateTextFieldTextColor(ControlStates state)
        {
            if (inputFieldAttrs != null && inputFieldAttrs.InputBoxAttributes != null && inputFieldAttrs.InputBoxAttributes.TextColor != null)
            {
                switch (state)
                {
                    case ControlStates.Disabled:
                    case ControlStates.DisabledSelected:
                        SetTextFieldTextColor(inputFieldAttrs.InputBoxAttributes.TextColor.Disabled);
                        break;
                    case ControlStates.Normal:
                    case ControlStates.Selected:
                        SetTextFieldTextColor(inputFieldAttrs.InputBoxAttributes.TextColor.Normal);
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateDeleteBtnState(ControlStates state)
        {
            if (deleteBtn != null && inputFieldAttrs != null && inputFieldAttrs.DeleteButtonAttributes != null && inputFieldAttrs.DeleteButtonAttributes.ResourceURL != null)
            {
                switch (state)
                {
                    case ControlStates.Disabled:
                    case ControlStates.DisabledSelected:
                        deleteBtn.ResourceUrl = inputFieldAttrs.DeleteButtonAttributes.ResourceURL.Disabled;
                        break;
                    case ControlStates.Selected:
                        deleteBtn.ResourceUrl = inputFieldAttrs.DeleteButtonAttributes.ResourceURL.Pressed;
                        break;
                    case ControlStates.Normal:
                        deleteBtn.ResourceUrl = inputFieldAttrs.DeleteButtonAttributes.ResourceURL.Normal;
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateAddBtnState(ControlStates state)
        {
            if (inputFieldAttrs == null || addBtnBg == null || addBtnOverlay == null || addBtnFg == null)
            {
                return;
            }
            switch (state)
            {
                case ControlStates.Disabled:
                case ControlStates.DisabledSelected:
                    {
                        if (inputFieldAttrs.AddButtonBgAttributes != null && inputFieldAttrs.AddButtonBgAttributes.ResourceURL != null)
                        {
                            addBtnBg.ResourceUrl = inputFieldAttrs.AddButtonBgAttributes.ResourceURL.Disabled;
                        }
                        if (inputFieldAttrs.AddButtonOverlayAttributes != null && inputFieldAttrs.AddButtonOverlayAttributes.ResourceURL != null)
                        {
                            addBtnOverlay.ResourceUrl = inputFieldAttrs.AddButtonOverlayAttributes.ResourceURL.Disabled;
                        }
                        if (inputFieldAttrs.AddButtonFgAttributes != null && inputFieldAttrs.AddButtonFgAttributes.ResourceURL != null)
                        {
                            addBtnFg.ResourceUrl = inputFieldAttrs.AddButtonFgAttributes.ResourceURL.Disabled;
                        }
                    }
                    break;
                case ControlStates.Selected:
                    {
                        if (inputFieldAttrs.AddButtonBgAttributes != null && inputFieldAttrs.AddButtonBgAttributes.ResourceURL != null)
                        {
                            addBtnBg.ResourceUrl = inputFieldAttrs.AddButtonBgAttributes.ResourceURL.Pressed;
                        }
                        if (inputFieldAttrs.AddButtonOverlayAttributes != null && inputFieldAttrs.AddButtonOverlayAttributes.ResourceURL != null)
                        {
                            addBtnOverlay.ResourceUrl = inputFieldAttrs.AddButtonOverlayAttributes.ResourceURL.Pressed;
                        }
                        if (inputFieldAttrs.AddButtonFgAttributes != null && inputFieldAttrs.AddButtonFgAttributes.ResourceURL != null)
                        {
                            addBtnFg.ResourceUrl = inputFieldAttrs.AddButtonFgAttributes.ResourceURL.Pressed;
                        }
                    }
                    break;
                case ControlStates.Normal:
                    {
                        if (inputFieldAttrs.AddButtonBgAttributes != null && inputFieldAttrs.AddButtonBgAttributes.ResourceURL != null)
                        {
                            addBtnBg.ResourceUrl = inputFieldAttrs.AddButtonBgAttributes.ResourceURL.Normal;
                        }
                        if (inputFieldAttrs.AddButtonOverlayAttributes != null && inputFieldAttrs.AddButtonOverlayAttributes.ResourceURL != null)
                        {
                            addBtnOverlay.ResourceUrl = inputFieldAttrs.AddButtonOverlayAttributes.ResourceURL.Normal;
                        }
                        if (inputFieldAttrs.AddButtonFgAttributes != null && inputFieldAttrs.AddButtonFgAttributes.ResourceURL != null)
                        {
                            addBtnFg.ResourceUrl = inputFieldAttrs.AddButtonFgAttributes.ResourceURL.Normal;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private bool OnDeleteBtnTouchEvent(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                if (deleteBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.PressDown;
                    deleteBtnClickHandler(this, args);
                }
                UpdateDeleteBtnState(ControlStates.Selected);
            }
            else if (state == PointStateType.Finished)
            {
                if (deleteBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.BounceUp;
                    deleteBtnClickHandler(this, args);
                }
                UpdateDeleteBtnState(ControlStates.Normal);
            }
            return true;
        }

        private bool OnSearchBtnTouchEvent(object source, TouchEventArgs e)
        {
            if (textState == TextState.Guide)
            {
                return true;
            }
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                if (searchBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.PressDown;
                    searchBtnClickHandler(this, args);
                }
            }
            else if (state == PointStateType.Finished)
            {
                if (searchBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.BounceUp;
                    searchBtnClickHandler(this, args);
                }
            }
            return true;
        }

        private bool OnAddBtnTouchEvent(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                if (addBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.PressDown;
                    addBtnClickHandler(this, args);
                }
                UpdateAddBtnState(ControlStates.Selected);
            }
            else if (state == PointStateType.Finished)
            {
                if (addBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.BounceUp;
                    addBtnClickHandler(this, args);
                }
                UpdateAddBtnState(ControlStates.Normal);
            }
            return true;
        }

        private bool OnCancelBtnTouchEvent(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                if (cancelBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.PressDown;
                    cancelBtnClickHandler(this, args);
                }
            }
            else if (state == PointStateType.Finished)
            {
                if (cancelBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.BounceUp;
                    cancelBtnClickHandler(this, args);
                }
            }
            return true;
        }

        public class ButtonClickArgs : EventArgs
        {
            public ButtonClickState State;
        }
    }
}
