using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using Pandap.Logic.Helper;
using Pandap.Logic.Model.App;
using Pandap.Logic.Model.Netsis;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Model._DTOs;
using Pandap.Logic.Model._Ref;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._Mesajlasma;
using Pandap.UI.AppModule._RaporDesigner;
using Pandap.UI.AppModule._SplashScreen;
using Pandap.UI.AppModule.__SiparisKalem;
using Pandap.UI.Helper;
using Pandap.UI.Services;
using PandapObjectHelper = Pandap.UI.Helper.PandapObjectHelper;
using Pandap.Logic.Model.Operasyon;

namespace Pandap.UI.AppModule.__Siparis
{
    public class SiparisViewModel : MyDxViewModelBase
    {
        public bool IsFormLoaded { get; set; } = false;

        private UnitOfWork uow = new UnitOfWork();

        public KayitModu SiparisKayitModu { get; set; }
        public KayitModu KalemKayitModu { get; set; }

        public DelegateCommand SiparisYeniCommand => new DelegateCommand(SipariYeni, canYeniSiparis);
        public DelegateCommand SiparisKapasitiftenYeniCommand => new DelegateCommand(KapasitiftenYeni, canKapasitiftenYeniSiparis);

      

        public DelegateCommand SiparisKaydetCommand => new DelegateCommand(SiparisKaydet, canSiparisKaydet);
        public DelegateCommand SiparisKopyalaCommand => new DelegateCommand(SipKopyala, canSiparisKopyala);

        private void SipKopyala()
        {
            SiparisKopyala(KayitModu.KopyadanYeni);
        }

        private bool canKapasitiftenYeniSiparis()
        {
            return SeciliSiparis.KapasitifMi.Value;
        }

        private void KapasitiftenYeni()
        {
            SiparisKopyala(KayitModu.KapasitiftenYeni);
        }


        public DelegateCommand<Language> TeyitFormYazdirTrCommand => new DelegateCommand<Language>(x => SiparisTeyitFormuYazdir(Language.TR));
        public DelegateCommand<Language> TeyitFormYazdirEnCommand => new DelegateCommand<Language>(x => SiparisTeyitFormuYazdir(Language.EN));
        public DelegateCommand<Language> TeyitFormDesignerCommand => new DelegateCommand<Language>(x => TeyitFormDesignerGoster(Language.EN));

      

        public DelegateCommand KalemYeniCommand => new DelegateCommand(YeniKalem, canYeniKalem);
        public DelegateCommand KalemKaydetCommand => new DelegateCommand(KalemKaydet, canKalemKaydet);
        public DelegateCommand KalemEditCommand => new DelegateCommand(KalemEdit, canKalemEdit);
        public DelegateCommand KalemSilCommand => new DelegateCommand(KalemSil, canKalemSil);

        public DelegateCommand KalemKopyalaCommand => new DelegateCommand(KopyadanYeniKalem, canKopyadanYeniKalem);
        public DelegateCommand KalemVazgecCommand => new DelegateCommand(KalemVazgec, canVazgec);

        public DelegateCommand NetsiseAktarCommand => new DelegateCommand(NetsiseAktar, canNestiseAktar);

        public DelegateCommand StokDetayGosterCommand { get; set; }
        public DelegateCommand ParametreleriGuncelleCommand { get; set; }

        public DelegateCommand SiparisMesajYazCommand => new DelegateCommand(SiparisMesajYaz);
        public DelegateCommand<object> SiparisKalemMesajYazCommand => new DelegateCommand(SiparisKalemMesajYaz);

        public DelegateCommand UrunKodunuBulCommand => new DelegateCommand(StokKodunuBul);
        public DelegateCommand FormLoadedCommand => new DelegateCommand(FormLoaded);

        public DelegateCommand<CancelEventArgs> FormClosingCommand => new DelegateCommand<CancelEventArgs>(FormClosing);

        private ObservableCollection<EntityLog> entitiyLogs;

        public ObservableCollection<EntityLog> EntityLogs
        {
            get => entitiyLogs;
            set => SetProperty(ref entitiyLogs, value);
        }

