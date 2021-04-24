using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace mnd.UI.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class RatioConverter : MarkupExtension, IValueConverter
    {
        private static RatioConverter _instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var c = double.Parse(parameter.ToString());

            double size = System.Convert.ToDouble(value) * c;
            return size;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new RatioConverter());
        }
    }
}