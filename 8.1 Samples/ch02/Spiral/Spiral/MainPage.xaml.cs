﻿using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Spiral
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            for (int angle = 0; angle < 3600; angle++)
            {
                double radians = Math.PI * angle / 180;
                double radius = angle / 10;
                double x = 360 + radius * Math.Sin(radians);
                double y = 360 + radius * Math.Cos(radians);
                polyline.Points.Add(new Point(x, y));
            }

        }
    }
}
