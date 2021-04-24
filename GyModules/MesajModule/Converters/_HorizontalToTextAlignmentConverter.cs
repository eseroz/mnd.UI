using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace mnd.UI.GyModules.MesajModule.Converters
{
    public class HorizontalToTextAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HorizontalAlignment textAlignment;

            var v = (bool)value;

            textAlignment = v == true ? HorizontalAlignment.Right : HorizontalAlignment.Left;

            return textAlignment;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}