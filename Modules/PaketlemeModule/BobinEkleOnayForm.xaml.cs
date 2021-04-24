using System.Windows;

namespace mnd.UI.Modules.PaketlemeModule
{
    /// <summary>
    /// Interaction logic for BobinEkleOnayForm.xaml
    /// </summary>
    public partial class BobinEkleOnayForm : Window
    {
        public BobinEkleOnayForm(PaleteBobinEkleFormModel model)
        {
            InitializeComponent();

            txtNot.Focus();

            this.DataContext = model;
        }

        public PaleteBobinEkleFormModel Model { get; internal set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
