using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace SimplePageNavigation
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

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            forwardButton.IsEnabled = this.Frame.CanGoForward;
            backButton.IsEnabled = this.Frame.CanGoBack;
        }

        void OnGotoButtonClick(object sender, RoutedEventArgs args)
        {
            this.Frame.Navigate(typeof(SecondPage));
        }

        void OnForwardButtonClick(object sender, RoutedEventArgs args)
        {
            this.Frame.GoForward();
        }

        void OnBackButtonClick(object sender, RoutedEventArgs args)
        {
            this.Frame.GoBack();
        }

    }
}
