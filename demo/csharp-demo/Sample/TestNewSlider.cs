﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace Tizen.NUI.Examples
{
    public class TestNewSlider : NUIApplication
    {
        TextLabel board;
        NewButton btn1;
        NewButton btn2;
        VDButton btn3;
        VDButton btn4;
        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = Window.Instance;

            board = new TextLabel();
            board.Size2D = new Size2D(1000, 100);
            board.Position2D = new Position2D(460, 800);
            board.PointSize = 30;
            board.HorizontalAlignment = HorizontalAlignment.Center;
            board.VerticalAlignment = VerticalAlignment.Center;
            board.BackgroundColor = Color.Magenta;
            window.Add(board);


            ExSlider slider = new ExSlider();
            slider.Position2D = new Position2D(300, 100);
            slider.Size2D = new Size2D(1370, 95);

            slider.MinValue = 0;
            slider.MaxValue = 100;
            slider.Value = 30;

            slider.PromptTextSize = new Size2D(60, 60);
            slider.MinValueTextSize = new Size2D(80, 50);
            slider.MaxValueTextSize = new Size2D(80, 50);
            window.Add(slider);


            btn1 = new NewButton("Text");
            btn1.Name = "SRTextButton";
            btn1.Position2D = new Position2D(500, 300);
            btn1.Size2D = new Size2D(300, 100);
            btn1.Text = "SRTextButton";
            btn1.Focusable = true;
            btn1.ClickEvent += Btn1_ClickEvent;
            window.Add(btn1);

            btn2 = new NewButton("Icon");
            btn2.Position2D = new Position2D(1000, 300);
            btn2.Size2D = new Size2D(100, 100);
            btn2.BackgroundColor = Color.Green;
            btn2.IconURL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/" + "star-highlight.png";
            btn2.IconSize2D = new Size2D(40, 40);
            btn2.Focusable = true;
            window.Add(btn2);

            btn3 = new VDButton("C_ButtonBasic_WhiteText");
            btn3.Name = "VDTextButton";
            btn3.Position2D = new Position2D(500, 500);
            btn3.Size2D = new Size2D(300, 100);
            btn3.Text = "VDTextButton";
            btn3.Focusable = true;
            btn3.ClickEvent += Btn1_ClickEvent;
            window.Add(btn3);

            btn4 = new VDButton("C_ButtonBasic_WhiteIcon");
            btn4.Position2D = new Position2D(1000, 500);
            btn4.Size2D = new Size2D(100, 100);
            btn4.IconURL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/" + "star-highlight.png";
            btn4.IconSize2D = new Size2D(40, 40);
            btn4.Focusable = true;
            window.Add(btn4);

            View testView1 = new View();
            testView1.Position2D = new Position2D(200, 400);
            testView1.Size2D = new Size2D(100, 100);
            testView1.BackgroundColor = Color.Green;
            window.Add(testView1);

            View testView2 = new View();
            testView2.Position2D = new Position2D(0, 0);
            testView2.Size2D = new Size2D(50, 50);
            testView2.BackgroundColor = Color.Red;
            testView2.PositionUsesPivotPoint = true;
            testView2.ParentOrigin = ParentOrigin.Center;
            testView2.PivotPoint = PivotPoint.Center;
            testView1.Add(testView2);

            FocusManager.Instance.SetCurrentFocusView(btn1);

            btn1.RightFocusableView = btn2;
            btn2.LeftFocusableView = btn1;
            btn2.DownFocusableView = btn4;
            btn1.DownFocusableView = btn3;
            btn3.UpFocusableView = btn1;
            btn3.RightFocusableView = btn4;
            btn4.LeftFocusableView = btn3;
            btn4.UpFocusableView = btn2;


        }

        private void Btn1_ClickEvent(object sender, NewButton.ClickEventArgs e)
        {
            View view = sender as View;
            board.Text = view.Name + " Clicked";

        }
    }
}
