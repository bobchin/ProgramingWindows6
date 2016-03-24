using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DependencyPropertiesWithBindings
{
    /// <summary>
    /// GradientButton.xaml の相互作用ロジック
    /// </summary>
    public partial class GradientButton : Button
    {
        public static DependencyProperty Color1Property { private set; get; }
        public static DependencyProperty Color2Property { private set; get; }

        static GradientButton()
        {
            Color1Property =
                DependencyProperty.Register("Color1",
                    typeof(Color),
                    typeof(GradientButton),
                    new PropertyMetadata(Colors.White));
            Color2Property =
                DependencyProperty.Register("Color2",
                    typeof(Color),
                    typeof(GradientButton),
                    new PropertyMetadata(Colors.Black));
        }

        public GradientButton()
        {
            InitializeComponent();
        }

        public Color Color1
        {
            set { SetValue(Color1Property, value); }
            get { return (Color)GetValue(Color1Property); }
        }

        public Color Color2
        {
            set { SetValue(Color2Property, value); }
            get { return (Color)GetValue(Color2Property); }
        }
    }
}
