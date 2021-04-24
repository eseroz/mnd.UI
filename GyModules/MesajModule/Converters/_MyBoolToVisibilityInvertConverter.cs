using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace mnd.UI.GyModules.MesajModule.Converters
{
    public class MyBoolToVisibilityInvertConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                bool? nullable = (bool?)value;
                flag = nullable.HasValue ? nullable.Value : false;
            }
            return (!flag ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}