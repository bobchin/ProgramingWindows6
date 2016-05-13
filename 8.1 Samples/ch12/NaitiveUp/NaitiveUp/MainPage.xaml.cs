using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace NaitiveUp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DisplayInformation.GetForCurrentView().OrientationChanged += OnOrientationChanged;
        }

        void OnOrientationChanged(DisplayInformation sender, object args)
        {
            SetRotation(sender);
        }

        void SetRotation(DisplayInformation sender)
        {
            rotate.Angle = 90 * (Log2(sender.CurrentOrientation) -
                                 Log2(sender.NativeOrientation));
            //rotate.Angle = 90 * (Log2(DisplayProperties.CurrentOrientation) -
            //                     Log2(DisplayProperties.NativeOrientation));
        }

        int Log2(DisplayOrientations orientation)
        {
            int value = (int)orientation;
            int log = 0;

            while (value > 0 && (value & 1) == 0)
            {
                value >>= 1;
                log += 1;
            }
            return log;
        }
    }
}
