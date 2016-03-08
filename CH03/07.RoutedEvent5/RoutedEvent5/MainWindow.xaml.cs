using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RoutedEvent5
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

        void OnTextBlockMouseDown(object sender, MouseButtonEventArgs args)
        {
            (sender as TextBlock).Foreground = GetRandomBrush();

            // イベントを上位の要素に伝えない
            args.Handled = true;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            contentGrid.Background = GetRandomBrush();
            base.OnMouseDown(e);
        }

        Brush GetRandomBrush()
        {
            rand.NextBytes(rgb);
            Color clr = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
            return new SolidColorBrush(clr);
        }
    }
}
