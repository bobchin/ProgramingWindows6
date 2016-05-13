using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace RotateAroundCenter
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
                rotate.CenterX = txtblk.ActualWidth / 2;
            };

            SizeChanged += (sender, args) =>
            {
                rotate.CenterY = args.NewSize.Height / 2;
            };

        }
    }
}
