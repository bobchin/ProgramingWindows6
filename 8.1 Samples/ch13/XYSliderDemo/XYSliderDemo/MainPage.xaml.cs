using System;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace XYSliderDemo
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool manualChange = false;

        public MainPage()
        {
            this.InitializeComponent();

            // Initialize position of cross-hair in XYSlider
            Loaded += async (sender, args) =>
            {
                Geolocator geolocator = new Geolocator();

                // Might not have permission!
                try
                {
                    Geoposition position = await geolocator.GetGeopositionAsync();

                    if (!manualChange)
                    {
                        // GeocoordinateクラスのLongitude, Latitudeメンバーが8.1で変更
                        BasicGeoposition geoPosition = position.Coordinate.Point.Position;
                        double x = (geoPosition.Longitude + 180) / 360;
                        double y = (90 - geoPosition.Latitude) / 180;
                        xySlider.Value = new Point(x, y);
                    }
                }
                catch
                {
                }
            };
        }

        void OnXYSliderValueChanged(object sender, Point point)
        {
            double longitude = 360 * point.X - 180;
            double latitude = 90 - 180 * point.Y;
            label.Text = String.Format("Longitude: {0:F0} Latitude: {1:F0}",
                                       longitude, latitude);
            manualChange = true;
        }
    }
}
