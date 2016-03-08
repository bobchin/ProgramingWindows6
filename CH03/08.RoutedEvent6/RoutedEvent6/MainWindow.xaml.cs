using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RoutedEvent6
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

            AddHandler(
                UIElement.MouseDownEvent,
                new MouseButtonEventHandler(OnWindowMouseDown),
                true);
        }

        void OnTextBlockMouseDown(object sender, MouseButtonEventArgs args)
        {
            (sender as TextBlock).Foreground = GetRandomBrush();

            // イベントを上位の要素に伝えない
            args.Handled = true;
        }

        void OnWindowMouseDown(object sender, MouseButtonEventArgs args)
        {
            contentGrid.Background = GetRandomBrush();
        }

        Brush GetRandomBrush()
        {
            rand.NextBytes(rgb);
            Color clr = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
            return new SolidColorBrush(clr);
        }
    }
}
