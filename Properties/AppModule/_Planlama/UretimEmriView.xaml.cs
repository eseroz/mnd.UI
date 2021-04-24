using System.Windows;
using System.Windows.Controls;
using Pandap.UI.Helper;

namespace Pandap.UI.AppModule._Planlama
{
    /// <summary>
    /// Interaction logic for RotaKartiView.xaml
    /// </summary>
    public partial class UretimEmriView : Window
    {
        public UretimEmriView()
        {
            InitializeComponent();
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            PrintDialog diag = new PrintDialog();
            var doc = PrintHelper.GetFixedDocument(ciktiSayfa, diag);

            PrintHelper.ShowPrintPreview(doc);
        }
    }
}