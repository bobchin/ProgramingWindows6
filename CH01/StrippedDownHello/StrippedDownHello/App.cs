using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StrippedDownHello
{
    public partial class App : Application
    {
        [STAThreadAttribute()]
        public static void Main(string[] args)
        {
            var app = new App();

            TextBlock txtblk = new TextBlock
            {
                Text = "Stripped-Down Windows 8",
                FontFamily = new FontFamily("Lucida sans Typewriter"),
                FontSize = 96,
                Foreground = new SolidColorBrush(Colors.Red),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var window = new Window
            {
                Title = "StrippedDownHello",
                WindowState = WindowState.Maximized,
                Content = txtblk
            };

            app.Run(window);
        }
    }
}
