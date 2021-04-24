using System;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Pandap.UI.AppModule._Planlama
{
    /// <summary>
    /// Interaction logic for SiparisPlanRapor.xaml
    /// </summary>
    public partial class UretimEmriListView : UserControl
    {
        public UretimEmriListView()
        {
            InitializeComponent();

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Pandap_Uretim_GridConfiguration.xml";

            if (File.Exists(path)) grdUretimEmirleri.RestoreLayoutFromXml(path);


        }


        private void SimpleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            grdUretimEmirleri.SaveLayoutToXml(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + @"\Pandap_Uretim_GridConfiguration.xml");

        }

        private void SimpleButton_Click_1(object sender, System.Windows.RoutedEventArgs e)
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