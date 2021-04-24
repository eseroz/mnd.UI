using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Operasyon;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.Helper;
using mnd.Logic.Persistence;
using mnd.Logic.Services._DTOs;
using mnd.UI.Helper;
using mnd.UI.Modules.SatinAlmaModuleYeni.Stoklar;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.Dashboard
{
    public class StokMaliyetVM : MyDxViewModelBase
    {
        private UnitOfWork uow;

        public string SonKurBilgi { get => sonKurBilgi; set => SetProperty(ref sonKurBilgi, value); }


        private StokTanimNetsisRepository repo;
        private YariMamulRepository repoYariMamul;

        private string sonKurBilgi;
        private ObservableCollection<vwStokTanim> netsisStoklar;
        private ObservableCollection<vwStokTanim> netsisStokGiderler;
        private List<MamulDepoStokDto> mamulDepoStoklar;
        private DateTime baslamaTarihi;
        private DateTime bitisTarihi;

        public ObservableCollection<vwStokTanim> NetsisStoklar { get => netsisStoklar; set => SetProperty(ref netsisStoklar, value); }

        public ObservableCollection<vwStokTanim> NetsisStokGiderler { get => netsisStokGiderler; set => SetProperty(ref netsisStokGiderler, value); }

        public List<MamulDepoStokDto> MamulDepoStoklar { get => mamulDepoStoklar; set => SetProperty(ref mamulDepoStoklar, value); }

        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnYenile);

        public DelegateCommand SorgulaCommand => new DelegateCommand(OnSorgula);


        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport,CanExcelExport);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "cari_export.xlsx");
        }

        private bool CanExcelExport(object arg)
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI;
        }

        private void OnSorgula()
        {
            OnYenile();
        }

        public DateTime BaslamaTarihi { get => baslamaTarihi; set => SetProperty(ref baslamaTarihi, value); }
        public DateTime BitisTarihi { get => bitisTarihi; set => SetProperty(ref bitisTarihi, value); }

        public StokMaliyetVM(string formAdi)
        {
            baslamaTarihi = DateTime.Now.Date;
            bitisTarihi = DateTime.Now.Date;

            OnYenile();
        }
        public void OnYenile()
        {
            uow = new UnitOfWork();
            repo = new StokTanimNetsisRepository();
            repoYariMamul = new YariMamulRepository();

            var dovizKurlariTumu = NetsisService.NetsisBelirliTarihtenSonrakiDovizKurlariniGetir(DateTime.Now.AddYears(-1));
            var dovizKurlari = NetsisService.NetsisDovizKurlariniGetirSonKayittan();
            var sonKurTarihi = dovizKurlari.First().Tarih.Value.Date;
            var euroTlKur = StokListViewModel.PariteGetir("EUR", sonKurTarihi, dovizKurlari, "TL");

            SonKurBilgi = "Tarih : " + sonKurTarihi.Date.ToShortDateString() + " : 1 EUR-> " + euroTlKur;


            NetsisStoklar = repo.NetsisStokTanimlariGetir()
                .Where(c => c.GRUP_KODU != "7")
                .ToObservableCollection();



            MamulDepoStoklar = uow.PlanlamaRepo.MamulDepoStoklariFiltrele(lmeYoksaGunluktenAlinsinMi: true, PALETKONUM.DEPO + "," + PALETKONUM.SEVKEMRI);


            foreach (var item in NetsisStoklar)
            {
                var pariteEur = StokListViewModel.PariteGetir(item.DovizTipi, sonKurTarihi, dovizKurlari, "EUR");

                item.StokToplami_Euro = item.BAKIYE.GetValueOrDefault() *
                                        item.SonFiyat.GetValueOrDefault() *
                                        pariteEur;
            }



            foreach (var item in MamulDepoStoklar)
            {
                var pariteEur = StokListViewModel.PariteGetir(item.DovizTipKod, sonKurTarihi, dovizKurlari, "EUR");
                item.PaletGenelToplamEuro = item.PaletGenelToplam * pariteEur;

                NetsisStoklar.Add(new vwStokTanim
                {
                    STOK_KODU = item.MaliyetStokKod,
                    STOKADI_TR = item.CariIsim,
                    StokToplami_Euro = item.PaletGenelToplamEuro.GetValueOrDefault(),
                    DovizTipi = item.DovizTipKod,
                    OLCU_BR1 = "Kg",
                    BAKIYE = item.PaletNet_Kg,
                    GRUP_AD = "3/MM-MAMUL"
                });

            }


            foreach (var item in YariMamulHesaplanmisListe("Levha"))
            {

                item.GRUP_AD = "2/YM-YARIMAMUL";
                item.KOD1_AD = "Levha";
                item.DovizTipi = "EUR";
                NetsisStoklar.Add(item);
            }

            foreach (var item in YariMamulHesaplanmisListe("Folyo"))
            {
                item.GRUP_AD = "2/YM-YARIMAMUL";
                item.KOD1_AD = "Folyo";
                item.DovizTipi = "EUR";
                NetsisStoklar.Add(item);
            }


            NetsisStokGiderler = repo.NetsisStokTanimlariGetir().Where(c => c.GRUP_KODU == "7")
                                                                .ToObservableCollection();

            var fiyatlar = repo.StokAlimFiyatlariGetir();


            foreach (var item in NetsisStokGiderler)
            {
                var stokFiyatlari = fiyatlar.Where(c => c.StokKod == item.STOK_KODU).ToList();

                decimal stokDovizliToplam = 0;
                foreach (var stok in stokFiyatlari)
                {
                    if (stok.IslemTarih <= bitisTarihi)
                    {
                        var pariteEur = StokListViewModel.PariteGetir(stok.DovizTip, stok.IslemTarih, dovizKurlariTumu, "EUR");
                        stokDovizliToplam += stok.BirimFiyat.GetValueOrDefault() * pariteEur;
                    }
                }

                item.StokToplami_Euro = stokDovizliToplam;
            }
        }




        public List<vwStokTanim> YariMamulHesaplanmisListe(string yariMamulTip)
        {
            List<vwStokTanim> liste = new List<vwStokTanim>();
            List<YariMamulHatData> yariMamulSonData;

            var tumu = repoYariMamul.YariMamulSonDataGetir();


            if (yariMamulTip == "Levha")
                yariMamulSonData = tumu.Where(c => c.Sirano <= 8).ToList();
            else
                yariMamulSonData = tumu.Where(c => c.Sirano >= 8).ToList();

            if (yariMamulSonData == null)
            {
                MessageBox.Show("Yarı mamül ile veri girişini yapınız", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return liste;
            }

            var yariMamulFiyatlar = repoYariMamul.YariMamulSonFiyatlariGetir();

            var kg_1050 = yariMamulSonData.Sum(c => c._1050);
            var kg_1100 = yariMamulSonData.Sum(c => c._1100);
            var kg_1200 = yariMamulSonData.Sum(c => c._1200);
            var kg_3003 = yariMamulSonData.Sum(c => c._3003);
            var kg_8011 = yariMamulSonData.Sum(c => c._8011);

            var kg_8079 = yariMamulSonData.Sum(c => c._8079);
            var kg_8156 = yariMamulSonData.Sum(c => c._8156);
            var kg_8006F = yariMamulSonData.Sum(c => c._8006F);
            var kg_8006M = yariMamulSonData.Sum(c => c._8006M);

            liste.Add(new vwStokTanim { STOK_KODU = "1050", BAKIYE = kg_1050, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_1050.GetValueOrDefault() * yariMamulFiyatlar._1050 });
            liste.Add(new vwStokTanim { STOK_KODU = "1100", BAKIYE = kg_1100, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_1100.GetValueOrDefault() * yariMamulFiyatlar._1100 });
            liste.Add(new vwStokTanim { STOK_KODU = "1200", BAKIYE = kg_1200, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_1200.GetValueOrDefault() * yariMamulFiyatlar._1200 });
            liste.Add(new vwStokTanim { STOK_KODU = "3003", BAKIYE = kg_3003, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_3003.GetValueOrDefault() * yariMamulFiyatlar._3003 });
            liste.Add(new vwStokTanim { STOK_KODU = "8011", BAKIYE = kg_8011, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_8011.GetValueOrDefault() * yariMamulFiyatlar._8011 });

            liste.Add(new vwStokTanim { STOK_KODU = "8079", BAKIYE = kg_8079, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_8079.GetValueOrDefault() * yariMamulFiyatlar._8079 });
            liste.Add(new vwStokTanim { STOK_KODU = "8156", BAKIYE = kg_8156, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_8156.GetValueOrDefault() * yariMamulFiyatlar._8156 });
            liste.Add(new vwStokTanim { STOK_KODU = "8006F", BAKIYE = kg_8006F, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_8006F.GetValueOrDefault() * yariMamulFiyatlar._8006F });
            liste.Add(new vwStokTanim { STOK_KODU = "8006M", BAKIYE = kg_8006M, YariMamulTip = yariMamulTip, StokToplami_Euro = kg_8006M.GetValueOrDefault() * yariMamulFiyatlar._8006M });


            return liste;
        }
    }
}
