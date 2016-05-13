using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace AnalogClock
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        void OnCompositionTargetRendering(object sender, object args)
        {
            DateTime dt = DateTime.Now;
            rotateSecond.Angle = 6 * (dt.Second + dt.Millisecond / 1000.0);
            rotateMinute.Angle = 6 * dt.Minute + rotateSecond.Angle / 60;
            rotateHour.Angle = 30 * (dt.Hour % 12) + rotateMinute.Angle / 12;
        }
    }
}
