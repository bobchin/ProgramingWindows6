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

namespace LineCapsAndJoinWithCustomClass
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
            Loaded += (sender, args) =>
            {
                foreach (UIElement child in startLineCapPanel.Children)
                {
                    (child as LineCapRadioButton).IsChecked =
                        (child as LineCapRadioButton).LineCapTag == polyline.StrokeStartLineCap;
                }
                foreach (UIElement child in endLineCapPanel.Children)
                {
                    (child as LineCapRadioButton).IsChecked =
                        (child as LineCapRadioButton).LineCapTag == polyline.StrokeEndLineCap;
                }
                foreach (UIElement child in lineJoinPanel.Children)
                {
                    (child as LineJoinRadioButton).IsChecked =
                        (child as LineJoinRadioButton).LineJoin == polyline.StrokeLineJoin;
                }
            };
        }

        private void OnStartLineCapRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeStartLineCap = (sender as LineCapRadioButton).LineCapTag;
        }

        private void OnEndLineCapRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeEndLineCap = (sender as LineCapRadioButton).LineCapTag;
        }

        private void OnLineJoinRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeLineJoin = (sender as LineJoinRadioButton).LineJoin;
        }
    }
}
