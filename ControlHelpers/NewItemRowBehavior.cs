using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Input;

namespace mnd.UI.ControlHelpers
{
    public class ItemRowBehavior : Behavior<TableView>
    {
        public ICommand RowUpdated
        {
            get { return (ICommand)GetValue(RowUpdatedProperty); }
            set { SetValue(RowUpdatedProperty, value); }
        }

        public static readonly DependencyProperty RowUpdatedProperty =
            DependencyProperty.Register(nameof(RowUpdated), typeof(ICommand), typeof(ItemRowBehavior), new PropertyMetadata(null));

        //-------------------------------------------------------------------------------------


        public ICommand InitNewItem
        {
            get { return (ICommand)GetValue(InitNewItemProperty); }
            set { SetValue(InitNewItemProperty, value); }
        }

        public static readonly DependencyProperty InitNewItemProperty =
            DependencyProperty.Register(nameof(InitNewItem), typeof(ICommand), typeof(ItemRowBehavior), new PropertyMetadata(null));






        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.RowUpdated += (s, e) => OnRowUpdated(e);

            AssociatedObject.InitNewRow += (s, e) => InitNewRow(e);

            


        }

        private void InitNewRow(InitNewRowEventArgs e)
        {
            try
            {
                if(InitNewItem!=null)
                {
                    var tableView = e.Source as TableView;
                    var row = tableView.Grid.GetRow(e.RowHandle);
                    InitNewItem.Execute(row);
                }
           
            }
            catch { }
        }

        protected virtual void OnRowUpdated(RowEventArgs e)
        {
            try
            {
                if (RowUpdated != null && RowUpdated.CanExecute(e.Row))
                {
                    RowUpdated.Execute(e.Row);
                  
                }
                   
            }
            catch { }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }


    }
}