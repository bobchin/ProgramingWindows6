using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace SimpleKeypad
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string inputString = "";
        char[] specialChars = { '*', '#' };

        public MainPage()
        {
            this.InitializeComponent();
        }

        void OnCharButtonClick(object sender, RoutedEventArgs args)
        {
            Button btn = sender as Button;
            inputString += btn.Content as string;
            FormatText();
        }

        void OnDeleteButtonClick(object sender, RoutedEventArgs args)
        {
            inputString = inputString.Substring(0, inputString.Length - 1);
            FormatText();
        }

        void FormatText()
        {
            bool hasNonNumbers = inputString.IndexOfAny(specialChars) != -1;

            if (hasNonNumbers || inputString.Length < 4 || inputString.Length > 10)
                resultText.Text = inputString;

            else if (inputString.Length < 8)
                resultText.Text = String.Format("{0}-{1}", inputString.Substring(0, 3),
                                                           inputString.Substring(3));
            else
                resultText.Text = String.Format("({0}) {1}-{2}", inputString.Substring(0, 3),
                                                                 inputString.Substring(3, 3),
                                                                 inputString.Substring(6));
            deleteButton.IsEnabled = inputString.Length > 0;
        }

    }
}
