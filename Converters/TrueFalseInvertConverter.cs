using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace mnd.UI.Converters
{
    public class TrueFalseInvertConverter : MarkupExtension, IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(value.ToString())) return false;

            return !((bool)value);
        }

        public object ConvertBack(object value, System.Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion IValueConverter Members

        public override object ProvideValue(System.IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}