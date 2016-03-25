using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ColorScrollWithDataContext
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainLogic();
        }

        private void MainLogic()
        {
            // XAMLでも指定可能
            this.DataContext = new RgbViewModel();

            // ハイライト色に初期化
            (this.DataContext as RgbViewModel).Color = SystemColors.HighlightColor;
        }
    }

    public class DoubleToStringHexByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)(double)value).ToString("X2");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
