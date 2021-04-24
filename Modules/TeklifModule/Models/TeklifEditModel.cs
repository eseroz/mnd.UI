using mnd.Logic.Model;
using System;
using System.Collections.ObjectModel;

namespace mnd.UI.Modules.TeklifModule.Models
{
    public class TeklifEditModel : MyBindableBase
    {
        private string tasimaSekli;
        private string tasimaSekliAdi_EN;
        private string teslimYeri;
        private string cariKod;
        private string cariAd;
        private string teklifSiraKod;
        private string cariDovizTipKod;
        private DateTime sonGecerlilikTarihi;
        private string ıletisimPersonelMail;
        private string ıletisimPersonelAdSoyad;
        private string ıletisimKisiUnvan;
        private string ıletisimKisiTel;
        private string potansiyelCariAd;
        private string bankaHesapKod;
        private string proformaNo;
        private string donusturulenSiparisKod;
        private string nakliyeDurumTip;

        public string DonusturulenSiparisKod { get => donusturulenSiparisKod; set => SetProperty(ref donusturulenSiparisKod, value); }
        public string BankaHesapKod { get => bankaHesapKod; set => SetProperty(ref bankaHesapKod, value); }
        public string TeklifSiraKod { get => teklifSiraKod; set => SetProperty(ref teklifSiraKod, value); }

        public string PlasiyerTeklifSiraKod { get; set; }


        public DateTime TeklifTarih { get; set; }

        public string SatisTemsilcisiAdSoyad { get; set; }


        public DateTime SonGecerlilikTarihi
        {

            get
            {
                return sonGecerlilikTarihi;
            }

            set
            {
                SetProperty(ref sonGecerlilikTarihi, value);

            }

        }

        public string CariKod { get => cariKod; set => SetProperty(ref cariKod, value); }

        public string CariAd { get => cariAd; set => SetProperty(ref cariAd, value); }

        public string PotansiyelCariAd { get => potansiyelCariAd; set => SetProperty(ref potansiyelCariAd, value); }
        public string CariDovizTipKod { get => cariDovizTipKod; set => SetProperty(ref cariDovizTipKod, value); }

        public int IletisimKisiId { get; set; }

        public string IletisimKisiAdSoyad { get => ıletisimPersonelAdSoyad; set => SetProperty(ref ıletisimPersonelAdSoyad, value); }

        public string IletisimKisiMail { get => ıletisimPersonelMail; set => SetProperty(ref ıletisimPersonelMail, value); }

        public string IletisimKisiUnvan { get => ıletisimKisiUnvan; set => SetProperty(ref ıletisimKisiUnvan, value); }

        public string IletisimKisiTel { get => ıletisimKisiTel; set => SetProperty(ref ıletisimKisiTel, value); }

        public string TeslimTipKod { get; set; }

        public string TeslimYeri { get => teslimYeri; set => SetProperty(ref teslimYeri, value); }
        public string TeslimNot { get; set; }

        public string TeslimYeriPostaKod { get; set; }

        public string LmeBelirlemeSekli { get; set; }

        public string OdemeSekliKod { get; set; }

        public string OdemeSekliDetay { get; set; }

        public int SevkYil { get; set; }
        public int SevkHafta { get; set; }

        public int TeslimYil { get; set; }
        public int TeslimHafta { get; set; }

        public string MiktarOlcuBirim { get; set; }

        public string Proforma_HSCODE { get; set; }

        public string TeklifGenelNot { get; set; }

        public string TeklifDurum { get; set; }
        public string IslemNot { get; set; }

        public string RetNeden { get; set; }

        public int MusteriIletisimKisiId { get; set; }

        public string TasimaSekliAdi_EN
        {
            get => tasimaSekliAdi_EN;
            set => SetProperty(ref tasimaSekliAdi_EN, value);
        }
        public string TasimaSekli { get => tasimaSekli; set => SetProperty(ref tasimaSekli, value); }

        public string NakliyeDurumTip { get => nakliyeDurumTip; set => SetProperty(ref nakliyeDurumTip , value); }
        public string GidecegiUlke { get; set; }

        public string ProformaNo { get => proformaNo; set => proformaNo = value; }

        public decimal NET_W_ROW_TOTAL { get; set; }
        public decimal GROSS_W_ROW_TOTAL { get; set; }
        public decimal VOLUME_M3_ROW_TOTAL { get; set; }
        public TeklifEditModel()
        {
            TeklifKalemlerDTO = new ObservableCollection<TeklifKalemEditModel>();

        }

        public ObservableCollection<TeklifKalemEditModel> TeklifKalemlerDTO { get; set; }
        public Guid RowGuid { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }
        public string SatisTemsilcisiMail { get; set; }
    }

}
