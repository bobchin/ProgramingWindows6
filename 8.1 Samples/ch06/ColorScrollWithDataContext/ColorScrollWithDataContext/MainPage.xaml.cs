using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace ColorScrollWithDataContext
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = new RgbViewModel();

            // Initialize to highlight color
            (this.DataContext as RgbViewModel).Color =
                        new UISettings().UIElementColor(UIElementType.Highlight);

        }
    }
}
