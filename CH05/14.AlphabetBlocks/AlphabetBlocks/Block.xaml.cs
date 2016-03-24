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

namespace AlphabetBlocks
{
    /// <summary>
    /// Block.xaml の相互作用ロジック
    /// </summary>
    public partial class Block : UserControl
    {
        static int zindex;

        public static DependencyProperty TextProperty { private set; get; }

        static Block()
        {
            TextProperty = DependencyProperty.Register("Text",
                typeof(string), 
                typeof(Block), 
                new PropertyMetadata("?"));
        }

        public static int ZIndex
        {
            get { return ++zindex; }
        }


        public Block()
        {
            InitializeComponent();
        }

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        private void OnThumbDragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            Canvas.SetZIndex(this, ZIndex);
        }

        private void OnThumbDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);
            Canvas.SetTop(this, Canvas.GetTop(this) + e.VerticalChange);
        }
    }
}
