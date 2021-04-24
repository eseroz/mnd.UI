using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.BC_Dokum.Model;
using mnd.Logic.BC_Uretim;
using mnd.Logic.Helper;
using mnd.UI.Helper;
using mnd.UI.Modules.UretimOperatorEkranlariModule.DurusEkleme;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    public class ShOperatorVM : MyDxViewModelBase
    {
        private string _aramaMetin;
        private ObservableCollection<DokumBobin> dokumBobinListe;
        private ObservableCollection<DokumBobinIslemAdim> ekliDokumPasBobinIslemAdimlari;
        private bool ıslemBaslatabilirMi;
        private bool ıslemBitirebilirMi;
        private string makinaKod;
        private int pas1_Sayi;
        private int pas2_Sayi;
        private int pas3_Sayi;
        private int pas4_Sayi;
        private int pas5_Sayi;
        private int pas6_Sayi;

        public ObservableCollection<DokumBobinIslemAdim> EkliDokumBobinIslemAdimlari
        { get => ekliDokumBobinIslemAdimlari; set => SetProperty(ref ekliDokumBobinIslemAdimlari, value); }

        DokumTrackingRepository repo_track = new DokumTrackingRepository();
        private ObservableCollection<DokumBobinIslemAdim> ekliDokumBobinIslemAdimlari;
        private bool calisiyorMu;
        private bool duruyorMu;
        private DispatcherTimer dispatcherTimer;

        public ShOperatorVM(string formAd)
        {


        }

       

        public string AktifPas { get; set; }
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

        public ObservableCollection<DokumBobin> DokumBobinListe
        {
            get => dokumBobinListe;
            set => SetProperty(ref dokumBobinListe, value);
        }


        public ObservableCollection<DokumBobinIslemAdim> EkliDokumBobinPasIslemAdimlari
        { get => ekliDokumPasBobinIslemAdimlari; set => SetProperty(ref ekliDokumPasBobinIslemAdimlari, value); }



        public DelegateCommand<DokumBobin> IslemBaslatCommand => new DelegateCommand<DokumBobin>(IslemBaslat);

        public DelegateCommand<DokumBobin> IslemBitirCommand => new DelegateCommand<DokumBobin>(IslemBitir);

        public DelegateCommand<DokumBobin> IslemDurdurCommand => new DelegateCommand<DokumBobin>(IslemDurdur);

        private void IslemDurdur(DokumBobin obj)
        {
            var repoMakinaIslemKayit = new MakinaAktiviteKayitRepository();
            var repoDurusListe = new MakinaDurusRepository();

            var dokum = obj as DokumBobin;

            var makinaIlkRunListe = repoMakinaIslemKayit.MakinaDurusHareketleriGetirRunSonrasi("SH");


            if (makinaIlkRunListe.Count == 0)
            {
                // veritabanında otomatik oluşturulup sonra gerçek bilgilerle değişecek

                var ilkRun1 = new MakinaAktiviteKayit();
                ilkRun1.MakinaKisaAd = dokum.DokumHattiKod;
                ilkRun1.MakinaAd = dokum.DokumHattiKod;
                ilkRun1.DuruşKodu = "1000";

                ilkRun1.GirişBobinNo = dokum.PlanBobinNo.ToString();
                ilkRun1.ÇıkışBobinNo = ilkRun1.GirişBobinNo;

                ilkRun1.GirişEni = dokum.PlanEn;
                ilkRun1.GirişBobinAğırlığı = dokum.PlanMiktar;
                ilkRun1.GirişKalınlık = dokum.PlanKalinlik;

                ilkRun1.ÇıkışEni = ilkRun1.GirişEni;
                ilkRun1.ÇıkışBobinAğırlığı = ilkRun1.GirişBobinAğırlığı;
                ilkRun1.ÇıkışKalınlık = ilkRun1.GirişKalınlık;

                ilkRun1.BaşlangıçSaati = DateTime.Now.AddMinutes(-500);
                ilkRun1.BitişSaati = DateTime.Now.AddMinutes(-100); ;
                ilkRun1.GirişKafileNo = "";
                ilkRun1.Operatör = AppPandap.AktifKullanici.KullaniciId;
                ilkRun1.KayitEklenmeTarihi = DateTime.Now;
                ilkRun1.Tarih = DateTime.Now.Date;
                ilkRun1.RowGuid = Guid.NewGuid();

                repoMakinaIslemKayit.MakinaAktiviteEkle(ilkRun1);
                repoMakinaIslemKayit.Kaydet();

            }


            repoMakinaIslemKayit = new MakinaAktiviteKayitRepository();
            var liste = repoMakinaIslemKayit.MakinaDurusHareketleriGetirRunSonrasi("SH");

            var ilkRun = new MakinaAktiviteKayit()
            {
                BaşlangıçSaati = DateTime.Now.AddMinutes(-50),
                BitişSaati=DateTime.Now.AddMinutes(-40),
                DuruşKodu = "1000",
                MakinaDuruşAd = "RUN",

                GirişBobinNo = dokum.PlanBobinNo.ToString(),
                GirişEni = dokum.PlanEn,
                GirişBobinAğırlığı = dokum.PlanMiktar,
                GirişKalınlık = dokum.PlanKalinlik,

                Operatör = AppPandap.AktifKullanici.KullaniciId,
                Tarih = DateTime.Now.Date,

                MakinaKisaAd = "SH"
            };


            var sonRun = new MakinaAktiviteKayit()
            {
                BaşlangıçSaati = DateTime.Now,
                DuruşKodu = "1000",
                MakinaDuruşAd = "RUN",

                GirişBobinNo = dokum.PlanBobinNo.ToString(),
                GirişEni = dokum.PlanEn,
                GirişBobinAğırlığı = dokum.PlanMiktar,
                GirişKalınlık = dokum.PlanKalinlik,

                Operatör = AppPandap.AktifKullanici.KullaniciId,
                Tarih = DateTime.Now.Date,

                MakinaKisaAd = "SH"
            };


            DurusEkleFormVM vm = new DurusEkleFormVM(ilkRun, sonRun);
            vm.MakinaDurusKodlari = repoDurusListe.MakinaDurusTanimlariGetir();
            vm.MakinaDurusHareketEdit = new MakinaAktiviteKayit();

            vm.RunSonrasiIslemKayitListe = repoMakinaIslemKayit
                .MakinaDurusHareketleriGetirRunSonrasi("SH")
                .Skip(1)
                .ToObservableCollection();

            DurusEkleForm form = new DurusEkleForm();

            form.DataContext = vm;

            var cev = form.ShowDialog();

            if (vm.Ok == true)
            {
                var yeniKayitlar = vm.RunSonrasiIslemKayitListe.Where(c => c.KayitEklenmeTarihi == null).ToList();

                foreach (var makinaIslem in yeniKayitlar)
                {
                    makinaIslem.KayitEklenmeTarihi = DateTime.Now;
                    makinaIslem.Operatör = AppPandap.AktifKullanici.KullaniciId;
                    makinaIslem.Tarih = DateTime.Now.Date;
                    repoMakinaIslemKayit.MakinaAktiviteEkle(makinaIslem);
                }

                repoMakinaIslemKayit.MakinaAktiviteEkle(vm.Run2);

                repoMakinaIslemKayit.Kaydet();

          
            }
        }

        public bool IslemBitirebilirMi
        {
            get => ıslemBitirebilirMi;
            set => ıslemBitirebilirMi = value;
        }

        public string MakinaKod { get => makinaKod; set => SetProperty(ref makinaKod, value); }
        public int Pas1_Sayi { get => pas1_Sayi; set => SetProperty(ref pas1_Sayi, value); }
        public int Pas2_Sayi { get => pas2_Sayi; set => SetProperty(ref pas2_Sayi, value); }
        public int Pas3_Sayi { get => pas3_Sayi; set => SetProperty(ref pas3_Sayi, value); }
        public int Pas4_Sayi { get => pas4_Sayi; set => SetProperty(ref pas4_Sayi, value); }
        public int Pas5_Sayi { get => pas5_Sayi; set => SetProperty(ref pas5_Sayi, value); }
        public int Pas6_Sayi { get => pas6_Sayi; set => SetProperty(ref pas6_Sayi, value); }
        public DelegateCommand<string> PasFiltreCommand => new DelegateCommand<string>(OnPasFiltre);

        public ObservableCollection<string> ShPasListe { get; set; }

        public async void AraMetinFunction(string metin)
        {
            await BulEkleAsync(metin);
        }

        public void Load(object parametreler)
        {
            MakinaKod = parametreler.ToString();

            DokumBobinListe = repo_track.DokumBobinListeGetirFromKonum(MakinaKod);

            EkliDokumBobinIslemAdimlari = DokumBobinListe
                               .SelectMany(c => c.DokumBobinIslemAdimlari)
                               .Where(c => c.MakinaIslem.Contains("SH") && c.AdimDurum == BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR)
                               .ToObservableCollection();


           



            foreach (var bobin in DokumBobinListe)
            {
                var aktifAdim = bobin.BobinAktifAdimGetir();

                if (aktifAdim.BaslamaTarihi != null && aktifAdim.BitisTarihi == null) aktifAdim.CalisiyorMu = true;

                if (aktifAdim.BaslamaTarihi == null && aktifAdim.BitisTarihi == null) aktifAdim.DuruyorMu = true;

        

                if (aktifAdim.MakinaIslem.Contains("SH/1")) Pas1_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/2")) Pas2_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/3")) Pas3_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/4")) Pas4_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/5")) Pas5_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/6")) Pas6_Sayi++;

            }


            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            foreach (var bobin in DokumBobinListe)
            {
                foreach (var item in bobin.DokumBobinIslemAdimlari)
                {
                    if( item.BaslamaTarihi!=null && item.BitisTarihi==null)
                        item.OnPropertyChanged(nameof(item.GecenSureStr));
                }

            }
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

            var bobin = repo_track.DokumBobinGetir(aramaMetin);

            if (bobin != null)
            {
                DokumBobinListe.Add(bobin);
                repo_track.BobinIzle(bobin);

                bobin.BobinKonum = MakinaKod;

                repo_track.Kaydet();

                foreach (var item in bobin.DokumBobinIslemAdimlari.Where(c => c.MakinaIslem.Contains("SH")))
                {
                    EkliDokumBobinIslemAdimlari.Add(item);
                }

                var aktifAdim = bobin.BobinAktifAdimGetir();

                if (aktifAdim.MakinaIslem.Contains("SH/1")) Pas1_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/2")) Pas2_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/3")) Pas3_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/4")) Pas4_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/5")) Pas5_Sayi++;
                if (aktifAdim.MakinaIslem.Contains("SH/6")) Pas6_Sayi++;


                if (aktifAdim.BaslamaTarihi != null && aktifAdim.BitisTarihi == null) aktifAdim.CalisiyorMu = true;

                if (aktifAdim.BaslamaTarihi == null && aktifAdim.BitisTarihi == null) aktifAdim.DuruyorMu = true;



            }

            AramaMetin = "";
        }

        private void IslemBaslat(DokumBobin dokumBobin)
        {
            var adm = dokumBobin.BobinAktifAdimGetir();
            adm.BaslamaTarihi = DateTime.Now;
            adm.AdimDurum = BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR;
            dokumBobin.Nereye = dokumBobin.GidecegiYerGetir();

            adm.CalisiyorMu = true;
            adm.DuruyorMu = false;

            repo_track.Kaydet();
        }

        private void IslemBitir(DokumBobin dokumBobin)
        {
            var adm = dokumBobin.DokumBobinIslemAdimlari.FirstOrDefault(c => c.BitisTarihi == null);
            adm.BitisTarihi = DateTime.Now;
            adm.AdimDurum = BOBIN_ISLEMADIM_DURUM.BİTTİ;
            adm.AktifMi = false;

            dokumBobin.BobinKonum = BOBIN_KONUM.SH_SON_DEPO;
            dokumBobin.Nereden = "SH";
            dokumBobin.Nereye = "";

            adm.CalisiyorMu = false;
            adm.DuruyorMu = true;


            if (adm.MakinaIslem.Contains("SH/1")) Pas1_Sayi--;
            if (adm.MakinaIslem.Contains("SH/2")) Pas2_Sayi--;
            if (adm.MakinaIslem.Contains("SH/3")) Pas3_Sayi--;
            if (adm.MakinaIslem.Contains("SH/4")) Pas4_Sayi--;
            if (adm.MakinaIslem.Contains("SH/5")) Pas5_Sayi--;
            if (adm.MakinaIslem.Contains("SH/6")) Pas6_Sayi--;

            var sonraki = dokumBobin.DokumBobinIslemAdimlari
                            .FirstOrDefault(c => c.SiraNo == adm.SiraNo + 1);

            if (sonraki != null)
            {
                sonraki.AktifMi = true;
                dokumBobin.Nereye = sonraki.MakinaKisaKod;

                if (sonraki.MakinaIslem.Contains("SH/1")) Pas1_Sayi++;
                if (sonraki.MakinaIslem.Contains("SH/2")) Pas2_Sayi++;
                if (sonraki.MakinaIslem.Contains("SH/3")) Pas3_Sayi++;
                if (sonraki.MakinaIslem.Contains("SH/4")) Pas4_Sayi++;
                if (sonraki.MakinaIslem.Contains("SH/5")) Pas5_Sayi++;
                if (sonraki.MakinaIslem.Contains("SH/6")) Pas6_Sayi++;

            }

            repo_track.Kaydet();

        }

        private void OnPasFiltre(string filtre)
        {
            AktifPas = filtre;

            filtre = "SH/" + filtre;

            EkliDokumBobinPasIslemAdimlari = EkliDokumBobinIslemAdimlari
                 .Where(c => c.MakinaIslem.Contains(filtre))
                 .Where(c => c.AdimDurum == "Aktif")
                 .ToObservableCollection();
        }
    }
}
