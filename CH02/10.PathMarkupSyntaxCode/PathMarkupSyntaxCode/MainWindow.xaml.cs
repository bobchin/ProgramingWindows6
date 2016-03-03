using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathMarkupSyntaxCode
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Path path = new Path
            {
                Stroke = new SolidColorBrush(Colors.Red),
                StrokeThickness = 12,
                StrokeLineJoin = PenLineJoin.Round,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Data = PathMarkupToGeometry(
                    "M 0 0 L 0 100 M 0 50 L 50 50 M 50 0 L 50 100" +
                    "M 125 0 C 60 -10, 60 60, 125 50, 60 40, 60 110, 125 100" +
                    "M 150 0 L 150 100, 200 100" +
                    "M 225 0 L 225 100, 275 100" +
                    "M 300 50 A 25 50 0 1 0 300 49.9"
                    )
            };

            (this.Content as Grid).Children.Add(path);
        }

        private Geometry PathMarkupToGeometry(string pathMarkup)
        {
            string xaml = "<Path " +
                          "xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>" +
                          "<Path.Data>" + pathMarkup + "</Path.Data></Path>";

            // Load を Parseへ
            Path path = XamlReader.Parse(xaml) as Path;

            // Detach the PathGeometry from the Path
            Geometry geometry = path.Data;
            path.Data = null;
            return geometry;
        }
    }
}
