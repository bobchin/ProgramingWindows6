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

namespace RotationCenterDemo
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
                translateBack1.X =
                translateBack2.X =
                translateBack3.X = -(translate.X = txtblk.ActualWidth / 2);
                translateBack1.Y =
                translateBack2.Y =
                translateBack3.Y = -(translate.Y = txtblk.ActualHeight / 2);
            };
        }
    }
}
