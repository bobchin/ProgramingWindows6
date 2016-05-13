﻿using System;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace EasyCameraCapture
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

        async void OnButtonClick(object sender, RoutedEventArgs args)
        {
            CameraCaptureUI cameraCap = new CameraCaptureUI();

            cameraCap.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.VerySmallQvga;

            StorageFile storageFile = await cameraCap.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (storageFile != null)
            {
                IRandomAccessStreamWithContentType stream = await storageFile.OpenReadAsync();
                BitmapImage bitmap = new BitmapImage();
                await bitmap.SetSourceAsync(stream);
                image.Source = bitmap;
            }
        }

    }
}
