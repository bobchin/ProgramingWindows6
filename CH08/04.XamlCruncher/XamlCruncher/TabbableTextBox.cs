using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XamlCruncher
{
    public class TabbableTextBox : TextBox
    {
        #region 依存関係プロパティ
        static TabbableTextBox()
        {
            /*
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(TabbableTextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
            */

            TabSpacesProperty =
                DependencyProperty.Register("TabSpaces",
                    typeof(int), typeof(TabbableTextBox),
                    new PropertyMetadata(4));
        }

        public static DependencyProperty TabSpacesProperty { private set; get; }


        public int TabSpaces
        {
            set { SetValue(TabSpacesProperty, value); }
            get { return (int)GetValue(TabSpacesProperty); }
        }
        #endregion


        public bool IsModified { set; get; }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.IsModified = true;

            if (e.Key == Key.Tab)
            {
                int line, col;
                GetPositionFromIndex(this.SelectionStart, out line, out col);
                int insertCount = this.TabSpaces - col % this.TabSpaces;
                this.SelectedText = new string(' ', insertCount);
                this.SelectionStart += insertCount;
                this.SelectionLength = 0;
                e.Handled = true;
                return;
            }
            base.OnKeyDown(e);
        }

        public void GetPositionFromIndex(int index, out int line, out int col)
        {
            line = col = 0;
            int iChar = this.SelectionStart;
            int iLine = this.GetLineIndexFromCharacterIndex(iChar);
            if (iLine == -1)
            {
                line = col = -1;
                return;
            }

            int iCol = iChar - this.GetCharacterIndexFromLineIndex(iLine);
            if (this.SelectionLength > 0)
            {
                iChar += this.SelectionLength;
                iLine = this.GetLineIndexFromCharacterIndex(iChar);
                iCol = iChar - this.GetCharacterIndexFromLineIndex(iLine);
            }
            line = iLine;
            col = iCol;

            /*
            if (index > Text.Length)
            {
                line = col = -1;
                return;
            }
            line = col = 0;
            int i = 0;

            while (i < index)
            {
                if (Text[i] == '\n')
                {
                    line++;
                    col = 0;
                }
                else if(Text[i] == '\r')
                {
                    index++;
                }
                else
                {
                    col++;
                }
                i++;
            }
            */
        }
    }
}
