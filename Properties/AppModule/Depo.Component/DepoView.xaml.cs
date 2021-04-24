using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Pandap.UI.Depolar
{
    /// <summary>
    /// Interaction logic for DepoView.xaml
    /// </summary>
    public partial class DepoView : UserControl
    {
        public DepoView()
        {
            InitializeComponent();
        }

        private void DepoExceleAktar(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.FileName = "export.xlsx";
            saveFileDialog.Filter = "excel dosyası (.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                viewDepo.ExportToXlsx(saveFileDialog.FileName);
            }
        }
    }
}