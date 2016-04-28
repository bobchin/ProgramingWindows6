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

namespace WheresMyElement
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool storyboardPaused = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (storyboardPaused)
            {
                storyBoard.Resume(this);
                storyboardPaused = false;
                return;
            }

            GeneralTransform xform = txtblk.TransformToVisual(contentGrid);

            // 要素の周りに青いポリゴンを描画
            polygon.Points.Clear();
            polygon.Points.Add(xform.Transform(new Point(0, 0)));
            polygon.Points.Add(xform.Transform(new Point(txtblk.ActualWidth, 0)));
            polygon.Points.Add(xform.Transform(new Point(txtblk.ActualWidth, txtblk.ActualHeight)));
            polygon.Points.Add(xform.Transform(new Point(0, txtblk.ActualHeight)));

            // 赤い外接矩形を描画
            path.Data = new RectangleGeometry
            {
                Rect = xform.TransformBounds(new Rect(new Point(0, 0), txtblk.DesiredSize)),
            };

            storyBoard.Pause();
            storyboardPaused = true;

            base.OnMouseDown(e);
        }
    }
}
