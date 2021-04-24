using System;
using System.Globalization;
using System.Windows.Data;

namespace mnd.UI.Converters
{
    public class CellBgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (value.ToString().Length == 0) return null;

            return "LightYellow";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}