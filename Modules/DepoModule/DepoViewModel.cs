using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.Helper;
using mnd.Logic.Model.Satis;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.Logic.Services._DTOs;
using mnd.UI.AppModules.AppModule;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;
using mnd.UI.Modules._SatisModule;
using mnd.UI.Modules.PaketlemeModule;

namespace mnd.UI.Modules.DepoModule
{
    public class DepoRapor1_Data
    {
        public string CariAd { get; set; }
        public string UlkeAd { get; set; }

        public string UlkeKod { get; set; }
        public double DepoMiktar { get; set; }
    }


    public class DepoViewModel : MyDxViewModelBase, IForm
    {

        public virtual IDialogService SevkEmriDialogService { get { return ServiceContainer.GetService<IDialogService>("SevkEmriDialogService"); } }

        public List<MamulDepoStokDto> _sevkEdilenlerListe;

        public ObservableCollection<Palet> depodakiPaletler;

        public List<MamulDepoStokDto> mamulDepoStokDtoListe;

        private Palet _depoSeciliPalet;

        private MamulDepoStokDto seciliMamulDepoStok;

        private MamulDepoStokDto seciliSevkEdilen;

        private int seciliTabIndex;

    


        public bool SarjYapabilirmi => AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA ||
                                       AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.OPERASYON;


        private UnitOfWork uow = new UnitOfWork();
        private int _sevkSeciliKayitSayisi;
        private bool _eskisineUretimYapilsinMi;
        private string _seciliYil;
        private string _seciliAy;
        private ObservableCollection<DepoRapor1_Data> _depoRapor1;
        private ObservableCollection<string> yillar;


        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");
        public IExportService ExportService2 => ServiceContainer.GetService<IExportService>("servis2");
        public IExportService ExportService3 => ServiceContainer.GetService<IExportService>("servis3");
        public IExportService ExportService4 => ServiceContainer.GetService<IExportService>("servis4");

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand1 => 
            new DelegateCommand<object>(o => ExportService1.ExportTo(ExportType.XLSX, getExpDosyaAd("DepoGuncel")), c => YetkiliMi_FromDb(nameof(ExcelExportCommand1)));

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand2 => 
            new DelegateCommand<object>(o => ExportService2.ExportTo(ExportType.XLSX, getExpDosyaAd("DepoSevkEdilenler")), c => YetkiliMi_FromDb(nameof(ExcelExportCommand2)));

        [YetkiKontrol]
        public DelegateCommand<object> DepoRapor1ExcelExportCommand => 
            new DelegateCommand<object>(o => ExportService3.ExportTo(ExportType.XLSX, getExpDosyaAd("DepoRapor1")),  c => YetkiliMi_FromDb(nameof(DepoRapor1ExcelExportCommand)));

        [YetkiKontrol]
        public DelegateCommand<object> DepoPivot1ExcelExportCommand => 
            new DelegateCommand<object>(o => ExportService4.ExportTo(ExportType.XLSX, getExpDosyaAd("DepoPivot1")), c => YetkiliMi_FromDb(nameof(DepoPivot1ExcelExportCommand)));



        public DelegateCommand<object> YerlesimKaydetCommand1 => 
            new DelegateCommand<object>(o=>ExportService1.SaveLayout("DepoGuncel.xml"), c => true);
        public DelegateCommand<object> YerlesimKaydetCommand2 => 
            new DelegateCommand<object>(o=> ExportService2.SaveLayout("DepoSevkEdilenler.xml"), c => true);
        public DelegateCommand<object> DepoRapor1YerlesimKaydetCommand => 
            new DelegateCommand<object>(o=> ExportService3.SaveLayout("DepoRapor1.xml"), c => true);
        public DelegateCommand<object> DepoPivot1YerlesimKaydetCommand => 
            new DelegateCommand<object>(o => ExportService4.SaveLayout("DepoPivot1.xml"), c => true);

     

