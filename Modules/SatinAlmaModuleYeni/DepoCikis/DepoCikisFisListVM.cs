using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.Helper;
using mnd.UI.AppModules.AppModule;
using mnd.UI.Helper;
using mnd.UI.Modules.SatinAlmaModuleYeni.Stoklar;
using mnd.UI.Services;
using mnd.Logic.Persistence;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis
{
    public class DepoCikisFisListVM : MyDxViewModelBase
    {
        private DepoRepository repoDepo = new DepoRepository();
        private ObservableCollection<DepoCikisFisDTO> depoCikisFisListe;
        private ObservableCollection<FisKalemViewModel> depoCikisFisListeKalemli;
        private string sonKurBilgi;

        public DelegateCommand YeniCommand => new DelegateCommand(OnYeniFis);

        public DelegateCommand<int> YilaGoreFiltreleCommand => new DelegateCommand<int>(OnFiltrele);

        private UnitOfWork uow = new UnitOfWork();


        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => YetkiliMi_FromDb(nameof(ExcelExportCommand)));

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "depocikis.xlsx");
        }

        private void OnFiltrele(int yil)
        {
            OnEkranTazele(yil);
        }

        public DelegateCommand<object> FisEditCommand => new DelegateCommand<object>(this.OnEditFis);

        private void OnEditFis(object satir)
        {
            string fisNo = "";

            if (satir is DepoCikisFisDTO) fisNo = (satir as DepoCikisFisDTO).FisNo;

            if (satir is FisKalemViewModel) fisNo = (satir as FisKalemViewModel).FisNo;

            var vm = new DepoCikisVM(fisNo, "Edit");

            var doc = AppPandap.pDocumentManagerService.CreateDocument("DepoCikisView", vm);
            doc.Title = "Depo Çıkış";
            doc.Show();
        }

        private void OnYeniFis()
        {
            var vm = new DepoCikisVM("", "Yeni");

            var doc = AppPandap.pDocumentManagerService.CreateDocument("DepoCikisView", vm);
            doc.Title = "Depo Çıkış";
            doc.Show();
        }

        public ObservableCollection<DepoCikisFisDTO> DepoCikisFisListe { get => depoCikisFisListe; set => SetProperty(ref depoCikisFisListe, value); }

        public ObservableCollection<FisKalemViewModel> DepoCikisFisListeKalemli
        { get => depoCikisFisListeKalemli; set => SetProperty(ref depoCikisFisListeKalemli, value); }

        public DepoCikisFisListVM(string formAdi)
        {
            OnEkranTazele(2021);

            FormMenuAd = formAdi;

            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);

            Messenger.Default.Register<KayitIslemEvent<DepoCikisFisDTO>>(this, KayitEklendi);
        }

        private void KayitEklendi(KayitIslemEvent<DepoCikisFisDTO> obj)
        {
            DepoCikisFisListe.Insert(0, obj.Kayit);

            KalemGorunumuTazele();
        }

        public void KalemGorunumuTazele()
        {
            StokTanimNetsisRepository repoNetsisFiyatlar = new StokTanimNetsisRepository();

            var dict = repoNetsisFiyatlar.StokAlimSonFiyatlariGetir().ToDictionary(c => c.StokKod);

            var dovizKurlari = NetsisService.NetsisDovizKurlariniGetirSonKayittan();
            var sonKurTarihi = dovizKurlari.First().Tarih.Value.Date;
            var euroTlKur = StokListViewModel.PariteGetir("EUR", sonKurTarihi, dovizKurlari, "TL");

            SonKurBilgi = "Tarih : " + sonKurTarihi.Date.ToShortDateString() + " : 1 EUR-> " + euroTlKur;

            if (!MaliyetFiyatSutunGorebilirMi) dict.Clear();

            DepoCikisFisListeKalemli = DepoCikisFisListe.SelectMany(fisDTO => fisDTO.KalemlerDTO,
                    (fis, kalem) => new { fis, kalem })
                .Select(s => new FisKalemViewModel
                {
                    FisNo = s.fis.FisNo,
                    FisTarihi = s.fis.FisTarihi,
                    MasrafMerkeziAd = s.fis.MasrafMerkeziAd,
                    TalepEdenKisi = s.fis.TalepEdenKisi,
                    TeslimAlanKisi = s.fis.TeslimAlanKisi,
                    IlgiliUnite=s.kalem.IlgiliUnite,
                    StokKodu = s.kalem.StokKodu,
                    StokAd = s.kalem.StokAd,
                    CikisMiktar = s.kalem.CikisMiktar,
                    OlcuBirimAd = s.kalem.OlcuBirimAd?.ToUpper(),

                    BirimFiyat = dict.ContainsKey(s.kalem.StokKodu) == true ? dict[s.kalem.StokKodu].BirimFiyat : 0,
                    DovizTip = dict.ContainsKey(s.kalem.StokKodu) == true ? dict[s.kalem.StokKodu].DovizTip : null,

                })
                .OrderByDescending(c => c.FisTarihi)
                .ToObservableCollection<FisKalemViewModel>();

            if (!MaliyetFiyatSutunGorebilirMi) return;

            foreach (var item in DepoCikisFisListeKalemli)
            {
                item.Toplam_Euro = item.CikisMiktar * item.BirimFiyat.GetValueOrDefault() *
                                   StokListViewModel.PariteGetir(item.DovizTip, sonKurTarihi, dovizKurlari, "EUR");
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

        public string SonKurBilgi { get => sonKurBilgi; set => SetProperty(ref sonKurBilgi , value); }

        private void OnEkranTazele(int yil)
        {
            if (yil == 2019)
                DepoCikisFisListe = repoDepo.DepoFisListesiGetir2019().ToObservableCollection();

            if (yil == 2020)
                DepoCikisFisListe = repoDepo.DepoFisListesiGetir2020().ToObservableCollection();

            if (yil == 2021)
                DepoCikisFisListe = repoDepo.DepoFisListesiGetir().ToObservableCollection();

            KalemGorunumuTazele();
        }
    }
}