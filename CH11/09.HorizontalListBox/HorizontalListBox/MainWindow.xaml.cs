using System.Windows;
using Petzold.ProgrammingWindows6.Chapter11;

namespace HorizontalListBox
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
            System.Diagnostics.Debug.WriteLine("Item Loaded" +
                ((sender as FrameworkElement).DataContext as NamedColor).Name);
        }
    }
}
