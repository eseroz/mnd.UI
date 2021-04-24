using mnd.Common.EntityHelpers;
using mnd.Logic.BC_Finans;
using System.Collections.Generic;
using System.Linq;

namespace mnd.UI.Modules.FinansModule
{
    public class KonsolideFinansalRaporVM : MyBindableBaseLite
    {
        private string raporDovizTip;
        private List<vwBanka> bankaRaporDovizFiltreli;

        public string RaporDovizTip
        {
            get => raporDovizTip;
            set
            {
                if (SetProperty(ref raporDovizTip, value))
                {
                    OnEkraniYenile();
                }

            }
        }

        private void OnEkraniYenile()
        {
            BankaRaporDovizFiltreli = BankaRapor.Where(c => c.DovizAd == RaporDovizTip).ToList();
        }

        public KonsolideFinansalRaporVM(string frm)
        {
            FinansSevice servis = new FinansSevice();


            BankaRapor = servis.BankaRaporGetir_HazirDegerler();

            foreach (var item in BankaRapor)
            {
                if (item.DovizAd == "TL") item.DovizAd = "1-TL";
                if (item.DovizAd == "USD") item.DovizAd = "2-USD";
                if (item.DovizAd == "EUR") item.DovizAd = "3-EUR";
                if (item.DovizAd == "GBP") item.DovizAd = "4-GBP";


            }




            var firmalar = "Panda;PandaVan;Panab;Pantech;Seherli;PanabTek".Split(';').ToList();
            var aktifPasif = " HAZIR;BORC".Split(';').ToList();

            var _hesapTip = "";

            foreach (var firma in firmalar)
            {

                foreach (var aktif_pasif in aktifPasif)
                {
                    if (aktif_pasif == " HAZIR") _hesapTip = " KASA";
                    if (aktif_pasif == "BORC") _hesapTip = "KREDİLER";

                    var vwBankaTL = new vwBanka { Firma = firma, AktifPasif = aktif_pasif, DovizAd = "1-TL", HesapTip = _hesapTip, Bakiye = 0 };
                    var vwBankaUSD = new vwBanka { Firma = firma, AktifPasif = aktif_pasif, DovizAd = "2-USD", HesapTip = _hesapTip, Bakiye = 0 };
                    var vwBankaEUR = new vwBanka { Firma = firma, AktifPasif = aktif_pasif, DovizAd = "3-EUR", HesapTip = _hesapTip, Bakiye = 0 };
                    var vwBankaGBP = new vwBanka { Firma = firma, AktifPasif = aktif_pasif, DovizAd = "4-GBP", HesapTip = _hesapTip, Bakiye = 0 };

                    BankaRapor.Add(vwBankaTL);
                    BankaRapor.Add(vwBankaUSD);
                    BankaRapor.Add(vwBankaEUR);
                    BankaRapor.Add(vwBankaGBP);


                }
            }





            RaporDovizTip = "2-USD";

        }

        public List<vwBanka> BankaRapor { get; set; }

        public List<vwBanka> BankaRaporDovizFiltreli
        { get => bankaRaporDovizFiltreli; set => SetProperty(ref bankaRaporDovizFiltreli, value); }

    }
}
