using System;
using System.Globalization;
using System.Windows.Data;

namespace mnd.UI.Converters
{

    [ValueConversion(typeof(object), typeof(bool))]
    public class MyVisibilityFromCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;

            //int sayi = (int)value;

            //if (sayi == 0) return false;

            //return true;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

