using DevExpress.Xpf.Grid;
using System;
using System.Globalization;
using System.Windows.Data;

namespace mnd.UI.Modules.UretimIsletmeModule
{
    public class HandleToIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var handle = (int)values[0] + 1;
            var grid = (GridControl)values[1];
            return grid.GetRowVisibleIndexByHandle(handle).ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}