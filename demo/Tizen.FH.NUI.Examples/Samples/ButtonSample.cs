﻿using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Controls;

namespace Tizen.FH.NUI.Samples
{
    public class ButtonSample : IExample
    {
        private SampleLayout root;

        private Button BasicButtonNormal;
        private Button BasicButtonDisabled;

        private Button ServiceButtonNormal;
        private Button ServiceButtonDisabled;

        private Button ToggleButtonNormal;
        private Button ToggleButtonDisabled;

        private Button OvalButtonNormal;
        private Button OvalButtonDisabled;

        public void Activate()
        {
            root = new SampleLayout();
            root.HeaderText = "Button";

            BasicButtonNormal = new Button("BasicButton");
            BasicButtonNormal.Size2D = new Size2D(300, 80);
            BasicButtonNormal.Position2D = new Position2D(156, 50);
            BasicButtonNormal.Text = "BasicButton";
            root.Add(BasicButtonNormal);

            BasicButtonDisabled = new Button("BasicButton");
            BasicButtonDisabled.Size2D = new Size2D(300, 80);
            BasicButtonDisabled.Position2D = new Position2D(624, 50);
            BasicButtonDisabled.Text = "BasicButton";
            BasicButtonDisabled.IsEnabled = false;
            root.Add(BasicButtonDisabled);

            ServiceButtonNormal = new Button("ServiceButton");
            ServiceButtonNormal.Size2D = new Size2D(300, 80);
            ServiceButtonNormal.Position2D = new Position2D(156, 200);
            ServiceButtonNormal.Text = "ServiceButton";
            root.Add(ServiceButtonNormal);

            ServiceButtonDisabled = new Button("ServiceButton");
            ServiceButtonDisabled.Size2D = new Size2D(300, 80);
            ServiceButtonDisabled.Position2D = new Position2D(624, 200);
            ServiceButtonDisabled.Text = "ServiceButton";
            ServiceButtonDisabled.IsEnabled = false;
            root.Add(ServiceButtonDisabled);

            ToggleButtonNormal = new Button("ToggleButton");
            ToggleButtonNormal.Size2D = new Size2D(300, 80);
            ToggleButtonNormal.Position2D = new Position2D(156, 350);
            ToggleButtonNormal.Text = "ToggleButton";
            root.Add(ToggleButtonNormal);

            ToggleButtonDisabled = new Button("ToggleButton");
            ToggleButtonDisabled.Size2D = new Size2D(300, 80);
            ToggleButtonDisabled.Position2D = new Position2D(624, 350);
            ToggleButtonDisabled.Text = "ToggleButton";
            root.Add(ToggleButtonDisabled);

            OvalButtonNormal = new Button("OvalButton");
            OvalButtonNormal.Size2D = new Size2D(100, 100);
            OvalButtonNormal.Position2D = new Position2D(156, 500);
            root.Add(OvalButtonNormal);

            OvalButtonDisabled = new Button("OvalButton");
            OvalButtonDisabled.Size2D = new Size2D(100, 100);
            OvalButtonDisabled.Position2D = new Position2D(624, 500);
            OvalButtonDisabled.IsEnabled = false;
            root.Add(OvalButtonDisabled);
        }

        public void Deactivate()
        {
            root.Remove(BasicButtonNormal);
            BasicButtonNormal.Dispose();

            root.Remove(BasicButtonDisabled);
            BasicButtonDisabled.Dispose();

            root.Remove(ServiceButtonNormal);
            ServiceButtonNormal.Dispose();

            root.Remove(ServiceButtonDisabled);
            ServiceButtonDisabled.Dispose();

            root.Remove(ToggleButtonNormal);
            ToggleButtonNormal.Dispose();

            root.Remove(ToggleButtonDisabled);
            ToggleButtonDisabled.Dispose();

            root.Remove(OvalButtonNormal);
            OvalButtonNormal.Dispose();
            root.Remove(OvalButtonDisabled);
            OvalButtonDisabled.Dispose();
    
            root.Dispose();
        }
    }
}
