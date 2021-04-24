using DevExpress.Mvvm;
using mnd.Logic.Model;
using mnd.UI.Helper;
using mnd.UI.Modules.TeklifModule.MessangerEvents;
using mnd.UI.Modules.TeklifModule.Models;
using mnd.UI.Modules.TeklifModule.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using mnd.UI.GyModules.MesajModule;

namespace mnd.UI.Modules.TeklifModule
{
    public class TeklifDurum : MyBindableBase
    {
        public static FiltreItemModel Bekliyor = new FiltreItemModel { ItemNameMy = nameof(Bekliyor), BadgeValue = 0 };
        public static FiltreItemModel Onaylandi = new FiltreItemModel { ItemNameMy = nameof(Onaylandi), BadgeValue = 0 };
        public static FiltreItemModel Reddedildi = new FiltreItemModel { ItemNameMy = nameof(Reddedildi), BadgeValue = 0 };
        public static FiltreItemModel SuresiBitti = new FiltreItemModel { ItemNameMy = nameof(SuresiBitti), BadgeValue = 0 };
        private FiltreItemModel seciliMenu;

        public List<FiltreItemModel> Liste => new List<FiltreItemModel> { Bekliyor, Onaylandi, Reddedildi, SuresiBitti };

        public FiltreItemModel SeciliMenu { get => seciliMenu; set => SetProperty(ref seciliMenu,value); }

   
    }


    public class TeklifListViewModel : MyDxViewModelBase, IForm
    {
        #region Delegates
        public DelegateCommand<object> YeniCommand => new DelegateCommand<object>(OnYeni, c => true);
        public DelegateCommand<TeklifListModel> DuzenleCommand => new DelegateCommand<TeklifListModel>(OnDuzenle, c => true);
        public DelegateCommand<FiltreItemModel> SorguSecCommand => new DelegateCommand<FiltreItemModel>(OnFiltreSorgula, true);
        public DelegateCommand OnaylandiCommand => new DelegateCommand(Onaylandi, true);
        public TeklifListModel SeciliTeklifListModel { get; set; }
        public TeklifIslemVM TeklifIslemViewModel { get => teklifIslemViewModel; set => SetProperty(ref teklifIslemViewModel, value); }
        public bool IsOpenTeklifIslemForm { get => isOpenTeklifIslemForm; set => SetProperty(ref isOpenTeklifIslemForm, value); }
        public DelegateCommand TeklifIslemFormOpenCommand => new DelegateCommand(OpenTeklifIslemForm);
        public DelegateCommand<string> TeklifIslemCevapCommand => new DelegateCommand<string>(TeklifIslemCevap);
        public DelegateCommand BeklemeyeAlCommand => new DelegateCommand(BeklemeyeAl);
        public DelegateCommand EkranTazeleCommand => new DelegateCommand(EkranTazele);
        public TeklifDurum TeklifInfo { get => teklifInfo; set => SetProperty(ref teklifInfo, value); }
        public ObservableCollection<TeklifListModel> Teklifler { get => teklifler; set => SetProperty(ref teklifler, value); }
        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");
        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);

        #endregion

        private void OnYerlesimKaydet(object obj)
        {
            ExportService1.SaveLayout("Teklifler.xml");
        }

        TeklifDataService service = new TeklifDataService();
        private bool isOpenTeklifIslemForm;
        private TeklifIslemVM teklifIslemViewModel;
        private ObservableCollection<TeklifListModel> teklifler;
        private TeklifDurum teklifInfo;


        public TeklifListViewModel(string formMenuAd)
        {
            TeklifInfo = new TeklifDurum();

            TeklifInfo.SeciliMenu = TeklifDurum.Bekliyor;

            EkraniYenile();

            Messenger.Default.Register<TeklifEklendiEvent>(this, TeklifEklendi);
        }

        private void EkranTazele()
        {
            EkraniYenile();
        }


