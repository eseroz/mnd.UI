using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.Windows;
using System.Windows.Controls;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip
{
    /// <summary>
    /// Interaction logic for KulceKontratListView.xaml
    /// </summary>
    public partial class KulceKontratListView : UserControl
    {
        public KulceKontratListView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            View1.ExportToXlsx("d:\\deneme.xlsx", new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }
    }
}
