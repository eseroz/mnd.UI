using Newtonsoft.Json;
using mnd.Logic.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace mnd.UI.Modules.TeklifModule.Models
{
    public class TeklifListModel : MyBindableBase
    {
        private string islemNot;
        private string retNeden;
        private string teklifDurum;
        private string donusturulenSiparisKod;
        private int _okunmamisMesajSayisi1;
        private int mesajSayisi;
        private DateTime sonGecerlilikTarihi;
        private bool potansiyel;

        public string TeklifSiraKod { get; set; }

        public string PlasiyerTeklifSiraKod { get; set; }

        public string TeklifDurum { get => teklifDurum; set => SetProperty(ref teklifDurum, value); }
        public string IslemNot { get => islemNot; set => SetProperty(ref islemNot, value); }
        public string RetNeden { get => retNeden; set => SetProperty(ref retNeden, value); }

        public bool Potansiyel { get => potansiyel; set => SetProperty(ref potansiyel, value); }

        public DateTime TeklifTarih { get; set; }

        public string SatisTemsilcisiAdSoyad { get; set; }


        public DateTime SonGecerlilikTarihi
        {
            get => sonGecerlilikTarihi;
            set => sonGecerlilikTarihi = value;
        }

        public string CariKod { get; set; }

        public string CariAd { get; set; }

        public string CariDovizTipKod { get; set; }

        public string IletisimPersonelAdSoyad { get; set; }

        public string IletisimPersonelMail { get; set; }

        public string TeslimTipKod { get; set; }

        public string TeslimYeri { get; set; }
        public string TeslimNot { get; set; }

        public string TeslimYeriPostaKod { get; set; }

        public string LmeBelirlemeSekli { get; set; }

        public string OdemeSekliKod { get; set; }

        public int SevkYil { get; set; }
        public int SevkHafta { get; set; }

        public int TeslimYil { get; set; }
        public int TeslimHafta { get; set; }

        public string MiktarOlcuBirim { get; set; }

        public string Proforma_HSCODE { get; set; }

        public string TeklifGenelNot { get; set; }
        public string DonusturulenSiparisKod { get => donusturulenSiparisKod; set => SetProperty(ref donusturulenSiparisKod, value); }
        public string CariPlasiyerKod { get; internal set; }



        [JsonIgnore]
        [NotMapped]
        public int MesajSayisi { get => mesajSayisi; set => SetProperty(ref mesajSayisi, value); }

        [JsonIgnore]
        [NotMapped]
        public int OkunmamisMesajSayisi { get => _okunmamisMesajSayisi1; set => SetProperty(ref _okunmamisMesajSayisi1, value); }

        public Guid RowGuid { get; set; }
    }
}
