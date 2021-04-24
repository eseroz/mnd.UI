using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.Helper;
using mnd.Logic.Model.Satis;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.Logic.Services;
using mnd.Logic.Services._DTOs;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;
using mnd.UI.Modules.PlanlamaModule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.PaketlemeModule
{
    public class PaleteBobinEkleFormModel
    {
        public string Aciklama { get; set; }
        public int? Agirlik_kg { get; internal set; }
        public decimal? En_mm { get; internal set; }
        public decimal? Kalinlik_micron { get; internal set; }
        public string AlasimTipKod { get; internal set; }
        public string PaketlemeNot { get; internal set; }
        public string KullanimAlan { get; internal set; }
    }


    public class PaketlemeViewModel : MyDxViewModelBase, IForm
    {
        private UnitOfWork uow = new UnitOfWork();
        KantarSerialPort k;

        private UretimEmri seciliUretimEmri;

        public bool AgirlikGirebilirMi => (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MAMULDEPO
            || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA ||
            AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.ADMIN
            );


        public void PaletVeDepoOnayEkranGuncelle()
        {
            var kul = AppPandap.AktifKullanici.KullaniciId;

            if (kul.Contains("paket"))
            {
                DepoOnayBekleyenPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetirYeni()
                              .Where(c => c.Ekleyen == kul)
                                .ToObservableCollection();

                Paletler = uow.PlanlamaRepo.PaletleriGetir()
                        .Where(c => c.Ekleyen == kul)
                        .ToObservableCollection();
            }
            else
            {
                DepoOnayBekleyenPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetirYeni()
                              .ToObservableCollection();
                Paletler = uow.PlanlamaRepo.PaletleriGetir()
                        .ToObservableCollection();
            }


        }


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

        public DelegateCommand<object> OpenPaletDialogCommand => new DelegateCommand<object>(OpenPaletDialog, c => true);
        public DelegateCommand<string> ClosePaletDialogCommand => new DelegateCommand<string>(ClosePaletDialog, c => true);

        public DelegateCommand<object> BobiniPaleteAktarCommand => new DelegateCommand<object>(OnPaleteBobinEkle, c => true);

        public DelegateCommand<object> PaletBobinSilCommand => new DelegateCommand<object>(OnPaletBobinSil, c => true);

        public DelegateCommand<object> BobinAgirlikKantardanAlCommand => new DelegateCommand<object>(OnBobinAgirlikKantardanAl, c => true);

        public PaketlemeViewModel()
        {


        }
        public void SerialPortOlusturBaglan()
        {
            k = new KantarSerialPort("COM1", "9600", "None", "8", "One");
        }

        private void OnBobinAgirlikKantardanAl(object obj)
        {
            if (k == null) SerialPortOlusturBaglan();

            KantarSonucWindow w = new KantarSonucWindow(k);
            k.dinlemeyiBaslat();

            var cev = w.ShowDialog();

            if (cev == true)
            {
                try
                {
                    var deger = int.Parse(w.txtKantarSonuc.Text);
                    SeciliBobin.Agirlik_kg = deger;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            k.dinlemeyiDurdur();

        }

        public DelegateCommand<object> DepoOnayaGonderCommand => new DelegateCommand<object>(OnDepoOnayaGonder, c => true);
        public DelegateCommand<object> PaletBarkodYazdirCommand => new DelegateCommand<object>(OnPaletBarkodYazdir, c => true);

        public DelegateCommand<object> PaletSilCommand => new DelegateCommand<object>(OnPaletSil, c => true);

        public DelegateCommand<object> DepoOnayindanbGeriAlCommand => new DelegateCommand<object>(OnDepoOnayindanbGeriAl, c => true);

        public PaketlemeViewModel(string menuFormAd)
        {
            MenuFormAd = menuFormAd;
        }

        private void OnDepoOnayindanbGeriAl(object obj)
        {
            var _palet = (Palet)obj;
            _palet.DepoOnayaGonderimTarihi = null;
            _palet.PaletKonum = PALETKONUM.PAKETLEME;

            uow.PlanlamaRepo.PaletKaydet(_palet);

            uow.Commit();

            PaletVeDepoOnayEkranGuncelle();


        }

        private void OnPaletSil(object obj)
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


            _palet.FiyatKalemKodNav = uow.SiparisKalemRepo.SiparisKalemiGetir(_palet.FiyatKalemKod);

            PaletBarkodViewerAc(_palet);
        }

        public void PaletBarkodViewerAc(Palet _palet)
        {
            var raporTanimId = 7;

            //PaletBarkod_GTATA_JAEGGI özel durum
            if (_palet.CariKod == "120-98-01-344" || _palet.CariKod == "120-98-01-493") raporTanimId = 60;

            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(raporTanimId);

            var dsObject = PaketlemeViewModel.BarkodPaletDtoFromSeciliPalet(_palet);

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);

        }

        public static BarkodPaletDto BarkodPaletDtoFromSeciliPalet(Palet _seciliPalet)
        {
            var barkodDto = new BarkodPaletDto
            {
                Musteri = _seciliPalet.FiyatKalemKodNav.SiparisNav.CariKartNavigation.CariIsim,
                MusteriAdres = _seciliPalet.FiyatKalemKodNav.SiparisNav.CariKartNavigation.CariAdres,

                UlkeAd = _seciliPalet.FiyatKalemKodNav.SiparisNav.CariKartNavigation.UlkeAd,
                UlkeKod = _seciliPalet.FiyatKalemKodNav.SiparisNav.CariKartNavigation.UlkeKod,

                PaketlenmeTarihi = DateTime.Now,

                MusteriSiparisNo = _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.SiparisNav.FirmaSiparisNo?.ToUpper(),

                MusteriSiparisKalemNo = _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.MusteriUrunKodu,
                //Ebat = (_seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.Kalinlik_micron.Value / 1000).ToString("n4") + " X " +
                //        _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.En_mm.Value.ToString("n1"),
                //AlasimVeYuzey = _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.AlasimTipKod + " " +
                //        _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.SertlikTipKod,
                UretimEmriNo = _seciliPalet.UretimEmriKodNav.UretimEmriKod + Environment.NewLine +
                            "[" + String.Join("/ ", _seciliPalet.Bobinler.Select(s => s.FH_CikisNo?.ToString()).Distinct()) + "]",

                UretimEmriNo_Basit = _seciliPalet.UretimEmriKodNav.UretimEmriKod,

                KafileNo = _seciliPalet.UretimEmriKodNav.KartNo,



                UretimBobinler = String.Join(", ", _seciliPalet.Bobinler.Select(s => s.BobinNo?.ToString())),
                UretimBobinSayisi = _seciliPalet.Bobinler.Count,

                NetKg = _seciliPalet.PaletNet_Kg,
                BrutKg = _seciliPalet.PaletNet_Kg + _seciliPalet.PaletDara_Kg,
                PandaSiparisNo = _seciliPalet.UretimEmriKodNav.SiparisKalemKodNav.SiparisKod,
                PaletNo = _seciliPalet.Id.ToString().PadLeft(10, '0'),
                PaletNo_Basit = _seciliPalet.Id.ToString()
            };

            return barkodDto;
        }

        public Palet tempPalet;

        public Palet TempPalet { get => tempPalet; set => SetProperty(ref tempPalet, value); }

        public ObservableCollection<CariLookUp> paleteHazirCariler;

        public ObservableCollection<CariLookUp> PaleteHazirCariler
        {
            get
            {
                var lookUp_cariler = uow.PlanlamaRepo.PaleteHazirCarileriGetir(AppPandap.AktifKullanici.KullaniciId);

                lookUp_cariler.Add(new CariLookUp { CariKod = "120-01-01-243", CariIsim = "PANDA STOK (Panda Aluminyum)" });

                return lookUp_cariler;
            }
        }

        public ObservableCollection<Palet> depodakiPaletler;

        public ObservableCollection<Palet> DepoOnayBekleyenPaletler
        {
            get => depodakiPaletler; set => SetProperty(ref depodakiPaletler, value);
        }

        public int gunlukTonaj;

        public int GunlukTonaj { get => gunlukTonaj; set => SetProperty(ref gunlukTonaj, value); }

        private void OnDepoOnayaGonder(object obj)
        {
            if(SeciliPalet==null)
            {
                MessageBox.Show("Paleti seçiniz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SeciliPalet.Bobinler.Count == 0)
            {
                MessageBox.Show("Palete hiç bobin eklenmemiş", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SeciliPalet.DepoOnayaGonderimTarihi = DateTime.Now;
            SeciliPalet.PaletKonum = PALETKONUM.DEPO_ONAY;

            var _uretimEmri = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliPalet.Bobinler.First().UretimEmriKod);

            uow.Commit();

            PaletVeDepoOnayEkranGuncelle();
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

                var _yeni_palet = Helper.PandapObjectHelper.CopyObject(TempPalet);
                _yeni_palet.PaletKonum = PALETKONUM.PAKETLEME;
                _yeni_palet.Ekleyen = AppPandap.AktifKullanici.KullaniciId;

                Paletler.Add(_yeni_palet);

                uow.PlanlamaRepo.PaletEkleKaydet(_yeni_palet);

                Paletler = uow.PlanlamaRepo.PaletleriGetir()
                       .Where(c => c.Ekleyen == AppPandap.AktifKullanici.KullaniciId)
                       .ToObservableCollection();


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
        public string MenuFormAd { get; }

        private void OnPaleteBobinEkle(object _bobin)
        {
            var bobin = (Bobin)_bobin;


            if (bobin.Agirlik_kg.GetValueOrDefault() == 0)
            {
                MessageBox.Show("Bobin ağrılığını kantardan okutunuz", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SeciliPalet.UretimEmriKod != null && bobin.UretimEmriKod != SeciliPalet.UretimEmriKod)
            {
                MessageBox.Show($"Bu palete sadece {SeciliPalet.UretimEmriKod} üretim emrinden gelen bobinler eklenebilir.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var kalem = PaketlemeSiparisService.SiparisKalemGetir(bobin.UretimEmriKodNav.SiparisKalemKod);

            PaleteBobinEkleFormModel model = new PaleteBobinEkleFormModel();
            model.Agirlik_kg = bobin.Agirlik_kg;
            //model.En_mm = kalem.En_mm;
            //model.Kalinlik_micron = kalem.Kalinlik_micron;
            //model.AlasimTipKod = kalem.AlasimTipKod;
            //model.Aciklama = kalem.KulcePrimTipKod;
            model.PaketlemeNot = PaketlemeSiparisService.PaketlemeNotGetir(kalem.SiparisKod);
            //model.KullanimAlan = kalem.KullanimAlanTipKod;

            BobinEkleOnayForm frm = new BobinEkleOnayForm(model);
            frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var cev = frm.ShowDialog();

            if (cev == false) return;



            var uowAyarlar = new UnitOfWork();
            AppPandap.UygulamaAyarlar.PaketMax_UEmri_yuzde = uowAyarlar.PlanlamaRepo.PaketlemeToleransGetir();
            uowAyarlar.Dispose();


            bobin.PaletId = SeciliPalet.Id;


            var toplamPaketAgirlik = bobin.UretimEmriKodNav.UretimBobinler.Sum(c => c.Agirlik_kg);

            var maxOranYuzde = AppPandap.UygulamaAyarlar.PaketMax_UEmri_yuzde;

            if (toplamPaketAgirlik.Value > SeciliBobin.UretimEmriKodNav.Uretim_PlanlananMiktar.Value * (maxOranYuzde + 100) / 100)
            {
                MessageBoxService.ShowMessage($"Toplam bobin ağırlığı üretim emri toplamının %{maxOranYuzde} inden fazla olamaz", "Pandap", MessageButton.OK, MessageIcon.Warning);
                return;
            }

            SeciliPalet.PaketlemeNot = model.PaketlemeNot;
            SeciliPalet.UretimEmriKod = bobin.UretimEmriKod;
            SeciliPalet.UretimEmriKodNav = uow.PlanlamaRepo.UretimEmriNavGetir(bobin.UretimEmriKod);

            SeciliPalet.FiyatKalemKod = SeciliPalet.UretimEmriKodNav.SiparisKalemKod;

            if (model.Aciklama?.Length > 0) SeciliPalet.Aciklama += $" {model.Aciklama}({bobin.BobinNo})";
            SeciliPalet.Bobinler.Add(bobin);

            uow.Commit();

            CariEklemeVeriGuncelle();

        }

        private void OnPaletBobinSil(object parameters)
        {

            var values = (object[])parameters;
            var palet = (Palet)values[0];
            var bobin = (Bobin)values[1];

            SeciliPalet = palet;

            palet.Bobinler.Remove(bobin);

            if (palet.Bobinler.Count == 0)
            {
                palet.UretimEmriKod = null;
            }

            bobin.PaletId = null;

            uow.Commit();


            CariEklemeVeriGuncelle();
        }

        public void CariEklemeVeriGuncelle()
        {


            CariyeAitKullanilabilirBobinler = uow.PlanlamaRepo.CariyeAitKullanilabilirBobinleriGetir(seciliPalet.CariKod);

            SeciliPalet.OnPropertyChanged(nameof(SeciliPalet.PaletDara_Kg));
            SeciliPalet.OnPropertyChanged(nameof(SeciliPalet.PaletNet_Kg));
            SeciliPalet.OnPropertyChanged(nameof(SeciliPalet.PaletBrut_Kg));
        }

        private void onKalemIslem(object obj)
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

                    //s.PLAN_UretimdekiMiktarToplam = s.PLAN_PlanlananMiktarToplam - s.PLAN_PaketlenenToplam;

                    uow.Commit();

                    return;
                }
            }
        }

        private ObservableCollection<Palet> paletler;


        public void Load()
        {
            PaletVeDepoOnayEkranGuncelle();

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