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

namespace SliderEvents
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

        private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /*
              デフォルトでは Sliderは0～10になっているようだ。
              Slider.Minimun = 0
              Slider.Maximun = 10
             */

            Slider slider = sender as Slider;
            Panel parentPanel = slider.Parent as Panel;
            int childIndex = parentPanel.Children.IndexOf(slider);
            TextBlock txtblk = parentPanel.Children[childIndex + 1] as TextBlock;
            txtblk.Text = e.NewValue.ToString();
        }
    }
}
