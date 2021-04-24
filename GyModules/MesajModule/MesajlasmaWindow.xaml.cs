using System.Windows;
using System.Windows.Input;

namespace mnd.UI.GyModules.MesajModule
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MesajlasmaWindow : Window
    {
        public MesajlasmaWindow()
        {
            InitializeComponent();

            txtMetin.Focus();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            lst1.ScrollIntoView(lst1.SelectedItem);
        }

        private void CommandBinding_PreviewCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TextEdit_PreviewExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var cmdName = (e.Command as RoutedUICommand).Name;

            if (cmdName == "Paste")
            {
                var mv = (MesajlasmaViewModel)this.DataContext;
                mv.Paste();
            }
        }

        private void Grid_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true; // drop image burda aktif olur.
        }

        private void Grid_PreviewDrop(object sender, DragEventArgs e)
        {
            e.Handled = true;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                var mv = (MesajlasmaViewModel)this.DataContext;
                mv.DropFiles(files);
            }
        }
    }
}