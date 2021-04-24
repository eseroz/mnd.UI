using System.Windows;

namespace Pandap.UI.AppModule._Mesajlasma
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MesajlasmaWindow : Window
    {
        public MesajlasmaWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            lst1.ScrollIntoView(lst1.SelectedItem);
        }
    }
}