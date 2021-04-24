using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.XtraReports.UI;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Model.Uretim;
using Pandap.Logic.Model._DTOs;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._Planlama;
using Pandap.UI.AppModule._RaporDesigner;
using Pandap.UI.Helper;

namespace Pandap.UI.AppModule._Paketleme
{
    public class PaketlemeViewModel : MyDxViewModelBase
    {
        private UnitOfWork uow = new UnitOfWork();

        private UretimEmri seciliUretimEmri;

        public UretimEmri SeciliUretimEmri
        {
            get { return seciliUretimEmri; }
            set { SetProperty(ref seciliUretimEmri, value); }
        }

        private Palet depoSeciliPalet;

        public Palet DepoSeciliPalet
        {
            get => depoSeciliPalet;
            set => SetProperty(ref depoSeciliPalet, value);
        }

        private Bobin seciliBobin;

        public Bobin SeciliBobin
        {
            get { return seciliBobin; }
            set
            {
                if (SetProperty(ref seciliBobin, value) == true)
                {
                    var aktif = value;

                    if (aktif == null) return;

                    aktif.PropertyChanged += SeciliBobin_PropertyChanged;
                }
            }
        }

        private Palet seciliPalet;

        public Palet SeciliPalet
        {
            get { return seciliPalet; }
            set
            {
                SetProperty(ref seciliPalet, value);

                if (seciliPalet == null)
                    CariyeAitKullanilabilirBobinler = uow.PlanlamaRepo.CariyeAitKullanilabilirBobinleriGetir(string.Empty);
                else
                    CariyeAitKullanilabilirBobinler = uow.PlanlamaRepo.CariyeAitKullanilabilirBobinleriGetir(seciliPalet.CariKod);
            }
        }

        private bool isOpenBobinDialog;

        public bool IsOpenBobinDialog
        {
            get => isOpenBobinDialog;
            set => SetProperty(ref isOpenBobinDialog, value);
        }

        private bool isOpenPaletDialog;

        public bool IsOpenPaletDialog
        {
            get => isOpenPaletDialog;
            set => SetProperty(ref isOpenPaletDialog, value);
        }

        public ObservableCollection<UretimEmri> UretimEmirleri { get; set; }

        public DelegateCommand<object> KalemIslemCommand => new DelegateCommand<object>(onKalemIslem, c => true);
        public DelegateCommand<object> KapatIslemCommand => new DelegateCommand<object>(onUretimEmriKapat, c => true);

        public DelegateCommand<object> OpenPaletDialogCommand => new DelegateCommand<object>(OpenPaletDialog, c => true);
        public DelegateCommand<string> ClosePaletDialogCommand => new DelegateCommand<string>(ClosePaletDialog, c => true);

        public DelegateCommand<object> BobiniPaleteAktarCommand => new DelegateCommand<object>(OnBobiniPaleteAktar, c => true);

        public DelegateCommand<object> PaletBobinSilCommand => new DelegateCommand<object>(OnPaletBobinSil, c => true);

        public DelegateCommand<object> DepoyaGonderCommand => new DelegateCommand<object>(OnDepoyaGonder, c => true);

        public DelegateCommand<object> PaletBarkodYazdirCommand => new DelegateCommand<object>(OnPaletBarkodYazdir, c => true);

        public DelegateCommand<object> PaletBarkodDesignerAcCommand => new DelegateCommand<object>(PaletBarkodDesignerAc, c => true);


        public DelegateCommand<object> PaletSilCommand => new DelegateCommand<object>(OnPaletSil, c => true);

        public DelegateCommand<object> DepoOnayindanbGeriAlCommand => new DelegateCommand<object>(OnDepoOnayindanbGeriAl, c => true);

        private async void OnDepoOnayindanbGeriAl(object obj)
        {
            var _palet = (Palet)obj;
            _palet.DepoyaAktarilmaTarihi = null;

            uow.PlanlamaRepo.PaletKaydet(_palet);

            uow.Commit();

            DepodakiPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetir();

            Paletler = uow.PlanlamaRepo.PaletleriGetir();
        }

        private async void OnPaletSil(object obj)
        {
            var _palet = (Palet)obj;

            uow.PlanlamaRepo.PaletSil(_palet);

            Paletler.Remove(_palet);

            uow.Commit();

          
           
        }

        private void OnPaletBarkodYazdir(object obj)
        {
            var _palet = (Palet)obj;

            if (_palet.Bobinler.Count == 0)
            {
                MessageBox.Show("Önce palete bobin ekleyiniz");
                return;
            }

            _palet.UretimEmriKodNav.SiparisKalemKodNav =
                uow.SiparisKalemRepo.SiparisKalemiGetir(_palet.UretimEmriKodNav.SiparisKalemKod);


            PaletBarkodViewerAc(_palet);

        }

