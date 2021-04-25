using AutoMapper;
using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.Model.App;
using mnd.Logic.Model.Operasyon;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules._SatisModule;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.OperasyonModule
{
    public class SevkEmriListVM : MyDxViewModelBase
    {

        public DelegateCommand<string> SiparisiAcCommand => new DelegateCommand<string>(OnSiparisiAc, c => true);

        public DelegateCommand<object> EkraniTazeleCommand => new DelegateCommand<object>(OnEkraniTazele, true);

        public DelegateCommand<object> SevkEmriCommand => new DelegateCommand<object>(OnSevkEmriSil, CanSevkiyatSil);

        public DelegateCommand<object> SevkTeslimEdildiCommand => new DelegateCommand<object>(OnSevkTeslimEdildi, CanSevkTesliEdildi);

        private void OnSevkTeslimEdildi(object obj)
        {
            SevkTeslimTarihiGirisForm frm = new SevkTeslimTarihiGirisForm();

            SevkTeslimVM vm = new SevkTeslimVM();
            vm.TeslimTarihi = DateTime.Now;

            frm.DataContext = vm;

            frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var cev = frm.ShowDialog();

            if (cev == true)
            {
                uow.SevkiyatEmirRepo.SevkiyatTeslimEdildi(SeciliSevkiyatEmri.SevkiyatEmriId, vm.TeslimTarihi);

                SeciliSevkiyatEmri.TeslimTarihi = DateTime.Now;
                uow.Commit();
            }

        }

        private bool CanSevkTesliEdildi(object arg)
        {
            return true;
        }

        private bool CanSevkiyatSil(object arg)
        {
            var silinebilirMi = SeciliSevkiyatEmri != null
                && AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.OPERASYON
                && SeciliSevkiyatEmri.CariIrsaliyeler.Count == 0;

            return silinebilirMi;
        }

        private void OnSevkEmriSil(object obj)
        {
            if (SeciliSevkiyatEmri.CariIrsaliyeler.Count > 0)
            {
                MessageBox.Show("Önce Sevkiyat emirinin paletlerini geri alınız", "Pandap", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }


            var cev = MessageBox.Show("Sevkiyat Emri Silinecek", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (cev == MessageBoxResult.OK)
            {
                uow.SevkiyatEmirRepo.SevkEmriSil(SeciliSevkiyatEmri);
                uow.Commit();

                SevkiyatEmirleri.Remove(SeciliSevkiyatEmri);
            }


        }

        public string SeciliFiltre { get; set; }


        public DelegateCommand BekleyenSevkEmrileriGosterCommand => new DelegateCommand(OnBekleyenSevkEmirleriGoster, true);

        private void OnBekleyenSevkEmirleriGoster()
        {

            SeciliFiltre = "BEKLEYEN";
            uow = new UnitOfWork();
            SevkiyatEmirleri = uow.SevkiyatEmirRepo.SevkiyatEmirleriGetir("BEKLEYEN");
        }

        public DelegateCommand GonderilenSevkEmirleriGosterCommand => new DelegateCommand(OnGonderilenSevkEmirleriGoster, true);

        private void OnGonderilenSevkEmirleriGoster()
        {
            uow = new UnitOfWork();

            SeciliFiltre = "MUHASEBE";
            SevkiyatEmirleri = uow.SevkiyatEmirRepo.SevkiyatEmirleriGetir(SeciliFiltre);
        }

        private void OnEkraniTazele(object obj)
        {
            uow = new UnitOfWork();
            SevkiyatEmirleri = uow.SevkiyatEmirRepo.SevkiyatEmirleriGetir(SeciliFiltre);



            SevkiyatEmirleri.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
        }

        public DelegateCommand<object> SevkiyatYazdirCommand => new DelegateCommand<object>(SevkiyatYazdir, CanSevkiyatYazdir);
        public DelegateCommand<object> PackingListOlusturCommand => new DelegateCommand<object>(PackingListYazdir, true);
        public DelegateCommand<object> CommercialInvoceOlusturCommand => new DelegateCommand<object>(TicariFaturaYazdir, true);
        public DelegateCommand<object> SiparisSozlesmesiYazdirCommand => new DelegateCommand<object>(SiparisSozlesmesiYazdir, true);

        public DelegateCommand<object> DuzenleCommand => new DelegateCommand<object>(Duzenle, CanDuzenle);

        private bool CanDuzenle(object arg)
        {
            if (SeciliSevkiyatEmri == null) return false;

            //if (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.OPERASYON) return true;
            //if( AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI) return false;

            return true;
        }

        public DelegateCommand<object> YeniCommand => new DelegateCommand<object>(YeniSevkEmri, true);

        public DelegateCommand<string> SevkSurecIslemCommand => new DelegateCommand<String>(OnSevkSurecDegistir, CanSurecDegistir);

        public virtual IWindowService WinDialogService => ServiceContainer.GetService<IWindowService>();

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);
        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);

        public DelegateCommand<object> SiparisAcCommand => new DelegateCommand<object>(SiparisiAc, true);

        public DelegateCommand<object> IrsaliyeDetayGosterCommand => new DelegateCommand<object>(OnIrsaliyeDetayGoster, CanIrsaliyeDetayGoster);

        private bool CanIrsaliyeDetayGoster(object arg)
        {

            if (SeciliSevkiyatEmri == null) return false;

            return true;
        }

        private void OnIrsaliyeDetayGoster(object obj)
        {

            UnitOfWork uow1 = new UnitOfWork();
            IrsaliyeDetayViewModel vm = new IrsaliyeDetayViewModel(SeciliCariIrsaliye.IrsaliyeId);

            WinDialogService.Show(nameof(IrsaliyeDetayView), vm);


            if (vm.Result == MessageBoxResult.OK)
            {
                var dbSevkiyatEmri = uow1.SevkiyatEmirRepo.SevkiyatEmriGetirFromId(SeciliSevkiyatEmri.SevkiyatEmriId);

                Mapper.Map<SevkiyatEmri, SevkiyatEmri>(dbSevkiyatEmri, SeciliSevkiyatEmri);

                SeciliSevkiyatEmri.OnPropertyChanged(nameof(SeciliSevkiyatEmri.CariIsimlerBirlesik));
            };


        }

        private void YeniSevkEmri(object obj)
        {
            UnitOfWork uow1 = new UnitOfWork();

            SevkEmriViewModel vm = new SevkEmriViewModel();

            var yeniSevkEmri = vm.Load(0);

            WinDialogService.Show(nameof(SevkEmriView), vm);

            if (vm.Result == MessageBoxResult.OK)
            {
                var dbSevkiyatEmri = uow1.SevkiyatEmirRepo.SevkiyatEmriGetirFromId(yeniSevkEmri.SevkiyatEmriId);

                SevkiyatEmirleri.Add(yeniSevkEmri);

            };

        }

        private void Duzenle(object obj)
        {
            UnitOfWork uow1 = new UnitOfWork();

            SevkEmriViewModel vm = new SevkEmriViewModel();
            vm.Load(SeciliSevkiyatEmri.SevkiyatEmriId);

            WinDialogService.Show(nameof(SevkEmriView), vm);

            if (vm.Result == MessageBoxResult.OK)
            {
                var dbSevkiyatEmri = uow1.SevkiyatEmirRepo.SevkiyatEmriGetirFromId(SeciliSevkiyatEmri.SevkiyatEmriId);

                Mapper.Map<SevkiyatEmri, SevkiyatEmri>(dbSevkiyatEmri, SeciliSevkiyatEmri);

                SeciliSevkiyatEmri.OnPropertyChanged(nameof(SeciliSevkiyatEmri.CariIsimlerBirlesik));


                // mapping sorunlu olabilir
                OnEkraniTazele(null);
            };
        }

        public Irsaliye SeciliCariIrsaliye
        {
            get
            {
                return _seciliCariIrsaliye;
            }
            set
            {
                if (_seciliCariIrsaliye == value) return;
                _seciliCariIrsaliye = value;

                if (_seciliCariIrsaliye != null)
                    _seciliCariIrsaliye.IrsaliyePaletGruplu = IrsaliyeService.IrsaliyePaletGrupla(_seciliCariIrsaliye);
            }
        }


        private void OnSiparisiAc(string siparisKodKalem)
        {

            var siparisKod = siparisKodKalem.Split('/')[0];

            var uow1 = new UnitOfWork();
            var vm = new SiparisViewModel(false);
            var sip = uow1.SiparisRepo.SiparisGetir(siparisKod);

            vm.Load(sip);

            var doc = AppPandap.pDocumentManagerService.CreateDocument(nameof(SiparisView), vm);
            doc.Title = sip.SiparisKod;
            doc.Show();
        }



        private void SiparisiAc(object obj)
        {
            var siparisKod = SeciliEkliPalet.FiyatKalemKodNav.SiparisKod;

            var uow1 = new UnitOfWork();

            var vm = new SiparisViewModel(false);
            vm.SiparisKayitModu = KayitModu.Edit;

            var sip = uow1.SiparisRepo.SiparisGetir(siparisKod);
            vm.Load(sip);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SiparisView", vm);
            doc.Title = sip.SiparisKod;
            doc.Show();


        }

        public string GridLayoutFileName => "SeviyatEmirleri.xml";

        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(GridLayoutFileName);
        }

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, GridLayoutFileName.Replace(".xml", ""));
        }




        private UnitOfWork uow = new UnitOfWork();

        public SevkEmriListVM(string formMenuAd)
        {
            FormMenuAd = formMenuAd;

            SevkiyatEmirleri = uow.SevkiyatEmirRepo.SevkiyatEmirleriGetir("BEKLEYEN");

            SevkiyatEmirleri.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);


        }

        public void Load()
        {
            ExportService.RestoreLayout(GridLayoutFileName);
        }

        public decimal FaturaTutar { get; set; }

        private void OnSevkSurecDegistir(string yeniSevkEmriKonum)
        {

            if (SeciliSevkiyatEmri.RiskVarMi == true)
            {
                MessageBox.Show("Önce risk onay işlemlerini tamamlayınız", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Yukleme başlat
            if (SeciliSevkiyatEmri.SevkSurecDurum == SEVKSURECKONUM.YUKLEME && yeniSevkEmriKonum == SEVKSURECKONUM.AKTIFYUKLEME)
            {
                var _uowSevkiyat0 = new UnitOfWork();

                var s_emri = _uowSevkiyat0.SevkiyatEmirRepo.SevkiyatEmriGetirFromId(SeciliSevkiyatEmri.SevkiyatEmriId);
                s_emri.YuklemeBaslamaTarih = DateTime.Now;

                _uowSevkiyat0.Commit();

                return;
            }


            // yükleme bitirme aşaması
            if (SeciliSevkiyatEmri.SevkSurecDurum == SEVKSURECKONUM.YUKLEME && yeniSevkEmriKonum == SEVKSURECKONUM.OPERASYON_KONTROL)
            {
                var _uowSevkiyat = new UnitOfWork();


                var yuklemedekiSevkiyatEmri = _uowSevkiyat.SevkiyatEmirRepo.YuklemedekiSevkiyatEmirleriGetir(SeciliSevkiyatEmri.SevkiyatEmriId);

                yuklemedekiSevkiyatEmri.PaletYuklemeVerileriniEkle();

                var yuklenmemisPaletSayisi = yuklemedekiSevkiyatEmri.KalanPaletler.Count;

                if (yuklenmemisPaletSayisi > 0)
                {
                    var yuklenemeyenler = yuklemedekiSevkiyatEmri.KalanPaletler.Select(c => c.PaletId);
                    var uyariMesaj = String.Join(";", yuklenemeyenler) + $"{Environment.NewLine} numaralı paletler yuklenemedi.{Environment.NewLine} Bu paletlerin tekrar depoya aktarılmasını onaylıyormusunuz?" + Environment.NewLine;

                    MessageBoxResult sonuc = MessageBox.Show(uyariMesaj, "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (sonuc == MessageBoxResult.OK)
                    {
                        var uowPalet = new UnitOfWork();

                        foreach (int paletId in yuklenemeyenler)
                        {
                            uowPalet.SevkiyatEmirRepo.PaletDurumDegistir(paletId, PALETKONUM.DEPO);
                            uowPalet.SevkiyatEmirRepo.IrsaliyePaletSil(paletId);

                        }
                        uowPalet.Commit();
                        MessageBox.Show("Paletler depoya geri aktarıldı ve sevk emri yeniden düzenlendi", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                }

            }


            if (SeciliSevkiyatEmri.SevkSurecDurum == SEVKSURECKONUM.YUKLEME && yeniSevkEmriKonum == SEVKSURECKONUM.OPERASYON_KONTROL)
            {
                var uowsevkGuncelle = new UnitOfWork();

                var sevkEmir = uowsevkGuncelle.SevkiyatEmirRepo.SevkiyatEmriGetirFromId(SeciliSevkiyatEmri.SevkiyatEmriId);
                sevkEmir.YuklemeBitisTarih = DateTime.Now;

                uowsevkGuncelle.Commit();
            }


            SeciliSevkiyatEmri.SevkSurecDurum = yeniSevkEmriKonum;
            uow.SevkiyatEmirRepo.SevkiyatSurecDegistir(SeciliSevkiyatEmri.SevkiyatEmriId, yeniSevkEmriKonum);

            uow.Commit();
        }

        private bool CanSurecDegistir(string yeniSevkEmriKonum)
        {
            if (SeciliSevkiyatEmri == null) return false;

            if (AppPandap.KullaniciRol == KULLANICIROLLERI.YONETICI) return true;
            if (AppPandap.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI) return true;

            var aktifSevkEmriKonum = SeciliSevkiyatEmri.SevkSurecDurum;

            if (aktifSevkEmriKonum == SEVKSURECKONUM.OPERASYON && yeniSevkEmriKonum == SEVKSURECKONUM.DEPO)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.OPERASYON;
            }

            if (aktifSevkEmriKonum == SEVKSURECKONUM.DEPO && yeniSevkEmriKonum == SEVKSURECKONUM.OPERASYON_KONTROL)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.MAMULDEPO;
            }

            if (aktifSevkEmriKonum == SEVKSURECKONUM.DEPO && yeniSevkEmriKonum == SEVKSURECKONUM.YUKLEME)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.MAMULDEPO;
            }

            if (aktifSevkEmriKonum == SEVKSURECKONUM.YUKLEME && yeniSevkEmriKonum == SEVKSURECKONUM.AKTIFYUKLEME)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.MAMULDEPO;
            }


            if (aktifSevkEmriKonum == SEVKSURECKONUM.YUKLEME && yeniSevkEmriKonum == SEVKSURECKONUM.OPERASYON_KONTROL)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.MAMULDEPO;
            }

            if (aktifSevkEmriKonum == SEVKSURECKONUM.OPERASYON_KONTROL && yeniSevkEmriKonum == SEVKSURECKONUM.PLAN_YONETICI)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.OPERASYON;
            }

            if (aktifSevkEmriKonum == SEVKSURECKONUM.OPERASYON_KONTROL && yeniSevkEmriKonum == SEVKSURECKONUM.DEPO)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.OPERASYON;
            }


            if (aktifSevkEmriKonum == SEVKSURECKONUM.PLAN_YONETICI && yeniSevkEmriKonum == SEVKSURECKONUM.MUHASEBE)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;
            }

            if (aktifSevkEmriKonum == SEVKSURECKONUM.PLAN_YONETICI && yeniSevkEmriKonum == SEVKSURECKONUM.OPERASYON_KONTROL)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;
            }

            if (aktifSevkEmriKonum == SEVKSURECKONUM.MUHASEBE && yeniSevkEmriKonum == SEVKSURECKONUM.PLAN_YONETICI)
            {
                return AppPandap.KullaniciRol == KULLANICIROLLERI.MUHASEBE;
            }



            return false;

        }


        private Palet seciliEkliPalet;

        public Palet SeciliEkliPalet
        {
            get => seciliEkliPalet;
            set => SetProperty(ref seciliEkliPalet, value);
        }

        private SevkiyatEmri seciliSevkiyatEmri;

        public SevkiyatEmri SeciliSevkiyatEmri
        {
            get => seciliSevkiyatEmri;
            set => SetProperty(ref seciliSevkiyatEmri, value);
        }

        private ObservableCollection<SevkiyatEmri> sevkiyatEmirleri;
        private Irsaliye _seciliCariIrsaliye;

        public ObservableCollection<SevkiyatEmri> SevkiyatEmirleri
        {
            get => sevkiyatEmirleri;
            set => SetProperty(ref sevkiyatEmirleri, value);
        }

        private bool CanSevkiyatYazdir(object arg)
        {
            return true;
        }

        private void SevkiyatYazdir(object obj)
        {
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(1);

            var dsObject = SeciliSevkiyatEmri;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        private void PackingListYazdir(object obj)
        {
            RaporTanim raporTanim = null;

            var cariKod = SeciliSevkiyatEmri.CariIrsaliyeler.First().CariKod;

            var ulkeKod = uow.PandapCariRepo.PandapCariGetir(cariKod).UlkeKod;

            if (cariKod == "120-98-01-184")
            {
                raporTanim = uow.RaporTanimRepo.RaporGetirFromId(50);
            }
            else if (ulkeKod == "US")
            {
                raporTanim = uow.RaporTanimRepo.RaporGetirFromId(30);
            }
            else
            {
                raporTanim = uow.RaporTanimRepo.RaporGetirFromId(3);
            }

            string seciliCariKod = SeciliCariIrsaliye.CariKod;

            var dsObject = SeciliCariIrsaliye;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        private void TicariFaturaYazdir(object obj)
        {
            RaporTanim raporTanim = null;


            if (_seciliCariIrsaliye.OdemeBankaKod == null)
            {
                MessageBox.Show("Banka kayıtlı değil", "Pandap", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            string seciliCariKod = SeciliCariIrsaliye.CariKod;

            if (seciliCariKod == null)
            {
                MessageBox.Show("Palet Seçiniz");
                return;
            }

            if (seciliCariKod == "120-98-01-545")  // smart usa
                raporTanim = raporTanim = uow.RaporTanimRepo.RaporGetirFromId(41);
            else
                raporTanim = uow.RaporTanimRepo.RaporGetirFromId(4);

            _seciliCariIrsaliye.IrsaliyePaletGruplu = IrsaliyeService.IrsaliyePaletGrupla(_seciliCariIrsaliye);

            var uowBanka = new UnitOfWork();

            _seciliCariIrsaliye.BankaRaporCiktiHtml = uowBanka.BankaRepo.BankaGetir(_seciliCariIrsaliye.OdemeBankaKod).BankaRaporCiktiHtml;

            string cukiKaliteMetin = ModuleStrings.CukiKaliteMetin;
            _seciliCariIrsaliye.KaliteMetin = SeciliCariIrsaliye.CariKod == ModuleStrings.CukiCariKod ? cukiKaliteMetin : null;



            var dsObject = SeciliCariIrsaliye;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }





        private void SiparisSozlesmesiYazdir(object obj)
        {

            var siparisKodlari = SeciliCariIrsaliye.SiparisKodlariBirlesik.Split(';');

            foreach (var item in siparisKodlari)
            {
                SiparisSozlesmeGoster(item);
            }

        }


        public void SiparisSozlesmeGoster(string siparisKod)
        {
            var fullIncludeSiparisData = uow.SiparisRepo.SiparisGetirFull(siparisKod);

            if (fullIncludeSiparisData == null)
            {
                MessageBox.Show("Teyit Formu İçin Gerekli Bilgiler Eksik");
                return;
            }

            Language lang = fullIncludeSiparisData.SatisTipKod == "YD" ? Language.EN : Language.TR;

            var model = TeyitFormService.TeyitFormDtoOlustur(fullIncludeSiparisData, lang);

            var l = new List<SiparisTeyitDTO>();
            l.Add(model);

            var dsObject = l;

            int raporId = lang == Language.EN ? 5 : 6;

            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(raporId);

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }
    }
}