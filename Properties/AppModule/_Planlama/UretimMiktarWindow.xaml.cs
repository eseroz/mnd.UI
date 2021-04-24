using System.Windows;

namespace Pandap.UI.AppModule._Planlama
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