using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.Helper;
using mnd.Logic.Model.Netsis;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.Stoklar
{
    public class StokListViewModel : MyDxViewModelBase
    {
        public string FormMenuAd { get; }
        public ObservableCollection<vwStokTanim> NetsisStoklar { get => netsisStoklar; set => SetProperty(ref netsisStoklar, value); }

        StokTanimNetsisRepository repo = new StokTanimNetsisRepository();
        private bool ısOpenBarkodPopup;
        private vwStokTanim seciliStok;
        private List<string> hucreListesi;
        private ObservableCollection<vwStokTanim> netsisStoklar;
        private string sonKurBilgi;


        public string GridLayoutFileName => "StokListesi.xml";

        public DelegateCommand<string> StokBarkodYazdirCommand => new DelegateCommand<string>(OnStokBarkodYazdir, c => true);

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);

        public DelegateCommand<object> EkranTazeleCommand => new DelegateCommand<object>(OnEkranYenile, c => true);

        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");

        public DelegateCommand<object> ExcelExportCommand1 => new DelegateCommand<object>(OnExcelExport1, CanExportYetkiliMi);

        private bool CanExportYetkiliMi(object arg)
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI ||
                 AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.ADMIN ||
                 AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MUHASEBE_YONETICI;

        }

        private void OnExcelExport1(object obj)
        {
            ExportService1.ExportTo(ExportType.XLSX, "StokListesi.xlsx");
        }

        public string SonKurBilgi { get => sonKurBilgi; set => SetProperty(ref sonKurBilgi, value); }

        private void OnEkranYenile(object obj)
        {
            repo.UpsertStokList();

            repo = new StokTanimNetsisRepository();
            NetsisStoklar = repo.NetsisStokTanimlariGetir()  // giderler dışında
                        .Where(c => c.GRUP_KODU != "7")
                        .ToObservableCollection();


            if (!MaliyetFiyatSutunGorebilirMi) return;

            var dovizKurlari = NetsisService.NetsisDovizKurlariniGetirSonKayittan();
            var sonKurTarihi = dovizKurlari.First().Tarih.Value.Date;
            var euroTlKur = StokListViewModel.PariteGetir("EUR", sonKurTarihi, dovizKurlari, "TL");

            SonKurBilgi = "Tarih : " + sonKurTarihi.Date.ToShortDateString() + " : 1 EUR-> " + euroTlKur;


            foreach (var item in NetsisStoklar)
            {
                var pariteEur = StokListViewModel.PariteGetir(item.DovizTipi, sonKurTarihi, dovizKurlari, "EUR");

                item.StokToplami_Euro = item.BAKIYE.GetValueOrDefault() *
                                        item.SonFiyat.GetValueOrDefault() *
                                        pariteEur;
            }


        }

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded);


        public bool FiyatSutunGorebilirMi
        {
            get
            {
                var gorebilirMi = (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI
                          || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATINALMA_YONETICI
                          || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATINALMA);


                return gorebilirMi;

            }
        }


        public bool HucreKodSadeceOkunurMu
        {
            get
            {
                var x = AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.HDEPO_UZM_USER
                       || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.HDEPO_SORUMLUSU;

                return !x;
            }
        }

        public bool MaliyetFiyatSutunGorebilirMi
        {
            get
            {
                var gorebilirMi = (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI);
                return gorebilirMi;

            }
        }

        private void OnFormLoaded()
        {
            ExportService.RestoreLayout(GridLayoutFileName);
        }

        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(GridLayoutFileName);
        }
        public StokListViewModel(string menuFormAd)
        {
            FormMenuAd = menuFormAd;

            OnEkranYenile(null);



        }

        // burayı kontrol et
        public vwStokTanim SeciliStok
        {
            get => seciliStok;
            set
            {
                try
                {
                    if (SeciliStok != null) SeciliStok.PropertyChanged -= SeciliStok_PropertyChanged;
                    value.PropertyChanged += SeciliStok_PropertyChanged;

                    SetProperty(ref seciliStok, value);

                }
                catch (Exception)
                {

                    ;
                }


            }
        }

        private void SeciliStok_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HucreKod")
            {
                repo.StokHucreGuncelle(SeciliStok.STOK_KODU, SeciliStok.HucreKod);
            }
        }

        UnitOfWork uow = new UnitOfWork();
        private void OnStokBarkodYazdir(string hucreKod)
        {
            var stok = SeciliStok;

            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(40);
            var dsObject = stok;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        public DelegateCommand<string> OncekiFiyatlariGosterCommand => new DelegateCommand<string>(OncekiFiyatlariGoster);


        private void OncekiFiyatlariGoster(string stokKod)
        {


            var fiyatListe = repo.StokAlimFiyatlariGetirFromStokKod(stokKod);

            StokFiyatListeWindow w = new StokFiyatListeWindow();
            w.DataContext = fiyatListe;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.ShowDialog();
        }

        public static decimal PariteGetir(string orjinal_DovizTipi, DateTime dovizKurTarih, List<DovizKur> DovizKurlari, string donusturulecek_DovizTipi = "EUR")
        {

            if (orjinal_DovizTipi == null) return 0;

            decimal parite = 1;

            if (orjinal_DovizTipi == donusturulecek_DovizTipi) return parite;

            var dovizKurlari = DovizKurlari.Where(c => c.Tarih == dovizKurTarih).ToList();

            if (dovizKurlari.Count == 0) return 0;

            if (dovizKurlari.Count == 0) dovizKurlari = DovizKurlari.Where(c => c.Tarih == dovizKurTarih.AddDays(-2)).ToList();


            if (orjinal_DovizTipi != "TL" && donusturulecek_DovizTipi == "TL")
            {
                parite = (decimal)dovizKurlari.Where(c => c.DovizAd == orjinal_DovizTipi).First().DovizSatis;
                return parite;
            }

            var dovizKuruOrjinal = orjinal_DovizTipi == "TL" ? 1 : dovizKurlari.Where(c => c.DovizAd == orjinal_DovizTipi).First().DovizSatis;
            var dovizKuru = dovizKurlari.Where(c => c.DovizAd == donusturulecek_DovizTipi).First().DovizSatis;


            parite = (decimal)(dovizKuruOrjinal / dovizKuru);
            return Math.Round(parite, 4);
        }

    }
}
