using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WhatSizeWithBindingConverter
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class FormattedStringConverter : IValueConverter
    {
        /*
         * Culture：言語・日時・通貨などの書式・年号・暦などのロケールに関する情報
         *
         * Cultureはスレッド毎に関連付けられ、Cultureを表すクラスがCultureInfoクラスとなる。
         * CultureInfoクラスに特定のカルチャにおける書式や暦などの情報を取得するためのプロパティを持っている。
         */


        /// <summary>
        /// ソースからターゲットへの変換処理
        /// </summary>
        /// <param name="value">ソースの値</param>
        /// <param name="targetType">ターゲットの型</param>
        /// <param name="parameter">XAMLでConverterParameterで指定する値</param>
        /// <param name="culture"></param>
        public object Convert(object value, Type targetType, 
                              object parameter, CultureInfo culture)
        {
            if (value is IFormattable &&
                parameter is string &&
                !String.IsNullOrEmpty(parameter as string) &&
                targetType == typeof(string))
            {
                /*
                if (culture == null)
                {
                    return (value as IFormattable).ToString(parameter as string, null);
                }
                else
                {
                    return (value as IFormattable).ToString(parameter as string, culture);
                }
                */
                return (value as IFormattable).ToString(parameter as string, culture);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, 
                                  object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
