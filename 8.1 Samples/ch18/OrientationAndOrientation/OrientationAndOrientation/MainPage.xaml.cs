using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace OrientationAndOrientation
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SimpleOrientationSensor simpleOrientationSensor = SimpleOrientationSensor.GetDefault();

        public MainPage()
        {
            this.InitializeComponent();

            // SimpleOrientationSensor initialization
            if (simpleOrientationSensor != null)
            {
                SetOrientationSensorText(simpleOrientationSensor.GetCurrentOrientation());
                simpleOrientationSensor.OrientationChanged += OnSimpleOrientationChanged;
            }

            // DisplayProperties initialization
            //SetDisplayOrientationText(DisplayProperties.CurrentOrientation);
            //DisplayProperties.OrientationChanged += OnDisplayPropertiesOrientationChanged;
            // Windows 8.1 change from DisplayProperties to DisplayInformation
            DisplayInformation displayInformation = DisplayInformation.GetForCurrentView();
            SetDisplayOrientationText(displayInformation.CurrentOrientation);
            displayInformation.OrientationChanged += onDisplayInforamtionOrientationChanged;
        }

        // SimpleOrientationSensor handler
        async void OnSimpleOrientationChanged(SimpleOrientationSensor sender,
                                              SimpleOrientationSensorOrientationChangedEventArgs args)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                SetOrientationSensorText(args.Orientation);
            });
        }

        void SetOrientationSensorText(SimpleOrientation simpleOrientation)
        {
            orientationSensorTextBlock.Text = simpleOrientation.ToString();
        }

        // DisplayProperties handler
        //void OnDisplayPropertiesOrientationChanged(object sender)
        //{
        //    SetDisplayOrientationText(DisplayProperties.CurrentOrientation);
        //}
        // DisplayInformation handler
        void onDisplayInforamtionOrientationChanged(DisplayInformation sender, object args)
        {
            SetDisplayOrientationText(DisplayInformation.GetForCurrentView().CurrentOrientation);
        }
        void SetDisplayOrientationText(DisplayOrientations displayOrientation)
        {
            displayOrientationTextBlock.Text = displayOrientation.ToString();
        }
    }
}
