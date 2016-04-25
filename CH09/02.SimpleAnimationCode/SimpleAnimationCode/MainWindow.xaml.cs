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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleAnimationCode
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
            DoubleAnimation anima = new DoubleAnimation
            {
                To = 96,
                Duration = new Duration(new TimeSpan(0, 0, 1)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(3),
            };

            Storyboard.SetTarget(anima, sender as Button);
            Storyboard.SetTargetProperty(anima, new PropertyPath(Button.FontSizeProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(anima);
            storyboard.Begin();
        }
    }
}
