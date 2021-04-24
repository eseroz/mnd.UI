﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace mnd.UI.Converters
{
    public class GreaterThanZeroVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Hidden;

            var count = (int)value;

            if (count > 0)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}