using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Newtonsoft.Json;
using mnd.Logic;
using mnd.Logic.BC_App;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_SatinAlmaYeni;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_Satis._Siparis.DataServices;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using mnd.Logic.Services._DTOs;
using mnd.UI.AppModules;
using mnd.UI.Helper;
using mnd.UI.Modules._SatisModule;
using mnd.UI.Modules.Dashboard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using mnd.UI.AppModules.NavMenuModule;
using mnd.UI.GyModules.MesajModule;
using LoginWindow = mnd.UI.AppModules.LoginModule.LoginWindow;

namespace mnd.UI
{
    public class MainViewModel : ViewModelBase
    {
        private int _appOkunmamisMesajSayisi;
        private string _eskiParola;
        private bool _isOpenParolaDegistirDialog;
        private UnitOfWork uow;
        private string _yeniParola;

        public NavMenuViewModel NavMenuViewModel
        {
            get => navMenuViewModel;
            set
            {
                SetProperty(ref navMenuViewModel, value, () => NavMenuViewModel);

            }
        }


        public bool RolSimulasyonYetkiliMi {get;}
        protected virtual IDialogService DialogService { get { return GetService<IDialogService>(); } }
        protected virtual IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        protected virtual INotificationService NotificationService { get { return GetService<INotificationService>(); } }

        public DelegateCommand<string> CloseParolaDegistirDialogCommand => new DelegateCommand<string>(OnCloseParolaDegistirDialog, c => true);

        public DelegateCommand FeedbackCommand => new DelegateCommand(OnExecuteFeedback);

        public DelegateCommand FormClosingCommand => new DelegateCommand(OnFormClosing);


        public ObservableCollection<KullaniciRol> KullaniciRolleri { get; set; }


        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded);

        public DelegateCommand<object> OpenParolaDegistirDialogCommand => new DelegateCommand<object>(c => IsOpenParolaDegistirDialog = true, x => true);


        SurecSqlBroker sqlSatinAlmaBroker;
        SurecSqlBroker sqlSatisSiparisAlmaBroker;

        AppRepositorySA repo_Surec = new AppRepositorySA();

        public ObservableCollection<SurecTanim> SatinAlmaSurecler { get; set; }
        public ObservableCollection<SurecTanim> SatisSiparisSurecler { get; }

        MakinaMaliyetService makinaRaporService = new MakinaMaliyetService();
        MailServiceRepository mailServisRepo = new MailServiceRepository();

        private bool mailGondermeServisiAcik;

        public bool MailGondermeServisiAcik
        {
            get => mailGondermeServisiAcik;
            set
            {
                mailGondermeServisiAcik = value;

                if (value == true)
                    timer.Start();
                else
                    timer.Stop();
            }
        }

        public bool MailSunucuYetkiliMi => AppPandap.AktifKullanici.KullaniciRol == "YONETICI" || AppPandap.AktifKullanici.KullaniciRol == "ADMIN";

        DispatcherTimer timer = new DispatcherTimer();
        private KullaniciRol seciliRol;
        private NavMenuViewModel navMenuViewModel;

        public KullaniciRol SeciliRol
        {
            get => seciliRol;
            set
            {
                seciliRol = value;

                AppPandap.AktifKullanici.KullaniciRol = SeciliRol.RolAd;

                NavMenuViewModel = new NavMenuViewModel();
                SatinAlmaBadgeGuncelle();
                SatisSiparisBadgeGuncelle();
            }
        }

        public MainViewModel()
        {
            AppPandap.LoginAktifMi = true;

            if (IsInDesignMode) return;


            LoginWindow login = new LoginWindow();
            var sonuc = login.ShowDialog();
            if (sonuc == false) Environment.Exit(0);


            uow = new UnitOfWork();
            AppPandap.UygulamaAyarlar = uow.AppRepo.UygulamaBilgiGetir();


            Messenger.Default.Register<MenuSelectedEvent>(this, OnMenuSecildi);

            KullaniciRolleri = uow.KullaniciRepo.KullaniciRolleriGetir();

            RolSimulasyonYetkiliMi = AppPandap.AktifKullanici.KullaniciRol == "YONETICI"
                                    || AppPandap.AktifKullanici.KullaniciRol == "ADMIN";

            NavMenuViewModel = new NavMenuViewModel();


            sqlSatinAlmaBroker = new SurecSqlBroker(SurecAd.SatinAlma);
            SatinAlmaSurecler = repo_Surec.SurecListe("SatınAlma");
            sqlSatinAlmaBroker.SurecEvent += (o, e) => SatinAlmaBadgeGuncelle();

            sqlSatisSiparisAlmaBroker = new SurecSqlBroker(SurecAd.SatisSiparis);
            SatisSiparisSurecler = repo_Surec.SurecListe("Satış_Sipariş");
            sqlSatisSiparisAlmaBroker.SurecEvent += (o, e) => SatisSiparisBadgeGuncelle();

            SatinAlmaBadgeGuncelle();
            SatisSiparisBadgeGuncelle();

            MailTanimListe = mailServisRepo.MailTanimlariGetir();

            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += timer_Tick;

        }

      

