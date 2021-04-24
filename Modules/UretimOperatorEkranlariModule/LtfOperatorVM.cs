using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.BC_Dokum.Model;
using mnd.Logic.Helper;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    public class LtfOperatorVM : MyDxViewModelBase
    {
        private string makinaKod;
        private string _aramaMetin;
        private ObservableCollection<DokumBobin> dokumKafileListe;

        DokumRepository repo = new DokumRepository();
        DokumTrackingRepository repo_track = new DokumTrackingRepository();

        private bool ıslemBitirebilirMi;
        private bool ıslemBaslatabilirMi;
        private ObservableCollection<DokumBobinIslemAdim> ekliDokumBobinIslemAdimlari;
        private bool calisiyorMu;
        private bool duruyorMu;

        public bool CalisiyorMu { get => calisiyorMu; set => SetProperty(ref calisiyorMu, value); }

        public bool DuruyorMu { get => duruyorMu; set => SetProperty(ref duruyorMu, value); }

        public string AramaMetin
        {
            get => _aramaMetin;
            set
            {
                if (SetProperty(ref _aramaMetin, value))
                {
                    AraMetinFunction(_aramaMetin);
                }

            }
        }


        public ObservableCollection<DokumBobinIslemAdim> EkliDokumBobinIslemAdimlari
        { get => ekliDokumBobinIslemAdimlari; set => SetProperty(ref ekliDokumBobinIslemAdimlari, value); }
        public DelegateCommand<object> IslemBaslatCommand => new DelegateCommand<object>(IslemBaslat);

        public DelegateCommand<object> IslemBitirCommand => new DelegateCommand<object>(IslemBitir);

        public string IslemBaslamaDurum { get; set; }

        private void IslemBaslat(object obj)
        {
            foreach (var dokumBobin in DokumBobinListe)
            {
                var adm = dokumBobin.DokumBobinIslemAdimlari.FirstOrDefault(c => c.BaslamaTarihi == null);
                adm.BaslamaTarihi = DateTime.Now;
                adm.AdimDurum = BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR;
                dokumBobin.Nereye = dokumBobin.GidecegiYerGetir();
            }

            repo_track.Kaydet();

            IslemBaslamaDurum = "Başladı";


            OnPropertyChanged(nameof(IslemBaslatabilirMi));
            OnPropertyChanged(nameof(IslemBitirebilirMi));

            CalisiyorMu = true;
            DuruyorMu = false;

        }
        private void IslemBitir(object obj)
        {
            foreach (var dokumBobin in DokumBobinListe)
            {
                var adm = dokumBobin.DokumBobinIslemAdimlari.FirstOrDefault(c => c.BitisTarihi == null);
                adm.BitisTarihi = DateTime.Now;
                adm.AdimDurum = BOBIN_ISLEMADIM_DURUM.BİTTİ;
                adm.AktifMi = false;

                var sonraki = dokumBobin.DokumBobinIslemAdimlari
                                .FirstOrDefault(c => c.SiraNo == adm.SiraNo + 1);

                sonraki.AktifMi = true;

                dokumBobin.BobinKonum = BOBIN_KONUM.LTF_SON_DEPO;
                dokumBobin.Nereye = sonraki.MakinaKisaKod;
                dokumBobin.Nereden = "LTF";
            }

            IslemBaslamaDurum = "Bitti";

            OnPropertyChanged(nameof(IslemBaslatabilirMi));
            OnPropertyChanged(nameof(IslemBitirebilirMi));

            CalisiyorMu = false;
            DuruyorMu = true;

            repo_track.Kaydet();
        }

        public bool IslemBaslatabilirMi
        {
            get
            {

                if (DokumBobinListe == null) return false;

                var sonuc = IslemBaslamaDurum != "Başladı" &&
                DokumBobinListe.Any(c => c.BobinAktifAdimGetir().BitisTarihi == null);

                return sonuc;
            }


        }

        public bool IslemBitirebilirMi
        {
            get => IslemBaslamaDurum == "Başladı";
        }



        public ObservableCollection<DokumBobin> DokumBobinListe
        {
            get => dokumKafileListe;
            set => SetProperty(ref dokumKafileListe, value);
        }


        public string MakinaKod { get => makinaKod; set => SetProperty(ref makinaKod, value); }
        public LtfOperatorVM(string formAd)
        {

            IslemBaslamaDurum = "";

        }

        public void Load(object parametreler)
        {
            MakinaKod = parametreler.ToString();

            DokumBobinListe = repo_track.DokumBobinListeGetirFromKonum(MakinaKod);

            EkliDokumBobinIslemAdimlari = DokumBobinListe
                                          .SelectMany(c => c.DokumBobinIslemAdimlari)
                                          .Where(c => c.MakinaIslem == "LTF" && c.AdimDurum == BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR)
                                          .ToObservableCollection();

            OnPropertyChanged(nameof(IslemBaslatabilirMi));

            CalisiyorMu = false;
            DuruyorMu = true;

        }

        public async void AraMetinFunction(string metin)
        {
            await BulEkleAsync(metin);
        }

        private async Task BulEkleAsync(string aramaMetin)
        {
            if (aramaMetin == null) return;
            if (aramaMetin.Length == 0) return;

            int barkodNo;

            var uygun = int.TryParse(aramaMetin, out barkodNo);

            if (uygun == false)
            {
                AramaMetin = "";
                return;
            }

            var bobin = repo.DokumBobinGetir(aramaMetin);


            if (bobin != null)
            {
                DokumBobinListe.Add(bobin);

                repo_track.BobinIzle(bobin);

                bobin.BobinKonum = MakinaKod;

                repo_track.Kaydet();

                foreach (var item in bobin.DokumBobinIslemAdimlari.Where(c => c.MakinaIslem.Contains("LTF")))
                {
                    item.AdimDurum = BOBIN_ISLEMADIM_DURUM.HAZIR;
                    EkliDokumBobinIslemAdimlari.Add(item);
                }

            }

            AramaMetin = "";
        }
    }
}
