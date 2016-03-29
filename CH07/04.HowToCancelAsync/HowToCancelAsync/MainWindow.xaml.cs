using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace HowToCancelAsync
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource tokenSource;

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

            tokenSource = new CancellationTokenSource();

            MessageDialog m = new MessageDialog("Choose a color", "How To Cancel Async", buttons);

            //５秒にセットされたタイマーを作動
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += OnTimerTick;
            timer.Start();

            Task<Object> asyncOp = m.ShowAsync(tokenSource.Token);
            object result = null;
            try
            {
                result = await asyncOp;
            }
            catch (Exception)
            {
                // この場合の例外はTaskCanceledException
                m.Close();
            }
            timer.Stop();

            if (result == null)
            {
                return;
            }
            Color clr = (Color)result;
            contentGrid.Background = new SolidColorBrush(clr);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}
