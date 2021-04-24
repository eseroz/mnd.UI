using mnd.Logic.Model;
using System;

namespace mnd.UI.Modules.TeklifModule.Models
{
    public class TeklifKalemEditModel : MyBindableBase
    {
        private int id;
        private string urunKod;
        private string urunAdiTR;
        private string urunAdiEN;
        private decimal? gR;
        private decimal? pCS;
        private decimal? bOX;
        private decimal? nETKG;
        private decimal? gROSS;
        private decimal? w;
        private decimal? l;
        private decimal? h;
        private decimal? m3;
        private decimal? cRTN;
        private string teklifKalemSiraKod;
        private string teklifSiraKod;
        private decimal? satisFiyati;
        private decimal tutar;
        private decimal butce;
        private int miktar;
        private DateTime teslimTarihi;
        private string nakliyeDurumTip;
        private string teslimYil;
        private string donemGrup;
        private string donem;

        public string TeklifKalemSiraKod { get => teklifKalemSiraKod; set => SetProperty(ref teklifKalemSiraKod, value); }
        public string TeklifSiraKod { get => teklifSiraKod; set => SetProperty(ref teklifSiraKod, value); }
        public string NakliyeDurumTip { get => nakliyeDurumTip; set => SetProperty(ref nakliyeDurumTip, value); }
        public string TeslimYil { get => teslimYil; set => SetProperty(ref teslimYil, value); }
        public string DonemGrup { get => donemGrup; set => SetProperty(ref donemGrup, value); }
        public string Donem { get => donem; set => SetProperty(ref donem, value); }
        public string TeklifKalemNot { get; set; }
        public DateTime TeslimTarihi { get => teslimTarihi; set => teslimTarihi = value; }
        public decimal? SatisFiyati { get => satisFiyati; set => SetProperty(ref satisFiyati, value); }
        public decimal Tutar { get => tutar; set => SetProperty(ref tutar, value); }
        public decimal Butce { get => butce; set => SetProperty(ref butce, value); }
        public int Miktar { get => miktar; set => SetProperty(ref miktar, value); }
        public int Id { get => id; set => SetProperty(ref id, value); }
        public string UrunKod { get => urunKod; set => SetProperty(ref urunKod, value); }
        public string UrunAdiTR { get => urunAdiTR; set => SetProperty(ref urunAdiTR, value); }
        public string UrunAdiEN { get => urunAdiEN; set => SetProperty(ref urunAdiEN, value); }
        public decimal? GR { get => gR; set => SetProperty(ref gR, value); }
        public decimal? PCS { get => pCS; set => SetProperty(ref pCS, value); }
        public decimal? BOX { get => bOX; set => SetProperty(ref bOX, value); }
        public decimal? NETKG { get => nETKG; set => SetProperty(ref nETKG, value); }
        public decimal? GROSS { get => gROSS; set => SetProperty(ref gROSS, value); }
        public decimal? W { get => w; set => SetProperty(ref w, value); }
        public decimal? L { get => l; set => SetProperty(ref l, value); }
        public decimal? H { get => h; set => SetProperty(ref h, value); }
        public decimal? M3 { get => m3; set => SetProperty(ref m3, value); }
        public decimal? CRTN { get => cRTN; set => SetProperty(ref cRTN, value); }

    }
}
