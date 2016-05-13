using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace DisplayHighSchoolStudents
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class StudentPage : Page
    {
        public StudentPage()
        {
            this.InitializeComponent();
            SizeChanged += OnPageSizeChanged;
        }

        void OnPageSizeChanged(object sender, SizeChangedEventArgs args)
        {
            // VisualStateをDetermineVisualStateで決定します
            VisualStateManager.GoToState(this, DetermineVisualState(args.NewSize), true);
            //VisualStateManager.GoToState(this, ApplicationView.Value.ToString(), true);
        }

        string DetermineVisualState(Size newSize)
        {
            if (newSize.Width <= 320)    // 320ピクセル以下は Snapped
                return "Snapped";
            var applicationView = ApplicationView.GetForCurrentView();
            if (applicationView.IsFullScreen)   // 全画面
            {
                if (applicationView.Orientation == ApplicationViewOrientation.Portrait)
                    return "FullScreenPortrait";
                else
                    return "FullScreenLandscape";
            }
            // 縦>横はFullScreenPortraitで、縦<横はFilled
            if (newSize.Width < newSize.Height)
                return "FullScreenPortrait";

            return "Filled";
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            this.DataContext = args.Parameter;
            base.OnNavigatedTo(args);
        }

        void OnBackButtonClick(object sender, RoutedEventArgs args)
        {
            this.Frame.GoBack();
        }

    }
}
