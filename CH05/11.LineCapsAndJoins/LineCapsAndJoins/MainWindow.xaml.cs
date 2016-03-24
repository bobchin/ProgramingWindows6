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

namespace LineCapsAndJoins
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
                    (child as RadioButton).IsChecked =
                        (PenLineCap)(child as RadioButton).Tag == polyline.StrokeStartLineCap;
                }
                foreach (UIElement child in endLineCapPanel.Children)
                {
                    (child as RadioButton).IsChecked =
                        (PenLineCap)(child as RadioButton).Tag == polyline.StrokeEndLineCap;
                }
                foreach (UIElement child in lineJoinPanel.Children)
                {
                    (child as RadioButton).IsChecked =
                        (PenLineJoin)(child as RadioButton).Tag == polyline.StrokeLineJoin;
                }
            };
        }

        private void OnStartLineCapRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeStartLineCap = (PenLineCap)(sender as RadioButton).Tag;
        }

        private void OnEndLineCapRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeEndLineCap = (PenLineCap)(sender as RadioButton).Tag;
        }

        private void OnLineJoinRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeLineJoin = (PenLineJoin)(sender as RadioButton).Tag;
        }
    }
}
