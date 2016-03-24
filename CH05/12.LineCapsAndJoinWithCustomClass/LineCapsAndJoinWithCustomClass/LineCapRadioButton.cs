using System.Windows.Controls;
using System.Windows.Media;

namespace LineCapsAndJoinWithCustomClass
{
    public class LineCapRadioButton : RadioButton
    {
        public PenLineCap LineCapTag { set; get; }
    }
}
