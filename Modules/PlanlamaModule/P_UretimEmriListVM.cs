using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.Model.Satis;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.PlanlamaModule
{
    public class P_UretimEmriListVM : MyDxViewModelBase, IForm
    {
        private ObservableCollection<PlanlamaTakipDto> planlamaDtoListe;
        private string seciliMenu;
        private PlanlamaTakipDto seciliPlanlamaTakipDto;
        private Yerlesim seciliYerlesim;
        private UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<Yerlesim> yerlesimler;
  

        public DelegateCommand KartGosterCommand => new DelegateCommand(OnRotaKartGoster, true);

        public DelegateCommand<object> KapatIslemCommand => new DelegateCommand<object>(OnUretimEmriKapat, c => true);

        public DelegateCommand UretimEmirleriGosterCommand => new DelegateCommand(OnUretimEmirleriGoster, true);

        public DelegateCommand EkraniYenileCommand => new DelegateCommand(OnEkraniYenile, true);

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, true);

        public DelegateCommand<string> KayitlariGosterCommand => new DelegateCommand<string>(OnKayitlariGoster, c => true);

        public string Filtre { get; set; }

        private void OnKayitlariGoster(string obj)
        {
  
             Filtre = obj == "tümü"?"tümü":"açıklar";
             OnEkraniYenile();
          
        }

        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(SeciliYerlesim.XmlDosyaAd);
        }

        public string GridExportDosyaAd => $"UretimEmri_{DateTime.Now.ToString("dd/MM/yyyy_HH_mm")}.xls";


        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, GridExportDosyaAd);
        }

        private void OnEkraniYenile()
        {
            var sadeceAcikOlanlar = Filtre == "tümü" ? true : false;

            uow = new UnitOfWork();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirUretimEmirleriPlanlamaTakipDto(sadeceAcikOlanlar);

            PlanlamaDtoListe.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
        }

        public P_UretimEmriListVM(string formMenuAd)
        {
            FormMenuAd = FormMenuAd;
        }

        public void Load()
        {
            Filtre = "açıklar";

            Yerlesimler = new ObservableCollection<Yerlesim>();

            yerlesimler.Add(new Yerlesim("1", "P_UretimEmirleri-1.xml"));
            yerlesimler.Add(new Yerlesim("2", "P_UretimEmirleri-2.xml"));
            yerlesimler.Add(new Yerlesim("3", "P_UretimEmirleri-3.xml"));

            SeciliYerlesim = yerlesimler.First();

            ExportService.RestoreLayout(SeciliYerlesim.XmlDosyaAd);

            OnEkraniYenile();

        }

        public ObservableCollection<PlanlamaTakipDto> PlanlamaDtoListe
        {
            get => planlamaDtoListe;
            set => SetProperty(ref planlamaDtoListe, value);
        }

        public string SeciliMenu
        {
            get => seciliMenu;
            set => SetProperty(ref seciliMenu, value);
        }

        public PlanlamaTakipDto SeciliPlanlamaTakipDto
        {
            get => seciliPlanlamaTakipDto;
            set
            {
                if (SetProperty(ref seciliPlanlamaTakipDto, value))
                {
                    if (seciliPlanlamaTakipDto == null) return;

                    seciliPlanlamaTakipDto.PropertyChanged -= SeciliPlanlamaTakipDto_PropertyChanged;
                    seciliPlanlamaTakipDto.PropertyChanged += SeciliPlanlamaTakipDto_PropertyChanged;
                }
            }
        }

        public Yerlesim SeciliYerlesim
        {
            get => seciliYerlesim;
            set
            {
                SetProperty(ref seciliYerlesim, value);
                ExportService.RestoreLayout(SeciliYerlesim.XmlDosyaAd);
            }
        }

        public ObservableCollection<Yerlesim> Yerlesimler
        {
            get => yerlesimler;
            set => SetProperty(ref yerlesimler, value);
        }

        public void OnUretimEmriKapat(object obj)
        {
            var kapatabilirMi = AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA
                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI
                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.ADMIN;
            if (kapatabilirMi==false)
            {
                MessageBox.Show("Üretim Emri kapatma yetkiniz bulunmamaktadır", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SeciliPlanlamaTakipDto.UretimEmriKod == "")
            {
                MessageBox.Show("Üretim Emri Kodu Bulunamadı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var mesaj = "Üretimde" + SeciliPlanlamaTakipDto.UretimdeYuruyenMiktar + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

            uow = new UnitOfWork();
            var uretimEmri = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliPlanlamaTakipDto.UretimEmriKod);

            var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel);

            SiparisKalem s = uow.SiparisKalemRepo.SiparisKalemiGetir(SeciliPlanlamaTakipDto.SiparisKalemKod);

            if (mesajSonuc == MessageBoxResult.Yes)
            {
                SeciliPlanlamaTakipDto.KapatildiMi = true;
                //s.PLAN_PlanlanacakKalanMiktarToplam = s.PLAN_PlanlanacakKalanMiktarToplam;
                uretimEmri.KapatildiMi = true;
            }

            if (mesajSonuc == MessageBoxResult.No)
            {
                SeciliPlanlamaTakipDto.KapatildiMi = true;
                uretimEmri.KapatildiMi = true;
                uretimEmri.KapatilmaTarihi = DateTime.Now;
            }

            uow.Commit();
        }

        private void OnRotaKartGoster()
        {
            UretimEmriView _view = new UretimEmriView();

            _view.DataContext = new UretimEmriViewModel(seciliPlanlamaTakipDto.SiparisKalemKod, seciliPlanlamaTakipDto.UretimEmriKod, seciliPlanlamaTakipDto);

            _view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _view.Show();
        }

        private void OnUretimEmirleriGoster()
        {
            uow = new UnitOfWork();
            SeciliYerlesim = Yerlesimler.Where(c => c.Ad == "1").First();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirUretimEmirleriPlanlamaTakipDto();
        }

        private void SeciliPlanlamaTakipDto_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChanged") return;

            if (e.PropertyName == nameof(seciliPlanlamaTakipDto.AmbalajKafesOlcu))
            {
                uow.PlanlamaRepo.KalemKafesOlcuKaydet(seciliPlanlamaTakipDto.SiparisKalemKod, seciliPlanlamaTakipDto.AmbalajKafesOlcu);
                return;
            }

            uow.PlanlamaRepo.UretimEmriBilgileriKaydet(SeciliPlanlamaTakipDto);
        }
    }
}