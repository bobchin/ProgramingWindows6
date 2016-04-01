using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleContextDialog
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTextBlockMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            ShowPopup();
        }

        private void ShowPopup()
        {
            StackPanel stackPanel = new StackPanel();

            // Buttonコントロールを２つ作成してStackPanelに追加
            Button btn1 = new Button
            {
                Content = "Larger font",
                Tag = 1.2,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(12),
            };
            btn1.Click += OnButtonClick;
            stackPanel.Children.Add(btn1);

            Button btn2 = new Button
            {
                Content = "Smaller font",
                Tag = 1 / 1.2,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(12),
            };
            btn2.Click += OnButtonClick;
            stackPanel.Children.Add(btn2);

            // RadioButtonコントロールを３つ作成してStackPanelに追加
            string[] names = { "Red", "Green", "Blue" };
            Color[] colors = { Colors.Red, Colors.Green, Colors.Blue };

            for (int i = 0; i < names.Length; i++)
            {
                RadioButton radioButton = new RadioButton
                {
                    Content = names[i],
                    Foreground = new SolidColorBrush(colors[i]),
                    IsChecked = (textBlock.Foreground as SolidColorBrush).Color == colors[i],
                    Margin = new Thickness(12),
                };
                radioButton.Checked += OnRadioButtonChecked;
                stackPanel.Children.Add(radioButton);
            }

            // StackPanelのBorderを作成
            Border border = new Border
            {
                Child = stackPanel,
                //Background = new SolidColorBrush(Colors.Black),
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(24),
            };

            // Popupオブジェクトを作成
            Popup popup = new Popup
            {
                Child = border,
                StaysOpen = false,
                //IsLightDismissEnabled = true,
            };

            popup.PlacementTarget = textBlock;
            popup.Placement = PlacementMode.Center;
            
            // コンテンツのサイズに基づいて位置を調整
            border.Loaded += (loadedSender, loadedArgs) =>
            {
                btn1.Focus();
            };

            // ポップアップを表示
            popup.IsOpen = true;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            textBlock.FontSize *= (double)(sender as Button).Tag;
        }

        private void OnRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            textBlock.Foreground = (sender as RadioButton).Foreground;
        }
    }
}
