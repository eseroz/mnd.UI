using mnd.Common;
using mnd.Logic.Persistence;
using mnd.UI.Modules.PlanlamaModule;
using System;

namespace mnd.UI.Services
{
    public class UretimEmriService
    {
        public static UretimEmriDTO Create_UretimEmriDTO_FromKalem(string kalemKod)
        {
            UnitOfWork uow = new UnitOfWork();

            var kalem = uow.SiparisKalemRepo.SiparisKalemiGetir(kalemKod);

            var seciliSiparisDTo = uow.SiparisRepo.SiparisGetir(kalem.SiparisKod);


            //var kabfolTolerans = " *" + kalem.Metraj_mt.ToString() + " +" + kalem.MetrajArti_mt.GetValueOrDefault().ToString()
            //                       + " -" + kalem.MetrajEksi_mt.GetValueOrDefault().ToString();

            //var teknikData = seciliSiparisDTo.TeknikOzellikNot != null && seciliSiparisDTo.TeknikOzellikNot.Length > 0 ? seciliSiparisDTo.TeknikOzellikNot + Environment.NewLine : "";
            //var paketlemeData = seciliSiparisDTo.PaketlemeNot != null && seciliSiparisDTo.PaketlemeNot.Length > 0 ? seciliSiparisDTo.PaketlemeNot + Environment.NewLine : "";
            //var kabfolData = kalem.KulcePrimTipKod == "KABFOL" ? kabfolTolerans + Environment.NewLine : "";
            //var pandaNot = seciliSiparisDTo.PandaNot != null && seciliSiparisDTo.PandaNot.Length > 0 ? seciliSiparisDTo.PandaNot + Environment.NewLine : "";
            //var firmaSipNo = "Firma Sip No: " + seciliSiparisDTo.FirmaSiparisNo + " - Firma ÜrunKod: " + kalem.MusteriUrunKodu;


            //var ozelTalimatBirlesikData = teknikData + paketlemeData + kabfolData + pandaNot + firmaSipNo;


            //var uretimEmriDto = new UretimEmriDTO
            //{
            //    SiparisKalemKod = kalem.SiparisKalemKod,


            //    UrunKod = kalem.AlasimTipKod + "-" + kalem.SertlikTipKod + "-" + kalem.YuzeyTipKod,
            //    HedefKalinlik = kalem.Kalinlik_micron.Value,

            //    OzelTalimat = ozelTalimatBirlesikData,

            //    Musteri = seciliSiparisDTo.CariKartNavigation.CariIsim,
            //    ÜlkeEN = seciliSiparisDTo.CariKartNavigation.UlkeAd,

            //    KullanimAlani = kalem.KullanimAlanTipKod,
            //    SevkTarihi = seciliSiparisDTo.SevkYil.ToString() + "-" + seciliSiparisDTo.SevkHafta.ToString() + ".HAFTA",

            //    KalemMiktar = kalem.Miktar_kg.Value,
            //    PlanlamaBakiye = kalem.PLAN_PlanlanacakKalanMiktarToplam.Value,

            //    Yüzey = kalem.YuzeyTipKod,
            //    Alasim = kalem.AlasimTipKod,

            //    Olcu = kalem.Kalinlik_micron.Value.TruncateDecimalPlaces(1).ToString() + " X " + kalem.En_mm.Value.TruncateDecimalPlaces(1).ToString(),

            //    AmbalajTipi = kalem.AmbalajTipKod,

            //    RuloDışçapMax = kalem.RuloDiscapMax_mm.Value.ToString(),
            //    RuloDışçapMin = kalem.RuloDiscapMin_mm.Value.ToString(),

            //    MasuraCinsi = kalem.MasuraTipKod,
            //    Kondusyon = kalem.SertlikTipKod,
            //    RuloIcCap_mm = kalem.RuloIcCap_mm.Value.ToString(),

            //    SiparisMiktarı = kalem.Miktar_kg.Value.ToString(),
            //    MasuraBoyu = string.Empty,

            //    KalınlıkToleransı_mm = "%" + kalem.KalinlikEksi_yuzde.ToString() + "+" + kalem.KalinlikArti_yuzde.ToString(),

            //    EnToleransı_mm = kalem.EnEksi_mm.Value.TruncateDecimalPlaces(1).ToString() + "+" + kalem.EnArti_mm.Value.TruncateDecimalPlaces(1).ToString(),

            //    MaksimumEkSayısı_mm = kalem.MaxEk.ToString() + " / " + "RULO",
            //    Hazırlayan = AppPandap.AktifKullanici.AdSoyad,
            //    KaydiriciOraniMinMaxStr=kalem.KaydiriciOraniMinMaxStr


            //};

            return new UretimEmriDTO();
        }

