using System.Data.SqlClient;
using System.Deployment.Application;
using System.Windows;
using Pandap.Logic.Persistence;
using Pandap.Logic.Properties;

namespace Pandap.UI.AppModule._Login
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            SqlConnectionStringBuilder builder;

            var path = Pandap.Logic.Properties.Settings.Default["SqlPath"].ToString();
            txtKullanici.Text = Pandap.Logic.Properties.Settings.Default["KullaniciAdi"].ToString();

            if (path != string.Empty && !path.Contains("x0"))
            {
                builder = new SqlConnectionStringBuilder(path);
                txtSqlPath.Text = builder.DataSource;
                txtServerSaŞifre.Password = builder.Password;
            }

            this.Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                this.Title = "Pandap " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                this.Title = "Pandap";
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UnitOfWork uow = new UnitOfWork();

            var yol = "server=x0;database=PANDAPDB;user id=sa;password=x1;MultipleActiveResultSets=true;App=Pandap";
            Settings.Default["SqlPath"] = yol.Replace("x0", txtSqlPath.Text).Replace("x1", txtServerSaŞifre.Password);

            Settings.Default.Save();

            var user = uow.KullaniciRepo.KullaniciGetir(txtKullanici.Text, txtParola.Password);

            if (user != null)
            {
                PandapGlobal.AktifKullanici = user;

                if (user.BagliKullanicilar == "Tümü")
                {
                    user.BagliKullanicilar = uow.KullaniciRepo.TumBagliKullanicilariGetir();
                }

                PandapGlobal.KullaniciRol = user.KullaniciRol;

                Settings.Default["KullaniciAdi"] = txtKullanici.Text;
                Settings.Default.Save();

                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hatalı giriş");
            }
        }
    }
}