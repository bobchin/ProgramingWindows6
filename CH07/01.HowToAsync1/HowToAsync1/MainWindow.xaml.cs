using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HowToAsync1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Color clr;

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
            MessageDialog m = new MessageDialog("Choose a color", "How To Async #1", buttons);

            Task<Object> messageTask = m.ShowAsync();
            messageTask.ContinueWith((task) =>
            {
                if (task.Result == null)
                {
                    return;
                }
                clr = (Color)task.Result;
                Dispatcher.BeginInvoke(new Action(() => { OnDispatcherRunAsyncCallback(); }), null);
            });
        }

        private void OnDispatcherRunAsyncCallback()
        {
            // 背景をセット
            contentGrid.Background = new SolidColorBrush(clr);
        }
    }
}