        private string getExpDosyaAd(string tab)=> $"{tab}_{DateTime.Now.ToString("dd/MM/yyyy_HH_mm")}.xlsx";
     

        public ObservableCollection<string> Yillar { get => yillar; set => SetProperty(ref yillar, value); }
        public List<string> Aylar => new List<string> { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "0" };



        public DelegateCommand<string> DepodakiPaletleriGosterCommand => new DelegateCommand<string>(OnDepodakiPaletleriGoster);
        private void OnDepodakiPaletleriGoster(string paletKonum) => DepoPaletleriGetir(PALETKONUM.DEPO);


        public DelegateCommand DepoAradakiPaletleriGosterCommand => new DelegateCommand(OnDepoAradakiPaletleriGetir);
        private void OnDepoAradakiPaletleriGetir() => DepoPaletleriGetir(PALETKONUM.URETIME_GERI_VERILEN);


        public DelegateCommand DepoKarantinaPaletleriGosterCommand => new DelegateCommand(DepoKarantinadakiPaletleriGetir);
        private void DepoKarantinadakiPaletleriGetir() => DepoPaletleriGetir(PALETKONUM.KARANTINA);


        public DelegateCommand DepoSevkEmrineEkliPaletleriGosterCommand => new DelegateCommand(DepoSevkEmrineEkliPaletleriGetir);
        private void DepoSevkEmrineEkliPaletleriGetir() => DepoPaletleriGetir(PALETKONUM.SEVKEMRI);


        private void DepoPaletleriGetir(string paletKonum)
        {
            uow = new UnitOfWork();
            MamulDepoStoklar = uow.PlanlamaRepo.MamulDepoStoklariFiltrele(lmeYoksaGunluktenAlinsinMi: true, paletKonum);
        }



        public string SeciliYil
        {
            get => _seciliYil;
            set
            {
                _seciliYil = value;

                SevkEdilenlerListe = uow.PlanlamaRepo
                    .MamulDepoSevkEdilenleriGetir(lmeYoksaGunluktenAlinsinMi: true, SeciliYil, SeciliAy);

            }
        }
        public string SeciliAy
        {
            get => _seciliAy;
            set
            {
                _seciliAy = value;

                SevkEdilenlerListe = uow.PlanlamaRepo
                    .MamulDepoSevkEdilenleriGetir(lmeYoksaGunluktenAlinsinMi: true, SeciliYil, SeciliAy);

            }
        }


        public DelegateCommand<object> DepoyaKabulCommand => new DelegateCommand<object>(OnDepoyaKabul, CanDepoyaKabul);

        public DelegateCommand<MamulDepoStokDto> DepoyaAlCommand => new DelegateCommand<MamulDepoStokDto>(OnDepoyaAl, true);

        private void OnDepoyaAl(MamulDepoStokDto obj)
        {

            var uow1 = new UnitOfWork();

            var palet = uow1.PlanlamaRepo.PaletGetir(obj.PaletId);

            MamulDepoStoklar.Remove(obj);

            palet.PaletKonum = PALETKONUM.DEPO;
            palet.DepoKabulTarihi = DateTime.Now;

            uow1.Commit();

        }

        public DelegateCommand<object> DepoKarantinayaGonderCommand => new DelegateCommand<object>(OnDepoKarantinayaGonder);

        public DelegateCommand<MamulDepoStokDto> DepodanKarantinayaGonderCommand => new DelegateCommand<MamulDepoStokDto>(OnDepodanKarantinayaGonder);


        public DelegateCommand<MamulDepoStokDto> DepodanUretimeGeriGonderCommand => new DelegateCommand<MamulDepoStokDto>(OnDepodanUretimeGeriGonder);

        private void OnDepodanUretimeGeriGonder(MamulDepoStokDto obj)
        {
            var uow1 = new UnitOfWork();

            var palet = uow1.PlanlamaRepo.PaletGetir(obj.PaletId);

            MamulDepoStoklar.Remove(obj);

            palet.PaletKonum = PALETKONUM.URETIME_GERI_VERILEN;
            palet.DepoKabulTarihi = DateTime.Now;

            uow1.Commit();
        }

