using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace SliderEvents
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        void OnSliderValueChanged(object sender, RangeBaseValueChangedEventArgs args)
        {
            Slider slider = sender as Slider;
            Panel parentPanel = slider.Parent as Panel;
            int childIndex = parentPanel.Children.IndexOf(slider);
            TextBlock txtblk = parentPanel.Children[childIndex + 1] as TextBlock;
            txtblk.Text = args.NewValue.ToString();
        }

    }
}
