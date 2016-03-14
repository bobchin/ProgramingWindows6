using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace ColorList1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainLogic();
        }

        private void MainLogic()
        {
            IEnumerable<PropertyInfo> properties = typeof(Colors).GetTypeInfo().DeclaredProperties;
            foreach (PropertyInfo property in properties)
            {
                Color clr = (Color)property.GetValue(null);

                StackPanel horzStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                // 左横部分
                Rectangle recttangle = new Rectangle
                {
                    Width = 72,
                    Height = 72,
                    Fill = new SolidColorBrush(clr),
                    Margin = new Thickness(6)
                };
                horzStackPanel.Children.Add(recttangle);

                // 右横部分
                StackPanel vertStackPanel = CreateVerticalStackPanel(clr, property);
                horzStackPanel.Children.Add(vertStackPanel);

                // 1行分を追加
                stackPanel.Children.Add(horzStackPanel);
            }
        }

        private StackPanel CreateVerticalStackPanel(Color clr, PropertyInfo property)
        {
            StackPanel vertStackPanel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Center
            };

            TextBlock txtblkName = new TextBlock()
            {
                Text = property.Name,
                FontSize = 24
            };
            vertStackPanel.Children.Add(txtblkName);

            TextBlock txtblkRgb = new TextBlock()
            {
                Text = String.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}", clr.A, clr.R, clr.G, clr.B),
                FontSize = 18
            };
            vertStackPanel.Children.Add(txtblkRgb);

            return vertStackPanel;
        }
    }
}
