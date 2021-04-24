using System;
using System.ComponentModel;
using System.Windows;

namespace mnd.UI.Helper
{
    public static class ViewModelLocator
    {
        // sorun çıkardı iptal edildiiii..........

        public static bool GetAutoWiredViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWiredViewModelProperty);
        }

        public static void SetAutoWiredViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWiredViewModelProperty, value);
        }

        public static readonly DependencyProperty AutoWiredViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWiredViewModel", typeof(bool),
                typeof(ViewModelLocator), new PropertyMetadata(false, AutoWiredModelChanged));

        private static void AutoWiredModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;

            var viewTypeFullName = d.GetType().FullName;

            var modelFullName = viewTypeFullName + "Model";

            var model = Type.GetType(modelFullName);

            (d as FrameworkElement).DataContext = Activator.CreateInstance(model);
        }
    }
}