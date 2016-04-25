using System.Windows;
using System.Windows.Media.Animation;

namespace SimpleAnimation
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

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            (this.Resources["storyboard"] as Storyboard).Begin();
        }
    }
}
