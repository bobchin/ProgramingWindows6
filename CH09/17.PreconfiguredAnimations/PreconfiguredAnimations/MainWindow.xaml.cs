using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PreconfiguredAnimations
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
            Button btn = sender as Button;
            string key = btn.Tag as string;
            Storyboard storyboard = this.Resources[key] as Storyboard;
            storyboard.Begin();
        }
    }
}
