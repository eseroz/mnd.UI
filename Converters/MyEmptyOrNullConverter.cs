using System;
using System.Globalization;
using System.Windows.Data;

namespace mnd.UI.Converters
{
    public class MyEmptyOrNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return true;

            if (value.ToString() == string.Empty) return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}