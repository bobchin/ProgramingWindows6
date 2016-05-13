using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace PointerLog
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            timer.Tick += OnTimerTick;
        }

        void OnClearButtonClick(object sender, RoutedEventArgs args)
        {
            logger.Clear();
        }

        void OnCaptureToggleButtonChecked(object sender, RoutedEventArgs args)
        {
            ToggleButton toggle = sender as ToggleButton;
            logger.CaptureOnPress = toggle.IsChecked.Value;
        }

        void OnReleaseCapturesButtonClick(object sender, RoutedEventArgs args)
        {
            timer.Start();
        }

        void OnTimerTick(object sender, object args)
        {
            logger.ReleasePointerCaptures();
            timer.Stop();
        }
    }
}
