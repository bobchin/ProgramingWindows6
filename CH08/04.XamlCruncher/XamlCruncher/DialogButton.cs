using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlCruncher
{
    public class DialogButton
    {
        public DialogButton(string title, object id, bool defaultButton = false, bool cancelButton = false)
        {
            this.Title = title;
            this.Id = id;
            this.Default = defaultButton;
            this.Cancel = cancelButton;
        }

        public string Title { get; set; }
        public object Id { get; set; }

        public bool Default { get; set; }
        public bool Cancel { get; set; }
    }
}
