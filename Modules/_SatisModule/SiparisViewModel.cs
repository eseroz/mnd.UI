using AutoMapper;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using mnd.Common.Helpers;
using mnd.Logic.BC_Satis._Teklif;
using mnd.Logic.Model._Ref;
using mnd.Logic.Model.App;
using mnd.Logic.Model.Netsis;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.Logic.Services;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules.TeklifModule.Models;
using mnd.UI.Modules.TeklifModule.Services;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using mnd.UI.Modules.TeklifModule;
using mnd.Logic.Model.Stok;

namespace mnd.UI.Modules._SatisModule
{
    public class SiparisViewModel : MyDxViewModelBase
    {
        public bool _isFormLoaded;

        private UnitOfWork uow = new UnitOfWork();

        private UnitOfWork uowPalet = new UnitOfWork();

        private UnitOfWork uowLme = new UnitOfWork();

        public KayitModu SiparisKayitModu { get; set; }
        public KayitModu KalemKayitModu { get; set; }

        public DelegateCommand SiparisYeniCommand => new DelegateCommand(SiparisYeni, SiparisKayitModu != KayitModu.Add);

        public string KalemEklePanel { get => kalemEklePanel; set => SetProperty(ref kalemEklePanel, value); }
        public DelegateCommand<string> SiparisEditCommand => new DelegateCommand<string>(SiparisEdit, true);
        //public DelegateCommand SiparisKopyalaCommand => new DelegateCommand(SiparisKopyala, () => SiparisKayitModu != KayitModu.Add);
        public DelegateCommand<bool> SiparisKaydetCommand => new DelegateCommand<bool>(SiparisKaydet, canSiparisKaydet);

        public DelegateCommand VerileriTazeleCommand => new DelegateCommand(VerileriTazele);

        public DelegateCommand KalemYeniCommand => new DelegateCommand(YeniKalem, true);
        //public DelegateCommand KalemKopyalaCommand => new DelegateCommand(KopyadanYeniKalem, canKopyadanYeniKalem);

        //public DelegateCommand KalemEditCommand => new DelegateCommand(KalemEdit, canKalemEdit);
        public DelegateCommand KalemKaydetCommand => new DelegateCommand(KalemEkleYadaGuncelle, CanKalemEkleYadaGuncelle);
        public DelegateCommand KalemVazgecCommand => new DelegateCommand(KalemVazgec, canVazgec);
        //public DelegateCommand KalemSilCommand => new DelegateCommand(KalemSil, canKalemSil);

        private void SiparisEdit(string siparisKod)
        {
            UnitOfWork uow2 = new UnitOfWork();

            siparisKod = siparisKod.Split('/')[0];


            var vm = new SiparisViewModel(false);

            var title = "";

            vm.SiparisKayitModu = KayitModu.Edit;

            var sip = uow2.SiparisRepo.SiparisGetir(siparisKod);

            title = siparisKod;

            vm.Load(sip);


            var doc = AppPandap.pDocumentManagerService.CreateDocument("SiparisView", vm);
            doc.Title = title;
            doc.Show();
        }

        private void VerileriTazele()
        {
            Load(SeciliSiparis);
        }

        public bool YurtDisiSatisMi => SeciliSiparis.SatisTipKod == "YD";

        public DelegateCommand<Language> TeyitFormYazdirTrCommand => new DelegateCommand<Language>(x => SiparisTeyitFormuYazdir(Language.TR));
        public DelegateCommand<Language> TeyitFormYazdirEnCommand => new DelegateCommand<Language>(x => SiparisTeyitFormuYazdir(Language.EN));

        public DelegateCommand StokDetayGosterCommand { get; set; }
        public DelegateCommand ParametreleriGuncelleCommand { get; set; }

        public DelegateCommand SiparisMesajYazCommand => new DelegateCommand(SiparisMesajYaz);

        public DelegateCommand LmeMesajYazCommand => new DelegateCommand(LmeMesajYaz);

