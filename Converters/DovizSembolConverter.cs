using mnd.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace mnd.UI.Converters
{

    [ValueConversion(typeof(object), typeof(bool))]
    public class DovizSembolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string doviztip = (string)value;

            if (doviztip == "") return "";

            var sembol = DovizHelper.SimgeyeDonustur(doviztip);
            return sembol;

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

