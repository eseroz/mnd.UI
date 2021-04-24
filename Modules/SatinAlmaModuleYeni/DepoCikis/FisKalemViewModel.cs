using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using mnd.Logic.Model;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;


namespace mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis
{
    public class FisKalemViewModel : MyBindableBase
    {
        private decimal toplam_Euro;

        public string FisNo { get; set; }
        public DateTime FisTarihi { get; set; }
        public string TalepEdenKisi { get; set; }
        public string TeslimAlanKisi { get; set; }

        public string StokKodu { get; set; }

        public string StokAd { get; set; }
        public decimal CikisMiktar { get; set; }

        public string OlcuBirimAd { get; set; }
        public string MasrafMerkeziAd { get; internal set; }
        public decimal Toplam_Euro { get => toplam_Euro; set => SetProperty(ref toplam_Euro, value); }

        public decimal? BirimFiyat { get; internal set; }
        public string DovizTip { get; internal set; }
        public int MasrafMerkeziKod { get; internal set; }
        public string MasrafSeviye1 { get; internal set; }
        public string MasrafSeviye2 { get; internal set; }
        public string MasrafSeviye3 { get; internal set; }

        public string IlgiliUnite { get; set; }

        public decimal PariteEuro { get; set; }
    }
}