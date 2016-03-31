using System;
using System.Collections.Generic;
using System.IO;
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

namespace QuickNotes
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
            this.Closing += OnClosing;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FileStream stream = File.Open(System.IO.Path.Combine(folder, "QuickNotes.txt"), FileMode.OpenOrCreate))
            {
                TextReader textReader = (TextReader)new StreamReader(stream);
                txtbox.Text = await textReader.ReadToEndAsync();
                txtbox.SelectionStart = txtbox.Text.Length;
            }

            txtbox.Focus();
        }

        private async void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FileStream stream = File.Open(System.IO.Path.Combine(folder, "QuickNotes.txt"), FileMode.OpenOrCreate))
            {
                TextWriter textWriter = (TextWriter)new StreamWriter(stream);
                await textWriter.WriteAsync(txtbox.Text);
                textWriter.Close();
            }
        }

    }
}
