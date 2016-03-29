using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HowToAsync3
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private Color clr;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            DialogButton[] buttons = new DialogButton[]
            {
                new DialogButton("Red", Colors.Red),
                new DialogButton("Green", Colors.Green),
                new DialogButton("Blue", Colors.Blue),
            };
            MessageDialog m = new MessageDialog("Choose a color", "How To Async #3", buttons);

            object result = await m.ShowAsync();
            if (result == null)
            {
                return;
            }
            clr = (Color)result;
            contentGrid.Background = new SolidColorBrush(clr);
        }

    }
}
