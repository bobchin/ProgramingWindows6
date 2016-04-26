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

namespace FastNotFluid
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

        private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            bool isError = false;

            TimeSpan time = new TimeSpan();
            try
            {
                time = animation.GetCurrentTime();
                isError = false;
            }
            catch
            {

                IsEnabled = true;
            }

            horzAnima.To = e.NewSize.Width;
            vertAnima.To = e.NewSize.Height;

            animation.Begin();
            if (!isError)
            {
                animation.Seek(time);
            }
        }
    }
}
