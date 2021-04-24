using System;
using System.Collections.Generic;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.RaporCikti
{
    public class SatinAlma_SiparisDto
    {
        public string TedarikciAd { get; set; }
        public List<SatinAlma_SiparisKalemDto> SiparisKalemler { get; set; }
        public DateTime TalepTarihi { get; set; }
        public string TalepNo { get; set; }
        public string TeslimSekli { get; set; }
        public string OdemeSekli { get; set; }
        public string StokGrupAd { get; internal set; }
        public int SiparisNo { get; internal set; }
        public DateTime SiparisTarihi { get; internal set; }
        public int? TeklifNo { get; set; }
        public string DovizCinsi { get; set; }

        public decimal IndirimTutar { get; set; }
        public decimal AraToplam { get; internal set; }
        public decimal KdvToplam { get; internal set; }
        public decimal GenelToplam { get; internal set; }
        public decimal ToplamTutar { get; internal set; }

        public SatinAlma_SiparisDto()
        {
            SiparisKalemler = new List<SatinAlma_SiparisKalemDto>();
        }


    }


    public class SatinAlma_SiparisKalemDto
    {
        public string StokAd { get; set; }
        public decimal Miktar { get; internal set; }
        public string Birim { get; internal set; }
        public DateTime? TeslimTarihi { get; internal set; }
        public decimal BirimFiyat { get; internal set; }
        public int KdvOran { get; internal set; }

        public decimal Tutar { get; internal set; }
        public string Marka { get; internal set; }
        public DateTime? IstenilenTarih { get; internal set; }
    }
}
