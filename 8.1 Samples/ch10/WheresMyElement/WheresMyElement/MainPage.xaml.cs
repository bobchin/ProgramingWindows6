using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace WheresMyElement
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool storyboardPaused;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnTapped(TappedRoutedEventArgs args)
        {
            if (storyboardPaused)
            {
                storyboard.Resume();
                storyboardPaused = false;
                return;
            }

            GeneralTransform xform = txtblk.TransformToVisual(contentGrid);

            // Draw blue polygon around element
            polygon.Points.Clear();
            polygon.Points.Add(xform.TransformPoint(new Point(0, 0)));
            polygon.Points.Add(xform.TransformPoint(new Point(txtblk.ActualWidth, 0)));
            polygon.Points.Add(xform.TransformPoint(new Point(txtblk.ActualWidth, txtblk.ActualHeight)));
            polygon.Points.Add(xform.TransformPoint(new Point(0, txtblk.ActualHeight)));

            // Draw red bounding box
            path.Data = new RectangleGeometry
            {
                Rect = xform.TransformBounds(new Rect(new Point(0, 0), txtblk.DesiredSize))
            };

            storyboard.Pause();
            storyboardPaused = true;
            base.OnTapped(args);
        }

    }
}
