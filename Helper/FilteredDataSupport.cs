using DevExpress.Xpf.Grid;
using System.Collections;
using System.Windows;
using System.Windows.Input;

namespace mnd.UI.Helper
{
    public class FilteredDataSupport
    {
        public static readonly DependencyProperty VisibleDataProperty =
            DependencyProperty.RegisterAttached("VisibleData", typeof(IList), typeof(FilteredDataSupport), new PropertyMetadata(OnVisibleDataChanged));

        public static void SetVisibleData(UIElement element, IList value)
        {
            element.SetValue(VisibleDataProperty, value);
        }

        public static IList GetVisibleData(UIElement element)
        {
            return (IList)element.GetValue(VisibleDataProperty);
        }

        public static ICommand GetFilterFinished(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(FilterFinishedProperty);
        }

        public static void SetFilterFinished(DependencyObject obj, ICommand value)
        {
            obj.SetValue(FilterFinishedProperty, value);
        }

        public static readonly DependencyProperty FilterFinishedProperty =
            DependencyProperty.RegisterAttached("FilterFinished", typeof(ICommand), typeof(FilteredDataSupport), new PropertyMetadata(null));

        private static void OnVisibleDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            GridControl grid = sender as GridControl;
            if (grid == null)
                return;
            if (e.OldValue == null && e.NewValue != null)
                grid.FilterChanged += OnFilterChanged;
            else if (e.OldValue != null && e.NewValue == null)
                grid.FilterChanged -= OnFilterChanged;
        }

        private static void OnFilterChanged(object sender, RoutedEventArgs e)
        {
            GridControl grid = sender as GridControl;
            if (grid == null)
                return;
            var res = grid.DataController.GetAllFilteredAndSortedRows();
            IList visibleData = grid.GetValue(VisibleDataProperty) as IList;
            if (visibleData == null)
                return;
            visibleData.Clear();
            foreach (object item in res)
            {
                visibleData.Add(item);
            }

            GetFilterFinished(sender as DependencyObject).Execute(null);
        }
    }
}