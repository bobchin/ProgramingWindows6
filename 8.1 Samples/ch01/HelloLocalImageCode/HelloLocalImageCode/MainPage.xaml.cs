﻿using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace HelloLocalImageCode
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            image.Source = new BitmapImage(new Uri("ms-appx:///Images/Greeting.png"));
        }
    }
}
