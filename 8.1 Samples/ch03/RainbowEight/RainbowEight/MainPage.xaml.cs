using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace RainbowEight
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double txtblkBaseSize;  // ie, for 1-pixel FontSize

        public MainPage()
        {
            this.InitializeComponent();

            Loaded += OnPageLoaded;
        }

        void OnPageLoaded(object sender, RoutedEventArgs args)
        {
            txtblkBaseSize = txtblk.ActualHeight;
            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        void OnCompositionTargetRendering(object sender, object args)
        {
            // Set FontSize as large as it can be
            txtblk.FontSize = this.ActualHeight / txtblkBaseSize;

            // Calculate t from 0 to 1 repetitively
            RenderingEventArgs renderingArgs = args as RenderingEventArgs;
            double t = (0.25 * renderingArgs.RenderingTime.TotalSeconds) % 1;

            // Loop through GradientStop objects
            for (int index = 0; index < gradientBrush.GradientStops.Count; index++)
                gradientBrush.GradientStops[index].Offset = index / 7.0 - t;
        }
    }
}
