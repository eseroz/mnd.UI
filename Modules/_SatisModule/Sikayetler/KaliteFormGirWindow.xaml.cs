using System.Windows;

namespace mnd.UI.Modules._SatisModule.Sikayetler
{
    /// <summary>
    /// Interaction logic for KaliteFormGirWindow.xaml
    /// </summary>
    public partial class KaliteFormGirWindow : Window
    {
        public KaliteFormGirWindow()
        {
            InitializeComponent();
        }

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
