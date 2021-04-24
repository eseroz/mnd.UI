using DevExpress.Mvvm.UI.Interactivity;
using System.Windows.Controls;

namespace mnd.UI.ControlHelpers
{
    public class KeepSelectionInView : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += DataGrid_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SelectionChanged -= DataGrid_SelectionChanged;
        }

        void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssociatedObject.SelectedItem != null)
            {

                AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);

            }

            else if (AssociatedObject.SelectedItems != null && AssociatedObject.SelectedItems.Count > 0)
                AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItems[0]);
        }
    }
}
