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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HowToAsync2
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

        object color;

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
            foreach (var b in buttons)
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
        }

        // 非同期メソッド
        public Task<Object> ShowAsync()
        {
            DispatcherOperation<Object> dispatcherOperation = Dispatcher.InvokeAsync(() =>
            {
                var ret = this.ShowDialog();

                // 戻り値を返す
                return this.color;
            }, System.Windows.Threading.DispatcherPriority.Normal);

            dispatcherOperation.Completed += (s, args) =>
            {
                // 戻り値を返すまで、Objectを延命させる
            };

            return dispatcherOperation.Task;
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