        public void PaletBarkodViewerAc(Palet _palet)
        {
            var reportPath = @"Content\PaletBarkod.repx";

            var fs = new FileStream(reportPath, FileMode.Open);
            MemoryStream ms = new MemoryStream();

            fs.CopyTo(ms);

            XtraReport v = XtraReport.FromStream(ms, true);

            var ds = v.DataSource as ObjectDataSource;

            if (ds != null)
                ds.DataSource = BarkodPaletDtoFromSeciliPalet(_palet);
            else
                MessageBox.Show("Data Source Tanımlı değil");

            PandapRaporSimpleViever view = new PandapRaporSimpleViever();
            view.report_view_control.DocumentSource = v;
            view.report_view_control.ZoomFactor = 0.85;
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            view.Width = 500;
            view.Height = 780;

            v.CreateDocument();

            view.ShowDialog();

            fs.Close();
        }

        private void PaletBarkodDesignerAc(object obj)
        {
            PandapRaporDesigner rpPaletBarkod = new PandapRaporDesigner();
            rpPaletBarkod.designer.DocumentOpened += (sender, e) =>
            {
                var v = e.Document;

                var ds = v.ReportModel.DataSource as ObjectDataSource;

                if (ds != null)
                    BarkodPaletDtoFromSeciliPalet(SeciliPalet);
                else
                    MessageBox.Show("Data Source Tanımı yapınız...");
            };

            rpPaletBarkod.ShowDialog();
        }

        public BarkodPaletDto BarkodPaletDtoFromSeciliPalet(Palet _seciliPalet)
        {
            return new BarkodPaletDto
            {
                Musteri = _seciliPalet.CariKartNav.CariIsim,
                UlkeAd = _seciliPalet.CariKartNav.UlkeAd,
                UlkeKod = _seciliPalet.CariKartNav.UlkeKod,
                PaketlenmeTarihi = DateTime.Now,
                MusteriSiparisNo = _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.SiparisNav.FirmaSiparisNo,

                MusteriSiparisKalemNo = _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.MusteriUrunKodu,
                Ebat = (_seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.Kalinlik_micron.Value / 1000).ToString("n4") + " X " +
                        _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.En_mm.Value.ToString("n1"),
                AlasimVeYuzey = _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.AlasimTipKod + " " +
                        _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.SertlikTipKod,
                UretimEmriNo = _seciliPalet.UretimEmriKodNav.UretimEmriKod + Environment.NewLine +
                            "[" + String.Join("/ ", _seciliPalet.Bobinler.Select(s => s.FH_CikisNo.ToString()).Distinct()) + "]",
                UretimBobinler = String.Join(", ", _seciliPalet.Bobinler.Select(s => s.BobinNo.ToString())),
                NetKg = _seciliPalet.ToplamBobinAgirligi,
                BrutKg = _seciliPalet.ToplamBobinAgirligi + _seciliPalet.PaletAgirlik,
                PandaSiparisNo = _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.SiparisKod,
                PaletNo = _seciliPalet.Id.ToString().PadLeft(10, '0')
            };
        }

        public Palet tempPalet;

        public Palet TempPalet { get => tempPalet; set => SetProperty(ref tempPalet, value); }

        public ObservableCollection<CariLookUp> paleteHazirCariler;

        public ObservableCollection<CariLookUp> PaleteHazirCariler
        {
            get
            {
                var lookUp_cariler = uow.PlanlamaRepo.PaleteHazirCarileriGetir();

                lookUp_cariler.Add(new CariLookUp { CariKod = "120-01-01-243", CariIsim = "PANDA STOK (Panda Aluminyum)" });

                return lookUp_cariler;
            }
        }

        public ObservableCollection<Palet> depodakiPaletler;

        public ObservableCollection<Palet> DepodakiPaletler
        {
            get => depodakiPaletler; set => SetProperty(ref depodakiPaletler, value);
        }

        public int gunlukTonaj;

        public int GunlukTonaj { get => gunlukTonaj; set => SetProperty(ref gunlukTonaj, value); }

        private async void OnDepoyaGonder(object obj)
        {
            SeciliPalet.DepoyaAktarilmaTarihi = DateTime.Now;

            var _uretimEmri = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliPalet.Bobinler.First().UretimEmriKod);

            uow.Commit();

            DepodakiPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetir();

            Paletler = uow.PlanlamaRepo.PaletleriGetir();
        }

        private void OpenPaletDialog(object obj)
        {
            IsOpenPaletDialog = true;

            OnPropertyChanged(nameof(PaleteHazirCariler));

            TempPalet = new Palet();
        }