        private bool CanDepodanPandaStokGonder(MamulDepoStokDto arg)
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI ||
                AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;
        }


        private void OnDepodanKarantinayaGonder(MamulDepoStokDto obj)
        {
            var uow1 = new UnitOfWork();

            var palet = uow1.PlanlamaRepo.PaletGetir(obj.PaletId);

            MamulDepoStoklar.Remove(obj);

            palet.PaletKonum = PALETKONUM.KARANTINA;
            palet.DepoKabulTarihi = DateTime.Now;

            uow1.Commit();

        }

        public DelegateCommand<object> DepoRedCommand => new DelegateCommand<object>(OnDepoRed, true);


        public DelegateCommand<object> AraDepoyaGonderCommand => new DelegateCommand<object>(OnAraDepoyaGonder, true);

        private void OnAraDepoyaGonder(object obj)
        {
            var palet = obj as Palet;

            palet.DepoKabulTarihi = null;
            palet.PaletKonum = PALETKONUM.URETIME_GERI_VERILEN;

            uow.Commit();

            DepoIcinBekleyenPaletler.Remove(palet);
        }

        [YetkiKontrol]
        public DelegateCommand<object> SevkiyatGeriAlIslemCommand => new DelegateCommand<object>(OnSevkiyatGeriAlIslem, c => YetkiliMi_FromDb(nameof(SevkiyatGeriAlIslemCommand)));

        [YetkiKontrol]
        public DelegateCommand<object> SevkiyatIslemCommand => new DelegateCommand<object>(OnSevkiyatIslem, c => YetkiliMi_FromDb(nameof(SevkiyatIslemCommand)));

        [YetkiKontrol]
        public DelegateCommand<object> YenidenUretimeGonderCommand => new DelegateCommand<object>(OnYenidenUretimeGonder, true);


        public DelegateCommand<MamulDepoStokDto> PaletBarkodYazdirCommand => new DelegateCommand<MamulDepoStokDto>(OnPaletBarkodYazdir, c => true);

        public DelegateCommand<string> SiparisiAcCommand => new DelegateCommand<string>(OnSiparisiAc, c => true);


        public DelegateCommand<string> SarjIlgiliKalemKodCommand => new DelegateCommand<string>(OnSarjIlgiliKalem, true);
        public DelegateCommand<object> SarjYeniSiparisCommand => new DelegateCommand<object>(OnSarjYeniSiparis, true);

        public DelegateCommand<object> EkraniGuncelleCommand => new DelegateCommand<object>(OnTabaGoreEkraniGuncelle, true);

        private void OnTabaGoreEkraniGuncelle(object obj)
        {

            uow = new UnitOfWork();

            if (SeciliTabIndex == 0) DepoIcinBekleyenPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetirYeni();

            if (SeciliTabIndex == 1 || SeciliTabIndex == 4)
            {
                MamulDepoStoklar = uow.PlanlamaRepo.MamulDepoStoklariFiltrele(lmeYoksaGunluktenAlinsinMi: true, PALETKONUM.DEPO);
            }

            if (SeciliTabIndex == 2) SevkEdilenlerListe = uow.PlanlamaRepo
                    .MamulDepoSevkEdilenleriGetir(lmeYoksaGunluktenAlinsinMi: true, SeciliYil, SeciliAy)
                        .OrderByDescending(c => c.SevkiyatTarihi).ToList();


            if (SeciliTabIndex == 3)
            {
                var m_stoklar = uow.PlanlamaRepo.MamulDepoStoklariFiltrele(lmeYoksaGunluktenAlinsinMi: true, PALETKONUM.DEPO);

                DepoRapor1 = m_stoklar
                     .GroupBy(c => new { c.CariIsim, c.UlkeAd, c.UlkeKod })
                     .Select(c => new DepoRapor1_Data
                     { CariAd = c.Key.CariIsim, UlkeAd = c.Key.UlkeAd, DepoMiktar = c.Sum(p => p.PaletNet_Kg.GetValueOrDefault()) })
                     .ToObservableCollection();
            }




        }

