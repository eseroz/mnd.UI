namespace mnd.UI.Services
{
    public class SiparisTeyitKalemDTO
    {
        public string MusteriUrunKodu { get; set; }
        public string Yuzey { get; set; }
        public string Alasim { get; set; }
        public string Kondusyon { get; set; }
        public decimal Kalinlik_micron { get; set; }
        public decimal En_mm { get; set; }
        public string SonKullanim { get; set; }
        public string IcCap_mm { get; set; }
        public string DiscapAralikli_mm { get; set; }
        public string RuloAgirligiAralikli_kg { get; set; }
        public string MasuraCinsi { get; set; }
        public int MaxEk { get; set; }
        public string SevkTarihi_HaftaYil { get; set; }
        public string TeslimTarihi_HaftaYil { get; set; }
        public decimal Miktar_Kg { get; set; }
        public string BirimTutar_Ton { get; set; }
        public string AmblajSeklikAdi { get; internal set; }
        public string SatirId { get; internal set; }
    }
}