        private void SurecGrupSayilariGuncelle()
        {
            TeklifDataService service = new TeklifDataService();

            var bagliPlasiyerKodListe = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');

            var teklifSiraKod = "Tümü";

            Dictionary<string, int> surecSayilari = service.TeklifSurecGrupSayilariGetir(bagliPlasiyerKodListe);

            TeklifInfo.Liste[0].BadgeValue = surecSayilari.GetValueOrDefault(TeklifDurum.Bekliyor.ItemNameMy);
            TeklifInfo.Liste[1].BadgeValue = surecSayilari.GetValueOrDefault(TeklifDurum.Onaylandi.ItemNameMy);
            TeklifInfo.Liste[2].BadgeValue = surecSayilari.GetValueOrDefault(TeklifDurum.Reddedildi.ItemNameMy);
            TeklifInfo.Liste[3].BadgeValue = surecSayilari.GetValueOrDefault(TeklifDurum.SuresiBitti.ItemNameMy);

        }


        private void EkraniYenile()
        {
            var bagliPlasiyerKodListe = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');
            var teklifDurum = TeklifInfo.SeciliMenu.ItemNameMy;
            var teklifSiraKod = "Tümü";

            Teklifler = service.TeklifleriGetir(AppPandap.AktifKullanici.PlasiyerKod, bagliPlasiyerKodListe, teklifDurum, 
                teklifSiraKod,AppPandap.AktifKullanici.KullaniciId);

            SurecGrupSayilariGuncelle();

            Teklifler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);


        }

        private void Onaylandi()
        {
         
            SeciliTeklifListModel.TeklifDurum = "Onaylandi";        

            TeklifDataService servis = new TeklifDataService();
            service.TeklifDurumDegistir(SeciliTeklifListModel.TeklifSiraKod, SeciliTeklifListModel.TeklifDurum,"","");
            EkraniYenile();
        }


        private void BeklemeyeAl()
        {
            SeciliTeklifListModel.TeklifDurum = "Bekliyor";
            TeklifDataService servis = new TeklifDataService();
            service.TeklifDurumDegistir(SeciliTeklifListModel.TeklifSiraKod, SeciliTeklifListModel.TeklifDurum,"","");

            EkraniYenile();
        }

        private void OpenTeklifIslemForm()
        {
            TeklifIslemViewModel = new TeklifIslemVM();
            IsOpenTeklifIslemForm = true;

        }

        private void TeklifIslemCevap(string cevap)
        {
            IsOpenTeklifIslemForm = false;

            if (cevap == "OK")
            {

                SeciliTeklifListModel.TeklifDurum = "Reddedildi";
                SeciliTeklifListModel.RetNeden = TeklifIslemViewModel.RetNeden;
                SeciliTeklifListModel.IslemNot = TeklifIslemViewModel.IslemAciklama;

                TeklifDataService servis = new TeklifDataService();
                service.TeklifDurumDegistir(SeciliTeklifListModel.TeklifSiraKod,
                                            SeciliTeklifListModel.TeklifDurum,
                                            SeciliTeklifListModel.IslemNot,
                                            TeklifIslemViewModel.RetNeden
                                          );

                EkraniYenile();

            }

            TeklifIslemViewModel = new TeklifIslemVM();
        }

        private void OnFiltreSorgula(FiltreItemModel o)
        {
            EkraniYenile();
        }
        
        private void OnYeni(object obj)
        {
            var vm = new TeklifViewModel();

            vm.Load("Yeni");

            var doc = AppPandap.pDocumentManagerService.CreateDocument("TeklifView", vm);
            doc.DestroyOnClose = true;
            doc.Title = "Yeni";
            doc.Show();
        }

        private void OnDuzenle(TeklifListModel obj)
        {
            var vm = new TeklifViewModel();

            vm.Load(obj.TeklifSiraKod);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("TeklifView", vm);
            doc.DestroyOnClose = true;
            doc.Title = obj.TeklifSiraKod;
            doc.Show();
        }

        private void TeklifEklendi(TeklifEklendiEvent obj)
        {
            TeklifListModel yeni = new TeklifListModel();

            yeni.TeklifSiraKod = obj.Teklif.TeklifSiraKod;
            yeni.CariAd = obj.Teklif.CariAd;
            yeni.IletisimPersonelAdSoyad = obj.Teklif.IletisimKisiAdSoyad;
            yeni.MiktarOlcuBirim = obj.Teklif.MiktarOlcuBirim;



            Teklifler.Add(yeni);
        }

        public void Load()
        {
            ExportService1.RestoreLayout("Teklifler.xml");
        }
    }
}
