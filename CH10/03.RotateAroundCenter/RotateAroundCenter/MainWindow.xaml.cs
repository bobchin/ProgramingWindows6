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

namespace RotateAroundCenter
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                rotate.CenterX = txtblk.ActualWidth / 2;
            };

            SizeChanged += (sender, args) =>
            {
                rotate.CenterY = args.NewSize.Height / 2;
            };
        }
    }
}
