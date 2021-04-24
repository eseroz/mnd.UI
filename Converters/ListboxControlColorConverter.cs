using System;
using System.Windows.Data;
using System.Windows.Media;

namespace mnd.UI.Converters
{
    public class ListboxControlColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var v = (bool)value;

            if (v == true)
            {
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.MediumAquamarine;
                return brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.White;
                return brush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion IValueConverter Members
    }
}