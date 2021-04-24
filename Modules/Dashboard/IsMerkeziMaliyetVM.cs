using DevExpress.Mvvm;
using mnd.Logic.BC_App;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.Helper;
using mnd.UI.Helper;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis;
using mnd.UI.Modules.SatinAlmaModuleYeni.Stoklar;

namespace mnd.UI.Modules.Dashboard
{
    public class IsMerkeziMaliyetVM: MyDxViewModelBase
    {

        public ObservableCollection<IsMerkezi> IsMerkezleri { get; set; }
        public ObservableCollection<DepoCikisFisDTO> DepoCikisFisListe { get => depoCikisFisListe; set => SetProperty(ref depoCikisFisListe, value); }

        IsMerkeziRepository repo = new IsMerkeziRepository();
        public ObservableCollection<FisKalemViewModel> DepoCikisFisListeKalemli
        { get => depoCikisFisListeKalemli; set => SetProperty(ref depoCikisFisListeKalemli, value); }


        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnYenile);


        public IsMerkeziMaliyetVM(string formAd)
        {
            OnYenile();
        }

        private void OnYenile()
        {
            var repoDc = new DepoRepository();
            IsMerkezleri = repo.TumIsMerkezleriGetir();

            KalemGorunumuTazele();
        }

     
        private DepoRepository repoDepo = new DepoRepository();
        private ObservableCollection<DepoCikisFisDTO> depoCikisFisListe;
        private ObservableCollection<FisKalemViewModel> depoCikisFisListeKalemli;

      
        public void KalemGorunumuTazele()
        {
            DepoCikisFisListe = repoDepo.DepoFisListesiGetir().ToObservableCollection();

            StokTanimNetsisRepository repoNetsisFiyatlar = new StokTanimNetsisRepository();

            var dict = repoNetsisFiyatlar.StokAlimSonFiyatlariGetir().ToDictionary(c => c.StokKod);

            var dovizKurlari = NetsisService.NetsisBelirliTarihtenSonrakiDovizKurlariniGetir(DateTime.Now.AddYears(-3));

            DepoCikisFisListeKalemli = DepoCikisFisListe.SelectMany(fisDTO => fisDTO.KalemlerDTO,
            (fis, kalem) => new { fis, kalem })
            .Select(s => new FisKalemViewModel
            {
                FisNo = s.fis.FisNo,
                FisTarihi = s.fis.FisTarihi,
                MasrafMerkeziAd = s.fis.MasrafMerkeziAd,
                MasrafMerkeziKod = s.fis.MasrafMerkeziKod,

                MasrafSeviye1= Seviye1_Ad_Getir(s.fis.MasrafMerkeziKod),
                MasrafSeviye2 = Seviye2_Ad_Getir(s.fis.MasrafMerkeziKod),
                MasrafSeviye3 = Seviye3_Ad_Getir(s.fis.MasrafMerkeziKod),


                TalepEdenKisi = s.fis.TalepEdenKisi,
                TeslimAlanKisi = s.fis.TeslimAlanKisi,
                StokKodu = s.kalem.StokKodu,
                StokAd = s.kalem.StokAd,
                IlgiliUnite=s.kalem.IlgiliUnite,
                CikisMiktar = s.kalem.CikisMiktar,
                OlcuBirimAd = s.kalem.OlcuBirimAd?.ToUpper(),

                BirimFiyat = dict.ContainsKey(s.kalem.StokKodu) == true ? dict[s.kalem.StokKodu].BirimFiyat : 0,
                DovizTip = dict.ContainsKey(s.kalem.StokKodu) == true ? dict[s.kalem.StokKodu].DovizTip : null,

            })
            .OrderByDescending(c => c.FisTarihi)
            .ToObservableCollection<FisKalemViewModel>();

            foreach (var item in DepoCikisFisListeKalemli)
            {
                var parite = StokListViewModel.PariteGetir(item.DovizTip, item.FisTarihi.Date.AddDays(-3), dovizKurlari, "EUR");
                item.PariteEuro = parite;
                item.Toplam_Euro = item.CikisMiktar * item.BirimFiyat.GetValueOrDefault() * parite;
            }

        }


        public string Seviye1_Ad_Getir(int kod_str)
        {
            int kod = kod_str;

            var satir = IsMerkezleri.Where(c => c.Kod == kod).FirstOrDefault();
            if (satir == null) return "Boş";

            if (satir.Seviye == 1) return satir.Tanim;

            if (satir.Seviye == 2)
            {
                var seviye1_satir= IsMerkezleri.Where(c => c.Kod == satir.ParentId).First();
                return seviye1_satir.Tanim;
            }

            if (satir.Seviye == 3)
            {
                var seviye2_satir = IsMerkezleri.Where(c => c.Kod == satir.ParentId).First();
                var seviye1_satir = IsMerkezleri.Where(c => c.Kod == seviye2_satir.ParentId).First();
                return seviye1_satir.Tanim;
            }

            return "";

        }

        public string Seviye2_Ad_Getir(int kod_str)
        {
            int kod = kod_str;

            var satir = IsMerkezleri.Where(c => c.Kod == kod).FirstOrDefault();
            if (satir == null) return "Boş";

            if (satir.Seviye == 1) return "-";

            if (satir.Seviye == 2) return satir.Tanim;
          
            if (satir.Seviye == 3)
            {
                var seviye2_satir = IsMerkezleri.Where(c => c.Kod == satir.ParentId).First();
                return seviye2_satir.Tanim;
            }

            return "";
        }

        public string Seviye3_Ad_Getir(int kod_str)
        {
            int kod = kod_str;

            var satir = IsMerkezleri.Where(c => c.Kod == kod).FirstOrDefault();
            if (satir == null) return "Boş";

            if (satir.Seviye == 1) return "-";

            if (satir.Seviye == 2) return "-";

            if (satir.Seviye == 3)
            {
                return satir.Tanim;
            }

            return "";
        }



    }
}
