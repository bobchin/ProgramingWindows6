using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace ManipulationManagerDemo
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ManipulationManager manipulationManager = new ManipulationManager();

        public MainPage()
        {
            this.InitializeComponent();

            image.ManipulationMode = ManipulationModes.All &
                                    ~ManipulationModes.TranslateRailsX &
                                    ~ManipulationModes.TranslateRailsY;
        }

        protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs args)
        {
            manipulationManager.AccumulateDelta(args.Position, args.Delta);
            matrixXform.Matrix = manipulationManager.Matrix;
            base.OnManipulationDelta(args);
        }
    }
}
