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

namespace AlphabetBlocks
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        const double BUTTON_SIZE = 60;
        const double BUTTON_FONT = 18;
        // タスクバー分のマージンWPFの場合の対応
        const double BOTTOM_MARGIN = 20;
        string blockChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?-+*/%=";
        Color[] colors = { Colors.Red, Colors.Green, Colors.Orange, Colors.Blue, Colors.Purple };
        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            buttonCanvas.Children.Clear();

            // タスクバー分のマージンWPFの場合の対応
            double newHeight = e.NewSize.Height - BOTTOM_MARGIN;

            //double widthFraction = e.NewSize.Width / (e.NewSize.Width + e.NewSize.Height);
            double widthFraction = e.NewSize.Width / (e.NewSize.Width + newHeight);
            int horzCount = (int)(widthFraction * blockChars.Length / 2);
            int vertCount = (int)(blockChars.Length / 2 - horzCount);
            int index = 0;

            double slotWidth = (e.NewSize.Width - BUTTON_SIZE) / horzCount;
            //double slotHeight = (e.NewSize.Height - BUTTON_SIZE) / horzCount;
            double slotHeight = (newHeight - BUTTON_SIZE) / horzCount;

            // 上部の左から右に配置されるボタン
            for (int i = 0; i < horzCount; i++)
            {
                Button button = MakeButton(index++);
                Canvas.SetLeft(button, i * slotWidth);
                Canvas.SetTop(button, 0);
                buttonCanvas.Children.Add(button);
            }

            // 右側の上から下に配置されるボタン
            for (int i = 0; i < vertCount; i++)
            {
                Button button = MakeButton(index++);
                Canvas.SetLeft(button, this.ActualWidth - BUTTON_SIZE);
                Canvas.SetTop(button, i * slotHeight);
                buttonCanvas.Children.Add(button);
            }

            // 下部の右から左に配置されるボタン
            for (int i = 0; i < horzCount; i++)
            {
                Button button = MakeButton(index++);
                Canvas.SetLeft(button, this.ActualWidth - (i * slotWidth) - BUTTON_SIZE);
                //Canvas.SetTop(button, this.ActualHeight - BUTTON_SIZE);
                Canvas.SetTop(button, newHeight - BUTTON_SIZE);
                buttonCanvas.Children.Add(button);
            }

            // 左側の下から上に配置されるボタン
            for (int i = 0; i < vertCount; i++)
            {
                Button button = MakeButton(index++);
                Canvas.SetLeft(button, 0);
                //Canvas.SetTop(button, this.ActualHeight - (i * slotHeight) - BUTTON_SIZE);
                Canvas.SetTop(button, newHeight - (i * slotHeight) - BUTTON_SIZE);
                buttonCanvas.Children.Add(button);
            }
        }

        private Button MakeButton(int index)
        {
            Button button = new Button
            {
                Content = blockChars[index].ToString(),
                Width = BUTTON_SIZE,
                Height = BUTTON_SIZE,
                FontSize = BUTTON_FONT,
                Tag = new SolidColorBrush(colors[index % colors.Length]),
            };
            button.Click += OnButtonClick;
            return button;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Block block = new Block
            {
                Text = button.Content as string,
                Foreground = button.Tag as Brush,
            };
            Canvas.SetLeft(block, this.ActualWidth / 2 - 144 * rand.NextDouble());
            Canvas.SetTop(block, this.ActualHeight / 2 - 144 * rand.NextDouble());
            Canvas.SetZIndex(block, Block.ZIndex);
            blockCanvas.Children.Add(block);
        }
    }
}
