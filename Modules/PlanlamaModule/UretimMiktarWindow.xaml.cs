using System.Windows;

namespace mnd.UI.Modules.PlanlamaModule
{
    /// <summary>
    /// Interaction logic for UretimMiktarWindow.xaml
    /// </summary>
    public partial class UretimMiktarWindow : Window
    {
        public UretimMiktarWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}