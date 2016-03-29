using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace HowToCancelAsync
{
    /// <summary>
    /// MessageDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class MessageDialog : Window
    {
        const double WIDTH = 100;
        const double HEIGHT = 30;
        const double MARGIN = 5;

        public MessageDialog()
        {
            InitializeComponent();
        }

        private object color;
        private DispatcherOperation<Object> dispatcherOperation;
        private CancellationToken token;
        private DispatcherTimer timer = null;

        public MessageDialog(string title, string message, DialogButton[] buttons) : this()
        {
            this.Topmost = true;
            this.Title = title;
            this.message.Text = message;
            this.color = null;

            // ボタンのコマンドオブジェクト
            DelegateCommand command = new DelegateCommand((parameter) =>
            {
                // 戻り値を準備する
                color = parameter;
                // ダイアログを閉じる
                this.Close();
            });

            // ボタンを追加
            foreach (DialogButton b in buttons)
            {
                Button button = new Button
                {
                    Content = b.Title,
                    Width = WIDTH,
                    Height = HEIGHT,
                    Margin = new Thickness(MARGIN),
                    Command = command,
                    CommandParameter = b.Id,
                };

                this.body.Children.Add(button);
            }

            this.Unloaded += MessageDialog_Unloaded;
        }

        private void MessageDialog_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Tick -= timer_Tick;
            }
        }

        // 非同期メソッド
        public Task<Object> ShowAsync()
        {
            dispatcherOperation = Dispatcher.InvokeAsync(() =>
            {
                bool? ret = this.ShowDialog();

                // 戻り値を返す
                return this.color;
            }, DispatcherPriority.Normal);

            dispatcherOperation.Completed += (s, args) =>
            {
                // 戻り値を返すまで、Objectを延命させる
            };

            return dispatcherOperation.Task;
        }

        // キャンセル可能な非同期メソッド
        public Task<Object> ShowAsync(CancellationToken token)
        {
            // 操作のキャンセルを可能にする（１秒間隔で確認する）
            this.token = token;
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(1);
            this.timer.Tick += timer_Tick;
            this.timer.Start();

            return ShowAsync();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            token.ThrowIfCancellationRequested();
        }

        public void Cancel()
        {
            dispatcherOperation.Abort();
            this.Close();
        }
    }


    public class DelegateCommand : ICommand
    {
        #region ICommandの実装
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion


        private Action<object> execute;
        private Func<object, bool> canExecute;

        // コンストラクタ
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = ((param) =>
            {
                return true;
            });
        }

    }
}
