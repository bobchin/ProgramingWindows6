using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace ScalableInternationalHelloWorld
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DisplayInformation displayInformation = DisplayInformation.GetForCurrentView();

        public MainPage()
        {
            this.InitializeComponent();

            SetFont();
            // 8.1では、DisplayProperties を DisplayInformationに変更
            displayInformation.OrientationChanged += OnDisplayInformationOrientationChanged;
        }


        void OnDisplayInformationOrientationChanged(DisplayInformation sender, object args)
        {
            SetFont();
        }

        void SetFont()
        {
            bool isLandscape =
                displayInformation.CurrentOrientation == DisplayOrientations.Landscape ||
                displayInformation.CurrentOrientation == DisplayOrientations.LandscapeFlipped;
            this.FontSize = isLandscape ? 40 : 24;
        }
    }
}
