using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace SegoeSymbols
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int CellSize = 36;
        private const int LineLength = (CellSize + 1) * 16 + 18;

        private FontFamily symbolFont = new FontFamily("Segoe UI Symbol");

        TextBlock[] txtblkColumnHeads = new TextBlock[16];
        TextBlock[,] txtblkCharacters = new TextBlock[16, 16];

        public MainWindow()
        {
            InitializeComponent();

            MainLogic();
        }

        private void MainLogic()
        {
            // 34行を作成（実際は17マスを使用する）
            // 最上位はアドレス表示。偶数行: 最下位文字 奇数行: 横仕切り線
            for (int row = 0; row < 34; row++)
            {
                // 行定義
                RowDefinition rowdef = new RowDefinition();
                if (row == 0 || row % 2 == 1)
                {
                    // 1行目と奇数行はAuto
                    rowdef.Height = GridLength.Auto;
                }
                else
                {
                    // 偶数行は固定サイズ
                    rowdef.Height = new GridLength(CellSize, GridUnitType.Pixel);
                }
                characterGrid.RowDefinitions.Add(rowdef);

                // はじめ以外の偶数行=固定サイズ行
                // 最下位文字 0～F を表示
                if (row != 0 && row % 2 == 0)
                {
                    TextBlock txtblk = new TextBlock
                    {
                        Text = (row / 2 - 1).ToString("X1"),
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    Grid.SetRow(txtblk, row);
                    Grid.SetColumn(txtblk, 0);
                    characterGrid.Children.Add(txtblk);
                }

                // 奇数行= 仕切り線
                if (row % 2 == 1)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Stroke = this.Foreground,
                        StrokeThickness = (row == 1 || row == 33)? 1.5: 0.5,
                        Height = 1,
                    };
                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, 0);
                    Grid.SetColumnSpan(rectangle, 34);
                    characterGrid.Children.Add(rectangle);
                }
            }

            // 34列を作成（実際は17マスを使用する）
            // 最左位は最下位文字表示。偶数行: フォント文字 奇数行: 縦仕切り線
            for (int col = 0; col < 34; col++)
            {
                // 列定義
                ColumnDefinition coldef = new ColumnDefinition();
                if (col == 0 || col % 2 == 1)
                {
                    // 1行目と奇数行はAuto
                    coldef.Width = GridLength.Auto;
                }
                else
                {
                    // 偶数行は固定サイズ
                    coldef.Width = new GridLength(CellSize);
                }
                characterGrid.ColumnDefinitions.Add(coldef);

                // はじめ以外の偶数列=フォント文字
                if (col != 0 && col % 2 == 0)
                {
                    TextBlock txtblk = new TextBlock
                    {
                        Text = "00" + (col / 2 - 1).ToString("X1") + "_",
                        HorizontalAlignment = HorizontalAlignment.Center,
                    };
                    Grid.SetRow(txtblk, 0);
                    Grid.SetColumn(txtblk, col);
                    characterGrid.Children.Add(txtblk);
                    txtblkColumnHeads[col / 2 - 1] = txtblk;
                }

                // 奇数列
                if (col % 2 == 1)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Stroke = this.Foreground,
                        StrokeThickness = (col == 1 || col == 33) ? 1.5 : 0.5,
                        Width = 1,
                    };
                    Grid.SetRow(rectangle, 0);
                    Grid.SetColumn(rectangle, col);
                    Grid.SetRowSpan(rectangle, 34);
                    characterGrid.Children.Add(rectangle);
                }
            }

            for (int col = 0; col < 16; col++)
            {
                for (int row = 0; row < 16; row++)
                {
                    TextBlock txtblk = new TextBlock
                    {
                        Text = ((char)(16 * col + row)).ToString(),
                        FontFamily = symbolFont,
                        FontSize = 24,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };

                    Grid.SetRow(txtblk, 2 * row + 2);
                    Grid.SetColumn(txtblk, 2 * col + 2);
                    characterGrid.Children.Add(txtblk);
                    txtblkCharacters[col, row] = txtblk;
                }
            }
        }

        private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int baseCode = 256 * (int)e.NewValue;

            for (int col = 0; col < 16; col++)
            {
                txtblkColumnHeads[col].Text = (baseCode / 16 + col).ToString("X3") + "_";

                for (int row = 0; row < 16; row++)
                {
                    int code = baseCode + 16 * col + row;
                    string strChar = null;

                    if (code <= 0xFFFF)
                    {
                        strChar = ((char)code).ToString();
                    }
                    else
                    {
                        code -= 0x10000;
                        int lead = 0xD800 + code / 1024;
                        int trail = 0xDC00 + code % 1024;
                        strChar = ((char)lead).ToString() + (char)trail;
                    }
                    txtblkCharacters[col, row].Text = strChar;
                }
            }
        }
    }

    public class DoubleToStringHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)(double)value).ToString("X2") + "__";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
