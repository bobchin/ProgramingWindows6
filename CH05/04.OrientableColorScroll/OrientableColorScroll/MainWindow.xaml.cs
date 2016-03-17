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

namespace OrientableColorScroll
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

        private void OnGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 横向きモード
            if (e.NewSize.Width > e.NewSize.Height)
            {
                secondColDef.Width = new GridLength(1, GridUnitType.Star);
                secondRowDef.Height = new GridLength(0);

                Grid.SetColumn(rectangleResult, 1);
                Grid.SetRow(rectangleResult, 0);
            }
            // 縦向きモード
            else
            {
                secondColDef.Width = new GridLength(0);
                secondRowDef.Height = new GridLength(1, GridUnitType.Star);

                Grid.SetColumn(rectangleResult, 0);
                Grid.SetRow(rectangleResult, 1);
            }
        }

        private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte r = (byte)redSlider.Value;
            byte g = (byte)greenSlider.Value;
            byte b = (byte)blueSlider.Value;

            redValue.Text = r.ToString("X2");
            greenValue.Text = g.ToString("X2");
            blueValue.Text = b.ToString("X2");

            brushResult.Color = Color.FromArgb(255, r, g, b);
        }
    }
}
