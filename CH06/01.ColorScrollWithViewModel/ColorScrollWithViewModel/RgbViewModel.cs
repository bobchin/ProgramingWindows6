using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorScrollWithViewModel
{
    public class RgbViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        double red, green, blue;
        Color color = Color.FromArgb(255, 0, 0, 0);

        public double Red
        {
            set
            {
                if (red != value)
                {
                    red = value;
                    OnProperyChanged("Red");
                    Calculate();
                }
            }

            get
            {
                return red;
            }
        }

        public double Green
        {
            set
            {
                if (green != value)
                {
                    green = value;
                    OnProperyChanged("Green");
                    Calculate();
                }
            }

            get
            {
                return green;
            }
        }

        public double Blue
        {
            set
            {
                if (blue != value)
                {
                    blue = value;
                    OnProperyChanged("Blue");
                    Calculate();
                }
            }

            get
            {
                return blue;
            }
        }

        public Color Color
        {
            /*
               安全のためクラス内部からしか変更できないようにprotectedにしておく
             */
            protected set
            {
                if (color != value)
                {
                    color = value;
                    OnProperyChanged("Color");
                }
            }
            /*
            set
            {
                if (color != value)
                {
                    color = value;
                    OnProperyChanged("Color");
                    this.Red = value.R;
                    this.Green = value.G;
                    this.Blue = value.B;
                }
            }
            */
            get
            {
                return color;
            }
        }

        private void OnProperyChanged(string properyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }

        private void Calculate()
        {
            this.Color = Color.FromArgb(255, (byte)this.Red, (byte)this.Green, (byte)this.Blue);
        }

    }
}
