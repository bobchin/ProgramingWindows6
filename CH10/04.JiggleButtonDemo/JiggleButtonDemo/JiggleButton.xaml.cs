using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace JiggleButtonDemo
{
    /// <summary>
    /// JiggleButton.xaml の相互作用ロジック
    /// </summary>
    public sealed partial class JiggleButton : Button
    {
        public JiggleButton()
        {
            InitializeComponent();
        }

        private void OnJiggleClick(object sender, RoutedEventArgs e)
        {
            (this.Resources["jiggleAnimation"] as Storyboard).Begin();
        }
    }
}
