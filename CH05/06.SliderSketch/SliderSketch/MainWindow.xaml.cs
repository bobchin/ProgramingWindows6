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

namespace SliderSketch
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnBorderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border border = sender as Border;
            xSlider.Maximum = e.NewSize.Width - border.Padding.Left
                                              - border.Padding.Right
                                              - polyline.StrokeThickness;

            ySlider.Maximum = e.NewSize.Height - border.Padding.Top
                                               - border.Padding.Bottom
                                               - polyline.StrokeThickness;
        }

        private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            polyline.Points.Add(new Point(xSlider.Value, ySlider.Value));
        }
    }
}
