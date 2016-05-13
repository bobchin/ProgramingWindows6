using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace RadialGradientBrushDemo
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

        void OnCanvasSizeChanged(object sender, SizeChangedEventArgs args)
        {
            // Canvas.Left animation
            leftAnima.To = args.NewSize.Width;

            // Canvas.Top animation
            rightAnima.To = args.NewSize.Height;
        }

    }
}
