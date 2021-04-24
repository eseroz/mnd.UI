using mnd.Common;
using mnd.Logic.Model;
using mnd.Logic.Model.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.PlanlamaModule
{
    public class AnakartKartVerimModel : MyBindableBase
    {
        private decimal kombin_Verimi_yuzde;
        private decimal kombin_Fire_yuzde;
        private decimal geometrik_Fire_yuzde;
        private decimal genel_Verim_Yuzde;
        private decimal ısletme_Fire_yuzde;
        private List<UretimEmri> uretimEmirleri;
        private List<PlanlamaTakipDto> kartListe;

        public AnakartKartVerimModel()
        {
            KartListe = new List<PlanlamaTakipDto>();
        }

        public string AnaKartNo { get; set; }
        public string SonPaketlemeTarihi { get; set; }

        public string MaxKombinEni_mm { get; set; }
        public string Kombin_Eni_mm { get; set; }
        public string Kombin_Miktari_kg { get; set; }

        public DateTime? AnakartBaslamaTarihi => KartListe.FirstOrDefault()?.EklenmeTarih;
        public DateTime? AnaKart_Tamamlanma_Tarihi => KartListe.Any(t => t.KapatilmaTarihi == null) == true ? null :
                                                      KartListe.Max(k => k.KapatilmaTarihi);

        public string AnakartTamamlanmaYilHafta
        {
            get
            {
                if (AnaKart_Tamamlanma_Tarihi == null) return null;
                return AnaKart_Tamamlanma_Tarihi.GetValueOrDefault().Year.ToString() + "/" +
                CalenderUtil.GetWeekNumberFromDate(AnaKart_Tamamlanma_Tarihi.GetValueOrDefault());
            }
        }

        public string AnakartTamamlanmaYilAy
        {
            get
            {
                if (AnaKart_Tamamlanma_Tarihi == null) return null;
                return AnaKart_Tamamlanma_Tarihi.GetValueOrDefault().Year.ToString() + "/" +
                CalenderUtil.GetMounthName(AnaKart_Tamamlanma_Tarihi.GetValueOrDefault().Month);
            }
        }

        public string KullanimAlanUrunGrup => KartListe.FirstOrDefault()?.KullanimAlanUrunGrup;
        public int AnakartTamamlanmaYuzde
        {
            get
            {
                if (kartListe.Count == 0) return 0;
                return (int)(((decimal)KartListe.Count(k => k.KapatildiMi == true) / (decimal)KartListe.Count) * 100);
            }
        }

        public int Anakart_Toplam_Paket_Miktar_kg => KartListe.Sum(k => k.PaketlenenMiktar);

        public decimal Kombin_Eni_AgirOrt_mm => (decimal)KartListe
                                        .Where(k => k.KombinlerEnToplam != null && k.KombinMiktari_kg != null)
                                        .ToList()
                                        .WeightedAverage(x => x.KombinlerEnToplam.GetValueOrDefault(), y => y.KombinMiktari_kg.GetValueOrDefault(),
                                        "KombinEniAğrılıklıOrt-" + AnaKartNo);
        public decimal Kombin_Max_Eni_Agir_Ort_mm => (decimal)KartListe
                                         .Where(k => k.MaxKombinEni != null && k.KombinMiktari_kg != null)
                                         .ToList()
                                         .WeightedAverage(x => x.MaxKombinEni.GetValueOrDefault(), y => y.KombinMiktari_kg.GetValueOrDefault(),
                                          "KombinMaxEniAgirlikliOrt-" + AnaKartNo);

        public int DokumEni_mm
        {
            get
            {
                if (kartListe.Count == 0) return 0;
                return (int)KartListe.Max(k => k.DokumEni_mm.GetValueOrDefault());
            }
        }

        public int Dokum_Miktari_kg
        {
            get
            {
                return KartListe.Sum(k => k.KartPlanlamaRulolari.Sum(v => v.DokmeRuloAgirligi_kg));
            }
        }

        public decimal Kombin_Verimi_yuzde { get => kombin_Verimi_yuzde; set => SetProperty(ref kombin_Verimi_yuzde, value); }
        public decimal Kombin_Fire_yuzde { get => kombin_Fire_yuzde; set => SetProperty(ref kombin_Fire_yuzde, value); }
        public decimal Geometrik_Fire_yuzde { get => geometrik_Fire_yuzde; set => SetProperty(ref geometrik_Fire_yuzde, value); }
      
        public decimal Isletme_Fire_yuzde { get => ısletme_Fire_yuzde; set => SetProperty(ref ısletme_Fire_yuzde, value); }


        public decimal Genel_Verim_Yuzde { get => genel_Verim_Yuzde; set => SetProperty(ref genel_Verim_Yuzde, value); }



        public List<UretimEmri> UretimEmirleri { get => uretimEmirleri; set => SetProperty(ref uretimEmirleri, value); }
        public List<PlanlamaTakipDto> KartListe { get => kartListe; set => SetProperty(ref kartListe , value); }
    }
}