        private void LmeMesajYaz()
        {
            StringBuilder strBuilder = new StringBuilder(SeciliSiparis.RowGuid.Value.ToString());
            strBuilder[0] = '1';
            strBuilder[1] = '1';

            Guid lmeGuid = Guid.Parse(strBuilder.ToString());

            var uowLme = new UnitOfWork();
            var row = uow.SiparisRepo.LmeBaglamaGetir(lmeGuid);

            AppMesaj.MesajFormAc(row);


        }

        public bool TekliftenMi { get; set; }

        public DelegateCommand<object> SiparisKalemMesajYazCommand => new DelegateCommand(SiparisKalemMesajYaz);

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

                        IsEditableForm = true;
                        return;
                    }

                    var jsonString = uow.EntiyLogRepo.GetEntityLogsFromGuidJsonStream(seciliEntityLog.Id);

                    SeciliSiparis = JsonConvert.DeserializeObject<Siparis>(jsonString);
                    SiparisKayitModu = KayitModu.Edit;
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

        //public TeklifDataService TeklifService { get => teklifService; set => SetProperty(ref teklifService, value); }
        //public TeklifEditModel SeciliTeklif { get => seciliTeklif; set => SetProperty(ref seciliTeklif, value); }
        public SiparisViewModel(bool tekliftenMi)
        {
            TekliftenMi = tekliftenMi;
               //if(_seciliTeklif !=null) SeciliTeklif = _seciliTeklif;
               //if(_teklifService!=null) TeklifService = _teklifService;

               _isFormLoaded = false;
            IsEditableForm = true;
        }

        private void FormLoaded()
        {
            _isFormLoaded = true;
        }


        public ObservableCollection<TBLIHRSTK> Urunler { get => urunler; set { SetProperty(ref urunler, value); } }
        public void Load(Siparis siparis)
        {

            uow = new UnitOfWork(); // reload işleminden kurtulmak için başka yerden de çağrılıyor çünkü

            var uowLookUp = new UnitOfWork();

            Musteriler = uowLookUp.SiparisLookUpRepo.CariKartlariGetir(AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';'));


            OdemeTipleri = uowLookUp.SiparisLookUpRepo.OdemeTipleriGetir();
            SatisTipleri = uowLookUp.SiparisLookUpRepo.SatisTipleriGetir();
            DovizTipleri = uowLookUp.SiparisLookUpRepo.DovizTipleriGetir();
            TeslimTipleri = uowLookUp.SiparisLookUpRepo.TeslimTipleriGetir();


            if (siparis.SiparisKod == null)
            {
                SiparisKayitModu = KayitModu.Add;
                SeciliSiparis = siparis;
            }
            else
            {
                SiparisKayitModu = KayitModu.Edit;
                SeciliSiparis = uow.SiparisRepo.SiparisGetirPure(siparis.SiparisKod);
                this.EntityLogs = uow.EntiyLogRepo.GetEntityLogsFromGuidWithoutJsonStream(SeciliSiparis.RowGuid.Value);
                this.EntityLogs.Insert(0, new EntityLog { Id = 0, KullaniciAdSoyad = "Son Hali" });
                SeciliSiparis.SiparisKalemleri.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
            }