        private EntityLog seciliEntityLog;

        public EntityLog SeciliEntityLog
        {
            get => seciliEntityLog;
            set
            {
                if (SetProperty(ref seciliEntityLog, value))
                {
                    if (seciliEntityLog == null) return;

                    if (seciliEntityLog.Id == 0)
                    {
                        Load(SeciliSiparis.SiparisKod);
                        IsEditableForm = true;
                        return;
                    }

                    var jsonString = uow.EntiyLogRepo.GetEntityLogsFromGuidJsonStream(seciliEntityLog.Id);

                    SeciliSiparis = JsonConvert.DeserializeObject<Siparis>(jsonString);
                    SiparisKayitModu = KayitModu.LogKaydi;
                    IsEditableForm = false;
                }
            }
        }

        private KalemViewModel kalemVM;

        private bool isEditableForm;

        public bool IsEditableForm
        {
            get => isEditableForm;
            set => SetProperty(ref isEditableForm, value);
        }

        public SiparisViewModel()
        {
            uow = new UnitOfWork();
            IsEditableForm = true;
        }

        private void FormLoaded()
        {
            IsFormLoaded = true;

            KalemVM = new KalemViewModel();

            KalemLookUpYukle(KalemVM);

            KalemVM.IsVisible = false;
            KalemVM.KayitModu = KayitModu.Varsayılan;
        }

        public void Load(string siparisKod)
        {
            uow = new UnitOfWork(); // reload işleminden kurtulmak için başka yerden de çağrılıyor çünkü
            IsFormLoaded = false;  // form load ->eventtocommanddan alınacak
            SeciliSiparis = uow.SiparisRepo.SiparisGetir(siparisKod);

            Musteriler = uow.SiparisLookUpRepo.CariKartlariGetir(PandapGlobal.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';'));
            OdemeTipleri = uow.SiparisLookUpRepo.OdemeTipleriGetir();
            SatisTipleri = uow.SiparisLookUpRepo.SatisTipleriGetir();
            DovizTipleri = uow.SiparisLookUpRepo.DovizTipleriGetir();
            TeslimTipleri = uow.SiparisLookUpRepo.TeslimTipleriGetir();

            if (String.IsNullOrEmpty(siparisKod))
            {
                SiparisKayitModu = KayitModu.Yeni;
                SeciliSiparis = Siparis.YeniSiparisOlustur();

                SeciliSiparis.SiparisSurecDurum = SiparisSurecDurum.SATISTA;
                SeciliSiparis.CreatedUserId = PandapGlobal.AktifKullanici.KullaniciId;
            }
            else
            {
                SeciliSiparis = uow.SiparisRepo.SiparisGetir(siparisKod);
                SiparisKayitModu = KayitModu.Edit;
            }

            SeciliSiparis.PropertyChanged += SeciliSiparis_PropertyChanged;

            this.EntityLogs = uow.EntiyLogRepo.GetEntityLogsFromGuidWithoutJsonStream(SeciliSiparis.RowGuid.Value);

            this.EntityLogs.Insert(0, new EntityLog { Id = 0, KullaniciAdSoyad = "Son Hali" });

            CariRiskDurum = uow.NetsisRepo.RiskDurumGetir(SeciliSiparis.CariKod);

            if (CariRiskDurum == null) { MessageBox.Show("Risk Durum Bulunamadı"); return; }

            IsEditableForm = true;
        }

        private CariKart seciliMusteri;

