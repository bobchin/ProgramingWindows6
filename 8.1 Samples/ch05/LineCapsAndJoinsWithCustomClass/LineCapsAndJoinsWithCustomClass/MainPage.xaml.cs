using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace LineCapsAndJoinsWithCustomClass
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
                    (child as LineCapRadioButton).IsChecked =
                        (child as LineCapRadioButton).LineCapTag == polyline.StrokeStartLineCap;

                foreach (UIElement child in endLineCapPanel.Children)
                    (child as LineCapRadioButton).IsChecked =
                        (child as LineCapRadioButton).LineCapTag == polyline.StrokeEndLineCap;

                foreach (UIElement child in lineJoinPanel.Children)
                    (child as LineJoinRadioButton).IsChecked =
                        (child as LineJoinRadioButton).LineJoinTag == polyline.StrokeLineJoin;
            };
        }

        void OnStartLineCapRadioButtonChecked(object sender, RoutedEventArgs args)
        {
            polyline.StrokeStartLineCap = (sender as LineCapRadioButton).LineCapTag;
        }

        void OnEndLineCapRadioButtonChecked(object sender, RoutedEventArgs args)
        {
            polyline.StrokeEndLineCap = (sender as LineCapRadioButton).LineCapTag;
        }

        void OnLineJoinRadioButtonChecked(object sender, RoutedEventArgs args)
        {
            polyline.StrokeLineJoin = (sender as LineJoinRadioButton).LineJoinTag;
        }
    }
}
