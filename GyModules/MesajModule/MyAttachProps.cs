using System.ComponentModel;
using System.Windows;

namespace mnd.UI.GyModules.MesajModule
{
    public class VisibilityHelpler
    {

        public static bool GetIsVisible(DependencyObject obj)
        {
            var value = (bool)obj.GetValue(IsVisibleProperty);


            return value;
        }

        public static void SetIsVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsVisibleProperty, value);
        }


        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(VisibilityHelpler), new PropertyMetadata(true, Degisti));

        private static void Degisti(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FrameworkElement;

            if (DesignerProperties.GetIsInDesignMode(d))
                obj.Visibility = Visibility.Visible;
            else
                obj.Visibility = (bool)e.NewValue == true ? Visibility.Visible : Visibility.Collapsed;




        }
    }
}