        private void OnSarjIlgiliKalem(string kalemKod)
        {
            kalemKod = kalemKod.Trim();

            if (kalemKod == "")
            {
                MessageBox.Show("Şarj edilecek kalem kodunu giriniz", "Pandap", MessageBoxButton.OK);
                return;
            }



            var uowSarj = new UnitOfWork();

            var sarjkalem = uowSarj.SiparisKalemRepo.SiparisKalemiGetir(kalemKod);

            if (sarjkalem == null)
            {
                MessageBox.Show($"{kalemKod} numaralı kalem bulunamadı.", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var siparis = uow.SiparisRepo.SiparisGetir(sarjkalem.SiparisKod);


            //if (siparis.KapasitifMi == true)
            //{
            //    MessageBox.Show("Kapasitif siparişe şarj yapılamaz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Stop);
            //    return;
            //}


            //if (sarjkalem.PLAN_KalemKapatildiMi == true)
            //{
            //    var cev = MessageBox.Show("Bu kalem kapalı yinede şarj etmek istiyormusunuz", "Pandap", MessageBoxButton.YesNo);
            //    if (cev == MessageBoxResult.No)
            //        return;
            //}

            var depoSecilenler = MamulDepoStoklar.Where(c => c.Sec == true).ToList();

            foreach (var item in depoSecilenler)
            {
                uowSarj.PlanlamaRepo.PaletiKalemSarjEt(item.PaletId, kalemKod);
            }

            uowSarj.Commit();

            uow = new UnitOfWork();
            MamulDepoStoklar = uow.PlanlamaRepo.MamulDepoStoklariGetir(lmeYoksaGunluktenAlinsinMi: true);

            MessageBoxService.ShowMessage("Şarj İşlemi Başarılı", "Pandap", MessageButton.OK, MessageIcon.Information);


        }

        public bool EskisineUretimYapilsinMi
        {
            get => _eskisineUretimYapilsinMi;
            set
            {
                SetProperty(ref _eskisineUretimYapilsinMi, value);
            }
        }

        private void OnSarjYeniSiparis(object obj)
        {

            var _secilenMamulPaletler = MamulDepoStoklar.Where(c => c.Sec == true).ToList();

            var farkliFirmaSayisi = _secilenMamulPaletler.Select(c => c.CariIsim).Distinct().Count();

            if (farkliFirmaSayisi > 1)
            {
                MessageBoxService.ShowMessage("Sadece aynı firmaya ait paletleri seçebilirsiniz", "Pandap", MessageButton.OK, MessageIcon.Stop);
                return;
            }

            //var siparis = Siparis.SiparisOlustur(AppPandap.AktifKullanici.KullaniciId, null, false, null);


            foreach (var paletDTO in _secilenMamulPaletler)
            {
                //var sipKalem = siparis.KalemOluştur();

                //var paletKalem = uow.SiparisKalemRepo.SiparisKalemiGetir(paletDTO.FiyatKalemKod);

                //sipKalem = Pandap.Logic.Helper.PandapObjectHelper.CopyObject(paletKalem);

                //sipKalem.SiparisKalemKod = null;
                //sipKalem.SiparisKod = siparis.SiparisKod;

                //sipKalem.Miktar_kg = paletDTO.PaletNet_Kg;
                //sipKalem.OPER_NetsisBelgeNo = null;

                //sipKalem.IscilikTutar = 0;

                //sipKalem.RowGuid = Guid.NewGuid();

                //siparis.KalemEkle(sipKalem);
            }

            //siparis.SarjMi = true;
            //siparis.KapasitifMi = false;
            //siparis.SarjPaletIdList = _secilenMamulPaletler.Select(c => c.PaletId).ToList();


            //var vm = new SiparisViewModel();
            //vm.SiparisKayitModu = KayitModu.Add;
            //vm.Load(siparis);

            //var doc = AppPandap.pDocumentManagerService.CreateDocument(nameof(SiparisView), vm);
            //doc.Title = "Yeni şarj";

            //doc.Show();


            if (EskisineUretimYapilsinMi)
            {
                var sipKod = _secilenMamulPaletler.First().SiparisKalemKod.Substring(0, 8);
                YeniSiparisOlustur(sipKod, _secilenMamulPaletler, "Yeniden Üretim");
            }

        }



        public void YeniSiparisOlustur(string sipKod, List<MamulDepoStokDto> _secilenMamulPaletler, string baslik)
        {
            //var sip_orj = uow.SiparisRepo.SiparisGetir(sipKod);

            //var sip_kopya = Siparis.SiparisOlustur(AppPandap.AktifKullanici.KullaniciId, null, false, sip_orj);

            //sip_kopya.SiparisKod = null;
            //sip_kopya.SiparisSurecDurum = SIPARISSURECDURUM.MUSTERIONAYLI;


            //sip_kopya.TumKalemleriSil();


            //foreach (var m_palet in _secilenMamulPaletler)
            //{
            //    var sipKalem = sip_kopya.KalemOluştur();

            //    var paletKalem = uow.SiparisKalemRepo.SiparisKalemiGetir(m_palet.SiparisKalemKod);

            //    sipKalem = Pandap.Logic.Helper.PandapObjectHelper.CopyObject(paletKalem);
            //    sipKalem.SiparisKalemKod = null;
            //    sipKalem.SiparisKod = sip_kopya.SiparisKod;

            //    sipKalem.Miktar_kg = m_palet.PaletNet_Kg;
            //    sipKalem.OPER_NetsisBelgeNo = null;

            //    sipKalem.RowGuid = Guid.NewGuid();

            //    sip_kopya.KalemEkle(sipKalem);
            //}

            //var vm = new SiparisViewModel();
            //vm.SiparisKayitModu = KayitModu.Add;
            //vm.Load(sip_kopya);

            //var doc1 = AppPandap.pDocumentManagerService.CreateDocument(nameof(SiparisView), vm);
            //doc1.Title = baslik;
            //doc1.Show();
        }


        private void OnSiparisiAc(string siparisKodKalem)
        {

            var siparisKod = siparisKodKalem.Split('/')[0];

            var uow1 = new UnitOfWork();
            var vm = new SiparisViewModel();
            var sip = uow1.SiparisRepo.SiparisGetir(siparisKod);

            vm.Load(sip);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SiparisView", vm);
            doc.Title = sip.SiparisKod;
            doc.Show();
        }

        private void OnPaletBarkodYazdir(MamulDepoStokDto _palet)
        {
            var raporTanimId = 7;

            //PaletBarkod_GTATA_JAEGGI özel durum   paketlemede de var
            if (_palet.CariKod == "120-98-01-344" || _palet.CariKod == "120-98-01-493") raporTanimId = 60;

            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(raporTanimId);

            var palet = uow.PlanlamaRepo.PaletGetir(_palet.PaletId);

            var dsObject = PaketlemeViewModel.BarkodPaletDtoFromSeciliPalet(palet);

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }



        public List<int> KayitSayiListe => new List<int> { 1000, 2000, 5000, 10000, 50000, 100000 };

        public DepoViewModel(string menuFormAd)
        {
            FormMenuAd = menuFormAd;

            _seciliYil = DateTime.Now.Year.ToString();
            _seciliAy = DateTime.Now.Month.ToString().PadLeft(2, '0');

            Yillar = new ObservableCollection<string>();

            for (int i = 2017; i <= DateTime.Now.Year; i++)
            {
                Yillar.Add(i.ToString());
            }


            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);

            SevkSeciliKayitSayisi = KayitSayiListe[0];
        }

        public void Load()
        {
            SeciliTabIndex = 0;

            DepoIcinBekleyenPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetirYeni();

            DepoDurum = new DepoInfo();

            ExportService1.RestoreLayout("DepoGuncel.xml");
            ExportService2.RestoreLayout("SevkEdilenler.xml");
            ExportService3.RestoreLayout("DepoRapor1.xml");
            ExportService4.RestoreLayout("DepoPivot1.xml");
        }

        public string SevkiyatBaslik => DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("tr"))
            + " " + DateTime.Now.Year.ToString() + " Sevkiyat T.";

