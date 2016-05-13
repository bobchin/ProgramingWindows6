using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace SimpleContextMenu
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SettingMenuItemCommand();   // メニューコマンドの初期設定
        }

        private void SettingMenuItemCommand()
        {
            // フォント サイズ変更メニューのコマンド設定
            var sizeChangeCommand = new RelayCommand(arg =>
            {
                var size = (double)arg;
                textBlock.FontSize *= size;
            });
            var menuItem = GetMenuItem("menuLarger");
            menuItem.Command = sizeChangeCommand;
            menuItem.CommandParameter = 1.2;
            menuItem = GetMenuItem("menuSmaller");
            menuItem.Command = sizeChangeCommand;
            menuItem.CommandParameter = 1 / 1.2;
            // 色変更メニューのコマンド設定
            var colorChangedCommand = new RelayCommand(arg =>
            {
                var color = (Color)arg;
                textBlock.Foreground = new SolidColorBrush(color);
            });
            menuItem = GetMenuItem("menuRed");
            menuItem.Command = colorChangedCommand;
            menuItem.CommandParameter = Colors.Red;
            menuItem = GetMenuItem("menuGreen");
            menuItem.Command = colorChangedCommand;
            menuItem.CommandParameter = Colors.Green;
            menuItem = GetMenuItem("menuBlue");
            menuItem.Command = colorChangedCommand;
            menuItem.CommandParameter = Colors.Blue;
        }

        private void textBlock_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            // メニュの表示
            TextBlock tb = sender as TextBlock;
            if (tb != null)
            {
                Flyout.ShowAttachedFlyout(tb);
            }
        }

        private MenuFlyoutItem GetMenuItem(string name)
        {
            // 8.1 プレビューは、MenuFlyoutItemをダイレクトに参照できないためです
            return (MenuFlyoutItem)menuFlyout.Items.Where(x => x.Name == name).FirstOrDefault();
        }

    }

    #region コマンドの定義
    class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        internal RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        internal RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
    #endregion
}