        private void ClosePaletDialog(string parametre)
        {
            if (parametre == "Ok")
            {
                if (tempPalet.CariKod == null)
                {
                    MessageBox.Show("Cari seçiniz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var _yeni_palet = PandapObjectHelper.CopyObject(TempPalet);

                Paletler.Add(_yeni_palet);

                uow.PlanlamaRepo.PaletEkleKaydet(_yeni_palet);

                Paletler = uow.PlanlamaRepo.PaletleriGetir();

                IsOpenPaletDialog = false;
            }
            else
                IsOpenPaletDialog = false;
        }

        public ObservableCollection<Bobin> cariyeAitKullanilabilirBobinler;

        public ObservableCollection<Bobin> CariyeAitKullanilabilirBobinler
        {
            get => cariyeAitKullanilabilirBobinler;
            set => SetProperty(ref cariyeAitKullanilabilirBobinler, value);
        }

        public ObservableCollection<Palet> Paletler { get => paletler; set => SetProperty(ref paletler, value); }

        private  void OnBobiniPaleteAktar(object _bobin)
        {

            var uowAyarlar = new UnitOfWork();
            PandapGlobal.UygulamaAyarlar.PaketMax_UEmri_yuzde = uowAyarlar.PlanlamaRepo.PaketlemeToleransGetir();
            uowAyarlar.Dispose();

            var bobin = (Bobin)_bobin;
            bobin.PaletId = SeciliPalet.Id;


            var toplamPaketAgirlik = bobin.UretimEmriKodNav.UretimBobinler.Sum(c => c.Agirlik_kg);

            var maxOranYuzde = PandapGlobal.UygulamaAyarlar.PaketMax_UEmri_yuzde;

            if (toplamPaketAgirlik.Value > SeciliBobin.UretimEmriKodNav.Uretim_PlanlananMiktar.Value * (maxOranYuzde + 100) / 100)
            {
                MessageBoxService.ShowMessage($"Toplam bobin ağırlığı üretim emri toplamının {toplamPaketAgirlik}'in  %{maxOranYuzde} undan fazla olamaz", "Pandap", MessageButton.OK, MessageIcon.Warning);
                return;
            }

            SeciliPalet.UretimEmriKod = bobin.UretimEmriKod;

            SeciliPalet.UretimEmriKodNav = uow.PlanlamaRepo.UretimEmriNavGetir(bobin.UretimEmriKod);




            SeciliPalet.Bobinler.Add(bobin);

            uow.Commit();

            CariEklemeVeriGuncelle();



        }


        public void CariEklemeVeriGuncelle()
        {
         

            CariyeAitKullanilabilirBobinler = uow.PlanlamaRepo.CariyeAitKullanilabilirBobinleriGetir(seciliPalet.CariKod);

            SeciliPalet.OnPropertyChanged(nameof(SeciliPalet.PaletAgirlik));
            SeciliPalet.OnPropertyChanged(nameof(SeciliPalet.ToplamBobinAgirligi));
            SeciliPalet.OnPropertyChanged(nameof(SeciliPalet.BrutAgirlik));
        }

        private  void OnPaletBobinSil(object obj)
        {
            var bobin = (Bobin)obj;
            bobin.PaletId = null;

           uow.Commit();
            SeciliPalet.Bobinler.Remove(bobin);

            CariEklemeVeriGuncelle();
        }

        public  void onUretimEmriKapat(object obj)
        {
            var mesaj = "Üretimde" + SeciliUretimEmri.Uretim_UretimdekiMiktar + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

            uow = new UnitOfWork();
            var uretimEmri = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliUretimEmri.UretimEmriKod);

            var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel);

            SiparisKalem s = uow.SiparisKalemRepo.SiparisKalemiGetir(SeciliUretimEmri.SiparisKalemKod);

            if (mesajSonuc == MessageBoxResult.Yes)
            {
                s.PLAN_PlanlanacakKalanMiktarToplam = s.PLAN_PlanlanacakKalanMiktarToplam + uretimEmri.Uretim_UretimdekiMiktar;
            }

            if (mesajSonuc == MessageBoxResult.No)
            {
                uretimEmri.KapatildiMi = true;
            }

          uow.Commit();
        }

        private  void onKalemIslem(object obj)
        {
            if (obj.ToString() == "Paketle")
            {
                UretimMiktarWindow w = new UretimMiktarWindow();
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                var cevap = w.ShowDialog();

                if (cevap == true && w.txtPaketlenen.Text.Length > 0)
                {
                    UretimEmri u = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliUretimEmri.UretimEmriKod);


                    SiparisKalem s = uow.SiparisKalemRepo.SiparisKalemiGetir(SeciliUretimEmri.SiparisKalemKod);

                    var uretimEmirListe = uow.PlanlamaRepo.KalemUretimEmirleriGetir(SeciliUretimEmri.SiparisKalemKod);

                    s.PLAN_UretimdekiMiktarToplam = s.PLAN_PlanlananMiktarToplam - s.PLAN_PaketlenenToplam;

                   uow.Commit();

                    return;
                }
            }
        }

        private ObservableCollection<Palet> paletler;

        public PaketlemeViewModel()
        {
            Paletler = uow.PlanlamaRepo.PaletleriGetir();

            DepodakiPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetir();

            GunlukTonaj = uow.PlanlamaRepo.GunlukTonajGetir();
        }

        private void SeciliBobin_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Agirlik_kg")
            {

            }



        }
    }
}