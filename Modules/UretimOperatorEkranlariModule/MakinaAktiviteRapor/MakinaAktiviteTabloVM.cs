using DevExpress.Mvvm;
using mnd.Logic.BC_App;
using mnd.Logic.BC_Uretim;
using mnd.UI.Helper;
using mnd.UI.Modules.UretimIsletmeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule.MakinaDurusRapor
{
    public class MakinaDurusTabloVM : MyDxViewModelBase
    {
        public List<Makina> MakinaListe { get; set; }
        public DelegateCommand VeriGetirCommand => new DelegateCommand(VerileriYukle);

        public DateTime BaslamaTarih
        {
            get => baslamaTarih;
            set
            {
                baslamaTarih = value;
                VerileriYukle();
            }
        }
        public DateTime BitisTarih
        {
            get => bitisTarih;
            set
            {
                bitisTarih = value;
                VerileriYukle();
            }
        }

        public int MakinaKod
        {
            get => makinaKod;
            set
            {
                makinaKod = value;
                VerileriYukle();

            }
        }

        public string FormUyariMesaj { get => formUyariMesaj; set => SetProperty(ref formUyariMesaj, value); }

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, true);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }

        public MakinaAktiviteKayitRepository repo = new MakinaAktiviteKayitRepository();

        public List<MakinaAktiviteKayit> UretimTabloSatirlar
        { get => uretimTabloSatirlar; set => SetProperty(ref uretimTabloSatirlar, value); }

        public List<MakinaDurusTanim> MakinaDuruslari { get; }

        IsMerkeziRepository repoIsMerkezi = new IsMerkeziRepository();
        private List<MakinaAktiviteKayit> uretimTabloSatirlar;
        private DateTime baslamaTarih;
        private DateTime bitisTarih;
        private int makinaKod;
        private string formUyariMesaj;

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, true);

        public bool IsFormLoaded { get; set; } = false;

        private void OnFormLoaded()
        {
            IsFormLoaded = true;
            VerileriYukle();
        }

        private void Load(string formAdi)
        {
            IsFormLoaded = true;
            VerileriYukle();
        }
        public MakinaDurusTabloVM(string form)
        {
            MakinaListe = repoIsMerkezi.UretimMakinalariGetir()
                 .Select(c => new Makina { MakinaKod = c.Kod, MakinaAd = c.Tanim })
                 .ToList();

            MakinaListe.Insert(0, new Makina { MakinaKod = 0, MakinaAd = "Seçiniz" });

            MakinaDuruslari = UretimDataService.MakinaDuruslariGetir();

            BaslamaTarih = DateTime.Now.Date;
            BitisTarih = DateTime.Now.Date;
        }

        public async void VerileriYukle()
        {
            FormUyariMesaj = "Yükleniyor";
            UretimTabloSatirlar = null;

            repo = new MakinaAktiviteKayitRepository();

            UretimTabloSatirlar = await repo.UretimTabloGetirTarihAraligiAsync(BaslamaTarih, BitisTarih, "DH1");

            foreach (var item in UretimTabloSatirlar)
            {
                item.MakinaDuruşAd = MakinaDuruslari.Where(c => c.DurusKod == item.DuruşKodu).FirstOrDefault()?.DurusAd;
            }

            FormUyariMesaj = "";

        }
    }
}
