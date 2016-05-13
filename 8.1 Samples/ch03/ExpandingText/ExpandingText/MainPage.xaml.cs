using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace ExpandingText
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
            RenderingEventArgs renderArgs = args as RenderingEventArgs;
            double t = (0.25 * renderArgs.RenderingTime.TotalSeconds) % 1;
            double scale = t < 0.5 ? 2 * t : 2 - 2 * t;
            txtblk.FontSize = 1 + scale * 143;
        }
    }
}
