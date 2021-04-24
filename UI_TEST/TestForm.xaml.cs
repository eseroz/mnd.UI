using mnd.Logic.Persistence;
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

namespace mnd.UI.UI_TEST
{
    /// <summary>
    /// Interaction logic for TestForm.xaml
    /// </summary>
    public partial class TestForm : Window
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            UnitOfWork uow = new UnitOfWork();

            var user = uow.KullaniciRepo.KullaniciGetir("cagatay.ercan");
            AppPandap.AktifKullanici = user;

            RaporTest.YazdirTest();
          
        }
    }
}