        public static UretimEmriDTO Create_UretimEmriDTO_FromUretimEmriKod(string uretimEmriKod)
        {
            UnitOfWork uow = new UnitOfWork();

            var uretim_emir = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(uretimEmriKod);

            var kalem = uow.SiparisKalemRepo.SiparisKalemiGetir(uretim_emir.SiparisKalemKod);

            var seciliSiparisDTo = uow.SiparisRepo.SiparisGetir(kalem.SiparisKod);

            //var kabfolTolerans = " *" + kalem.Metraj_mt.ToString() + " +" + kalem.MetrajArti_mt.GetValueOrDefault().ToString()
            //                     + " -" + kalem.MetrajEksi_mt.GetValueOrDefault().ToString();

            //var teknikData = seciliSiparisDTo.TeknikOzellikNot != null && seciliSiparisDTo.TeknikOzellikNot.Length > 0 ? seciliSiparisDTo.TeknikOzellikNot + Environment.NewLine : "";
            //var paketlemeData = seciliSiparisDTo.PaketlemeNot != null && seciliSiparisDTo.PaketlemeNot.Length > 0 ? seciliSiparisDTo.PaketlemeNot + Environment.NewLine : "";
            //var kabfolData = kalem.KulcePrimTipKod == "KABFOL" ? kabfolTolerans + Environment.NewLine : "";
            //var pandaNot = seciliSiparisDTo.PandaNot != null && seciliSiparisDTo.PandaNot.Length > 0 ? seciliSiparisDTo.PandaNot + Environment.NewLine : "";
            //var firmaSipNo = "Firma Sip No: " + seciliSiparisDTo.FirmaSiparisNo + " - Firma ÜrunKod: " + kalem.MusteriUrunKodu;


            //var ozelTalimatBirlesikData = teknikData + paketlemeData + kabfolData + pandaNot + firmaSipNo;



            var uretimEmriDto = new UretimEmriDTO
            {
                SiparisKalemKod = kalem.SiparisKalemKod,
                UretimEmriKod = uretim_emir.UretimEmriKod,
                EklenmeTarih = uretim_emir.EklenmeTarih,
                KartNo = uretim_emir.KartNo,

                //UrunKod = kalem.AlasimTipKod + "-" + kalem.SertlikTipKod + "-" + kalem.YuzeyTipKod,
                //HedefKalinlik = kalem.Kalinlik_micron.Value,

                //OzelTalimat = uretim_emir.OzelTalimat==null? ozelTalimatBirlesikData : uretim_emir.OzelTalimat,
                //KaydiriciOraniMinMaxStr=kalem.KaydiriciOraniMinMaxStr,

                PlanlamaNot = uretim_emir.PlanlamaNot,

                Kombinler = uretim_emir.Kombinler,
                KombinlerEnToplam = uretim_emir.KombinlerEnToplam,
                DilmeSeperatorNot = uretim_emir.DilmeSeperatorNot,

                Musteri = seciliSiparisDTo.CariKartNavigation.CariIsim,
                ÜlkeEN = seciliSiparisDTo.CariKartNavigation.UlkeAd,

                //KullanimAlani = kalem.KullanimAlanTipKod,
                //SevkTarihi = seciliSiparisDTo.SevkYil.ToString() + "-" + seciliSiparisDTo.SevkHafta.ToString() + ".HAFTA",

                //KalemMiktar = kalem.Miktar_kg.Value,
                //PlanlamaBakiye = kalem.PLAN_PlanlanacakKalanMiktarToplam.Value,

                //Yüzey = kalem.YuzeyTipKod,
                //Alasim = kalem.AlasimTipKod,

                //Olcu = kalem.Kalinlik_micron.Value.TruncateDecimalPlaces(1).ToString() + " X " + kalem.En_mm.Value.TruncateDecimalPlaces(1).ToString(),

                //AmbalajTipi = kalem.AmbalajTipKod,

                //RuloDışçapMax = kalem.RuloDiscapMax_mm.Value.ToString(),
                //RuloDışçapMin = kalem.RuloDiscapMin_mm.Value.ToString(),

                //MasuraCinsi = kalem.MasuraTipKod,

                //Kondusyon = kalem.SertlikTipKod,
                //RuloIcCap_mm = kalem.RuloIcCap_mm.Value.ToString(),

                PlanlananMiktar_kg = uretim_emir.Uretim_PlanlananMiktar.GetValueOrDefault(),

                //SiparisMiktarı = kalem.Miktar_kg.Value.ToString(),
                //MasuraBoyu = string.Empty,

                //KalınlıkToleransı_mm = "%" + kalem.KalinlikEksi_yuzde.ToString() + "+" + kalem.KalinlikArti_yuzde.ToString(),

                //EnToleransı_mm = kalem.EnEksi_mm.Value.TruncateDecimalPlaces(1).ToString() + "+" + kalem.EnArti_mm.Value.TruncateDecimalPlaces(1).ToString(),

                //MaksimumEkSayısı_mm = kalem.MaxEk.ToString() + " / " + "RULO",
                //Hazırlayan = AppPandap.AktifKullanici.AdSoyad,

                PlanlamaRulolari = uretim_emir.PlanlamaRulolari,
                MakineAsamalari1= uretim_emir.MakineAsamalari1,
                MakineAsamalari2=uretim_emir.MakineAsamalari2,

                KombinMiktari_kg = uretim_emir.KombinMiktari_kg.GetValueOrDefault(),
                MaxKombinEni=uretim_emir.MaxKombinEni
            };

            return uretimEmriDto;
        }
    }
}