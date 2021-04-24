using mnd.Logic.Helper;
using mnd.Logic.Model.Operasyon;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules.OperasyonModule
{
    public class IrsaliyeService
    {
        public static ObservableCollection<IrsaliyePalet> IrsaliyePaletGrupla(Irsaliye irsaliye)
        {
            var gruplu = irsaliye.IrsaliyePaletler
                 .GroupBy(c => new
                 {
                     c.PaletGrupKey,
                     c.UrunFaturaAd,
                     c.BirimFiyat_Kg,
                     c.NetsisStokKod,
                     c.LfxKod,
                     c.Kalinlik,
                     c.En,
                     c.Alasim,
                     c.Sertlik,
                     c.NetsisDovizTipId,
                     c.NetsisSatisFaturaTipId,
                     c.KdvOran,
                     c.DovizTipKod,
                     c.LmeBF_Ton,
                     c.KulceBF_Ton,
                     c.IscilikVadeFarkiTutar,
                     c.IscilikBF_Ton,
                     c.GTip,
                     c.GTipSatirKod,
                     c.GTipNo,
                     c.SiparisKod,
                     c.FirmaSiparisNo,

                 })
                 .Select(g => new IrsaliyePalet
                 {
                     NetsisStokKod = g.Key.NetsisStokKod,
                     LfxKod = g.Key.LfxKod,
                     UrunFaturaAd = g.Key.UrunFaturaAd,

                     SiparisKod = g.Key.SiparisKod,

                     Kalinlik = g.Key.Kalinlik.GetValueOrDefault(),
                     En = g.Key.En.GetValueOrDefault(),

                     Alasim = g.Key.Alasim,
                     Sertlik = g.Key.Sertlik,



                     // hesaplamayı doğrula satır toplamları geneli karşılar mı? 
                     LmeBF_Ton = g.Key.LmeBF_Ton,
                     KulceBF_Ton = g.Key.KulceBF_Ton,
                     IscilikBF_Ton = g.Key.IscilikBF_Ton,
                     IscilikVadeFarkiTutar = g.Key.IscilikVadeFarkiTutar,

                     BirimFiyat_Kg = g.Key.BirimFiyat_Kg,
                     KdvOran = g.Key.KdvOran,

                     PaletNet_Kg = g.Sum(c => c.PaletNet_Kg),


                     GTip = g.Key.GTip,
                     GTipSatirKod = g.Key.GTipSatirKod,
                     GTipNo = g.Key.GTipNo,

                     DovizTipKod = g.Key.DovizTipKod,

                     PaletSayisi = g.Sum(c => c.PaletSayisi),
                     MasuraSayisi = g.Sum(c => c.MasuraSayisi),

                     FirmaSiparisNo = g.Key.FirmaSiparisNo,

                 })
                 .ToObservableCollection();



            foreach (var item in gruplu)
            {
                var varsayilanBirimFiyat_Kg = (decimal)(item.LmeBF_Ton + item.KulceBF_Ton.GetValueOrDefault() + item.IscilikBF_Ton) / 1000;

                item.IscilikVadeFarkiTutar = varsayilanBirimFiyat_Kg * (item.IscilikVadeFarkiOran / 100);  // kdv gibi ekleniyor

                item.BirimFiyat_Kg = varsayilanBirimFiyat_Kg + item.IscilikVadeFarkiTutar.GetValueOrDefault();

                item.BirimFiyat_Kg = Math.Round(item.BirimFiyat_Kg, 3);

                item.PaletToplamTutar = item.BirimFiyat_Kg * item.PaletNet_Kg;

                item.PaletKdvTutar = item.PaletToplamTutar * item.KdvOran / (decimal)100.0;

                item.PaletGenelToplamTutar = (item.PaletToplamTutar + item.PaletKdvTutar);
            }

            return gruplu;
        }




    }
}
