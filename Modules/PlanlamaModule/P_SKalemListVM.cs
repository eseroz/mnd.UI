using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Uretim;
using mnd.Logic.Model.Satis;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.AppModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.PlanlamaModule
{
    public class P_SKalemListVM : MyDxViewModelBase, IForm
    {
        private ObservableCollection<PlanlamaTakipDto> planlamaDtoListe;
        private PlanlamaTakipDto seciliPlanlamaTakipDto;
        private Yerlesim seciliYerlesim;
        private UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<Yerlesim> yerlesimler;
        private bool sadeceKapatilanlariGoster;

        AnaKartRepository anakartRepo = new AnaKartRepository();

        public ObservableCollection<PlanlamaTakipDto> SeciliPlanlamaTakipDtos { get; set; }

        public DelegateCommand PlanlaCommand => new DelegateCommand(OnPlanla, canPlanla);

        public DelegateCommand KalemiAcCommand => new DelegateCommand(OnKalemiAc, canKalemAc);

        public DelegateCommand<UretimEmri> UretimEmriSilCommand => new DelegateCommand<UretimEmri>(OnUretimEmriSil, CanUretimEmriSil);

        public DelegateCommand<object> HesapDetayCommand => new DelegateCommand<object>(OnHesapDetay);

        private void OnHesapDetay(object obj)
        {
            P_SKalemListView_IW f = new P_SKalemListView_IW();
            f.Show();
        }

        private bool CanUretimEmriSil(UretimEmri uretimEmri)
        {

            if (uretimEmri == null) return false;

            var onay = uretimEmri.Uretim_PaketlenenMiktar == 0 && uretimEmri.KapatildiMi.GetValueOrDefault() == false;
            return onay;
        }

        private void OnUretimEmriSil(UretimEmri obj)
        {
            var cev=MessageBox.Show("Üretim emri silinecek onaylıyormusunuz", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (cev == MessageBoxResult.Cancel) return;

            var sipKalem=PlanlamaDtoListe.First(canKalemAc => canKalemAc.SiparisKalemKod == obj.SiparisKalemKod);

            sipKalem.UretimEmirleri.Remove(obj);

            uow.PlanlamaRepo.UretimEmriSil(obj.UretimEmriKod);

        }


      

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoad);

        private void OnFormLoad()
        {
            FormLoaded = true;
        }

        public bool SadeceKapatilanlariGoster
        {
            get => sadeceKapatilanlariGoster;

            set
            {
                SetProperty(ref sadeceKapatilanlariGoster, value);

                if (FormLoaded) OnSiparisKalemleriGoster(sadeceKapatilanlariGoster);

            }
        }


        private bool canKalemAc()
        {
            if (SeciliPlanlamaTakipDto == null) return false;

            return SeciliPlanlamaTakipDto.KapatildiMi &&
                SeciliPlanlamaTakipDto.SiparisDurum == SIPARISSURECDURUM.MUSTERIONAYLI
                && AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;

        }

        private void OnKalemiAc()
        {
            uow = new UnitOfWork();
            uow.PlanlamaRepo.KalemiAc(SeciliPlanlamaTakipDto.SiparisKalemKod);
            SeciliPlanlamaTakipDto.KapatildiMi = false;
        }

        public DelegateCommand KartGosterCommand => new DelegateCommand(OnRotaKartGoster, true);


        [YetkiKontrol]
        public DelegateCommand<object> KapatIslemCommand => new DelegateCommand<object>(OnKalemKapat, c => YetkiliMi_FromDb(nameof(KapatIslemCommand)));


   
        public DelegateCommand<bool> SiparisKalemleriGosterCommand => new DelegateCommand<bool>(OnSiparisKalemleriGoster, true);


        public DelegateCommand YardimCommand => new DelegateCommand(OnYardimGoster, true);

        public DelegateCommand EkraniYenileCommand => new DelegateCommand(OnEkraniYenile, true);

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);

        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(SeciliYerlesim.XmlDosyaAd);
        }


        public string GridExportDosyaAd => $"Planlama_kalem_{DateTime.Now.ToString("dd/MM/yyyy_HH_mm")}.xls";

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, GridExportDosyaAd);
        }

        private void OnEkraniYenile()
        {
            uow = new UnitOfWork();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirKalemPlanlamaTakip_ReelDto();

            var kapasitifListe = uow.PlanlamaRepo.GetirKalemPlanlamaTakip_KapasitifDto();

            foreach (var item in kapasitifListe)
            {
                PlanlamaDtoListe.Add(item);
            }

            PlanlamaDtoListe.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

        }

        private void OnYardimGoster()
        {
        }

        public P_SKalemListVM(string menuAd)
        {
            FormMenuAd = menuAd;

            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);
        }

        public void Load()
        {
            Yerlesimler = new ObservableCollection<Yerlesim>();

            yerlesimler.Add(new Yerlesim("1", "P_SiparisKalemList1-1.xml"));
            yerlesimler.Add(new Yerlesim("2", "P_SiparisKalemList1-2.xml"));
            yerlesimler.Add(new Yerlesim("3", "P_SiparisKalemList1-3.xml"));

            SeciliYerlesim = yerlesimler.First();

            ExportService.RestoreLayout(SeciliYerlesim.XmlDosyaAd);

            OnEkraniYenile();

          
        }

        public ObservableCollection<PlanlamaTakipDto> PlanlamaDtoListe
        {
            get => planlamaDtoListe;
            set => SetProperty(ref planlamaDtoListe, value);
        }

        public PlanlamaTakipDto SeciliPlanlamaTakipDto
        {
            get => seciliPlanlamaTakipDto;
            set
            {
                var oldValue = seciliPlanlamaTakipDto;
                if (SetProperty(ref seciliPlanlamaTakipDto, value))
                {
                    if (oldValue != null) oldValue.PropertyChanged -= SeciliPlanlamaTakipDto_PropertyChanged;

                    if (value != null) seciliPlanlamaTakipDto.PropertyChanged += SeciliPlanlamaTakipDto_PropertyChanged;

                }
            }
        }

        public Yerlesim SeciliYerlesim
        {
            get => seciliYerlesim;
            set
            {
                if (SetProperty(ref seciliYerlesim, value))
                {
                    ExportService.RestoreLayout(SeciliYerlesim.XmlDosyaAd);
                }
            }
        }

        public ObservableCollection<Yerlesim> Yerlesimler
        {
            get => yerlesimler;
            set => SetProperty(ref yerlesimler, value);
        }
        public bool FormLoaded { get; private set; }

        public void OnKalemKapat(object obj)
        {
            var mesaj = "Üretimde" + SeciliPlanlamaTakipDto.UretimdeYuruyenMiktar + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

            var uow1 = new UnitOfWork();
            var uretimEmri = uow1.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliPlanlamaTakipDto.UretimEmriKod);

            var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel);

            SiparisKalem seciliKalem = uow1.SiparisKalemRepo.SiparisKalemiGetir(SeciliPlanlamaTakipDto.SiparisKalemKod);

            if (mesajSonuc == MessageBoxResult.Yes)
            {
                //seciliKalem.PLAN_PlanlanacakKalanMiktarToplam = seciliKalem.PLAN_PlanlanacakKalanMiktarToplam;

                //seciliKalem.UretimEmirleri.ForEach(c => { c.KapatildiMi = true; });
                uow1.Commit();

                SeciliPlanlamaTakipDto.KapatildiMi = true;
            }

            if (mesajSonuc == MessageBoxResult.No)
            {
                SeciliPlanlamaTakipDto.KapatildiMi = true;

                //seciliKalem.PLAN_KalemKapatildiMi = true;
                //seciliKalem.PLAN_KalemKapatilmaTarihi = DateTime.Now;
                //seciliKalem.UretimEmirleri.ForEach(c => { c.KapatildiMi = true; });

                uow1.Commit();
            }

            uow1.Dispose();
        }

        private bool canPlanla()
        {
            if (SeciliPlanlamaTakipDto == null) return false;

            return (!SeciliPlanlamaTakipDto.KapatildiMi) && SeciliPlanlamaTakipDto.SiparisDurum == SIPARISSURECDURUM.MUSTERIONAYLI
                && SeciliPlanlamaTakipDto.KapasitifDurum == "Reel";
        }

        private void OnRotaKartGoster()
        {
            UretimEmriView _view = new UretimEmriView();

            _view.DataContext = new UretimEmriViewModel(seciliPlanlamaTakipDto.SiparisKalemKod, seciliPlanlamaTakipDto.UretimEmriKod, SeciliPlanlamaTakipDto);

            _view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _view.Show();
        }

        private void OnPlanla()
        {
            string kartNo = "";
        

            UretimEmriView _view = new UretimEmriView();

            var msg = MessageBox.Show("Yeni Anakart Oluşturulsun Mu? " + Environment.NewLine +
                "(Hayır derseniz son anakarta ek kart oluşturulacaktır)"
                , "Pandap", MessageBoxButton.YesNo);

            if(msg==MessageBoxResult.Yes)
            {
                var sonAnakartSayi = anakartRepo.SonAnakartNoGetir();

                if (int.Parse(sonAnakartSayi) < int.Parse((DateTime.Now).ToString("yy") + "0000"))
                {
                    sonAnakartSayi = (DateTime.Now).ToString("yy") + "0000";
                }


                var sonAnakartNo = (int.Parse(sonAnakartSayi) + 1).ToString();

                kartNo = sonAnakartNo + "/01";

                SeciliPlanlamaTakipDto.AnaKartNo = sonAnakartNo;
                SeciliPlanlamaTakipDto.KartNo = kartNo;
            }
            else
            {
                var sonAnakartNo = (int.Parse(anakartRepo.SonAnakartNoGetir())).ToString();

                var anakartaAit_SonKartNo = anakartRepo.SonUretimEmriKartNoGetir(sonAnakartNo);


                var kartNoSag = (int.Parse(anakartaAit_SonKartNo.Split('/')[1]) + 1).ToString().PadLeft(2, '0');
                kartNo = sonAnakartNo + "/" + kartNoSag;

                SeciliPlanlamaTakipDto.AnaKartNo = sonAnakartNo;
                SeciliPlanlamaTakipDto.KartNo = kartNo;
            }
          
            var vm= new UretimEmriViewModel(seciliPlanlamaTakipDto.SiparisKalemKod, null, SeciliPlanlamaTakipDto);
            _view.DataContext = vm;

            _view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _view.Show();
        }

        private void OnSiparisKalemleriGoster(bool sadeceKapalilarMi = false)
        {
            uow = new UnitOfWork();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirKalemPlanlamaTakip_ReelDto(sadeceKapalilarMi);
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