using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace LineCapsAndJoins
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += (sender, args) =>
            {
                foreach (UIElement child in startLineCapPanel.Children)
                    (child as RadioButton).IsChecked =
                        (PenLineCap)(child as RadioButton).Tag == polyline.StrokeStartLineCap;

                foreach (UIElement child in endLineCapPanel.Children)
                    (child as RadioButton).IsChecked =
                        (PenLineCap)(child as RadioButton).Tag == polyline.StrokeEndLineCap;

                foreach (UIElement child in lineJoinPanel.Children)
                    (child as RadioButton).IsChecked =
                        (PenLineJoin)(child as RadioButton).Tag == polyline.StrokeLineJoin;
            };
        }

        void OnStartLineCapRadioButtonChecked(object sender, RoutedEventArgs args)
        {
            polyline.StrokeStartLineCap = (PenLineCap)(sender as RadioButton).Tag;
        }

        void OnEndLineCapRadioButtonChecked(object sender, RoutedEventArgs args)
        {
            polyline.StrokeEndLineCap = (PenLineCap)(sender as RadioButton).Tag;
        }

        void OnLineJoinRadioButtonChecked(object sender, RoutedEventArgs args)
        {
            polyline.StrokeLineJoin = (PenLineJoin)(sender as RadioButton).Tag;
        }
    }
}
