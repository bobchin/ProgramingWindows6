﻿using System;
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

namespace PetzoldWindows8Controls
{
    /// <summary>
    /// ColorItem.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorItem : UserControl
    {
        public ColorItem(string name, Color clr)
        {
            InitializeComponent();

            rectangle.Fill = new SolidColorBrush(clr);
            txtblkName.Text = name;
            txtblkRgb.Text = String.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}", clr.A, clr.R, clr.G, clr.B);
        }
    }
}
