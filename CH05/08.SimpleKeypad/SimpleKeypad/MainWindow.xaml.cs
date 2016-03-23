using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleKeypad
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private string inputString = "";
        private char[] specialChars = { '*', '#' };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCharButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            inputString += btn.Content as string;
            FormatText();
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            inputString = inputString.Substring(0, inputString.Length - 1);
            FormatText();
        }

        private void FormatText()
        {
            bool hasNonNumbers = (inputString.IndexOfAny(specialChars) != -1);

            if (hasNonNumbers || inputString.Length < 4 || inputString.Length > 10)
            {
                resultText.Text = inputString;
            }
            // 4文字以上8文字未満
            else if (inputString.Length < 8)
            {
                resultText.Text = String.Format("{0}-{1}", inputString.Substring(0, 3), inputString.Substring(3));
            }
            // 8文字以上10文字以下
            else
            {
                resultText.Text = String.Format("({0}) {1}-{2}",
                    inputString.Substring(0, 3), inputString.Substring(3, 3), inputString.Substring(6));
            }

            deleteButton.IsEnabled = (inputString.Length > 0);
        }
    }
}
