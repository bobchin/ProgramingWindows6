using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace SimpleContextDailog
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SettingMenu();
        }


        private void SettingMenu()
        {
            btn1.Tag = 1.2;
            btn2.Tag = 1 / 1.2;

            Color[] colors = {Colors.Red, Colors.Green, Colors.Blue};
            RadioButton[] radioButtons = { red, green, blue };
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i].IsChecked = (textBlock.Foreground as SolidColorBrush).Color == colors[i];
            }
            
        }
        private void OnTextBlockRighTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var tb = sender as TextBlock;
            if (tb != null)
            {
                Flyout.ShowAttachedFlyout(tb);
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            textBlock.FontSize *= (double)(sender as Button).Tag;
        }

        private void OnRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            textBlock.Foreground = (sender as RadioButton).Foreground;
        }
    }
}
