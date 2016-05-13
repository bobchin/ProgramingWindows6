using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace SliderSketch
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

        void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Border border = sender as Border;
            xSlider.Maximum = args.NewSize.Width - border.Padding.Left
                                                 - border.Padding.Right
                                                 - polyline.StrokeThickness;

            ySlider.Maximum = args.NewSize.Height - border.Padding.Top
                                                  - border.Padding.Bottom
                                                  - polyline.StrokeThickness;
        }

        void OnSliderValueChanged(object sender, RangeBaseValueChangedEventArgs args)
        {
            polyline.Points.Add(new Point(xSlider.Value, ySlider.Value));
        }

    }
}
