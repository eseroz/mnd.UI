using System;
using System.Globalization;
using System.Windows.Data;

namespace mnd.UI.Converters
{
    public class HaftaKontrolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var aktifYilHafta = parameter.ToString();

            var result = String.Compare(value.ToString(), aktifYilHafta, StringComparison.Ordinal);

            if (result > 0) return true;


            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}