using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace mnd.UI.Converters
{
    public class Base64ImageConverterMetin : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            string data = value as string;

            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(data));
            bi.EndInit();

            return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}