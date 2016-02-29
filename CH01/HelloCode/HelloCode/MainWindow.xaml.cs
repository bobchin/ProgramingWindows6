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

namespace HelloCode
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /* 一般的な形式
            TextBlock txtblk = new TextBlock();
            txtblk.Text = "Hello, Windows8!";
            txtblk.FontFamily = new FontFamily("Times New Roman");
            txtblk.FontSize = 96;
            txtblk.FontStyle = FontStyles.Italic;
            txtblk.Foreground = new SolidColorBrush(Colors.Yellow);
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            txtblk.VerticalAlignment = VerticalAlignment.Center;
            */

            /* C# 3.0 の初期化式を使用した場合
            TextBlock txtblk = new TextBlock
            {
                Text = "Hello, Windows8!",
                FontFamily = new FontFamily("Times New Roman"),
                FontSize = 96,
                FontStyle = FontStyles.Italic,
                Foreground = new SolidColorBrush(Colors.Yellow),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            */

            // Window でフォント系を指定する場合
            this.FontFamily = new FontFamily("Times New Roman");
            this.FontSize = 96;
            this.FontStyle = FontStyles.Italic;
            this.Foreground = new SolidColorBrush(Colors.Yellow);

            TextBlock txtblk = new TextBlock
            {
                Text = "Hello, Windows8!",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            contentGrid.Children.Add(txtblk);
        }
    }
}
