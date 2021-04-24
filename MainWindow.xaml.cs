using DevExpress.Xpf.Core;
using System.Windows.Controls;
using System.Windows.Input;

namespace mnd.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Title = AppPandap.GetAppTitle();

            AppPandap.MainWindow = this;
        }

        private void ItemOnPreviewMouseDown(
            object sender, MouseButtonEventArgs e)
        {
            ((ListBoxItem)sender).IsSelected = true;
        }

        private void DockLayoutManager_DockItemClosing(object sender, DevExpress.Xpf.Docking.Base.ItemCancelEventArgs e)
        {

        }

        private void LayoutGroup_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}