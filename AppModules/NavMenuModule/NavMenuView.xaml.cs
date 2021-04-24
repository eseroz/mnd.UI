using System.Windows.Controls;

namespace mnd.UI.AppModules.NavMenuModule
{
    /// <summary>
    /// Interaction logic for NavMenuView.xaml
    /// </summary>
    public partial class NavMenuView : UserControl
    {
        public NavMenuView()
        {
            InitializeComponent();
            accordion.SearchText = AppPandap.NavSearchFilter;
        }

        private void accordion_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}