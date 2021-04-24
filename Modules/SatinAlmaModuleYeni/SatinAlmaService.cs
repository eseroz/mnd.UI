using mnd.Common.Helpers;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules.SatinAlmaModuleYeni
{
    public class SatinAlmaService
    {
        public static KararFormModel TeklifToKararModel(Talep SeciliTeklif)
        {
            var KararFormModel = new KararFormModel();

            StokTanimNetsisRepository repoStokTanim = new StokTanimNetsisRepository();

            var firmalar = SeciliTeklif.TeklifIlgiliFirmalar.OrderBy(c => c.Id).ToList();

            foreach (var firmaData in SeciliTeklif.TeklifIlgiliFirmalar)
            {
                var sutunData = new FirmaSutunDataModel();
                sutunData.CariAd = firmaData.CariAd;
                sutunData.CariKod = firmaData.CariKod;
                sutunData.NakliyeDurum = firmaData.NakliyeDurum;
                sutunData.OdemeSekli = firmaData.OdemeSekli;
                sutunData.DovizTip = firmaData.DovizTip;
                sutunData.IndirimMiktari = firmaData.IndirimMiktari.GetValueOrDefault();
                sutunData.RowGuid = firmaData.RowGuid;
                KararFormModel.TabloTeklifSutunlari.Add(sutunData);
            }

            foreach (var talepkalem in SeciliTeklif.TalepKalemler)
            {
                FirmaTeklifSatirModel kararSatir = new FirmaTeklifSatirModel(SeciliTeklif);

                kararSatir.RowGuid = talepkalem.RowGuid;
                kararSatir.KalemId = talepkalem.TalepKalemId;
                kararSatir.StokAd = talepkalem.StokAd;
                kararSatir.Miktar = talepkalem.Miktar;
                kararSatir.OlcuBirimi = talepkalem.Birim;
                kararSatir.YoneticiKalemIptalMi = talepkalem.YoneticiKalemIptalMi;
                kararSatir.StokKod = talepkalem.StokKod;
                kararSatir.IstenilenTarih = talepkalem.IstenilenTarih;
                kararSatir.Aciklama = talepkalem.Aciklama;
                kararSatir.IlkTalepId = talepkalem.IlkTalepId;

                var guncel = repoStokTanim.StokMiktariGetir(talepkalem.StokKod);

                if (guncel != null) kararSatir.GuncelStokMiktari = guncel.Bakiye;

                kararSatir.TalepEdenAdSoyad = SeciliTeklif.TalepEdenAdSoyad;
                kararSatir.IsMerkezi = SeciliTeklif.IsMerkeziAd;


                var stokSonBilgi = repoStokTanim.StokAlimSonFiyatGetir(talepkalem.StokKod);

                if (stokSonBilgi != null)
                {
                    kararSatir.SonAlimFiyat = stokSonBilgi.BirimFiyat.GetValueOrDefault().ToString("0.##") + " " + stokSonBilgi.DovizTip;
                    kararSatir.SonAlimFirmaAd = stokSonBilgi.CariIsim;
                    kararSatir.SonAlimFirmaCariKod = stokSonBilgi.CariKod;
                }

                for (int i = 1; i <= SeciliTeklif.TeklifIlgiliFirmalar.Count; i++)
                {
                    var firma = SeciliTeklif.TeklifIlgiliFirmalar[i - 1];

                    var kararSatirTeklifHucre = new FirmaHucreDataModel(SeciliTeklif.TalepTarihi, talepkalem.Miktar);

                    var kalemteklif_firma = talepkalem.TalepKalemTeklifler
                            .FirstOrDefault(c => c.TeklifVerenFirmaCariKod == firma.CariKod);

                    if (kalemteklif_firma == null) continue;


                    kararSatirTeklifHucre.CariKod = firma.CariKod;
                    kararSatirTeklifHucre.CariAd = firma.CariAd;

                    kararSatirTeklifHucre.BirimFiyat = kalemteklif_firma?.TeklifFiyat;

                    kararSatirTeklifHucre.SatinAlmaTercihiMi = kalemteklif_firma.SatinAlmaTercihiMi;
                    kararSatirTeklifHucre.YoneticiTercihiMi = kalemteklif_firma.YoneticiTercihiMi;

                    kararSatirTeklifHucre.Marka = kalemteklif_firma.Marka;
                    kararSatirTeklifHucre.TeslimTarihi = kalemteklif_firma.TeslimTarihi;

                    kararSatirTeklifHucre.SatinAlmaTercihiMi = kalemteklif_firma == null ? false : kalemteklif_firma.SatinAlmaTercihiMi;
                    kararSatirTeklifHucre.YoneticiTercihiMi = kalemteklif_firma == null ? false : kalemteklif_firma.YoneticiTercihiMi;

                    NesneIslemleri.OzellikDegerAta(kararSatir, $"Firma{i}_DataModel", kararSatirTeklifHucre);
                }

                KararFormModel.TabloTeklifSatirlari.Add(kararSatir);
            }

            return KararFormModel;
        }

        public static List<Talep> TekliftenSiparisleriOlustur(Talep SeciliTeklif)
        {
            List<Talep> Siparisler = new List<Talep>();

            var seciliTeklifKalemler = SeciliTeklif.TalepKalemler
                .SelectMany(kalem => kalem.TalepKalemTeklifler.Where(c => c.YoneticiTercihiMi == true)
                                    , (kalem, kalemTeklif) => new { kalem, kalemTeklif })
                .Select(s => new { s.kalem, s.kalemTeklif })
                .Where(c=>c.kalem.YoneticiKalemIptalMi.GetValueOrDefault()==false)
                .ToList();

            var teklifIlgiliFirmalar = SeciliTeklif.TeklifIlgiliFirmalar;

            var tercihCariler = seciliTeklifKalemler
                    .Select(c => new
                    {
                        c.kalemTeklif.TeklifVerenFirmaCariKod,
                        c.kalemTeklif.TeklifVerenFirmaAd,
                    })
                    .Distinct()
                    .ToList();

            foreach (var item in tercihCariler)
            {
                var cariSutunBilgi = teklifIlgiliFirmalar.First(c => c.CariKod == item.TeklifVerenFirmaCariKod);

                Talep siparis = new Talep();

                siparis.TeklifNo = SeciliTeklif.TalepId;
                siparis.Tip = "Sipariş";
                siparis.TalepTarihi = DateTime.Now;

                siparis.SiparisTeslimSekli = cariSutunBilgi.NakliyeDurum;
                siparis.SiparisOdemeSekli = cariSutunBilgi.OdemeSekli;
                siparis.SiparisFirmaDovizCinsi = cariSutunBilgi.DovizTip;

                siparis.FirmaIndirimTutar = cariSutunBilgi.IndirimMiktari.GetValueOrDefault();

                siparis.RowGuid = Guid.NewGuid();
                siparis.OnaylananFirmaKod = item.TeklifVerenFirmaCariKod;
                siparis.OnaylananFirmaAd = cariSutunBilgi.CariAd;

                siparis.TalepSurecKonum = SATINALMA_SURECDURUM.ONAYLANAN_SIPARISLER;

                siparis.StokGrupKod = SeciliTeklif.StokGrupKod;
                siparis.StokGrupAd = SeciliTeklif.StokGrupAd;

                siparis.IsMerkeziAd = SeciliTeklif.IsMerkeziAd;
                siparis.IsMerkeziKod = SeciliTeklif.IsMerkeziKod;

                siparis.TalepEdenTc = SeciliTeklif.TalepEdenTc;
                siparis.TalepEdenAdSoyad = SeciliTeklif.TalepEdenAdSoyad;

                var eklenecekKalemler = seciliTeklifKalemler
                               .Where(c => c.kalemTeklif.TeklifVerenFirmaCariKod == item.TeklifVerenFirmaCariKod).ToList();

                foreach (var ek in eklenecekKalemler)
                {
                    var kalem = new TalepKalem
                    {
                        StokAd = ek.kalem.StokAd,
                        StokKod = ek.kalem.StokKod,
                        KalemTip = "Sipariş",

                        Birim = ek.kalem.Birim,
                        StokGrupAd = ek.kalem.StokGrupAd,
                        TercihMarkaModel = ek.kalem.TercihMarkaModel,

                        IstenilenTarih = ek.kalem.IstenilenTarih,
                        TeklifeAktarilmaTarihi = DateTime.Now,

                        Miktar = ek.kalemTeklif.Miktar.GetValueOrDefault(),

                        SiparisEdilenMarka = ek.kalemTeklif.Marka,
                        SiparisTeslimTarihi = ek.kalemTeklif.TeslimTarihi,

                        SiparisBirimFiyat = ek.kalemTeklif.TeklifFiyat,
                        SiparisToplamFiyat = ek.kalemTeklif.TeklifFiyat * ek.kalem.SiparisBirimFiyat,

                        SiparisDovizCinsi = cariSutunBilgi.DovizTip,

                        RowGuid = Guid.NewGuid(),

                        TalepEdenAdSoyad = ek.kalem.TalepEdenAdSoyad,
                        IsMerkeziAd = ek.kalem.IsMerkeziAd,

                        StokGrupKod = ek.kalem.StokGrupKod
                    };

                    siparis.TalepKalemler.Add(kalem);
                }

                Siparisler.Add(siparis);
            }

            return Siparisler;
        }

        public static void KararFormMapSeciliTeklif(KararFormModel KararFormModel, Talep SeciliTeklifTablo, TalepRepository repo =null)
        {
            foreach (var kalem in SeciliTeklifTablo.TalepKalemler)
            {
                var satir = KararFormModel.TabloTeklifSatirlari.First(c => c.KalemId == kalem.TalepKalemId);

                for (int i = 1; i <= KararFormModel.TabloTeklifSutunlari.Count; i++)
                {
                    var kararSatirTeklifHucre = (FirmaHucreDataModel)NesneIslemleri.NesneOzellikObjeGetir(satir, $"Firma{i}_DataModel");
                    if (kararSatirTeklifHucre == null) continue;
                    
                    var kararSutun = KararFormModel.TabloTeklifSutunlari[i - 1];

                    var talepKalemTeklif = new TalepKalemTeklif();

                    talepKalemTeklif.TeklifVerenFirmaCariKod = kararSutun.CariKod;
                    talepKalemTeklif.TeklifVerenFirmaAd = kararSutun.CariAd;


                    talepKalemTeklif.TeklifFiyat = kararSatirTeklifHucre.BirimFiyat;

                    talepKalemTeklif.Miktar = kararSatirTeklifHucre.T_Miktar;
                    talepKalemTeklif.Marka = kararSatirTeklifHucre.Marka;
                    talepKalemTeklif.DovizTip = kararSatirTeklifHucre.T_DovizTip;
                    talepKalemTeklif.TeslimTarihi = kararSatirTeklifHucre.TeslimTarihi;
                    talepKalemTeklif.SatinAlmaTercihiMi = kararSatirTeklifHucre.SatinAlmaTercihiMi;
                    talepKalemTeklif.YoneticiTercihiMi = kararSatirTeklifHucre.YoneticiTercihiMi;

                    kalem.TalepKalemTeklifler.Add(talepKalemTeklif);
                }
            }


            SeciliTeklifTablo.TeklifIlgiliFirmalar.Clear();

       

            foreach (var kararSutunData in KararFormModel.TabloTeklifSutunlari)
            {

                if (kararSutunData.CariKod == "") continue;
                var ilgiliFirma = new TeklifIlgiliFirma
                {
                    CariKod = kararSutunData.CariKod,
                    CariAd = kararSutunData.CariAd,
                    OdemeSekli = kararSutunData.OdemeSekli,
                    NakliyeDurum = kararSutunData.NakliyeDurum,
                    IndirimMiktari = kararSutunData.IndirimMiktari,
                    DovizTip = kararSutunData.DovizTip,
                    RowGuid = kararSutunData.RowGuid
                };

                SeciliTeklifTablo.TeklifIlgiliFirmalar.Add(ilgiliFirma);

                if (repo != null) repo.Kaydet(); // graph data insert order problemi için

            }
        }

        public static List<Talep> KararFormToSiparisListe(KararFormModel KararFormModel, Talep SeciliTeklif)
        {
            List<Talep> OlusturulacakTalepler = new List<Talep>();

            foreach (var item in KararFormModel.TabloTeklifSatirlari)
            {
                for (int i = 0; i < KararFormModel.TabloTeklifSutunlari.Count; i++)
                {
                    var data = (FirmaHucreDataModel)NesneIslemleri.NesneOzellikObjeGetir(item, $"Firma{i + 1}_DataModel");

                    if (data.YoneticiTercihiMi)
                    {
                        item.SecilenFirmaCariKod = KararFormModel.TabloTeklifSutunlari[i].CariKod;
                        item.SecilenFirmaCariAd = KararFormModel.TabloTeklifSutunlari[i].CariAd;

                        item.SeciliHucreModel = data;
                    }
                }
            }

            var secilenCariKodlar = KararFormModel.TabloTeklifSatirlari
                                    .Where(c => c.SeciliHucreModel != null)
                                    .Select(c => c.SecilenFirmaCariKod)
                                    .Distinct();

            foreach (var carikod in secilenCariKodlar)
            {
                var cariSutunBilgi = KararFormModel.TabloTeklifSutunlari.First(c => c.CariKod == carikod);

                Talep siparis = new Talep();

                siparis.TeklifNo = SeciliTeklif.TalepId;
                siparis.Tip = "Sipariş";
                siparis.TalepTarihi = DateTime.Now;

                siparis.SiparisTeslimSekli = cariSutunBilgi.NakliyeDurum;

                siparis.SiparisOdemeSekli = cariSutunBilgi.OdemeSekli;
                siparis.SiparisFirmaDovizCinsi = cariSutunBilgi.DovizTip;

                siparis.RowGuid = Guid.NewGuid();
                siparis.OnaylananFirmaKod = carikod;
                siparis.OnaylananFirmaAd = cariSutunBilgi.CariAd;
                siparis.TalepSurecKonum = "ONAYLANAN_SIPARISLER";

                siparis.StokGrupKod = SeciliTeklif.StokGrupKod;
                siparis.StokGrupAd = SeciliTeklif.StokGrupAd;

                siparis.IsMerkeziAd = SeciliTeklif.IsMerkeziAd;
                siparis.IsMerkeziKod = SeciliTeklif.IsMerkeziKod;

                siparis.TalepEdenTc = SeciliTeklif.TalepEdenTc;
                siparis.TalepEdenAdSoyad = SeciliTeklif.TalepEdenAdSoyad;

                siparis.TalepKalemler = new ObservableCollection<TalepKalem>();

                var cariSeciliTeklifler = KararFormModel.TabloTeklifSatirlari
                                        .Where(c => c.SecilenFirmaCariKod == carikod)
                                        .ToList();

                foreach (var item in cariSeciliTeklifler)
                {
                    var kalem = new TalepKalem
                    {
                        StokAd = item.StokAd,
                        StokKod = item.StokKod,
                        KalemTip = "Sipariş",

                        Birim = item.OlcuBirimi,
                        StokGrupAd = item.StokGrupAd,
                        TercihMarkaModel = item.TercihMarkaModel,

                        Miktar = item.SeciliHucreModel.T_Miktar.GetValueOrDefault(),
                        SiparisEdilenMarka = item.SeciliHucreModel.Marka,
                        SiparisTeslimTarihi = item.SeciliHucreModel.TeslimTarihi,

                        SiparisBirimFiyat = item.SeciliHucreModel.BirimFiyat,
                        SiparisToplamFiyat = item.SeciliHucreModel.ToplamFiyat,

                        SiparisDovizCinsi = cariSutunBilgi.DovizTip,

                        RowGuid = Guid.NewGuid(),

                        TalepEdenAdSoyad = item.TalepEdenAdSoyad,
                        IsMerkeziAd = item.IsMerkezi,
                        StokGrupKod = item.StokGrupKod
                    };

                    siparis.TalepKalemler.Add(kalem);
                }

                OlusturulacakTalepler.Add(siparis);
            }

            return OlusturulacakTalepler;
        }
    }
}