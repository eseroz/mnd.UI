using mnd.Logic.Model.Netsis;
using mnd.Logic.Persistence;
using System;
using System.Collections.Generic;

namespace mnd.UI.Services
{
    public class NetsisService
    {
        public static double NetsisDovizKuruGetir(string dovizAd, DateTime tarih)
        {
            UnitOfWork uow = new UnitOfWork();

            var cevap = uow.SiparisLookUpRepo.NetsisDovizKurunuGetir(dovizAd, tarih.Date);


            if (cevap == null) new Exception(tarih.Date + " " + dovizAd + "kur değeri girilmemiş");

            return cevap.DovizSatis.GetValueOrDefault();
        }

        public static List<DovizKur> NetsisBelirliTarihtenSonrakiDovizKurlariniGetir(DateTime tarih)
        {
            UnitOfWork uow = new UnitOfWork();

            var liste = uow.SiparisLookUpRepo.NetsisBelirliTarihtenSonrakiDovizKurlariniGetir(tarih.Date);

            return liste;
        }

        public static List<DovizKur> NetsisDovizKurlariniGetirSonKayittan()
        {
            UnitOfWork uow = new UnitOfWork();

            var liste = uow.SiparisLookUpRepo.NetsisKayitliSonDovizKurlariniGetir();

            return liste;
        }

    }
}