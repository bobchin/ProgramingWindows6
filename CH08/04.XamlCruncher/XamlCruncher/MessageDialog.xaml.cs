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

namespace XamlCruncher
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

        object id;

        public MessageDialog(string title, string message, DialogButton[] buttons)
            : this()
        {
            this.Topmost = true;
            this.Title = title;
            this.message.Text = message;
            this.id = null;

            // ボタンのコマンド オブジェクト
            var command = new DelegateCommand((parameter) =>
            {
                // 戻り値を準備する
                id = parameter;
                // ダイアログを閉じる
                this.Close();
            });
            // ボタンを追加します
            foreach (var b in buttons)
            {
                var button = new Button()
                {
                    Content = b.Title,
                    Width = WIDTH,
                    Height = HEIGHT,
                    Margin = new Thickness(MARGIN),
                    Command = command,
                    CommandParameter = b.Id
                };
                if (b.Default)
                    button.IsDefault = true;
                if (b.Cancel)
                    button.IsCancel = true;
                this.body.Children.Add(button);
            }

        }

        // 非同期メソッド
        public Task<Object> ShowAsync()
        {
            var dispatcherOperation = Dispatcher.InvokeAsync(() =>
            {
                var ret = this.ShowDialog();
                // 戻り値を返す
                return this.id;
            }, System.Windows.Threading.DispatcherPriority.Normal);
            dispatcherOperation.Completed += (s, args) =>
            {
                // 戻る値を返すまで、Objectを延命させる
            };
            return dispatcherOperation.Task;
        }


    }

    class DelegateCommand : ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        // Event required by ICommand
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = AlwaysCanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        // Default CanExecute method
        bool AlwaysCanExecute(object param)
        {
            return true;
        }
    }
}