        public ObservableCollection<Palet> DepoIcinBekleyenPaletler
        {
            get => depodakiPaletler; set => SetProperty(ref depodakiPaletler, value);
        }

        public DepoInfo DepoDurum { get; set; }

        public Palet DepoSeciliPalet
        {
            get { return _depoSeciliPalet; }
            set => SetProperty(ref _depoSeciliPalet, value);
        }

        public bool FormLoaded { get; set; }

        public DelegateCommand InitCommand => new DelegateCommand(FormLoad, true);

        public ObservableCollection<DepoRapor1_Data> DepoRapor1 { get => _depoRapor1; set => SetProperty(ref _depoRapor1, value); }

        public List<MamulDepoStokDto> MamulDepoStoklar
        {
            get => mamulDepoStokDtoListe; set => SetProperty(ref mamulDepoStokDtoListe, value);
        }

        public MamulDepoStokDto SeciliMamulDepoStok
        {
            get => seciliMamulDepoStok;
            set
            {
                if (FormLoaded == false) return;

                var oldValue = seciliMamulDepoStok;

                if (SetProperty(ref seciliMamulDepoStok, value) == true)
                {
                    if (oldValue != null) oldValue.PropertyChanged -= SeciliMamulDepoStok_PropertyChanged;

                    if (value != null) value.PropertyChanged += SeciliMamulDepoStok_PropertyChanged;


                }

            }
        }





