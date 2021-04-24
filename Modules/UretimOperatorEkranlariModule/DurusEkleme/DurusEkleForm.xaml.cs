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

namespace mnd.UI.Modules.UretimOperatorEkranlariModule.DurusEkleme
{
    /// <summary>
    /// Interaction logic for DurusEkleForm.xaml
    /// </summary>
    public partial class DurusEkleForm : Window
    {
        public DurusEkleForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
