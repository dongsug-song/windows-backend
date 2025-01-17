﻿using Tizen.FH.NUI.Controls;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Tizen.FH.NUI.Samples
{
    public class TimePicker : IExample
    {
        private SampleLayout root;
        private Tizen.NUI.Components.Popup popup = null;
        private Controls.TimePicker timePicker = null;
        private Tizen.NUI.Components.Button[] button = new Tizen.NUI.Components.Button[3];
        private int num = 3;

        private static string[] mode = new string[]
        {
            "TimePicker",
            "TimePicker with AMPM",
            "TimePicker with Repeat",
        };
        public void Activate()
        {
            Window.Instance.BackgroundColor = Color.White;
            root = new SampleLayout();
            root.HeaderText = "Time Picker";

            CreateBaseTimePicker();

            for (int i = 0; i < num; i++)
            {
                button[i] = new Tizen.NUI.Components.Button("ServiceButton");
                button[i].Size2D = new Size2D(240, 80);
                button[i].Position2D = new Position2D(160 + i * 260, 1350);
                button[i].Text = mode[i];
                button[i].PointSize = 11;
                button[i].ClickEvent += ButtonClickEvent;
                root.Add(button[i]);
            }
        }

        private void CreateBaseTimePicker()
        {
            DestoryTimePicker();
            popup = new Tizen.NUI.Components.Popup("Popup");
            popup.Size2D = new Size2D(1032, 776);
            popup.Position2D = new Position2D(24, 50);
            popup.TitleText = "Timer 01";
            popup.ButtonCount = 2;
            popup.SetButtonText(0, "Cancel");
            popup.SetButtonText(1, "OK");
            popup.PopupButtonClickEvent += PopupButtonClickedEvent;
            root.Add(popup);

            timePicker = new Controls.TimePicker("DATimePicker");
            timePicker.Size2D = new Size2D(1032, 524);
            timePicker.Position2D = new Position2D(0, 0);
            popup.ContentView.Add(timePicker);
        }

        private void CreateAMPMTimePicker()
        {
            DestoryTimePicker();
            popup = new Tizen.NUI.Components.Popup("Popup");
            popup.Size2D = new Size2D(1032, 776);
            popup.Position2D = new Position2D(24, 50);
            popup.TitleText = "Timer 02";
            popup.ButtonCount = 2;
            popup.SetButtonText(0, "Cancel");
            popup.SetButtonText(1, "OK");
            popup.PopupButtonClickEvent += PopupButtonClickedEvent;
            root.Add(popup);

            timePicker = new Controls.TimePicker("DATimePickerAMPM");
            timePicker.Size2D = new Size2D(1032, 524);
            timePicker.Position2D = new Position2D(0, 0);
            popup.ContentView.Add(timePicker);
        }

        private void CreateRepeatTimePicker()
        {
            DestoryTimePicker();
            popup = new Tizen.NUI.Components.Popup("Popup");
            popup.Size2D = new Size2D(1032, 1064);
            popup.Position2D = new Position2D(24, 50);
            popup.TitleText = "Timer 02";
            popup.ButtonCount = 2;
            popup.SetButtonText(0, "Cancel");
            popup.SetButtonText(1, "OK");
            popup.PopupButtonClickEvent += PopupButtonClickedEvent;
            root.Add(popup);

            timePicker = new Controls.TimePicker("DATimePickerRepeat");
            timePicker.Size2D = new Size2D(1032, 812);
            timePicker.Position2D = new Position2D(0, 0);
            popup.ContentView.Add(timePicker);
        }


        private void PopupButtonClickedEvent(object sender, Tizen.NUI.Components.Popup.ButtonClickEventArgs e)
        {

        }

        private void ButtonClickEvent(object sender, Tizen.NUI.Components.Button.ClickEventArgs e)
        {
            Tizen.NUI.Components.Button btn = sender as Tizen.NUI.Components.Button;
            if (button[0] == btn)
            {
                CreateBaseTimePicker();
            }
            else if (button[1] == btn)
            {
                CreateAMPMTimePicker();
            }
            else if (button[2] == btn)
            {
                CreateRepeatTimePicker();
            }
        }

        private void DestoryTimePicker()
        {
            if (popup != null)
            {
                if (timePicker != null)
                {
                    popup.ContentView.Remove(timePicker);
                    timePicker.Dispose();
                    timePicker = null;
                }

                root.Remove(popup);
                popup.Dispose();
                popup = null;
            }
        }

        public void Deactivate()
        {
            if (root != null)
            {
                DestoryTimePicker();

                for (int i = 0; i < num; i++)
                {
                    if (button[i] != null)
                    {
                        root.Remove(button[i]);
                        button[i].Dispose();
                        button[i] = null;
                    }
                }

                root.Dispose();
            }
        }
    }
}
