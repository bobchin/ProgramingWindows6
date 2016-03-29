using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HowToAsync2
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

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            DialogButton[] buttons = new DialogButton[]
            {
                new DialogButton("Red", Colors.Red),
                new DialogButton("Green", Colors.Green),
                new DialogButton("Blue", Colors.Blue),
            };
            MessageDialog m = new MessageDialog("Choose a color", "How To Async #2", buttons);

            Task<Object> messageTask = m.ShowAsync();

            TaskAwaiter<object> messageAwaiter = messageTask.GetAwaiter();
            messageAwaiter.OnCompleted(() =>
            {
                object ret = messageAwaiter.GetResult();
                if (ret == null)
                {
                    return;
                }
                clr = (Color)ret;
                contentGrid.Background = new SolidColorBrush(clr);
            });
        }
    }
}
