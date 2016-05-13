using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Windows.Input;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace FingerPaint3
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Dictionary<uint, Polyline> pointerDictionary = new Dictionary<uint, Polyline>();
        Random rand = new Random();
        byte[] rgb = new byte[3];

        public MainPage()
        {
            this.InitializeComponent();
            this.IsTabStop = true;
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs args)
        {
            // イベント引数から情報を取得
            uint id = args.Pointer.PointerId;
            Point point = args.GetCurrentPoint(this).Position;

            // ランダムな色を作成
            rand.NextBytes(rgb);
            Color color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);

            // Polyline を作成
            Polyline polyline = new Polyline
            {
                Stroke = new SolidColorBrush(color),
                StrokeThickness = 24
            };
            polyline.PointerPressed += OnPolylinePointerPressed;
            polyline.RightTapped += OnPolylineRightTapped;
            polyline.Points.Add(point);

            // Gridに追加
            contentGrid.Children.Add(polyline);

            // ディクショナリに追加
            pointerDictionary.Add(id, polyline);

            // ポインターをキャプチャ
            CapturePointer(args.Pointer);

            // 入力フォーカスを設定
            Focus(FocusState.Programmatic);

            base.OnPointerPressed(args);
        }

        void OnPolylinePointerPressed(object sender, PointerRoutedEventArgs args)
        {
            args.Handled = true;
        }

        void OnPolylineRightTapped(object sender, RightTappedRoutedEventArgs args)
        {
            Polyline polyline = sender as Polyline;
            MenuFlyout popupMenu = CreateMenuFlyout(polyline);  // コードで作成
            //MenuFlyout popupMenu = GetMenuFlyout(polyline);   // リソースで定義

            // FlyoutBase.ShowAttachedFlyoutメソッドか、ShowAtメソッドを使用する
            //FlyoutBase.SetAttachedFlyout(polyline, popupMenu);
            //Flyout.ShowAttachedFlyout(polyline);
            popupMenu.ShowAt(polyline);

        }

        protected override void OnPointerMoved(PointerRoutedEventArgs args)
        {
            // イベント引数から情報を取得
            uint id = args.Pointer.PointerId;
            Point point = args.GetCurrentPoint(this).Position;

            // IDがディクショナリに存在する場合は、Polylineに座標を追加
            if (pointerDictionary.ContainsKey(id))
                pointerDictionary[id].Points.Add(point);

            base.OnPointerMoved(args);
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs args)
        {
            // イベント引数から情報を取得
            uint id = args.Pointer.PointerId;

            // IDがディクショナリに存在する場合は、それを削除
            if (pointerDictionary.ContainsKey(id))
                pointerDictionary.Remove(id);

            base.OnPointerReleased(args);
        }

        protected override void OnPointerCaptureLost(PointerRoutedEventArgs args)
        {
            // イベント引数から情報を取得
            uint id = args.Pointer.PointerId;

            // IDがディクショナリに存在する場合は描画を中止
            if (pointerDictionary.ContainsKey(id))
            {
                contentGrid.Children.Remove(pointerDictionary[id]);
                pointerDictionary.Remove(id);
            }

            base.OnPointerCaptureLost(args);
        }

        protected override void OnKeyDown(KeyRoutedEventArgs args)
        {
            if (args.Key == VirtualKey.Escape)
                ReleasePointerCaptures();

            base.OnKeyDown(args);
        }

        #region コードでMenuFlyoutを作成
        private MenuFlyout CreateMenuFlyout(Polyline polyline)
        {
            var menu = new MenuFlyout() { Placement = FlyoutPlacementMode.Top };
            // 色の変更コマンドの設定
            var changeCommand = new RelayCommand(arg =>
                {
                    Polyline line = arg as Polyline;
                    rand.NextBytes(rgb);
                    Color color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
                    (line.Stroke as SolidColorBrush).Color = color;
                });
            var changeColor = new MenuFlyoutItem()
            {
                Text = "Change Color",
                Command = changeCommand,
                CommandParameter = polyline
            };
            menu.Items.Add(changeColor);
            // 削除コマンドの設定
            var deleteCommand = new RelayCommand(arg =>
            {
                Polyline line = arg as Polyline;
                contentGrid.Children.Remove(line);
            });
            var delete = new MenuFlyoutItem()
                {
                    Text = "Delete",
                    Command = deleteCommand,
                    CommandParameter = polyline
                };
            menu.Items.Add(delete);
            
            return menu;
        }
        #endregion

        #region Pageのリソース定義を利用する場合
        private MenuFlyout GetMenuFlyout(Polyline polyline)
        {
            var menu = this.Resources["popupMenu"] as MenuFlyout;
            // 色の変更コマンドの設定
            var changeCommand = new RelayCommand(arg =>
            {
                Polyline line = arg as Polyline;
                rand.NextBytes(rgb);
                Color color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
                (line.Stroke as SolidColorBrush).Color = color;
            });
            var changeColor = GetMenuItem(menu, "menuChangeColor");
            changeColor.Command = changeCommand;
            changeColor.CommandParameter = polyline;
            // 削除コマンドの設定
            var deleteCommand = new RelayCommand(arg =>
            {
                Polyline line = arg as Polyline;
                contentGrid.Children.Remove(line);
            });
            var delete = GetMenuItem(menu, "menuDelete");
            delete.Command = deleteCommand;
            delete.CommandParameter = polyline;

            return menu;
        }

        private MenuFlyoutItem GetMenuItem(MenuFlyout menu, string name)
        {
            return (MenuFlyoutItem) menu.Items.Where(x => x.Name == name).FirstOrDefault();
        }
        #endregion
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
