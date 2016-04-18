using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace XamlCruncher
{
    /// <summary>
    /// RulerContainer.xaml の相互作用ロジック
    /// </summary>
    public partial class RulerContainer : UserControl
    {
        private const double RULER_WIDTH = 12;

        #region 依存関係プロパティ
        static RulerContainer()
        {
            ChildProperty =
                DependencyProperty.Register("Child",
                    typeof(UIElement), typeof(RulerContainer),
                    new PropertyMetadata(null, OnChildChanged));

            ShowRulerProperty =
                DependencyProperty.Register("ShowRuler",
                    typeof(bool), typeof(RulerContainer),
                    new PropertyMetadata(false, OnShowRulerChanged));

            ShowGridLinesProperty =
                DependencyProperty.Register("ShowGridLines",
                    typeof(bool), typeof(RulerContainer),
                    new PropertyMetadata(false, OnShowGridLinesChanged));
        }

        public static DependencyProperty ChildProperty { private set; get; }
        public static DependencyProperty ShowRulerProperty { private set; get; }
        public static DependencyProperty ShowGridLinesProperty { private set; get; }


        #region インスタンスコンストラクタとプロパティ
        public RulerContainer()
        {
            InitializeComponent();
        }

        public UIElement Child
        {
            set { SetValue(ChildProperty, value); }
            get { return (UIElement)GetValue(ChildProperty); }
        }

        public bool ShowRuler
        {
            set { SetValue(ShowRulerProperty, value); }
            get { return (bool)GetValue(ShowRulerProperty); }
        }

        public bool ShowGridLines
        {
            set { SetValue(ShowGridLinesProperty, value); }
            get { return (bool)GetValue(ShowGridLinesProperty); }
        }
        #endregion


        #region プロパティが変化したときに呼び出されるコールバック
        private static void OnChildChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as RulerContainer).boder.Child = (UIElement)args.NewValue;
        }
        private static void OnShowRulerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as RulerContainer).RedrawRuler();
        }
        private static void OnShowGridLinesChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as RulerContainer).RedrawGridLines();
        }
        #endregion

        #region Gridのイベント
        private void OnGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RedrawRuler();
            RedrawGridLines();
        }
        #endregion

        #endregion


        #region 再描画処理 Line要素とTextBlock要素を作成し２つのパネルに配置
        private void RedrawGridLines()
        {
            gridLinesGrid.Children.Clear();

            if (!this.ShowGridLines)
            {
                return;
            }

            // 縦のグリッド線は24ピクセル間隔
            for (double x = 24; x < gridLinesGrid.ActualWidth; x += 24)
            {
                Line line = new Line
                {
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = gridLinesGrid.ActualHeight,
                    Stroke = this.Foreground,
                    StrokeThickness = x % 96 == 0 ? 1 : 0.5, // 96ピクセルごとに太い線
                };
                gridLinesGrid.Children.Add(line);
            }

            // 横のグリッド線も24ピクセル間隔
            for (double y = 24; y < gridLinesGrid.ActualHeight; y += 24)
            {
                Line line = new Line
                {
                    X1 = 0,
                    Y1 = y,
                    X2 = gridLinesGrid.ActualWidth,
                    Y2 = y,
                    Stroke = this.Foreground,
                    StrokeThickness = y % 96 == 0 ? 1 : 0.5, // 96ピクセルごとに太い線
                };
                gridLinesGrid.Children.Add(line);
            }
        }

        private void RedrawRuler()
        {
            rulerCanvas.Children.Clear();

            if (!this.ShowRuler)
            {
                innerGrid.Margin = new Thickness();
                return;
            }

            innerGrid.Margin = new Thickness(RULER_WIDTH, RULER_WIDTH, 0, 0);

            // 上部のルーラー
            for (double x = 0; x < gridLinesGrid.ActualWidth - RULER_WIDTH; x += 12)
            {
                // 1インチごとに数字を表示
                if (x > 0 && x % 96 == 0)
                {
                    TextBlock txtblk = new TextBlock
                    {
                        Text = (x / 96).ToString("F0"),
                        FontSize = RULER_WIDTH - 2,
                    };

                    // 作成時はActualWidthは0になるが、Measure()呼び出しで強制的に計算される
                    txtblk.Measure(new Size());
                    Canvas.SetLeft(txtblk, RULER_WIDTH + x - txtblk.ActualWidth / 2);
                    Canvas.SetTop(txtblk, 0);
                    rulerCanvas.Children.Add(txtblk);
                }
                // 1/8 インチごとに目盛を表示
                else
                {
                    Line line = new Line
                    {
                        X1 = RULER_WIDTH + x,
                        Y1 = (x % 48 == 0) ? 2 : 4,
                        X2 = RULER_WIDTH + x,
                        Y2 = (x % 48 == 0) ? RULER_WIDTH - 2 : RULER_WIDTH - 4,
                        Stroke = this.Foreground,
                        StrokeThickness = 1,
                    };
                    rulerCanvas.Children.Add(line);
                }
            }

            // 左側のルーラー
            for (double y = 0; y < gridLinesGrid.ActualHeight - RULER_WIDTH; y += 12)
            {
                // 1インチごとに数字を表示
                if (y > 0 && y % 96 == 0)
                {
                    TextBlock txtblk = new TextBlock
                    {
                        Text = (y / 96).ToString("F0"),
                        FontSize = RULER_WIDTH - 2,
                    };

                    // 作成時はActualHeightは0になるが、Measure()呼び出しで強制的に計算される
                    txtblk.Measure(new Size());
                    Canvas.SetLeft(txtblk, 2);
                    Canvas.SetTop(txtblk, RULER_WIDTH + y - txtblk.ActualHeight / 2);
                    rulerCanvas.Children.Add(txtblk);
                }
                // 1/8 インチごとに目盛を表示
                else
                {
                    Line line = new Line
                    {
                        X1 = (y % 48 == 0) ? 2 : 4,
                        Y1 = RULER_WIDTH + y,
                        X2 = (y % 48 == 0) ? RULER_WIDTH - 2 : RULER_WIDTH - 4,
                        Y2 = RULER_WIDTH + y,
                        Stroke = this.Foreground,
                        StrokeThickness = 1,
                    };
                    rulerCanvas.Children.Add(line);
                }
            }

            // 数字が表示される線を太くする

            // 上部横線
            Line topLine = new Line
            {
                X1 = RULER_WIDTH - 1,
                Y1 = RULER_WIDTH - 1,
                X2 = rulerCanvas.ActualWidth,
                Y2 = RULER_WIDTH - 1,
                Stroke = this.Foreground,
                StrokeThickness = 2,
            };
            rulerCanvas.Children.Add(topLine);

            // 左側縦線
            Line leftLine = new Line
            {
                X1 = RULER_WIDTH - 1,
                Y1 = RULER_WIDTH - 1,
                X2 = RULER_WIDTH - 1,
                Y2 = rulerCanvas.ActualHeight,
                Stroke = this.Foreground,
                StrokeThickness = 2,
            };
            rulerCanvas.Children.Add(leftLine);
        }
        #endregion
    }
}