        private void timer_Tick(object sender, EventArgs e)
        {
            var performansGunlukMail = MailTanimListe.Where(c => c.MailAd == "MakinaPerformans").FirstOrDefault();
            var performansGunlukMailGonderildiMi = mailServisRepo.GunlukMailGittiMi(performansGunlukMail.MailAd, DateTime.Now.Date);

            if (performansGunlukMailGonderildiMi == false && DateTime.Now > performansGunlukMail.GunlukMailGondermeZamani)
            {
                var raporTarihi = DateTime.Now.AddDays(-1).Date;
                var raporData = makinaRaporService.MakinaPerformansRaporDataGetir(raporTarihi);
                var html = makinaRaporService.PerformansHtmlTabloOlustur(raporData, raporTarihi);

                var cev = makinaRaporService.MailGonder(performansGunlukMail, raporTarihi.ToShortDateString(), html);

                mailServisRepo.MailDbLogEkle(performansGunlukMail.MailAd);
            }

        }

        public void SatinAlmaBadgeGuncelle()
        {
            Thread.Sleep(200);
            var satinalma_surec_sayilari = NavBadgeSatinAlmaService.SatinAlmaSurecSayilariniGetir();
            NavMenuViewModel.SatinAlmaSurec_IstatistikleriniGuncelle(satinalma_surec_sayilari, SatinAlmaSurecler);
        }

        public void SatisSiparisBadgeGuncelle()
        {
            if (AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari != null)
            {
                var bagliKullanicilar = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');
           
            var satis_siparis_surec_sayilari = NavBadgeSatisService
                .SiparisSurecSayilariniGetir(bagliKullanicilar);

            NavMenuViewModel.SatisSiparisSurec_IstatistikleriniGuncelle(satis_siparis_surec_sayilari, SatisSiparisSurecler);
            }
        }

        private void OnFormClosing()
        {
            sqlSatinAlmaBroker.Durdur();
            sqlSatisSiparisAlmaBroker.Durdur();
        }


        private void OnMenuSecildi(MenuSelectedEvent obj)
        {
            ShowView(obj.SeciliMenu);
        }

        private void OnFormLoaded()
        {
            AppPandap.pDocumentManagerService = this.DocumentManagerService;
        }

        public void ShowView(object navItem)
        {
            if (navItem == null) return;

            var main_modul_yol = "";
            var nav = (MenuItem)navItem;

            var doc = DokumanVarMi(nav);

            if (nav.ViewName == null) return;

            object[] objList;

            //TODO form bir daha açılsın testi sonra geri al
            if (doc == null)
            {
                main_modul_yol = "mnd.UI.";
                var yol = main_modul_yol + nav.ViewModelName;

                Type t = Type.GetType(yol);
                var vm = Activator.CreateInstance(t, nav.Name);

                if (nav.ParameterObj != null)
                    objList = JsonConvert.DeserializeObject<List<object>>(nav.ParameterObj).ToArray();
                else
                    objList = JsonConvert.DeserializeObject<List<object>>("[]").ToArray();

                doc = AppPandap.pDocumentManagerService.CreateDocument(main_modul_yol + nav.ViewName, vm);

               vm.GetType().GetMethod("Load")?.Invoke(vm, objList);

                doc.Title = nav.Name;

            }

            AppPandap.pDocumentManagerService.ActiveDocument = doc;
        }

        public int AppOkunmamisMesajSayisi
        {
            get => _appOkunmamisMesajSayisi;
            set
            {
                if (_appOkunmamisMesajSayisi == value)
                {
                    return;
                }

                _appOkunmamisMesajSayisi = value;
                RaisePropertyChanged(nameof(AppOkunmamisMesajSayisi));
            }
        }

        public string EskiParola
        {
            get => _eskiParola;
            set => SetProperty(ref _eskiParola, value, () => EskiParola);
        }

        public bool IsOpenParolaDegistirDialog
        {
            get => _isOpenParolaDegistirDialog;
            set => SetProperty(ref _isOpenParolaDegistirDialog, value, () => IsOpenParolaDegistirDialog);
        }

        public ObservableCollection<SqlGrupSayi> PlanlamaSayilar { get; set; }

        public string YeniParola
        {
            get => _yeniParola;
            set => SetProperty(ref _yeniParola, value, () => YeniParola);
        }
        public List<MailTanim> MailTanimListe { get; }

        private IDocument DokumanVarMi(MenuItem nav)
        {
            var doc = AppPandap.pDocumentManagerService.Documents.Where(c => c.Title.ToString() == nav.Name).FirstOrDefault();
            return doc;
        }

        private void OnCloseParolaDegistirDialog(string obj)
        {
            IsOpenParolaDegistirDialog = false;
            if (obj == "Ok")
            {
                var kul = uow.KullaniciRepo.KullaniciGetir(AppPandap.AktifKullanici.KullaniciId);

                if (EskiParola == kul.Parola)
                {
                    kul.Parola = YeniParola;
                    uow.Commit();
                    MessageBox.Show("Parolanız degisti");

                    EskiParola = string.Empty;
                    YeniParola = string.Empty;
                }
                else
                {
                    MessageBox.Show("Eski parolanız yanlış");
                    EskiParola = string.Empty;
                    YeniParola = string.Empty;
                }
            }
        }

        private void OnExecuteFeedback()
        {
            var rows = new List<Ayarlar>();
            rows.Add(AppPandap.UygulamaAyarlar);

            rows.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            AppMesaj.MesajFormAc(AppPandap.UygulamaAyarlar);
        }

        public void OnClose(CancelEventArgs e)
        {

        }

        public void OnDestroy()
        {

        }
    }
}