using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToAsync3
{
    public class DialogButton
    {
        public DialogButton(string title, object color)
        {
            this.Title = title;
            this.Id = color;
        }

        public string Title { set; get; }
        public object Id { set; get; }
    }
}
