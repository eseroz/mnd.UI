namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    using System;
    using System.Linq;
    using System.Windows.Threading;
    using DevExpress.Mvvm;
    using mnd.Common.Helpers;
    using mnd.Logic.BC_Dokum.Data;
    using mnd.Logic.BC_Dokum.Model;
    using mnd.Logic.BC_Uretim;
    using mnd.Logic.Model;
    using mnd.Logic.Persistence;
    using mnd.UI.AppModules.RaporDesignerModule;
    using mnd.UI.Modules.UretimOperatorEkranlariModule.BobinBitisFormlari;
    using mnd.UI.Modules.UretimOperatorEkranlariModule.DurusEkleme;

    public class BobinItemVM : MyBindableBase
    {
        private string islemDurum;

        private UnitOfWork uow = new UnitOfWork();

        DokumTrackingRepository repo_bobin = new DokumTrackingRepository();

        public DokumBobin Bobin { get; set; }

        public DelegateCommand IslemBaslatCommand => new DelegateCommand(IslemBaslat, CanIslemBaslat);

        public DelegateCommand IslemDevamEtCommand => new DelegateCommand(OnIslemDevam, CanIslemDevam);

        public DelegateCommand BobinIslemIptalCommand => new DelegateCommand(OnIslemIptal, CanIslemIptal);

        public DelegateCommand BarkodYazdirCommand => new DelegateCommand(OnBarkodYazdir);

        MakinaAktiviteKayitRepository repoMakinaAktivite = new MakinaAktiviteKayitRepository();

        private void OnBarkodYazdir()
        {
            var raporTanimId = 70;

            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(raporTanimId);

            BobinBarkodModel barkodModel = new BobinBarkodModel();
            barkodModel.BobinNo = Bobin.PlanBobinNo.GetValueOrDefault();
            barkodModel.Kalinlik = Bobin.PlanKalinlik.GetValueOrDefault();
            barkodModel.Alasim = Bobin.AlasimTipKod;

            barkodModel.QrData = "Operator : Ahmet Yılmaz";

            var dsObject = barkodModel;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        public bool IslemGorenBobinMi { get => ıslemGorenBobinMi; set => SetProperty(ref ıslemGorenBobinMi, value); }

        public BobinItemVM()
        {
           
        }

        private bool CanIslemIptal()
        {
            var sonuc = IslemDurum != BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR;
            return sonuc;
        }

        private void OnIslemIptal()
        {
            Messenger.Default.Send(new BobinIslemIptalEvent(Bobin));
        }

        private bool CanIslemDevam()
        {
            var sonuc = IslemDurum == BOBIN_ISLEMADIM_DURUM.DURDURULDU;
            return sonuc;
        }

        private void OnIslemDevam()
        {
            IslemDurum = BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR;
            OnPropertyChanged(nameof(CalisiyorMu));
        }

        public DelegateCommand IslemBitirCommand => new DelegateCommand(IslemBitir, CanIslemBitir);

        private DispatcherTimer dispatcherTimer;

        private DateTime? ıslemBaslamaTarihi;

        private DateTime? ıslemBitisTarihi;

        private bool ıslemGorenBobinMi;

        public DelegateCommand IslemDurdurCommand => new DelegateCommand(IslemDurdur, CanDurdur);

        public DelegateCommand BobinCikarCommand => new DelegateCommand(BobinCikar, CanBobinCikar);

        private bool CanBobinCikar()
        {
            var sonuc = IslemDurum == BOBIN_ISLEMADIM_DURUM.BİTTİ;
            return sonuc;
        }

        private void BobinCikar()
        {
            Bobin.BobinIslemDurum = IslemDurum;
            Bobin.BobinIslemBitisTarihi = IslemBitisTarihi;

            Bobin.Nereden = Bobin.BobinKonum;
            Bobin.Nereye = Bobin.DokumBobinIslemAdimlari.First().MakinaIslem;

            Bobin.BobinKonum = BOBIN_KONUM.DH_BITIS_DEPO;

            repo_bobin.Kaydet();

            Messenger.Default.Send(new BobinCikartildiEvent(Bobin));
        }

        private bool CanIslemBaslat()
        {
            var sonuc = IslemDurum == BOBIN_ISLEMADIM_DURUM.HAZIR;



            return sonuc;
        }

        private bool CanDurdur()
        {
            var sonuc = IslemDurum == BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR;

            return sonuc;
        }

        private bool CanIslemBitir()
        {
            var sonuc = IslemDurum == BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR || IslemDurum == BOBIN_ISLEMADIM_DURUM.DURDURULDU;

            return sonuc;
        }

        public string IslemDurum { get => islemDurum; set => SetProperty(ref islemDurum, value); }

        public DateTime? IslemBaslamaTarihi { get => ıslemBaslamaTarihi; set => SetProperty(ref ıslemBaslamaTarihi, value); }

        public DateTime? IslemBitisTarihi { get => ıslemBitisTarihi; set => SetProperty(ref ıslemBitisTarihi, value); }

        public bool? CalisiyorMu => IslemDurum == BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR;

        public BobinItemVM(DokumBobin bobin)
        {
            Bobin = bobin;
            IslemDurum = Bobin.BobinIslemDurum;
            IslemBaslamaTarihi = Bobin.BobinIslemBaslamaTarihi;
            IslemBitisTarihi = Bobin.BobinIslemBitisTarihi;

            repo_bobin.BobinIzle(Bobin);

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void IslemBaslat()
        {
            IslemDurum = BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR;
            IslemBaslamaTarihi = DateTime.Now;


            OnPropertyChanged(nameof(CalisiyorMu));

            Bobin.BobinIslemDurum = IslemDurum;
            Bobin.BobinIslemBaslamaTarihi = IslemBaslamaTarihi;

            repo_bobin.Kaydet();



            var makinaAktivite = new MakinaAktiviteKayit();
            makinaAktivite.BaşlangıçSaati = Bobin.BobinIslemBaslamaTarihi;
            makinaAktivite.MakinaKisaAd = Bobin.DokumHattiKod;
            makinaAktivite.GirişBobinNo = Bobin.PlanBobinNo.ToString();
            makinaAktivite.KayitEklenmeTarihi = DateTime.Now;
            makinaAktivite.Operatör = AppPandap.AktifKullanici.KullaniciId;
            makinaAktivite.AktiviteDurum = BOBIN_ISLEMADIM_DURUM.ÇALIŞIYOR;

            repoMakinaAktivite.MakinaAktiviteEkle(makinaAktivite);
            repoMakinaAktivite.Kaydet();

          //  Messenger.Default.Send(new BobinUpdateEvent(Bobin));
        }

        private void IslemDurdur()
        {

       

            IslemDurum = BOBIN_ISLEMADIM_DURUM.DURDURULDU;
            OnPropertyChanged(nameof(CalisiyorMu));

            repo_bobin.Kaydet();

            DurusEkleForm f = new DurusEkleForm();
            DurusEkleFormVM vm = new DurusEkleFormVM();

            

            f.DataContext = vm;

            var cev = f.ShowDialog();


           // Messenger.Default.Send(new BobinUpdateEvent(Bobin));
        }

        private void IslemBitir()
        {
            BobinBitirForm f = new BobinBitirForm();
            BobinBitirFormVM vm = new BobinBitirFormVM();
            vm.Model = new BobinBitirModel();

            f.DataContext = vm;
            var cev = f.ShowDialog();


            IslemDurum = BOBIN_ISLEMADIM_DURUM.BİTTİ;
          
            IslemBitisTarihi = DateTime.Now;

            Bobin.BobinIslemDurum = IslemDurum;
            Bobin.BobinIslemBitisTarihi = IslemBitisTarihi;

            Bobin.ReelKalinlik = vm.Model.Kalinlik;
            Bobin.ReelEn = vm.Model.En;
            Bobin.ReelMiktar = vm.Model.Miktar;
            Bobin.ReelBitisTarihi = Bobin.ReelBitisTarihi??DateTime.Now;

            repo_bobin.Kaydet();

            OnPropertyChanged(nameof(CalisiyorMu));

            var calisilanBobinActivite = repoMakinaAktivite.CalisilanBobinAktiviteGetir(Bobin.DokumHattiKod);
            calisilanBobinActivite.BitişSaati = DateTime.Now;
            calisilanBobinActivite.ÇıkışBobinNo = Bobin.PlanBobinNo.ToString();
            calisilanBobinActivite.ÇıkışEni = Bobin.ReelEn;
            calisilanBobinActivite.ÇıkışBobinAğırlığı = Bobin.ReelMiktar;
            calisilanBobinActivite.ÇıkışKalınlık = Bobin.ReelKalinlik;

            repoMakinaAktivite.Kaydet();
            //Messenger.Default.Send(new BobinUpdateEvent(Bobin));
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (IslemBaslamaTarihi != null && IslemBitisTarihi == null)
                OnPropertyChanged(nameof(GecenSureStr));
        }

        public string GecenSureStr
        {
            get
            {
                if (IslemBaslamaTarihi == null) return "";
                return GetElapsedTimeString(DateTime.Now - IslemBaslamaTarihi.GetValueOrDefault());
            }
        }

        public string GetElapsedTimeString(TimeSpan result)
        {
            string elapsedTimeString = string.Format("{0}:{1}:{2}",
                                              result.Hours.ToString("00"),
                                              result.Minutes.ToString("00"),
                                              result.Seconds.ToString("00")
                                             );

            return elapsedTimeString;
        }
    }
}
