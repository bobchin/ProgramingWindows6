using Windows.UI;               // for Colors
using Windows.UI.Text;          // for FontStyle
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace HelloCode
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            TextBlock txtblk = new TextBlock();
            txtblk.Text = "Hello, Windows 8!";
            txtblk.FontFamily = new FontFamily("Times New Roman");
            txtblk.FontSize = 96;
            txtblk.FontStyle = FontStyle.Italic;
            txtblk.Foreground = new SolidColorBrush(Colors.Yellow);
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            txtblk.VerticalAlignment = VerticalAlignment.Center;

            contentGrid.Children.Add(txtblk);
        }
    }
}
