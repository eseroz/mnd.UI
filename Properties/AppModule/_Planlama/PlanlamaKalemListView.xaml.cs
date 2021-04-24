using System.Windows.Controls;
using Microsoft.Win32;

namespace Pandap.UI.AppModule._Planlama
{
    /// <summary>
    /// Interaction logic for SiparisPlanRapor.xaml
    /// </summary>
    public partial class PlanlamaKalemListView : UserControl
    {
        public PlanlamaKalemListView()
        {
            InitializeComponent();
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.FileName = "export.xlsx";
            saveFileDialog.Filter = "excel dosyası (.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                view1.ExportToXlsx(saveFileDialog.FileName);
            }
        }
    }
}