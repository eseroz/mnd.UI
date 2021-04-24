using DevExpress.Xpf.Bars;
using DevExpress.Xpf.PivotGrid;
using System;
using System.Reflection;
using System.Windows;

namespace mnd.UI.Modules.UretimIsletmeModule
{
    public class HeaderMenuHelper
    {
        #region AttachedProperties

        public static readonly DependencyProperty AllowFieldSummaryTypeChangingProperty =
               DependencyProperty.RegisterAttached("AllowFieldSummaryTypeChanging", typeof(Boolean), typeof(HeaderMenuHelper));

        public static void SetAllowFieldSummaryTypeChanging(DependencyObject element, Boolean value)
        {
            element.SetValue(AllowFieldSummaryTypeChangingProperty, value);
        }

        public static Boolean GetAllowFieldSummaryTypeChanging(DependencyObject element)
        {
            return (Boolean)element.GetValue(AllowFieldSummaryTypeChangingProperty);
        }

        public static readonly DependencyProperty AllowFieldSummaryDisplayTypeChangingProperty =
              DependencyProperty.RegisterAttached("AllowFieldSummaryDisplayTypeChanging", typeof(Boolean), typeof(HeaderMenuHelper));

        public static void SetAllowFieldSummaryDisplayTypeChanging(DependencyObject element, Boolean value)
        {
            element.SetValue(AllowFieldSummaryDisplayTypeChangingProperty, value);
        }

        public static Boolean GetAllowFieldSummaryDisplayTypeChanging(DependencyObject element)
        {
            return (Boolean)element.GetValue(AllowFieldSummaryDisplayTypeChangingProperty);
        }

        public static readonly DependencyProperty AllowPopupMenuCustomizationProperty =
              DependencyProperty.RegisterAttached("AllowPopupMenuCustomization", typeof(Boolean), typeof(HeaderMenuHelper),
              new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnAllowPopupMenuCustomization)));

        public static void SetAllowPopupMenuCustomization(DependencyObject element, Boolean value)
        {
            element.SetValue(AllowPopupMenuCustomizationProperty, value);
        }

        public static Boolean GetAllowPopupMenuCustomization(DependencyObject element)
        {
            return (Boolean)element.GetValue(AllowPopupMenuCustomizationProperty);
        }

        #endregion AttachedProperties

        #region PivotPopupMenuCustomization

        public static void OnAllowPopupMenuCustomization(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            PivotGridControl pivotGrid = o as PivotGridControl;
            if (pivotGrid == null) return;
            if ((Boolean)args.NewValue == true && (Boolean)args.OldValue == false)
                pivotGrid.PopupMenuShowing += pivotGrid_PopupMenuShowing;
            else if ((Boolean)args.NewValue == false && (Boolean)args.OldValue == true)
                pivotGrid.PopupMenuShowing -= pivotGrid_PopupMenuShowing;
        }

        private static void pivotGrid_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == PivotGridMenuType.Header)
            {
                PivotGridControl pivot = (PivotGridControl)sender;
                PivotGridField field = e.GetFieldInfo().Field;
                if (Convert.ToBoolean(field.GetValue(HeaderMenuHelper.AllowFieldSummaryTypeChangingProperty)))
                    e.Customizations.Add(CreateBarSubItem("Summary Type", "SummaryType", field));
                if (Convert.ToBoolean(field.GetValue(HeaderMenuHelper.AllowFieldSummaryDisplayTypeChangingProperty)))
                    e.Customizations.Add(CreateBarSubItem("Summary Display Type", "SummaryDisplayType", field));
            }
        }

        #endregion PivotPopupMenuCustomization

        #region CustomBarItemsCreation

        private static BarSubItem CreateBarSubItem(string displayText, string propertyName, PivotGridField field)
        {
            BarSubItem barSubItem = new BarSubItem();
            barSubItem.Name = "bsi" + propertyName;
            barSubItem.Content = displayText;

            PropertyInfo property = typeof(PivotGridField).GetProperty(propertyName);

            foreach (object enumValue in Enum.GetValues(property.PropertyType))
            {
                BarCheckItem checkItem = new BarCheckItem();
                checkItem.Name = "bci" + propertyName + enumValue;
                checkItem.Content = enumValue.ToString();
                checkItem.IsChecked = Object.Equals(property.GetValue(field, new object[0]), enumValue);
                checkItem.Tag = new object[] { field, property, enumValue };
                checkItem.ItemClick += itemClickEventHandler;

                barSubItem.ItemLinks.Add(checkItem);
            }
            return barSubItem;
        }

        private static void itemClickEventHandler(object sender, ItemClickEventArgs e)
        {
            BarItem barItem = sender as BarItem;
            object[] barItemInfo = (object[])barItem.Tag;
            PivotGridField field = (PivotGridField)barItemInfo[0];
            PropertyInfo property = (PropertyInfo)barItemInfo[1];
            object newValue = barItemInfo[2];
            property.SetValue(field, newValue, new object[0]);
        }

        #endregion CustomBarItemsCreation
    }
}