using System;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace FingerPaint
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        AppSettings appSettings = new AppSettings();
        double imageScale = 1;
        Point imageOffset = new Point();

        public MainPage()
        {
            this.InitializeComponent();

            SizeChanged += OnMainPageSizeChanged;
            Loaded += OnMainPageLoaded;
            Application.Current.Suspending += OnApplicationSuspending;
        }

        void OnMainPageSizeChanged(object sender, SizeChangedEventArgs args)
        {
            // VisualStateをDetermineVisualStateで決定します
            VisualStateManager.GoToState(this, DetermineVisualState(args.NewSize), true);

            if (bitmap != null)
            {
                CalculateImageScaleAndOffset();
            }
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


        void CalculateImageScaleAndOffset()
        {
            imageScale = Math.Min(this.ActualWidth / bitmap.PixelWidth,
                                  this.ActualHeight / bitmap.PixelHeight);
            imageOffset = new Point((this.ActualWidth - imageScale * bitmap.PixelWidth) / 2,
                                    (this.ActualHeight - imageScale * bitmap.PixelHeight) / 2);
        }

        async void OnMainPageLoaded(object sender, RoutedEventArgs args)
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.GetFileAsync("FingerPaint.png");
                await LoadBitmapFromFile(storageFile);
            }
            catch
            {
                // Ignore any errors
            }

            if (bitmap == null)
                await CreateNewBitmapAndPixelArray();
        }

        async void OnApplicationSuspending(object sender, SuspendingEventArgs args)
        {
            // Save application settings
            appSettings.Save();

            // Save current bitmap
            SuspendingDeferral deferral = args.SuspendingOperation.GetDeferral();

            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("FingerPaint.png",
                                                        CreationCollisionOption.ReplaceExisting);
                await SaveBitmapToFile(storageFile);
            }
            catch
            {
                // Ignore any errors
            }

            deferral.Complete();
        }

        void OnColorAppBarButtonClick(object sender, RoutedEventArgs args)
        {
            DisplayDialog(sender, new ColorSettingDialog());
        }

        void OnThicknessAppBarButtonClick(object sender, RoutedEventArgs args)
        {
            DisplayDialog(sender, new ThicknessSettingDialog());
        }

        void DisplayDialog(object sender, FrameworkElement dialog)
        {
            dialog.DataContext = appSettings;

            // DailogをPopupクラスより、Flyoutクラスへ書き換え
            Flyout flyout = new Flyout()
            {
                Content = dialog,
                Placement = FlyoutPlacementMode.Bottom
            };

            flyout.Closed += (dislogSender, dislogArgs) =>
                {
                    this.BottomAppBar.IsOpen = false;
                };
            flyout.ShowAt((FrameworkElement)sender);
        }

    }
}
