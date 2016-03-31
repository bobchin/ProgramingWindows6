using Microsoft.Win32;
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

namespace PrimitivePad
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // WPFではAppicationDataが使用できない
        //private ApplicationDataContainer appData = ApplicationData.Current.LocalSettings;

        public MainWindow()
        {
            InitializeComponent();

            MainLogic();
        }

        private void MainLogic()
        {
            Loaded += (sender, args) =>
            {
                /*
                if (appData.Values.ContainsKey("TextWrapping"))
                {
                    txtbox.TextWrapping = (TextWrapping)appData.Values["TextWrapping"];
                }
                */
                txtbox.TextWrapping = Properties.Settings.Default.TextWrapping;

                wrapButton.IsChecked = (txtbox.TextWrapping == TextWrapping.Wrap);
                wrapButton.Content = (bool)wrapButton.IsChecked ? "Wrap" : "NoWrap";
                
                //txtbox.Focus(FocusState.Programmatic);
            };
        }

        private async void OnFileOpenButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "テキスト(*.txt)|*.txt";
            dlg.DefaultExt = ".txt";
            bool? dlgResult = dlg.ShowDialog();
            if (dlgResult != true)
            {
                return;
            }

            using (FileStream stream = File.OpenRead(dlg.FileName))
            {
                TextReader textReader = new StreamReader(stream);
                string text = await textReader.ReadToEndAsync();
                txtbox.Text = text;
            }
        }

        private async void OnFileSaveAsButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "テキスト(*.txt)|*.txt";
            dlg.DefaultExt = ".txt";
            bool? dlgResult = dlg.ShowDialog();
            if (dlgResult != true)
            {
                return;
            }

            using (FileStream stream = File.OpenWrite(dlg.FileName))
            {
                TextWriter textWriter = (TextWriter)new StreamWriter(stream);
                await textWriter.WriteAsync(txtbox.Text);
                textWriter.Close();
            }
        }

        private void OnWrapButtonChecked(object sender, RoutedEventArgs e)
        {
            txtbox.TextWrapping = (bool)wrapButton.IsChecked ? TextWrapping.Wrap : TextWrapping.NoWrap;
            wrapButton.Content = (bool)wrapButton.IsChecked ? "Wrap" : "No Wrap";

            // WPFではAppicationDataが使用できない
            //appData.Values["TextWrapping"] = (int)txtbox.TextWrapping;
            Properties.Settings.Default.TextWrapping = (bool)wrapButton.IsChecked ? TextWrapping.Wrap : TextWrapping.NoWrap;
            Properties.Settings.Default.Save();
        }
    }
}
