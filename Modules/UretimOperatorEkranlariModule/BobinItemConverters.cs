using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    public class AktifBobinRenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var aktifMi = (bool)value;

            if (aktifMi) return "White";

            return "SmokeWhite";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
