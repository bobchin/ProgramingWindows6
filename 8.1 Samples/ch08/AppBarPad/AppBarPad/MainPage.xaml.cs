using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace AppBarPad
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Get local settings object
            ApplicationDataContainer appData = ApplicationData.Current.LocalSettings;

            Loaded += async (sender, args) =>
            {
                // Load TextBox settings
                if (appData.Values.ContainsKey("TextWrapping"))
                    txtbox.TextWrapping = (TextWrapping)appData.Values["TextWrapping"];

                if (appData.Values.ContainsKey("FontSize"))
                    txtbox.FontSize = (double)appData.Values["FontSize"];

                // Load TextBox content
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("AppBarPad.txt",
                                                    CreationCollisionOption.OpenIfExists);
                txtbox.Text = await FileIO.ReadTextAsync(storageFile);

                // Enable the TextBox and give it input focus
                txtbox.IsEnabled = true;
                txtbox.Focus(FocusState.Programmatic);
            };

            Application.Current.Suspending += async (sender, args) =>
            {
                // Save TextBox settings
                appData.Values["TextWrapping"] = (int)txtbox.TextWrapping;
                appData.Values["FontSize"] = txtbox.FontSize;

                // Save TextBox content
                SuspendingDeferral deferral = args.SuspendingOperation.GetDeferral();
                await PathIO.WriteTextAsync("ms-appdata:///local/AppBarPad.txt", txtbox.Text);
                deferral.Complete();
            };
        }

        void OnFontIncreaseAppBarButtonClick(object sender, RoutedEventArgs args)
        {
            ChangeFontSize(1.1);
        }

        void OnFontDecreaseAppBarButtonClick(object sender, RoutedEventArgs args)
        {
            ChangeFontSize(1 / 1.1);
        }

        void ChangeFontSize(double multiplier)
        {
            txtbox.FontSize *= multiplier;
        }

        void OnWrapOptionAppBarButtonClick(object sender, RoutedEventArgs args)
        {
            // Create dialog
            WrapOptionsDialog wrapOptionsDialog = new WrapOptionsDialog
            {
                TextWrapping = txtbox.TextWrapping
            };

            // Bind dialog to TextBox
            Binding binding = new Binding
            {
                Source = wrapOptionsDialog,
                Path = new PropertyPath("TextWrapping"),
                Mode = BindingMode.TwoWay
            };
            txtbox.SetBinding(TextBox.TextWrappingProperty, binding);

            // ダイアログを Flyoutへ変更
            Flyout flyout = new Flyout() 
            { 
                Content = wrapOptionsDialog, 
                Placement = FlyoutPlacementMode.Top
            };
            flyout.ShowAt(sender as FrameworkElement);

        }

        async void OnOpenAppBarButtonClick(object sender, RoutedEventArgs args)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".txt");
            StorageFile storageFile = await picker.PickSingleFileAsync();

            // If user presses Cancel, result is null
            if (storageFile == null)
                return;

            txtbox.Text = await FileIO.ReadTextAsync(storageFile);
        }

        async void OnSaveAsAppBarButtonClick(object sender, RoutedEventArgs args)
        {
            FileSavePicker picker = new FileSavePicker();
            picker.DefaultFileExtension = ".txt";
            picker.FileTypeChoices.Add("Text", new List<string> { ".txt" });
            StorageFile storageFile = await picker.PickSaveFileAsync();

            // If user presses Cancel, result is null
            if (storageFile == null)
                return;

            await FileIO.WriteTextAsync(storageFile, txtbox.Text);
        }

    }
}
