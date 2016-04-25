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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AttachedPropertyAnimation
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard storyboard;
        private TimeSpan time;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                (this.Resources["storyboard"] as Storyboard).Begin();
            };
        }

        private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs args)
        {
            /*
            Storyboard storyboard = this.Resources["storyboard"] as Storyboard;

            // Canvas.Left アニメーション
            DoubleAnimation anima = storyboard.Children[0] as DoubleAnimation;
            anima.To = args.NewSize.Width;

            // Canvas.Top アニメーション
            anima = storyboard.Children[1] as DoubleAnimation;
            anima.To = args.NewSize.Height;
            */

            bool isTime = false;
            Storyboard storyboard = this.Resources["storyboard"] as Storyboard;
            try
            {
                time = storyboard.GetCurrentTime();
                isTime = true;
            }
            catch { }

            storyboard.Stop();

            // Canvas.Left アニメーション
            DoubleAnimation anima = storyboard.Children[0] as DoubleAnimation;
            anima.To = args.NewSize.Width;

            // Canvas.Top アニメーション
            anima = storyboard.Children[1] as DoubleAnimation;
            anima.To = args.NewSize.Height;

            storyboard.Begin();
            if (isTime)
            {
                storyboard.Seek(time);
            }
        }
    }
}
