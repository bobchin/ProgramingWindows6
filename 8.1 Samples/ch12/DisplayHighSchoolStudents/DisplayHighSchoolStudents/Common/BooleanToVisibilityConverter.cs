using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DisplayHighSchoolStudents.Common
{
    /// <summary>
    /// true を <see cref="Visibility.Visible"/> に、および false を 
    /// <see cref="Visibility.Collapsed"/> に変換する値コンバーター。
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
