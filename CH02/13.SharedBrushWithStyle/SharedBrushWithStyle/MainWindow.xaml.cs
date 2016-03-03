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

namespace SharedBrushWithStyle
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Style style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.FontSizeProperty, 96));
            style.Setters.Add(new Setter(TextBlock.FontFamilyProperty, new FontFamily("Times New Roman")));

            this.Resources.Add("styleFont", style);

            InitializeComponent();
        }
    }
}
