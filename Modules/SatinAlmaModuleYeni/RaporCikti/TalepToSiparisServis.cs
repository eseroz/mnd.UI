using mnd.Common;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.RaporCikti
{
    public class TalepToSiparisServis
    {
        public static SatinAlma_SiparisDto TalepToSiparisDto(Talep talep)
        {
            var siparisDto = new SatinAlma_SiparisDto();


            siparisDto.TedarikciAd = talep.OnaylananFirmaAd;
            siparisDto.TeslimSekli = talep.SiparisTeslimSekli;
            siparisDto.OdemeSekli = talep.SiparisOdemeSekli;
            siparisDto.StokGrupAd = talep.StokGrupAd;
            siparisDto.SiparisNo = talep.TalepId;
            siparisDto.SiparisTarihi = talep.SiparisTarih == null ? DateTime.Now : talep.SiparisTarih.Value;
            siparisDto.TeklifNo = talep.TeklifNo;

            siparisDto.DovizCinsi = DovizHelper.SimgeyeDonustur(talep.SiparisFirmaDovizCinsi);

            foreach (var item in talep.TalepKalemler)
            {
                var kalem = new SatinAlma_SiparisKalemDto();
                kalem.StokAd = item.StokAd;
                kalem.Miktar = item.Miktar;
                kalem.Birim = item.Birim;
                kalem.TeslimTarihi = item.SiparisTeslimTarihi;
                kalem.BirimFiyat = item.SiparisBirimFiyat.GetValueOrDefault(); 
                kalem.KdvOran = 18;
                kalem.Marka = item.SiparisEdilenMarka;
               
                kalem.Tutar = (decimal)(item.Miktar * kalem.BirimFiyat);
                kalem.Birim = item.Birim;
                kalem.TeslimTarihi = item.SiparisTeslimTarihi;

                siparisDto.SiparisKalemler.Add(kalem);

            }

            siparisDto.ToplamTutar = siparisDto.SiparisKalemler.Sum(c => c.Tutar);
            siparisDto.IndirimTutar = talep.FirmaIndirimTutar.GetValueOrDefault();
            siparisDto.AraToplam = siparisDto.ToplamTutar - siparisDto.IndirimTutar;
            siparisDto.KdvToplam= siparisDto.AraToplam * (18/100M);

            siparisDto.GenelToplam = siparisDto.AraToplam + siparisDto.KdvToplam;

            return siparisDto;
        }


        public static SatinAlma_SiparisDto TalepToIstekFormDto(Talep talep)
        {
            var siparis = new SatinAlma_SiparisDto();

            siparis.TalepTarihi = talep.TalepTarihi;
            siparis.TalepNo = talep.TalepId.ToString();

            foreach (var item in talep.TalepKalemler)
            {
                var kalem = new SatinAlma_SiparisKalemDto();
                kalem.StokAd = item.StokAd;
                kalem.Miktar = item.Miktar;
                kalem.Birim = item.Birim;
                kalem.IstenilenTarih = item.IstenilenTarih;

                kalem.Marka = item.TercihMarkaModel;

                siparis.SiparisKalemler.Add(kalem);

            }


            return siparis;
        }

    }
}
