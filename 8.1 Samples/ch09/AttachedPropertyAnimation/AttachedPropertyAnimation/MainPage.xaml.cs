using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace AttachedPropertyAnimation
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += (sender, args) =>
            {
                (this.Resources["storyboard"] as Storyboard).Begin();
            };
        }

        void OnCanvasSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Storyboard storyboard = this.Resources["storyboard"] as Storyboard;

            // Canvas.Left animation
            DoubleAnimation anima = storyboard.Children[0] as DoubleAnimation;
            anima.To = args.NewSize.Width;

            // Canvas.Top animation
            anima = storyboard.Children[1] as DoubleAnimation;
            anima.To = args.NewSize.Height;
        }
    }
}
