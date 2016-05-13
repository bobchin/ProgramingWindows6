using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace RotationCenterDemo
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
                translateBack1.X =
                translateBack2.X =
                translateBack3.X = -(translate.X = txtblk.ActualWidth / 2);

                translateBack1.Y =
                translateBack2.Y =
                translateBack3.Y = -(translate.Y = txtblk.ActualHeight / 2);
            };

        }
    }
}
