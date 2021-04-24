using AutoMapper;
using mnd.Logic.BC_Satis._Siparis;
using mnd.Logic.BC_Satis._Teklif;
using mnd.Logic.Helper;
using mnd.Logic.Services.SiparisService;
using mnd.UI.Modules._SatisModule;
using mnd.UI.Modules.TeklifModule.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules.TeklifModule.Services
{
    public class TeklifDataService
    {
        TeklifRepository teklifRepo = new TeklifRepository();

        public string SonKayitNoGetir()
        {
            return teklifRepo.SonKayitNoGetir();
        }
        public string SiparisSonKayitNoGetir()
        {
            return teklifRepo.SiparisSonKayitNoGetir();
        }
        public ObservableCollection<TeklifListModel> TeklifleriGetir(string kullaniciPlasiyerKod, string[] bagliPlasiyerKodlari, string teklifDurum, string teklifSiraKod, string kullaniciId)
        {


            var teklifler = teklifRepo.TeklifQuery()
                                     .Select(c => new TeklifListModel
                                     {
                                         PlasiyerTeklifSiraKod = c.PlasiyerTeklifSiraKod,
                                         TeklifSiraKod = c.TeklifSiraKod,
                                         TeklifTarih = c.TeklifTarih,
                                         SonGecerlilikTarihi = c.SonGecerlilikTarihi,
                                         SevkHafta = c.SevkHafta,
                                         SevkYil = c.SevkYil,
                                         CariKod = c.CariKod,
                                         CariAd = c.CariAd,
                                         CariDovizTipKod = c.CariDovizTipKod,
                                         CariPlasiyerKod = c.MusteriNav.PlasiyerKod,
                                         SatisTemsilcisiAdSoyad = c.SatisTemsilcisiAdSoyad,
                                         TeklifDurum = c.TeklifDurum,
                                         DonusturulenSiparisKod = c.DonusturulenSiparisKod,
                                         IslemNot = c.IslemNot,
                                         RetNeden = c.RetNeden,
                                         RowGuid = c.RowGuid,
                                         Potansiyel = false
                                        

                                     })
                                     .Where
                                    (
                                        c =>
                                        (bagliPlasiyerKodlari.Contains(c.CariPlasiyerKod)) &&
                                        (c.TeklifSiraKod == teklifSiraKod || teklifSiraKod == "Tümü") &&
                                        (c.TeklifDurum == teklifDurum || teklifDurum == "Tümü")
                                    ).ToList();


            var potansiyelTeklifler = teklifRepo.TeklifPotansiyelQuery()
                                     .Where(c => String.IsNullOrEmpty(c.PotansiyelCariAd) == false)
                                     .Select(c => new TeklifListModel
                                    {
                                        PlasiyerTeklifSiraKod = c.PlasiyerTeklifSiraKod,
                                        TeklifSiraKod = c.TeklifSiraKod,
                                        TeklifTarih = c.TeklifTarih,
                                        SonGecerlilikTarihi = c.SonGecerlilikTarihi,
                                        SevkHafta = c.SevkHafta,
                                        SevkYil = c.SevkYil,
                                        CariKod = "",
                                        CariAd = c.PotansiyelCariAd,
                                        CariDovizTipKod = c.CariDovizTipKod,
                                        CariPlasiyerKod = "",
                                        SatisTemsilcisiAdSoyad = c.SatisTemsilcisiAdSoyad,
                                        TeklifDurum = c.TeklifDurum,
                                        DonusturulenSiparisKod = c.DonusturulenSiparisKod,
                                        IslemNot = c.IslemNot,
                                        RetNeden = c.RetNeden,
                                        RowGuid = c.RowGuid,
                                        Potansiyel = true
                                     })
                                    .Where
                                    (
                                        c =>
                                        (c.SatisTemsilcisiAdSoyad == c.SatisTemsilcisiAdSoyad) &&
                                        (c.TeklifSiraKod == teklifSiraKod || teklifSiraKod == "Tümü") &&
                                        (c.TeklifDurum == teklifDurum || teklifDurum == "Tümü")
                                    ).ToList();


            teklifler.AddRange(potansiyelTeklifler);
            return teklifler.ToObservableCollection();

        }

        public void TeklifDurumDegistir(string teklifKod, string teklifDurum, string islemNot, string retneden)
        {
            teklifRepo.TeklifDurumDegistir(teklifKod, teklifDurum, islemNot, retneden);

        }

        public int PlasiyerBazliKayitSayisi(string plasiyerKod)
        {
            var sayi = teklifRepo.PlasiyerBazliAktifGunTeklifSayisi(plasiyerKod);
            return sayi;
        }
        public TeklifEditModel TeklifGetir(string teklifKod)
        {
            var teklif = teklifRepo.TeklifGetir(teklifKod);

            var teklifEditModel = MapToTeklifEditModel(teklif);

            return teklifEditModel;
        }


        private static TeklifEditModel MapToTeklifEditModel(Teklif c)
        {


            var dest = Mapper.Map<Teklif, TeklifEditModel>(c);

            c.TeklifKalemler.ToList().ForEach(k =>
            {

                var mapli = Mapper.Map<TeklifKalem, TeklifKalemEditModel>(k);
                dest.TeklifKalemlerDTO.Add(mapli);
            });


            return dest;



        }

        private static TeklifKalemEditModel MapToTeklifKalemEditModel(TeklifKalem c)
        {
            return new TeklifKalemEditModel()
            {
                //AlasimKod = c.AlasimKod,
                //AmbalajTipKod = c.AmbalajTipKod,
                //BirimFiyat = c.BirimFiyat,
                //TeklifKalemSiraKod = c.TeklifKalemSiraKod,
                //MiktarMin = c.MiktarMin

            };
        }

        public Dictionary<string, int> TeklifSurecGrupSayilariGetir(string[] bagliPlasiyerKodListe)
        {
            TeklifRepository teklifRepo1 = new TeklifRepository();

            return teklifRepo1.TeklifSurecSayilariGetir(bagliPlasiyerKodListe);
        }

        public void Kaydet(TeklifEditModel seciliTeklifDto)
        {
            TeklifRepository teklifRepo1 = new TeklifRepository();
           
            var teklif_Indb = teklifRepo1.TeklifGetir(seciliTeklifDto.TeklifSiraKod);

            if (teklif_Indb == null)
            {
                teklif_Indb = new Teklif();
                teklifRepo1.TeklifEkle(teklif_Indb);
            }

            Mapper.Map(seciliTeklifDto, teklif_Indb);


            //deleted
            teklif_Indb.TeklifKalemler
               .Where(d => !seciliTeklifDto.TeklifKalemlerDTO.Any(detailDTO => detailDTO.TeklifKalemSiraKod == d.TeklifKalemSiraKod))
               .ToList()
               .ForEach(deleted => teklif_Indb.TeklifKalemler.Remove(deleted));



            //added
            seciliTeklifDto.TeklifKalemlerDTO.ToList().ForEach((Action<TeklifKalemEditModel>)(detailDTO =>
            {
                var detail = teklif_Indb.TeklifKalemler.FirstOrDefault((Func<TeklifKalem, bool>)(d => d.TeklifKalemSiraKod == detailDTO.TeklifKalemSiraKod));
                if (detail == null)
                {
                    detail = new TeklifKalem();
                    teklif_Indb.TeklifKalemler.Add(detail);
                }

                Mapper.Map(detailDTO, detail);

            }));


            teklifRepo1.Kaydet();
        }

 
    }
}
