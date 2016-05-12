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
using Petzold.ProgrammingWindows6.Chapter11;

namespace ListBoxWithItemTemplate
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

        private void OnItemLoaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Item Loaded: " +
                ((sender as FrameworkElement).DataContext as NamedColor).Name);
        }
    }
}
