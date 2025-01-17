﻿using Tizen.FH.NUI.Controls;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Tizen.FH.NUI.Samples
{
    public class Navigation : IExample
    {
        private SampleLayout root;

        private TextLabel text = null;
        private Controls.Navigation whiteNavigation = null;
        private Controls.Navigation blackNavigation = null;
        private Controls.Navigation conditionNavigation = null;
        private Controls.Navigation blackConditionNavigation = null;
        private Controls.Navigation whiteEditNavigation = null;
        private Controls.Navigation blackEditNavigation = null;

        private static string[] itemPressImage = new string[]
        {
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_slideshow_press.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_calendar_press.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_copy_press.png",
        };
        private static string[] itemNormalImage = new string[]
        {
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_slideshow.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_calendar.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_copy.png",
        };
        private static string[] itemDimImage = new string[]
        {
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_slideshow_dim.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_calendar_dim.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_copy_dim.png",
        };

        private static string[] itemBlackPressImage = new string[]
        {
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_copy_b_press.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_play_b_press.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_search_b_press.png",
        };
        private static string[] itemBlackNormalImage = new string[]
        {
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_copy_b.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_play_b.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_search_b.png",
        };
        private static string[] itemBlackDimImage = new string[]
        {
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_copy_b_dim.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_play_b_dim.png",
            CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_search_b_dim.png",
        };

        public void Activate()
        {
            Window.Instance.BackgroundColor = Color.White;
            root = new SampleLayout();
            root.HeaderText = "Navigation";

            text = new TextLabel();
            text.PointSize = 14;
            text.Text = "Create Navigation by style";
            text.Size2D = new Size2D(480, 100);
            text.Position2D = new Position2D(300, 10);
            text.MultiLine = true;
            root.Add(text);

            ////////white navigation//////////
            #region WhiteNaviagtion
            whiteNavigation = new Controls.Navigation("Back");
            whiteNavigation.Position2D = new Position2D(100, 150);

            root.Add(whiteNavigation);

            Controls.Navigation.NavigationItemData backItem = new Controls.Navigation.NavigationItemData("WhiteBackItem");
            whiteNavigation.AddItem(backItem);
            #endregion

            ////////black navigation//////////
            #region BlackNavigation
            blackNavigation = new Controls.Navigation("Back");
            blackNavigation.Position2D = new Position2D(300, 150);

            root.Add(blackNavigation);

            Controls.Navigation.NavigationItemData blackBackItem = new Controls.Navigation.NavigationItemData("BlackBackItem");
            blackNavigation.AddItem(blackBackItem);
            #endregion

            //////condition navigation//////////
            #region WhiteConditionNavigation
            conditionNavigation = new Controls.Navigation("WhiteCondition");
            conditionNavigation.Position2D = new Position2D(100, 400);
            conditionNavigation.ItemChangedEvent += NavigationItemChangedEvent;
            root.Add(conditionNavigation);

            for (int i = 0; i < 3; i++)
            {
                Controls.Navigation.NavigationItemData conditionItem = new Controls.Navigation.NavigationItemData("WhiteConditionItem");
                conditionItem.Size = new Size(116, 128);
                conditionItem.Text = "Text " + i;
                conditionItem.SubText = "SubText " + i;
                conditionItem.IconURLSelector = new StringSelector
                {
                    Pressed = itemPressImage[i],
                    Disabled = itemDimImage[i],
                    DisabledFocused = itemDimImage[i],
                    DisabledSelected = itemDimImage[i],
                    Other = itemNormalImage[i]
                };
                conditionNavigation.AddItem(conditionItem);
            }
            #endregion

            //////black condition navigation//////////
            #region BlackConditionNavigation
            blackConditionNavigation = new Controls.Navigation("BlackCondition");
            blackConditionNavigation.Position2D = new Position2D(300, 400);
            blackConditionNavigation.ItemChangedEvent += NavigationItemChangedEvent;
            root.Add(blackConditionNavigation);

            for (int i = 0; i < 3; i++)
            {
                Controls.Navigation.NavigationItemData conditionItem = new Controls.Navigation.NavigationItemData("BlackConditionItem");
                conditionItem.Size = new Size(116, 128);
                conditionItem.Text = "Text " + i;
                conditionItem.SubText = "SubText " + i;
                conditionItem.IconURLSelector = new StringSelector
                {
                    Pressed = itemBlackPressImage[i],
                    Disabled = itemBlackDimImage[i],
                    DisabledFocused = itemBlackDimImage[i],
                    DisabledSelected = itemBlackDimImage[i],
                    Other = itemBlackNormalImage[i]
                };
                blackConditionNavigation.AddItem(conditionItem);
            }
            #endregion

            //////////White Edit Mode///////////////
            #region WhiteEditModeNavigation
            whiteEditNavigation = new Controls.Navigation("WhiteEditMode");
            whiteEditNavigation.Size = new Size(178, 800);
            whiteEditNavigation.Position2D = new Position2D(500, 150);
            whiteEditNavigation.ItemChangedEvent += NavigationItemChangedEvent;
            root.Add(whiteEditNavigation);

            Controls.Navigation.NavigationItemData firstEditItem = new Controls.Navigation.NavigationItemData("WhiteEditModeFirstItem");
            firstEditItem.Size = new Size(178, 184);
            firstEditItem.Text = "1";
            firstEditItem.SubText = "SELECTED";
            whiteEditNavigation.AddItem(firstEditItem);

            for (int i = 0; i < 2; i++)
            {
                Controls.Navigation.NavigationItemData editItem = new Controls.Navigation.NavigationItemData("WhiteEditModeItem");
                editItem.Size = new Size(178, 108);
                editItem.Text = "Text " + i;
                editItem.IconURLSelector = new StringSelector
                {
                    Pressed = itemPressImage[i],
                    Disabled = itemDimImage[i],
                    DisabledFocused = itemDimImage[i],
                    DisabledSelected = itemDimImage[i],
                    Other = itemNormalImage[i]
                };
                whiteEditNavigation.AddItem(editItem);
            }
            Controls.Navigation.NavigationItemData editLastItem = new Controls.Navigation.NavigationItemData("WhiteEditModeLastItem");
            editLastItem.Size = new Size(178, 166);
            editLastItem.Text = "Cancel";
            editLastItem.IconURLSelector = new StringSelector
            {
                Pressed = CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel_press.png",
                Disabled = CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel_dim.png",
                DisabledFocused = CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel_dim.png",
                DisabledSelected = CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel_dim.png",
                Other = CommonResource.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel.png"
            };
            whiteEditNavigation.AddItem(editLastItem);
            #endregion

            //////////Black Edit Mode///////////////
            #region BlackEditModeNavigation
            blackEditNavigation = new Controls.Navigation("BlackEditMode");
            blackEditNavigation.Size = new Size(178, 800);
            blackEditNavigation.Position2D = new Position2D(700, 150);
            blackEditNavigation.ItemChangedEvent += NavigationItemChangedEvent;
            root.Add(blackEditNavigation);

            Controls.Navigation.NavigationItemData firstEditItem2 = new Controls.Navigation.NavigationItemData("BlackEditModeFirstItem");
            firstEditItem2.Size = new Size(178, 184);
            firstEditItem2.Text = "1";
            firstEditItem2.SubText = "SELECTED";
            blackEditNavigation.AddItem(firstEditItem2);

            for (int i = 0; i < 2; i++)
            {
                Controls.Navigation.NavigationItemData editItem = new Controls.Navigation.NavigationItemData("BlackEditModeItem");
                editItem.Size = new Size(178, 108);
                editItem.Text = "Text " + i;
                editItem.IconURLSelector = new StringSelector
                {
                    Pressed = itemBlackPressImage[i],
                    Disabled = itemBlackDimImage[i],
                    DisabledFocused = itemBlackDimImage[i],
                    DisabledSelected = itemBlackDimImage[i],
                    Other = itemBlackNormalImage[i]
                };
                blackEditNavigation.AddItem(editItem);
            }
            Controls.Navigation.NavigationItemData editLastItem2 = new Controls.Navigation.NavigationItemData("BlackEditModeLastItem");
            editLastItem2.Size = new Size(178, 166);
            editLastItem2.Text = "Cancel";
            editLastItem2.IconURLSelector = new StringSelector
            {
                Pressed = CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_cancel_b_press.png",
                Disabled = CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_cancel_b_dim.png",
                DisabledFocused = CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_cancel_b_dim.png",
                DisabledSelected = CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_cancel_b_dim.png",
                Other = CommonResource.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_cancel_b.png"
            };
            blackEditNavigation.AddItem(editLastItem2);
            #endregion
        }

        private void NavigationItemChangedEvent(object sender, Controls.Navigation.ItemChangeEventArgs e)
        {
            //conditionNavigation.DeleteItem(0);
            text.Text = "Create Navigation by style, Selected index from " + e.PreviousIndex + " to " + e.CurrentIndex;
        }

        public void Deactivate()
        {
            if (root != null)
            {
                if (text != null)
                {
                    root.Remove(text);
                    text.Dispose();
                    text = null;
                }

                if (whiteNavigation != null)
                {
                    root.Remove(whiteNavigation);
                    whiteNavigation.Dispose();
                    whiteNavigation = null;
                }
                if (blackNavigation != null)
                {
                    root.Remove(blackNavigation);
                    blackNavigation.Dispose();
                    blackNavigation = null;
                }
                if (conditionNavigation != null)
                {
                    root.Remove(conditionNavigation);
                    conditionNavigation.Dispose();
                    conditionNavigation = null;
                }
                if (blackConditionNavigation != null)
                {
                    root.Remove(blackConditionNavigation);
                    blackConditionNavigation.Dispose();
                    blackConditionNavigation = null;
                }

                if (whiteEditNavigation != null)
                {
                    root.Remove(whiteEditNavigation);
                    whiteEditNavigation.Dispose();
                    whiteEditNavigation = null;
                }

                if (blackEditNavigation != null)
                {
                    root.Remove(blackEditNavigation);
                    blackEditNavigation.Dispose();
                    blackEditNavigation = null;
                }

                root.Dispose();
                root = null;
            }
        }
    }
}
