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
using System.Windows.Shapes;

namespace mnd.UI.Modules.OperasyonModule
{
    /// <summary>
    /// Interaction logic for SevkTeslimTarihiGirisForm.xaml
    /// </summary>
    public partial class SevkTeslimTarihiGirisForm : Window
    {
        public SevkTeslimTarihiGirisForm()
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
