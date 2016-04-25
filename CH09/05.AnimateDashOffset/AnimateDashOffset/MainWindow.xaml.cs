using System.Windows;
using System.Windows.Media.Animation;

namespace AnimateDashOffset
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (this.Resources["storyboard"] as Storyboard).Begin();
        }
    }
}
