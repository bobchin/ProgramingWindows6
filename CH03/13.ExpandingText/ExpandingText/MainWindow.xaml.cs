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

namespace ExpandingText
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        void OnCompositionTargetRendering(object sender, object args)
        {
            RenderingEventArgs renderArgs = args as RenderingEventArgs;

            // 0～1 までの繰り返し
            double t = (0.25 * renderArgs.RenderingTime.TotalSeconds) % 1;
            // 0～1～0 の繰り返し
            double scale = t < 0.5 ? (2 * t) : (2 - 2 * t);
            // 1～144～1 までの繰り返し
            txtblk.FontSize = 1 + scale * 143;
        }
    }
}
