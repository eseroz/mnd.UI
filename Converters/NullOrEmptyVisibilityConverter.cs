using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace mnd.UI.Converters
{
    public class NullOrEmptyVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Hidden;

            if (value.ToString() == string.Empty) return Visibility.Hidden;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}