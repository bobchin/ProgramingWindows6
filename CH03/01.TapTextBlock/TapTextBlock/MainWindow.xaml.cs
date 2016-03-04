using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TapTextBlock
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        byte[] rgb = new byte[3];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtblk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rand.NextBytes(rgb);
            Color clr = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
            txtblk.Foreground = new SolidColorBrush(clr);
        }
    }
}
