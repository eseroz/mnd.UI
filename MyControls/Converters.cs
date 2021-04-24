using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace mnd.UI.MyControls
{
  
    public class SurecColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var deger = (string)value;

            if (deger == "Başladı") return new SolidColorBrush(Colors.Transparent);

            if (deger == "Onaylanacak") return new SolidColorBrush(Colors.LightGray);

            if (deger == "Onaylandı") return new SolidColorBrush(Colors.Blue);

            if (deger == "Onayda") return new SolidColorBrush(Colors.Blue);

            if (deger == "Bitti") return new SolidColorBrush(Colors.Blue);




            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class SurecFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var deger = (string)value;

            if (deger == "Onaylandı") return FontWeights.Bold;

            if (deger == "Onayda") return FontWeights.Bold;


            return  FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class SurecVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var deger = (string)value;
            var param = (string)parameter;

            if (deger == param) return Visibility.Visible;

          

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

   
}