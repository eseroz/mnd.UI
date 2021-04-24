using mnd.Common.Helpers;
using mnd.Logic.Model.Satis;
using System.Linq;

namespace mnd.UI.Services
{
    public class TeyitFormService
    {
        public static SiparisTeyitDTO TeyitFormDtoOlustur(Siparis fullSiparis, Language lang)
        {
            var siparisTeyitData = new SiparisTeyitDTO
            {
                PandaPlasiyerEmail = AppPandap.AktifKullanici.Email,
                SiparisBelgeNo = fullSiparis.SiparisKod,
                SiparisTarihi = fullSiparis.SiparisTarih.Value,
                MusteriAd = fullSiparis.CariKartNavigation.CariIsim,
                MusteriVergiNo = lang == Language.TR ? fullSiparis.CariKartNavigation.VergiNumarasi : "-",
                Tel = fullSiparis.CariKartNavigation.CariTel,
                Fax = fullSiparis.CariKartNavigation.Fax,
                FaturaAdresi = fullSiparis.CariKartNavigation.CariAdres,

                SevkHaftaYil = fullSiparis.SevkHafta.ToString() + "," + fullSiparis.SevkYil,
                TeslimHaftaYil = fullSiparis.TeslimHafta.ToString() + "," + fullSiparis.TeslimYil,

                MusteriSiparisNo = fullSiparis.FirmaSiparisNo,
                TeslimSekli = fullSiparis.TeslimTipKodNavigation.Aciklama,
                OdemeSekliAdi = lang == Language.EN ? fullSiparis.OdemeTipKodNavigation.Aciklama_En : fullSiparis.OdemeTipKodNavigation.Aciklama,
                OdemeSekliAciklama = fullSiparis.OdemeAciklama,
                //LmeBaglamaSekli = fullSiparis.LmeBaglamaNot,
                FaturaKurCinsi = fullSiparis.FaturaDovizTipKod,

                TeslimatAdresi = fullSiparis.IrsaliyeAdresi,

                OzelNotlar = fullSiparis.OzelNot ?? string.Empty,
                TeknikOzellik = fullSiparis.TeknikOzellikNot ?? string.Empty,
                Paketleme = fullSiparis.PaketlemeNot ?? string.Empty,
                Sertifikasyonu = "*Test report according EN 10 204 3.1",
            };

            // left join yapma sorununu gider
            //siparisTeyitData.MusteriIlgiliDetay = fullSiparis?.CariEmailNavigation?.YetkiliKisi + "/" 
            //                + fullSiparis?.CariEmailNavigation?.Email ?? string.Empty;


            if (!string.IsNullOrEmpty(fullSiparis.IlgiliNot))
                siparisTeyitData.MusteriIlgiliDetay = fullSiparis.IlgiliNot;

            siparisTeyitData.Kalemler = fullSiparis.SiparisKalemleri.Select(k => new SiparisTeyitKalemDTO
            {
                MusteriUrunKodu = k.MusteriUrunKodu,
                //Yuzey = lang == Language.EN ? k?.YuzeyTipKodNavigation?.Aciklama_EN : k.YuzeyTipKodNavigation.YuzeyKod,
                //Alasim = k.AlasimTipKod,
                //Kondusyon = k.SertlikTipKod,
                //AmblajSeklikAdi = lang == Language.EN ? k.AmbalajTipKodNavigation.Aciklama_EN : k.AmbalajTipKodNavigation.Aciklama,
                //Kalinlik_micron = k.Kalinlik_micron.Value,
                //En_mm = k.En_mm.Value,
                //SonKullanim = lang == Language.EN ? k.KullanimAlanTipKodNavigation.Aciklama_EN : k.KullanimAlanTipKodNavigation.Aciklama,
                //IcCap_mm = k.RuloIcCap_mm.ToString(),
                //DiscapAralikli_mm = k.RuloDiscapMin_mm.ToString() + "-" + k.RuloDiscapMax_mm.ToString(),
                //RuloAgirligiAralikli_kg = k.RuloAgirligiMin_kg.ToString() + "-" + k.RuloAgirligiMax_kg.ToString(),
                //MasuraCinsi = k.MasuraTipKod,
                //MaxEk = k.MaxEk.Value,
                SevkTarihi_HaftaYil = (lang == Language.EN ? "Week " : "Hafta ") + siparisTeyitData.SevkHaftaYil,
                TeslimTarihi_HaftaYil = (lang == Language.EN ? "Week " : "Hafta ") + siparisTeyitData.TeslimHaftaYil
                //,Miktar_Kg = k.Miktar_kg.Value,

                //BirimTutar_Ton = (k.LmeTutar > 0 ? (k.BirimFiyat * 1000).Value.ToString("n1") :
                //        "LME + " + (k.BirimFiyat * 1000).Value.ToString("n1")) + " " + fullSiparis.TakipDovizTipKodNavigation.Simge + " / Ton"
            })
            .OrderBy(c => c.SatirId)
            .ToList();

            return siparisTeyitData;
        }
    }
}