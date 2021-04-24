using DevExpress.Mvvm;
using Newtonsoft.Json;
using mnd.Common.Helpers;
using mnd.Logic.BC_Satis._Sikayet;
using mnd.Logic.BC_Satis._Sikayet.DataServices;
using mnd.Logic.Model;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace mnd.UI.Modules._SatisModule.Sikayetler
{
    public class TeklifDurum : MyBindableBase
    {
        public static FiltreItemModel Taslak = new FiltreItemModel { ItemNameMy = nameof(Taslak), BadgeValue = 0 };
        public static FiltreItemModel Kalitede = new FiltreItemModel { ItemNameMy = nameof(Kalitede), BadgeValue = 0 };
        public static FiltreItemModel Satista = new FiltreItemModel { ItemNameMy = nameof(Satista), BadgeValue = 0 };

        public static FiltreItemModel Arsiv = new FiltreItemModel { ItemNameMy = nameof(Arsiv), BadgeValue = 0 };

        private FiltreItemModel seciliMenu;

        public List<FiltreItemModel> Liste => new List<FiltreItemModel> { Taslak, Kalitede, Satista, Arsiv };

        public FiltreItemModel SeciliMenu { get => seciliMenu; set => SetProperty(ref seciliMenu, value); }

    }


    public class SikayetListVM : MyDxViewModelBase, IDocumentViewModel
    {
        private ObservableCollection<Sikayet> sikayetler;
        public TeklifDurum TeklifInfo { get => teklifInfo; set => SetProperty(ref teklifInfo, value); }

        public DelegateCommand<Sikayet> DuzenleCommand => new DelegateCommand<Sikayet>(OnDuzenle, c => true);

        public DelegateCommand<Sikayet> KaliteyeGonderCommand => new DelegateCommand<Sikayet>(OnKaliteyeGonder);

        public DelegateCommand<Sikayet> KaliteFormGirCommand => new DelegateCommand<Sikayet>(OnKaliteFormGir, canFormGiris);

        public DelegateCommand EkraniYenileCommand => new DelegateCommand(OnEkraniYenile);

        private void OnEkraniYenile()
        {
            Sikayetler = repo.SikayetleriGetir();
        }

        private bool canFormGiris(Sikayet arg)
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.KALITE;
        }

        private void OnKaliteFormGir(Sikayet seciliSikayet)
        {
            var json = JsonConvert.SerializeObject(seciliSikayet);
            var kopyaSikayet = JsonConvert.DeserializeObject<Sikayet>(json);


            KaliteFormGirWindow f = new KaliteFormGirWindow();
            KaliteFormGirisVM vm = new KaliteFormGirisVM(kopyaSikayet);

            f.DataContext = vm;

            var cev = f.ShowDialog();

            if (cev.GetValueOrDefault() == true)
            {
                var servis = new SikayetDataService();
                servis.KaliteDataGuncelle(kopyaSikayet.Id, kopyaSikayet.IadeMiktari, kopyaSikayet.DuzeltmeOnlemeFaliyetNo, kopyaSikayet.KonuDetay);

                seciliSikayet.DuzeltmeOnlemeFaliyetNo = kopyaSikayet.DuzeltmeOnlemeFaliyetNo;
                seciliSikayet.IadeMiktari = kopyaSikayet.IadeMiktari;
                seciliSikayet.KonuDetay = kopyaSikayet.KonuDetay;

            }
        }

        private void OnKaliteyeGonder(Sikayet obj)
        {
            obj.SurecKonum = "Kalitede";

            var sikayet = repo.SikayetGetir(obj.Id);
            sikayet.SurecKonum = "Kalitede";
            repo.Kaydet();
        }

        public DelegateCommand<Sikayet> SatisaGonderCommand => new DelegateCommand<Sikayet>(OnSatisaGonder);

        public DelegateCommand<Sikayet> ArsiveGonderCommand => new DelegateCommand<Sikayet>(OnArsiveGonder);

        private void OnArsiveGonder(Sikayet obj)
        {
            obj.SurecKonum = "Arşiv";

            var sikayet = repo.SikayetGetir(obj.Id);
            sikayet.SurecKonum = "Arşiv";
            repo.Kaydet();
        }

        private void OnSatisaGonder(Sikayet obj)
        {
            obj.SurecKonum = "Satışta";

            var sikayet = repo.SikayetGetir(obj.Id);
            sikayet.SurecKonum = "Satışta";
            repo.Kaydet();
        }

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);
        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnLoad);

        public DelegateCommand YeniCommand => new DelegateCommand(OnYeniSikayet);


        SikayetRepository repo = new SikayetRepository();
        private void OnYeniSikayet()
        {
            var vm = new SikayetViewModel();
            vm.Load(0);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SikayetView", vm);
            doc.Title = "Yeni Şikayet";
            doc.DestroyOnClose = true;
            doc.Show();
        }

        private void OnLoad()
        {
            ExportService1.RestoreLayout("Sikayetler.xml");

            Messenger.Default.Register<SikayetEklendiEvent>(this, OnSikayetEklendi);
        }

        private void OnSikayetEklendi(SikayetEklendiEvent obj)
        {

            OnEkraniYenile();
        }

        private void OnYerlesimKaydet(object obj)
        {
            ExportService1.SaveLayout("Sikayetler.xml");
        }
        private void OnDuzenle(Sikayet obj)
        {
            var vm = new SikayetViewModel();

            vm.Load(obj.Id);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SikayetView", vm);
            doc.DestroyOnClose = true;
            doc.Title = obj.Id;
            doc.Show();
        }
        public ObservableCollection<Sikayet> Sikayetler { get => sikayetler; set => SetProperty(ref sikayetler, value); }

        private TeklifDurum teklifInfo;

        public SikayetListVM(string menuFormAd)
        {
            FormMenuAd = menuFormAd;
            SikayetRepository repo = new SikayetRepository();
            Sikayetler = repo.SikayetleriGetir();

            TeklifInfo = new TeklifDurum();

            TeklifInfo.SeciliMenu = TeklifDurum.Satista;

            Sikayetler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

        }

        public SikayetListVM()
        {

            Sikayetler = repo.SikayetleriGetir();

            Sikayetler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

        }

        public string FormMenuAd { get; }

        public object Title => "Şikayetler";

        public bool Close()
        {
            return true;
        }
    }
}
