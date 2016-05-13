using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace GettingCharacterInput
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Window.Current.CoreWindow.CharacterReceived += OnCoreWindowCharacterReceived;
        }

        void OnCoreWindowCharacterReceived(CoreWindow sender, CharacterReceivedEventArgs args)
        {
            // Process Backspace key
            if (args.KeyCode == 8 && txtblk.Text.Length > 0)
            {
                txtblk.Text = txtblk.Text.Substring(0, txtblk.Text.Length - 1);
            }
            // All other keys    
            else
            {
                txtblk.Text += (char)args.KeyCode;
            }
        }
    }
}