            SeciliSiparis.PropertyChanged += SeciliSiparis_PropertyChanged;


        }

        private ObservableCollection<Donem> DonemDoldur()
        {
            ObservableCollection<Donem> DonemListesi = new ObservableCollection<Donem>();
            DonemListesi.Add(new Donem { DonemAdi = "1. Çeyrek", DonemGrupAdi = "Çeyreklik" });
            DonemListesi.Add(new Donem { DonemAdi = "2. Çeyrek", DonemGrupAdi = "Çeyreklik" });
            DonemListesi.Add(new Donem { DonemAdi = "3. Çeyrek", DonemGrupAdi = "Çeyreklik" });
            DonemListesi.Add(new Donem { DonemAdi = "4. Çeyrek", DonemGrupAdi = "Çeyreklik" });

            DonemListesi.Add(new Donem { DonemAdi = "Ocak", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Şubat", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Mart", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Nisan", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Mayıs", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Haziran", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Temmuz", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Ağustos", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Eylül", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Ekim", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Kasım", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Aralık", DonemGrupAdi = "Aylık" });

            for (int i = 1; i < 53; i++)
            {
                DonemListesi.Add(new Donem { DonemAdi = i + ". Hafta", DonemGrupAdi = "Spot" });
            }

            return DonemListesi;

        }

        public CariKart SeciliMusteri
        {
            get => _seciliMusteri;
            set
            {
                SetProperty(ref _seciliMusteri, value);
                CariDegisti();
            }
        }

        public void CariDegisti()
        {
            if (SeciliMusteri == null || _isFormLoaded != true) return;

            SeciliSiparis.SatisTipKod = SeciliMusteri.UlkeKod == "TR" ? SATISTIPI.YI : SATISTIPI.YD;

            TeslimTipleri = SeciliMusteri.UlkeKod == "TR" ? uow.SiparisLookUpRepo.TeslimTipleriGetir(SATISTIPI.YI)
                                                          : uow.SiparisLookUpRepo.TeslimTipleriGetir(SATISTIPI.YD);

            SeciliSiparis.TemsilciAdSoyad = SeciliMusteri.PlasiyerAd;

            SeciliSiparis.FaturaDovizTipKod = SeciliMusteri.DovizAd;

            SeciliSiparis.TakipDovizTipKod = SeciliMusteri.UlkeKod == "TR" ? "" : SeciliMusteri.DovizAd;

            SeciliSiparis.FaturaAdresi = SeciliMusteri.CariAdres;

            var kisi = uow.NetsisRepo.CariIlgiliKisiGetir(SeciliSiparis.CariKod);

            SeciliSiparis.IlgiliNot = $"{kisi?.YetkiliKisi ?? " "} / {kisi?.Email ?? " "} / {kisi?.Tel ?? " "}";

        }

        private void SeciliSiparis_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!_isFormLoaded) return; // form yüklü değilse işlem yapma

        }

        public KalemViewModel KalemVM
        {
            get => kalemVM;
            set => SetProperty(ref kalemVM, value);
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

        }
    

        //public int SeciliKalemSayisi => SeciliSiparis.SeciliKalemSayisiniGetir();

        private Siparis seciliSiparis;

        public Siparis SeciliSiparis
        {
            get => seciliSiparis;
            set
            {

                SetProperty(ref seciliSiparis, value);

            }
        }


        public Siparis IlgiliKapasitif { get; set; }


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

        public ObservableCollection<LmeBaglama> LmeBaglamaListe { get => _lmeBaglamaListe; set => SetProperty(ref _lmeBaglamaListe, value); }

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

        private bool canSiparisKaydet(bool tekliftenMi)
        {
            return (SeciliSiparis.SiparisSurecDurum == SIPARISSURECDURUM.SATISTA
                    || SeciliSiparis.SiparisSurecDurum == SIPARISSURECDURUM.YENIKAYIT
                    || SeciliSiparis.SiparisSurecDurum == SIPARISSURECDURUM.MUSTERIONAYLI
                    )
                && (AppPandap.KullaniciRol == KULLANICIROLLERI.SATIS ||
                    AppPandap.KullaniciRol == KULLANICIROLLERI.SATIS_DESTEK ||
                 AppPandap.KullaniciRol == KULLANICIROLLERI.SATISYONETICI_BOLGE ||
                 AppPandap.KullaniciRol == KULLANICIROLLERI.ADMIN

                ); ;

        }

        private bool canMesajYaz()
        {
            return (SeciliSiparis != null && SeciliSiparis.RowGuid != null);
        }

        private void SiparisMesajYaz()
        {
            var rows = new List<Siparis> { SeciliSiparis };
            rows.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            AppMesaj.MesajFormAc(SeciliSiparis);
        }

        private void SiparisKalemMesajYaz()
        {
            AppMesaj.MesajFormAc(SeciliKalem);
        }

        private void SiparisYeni()
        {
            var vm = new SiparisViewModel(false);
            vm.SiparisKayitModu = KayitModu.Add;

            var siparis = Siparis.SiparisOlustur(AppPandap.AktifKullanici.KullaniciId, null, null);

            var title = "Yeni Sipariş";
            vm.Load(siparis);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SiparisView", vm);
            doc.Title = title;
            doc.Show();
        }

        private bool canKalemKapat() => kalemVM != null;

        private bool canVazgec() => kalemVM != null;

        private bool CanKalemEkleYadaGuncelle() => kalemVM != null;


        private CariKart _seciliMusteri;
        private ObservableCollection<LmeBaglama> _lmeBaglamaListe;
        private string kalemEklePanel;
        private ObservableCollection<TBLIHRSTK> urunler;


        private void YeniKalem()
        {

            KalemEklePanel = "Visible";
            KalemVM = new KalemViewModel();
            KalemVM.KayitModu = KayitModu.Add;

            KalemVM.TempKalem = SeciliSiparis.KalemOluştur();
            //SeciliKalem = kalemVM.TempKalem;

            kalemVM.TempKalem.PropertyChanged += TempKalem_PropertyChanged;
       
            KalemLookUpYukle(kalemVM);
  
        }

        private void TempKalem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(SiparisKalem.DonemGrup))
            {
                kalemVM.DonemListesi = DonemDoldur();
                kalemVM.DonemListesi = new ObservableCollection<Donem>(kalemVM.DonemListesi.Where(p => p.DonemGrupAdi == kalemVM.TempKalem.DonemGrup).ToList());
            }
        }

        private void KopyadanYeniKalem()
        {
            KalemVM = new KalemViewModel();
            KalemVM.DovizTipKod = DovizTipleri.First(c => c.DovizTipKod == SeciliSiparis.TakipDovizTipKod).Simge;

            KalemLookUpYukle(KalemVM);

            //KalemVM.TempKalem = SeciliSiparis.KopyadanYeniKalem(SeciliKalem);
            kalemVM.KayitModu = KayitModu.Add;


            KalemVM.TempKalem.RowGuid = Guid.NewGuid();

        }

        private void KalemEdit()
        {
            var tempKalem = Mapper.Map<SiparisKalem>(SeciliKalem);

            KalemVM = new KalemViewModel(tempKalem);

            KalemLookUpYukle(KalemVM);

            KalemVM.DovizTipKod = DovizTipleri.First(c => c.DovizTipKod == SeciliSiparis.TakipDovizTipKod).Simge;
            KalemVM.KayitModu = KayitModu.Edit;

        }

        private void KalemEkleYadaGuncelle()
        {
            var isValid = kalemVM.TempKalem.IsValidModel();
            var hatalar = kalemVM.TempKalem.Hatalar;

            if (isValid == false) return;


            if (kalemVM.KayitModu == KayitModu.Add)
            {
                var yeniKalem = Mapper.Map<SiparisKalem>(KalemVM.TempKalem);
                SeciliSiparis.KalemEkle(yeniKalem);       
            }
            else if (kalemVM.KayitModu == KayitModu.Edit)
            {
                Mapper.Map(KalemVM.TempKalem, SeciliKalem);
            }

            //TODO tempkalem null yapılmazsa güncelleme sonrası ambalajtip null oluyor sebebi çözülemedi
            KalemVM.KayitModu = KayitModu.Default;
            KalemVM.TempKalem = null;
            KalemVM = null;
        }

        private void IlgiliKapasitifeEksiKalemMiktarEkle(SiparisKalem yeniKalem)
        {
            var yeniKapasitifKalem = Mapper.Map<SiparisKalem>(yeniKalem);
            //yeniKapasitifKalem.KapasitifMi = true;
            //yeniKapasitifKalem.Miktar_kg = yeniKalem.Miktar_kg * -1;
            //yeniKapasitifKalem.IlgiliKalemKod = yeniKalem.RowGuid.ToString(); // geçici key
            yeniKapasitifKalem.RowGuid = Guid.NewGuid();
            //yeniKapasitifKalem.LmeBaglamaKod = null;
            //IlgiliKapasitif.KalemEkle(yeniKapasitifKalem);
        }

        private void KalemVazgec()
        {
            KalemVM.TempKalem = null;
            KalemVM = null;
        }

        private void KalemSil()
        {
            //if (KapasitiftenSiparisMi)
            //{
            //    var anaSiparisKalem = IlgiliKapasitif.SiparisKalemleri.First().Miktar_kg;
            //    SiparisKalem silinecekKalem;

            //    if (SeciliKalem.SiparisKalemKod == null)
            //        silinecekKalem = IlgiliKapasitif.SiparisKalemleri.FirstOrDefault(c => c.IlgiliKalemKod == SeciliKalem.RowGuid.ToString());
            //    else
            //        silinecekKalem = IlgiliKapasitif.SiparisKalemleri.FirstOrDefault(c => c.IlgiliKalemKod == SeciliKalem.SiparisKalemKod);

            //    if (silinecekKalem != null) IlgiliKapasitif.KalemSil(silinecekKalem);
            //}

            //SeciliSiparis.KalemSil(SeciliKalem);

        }

        public void KalemLookUpYukle(KalemViewModel vmKalem)
        {
            var uow2 = new UnitOfWork();

            //vmKalem.LmeTipleri = uow2.SiparisKalemRepo.LmeTipleriGetir();
            vmKalem.DovizTipleri = uow2.SiparisKalemRepo.DovizTipleriGetir();
            //vmKalem.KulceTipleri = uow2.SiparisKalemRepo.KulcePrimTipleriGetir();
            vmKalem.BirimTipleri = uow2.SiparisKalemRepo.BirimTipleriGetir();

            vmKalem.Urunler = new ObservableCollection<TBLIHRSTK>(LookupTables.Default.Urunler);
            //vmKalem.Urunler = uow2.SiparisKalemRepo.UrunleriGetir();
            //vmKalem.SertlikTipleri = uow2.SiparisKalemRepo.SertlikTipleriGetir();
            vmKalem.AmbalajTipleri = uow2.SiparisKalemRepo.AmbalajTipleriGetir();


            vmKalem.NakliyeDurumTipleri = new ObservableCollection<NakliyeDurumTip>();
            vmKalem.NakliyeDurumTipleri.Add(new NakliyeDurumTip { NakliyeDurumTipAdi = "Dahil" });
            vmKalem.NakliyeDurumTipleri.Add(new NakliyeDurumTip { NakliyeDurumTipAdi = "Hariç" });
            vmKalem.DonemGrupListesi = new ObservableCollection<DonemGrup>();

            vmKalem.DonemGrupListesi.Add(new DonemGrup { DonemGrupAdi = "Çeyreklik" });
            vmKalem.DonemGrupListesi.Add(new DonemGrup { DonemGrupAdi = "Aylık" });
            vmKalem.DonemGrupListesi.Add(new DonemGrup { DonemGrupAdi = "Yıllık" });
            vmKalem.DonemGrupListesi.Add(new DonemGrup { DonemGrupAdi = "Spot" });

            vmKalem.DonemListesi = DonemDoldur();

            uow2.Dispose();
            uow2 = null;
        }

        private void SiparisTeyitFormuYazdir(Language lang)
        {
            if (uow.DegisiklikVarMi())
            {
                var sonuc = MessageBoxService.ShowMessage("Önce Siparişi Kaydediniz...", "MNDApp", MessageButton.OK,
                    MessageIcon.Warning);
                return;
            }

            var fullIncludeSiparisData = uow.SiparisRepo.SiparisGetirFull(SeciliSiparis.SiparisKod);

            if (fullIncludeSiparisData == null)
            {
                MessageBox.Show("Teyit Formu İçin Gerekli Bilgiler Eksik");
                return;
            }

            int raporId = lang == Language.EN ? 5 : 6;
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(raporId);

            var model = TeyitFormService.TeyitFormDtoOlustur(fullIncludeSiparisData, lang);

            var l = new List<SiparisTeyitDTO>();


            var duzenleyen = SeciliSiparis.LastEditedBy ?? SeciliSiparis.CreatedUserId;
            var kul = KullaniciService.KullaniciGetirFromKullanici(duzenleyen);

            if (kul != null)
                model.Duzenleyen = kul.AdSoyad;
            else
                model.Duzenleyen = "";


            l.Add(model);

            var dsObject = l;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        public object TeyitFormRaporDataGetir(Language lang)
        {
            var uow_form = new UnitOfWork();

            var fullIncludeSiparisData = uow_form.SiparisRepo.SiparisGetirFull(SeciliSiparis.SiparisKod);

            if (fullIncludeSiparisData == null)
            {
                MessageBox.Show("Teyit Formu İçin Gerekli Bilgiler Eksik");
                return null;
            }

            var teyitFormDto = TeyitFormService.TeyitFormDtoOlustur(fullIncludeSiparisData, lang);

            var teyitFormDtoListe = new List<SiparisTeyitDTO>();
            teyitFormDtoListe.Add(teyitFormDto);

            return teyitFormDtoListe;
        }

        private void SiparisKaydet(bool tekliftenMi)
        {
            var uow_ayarlar = new UnitOfWork();

            var ayar_data = uow_ayarlar.AppRepo.UygulamaBilgiGetir();

            if (!IsValidSiparis()) return;

            try
            {
                SeciliSiparis.SiparisKod = String.IsNullOrEmpty(SeciliSiparis.SiparisKod) ? uow.SiparisRepo.YeniBelgeNoGetirYildan()
                                                                              : SeciliSiparis.SiparisKod;

                SeciliSiparis.TemsilciAdSoyad = SeciliSiparis?.CariKartNavigation?.PlasiyerAd;


                ////SeciliSiparis.KalemKodlariAta(SeciliSiparis);

                //var xxxx = seciliSiparis.SiparisKalemleri;



                if (SiparisKayitModu == KayitModu.Add)
                {
                    SeciliSiparis.CreatedUserId = AppPandap.AktifKullanici.KullaniciId;
                    SeciliSiparis.CreatedDate = DateTime.Now;

                    uow.SiparisRepo.SiparisEkle(SeciliSiparis);
                }

                SeciliSiparis.LastEditedBy = AppPandap.AktifKullanici.KullaniciId;
                SeciliSiparis.LastEditedDate = DateTime.Now;

                uow.Commit();


                if (tekliftenMi)
                {
                    TeklifRepository teklifRepo = new TeklifRepository();
                    var teklif = teklifRepo.TeklifGetir(SeciliSiparis.TeklifSiraKod);
                    teklif.TeklifDurum = "Onaylandi";
                    teklif.DonusturulenSiparisKod = SeciliSiparis.SiparisKod;
                    teklifRepo.Kaydet();
                }


                AppPandap.pDocumentManagerService.ActiveDocument.Title = SeciliSiparis.SiparisKod;

                MessageBoxService.ShowMessage("Sipariş Kaydedildi", "Siparis Kayıt", MessageButton.OK, MessageIcon.Information);

                Messenger.Default.Send<Siparis>(SeciliSiparis);

                SiparisLogla();
                SiparisKayitModu = KayitModu.Default;
            }
            catch (Exception ex)
            {
                var mesaj = ex.Message + Environment.NewLine + ex?.InnerException?.Message;

                MessageBoxService.ShowMessage(mesaj);
            }
        }




        private bool IsValidSiparis()
        {
            var uowSatis = new UnitOfWork();

            if (SeciliSiparis != null && String.IsNullOrEmpty(SeciliSiparis.FirmaSiparisNo))
            {
                var mesaj = $"Firma sipariş numarasını giriniz.";
                MessageBox.Show(mesaj, "MNDApp", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //if (SeciliSiparis != null && String.IsNullOrEmpty(SeciliSiparis.TakipDovizTipKod))
            //{
            //    var mesaj = $"LME döviz cinsini giriniz.";
            //    MessageBox.Show(mesaj, "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return false;
            //}

            //if (SeciliSiparis != null && SeciliSiparis.TakipDovizTipKod=="TL")
            //{
            //    var mesaj = $"LME döviz tipi TL olamaz";
            //    MessageBox.Show(mesaj, "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return false;
            //}



            var valid = SeciliSiparis.IsValidModel();
            var hatalar = SeciliSiparis.Hatalar;

            if (!valid)
            {
                MessageBox.Show(hatalar.Count + " Bazı alanlarda sorunlar var kontrol ediniz.", "Siparis Kayıt", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }

            if (KalemVM != null)
            {
                MessageBox.Show("Kalem ile ilgili işleminizi tamamlayınız", "Siparis Kayıt", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }

            if (SeciliSiparis.SiparisKalemleri.Count() == 0)
            {
                MessageBox.Show("Kalem ekleyiniz", "Siparis Kayıt", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }

            return true;
        }

        private void SiparisLogla()
        {
            EntityLog log = new EntityLog
            {
                EntityRowGuid = SeciliSiparis.RowGuid.Value,
                EntityJsonStream = JsonConvert.SerializeObject(SeciliSiparis),
                KullaniciAdSoyad = AppPandap.AktifKullanici.KullaniciId,
                KayitTarihi = DateTime.Now
            };

            if (EntityLogs == null) EntityLogs = new ObservableCollection<EntityLog>();

            EntityLogs.Add(log);
            uow.EntiyLogRepo.EkleKaydet(log);
        }


        public decimal SiparisKalemToplamFiyatGetir()
        {
            UnitOfWork uowLmeBul = new UnitOfWork();
            var lmeTarih = DateTime.Now.AddDays(-1).Date;

            var lmeGunlukFiyat = LmeService.LmeFiyatGetirTarihten(lmeTarih);

            if (lmeGunlukFiyat == null)
            {
                MessageBox.Show("Lme Günlük Fiyat Tanımlı Değil");
                return 0;
            }

            var x = SeciliSiparis.SiparisKalemleri;

            //var x1 =
            //   x.ToList()
            //   .Select(c => new
            //   {
            //       cariKod = SeciliSiparis.CariKod,
            //       IlgiliId = c.SiparisKalemKod,
            //       //lmeBF = c.LmeTutar.GetValueOrDefault(),
            //       //kulceBF = c.KulceTutar.GetValueOrDefault(),
            //       //iscilikBF = c.IscilikTutar.GetValueOrDefault(),
            //       kdvOran = c.KdvOran,
            //       //iscilikVadeFarkiOran = c.IscilikVadeFarkiOran.GetValueOrDefault(),
            //       //miktarKg = c.Miktar_kg.GetValueOrDefault(),
            //       dovizTipKod = SeciliSiparis.TakipDovizTipKod,

            //   })

            //   .Select(c => SiparisFiyatHesapService.FiyatSonucDTO_Getir(c.cariKod,


            //                           c.IlgiliId, c.dovizTipKod))
            //   .GroupBy(g => new { g.CariKod, g.DovizTipKod })
            //   .Select(c => new CariSiparisMiktarlari
            //   {
            //       CariKod = c.Key.CariKod,
            //       DovizTipKod = c.Key.DovizTipKod,
            //       Miktar_Kg = c.Sum(t => t.MiktarKg),
            //       GenelToplamTutar = c.Sum(t => t.GenelToplamTutar)
            //   })
            //  .ToList();

            //return x1.Sum(c => c.GenelToplamTutar);
            return 0;
        }



        private void FormClosing(CancelEventArgs e)
        {
            if (uow.DegisiklikVarMi())
            {
                var sonuc = MessageBoxService.ShowMessage(SeciliSiparis.SiparisKod + " içinde yaptığınız değişiklikleri kaydetmek istiyormusunuz?",
                    "MNDApp", MessageButton.YesNoCancel, MessageIcon.Warning);

                if (sonuc == MessageResult.Cancel) { e.Cancel = true; return; };

                if (sonuc == MessageResult.Yes) { this.SiparisKaydet(TekliftenMi); e.Cancel = false; };

                if (sonuc == MessageResult.No) { e.Cancel = false; };
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}