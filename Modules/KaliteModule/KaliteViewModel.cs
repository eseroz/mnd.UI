using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;

namespace mnd.UI.Modules.KaliteModule
{
    public class KaliteViewModel : MyDxViewModelBase
    {
        private UnitOfWork uow = new UnitOfWork();

        private UretimEmri seciliUretimEmri;

        public UretimEmri SeciliUretimEmri
        {
            get { return seciliUretimEmri; }
            set { SetProperty(ref seciliUretimEmri, value); }
        }

        public DelegateCommand<UretimEmri> SertifikaYazdirCommand => new DelegateCommand<UretimEmri>(SertifikaYazdir);

        public KaliteViewModel(string menuFormAd)
        {
            FormMenuAd = menuFormAd;
            KapaliEmirlerGorunsunMu = false;
        }


        public bool KapaliEmirlerGorunsunMu
        {
            get => _kapalilarGorunsunMu; set
            {
                SetProperty(ref _kapalilarGorunsunMu, value);
                Load();
            }
        }

        public void Load()
        {
            uow = new UnitOfWork();

            UretimEmirleri = uow.PlanlamaRepo.KaliteUretimEmirleriGetir(KapaliEmirlerGorunsunMu);

        }

        private bool CanPandaStogaCevir(Bobin arg)
        {
            var _bobin = arg as Bobin;

            if (_bobin == null) return false;

            return _bobin.CariKod_Onceki == null;
        }

        private void PandaStogaCevir(Bobin bobin)
        {
            MessageBoxResult cevap =
            MessageBox.Show("Panda Stoğa Aktarılıyor. Onaylıyor musunuz", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (cevap == MessageBoxResult.OK)
            {
                bobin.CariKod_Onceki = bobin.CariKod;
                bobin.CariKod = AppPandap.PANDACARI;

                uow.Commit();
            }
        }

        public void SertifikaYazdir(UretimEmri uretimEmri)
        {
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(8);

            var dsObject = uretimEmri.ToKaliteSertifikaDto(); ;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        public DelegateCommand<Bobin> PandapMessangerAcCommand => new DelegateCommand<Bobin>(PandapMessangerAc);

        private void PandapMessangerAc(Bobin row)
        {
            AppMesaj.MesajFormAc(row);
        }

        private bool isOpenBobinDialog;

        public bool IsOpenBobinDialog
        {
            get => isOpenBobinDialog;
            set => SetProperty(ref isOpenBobinDialog, value);
        }

        private ObservableCollection<UretimEmri> uretimEmirleri;

        public ObservableCollection<UretimEmri> UretimEmirleri
        {
            get => uretimEmirleri;
            set => SetProperty(ref uretimEmirleri, value);
        }

        public DelegateCommand<object> OpenBobinDialogCommand => new DelegateCommand<object>(OnOpenBobinDialog, CanOpenBobinDialog);

        private bool CanOpenBobinDialog(object arg)
        {
            return SeciliUretimEmri != null;
        }

        public DelegateCommand<string> CloseBobinDialogCommand => new DelegateCommand<string>(OnCloseBobinDialog, c => true);

        public DelegateCommand<object> BobinSilCommand => new DelegateCommand<object>(OnBobinSil, CanBobinSil);

        private bool CanBobinSil(object arg)
        {
            var _bobin = arg as Bobin;

            if (_bobin == null) return false;

            return _bobin.PaletId == null;
        }

        private Bobin tempBobin;

        public Bobin TempBobin { get => tempBobin; set => SetProperty(ref tempBobin, value); }

        private KaliteBobinViewModel kaliteBobinViewModel;
        private bool _kapalilarGorunsunMu;

        private void OnOpenBobinDialog(object obj)
        {
            var urunData = SeciliUretimEmri.SiparisKalemKodNav;

            if (SeciliUretimEmri.KaliteStandartlari_Json == null)
            {
                //var kaliteAralik = uow.KaliteRepo.VarsayilanKaliteAralikGetir(urunData.KullanimAlanTipKod, urunData.AlasimTipKod,
                //    urunData.SertlikTipKod, urunData.Kalinlik_micron);

                if (true)
                {
                    MessageBox.Show("Bu ürün için aralıklar tanımlı değil");
                    return;
                }

                //SeciliUretimEmri.KaliteSinirlari = kaliteAralik;

            }
            else
            {
                SeciliUretimEmri.KaliteSinirlari = JsonConvert.DeserializeObject<KaliteStandart>(SeciliUretimEmri.KaliteStandartlari_Json);
            }

            kaliteBobinViewModel = new KaliteBobinViewModel();
            kaliteBobinViewModel.AktifUretimEmri = SeciliUretimEmri;

            kaliteBobinViewModel.KapatCommand = CloseBobinDialogCommand;

            KaliteBobinWindow kaliteBobinWindow = new KaliteBobinWindow();

            kaliteBobinWindow.DataContext = kaliteBobinViewModel;

            kaliteBobinWindow.Show();
        }

        private void OnCloseBobinDialog(string obj)
        {
            kaliteBobinViewModel.AktifUretimEmri.KaliteStandartlari_Json = JsonConvert.SerializeObject(kaliteBobinViewModel.AktifUretimEmri.KaliteSinirlari);

            foreach (var bobin in kaliteBobinViewModel.AktifUretimEmri.UretimBobinler)
            {
                bobin.Ekleyen = AppPandap.AktifKullanici.KullaniciId;
            }  


            uow.Commit();
        }

        private void OnBobinSil(object obj)
        {
            var bobin = (Bobin)obj;
            SeciliUretimEmri.UretimBobinler.Remove(bobin);

            uow.Commit();
        }
    }
}