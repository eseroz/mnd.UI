using mnd.Logic.Model.Uretim;
using mnd.UI.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules.PlanlamaModule
{
    public class UretimEmriDTO : MyDxViewModelBase
    {
        private string kartNo;
        private ObservableCollection<UretimEmriMakineAsama1> makineAsamalari1;
        private ObservableCollection<UretimEmriMakineAsama2> makineAsamalari2;
        private int planlamaBakiye;
        private int planlananMiktar_Kg;
        public string Alasim { get; set; }
        public string AmbalajTipi { get; set; }


        public string EnToleransı_mm { get; set; }
        public string Hazırlayan { get; set; }
        public decimal HedefKalinlik { get; set; }
        public int KalemMiktar { get; internal set; }
        public string KalınlıkToleransı_mm { get; set; }

        private string kombinler;

        public string Kombinler
        {
            get
            {
                return kombinler;
            }
            set
            {
                if (SetProperty(ref kombinler, value) == true)
                {
                    try
                    {
                        var validKombinString = kombinler.Replace("   ", "  ").Replace("  ", " ").Replace("x", "X");



                        KombinlerEnToplam = validKombinString.Split(' ')
                                             .Select(c => double.Parse(c.Split('X')[1]) * double.Parse(c.Split('X')[0]))
                                             .ToList()
                                             .Sum();

                    }
                    catch (Exception)
                    {
                        ;
                    }
                }
            }
        }

        private double? kombinlerEnToplam;

        public double? KombinlerEnToplam
        {
            get => kombinlerEnToplam;
            set => SetProperty(ref kombinlerEnToplam, value);
        }

        public string PlanlamaNot { get; set; }
        public string DilmeSeperatorNot { get; set; }

        public string KartNo
        {
            get => kartNo;
            set => SetProperty(ref kartNo, value);
        }

        public string Kondusyon { get; set; }
        public string KullanimAlani { get; set; }

        public ObservableCollection<UretimEmriMakineAsama1> MakineAsamalari1
        {
            get => makineAsamalari1;
            set => SetProperty(ref makineAsamalari1, value);
        }

        public ObservableCollection<UretimEmriMakineAsama2> MakineAsamalari2
        {
            get => makineAsamalari2;
            set => SetProperty(ref makineAsamalari2, value);
        }

        public string MaksimumEkSayısı_mm { get; set; }
        public string MasuraBoyu { get; set; }
        public string MasuraCinsi { get; set; }
        public string Musteri { get; set; }
        public string Olcu { get; set; }
        public string Onay { get; set; }

        public string OzelTalimat { get; set; }

        public int PlanlamaBakiye
        {
            get => planlamaBakiye;
            set => SetProperty(ref planlamaBakiye, value);
        }

        private ObservableCollection<UretimEmriRulo> planlamaRulolari;

        public ObservableCollection<UretimEmriRulo> PlanlamaRulolari
        {
            get => planlamaRulolari;
            set => SetProperty(ref planlamaRulolari, value);
        }

        public UretimEmriDTO()
        {
            PlanlamaRulolari = new ObservableCollection<UretimEmriRulo>();
            MakineAsamalari1 = new ObservableCollection<UretimEmriMakineAsama1>();
            MakineAsamalari2 = new ObservableCollection<UretimEmriMakineAsama2>();
        }

        public int PlanlananMiktar_kg
        {
            get => planlananMiktar_Kg;
            set => SetProperty(ref planlananMiktar_Kg, value);
        }

        public string RuloDışçapMax { get; set; }
        public string RuloDışçapMin { get; set; }
        public string RuloIcCap_mm { get; set; }
        public string SevkTarihi { get; set; }
        public string SiparisKalemKod { get; set; }
        public string SiparisMiktarı { get; set; }

        private string uretimEmriKod;

        public string UretimEmriKod
        {
            get => uretimEmriKod;
            set => SetProperty(ref uretimEmriKod, value);
        }

        public string UrunKod { get; internal set; }
        public string ÜlkeEN { get; set; }
        public string Yüzey { get; set; }
        public string SonAsama1 { get; internal set; }
        public string SonAsama2 { get; internal set; }
        public string DilmeVeSeperatorAciklama { get; set; }
        public string Kombin { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public string AnaKartNo { get; internal set; }

        public int KombinMiktari_kg { get; set; }
        public int? MaxKombinEni { get;  set; }
        public string KaydiriciOraniMinMaxStr { get;  set; }
     
    }
}