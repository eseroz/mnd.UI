using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace mnd.UI.Converters
{
    public class ImageFromAssemblyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var imagePath = $"pack://application:,,,/Content/images/flags-iso/flat/24/{ value.ToString().ToUpper()}.png";

            try
            {
                BitmapImage bmi = new BitmapImage(new Uri(imagePath));
                return bmi;
            }
            catch (Exception)
            {
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}