using System;
using System.Globalization;
using System.Windows.Data;

namespace mnd.UI.Converters
{
    public class AlasimRenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var alasimDeger = (string)value;

            if (alasimDeger == "8006M") return "Green";

            if (alasimDeger == "8006F") return "Blue";

            if (alasimDeger == "3003") return "Yellow";

            if (alasimDeger == "1200") return "Gray";

            if (alasimDeger == "1050") return "SmokeWhite";  // white olmasına rağmen belli olsun diye

            if (alasimDeger == "8156") return "Orange";

            if (alasimDeger == "8079") return "Pink";

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class AlasimFontRenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var alasimDeger = (string)value;

            if (alasimDeger == "8006M") return "White";     //"Green"

            if (alasimDeger == "8006F") return "White";  //"Blue";

            if (alasimDeger == "3003") return "Black";// "Yellow";

            if (alasimDeger == "1200") return "White"; // "Gray";

            if (alasimDeger == "1050") return "Black"; // "SmokeWhite";  // white olmasına rağmen belli olsun diye

            if (alasimDeger == "8156") return "White"; // "Orange";

            if (alasimDeger == "8079") return "Black"; // "Pink";

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}