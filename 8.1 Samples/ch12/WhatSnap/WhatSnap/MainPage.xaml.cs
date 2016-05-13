using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace WhatSnap
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
            // Windows 8.1では、DisplayPropertiesの使用は推奨されません
            //DisplayProperties.LogicalDpiChanged += OnLogicalDpiChanged;
            // DisplayPropertiesの代わりに、追加されたDisplayInformationを使用します
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

        // Windows 8のDisplayProperties.LogicalDpiChangedイベント
        //void OnLogicalDpiChanged(object sender)
        //{
        //    UpdateDisplay();
        //}

        // Windows 8.1 プレビューでは、DisplayPropertiesクラスの使用は推奨されません
        // DisplayInformation.DpiChangedイベントを使用します
        void OnLogicalDpiChanged(DisplayInformation sender, Object result)
        {
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            // Windows 8.1では、DisplayInformationの使用が推奨されます。
            var displayInformation = DisplayInformation.GetForCurrentView();
            double logicalDpi = displayInformation.LogicalDpi;
            int pixelWidth = (int)Math.Round(logicalDpi * this.ActualWidth / 96);
            int pixelHeight = (int)Math.Round(logicalDpi * this.ActualHeight / 96);
            // Windows 8.1で追加されたApplicationView.GetForCurrentViewメソッドを使用しています
            ApplicationView applicationView = ApplicationView.GetForCurrentView();

            textBlock.Text =
                String.Format("ApplicationViewState(8.0) = {0}\r\n" +
                              "AdjacentToLeftDisplayEdge(8.1) = {1}\r\n" +
                              "AdjacentToRightDisplayEdge(8.1) = {2}\r\n" +
                              "IsFullScreen(8.1) = {3}\r\n" +
                              "Orientation(8.1) = {4}\r\n" +
                              "Window size = {5} x {6}\r\n" +
                              "ResolutionScale = {7}\r\n" +
                              "Logical DPI = {8}\r\n" +
                              "Pixel size = {9} x {10}",
                              ApplicationView.Value,    /* Windows 8.1では推奨されません */
                              applicationView.AdjacentToLeftDisplayEdge,
                              applicationView.AdjacentToRightDisplayEdge,
                              applicationView.IsFullScreen,
                              applicationView.Orientation,
                              this.ActualWidth, this.ActualHeight,
                              displayInformation.ResolutionScale,
                              displayInformation.LogicalDpi,
                              pixelWidth, pixelHeight);
        }
    
    }
}
