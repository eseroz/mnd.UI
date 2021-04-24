using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mnd.UI.Modules._SatisModule.KapasiteTakip
{
    /// <summary>
    /// Interaction logic for KapasiteTakipListView.xaml
    /// </summary>
    public partial class KapasiteTakipListView : UserControl
    {
        public KapasiteTakipListView()
        {
            InitializeComponent();


        }

        private void pvt2_CellSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (pvt2.MultiSelection.SelectedCells.Count == 0)
            {
                sumText.Visibility = Visibility.Hidden;
                return;
            }

            sumText.Visibility = Visibility.Visible;

            double sum = 0;
            foreach (var cell in pvt2.MultiSelection.SelectedCells)
            {
                sum += Convert.ToDouble(pvt2.GetCellValue(cell.X, cell.Y));
            }
            sumText.Text = sum.ToString("n0");


        }
    }
}
