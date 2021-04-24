using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Windows.Controls;

namespace mnd.UI.Behaviors
{
    public class ScrollIntoViewForListBox : Behavior<ListBox>
    {

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }


        void AssociatedObject_SelectionChanged(object sender,
                                               SelectionChangedEventArgs e)
        {
            if (sender is ListBox)
            {
                ListBox listBox = (sender as ListBox);
                if (listBox.SelectedItem != null)
                {
                    listBox.Dispatcher.BeginInvoke(
                        (Action)(() =>
                        {
                            listBox.UpdateLayout();
                            if (listBox.SelectedItem !=
                            null)
                                listBox.ScrollIntoView(
                                listBox.SelectedItem);
                        }));
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SelectionChanged -=
                AssociatedObject_SelectionChanged;

        }
    }
}
