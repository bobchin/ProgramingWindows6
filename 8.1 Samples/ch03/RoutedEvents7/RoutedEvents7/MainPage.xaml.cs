using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace RoutedEvents7
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Random rand = new Random();
        byte[] rgb = new byte[3];

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnTapped(TappedRoutedEventArgs args)
        {
            rand.NextBytes(rgb);
            Color clr = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
            SolidColorBrush brush = new SolidColorBrush(clr);

            if (args.OriginalSource is TextBlock)
                (args.OriginalSource as TextBlock).Foreground = brush;

            else if (args.OriginalSource is Grid)
                (args.OriginalSource as Grid).Background = brush;

            base.OnTapped(args);
        }

    }
}
