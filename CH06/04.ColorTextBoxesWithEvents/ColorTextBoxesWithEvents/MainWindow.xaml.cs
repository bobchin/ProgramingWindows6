using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorTextBoxesWithEvents
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        RgbViewModel rgbViewModel;
        Brush textBoxTextBrush;
        Brush textBoxErrorBrush = new SolidColorBrush(Colors.Red);

        public MainWindow()
        {
            InitializeComponent();

            MainLogic();
        }

        private void MainLogic()
        {
            // TextBox用のブラシを作成
            //textBoxTextBrush = this.Resources["TextBoxForegroundThemeBrush"] as SolidColorBrush;
            textBoxTextBrush = new SolidColorBrush(Colors.Brown);

            // RgbViewModelオブジェクトを作成し、フィールドに保存
            rgbViewModel = new RgbViewModel();
            rgbViewModel.PropertyChanged += OnRgbViewModelPropertyChanged;
            this.DataContext = rgbViewModel;

            // ハイライト色に初期化
            rgbViewModel.Color = SystemColors.HighlightColor;
        }

        private void OnRgbViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Red":
                    redTextBox.Text = rgbViewModel.Red.ToString("F0");
                    break;

                case "Green":
                    greenTextBox.Text = rgbViewModel.Green.ToString("F0");
                    break;

                case "Blue":
                    blueTextBox.Text = rgbViewModel.Blue.ToString("F0");
                    break;

                default:
                    break;
            }
        }

        private void OnTextBoxTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (rgbViewModel == null)
            {
                return;
            }

            byte value;

            if (sender == redTextBox && Validate(redTextBox, out value))
            {
                rgbViewModel.Red = value;
            }

            if (sender == greenTextBox && Validate(greenTextBox, out value))
            {
                rgbViewModel.Green = value;
            }

            if (sender == blueTextBox && Validate(blueTextBox, out value))
            {
                rgbViewModel.Blue = value;
            }
        }

        private bool Validate(TextBox textbox, out byte value)
        {
            bool valid = byte.TryParse(textbox.Text, out value);
            textbox.Foreground = valid ? textBoxTextBrush : textBoxErrorBrush;
            return valid;
        }
    }
}
