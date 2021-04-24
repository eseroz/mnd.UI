using System;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace mnd.UI.GyModules.MesajModule.Converters
{
    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string data = value as string;

            if (data == null)
                return null;

            string[] dokumanTipleri = { ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".jpeg", ".txt", ".msg", ".eml" };

            var inx = data.IndexOf(';');
            var file_extension = data.Substring(0, inx).ToLower();
            var file_data = data.Substring(inx + 1, data.Length - inx - 1);

            if (dokumanTipleri.Contains(file_extension))
                return System.Convert.FromBase64String(file_data);
            else
            {
                BitmapImage bi = new BitmapImage();

                bi.BeginInit();
                bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(file_data));
                bi.EndInit();

                return bi;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}