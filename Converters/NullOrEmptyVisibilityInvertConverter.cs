using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace mnd.UI.Converters
{
    public class NullOrEmptyVisibilityInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Visible;

            if (value.ToString() == string.Empty) return Visibility.Visible;

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}