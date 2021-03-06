﻿using Petzold.ProgrammingWindows6.Chapter11;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace HorizontalListBox
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        void OnItemLoaded(object sender, RoutedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Item Loaded: " +
                ((sender as FrameworkElement).DataContext as NamedColor).Name);
        }

    }
}
