using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace XamlCruncher
{
    /// <summary>
    /// SettingsDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingsDialog : UserControl
    {
        public SettingsDialog()
        {
            InitializeComponent();

            this.Loaded += SettingsDialog_Loaded;
            
        }

        private void SettingsDialog_Loaded(object sender, RoutedEventArgs e)
        {
            AppSettings appSettings = DataContext as AppSettings;

            if (appSettings != null)
            {
                foreach (UIElement child in orientationRadioButtonGrid.Children)
                {
                    EditOrientationRadioButton radioButton = child as EditOrientationRadioButton;
                    radioButton.IsChecked =
                        appSettings.EditOrientation == radioButton.EditOrientationTag;
                }
            }
        }

        private void OnOrientationRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            AppSettings appSettings = DataContext as AppSettings;
            EditOrientationRadioButton radioButton = sender as EditOrientationRadioButton;

            if (appSettings != null)
            {
                appSettings.EditOrientation = radioButton.EditOrientationTag;
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            var popup = this.Parent as Popup;
            popup.IsOpen = false;
        }

    }
}
