using DevExpress.DashboardCommon;
using DevExpress.DataAccess;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic.Model.App;
using mnd.Logic.Model.Netsis;
using mnd.Logic.Persistence;
using mnd.Logic.Persistence.Repositories;
using mnd.UI.AppModules.AppModule;
using mnd.UI.AppModules.AppModule;
using mnd.UI.Helper;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.Dashboard
{
    public class SatislarVM : MyDxViewModelBase
    {
        public ObservableCollection<DataPoint> DataTonaj { get => data; set => SetProperty(ref data, value); }

        public ObservableCollection<DataPoint> DataGelirler { get => dataGelirler; set => SetProperty(ref dataGelirler, value); }

        public ObservableCollection<DataPoint> DataTonajZaman { get => dataTonajZaman; set => SetProperty(ref dataTonajZaman, value); }

        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");


        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, CanExcelExport);


        public DelegateCommand<object> IsZekasiAcCommand => new DelegateCommand<object>(OnIsZekasiAc);

        private void OnIsZekasiAc(object obj)
        {
            var cultureName = "";

            if (RaporDovizTip == "TL") cultureName = "tr-TR";
            if (RaporDovizTip == "USD") cultureName = "en-US";
            if (RaporDovizTip == "EUR") cultureName = "de-DE";
            if (RaporDovizTip == "GBP") cultureName = "en-GB";

             DashboardWindow w = new DashboardWindow();
  
            w.MainDashBoard.ObjectDataSourceLoadingBehavior = DocumentLoadingBehavior.LoadSafely;
            w.MainDashBoard.Dashboard.CurrencyCultureName = cultureName;
            w.MainDashBoard.AsyncDataLoading += MainDashBoard_AsyncDataLoading;

            w.Show();
        }

        private void MainDashBoard_AsyncDataLoading(object sender, DevExpress.DashboardCommon.DataLoadingEventArgs e)
        {
            e.Data = new BindingList<IrsaliyePaletOzetDTO>(IrsaliyeVisibleData);
        }

        private bool CanExcelExport(object arg)
        {
            return true;

            var yetkiliMi= YetkiliMi_FromDb(nameof(ExcelExportCommand));

            return yetkiliMi;
        }

        private void OnExcelExport(object obj)
        {
            ExportService1.ExportTo(ExportType.XLSX, "export.xls");
        }
        public ObservableCollection<DataPoint> DataGelirZaman { get => gelirZaman; set => SetProperty(ref gelirZaman, value); }

        public List<Kullanici> SatisPersonelleri { get; set; }
        public List<IrsaliyePaletOzetDTO> Irsaliyeler { get => _irsaliyeler; set => SetProperty(ref _irsaliyeler, value); }
        public List<DovizKur> DovizKurlari { get; private set; }

        public string PieTotalTextPattern => "Toplam \r\n{TV:N0} " + DovizHelper.SimgeyeDonustur(RaporDovizTip);
        public string PieTextPattern => "{A}:  {V:N0} " + DovizHelper.SimgeyeDonustur(RaporDovizTip);

        public string CizgiTextPattern => "{}{S}: {V:n0} "  +DovizHelper.SimgeyeDonustur(RaporDovizTip);

        public string ChartNumberFormat
        {
            get => chartNumberFormat; 
            set
            {
               SetProperty(ref chartNumberFormat ,value);
            }
        }
        public List<IrsaliyePaletOzetDTO> IrsaliyeVisibleData
        { get => ırsaliyeListeKalemliVisibleData; set => SetProperty(ref ırsaliyeListeKalemliVisibleData, value); }

        public DelegateCommand FiltreBittiCommand => new DelegateCommand(GridControlFiltreBitti, () => true);

        private void GridControlFiltreBitti()
        {
            GrafikleriYukle();

        }

        public bool IsLoading { get => isLoading; set => SetProperty(ref isLoading, value); }

        public string RaporDovizTip
        {
            get => raporDovizTip;
            set
            {
                if (SetProperty(ref raporDovizTip, value))
                {
                    ChartNumberFormat = "{Argument}:{ S} {V:n0} ";
                    
                    OnEkraniTazele();
                }

            }
        }

        UnitOfWork uow = new UnitOfWork();
        private List<IrsaliyePaletOzetDTO> _irsaliyeler;
        private List<IrsaliyePaletOzetDTO> ırsaliyeListeListeKalemli;
        private string raporDovizTip;

        public string SeciliTonajIstatistik
        {
            get => seciliTonajIstatistik;
            set
            {
                SetProperty(ref seciliTonajIstatistik, value);
                GrafikleriYukle();
            }
        }


        public string SeciliGelirIstatistik
        {
            get => gelirIstatistikleri;
            set
            {
                SetProperty(ref gelirIstatistikleri, value);
                GrafikleriYukle();

            }
        }

        public string SeciliTonajZamanIstatistik
        {
            get => tonajZamanIstatistik;
            set
            {
                tonajZamanIstatistik = value;
                GrafikleriYukle();
            }
        }

        public string SeciliGelirZamanIstatistik
        {
            get => seciliGelirZamanIstatistik;
            set
            {
                seciliGelirZamanIstatistik = value;
                GrafikleriYukle();

            }
        }

        private ObservableCollection<DataPoint> data;
        private string seciliTonajIstatistik;
        private string gelirIstatistikleri;
        private ObservableCollection<DataPoint> dataGelirler;
        private bool isLoading;
        private List<IrsaliyePaletOzetDTO> ırsaliyeListeKalemliVisibleData;
        private ObservableCollection<DataPoint> dataTonajZaman;
        private string tonajZamanIstatistik;
        private ObservableCollection<DataPoint> gelirZaman;
        private string seciliGelirZamanIstatistik;
        private string chartNumberFormat;

        public List<IrsaliyePaletOzetDTO> IrsaliyeListeListeKalemli
        {
            get => ırsaliyeListeListeKalemli;
            set => SetProperty(ref ırsaliyeListeListeKalemli, value);
        }

        public SatislarVM(string formMenuAd)
        {
            IsLoading = false;
            DovizKurlari = NetsisService.NetsisBelirliTarihtenSonrakiDovizKurlariniGetir(DateTime.Now.Date.AddDays(-3000))
                           .OrderByDescending(c => c.Tarih).ToList();

            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, formMenuAd);

            RaporDovizTip = "EUR";

        }

        public SatislarVM()
        {
            IsLoading = false;
        }

        public void GrafikleriYukle()
        {

                this.DataTonaj = DataPoint.GetDataPoints(IrsaliyeVisibleData, SeciliTonajIstatistik);
                this.DataGelirler = DataPoint.GetGelirDataPoints(IrsaliyeVisibleData, SeciliGelirIstatistik);
                this.DataTonajZaman = DataPoint.GetDataTonajZamanPoints(IrsaliyeVisibleData, SeciliTonajZamanIstatistik);
                this.DataGelirZaman = DataPoint.GetGelirDataZamanPoints(IrsaliyeVisibleData, SeciliGelirZamanIstatistik);

                OnPropertyChanged(nameof(DataTonaj));
                OnPropertyChanged(nameof(DataGelirler));
                OnPropertyChanged(nameof(DataTonajZaman));
                OnPropertyChanged(nameof(DataGelirZaman));
   
        }

        private async void OnEkraniTazele()
        {
            IsLoading = true;

            var ulkeLangLong = JsonConvert.DeserializeObject<List<UlkeEnlemBoylam>>(JsonData.JsonUlkeLatLong).ToDictionary(c => c.country);

           

         

            uow = new UnitOfWork();

            Irsaliyeler = await uow.MuhasebeRepo.IrsaliyeOzetGetir("AKTARILDI");


            var ulkeKodBos = Irsaliyeler.Where(c => c.UlkeKod == null).ToList();

            foreach (var item in Irsaliyeler)
            {
                item.ToplamFiyat = Math.Round(item.ToplamFiyat, 2);

                if (item.Doviz != null)
                {
                    item.Parite = PariteGetir(item.Doviz, item.IrsaliyeTarihi.GetValueOrDefault(), RaporDovizTip);
                    item.ToplamFiyatSeciliDoviz = item.Parite * item.ToplamFiyat;
                    item.IscilikBF_Doviz = item.Parite * item.IscilikBF;

                    item.Iscilik_Doviz_Toplam = item.Parite * item.IscilikBF * item.PaletNet_Ton;

                    item.KulceBF_Doviz= item.Parite * item.KulceBF;
                    item.Kulce_Doviz_Toplam = item.Parite * item.KulceBF * item.PaletNet_Ton;


                    item.UlkeEnlem = ulkeLangLong[item.UlkeKod.ToUpper()].latitude;
                    item.UlkeBoylam = ulkeLangLong[item.UlkeKod.ToUpper()].longitude;

                }

                item.ToplamFiyatSeciliDoviz = Math.Round(item.ToplamFiyatSeciliDoviz, 2);

            }

            IrsaliyeListeListeKalemli = Irsaliyeler;
            IrsaliyeVisibleData = Irsaliyeler.Select(c => c).ToList();

            IsLoading = false;

            GrafikleriYukle();



        }

        public decimal PariteGetir(string orjinal_DovizTipi, DateTime dovizKurTarih, string donusturulecek_DovizTipi = "EUR")
        {
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


    public class KeyDataAlan
    {
        public int Week { get; set; }
        public int Mounth { get; set; }
        public string Year { get; set; }
        public decimal Value { get; set; }
    }
    public class DataPoint
    {
        public string Argument { get; set; }
        public double Value { get; set; }

        public List<KeyDataAlan> Values { get; set; }

        public static ObservableCollection<DataPoint> GetDataPoints(List<IrsaliyePaletOzetDTO> temelData, string ıstatistikAd)
        {
            ObservableCollection<DataPoint> datalar = new ObservableCollection<DataPoint>();
            if (ıstatistikAd == "Ulke")
            {
                datalar= temelData.GroupBy(c => c.UlkeKod)
                  .Select(c => new DataPoint { Argument = c.Key, Value = c.Sum(p => p.PaletNet_Kg) })
                  .ToObservableCollection();
            }
            
            if (ıstatistikAd== "Plasiyer")
            {
                datalar = temelData.GroupBy(c => c.PlasiyerAd)
               .Select(c => new DataPoint { Argument = c.Key, Value = c.Sum(p => p.PaletNet_Kg) })
               .ToObservableCollection();
            }

            if (ıstatistikAd == "KullanimAlan")
            {
                datalar = temelData.GroupBy(c => c.KullanimAlani)
               .Select(c => new DataPoint { Argument = c.Key, Value = c.Sum(p => p.PaletNet_Kg) })
               .ToObservableCollection();
            }

            if (ıstatistikAd == "Kalinlik")
            {
                datalar = temelData.GroupBy(c => c.KalinlikGrup)
               .Select(c => new DataPoint { Argument = c.Key, Value = c.Sum(p => p.PaletNet_Kg) })
               .ToObservableCollection();
            }


            return datalar;

        }

        internal static ObservableCollection<DataPoint> GetDataTonajZamanPoints(List<IrsaliyePaletOzetDTO> temelData, string ıstatistikAd)
        {
            ObservableCollection<DataPoint> datalar = new ObservableCollection<DataPoint>();
            if (ıstatistikAd == "Ulke")
            {
                datalar = temelData.GroupBy(c => new { c.UlkeKod} )
                  .Select(c => new DataPoint
                  {
                      Argument = c.Key.UlkeKod,
                      Values = c.Select(p => new KeyDataAlan { Year = p.YilHafta, Value = p.PaletNet_Kg }).ToList()
                            .GroupBy(x => x.Year).Select(s => new KeyDataAlan { Year=s.Key, Value=s.Sum(a=>a.Value)}).ToList()
                      })
                  .ToObservableCollection();
            }

            if (ıstatistikAd == "Plasiyer")
            {
                datalar = temelData.GroupBy(c => new { c.PlasiyerAd })
                   .Select(c => new DataPoint
                   {
                       Argument = c.Key.PlasiyerAd,
                       Values = c.Select(p => new KeyDataAlan { Year = p.YilHafta, Value = p.PaletNet_Kg }).ToList()
                             .GroupBy(x => x.Year).Select(s => new KeyDataAlan { Year = s.Key, Value = s.Sum(a => a.Value) }).ToList()
                   })
                   .ToObservableCollection();
            }

            if (ıstatistikAd == "KullanimAlan")
            {
                datalar = temelData.GroupBy(c => new { c.KullanimAlani })
                 .Select(c => new DataPoint
                 {
                     Argument = c.Key.KullanimAlani,
                     Values = c.Select(p => new KeyDataAlan { Year = p.YilHafta, Value = p.PaletNet_Kg }).ToList()
                           .GroupBy(x => x.Year).Select(s => new KeyDataAlan { Year = s.Key, Value = s.Sum(a => a.Value) }).ToList()
                 })
                 .ToObservableCollection();
            }

            if (ıstatistikAd == "Kalinlik")
            {
                datalar = temelData.GroupBy(c => new { c.KalinlikGrup })
                  .Select(c => new DataPoint
                  {
                      Argument = c.Key.KalinlikGrup,
                      Values = c.Select(p => new KeyDataAlan { Year = p.YilHafta, Value = p.PaletNet_Kg }).ToList()
                            .GroupBy(x => x.Year).Select(s => new KeyDataAlan { Year = s.Key, Value = s.Sum(a => a.Value) }).ToList()
                  })
                  .ToObservableCollection();
            }


            return datalar;
        }

        internal static ObservableCollection<DataPoint> GetGelirDataPoints(List<IrsaliyePaletOzetDTO> temelData, string ıstatistikAd)
        {
            ObservableCollection<DataPoint> datalar = new ObservableCollection<DataPoint>();
            if (ıstatistikAd == "Ulke")
            {
                datalar = temelData.GroupBy(c => c.UlkeAd)
                  .Select(c => new DataPoint { Argument = c.Key, Value = (double) (c.Sum(p => p.ToplamFiyatSeciliDoviz)) })
                  .ToObservableCollection();
            }

            if (ıstatistikAd == "Plasiyer")
            {
                datalar = temelData.GroupBy(c => c.PlasiyerAd)
               .Select(c => new DataPoint { Argument = c.Key, Value = (double) (c.Sum(p => p.ToplamFiyatSeciliDoviz)) })
               .ToObservableCollection();
            }

            if (ıstatistikAd == "KullanimAlan")
            {
                datalar = temelData.GroupBy(c => c.KullanimAlani)
               .Select(c => new DataPoint { Argument = c.Key, Value = (double) c.Sum(p => p.ToplamFiyatSeciliDoviz) })
               .ToObservableCollection();
            }

            if (ıstatistikAd == "Kalinlik")
            {
                datalar = temelData.GroupBy(c => c.KalinlikGrup)
               .Select(c => new DataPoint { Argument = c.Key, Value = (double) c.Sum(p => p.ToplamFiyatSeciliDoviz) })
               .ToObservableCollection();
            }


            return datalar;
        }



        internal static ObservableCollection<DataPoint> GetGelirDataZamanPoints(List<IrsaliyePaletOzetDTO> temelData, string ıstatistikAd)
        {
            ObservableCollection<DataPoint> datalar = new ObservableCollection<DataPoint>();
            if (ıstatistikAd == "Ulke")
            {
                datalar = temelData.GroupBy(c => new { c.SahipSirket })
                  .Select(c => new DataPoint
                  {
                      Argument = c.Key.SahipSirket,
                      Values = c.Select(p => new KeyDataAlan { Year = p.YilHafta, Value = p.ToplamFiyatSeciliDoviz }).ToList()
                            .GroupBy(x => x.Year).Select(s => new KeyDataAlan { Year = s.Key, Value = s.Sum(a => a.Value) }).ToList()
                  })
                  .ToObservableCollection();
            }

            if (ıstatistikAd == "Plasiyer")
            {
                datalar = temelData.GroupBy(c => new { c.PlasiyerAd })
                   .Select(c => new DataPoint
                   {
                       Argument = c.Key.PlasiyerAd,
                       Values = c.Select(p => new KeyDataAlan { Year = p.YilHafta, Value = p.ToplamFiyatSeciliDoviz }).ToList()
                             .GroupBy(x => x.Year).Select(s => new KeyDataAlan { Year = s.Key, Value = s.Sum(a => a.Value) }).ToList()
                   })
                   .ToObservableCollection();
            }

            if (ıstatistikAd == "KullanimAlan")
            {
                datalar = temelData.GroupBy(c => new { c.KullanimAlani })
                 .Select(c => new DataPoint
                 {
                     Argument = c.Key.KullanimAlani,
                     Values = c.Select(p => new KeyDataAlan { Year = p.YilHafta, Value = p.ToplamFiyatSeciliDoviz }).ToList()
                           .GroupBy(x => x.Year).Select(s => new KeyDataAlan { Year = s.Key, Value = s.Sum(a => a.Value) }).ToList()
                 })
                 .ToObservableCollection();
            }

            if (ıstatistikAd == "Kalinlik")
            {
                datalar = temelData.GroupBy(c => new { c.KalinlikGrup })
                  .Select(c => new DataPoint
                  {
                      Argument = c.Key.KalinlikGrup,
                      Values = c.Select(p => new KeyDataAlan { Year = p.YilHafta, Value = p.ToplamFiyatSeciliDoviz }).ToList()
                            .GroupBy(x => x.Year).Select(s => new KeyDataAlan { Year = s.Key, Value = s.Sum(a => a.Value) }).ToList()
                  })
                  .ToObservableCollection();
            }


            return datalar;
        }
    }
}
