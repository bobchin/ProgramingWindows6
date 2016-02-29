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

namespace SharedBrush
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // ソースから追加することも可能
            this.Resources.Add("fontFamily2", new FontFamily("Times New Roman"));

            InitializeComponent();

            // XAMLで指定していればこのようにリソースが参照できる
            FontFamily fntfam = this.Resources["fontFamily"] as FontFamily;

            // リソースは共有されるか？
            /*
            TextBlock txtblk = (this.Content as Grid).Children[1] as TextBlock;
            LinearGradientBrush brush = txtblk.Foreground as LinearGradientBrush;
            brush.StartPoint = new Point(0, 1);
            brush.EndPoint = new Point(0, 0);
            */
        }
    }
}
