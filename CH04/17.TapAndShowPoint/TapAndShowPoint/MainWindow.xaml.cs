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

namespace TapAndShowPoint
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

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            MainLogic(e);

            base.OnMouseDown(e);
        }

        private void MainLogic(MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(this);

            // ドットを作成
            Ellipse ellipse = new Ellipse
            {
                Width = 3,
                Height = 3,
                Fill = this.Foreground
            };

            Canvas.SetLeft(ellipse, pt.X);
            Canvas.SetTop(ellipse, pt.Y);
            canvas.Children.Add(ellipse);

            // テキストを作成
            TextBlock txtblk = new TextBlock
            {
                Text = String.Format("{0}", pt),
                FontSize = 24
            };

            Canvas.SetLeft(txtblk, pt.X);
            Canvas.SetTop(txtblk, pt.Y);
            canvas.Children.Add(txtblk);

            e.Handled = true;
        }
    }
}