        public MamulDepoStokDto SeciliSevkEdilen
        {
            get => seciliSevkEdilen;

            set
            {
                if (SetProperty(ref seciliSevkEdilen, value))
                {
                    if (SeciliSevkEdilen == null) return;

                    SeciliSevkEdilen.PropertyChanged -= SeciliSevkEdilen_PropertyChanged;
                    SeciliSevkEdilen.PropertyChanged += SeciliSevkEdilen_PropertyChanged;
                };
            }
        }

        public int SeciliTabIndex
        {
            get
            {
                return seciliTabIndex;
            }
            set
            {
                if (SetProperty(ref seciliTabIndex, value))
                {
                    OnTabaGoreEkraniGuncelle(null);
                }
            }
        }

        public List<MamulDepoStokDto> SevkEdilenlerListe
        {
            get => _sevkEdilenlerListe; set => SetProperty(ref _sevkEdilenlerListe, value);
        }
        public int SevkSeciliKayitSayisi
        {
            get => _sevkSeciliKayitSayisi;
            set
            {
                SetProperty(ref _sevkSeciliKayitSayisi, value);


            }
        }

        private void OnYenidenUretimeGonder(object obj)
        {
            if (SeciliMamulDepoStok == null) return;

            var secilenSayi = MamulDepoStoklar.Count(c => c.Sec == true);

            if (secilenSayi == 0)
            {
                MessageBox.Show("Üretime geri gidecek edilecek malzeme seçiniz");
            }

            MessageBoxResult cevap = MessageBox.Show("Yeniden üretim işlemini onaylıyormusunuz?", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (cevap == MessageBoxResult.Cancel) return;

            foreach (var item in mamulDepoStokDtoListe)
            {
                if (item.Sec == true)
                {
                    var _palet = uow.PlanlamaRepo.PaletGetir(item.PaletId);

                    _palet.SevkiyatTarihi = null;
                    _palet.DepoOnayaGonderimTarihi = null;
                }
            }

            uow.Commit();

            OnTabaGoreEkraniGuncelle(null);
        }

        private bool CanDepoyaKabul(object arg)
        {
            return SeciliTabIndex == 0;
        }




        private void FormLoad()
        {
            FormLoaded = true;
        }

        private void IrsaliyeNoKaydet(object obj)
        {
            uow.PlanlamaRepo.PaletIrsaliyeNoKaydet(SeciliSevkEdilen.PaletId, SeciliSevkEdilen.NetsisIrsaliyeNo);
        }

        private void OnDepoKarantinayaGonder(object obj)
        {
            var palet = obj as Palet;

            palet.DepoKabulTarihi = null;
            palet.PaletKonum = PALETKONUM.KARANTINA;

            uow.Commit();

            DepoIcinBekleyenPaletler.Remove(palet);
        }

        private void OnDepoRed(object obj)
        {
            var palet = obj as Palet;

            palet.DepoKabulTarihi = null;
            palet.PaletKonum = PALETKONUM.DEPO_ONAY;

            uow.Commit();

            DepoIcinBekleyenPaletler.Remove(palet);
        }

        private void OnDepoyaKabul(object obj)
        {

            var palet = obj as Palet;

            palet.DepoKabulTarihi = DateTime.Now;
            palet.PaletKonum = PALETKONUM.DEPO;

            uow.Commit();

            DepoIcinBekleyenPaletler.Remove(palet);

        }

        private void OnSevkiyatGeriAlIslem(object obj)
        {
            var uow1 = new UnitOfWork();

            var secilenSayi = _sevkEdilenlerListe.Count(c => c.Sec == true);

            if (secilenSayi == 0)
            {
                MessageBox.Show("Sevk edilecek malzeme seçiniz");
                return;
            }

            MessageBoxResult cevap = MessageBox.Show("Sevkiyat Geri Alma işlemini onaylıyormusunuz?", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (cevap == MessageBoxResult.Cancel) return;

            foreach (var item in _sevkEdilenlerListe)
            {
                if (item.Sec == true)
                {
                    var _palet = uow1.PlanlamaRepo.PaletGetir(item.PaletId);
                    _palet.PaletKonum = PALETKONUM.DEPO;
                    _palet.SevkiyatTarihi = null;
                }
            }

            var x = uow1.Commit();

            OnTabaGoreEkraniGuncelle(null);
        }

        private void OnSevkiyatIslem(object obj)
        {
            if (SeciliMamulDepoStok == null) return;

            var secilenSayi = MamulDepoStoklar.Count(c => c.Sec == true);

            if (secilenSayi == 0)
            {
                MessageBox.Show("Sevk edilecek malzeme seçiniz");
            }

            MessageBoxResult cevap = MessageBox.Show("Sevk işlemini onaylıyormusunuz?", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (cevap == MessageBoxResult.Cancel) return;

            foreach (var item in mamulDepoStokDtoListe)
            {
                if (item.Sec == true)
                {
                    var _palet = uow.PlanlamaRepo.PaletGetir(item.PaletId);

                    _palet.SevkiyatTarihi = DateTime.Now.Date;
                    _palet.PaletKonum = PALETKONUM.SEVKEDILDI;
                }
            }

            var x = uow.Commit();

            OnTabaGoreEkraniGuncelle(null);
        }

        private void PaletAciklamaKaydet(object obj)
        {
            uow.PlanlamaRepo.PaletAciklamaKaydet(SeciliMamulDepoStok.PaletId, seciliMamulDepoStok.Aciklama);
        }

        private void SeciliMamulDepoStok_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SeciliMamulDepoStok.Aciklama))
            {
                uow.PlanlamaRepo.PaletAciklamaKaydet(SeciliMamulDepoStok.PaletId, seciliMamulDepoStok.Aciklama);
            };
        }

        private void SeciliSevkEdilen_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SeciliSevkEdilen.NetsisIrsaliyeNo))
            {
                IrsaliyeNoKaydet(null);
            };
        }
    }
}