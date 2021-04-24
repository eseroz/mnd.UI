using mnd.Logic.BC_Uretim;
using mnd.Logic.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mnd.Common;
using DevExpress.Mvvm;
using System.Windows;
using mnd.UI.Helper;
using mnd.Logic.Model.Uretim;

namespace mnd.UI.Modules.PlanlamaModule
{
    public class KartVerimListVM : MyDxViewModelBase
    {
        private UnitOfWork uow = new UnitOfWork();

        AnaKartRepository repo = new AnaKartRepository();
        private ObservableCollection<PlanlamaTakipDto> planlamaDtoListe;
        private AnakartKartVerimModel seciliKayit;
        private List<AnakartKartVerimModel> anakartVerimDTO_Liste;
        private List<AnakartKartVerimModel> anakartVerimDTO_TamamlananlarListe;

        public ObservableCollection<PlanlamaTakipDto> PlanlamaDtoListe
        {
            get => planlamaDtoListe;
            set => SetProperty(ref planlamaDtoListe, value);
        }

        public List<AnakartKartVerimModel> AnakartVerimDTO_Liste { get => anakartVerimDTO_Liste; set => SetProperty(ref anakartVerimDTO_Liste, value); }

        public List<AnakartKartVerimModel> AnakartVerimDTO_TamamlananlarListe { get => anakartVerimDTO_TamamlananlarListe; 
            set =>SetProperty(ref anakartVerimDTO_TamamlananlarListe, value); }


        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);

        public DelegateCommand<object> SatirGuncelleCommand => new DelegateCommand<object>(OnSatirGuncelle);

        public DelegateCommand<object> HesapDetayCommand => new DelegateCommand<object>(OnHesapDetay);

        private void OnHesapDetay(object obj)
        {
            KartVerimInfoWindow f = new KartVerimInfoWindow();
            f.Show();
        }

        private void OnSatirGuncelle(object obj)
        {
            if (obj is PlanlamaTakipDto)
            {
                var ue = obj as PlanlamaTakipDto;
                repo.UretimEmriVerimGuncelle(ue.UretimEmriKod, ue.KombinMiktari_kg.GetValueOrDefault(), ue.MaxKombinEni.GetValueOrDefault());

                var anakartDTO = AnakartVerimDTO_Liste.First(x => x.AnaKartNo == ue.AnaKartNo);
                YuzdeleriHesapla(anakartDTO);

                PivotTabloGuncelle();

                MessageBox.Show("Üretim emri güncellendi", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (obj is UretimEmriRulo)
            {
                var ur = obj as UretimEmriRulo;
                repo.UretimEmriRuloGuncelle(ur.Id, ur.DokumEni_mm, ur.DokmeRuloAgirligi_kg);

                var anakartDTO = AnakartVerimDTO_Liste.First(x => x.AnaKartNo == ur.UretimEmriKodNav.AnaKartNo);

                anakartDTO.OnPropertyChanged("DokumEni_mm");
                anakartDTO.OnPropertyChanged("DokmeRuloAgirligi_kg");

        
                YuzdeleriHesapla(anakartDTO);

                PivotTabloGuncelle();

                MessageBox.Show("Planlama rulo güncellendi", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>();
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport,
          c => true);

        public AnakartKartVerimModel SeciliKayit
        {
            get => seciliKayit;
            set => SetProperty(ref seciliKayit, value);
        }

        public KartVerimListVM(string formAd)
        {
            EkranYenile();
            SeciliKayit = AnakartVerimDTO_Liste.Last();
        }



        public void OnEkranYenile()
        {
            EkranYenile();
            SeciliKayit = AnakartVerimDTO_Liste.Last();
            MessageBox.Show("Ekran Yenilendi", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnExcelExport(object obj) => ExportService1.ExportTo(ExportType.XLSX,
          $"{"KartVerim".Replace(".xml", "")} {DateTime.Now.ToString("ddMMyyyy_HHmm")}.xlsx");
        private void EkranYenile()
        {
            uow = new UnitOfWork();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirUretimEmirleriPlanlamaTakipDtoVerim();

            var anakartListe = repo.AnaKartlariGetir()
                .OrderBy(c => c.AnakartNo)
                .ToList();




            var liste = anakartListe.Select(c => new AnakartKartVerimModel
            {
                AnaKartNo = c.AnakartNo,
                KartListe = PlanlamaDtoListe.Where(p => p.AnaKartNo == c.AnakartNo)
                             .OrderBy(x => x.KartNo).ToList()
            });

        
            var temp1 = 0;

            AnakartVerimDTO_Liste = liste
            .Select(o => YuzdeleriHesapla(o))
            .ToList();


            PivotTabloGuncelle();

        }

        private void PivotTabloGuncelle()
        {
            AnakartVerimDTO_TamamlananlarListe = AnakartVerimDTO_Liste
                                              .Where(c => c.AnaKart_Tamamlanma_Tarihi != null).ToList();
        }

        public AnakartKartVerimModel YuzdeleriHesapla(AnakartKartVerimModel o)
        {
            if (o.Kombin_Eni_AgirOrt_mm != 0 && o.Kombin_Max_Eni_Agir_Ort_mm != 0 && o.Dokum_Miktari_kg != 0 && o.DokumEni_mm != 0)
            {
                o.Kombin_Verimi_yuzde = o.Kombin_Eni_AgirOrt_mm / o.Kombin_Max_Eni_Agir_Ort_mm;

                o.Kombin_Fire_yuzde = 1 - o.Kombin_Verimi_yuzde;
                o.Geometrik_Fire_yuzde = 1 - o.Kombin_Max_Eni_Agir_Ort_mm / o.DokumEni_mm;

                o.Genel_Verim_Yuzde = (decimal)((decimal)o.Anakart_Toplam_Paket_Miktar_kg / (decimal)o.Dokum_Miktari_kg);

                o.Isletme_Fire_yuzde = 1 - (o.Kombin_Fire_yuzde + o.Geometrik_Fire_yuzde + o.Genel_Verim_Yuzde);
            }

            return o;
        }

    }
}
