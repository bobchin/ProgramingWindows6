using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace CustomButtonTemplate
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

        void OnButton1Click(object sender, RoutedEventArgs args)
        {
            centerButton.IsEnabled = false;
        }

        void OnButton3Click(object sender, RoutedEventArgs args)
        {
            centerButton.IsEnabled = true;
        }

    }
}
