using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleContextMenu
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // メニューコマンドの初期設定
            SettingMenuItemCommand();
        }

        private void SettingMenuItemCommand()
        {
            // フォント サイズ変更メニューのコマンド設定
            RelayCommand sizeChangeCommand = new RelayCommand(arg =>
            {
                double size = (double)arg;
                textBlock.FontSize *= size;
            });
            menuLarger.Command = sizeChangeCommand;
            menuLarger.CommandParameter = 1.2;
            menuSmaller.Command = sizeChangeCommand;
            menuSmaller.CommandParameter = 1 / 1.2;

            // 色変更メニューのコマンド設定
            RelayCommand colorChangedCommand = new RelayCommand(arg =>
            {
                Color color = (Color)arg;
                textBlock.Foreground = new SolidColorBrush(color);
            });
            menuRed.Command = colorChangedCommand;
            menuRed.CommandParameter = Colors.Red;
            menuGreen.Command = colorChangedCommand;
            menuGreen.CommandParameter = Colors.Green;
            menuBlue.Command = colorChangedCommand;
            menuBlue.CommandParameter = Colors.Blue;
        }

        private void OnTextBlockMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuFlyout.Placement = PlacementMode.MousePoint;
            menuFlyout.IsOpen = true;
        }
    }

    public class RelayCommand : ICommand
    {
        #region ICommand の実装
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return (_canExecute == null) ? true : _canExecute();
        }
        #endregion

        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        internal RelayCommand(Action<object> execute) : this(execute, null)
        {
        }
        internal RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
