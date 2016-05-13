using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace TapToFlip
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Storyboard flipStoryboard, flipBackStoryboard;
        bool flipped = false;

        public MainPage()
        {
            this.InitializeComponent();

            flipStoryboard = this.Resources["flipStoryboard"] as Storyboard;
            flipBackStoryboard = this.Resources["flipBackStoryboard"] as Storyboard;
        }

        void OnGridTapped(object sender, TappedRoutedEventArgs args)
        {
            if (flipStoryboard.GetCurrentState() == ClockState.Active ||
                flipBackStoryboard.GetCurrentState() == ClockState.Active)
            {
                return;
            }

            Storyboard storyboard = flipped ? flipBackStoryboard : flipStoryboard;
            storyboard.Begin();
            flipped ^= true;
        }
    }
}
