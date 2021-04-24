using DevExpress.Mvvm;
using mnd.Common;
using mnd.Logic.Model;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Linq;
using System.Windows;

namespace mnd.UI
{
    public class AppPandap : MyBindableBase
    {
        public const string PANDACARI = "120-01-01-243";

        public static bool LoginAktifMi
        {
            get; set;
        }


        public static string NavSearchFilter { get; set; }

        public static bool YoneticiMi { get; set; } = false;

        public static string KullaniciRol { get; set; }

        public static Kullanici AktifKullanici { get; set; }

        public static string WebApiNetsisPath = $"http://{GlobalSettings.Default.Sunucu}:8081/";
        //public static string WebApiPath = $"http://localhost:57541/";

        internal static IDocumentManagerService pDocumentManagerService;

        public static Ayarlar UygulamaAyarlar { get; set; }

        public static string AppTitle { get; set; }

        public static Window MainWindow { get; set; }

        public static string GetAppTitle()
        {
            string title = "Mnd CRM ver: ";

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                title += ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }

            title += " Sunucu : " + GlobalSettings.Default.Sunucu;

            AppTitle = title;
            return title;
        }

        public static bool RaporTasarimModuAktifMi { get; set; }

        public static void SetDefaultUser(string kullanici, string parola)
        {
            UnitOfWork uow = new UnitOfWork();

            var user = uow.KullaniciRepo.KullaniciGetir(kullanici, parola);
            user.BagliKullanicilar = user.BagliKullanicilar == "Tümü" ? uow.KullaniciRepo.TumBagliKullanicilariGetir() : user.BagliKullanicilar;

            AppPandap.AktifKullanici = user;
            AppPandap.KullaniciRol = user.KullaniciRol;
        }


        public static IDocument DokumanOlustur(string documentType, object vm, string title)
        {
            var doc = pDocumentManagerService.Documents.Where(c => c.Title.ToString() == title).FirstOrDefault();

            if (doc != null)
            {
                doc.DestroyOnClose = true;
                return doc;
            }

            var docYeni = pDocumentManagerService.CreateDocument(documentType, vm);
            docYeni.Title = title;
            docYeni.DestroyOnClose = true;


            return docYeni;
        }

        public static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { };

        public static void OnGlobalPropertyChanged(string propertyName)
        {
            GlobalPropertyChanged(
                typeof(AppPandap),
                new PropertyChangedEventArgs(propertyName));
        }
    }
}