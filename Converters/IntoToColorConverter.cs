using System;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace mnd.UI.Converters
{
    public class IntoToColorConverter : MarkupExtension, IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (!String.IsNullOrEmpty(value.ToString()))
                return Brushes.Yellow;
            else
                return Brushes.White;
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