        public CariKart SeciliMusteri
        {
            get => seciliMusteri;

            set
            {
                if (!IsFormLoaded) return;

                if (!SetProperty<CariKart>(ref seciliMusteri, value)) return;

                if (seciliMusteri != null)
                {
                    SeciliSiparis.SatisTipKod = seciliMusteri.UlkeKod == "TR" ? SATISTIPI.YI : SATISTIPI.YD;

                    TeslimTipleri = seciliMusteri.UlkeKod == "TR" ? uow.SiparisLookUpRepo.TeslimTipleriGetir(SATISTIPI.YI)
                                                                  : uow.SiparisLookUpRepo.TeslimTipleriGetir(SATISTIPI.YD);

                    SeciliSiparis.FaturaDovizTipKod = seciliMusteri.DovizAd;
                    SeciliSiparis.TakipDovizTipKod = seciliMusteri.DovizAd;

                    var dovizKur = uow.SiparisLookUpRepo.NetsisDunkuDovizKurunuGetir(seciliMusteri.DovizAd);

                    SeciliSiparis.FaturaDovizKuru = dovizKur != null ? (decimal)dovizKur.DovizSatis : 0;
                    SeciliSiparis.TakipDovizKuru = dovizKur != null ? (decimal)dovizKur.DovizSatis : 0;

                    CariRiskDurum = uow.NetsisRepo.RiskDurumGetir(seciliMusteri.CariKod);

                    if (CariRiskDurum == null) { MessageBox.Show("Risk Durum Bulunamadı"); return; }
                }
            }
        }

