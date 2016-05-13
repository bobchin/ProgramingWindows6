using Windows.UI.Xaml.Controls;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace FingerPaint
{
    public sealed partial class AboutBox : SettingsFlyout
    {
        public AboutBox()
        {
            this.InitializeComponent();
        }

        //void OnBackButtonClick(object sender, RoutedEventArgs args)
        //{
        //    // Dismiss Popup
        //    Popup popup = this.Parent as Popup;
            
        //    if (popup != null)
        //        popup.IsOpen = false;
        //}

    }
}
