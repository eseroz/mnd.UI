using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic.Persistence;
using System.Data.SqlClient;
using System.Windows;

namespace mnd.UI.AppModules.LoginModule
{
    public partial class LoginWindow : Window
    {
        public bool BeniHatirla { get; set; }

        public LoginWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            BeniHatirla = GlobalSettings.Default.BeniHatirla == "Evet";
            chkBeniHatirla.IsChecked = BeniHatirla;


            if (BeniHatirla)
            {
                txtKullanici.Text = GlobalSettings.Default.KullaniciAdi;
                txtParola.Password = GlobalSettings.Default.KullaniciParola;
            }

            txtDbUser.Text = GlobalSettings.Default.DbUser;

            if (GlobalSettings.Default.SqlCnnString != null)
            {
                var cnnBuilder = new SqlConnectionStringBuilder(GlobalSettings.Default.SqlCnnString);
                txtSunucuAdi.Text = cnnBuilder.DataSource;
                txtServerSifre.Password = cnnBuilder.Password;
            }

            this.Loaded += LoginWindow_Loaded;

            this.Title = "Kullanici Giriş - ver : " + GetAppVersion();

            #if DEBUG
                //AppPandap.NavSearchFilter = "";
            #endif

        }

        public string GetAppVersion()
        {
            return "";

            //Package package = Package.Current;
            //PackageId packageId = package.Id;
            //PackageVersion version = packageId.Version;

            //return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = AppPandap.GetAppTitle();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UpdateSettings();

            UnitOfWork uow = new UnitOfWork();

            var user = uow.KullaniciRepo.KullaniciGetir(txtKullanici.Text, txtParola.Password);

            if (user != null)
            {
                AppPandap.AktifKullanici = user;

                AppPandap.KullaniciRol = user.KullaniciRol;

                AppPandap.YoneticiMi = user.KullaniciRol == KULLANICIROLLERI.YONETICI;

                this.DialogResult = true;

                uow.Dispose();
            }
            else
            {
                MessageBox.Show("Hatalı giriş");
            }
        }

        public void UpdateSettings()
        {

            GlobalSettings.Default.BeniHatirla = chkBeniHatirla.IsChecked.GetValueOrDefault() == true ? "Evet" : "Hayır";

            GlobalSettings.Default.KullaniciAdi = txtKullanici.Text;
            GlobalSettings.Default.KullaniciParola = txtParola.Password;


            GlobalSettings.Default.Sunucu = txtSunucuAdi.Text;
            GlobalSettings.Default.DbUser = txtDbUser.Text;
            GlobalSettings.Default.DbUserPass = txtServerSifre.Password;

            GlobalSettings.Default.Kaydet();

        }

        private void BeniHatirlaClick(object sender, RoutedEventArgs e)
        {
            GlobalSettings.Default.BeniHatirla = chkBeniHatirla.IsChecked.GetValueOrDefault() == true ? "Evet" : "Hayır";
        }
    }
}