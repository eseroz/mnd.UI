using System;
using System.Collections.Generic;

namespace mnd.UI.Services
{
    public class SiparisTeyitDTO
    {
        public string SiparisTeyitNo { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public string MusteriAd { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }

        public string FaturaAdresi { get; set; }
        public string MusteriSiparisNo { get; set; }
        public string TeslimSekli { get; set; }
        public string OdemeSekliAdi { get; set; }
        public string OdemeSekliAciklama { get; set; }
        public string FaturaKurCinsi { get; set; }
        public string MusteriIlgiliAdSoyad { get; set; }
        public string MusteriIlgiliEmail { get; set; }
        public string TeslimatAdresi { get; set; }

        public string MiktarToleransi { get; set; }
        public string TerminHaftasiToleransi { get; set; }
        public string Paketleme { get; set; }
        public string Sertifikasyonu { get; set; }

        public string PandaFirmaAd { get; set; }
        public string PandaAdres { get; set; }
        public string PandaPlasiyerEmail { get; set; }
        public string PandaVergiNo { get; set; }
        public string PandaVergiDairesi { get; set; }
        public string Panda_Tel_Fax { get; set; }

        public string PandaNot1 { get; set; }
        public string PandaNot2 { get; set; }
        public string PandaNot3 { get; set; }
        public string PandaNot4 { get; set; }

        public List<SiparisTeyitKalemDTO> Kalemler { get; set; }

        public string LmeBaglamaSekli { get; internal set; }
        public string OzelNotlar { get; internal set; }
        public string TeknikOzellik { get; internal set; }
        public string MusteriVergiNo { get; internal set; }
        public string MusteriIlgiliDetay { get; internal set; }
        public string SiparisBelgeNo { get; internal set; }
        public string SevkHaftaYil { get; internal set; }
        public string TeslimHaftaYil { get; internal set; }
        public string Duzenleyen { get; internal set; }
    }
}