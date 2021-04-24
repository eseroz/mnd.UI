using System;
using System.Collections.Generic;

namespace mnd.UI.Modules.TeklifModule
{
    public class TeklifCiktiFormDto
    {
        private string proformaNo;

        //public string LME { get; set; }

        public string Quantity { get; set; }

        public string IncoTerms { get; set; }
        public string Incoterms { get; internal set; }

        public string PaymentTerms { get; set; }

        public string Delivery { get; set; }

        public string OfferRef { get; set; }

        public string ValidTill { get; set; }

        public string ThicknessAndWidth { get; set; }

        public string ConversionPriceTon { get; set; }
        public string FirmaSorumluAd { get; internal set; }

        public string TeklifFormGenelBaslik { get; internal set; }
        public string ConversionPrice { get; internal set; }

        public string BankaHesapListeAd { get; set; }

        public List<TeklifCiktiKalemDto> TeklifCiktiKalemDtoList { get; set; }
        public string TeklifGenelToplamStr { get; set; }
        public string TeklifGenelNot { get; set; }
        public string CariAd { get; internal set; }
        public string CariAdres { get; internal set; }
        public string CariTelefon { get; internal set; }
        public string CariFax { get; internal set; }

        public DateTime TeklifTarih { get; internal set; }
        public string FirmaIlgiliKisiTel { get; internal set; }
        public string FirmaIlgiliKisiMail { get; internal set; }
        public DateTime TeklifSonTarih { get; internal set; }

        public string SatisTemsilcisiAdSoyad { get; internal set; }
        public string SatisTemsilcisiMail { get; internal set; }

        public string TasimaSekliAdi { get; internal set; }
        public string GidecegiUlke { get; internal set; }

        public string BankaAdi { get; set; }

        public string BankaSube { get; set; }

        public string HesapNo { get; set; }

        public string DovizTipi { get; set; }

        public string ProformaNo { get => proformaNo; set => proformaNo = value; }


        public string FooterCompanyName { get; set; }
        public string FooterAddress { get; set; }

        public string FooterBank { get; set; }

        public string FooterAccountNr { get; set; }

        public string FooterIbanNr { get; set; }
        public string FooterSwiftCode { get; set; }

        public string MndFaturaBaslik { get; set; }
        public string MndAdsress { get; set; }
        public string MndTelefon { get; set; }
        public string MndFax { get; set; }
        public TeklifCiktiFormDto()
        {
            TeklifCiktiKalemDtoList = new List<TeklifCiktiKalemDto>();
        }
    }

    public class TeklifCiktiKalemDto
    {
        public string TeklifKalemSiraKod { get; set; }
        public string TeklifSiraKod { get; set; }
        public string TeklifKalemNot { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public decimal SatisFiyati { get; set; }
        public decimal Tutar { get; set; }
        public decimal Butce { get; set; }
        public int Miktar { get; set; }
        public int Id { get; set; }
        public string UrunKod { get; set; }
        public string UrunAdiTR { get; set; }
        public string UrunAdiEN { get; set; }
        public decimal? GR { get; set; }
        public decimal? PCS { get; set; }
        public decimal? BOX { get; set; }
        public decimal? NETKG { get; set; }
        public decimal? GROSS { get; set; }
        public decimal? W { get; set; }
        public decimal? L { get; set; }
        public decimal? H { get; set; }
        public decimal? M3 { get; set; }
        public decimal? CRTN { get; set; }
        public decimal NET_W_ROW_TOTAL { get { return ((decimal)this.NETKG * Miktar);  } }
        public decimal GROSS_W_ROW_TOTAL { get { return ((decimal)this.GROSS * Miktar); } }
        public decimal VOLUME_M3_ROW_TOTAL { get { return ((decimal)this.M3 * Miktar); } }
        public decimal KalemTutar { get { return (this.Miktar * this.SatisFiyati); } }
    }
}
