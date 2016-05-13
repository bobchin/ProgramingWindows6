using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace WhatRes
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.SizeChanged += OnMainPageSizeChanged;
            DisplayInformation.GetForCurrentView().DpiChanged += OnLogicalDpiChanged;
            
            Loaded += (sender, args) =>
            {
                UpdateDisplay();
            };
        }

        void OnMainPageSizeChanged(object sender, SizeChangedEventArgs args)
        {
            UpdateDisplay();
        }

        // Windows 8.1 プレビューでは、DisplayPropertiesクラスの使用は推奨されません
        // DisplayInformation.DpiChangedイベントを使用します
        void OnLogicalDpiChanged(DisplayInformation sender, Object result)
        {
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            DisplayInformation displayInformation = DisplayInformation.GetForCurrentView();
            double logicalDpi = displayInformation.LogicalDpi;
            int pixelWidth = (int)Math.Round(logicalDpi * this.ActualWidth / 96);
            int pixelHeight = (int)Math.Round(logicalDpi * this.ActualHeight / 96);

            textBlock.Text =
                String.Format("Window size = {0} x {1}\r\n" +
                              "ResolutionScale = {2}\r\n" +
                              "Logical DPI = {3}\r\n" +
                              "Pixel size = {4} x {5}",
                              this.ActualWidth, this.ActualHeight,
                              displayInformation.ResolutionScale,
                              displayInformation.LogicalDpi,
                              pixelWidth, pixelHeight);
        }
    }
}