        private void SeciliSiparis_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!IsFormLoaded) return;

            if (e.PropertyName == nameof(SeciliSiparis.FaturaDovizTipKod))
            {
                var dovizKur = uow.SiparisLookUpRepo.NetsisDunkuDovizKurunuGetir(SeciliSiparis.FaturaDovizTipKod);
                SeciliSiparis.FaturaDovizKuru = dovizKur != null ? (decimal)dovizKur.DovizSatis : 0;
            }

            if (e.PropertyName == nameof(SeciliSiparis.TakipDovizTipKod))
            {
                var dovizKur = uow.SiparisLookUpRepo.NetsisDunkuDovizKurunuGetir(SeciliSiparis.TakipDovizTipKod);
                SeciliSiparis.TakipDovizKuru = dovizKur != null ? (decimal)dovizKur.DovizSatis : 0;
            }
        }

        public KalemViewModel KalemVM
        {
            get => kalemVM;
            set
            {
                SetProperty(ref kalemVM, value);
            }
        }

        private SiparisKalem seciliKalem;

        public SiparisKalem SeciliKalem
        {
            get => seciliKalem;
            set
            {
                if (SetProperty(ref seciliKalem, value))
                {
                    if (seciliKalem != null) seciliKalem.PropertyChanged += SeciliKalem_PropertyChanged;
                }
            }
        }

        private void SeciliKalem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SatirSecildiMi")
            {
                OnPropertyChanged(nameof(SeciliKalemSayisi));
            }
        }

        public int SeciliKalemSayisi
        {
            get
            {
                return SeciliSiparis.SiparisKalemleri.Where(c => c.SatirSecildiMi == true).Count();
            }
        }

        private Siparis seciliSiparis;

        public Siparis SeciliSiparis
        {
            get => seciliSiparis;
            set => SetProperty(ref seciliSiparis, value);
        }

        #region LookUpData

        private ObservableCollection<CariKart> musteriler;

        public ObservableCollection<CariKart> Musteriler
        {
            get => musteriler;
            set => SetProperty(ref musteriler, value);
        }

        private ObservableCollection<OdemeTip> odemeSekilleri;

        public ObservableCollection<OdemeTip> OdemeTipleri
        {
            get => odemeSekilleri;
            set => SetProperty(ref odemeSekilleri, value);
        }

        private ObservableCollection<TeslimTip> teslimTipleri;

        public ObservableCollection<TeslimTip> TeslimTipleri
        {
            get => teslimTipleri;
            private set => SetProperty(ref teslimTipleri, value);
        }

        private ObservableCollection<DovizTip> dovizTipleri;

        public ObservableCollection<DovizTip> DovizTipleri
        {
            get => dovizTipleri;
            private set => SetProperty(ref dovizTipleri, value);
        }

        private ObservableCollection<SatisTip> satisTipleri;

        public ObservableCollection<SatisTip> SatisTipleri
        {
            get => satisTipleri;
            set => SetProperty(ref satisTipleri, value);
        }

        #endregion LookUpData

        private bool canSiparisKaydet()
        {
            return SeciliSiparis != null && SeciliSiparis.SiparisSurecDurum == SiparisSurecDurum.SATISTA;
        }

        private void StokKodunuBul()
        {
            var urun = KalemVM.Urunler.Where(c =>
               c.AlasimKod == KalemVM.SiparisKalem.AlasimTipKod &&
               c.UrunGrubu == KalemVM.SiparisKalem.KullanimAlanTipKod &&
               c.KalinlikIfade == (KalemVM.SiparisKalem.Kalinlik_micron <= c.KalinlikKosul ? "İnce" : "Kalın") &&
               (c.YuzeyKod == KalemVM.SiparisKalem.YuzeyTipKod || c.YuzeyKod == null)
             ).FirstOrDefault();

            if (urun == null)
            {
                MessageBox.Show("Uygun stok kodu bulunamadı", "Stok", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                MessageBox.Show("Bulunan Stok Kodu : " + urun.UrunKod, "Stok", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (urun != null) KalemVM.SiparisKalem.UrunKod = urun.UrunKod;
        }

        private bool canMesajYaz()
        {
            return (SeciliSiparis != null && SeciliSiparis.RowGuid != null);
        }

        private void SiparisMesajYaz()
        {
            MesajlasmaViewModel v = new MesajlasmaViewModel(SeciliSiparis.RowGuid.Value, PandapGlobal.AktifKullanici.AdSoyad);
            MesajlasmaWindow w = new MesajlasmaWindow();
            w.DataContext = v;
            w.Show();
        }

        private void SiparisKalemMesajYaz()
        {
            MesajlasmaViewModel v = new MesajlasmaViewModel(SeciliKalem.RowGuid.Value, PandapGlobal.AktifKullanici.AdSoyad);
            MesajlasmaWindow w = new MesajlasmaWindow();
            w.DataContext = v;
            w.Show();
        }

        private bool canYeniSiparis()
        {
            return SiparisKayitModu != KayitModu.Yeni;
        }

        private bool canSiparisKopyala()
        {
            return SiparisKayitModu != KayitModu.Yeni;
        }

        private void SiparisKopyala(KayitModu kayitModu)
        {
            SiparisKayitModu = kayitModu;

            if (SeciliSiparis.KapasitifMi == true && kayitModu!=KayitModu.KapasitiftenYeni)
            {
                var kopya = SeciliSiparis.Copy();

                SeciliSiparis = JsonConvert.DeserializeObject<Siparis>(JsonConvert.SerializeObject((object)SeciliSiparis).ToString());
                SeciliSiparis.KapasitifMi = true;

                SeciliSiparis.SiparisKod = "<Yeni Kapasitif Kopyası>";

                SeciliSiparis.PaketlenenMiktarKg = 0;

                SeciliSiparis.SiparisTarih = DateTime.Now.Date;
                SeciliSiparis.RowGuid = Guid.NewGuid();
                SeciliSiparis.SiparisSurecDurum = SiparisSurecDurum.SATISTA;
                SeciliSiparis.CreatedUserId = PandapGlobal.AktifKullanici.KullaniciId;
                SeciliSiparis.RowGuid = Guid.NewGuid();

                SeciliSiparis.SiparisKalemleri.ToList().ForEach(c =>
                {
                    c.SiparisNav = seciliSiparis;

                    c.SiparisKalemKod = null;
                    c.RowGuid = Guid.NewGuid();
                    c.OPER_NetsisBelgeNo = null;
                    c.PLAN_KalemKapatildiMi = false;
                    c.PLAN_UretimdekiMiktarToplam = 0;
                    c.PLAN_PlanlananMiktarToplam = 0;
                    c.PLAN_KalemKapatildiMi = false;
                    c.PLAN_KalemKapatilmaTarihi = null;
                    c.PLAN_PlanlanacakKalanMiktarToplam = 0;

                    c.RowGuid = Guid.NewGuid();

                });
            }
            else
            {
                var ilgiliKapasitifSiparisNo = SeciliSiparis.KapasitifMi.Value ? SeciliSiparis.SiparisKod : null;

                var kopya = SeciliSiparis.Copy();

                SeciliSiparis = JsonConvert.DeserializeObject<Siparis>(JsonConvert.SerializeObject((object)SeciliSiparis).ToString());
                SeciliSiparis.IlgiliKapasitifSiparisKod = ilgiliKapasitifSiparisNo;
                SeciliSiparis.KapasitifMi = false;

                SeciliSiparis.SiparisKod = "<Yeni Sipariş Kopyası>";

                SeciliSiparis.PaketlenenMiktarKg = 0;

                SeciliSiparis.SiparisTarih = DateTime.Now.Date;
                SeciliSiparis.RowGuid = Guid.NewGuid();
                SeciliSiparis.SiparisSurecDurum = SiparisSurecDurum.SATISTA;
                SeciliSiparis.CreatedUserId = PandapGlobal.AktifKullanici.KullaniciId;


                SeciliSiparis.SiparisKalemleri.ToList().ForEach(c =>
                {
                    c.SiparisNav = seciliSiparis;

                    c.SiparisKalemKod = null;
                    c.RowGuid = Guid.NewGuid();
                    c.OPER_NetsisBelgeNo = null;
                    c.PLAN_KalemKapatildiMi = false;
                    c.PLAN_UretimdekiMiktarToplam = 0;
                    c.PLAN_PlanlananMiktarToplam = 0;
                    c.PLAN_KalemKapatildiMi = false;
                    c.PLAN_KalemKapatilmaTarihi = null;
                    c.PLAN_PlanlanacakKalanMiktarToplam = 0;

                });

                if (SeciliSiparis.IlgiliKapasitifSiparisKod != null) SeciliSiparis.SiparisKalemleri.Clear();
            }
        }

        private void SipariYeni()
        {
            if (uow.DegisiklikVarMi())
            {
                MessageBoxService.ShowMessage("Kayıttda değişiklikleriniz var. Önce kayıt işlemini gerçekleştiriniz");
            }

            CariRiskDurum = null;

            SiparisKayitModu = KayitModu.Yeni;
            SeciliSiparis = Siparis.YeniSiparisOlustur();

            SeciliSiparis.SiparisKod = "<Yeni Sipariş>";
            SeciliSiparis.SiparisSurecDurum = SiparisSurecDurum.SATISTA;
            SeciliSiparis.CreatedUserId = PandapGlobal.AktifKullanici.KullaniciId;
        }

        private bool canKalemEdit() => kalemVM?.KayitModu == KayitModu.Varsayılan || kalemVM == null;

        private bool canKalemSil() => canKalemEdit();

        private bool canYeniKalem() => canKalemEdit();

        private bool canKopyadanYeniKalem() => canKalemEdit();

        private bool canKalemKapat() => !canKalemEdit();

        private bool canVazgec() => !canKalemEdit();

        private bool canKalemKaydet() => !canKalemEdit();

        private bool canNestiseAktar()
        {
            var yetkiliRol = PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.OPERASYON
                    ||
                   PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.ADMIN;

            return SeciliKalemSayisi > 0 && yetkiliRol && SeciliSiparis.SiparisSurecDurum == SiparisSurecDurum.MUSTERIONAYLI;
        }

        private CariRiskKontrol cariRiskDurum;

        public CariRiskKontrol CariRiskDurum
        {
            get
            {
                return cariRiskDurum;
            }
            set
            {
                SetProperty(ref cariRiskDurum, value);
            }
        }

        private async void NetsiseAktar()
        {
            var kalemToplam = SeciliSiparis.SiparisKalemleri
                .Where(c => c.SatirSecildiMi && c.OPER_NetsisBelgeNo != null)
                .Sum(c => c.GenelToplamTutar.Value);

            //var x = cariRiskDurum.DovizTuru;

            //if (kalemToplam > CariRiskDurum.Toplam * (-1))
            //{
            //    MessageBox.Show("Risk Limiti Aşıldı." + Environment.NewLine + "Aşım Miktarı :" + (kalemToplam - CariRiskDurum.Toplam.Value * (-1)).ToString("n0"));
            //    return;
            //}

            SplashScreenHelper.Instance.ShowLoadingScreen();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(PandapGlobal.ApiPath);

            var reqDTO = new Panda2017_NetsisDTO
            {
                SiparisKod = SeciliSiparis.SiparisKod,
                KullaniciId = PandapGlobal.AktifKullanici.KullaniciId,
                KalemId_Liste = SeciliSiparis.SiparisKalemleri
                                .Where(c => c.SatirSecildiMi)
                                .Select(c => c.SiparisKalemKod).ToList()
            };

            var response = await client.PostAsync("/api/netsis",
                        new StringContent(JsonConvert.SerializeObject(reqDTO).ToString(), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                SplashScreenHelper.Instance.HideSplashScreen();
                var sonuc = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

                MessageBoxService.ShowMessage(sonuc, "Netsis Kayıt", MessageButton.OK, MessageIcon.Information);

                this.Load(this.SeciliSiparis.SiparisKod);

                OnPropertyChanged("SeciliSiparis");
            }
            else
            {
                MessageBoxService.ShowMessage("Hata Oluştu", response.StatusCode.ToString(), MessageButton.OK, MessageIcon.Information);
            }

            SplashScreenHelper.Instance.HideSplashScreen();
        }

        private void KalemVazgec()
        {
            SeciliKalem = JsonConvert.DeserializeObject<SiparisKalem>(JsonConvert.SerializeObject((object)KalemOrjinal).ToString());

            kalemVM.KayitModu = KayitModu.Varsayılan;
        }

        private void KopyadanYeniKalem()
        {
            KalemVM.KayitModu = KayitModu.KopyadanYeni;

            KalemVM.SiparisKalem = JsonConvert.DeserializeObject<SiparisKalem>(JsonConvert.SerializeObject((object)SeciliKalem).ToString());
            KalemVM.SiparisKalem.SiparisKalemKod = null;
            KalemVM.SiparisKalem.SiparisNav = SeciliSiparis;

            KalemVM.SiparisKalem.RowGuid = Guid.NewGuid();
        }

        private void KalemSil()
        {
            SeciliSiparis.SiparisKalemleri.Remove(SeciliKalem);
        }

        public SiparisKalem KalemOrjinal { get; set; }

        private void KalemEdit()
        {
            KalemOrjinal = PandapObjectHelper.CopyObject(SeciliKalem);

            kalemVM.KayitModu = KayitModu.Edit;
            KalemVM.SiparisKalem = SeciliKalem;
        }

        public void KalemLookUpYukle(KalemViewModel vmKalem)
        {
            vmKalem.LmeTipleri = uow.SiparisKalemRepo.LmeTipleriGetir();
            vmKalem.DovizTipleri = uow.SiparisKalemRepo.DovizTipleriGetir();
            vmKalem.KulceTipleri = uow.SiparisKalemRepo.KulcePrimTipleriGetir();
            vmKalem.BirimTipleri = uow.SiparisKalemRepo.BirimTipleriGetir();
            vmKalem.Alasimlar = uow.SiparisKalemRepo.AlasimTipleriGetir();
            vmKalem.MasuraTipleri = uow.SiparisKalemRepo.MasuraTipleriGetir();
            vmKalem.YuzeyTipleri = uow.SiparisKalemRepo.YuzeyTipleriGetir();
            vmKalem.SertlikTipleri = uow.SiparisKalemRepo.SertlikTipleriGetir();
            vmKalem.KullanimAlanlari = uow.SiparisKalemRepo.KullanimAlanTipleriGetir();
            vmKalem.AmbalajTipleri = uow.SiparisKalemRepo.AmbalajTipleriGetir();
            vmKalem.GumrukTipleri = uow.SiparisKalemRepo.GumrukTipleriGetir();
            vmKalem.Urunler = uow.SiparisKalemRepo.UrunleriGetir();
        }

        private void YeniKalem()
        {
            SeciliKalem = null;
            KalemVM.KayitModu = KayitModu.Yeni;

            if (SeciliSiparis.IlgiliKapasitifSiparisKod != null)
            {
                var _kalem = uow.SiparisKalemRepo.SipariseAitIlkKalemiGetir(SeciliSiparis.IlgiliKapasitifSiparisKod);
                _kalem.RowGuid = Guid.NewGuid();
                _kalem.SiparisKalemKod = null;

                KalemVM.SiparisKalem = _kalem;
            }
            else
            {
                KalemVM.SiparisKalem = SiparisKalem.YeniSiparisKalemOluştur(SeciliSiparis);
            }

            KalemVM.SiparisKalem.SiparisNav = SeciliSiparis;

            kalemVM.IsVisible = true;

            KalemLookUpYukle(kalemVM);
        }

        private void KalemKaydet()
        {
            var isValid = kalemVM.SiparisKalem.IsValidModel();
            var hatalar = kalemVM.SiparisKalem.Hatalar;

            if (isValid == false) return;

            if (kalemVM.KayitModu == KayitModu.Yeni || kalemVM.KayitModu == KayitModu.KopyadanYeni)
            {
                SeciliSiparis.SiparisKalemleri.Add(kalemVM.SiparisKalem);

                if (SeciliSiparis.IlgiliKapasitifSiparisKod != null)
                {
                    var kopyaKalem = PandapObjectHelper.CopyObject(kalemVM.SiparisKalem);

                    uow.SiparisKalemRepo.SipariseDirekEksiMiktarKalemEkle
                        (SeciliSiparis.IlgiliKapasitifSiparisKod, kopyaKalem);
                }
            }

            kalemVM.KayitModu = KayitModu.Varsayılan;
        }

        private void SiparisTeyitFormuYazdir(Language lang)
        {
            if (SiparisKayitModu == KayitModu.KopyadanYeni || SiparisKayitModu == KayitModu.Yeni)
            {
                var sonuc = MessageBoxService.ShowMessage("Önce Siparişi Kaydediniz...", "Pandap", MessageButton.OK,
                    MessageIcon.Warning);

                return;
            }

            var reportPath = lang == Language.TR ? @"Content\SiparisTeyitFormuTr.repx" : @"Content\SiparisTeyitFormuEN.repx";

            var fs = new FileStream(reportPath, FileMode.Open);
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);

            XtraReport xr = XtraReport.FromStream(ms, true);

            var ds = xr.DataSource as ObjectDataSource;

            if (ds != null)
            {
                var uow_form = new UnitOfWork();

                var fullIncludeSiparisData = uow_form.SiparisRepo.SiparisGetirFull(SeciliSiparis.SiparisKod);

                if (fullIncludeSiparisData == null)
                {
                    MessageBox.Show("Teyit Formu İçin Gerekli Bilgiler Eksik");
                    return;
                }

                var model = TeyitFormService.TeyitFormDtoOlustur(fullIncludeSiparisData, lang);

                var l = new List<SiparisTeyitDTO>();
                l.Add(model);

                ds.DataSource = l;
            }
            else
                MessageBox.Show("Data Source Tanımlı değil");

            PandapRaporSimpleViever view = new PandapRaporSimpleViever();
            view.report_view_control.DocumentSource = xr;
            view.report_view_control.ZoomFactor = 0.85;
            view.Width = 990; view.Height = 780;

            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            xr.CreateDocument();

            view.ShowDialog();

            fs.Close();
        }

        private void TeyitFormDesignerGoster(Language lang)
        {
            RaporDesigner1 reportDesignWindow = new RaporDesigner1();

            reportDesignWindow.designer.DocumentOpened += (s, e) =>
            {
                var v = e.Document;

                var ds = v.ReportModel.DataSource as ObjectDataSource;

                if (ds != null)
                {
                    var uow_form = new UnitOfWork();

                    var fullIncludeSiparisData = uow_form.SiparisRepo.SiparisGetirFull(SeciliSiparis.SiparisKod);

                    if (fullIncludeSiparisData == null)
                    {
                        MessageBox.Show("Teyit Formu İçin Gerekli Bilgiler Eksik");
                        return;
                    }

                    var teyitFormDto = TeyitFormService.TeyitFormDtoOlustur(fullIncludeSiparisData, lang);

                    var teyitFormDtoListe = new List<SiparisTeyitDTO>();
                    teyitFormDtoListe.Add(teyitFormDto);

                    ds.DataSource = teyitFormDtoListe;
                }
                else
                    MessageBox.Show("Data Source Tanımı yapınız...");
            };

            reportDesignWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            reportDesignWindow.Show();
        }

        private  void SiparisKaydet()
        {
            var valid = SeciliSiparis.IsValidModel();
            var hatalar = SeciliSiparis.Hatalar;

            if (!valid)
            {
                MessageBoxService.ShowMessage("Bazı alanlarda sorunlar var kontrol ediniz", "Siparis Kayıt", MessageButton.OK, MessageIcon.Stop);
                return;
            }

            if (kalemVM?.IsVisible == true)
            {
                MessageBoxService.ShowMessage("Kalem ile ilgili işleminizi tamamlayınız", "Siparis Kayıt", MessageButton.OK, MessageIcon.Stop);
                return;
            }

            if (SeciliSiparis.SiparisKalemleri.Count == 0)
            {
                MessageBoxService.ShowMessage("Kalem ekleyiniz", "Siparis Kayıt", MessageButton.OK, MessageIcon.Stop);
                return;
            }

            try
            {
                // geçici olarak manual dosya no

                SeciliSiparis.SiparisKod = SiparisKayitModu != KayitModu.Edit ? uow.SiparisRepo.YeniBelgeNoGetirYildan()
                                                                              : SeciliSiparis.SiparisKod;


                var maxKalemKod_satir_value = SeciliSiparis.SiparisKalemleri
                                  .OrderByDescending(c => c.SiparisKalemKod)
                                  .Select(c => c.SiparisKalemKod)
                                  .FirstOrDefault();

                var maxKalemNumber = maxKalemKod_satir_value != null
                                ? int.Parse(maxKalemKod_satir_value.Substring(maxKalemKod_satir_value.Length - 2)) : 0;

                var eklenenKalemler = SeciliSiparis.SiparisKalemleri.Where(c => c.SiparisKalemKod == null);

                foreach (var sipKalem in eklenenKalemler)
                {
                    maxKalemNumber += 1;
                    sipKalem.SiparisKalemKod = SeciliSiparis.SiparisKod + "/" + maxKalemNumber.ToString().PadLeft(2, '0');
                }

                if (SiparisKayitModu != KayitModu.Edit) uow.SiparisRepo.EkleAsync(SeciliSiparis);

                SeciliSiparis.TemsilciAdSoyad = SeciliSiparis.CariKartNavigation.PlasiyerAd;

               uow.Commit();

                MessageBoxService.ShowMessage("Sipariş Kaydedildi", "Siparis Kayıt", MessageButton.OK, MessageIcon.Information);

                Messenger.Default.Send<Siparis>(SeciliSiparis);

                SiparisLogla();
                SiparisKayitModu = KayitModu.Edit;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBoxService.ShowMessage(errorMessage, "Siparis Kayıt", MessageButton.OK, MessageIcon.Stop);
            }
        }

        private void SiparisLogla()
        {
            EntityLog log = new EntityLog
            {
                EntityRowGuid = SeciliSiparis.RowGuid.Value,
                EntityJsonStream = JsonConvert.SerializeObject(SeciliSiparis),
                KullaniciAdSoyad = PandapGlobal.AktifKullanici.KullaniciId,
                KayitTarihi = DateTime.Now
            };

            if (EntityLogs == null) EntityLogs = new ObservableCollection<EntityLog>();

            EntityLogs.Add(log);
            uow.EntiyLogRepo.Add(log);
        }

        private void FormClosing(CancelEventArgs e)
        {
            if (uow.DegisiklikVarMi())
            {
                var sonuc = MessageBoxService.ShowMessage(SeciliSiparis.SiparisKod + " içinde yaptığınız değişiklikleri kaydetmek istiyormusunuz?",
                    "Pandap", MessageButton.YesNoCancel, MessageIcon.Warning);

                if (sonuc == MessageResult.Cancel) { e.Cancel = true; return; };

                if (sonuc == MessageResult.Yes) { this.SiparisKaydet(); e.Cancel = false; };

                if (sonuc == MessageResult.No) { e.Cancel = false; };
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}