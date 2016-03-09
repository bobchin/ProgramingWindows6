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

namespace RainbowEight
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // FontSizeが1ピクセルなど
        double txtblkBaseSize;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs args)
        {
            // ActualHeightが異なるため
            // txtblkBaseSize = txtblk.ActualHeight;
            txtblkBaseSize = txtblk.DesiredSize.Height;
            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        private void OnCompositionTargetRendering(object sender, EventArgs args)
        {
            // FontSizeをできるだけ大きくする
            txtblk.FontSize = this.ActualHeight / txtblkBaseSize;

            // tの値を0～1にする計算を繰り返す
            RenderingEventArgs renderingArgs = args as RenderingEventArgs;
            double t = (0.25 * renderingArgs.RenderingTime.TotalSeconds) % 1;

            // GradientStopオブジェクトをループ処理する
            for (int index = 0; index < gradientBrush.GradientStops.Count; index++)
            {
                gradientBrush.GradientStops[index].Offset = index / 7.0 - t;
            }
        }
    }
}
