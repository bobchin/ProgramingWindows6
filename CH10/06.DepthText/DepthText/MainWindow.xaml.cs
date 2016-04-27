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

namespace DepthText
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // 48ピクセル
        const int COUNT = 48;

        public MainWindow()
        {
            InitializeComponent();

            MainLogic();
        }

        private void MainLogic()
        {
            Grid grid = this.Content as Grid;
            Brush foreground = new SolidColorBrush(Colors.Black);
            Brush grayBursh = new SolidColorBrush(Colors.Gray);

            for (int i = 0; i < COUNT; i++)
            {
                bool firstOrLast = (i == 0) || (i == COUNT - 1);
                TextBlock txtblk = new TextBlock
                {
                    Text = "DEPTH",
                    FontSize = 192,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    RenderTransform = new TranslateTransform
                    {
                        X = COUNT - i - 1,
                        Y = i - COUNT + 1,
                    },
                    Foreground = firstOrLast? foreground: grayBursh,
                };
                grid.Children.Add(txtblk);
            }
        }
    }
}
