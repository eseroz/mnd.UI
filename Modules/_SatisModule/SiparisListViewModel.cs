using DevExpress.Mvvm;
using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic;
using mnd.Logic.BC_Satis._Siparis.DataServices;
using mnd.Logic.Helper;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.Logic.Persistence.Repositories;
using mnd.Logic.Services;
using mnd.Logic.Services.SiparisService;
using mnd.UI.AppModules.AppModule;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules.OperasyonModule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules._SatisModule
{
    public class SiparisListViewModel : MyDxViewModelBase
    {
        private bool _planlamaKapatilanlariGoster;
        private SiparisDTO _seciliSiparisDto;
        private List<string> _siparisDurumlari = new List<string>();
        private ObservableCollection<SiparisDTO> _siparisListe;

        private int _toplamPaketMiktari;
        private ObservableCollection<AlasimMiktarDto> _alasimDurumlari;
        private bool _dataIsLoading;

        private string _gridLayoutFileName1;
        private string _gridLayoutFileName2;
        private bool _kapasitifSiparislerGoster = false;
        private string _listeSiparisSurecDurum;
        private bool _normalSiparislerGoster = true;
        private bool _planlamaKapatılanSiparislerGoster;
        private bool _sadeceAcikSiparisKalemleri = true;
        private bool _sadeceOnayliSiparisKalemleri = true;
        private ObservableCollection<SiparisDTO> _siparisListeVisibleData;
        private readonly UnitOfWork _uow;

        public DelegateCommand FormLoadedCommand => new DelegateCommand(FormLoaded);

        Stopwatch _stopWatch = new Stopwatch();


        public int KapasitifSiparisSayisi { get => kapasitifSiparisSayisi; set => SetProperty(ref kapasitifSiparisSayisi, value); }

        public int NormalSiparisSayisi { get => normalSiparisSayisi; set => SetProperty(ref normalSiparisSayisi, value); }


        public string AktifYilHafta { get => aktifYilHafta; set => SetProperty(ref aktifYilHafta, value); }

        private void FormLoaded()
        {

        }

        public double FormYuklenmeSuresi { get => formYuklenmeSuresi; set => SetProperty(ref formYuklenmeSuresi, value); }

        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");

        public IExportService ExportService2 => ServiceContainer.GetService<IExportService>("servis2");

        [YetkiKontrol]
        public DelegateCommand<bool> SiparisYeniCommand => new DelegateCommand<bool>(SiparisYeni, t => YetkiliMi_FromDb(nameof(SiparisYeniCommand)));

        public DelegateCommand SiparisKapasitiftenYeniCommand => new DelegateCommand(KapasitiftenYeni, () => SeciliSiparisDto != null && SeciliSiparisDto.KapasitifMi.GetValueOrDefault());

        [YetkiKontrol]
        public DelegateCommand SiparisEditCommand => new DelegateCommand(SiparisEdit, () => YetkiliMi_FromDb(nameof(SiparisEditCommand)));

        [YetkiKontrol]
        public DelegateCommand SiparisSilCommand => new DelegateCommand(SiparisSil, CanSiparisSil);

        [YetkiKontrol]
        public DelegateCommand SiparisKapatCommand => new DelegateCommand(SiparisKapat, () => YetkiliMi_FromDb(nameof(SiparisKapatCommand)));

        [YetkiKontrol]
        public DelegateCommand KarantinadanGeriAlCommand => new DelegateCommand(KarantinadanGeriAl, () => YetkiliMi_FromDb(nameof(KarantinadanGeriAlCommand)));

        [YetkiKontrol]
        public DelegateCommand KarantinayaAktarCommand => new DelegateCommand(KarantinayaAktar, () => YetkiliMi_FromDb(nameof(KarantinayaAktarCommand)));

        [YetkiKontrol]
        public DelegateCommand BolgeYoneticiOnayaGonderCommand => new DelegateCommand(BolgeYoneticiOnayaGonder, () => YetkiliMi_FromDb(nameof(BolgeYoneticiOnayaGonderCommand)));

        [YetkiKontrol]
        public DelegateCommand<object> OnayCommand => new DelegateCommand<object>(Onayla, c => YetkiliMi_FromDb(nameof(OnayCommand)));

        [YetkiKontrol]
        public DelegateCommand PlanlamayaGonderCommand => new DelegateCommand(PlanlamayaGonder, () => YetkiliMi_FromDb(nameof(PlanlamayaGonderCommand)));

        public DelegateCommand VerileriTazeleCommand => new DelegateCommand(VerileriTazele, true);

        public DelegateCommand<SiparisDTO> PandapMessangerAcCommand => new DelegateCommand<SiparisDTO>(PandapMessangerAc);

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand1 => new DelegateCommand<object>(OnExcelExport1, c => YetkiliMi_FromDb(nameof(ExcelExportCommand1)));

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand2 => new DelegateCommand<object>(OnExcelExport2, c => YetkiliMi_FromDb(nameof(ExcelExportCommand2)));

        public DelegateCommand<object> YerlesimKaydetCommand1 => new DelegateCommand<object>(OnYerlesimKaydet1, c => true);

        public DelegateCommand<object> YerlesimKaydetCommand2 => new DelegateCommand<object>(OnYerlesimKaydet2, c => true);

        public DelegateCommand BittiCommand => new DelegateCommand(GridControlFiltreBitti, () => true);

        [YetkiKontrol]
        public bool FiyatSutunGorunsunMu => FormPermissions.Any(c => c.Komut == nameof(FiyatSutunGorunsunMu));

        public bool SiparisIslemVarmi
        {
            get
            {
                return PlanlamayaGonderCommand.CanExecute(null)
                        || OnayCommand.CanExecute(null)
                        || BolgeYoneticiOnayaGonderCommand.CanExecute(null);
            }
        }

        private bool CanSiparisSil()
        {
            if (SeciliSiparisDto == null) return false;

            var kalemListe = SeciliSiparisDto.SiparisKalemDTO_List.ToList();

            return YetkiliMi_FromDb(nameof(SiparisSilCommand))
                    //&& !kalemListe)
                    && SeciliSiparisDto.SiparisSurecDurum == SIPARISSURECDURUM.SATISTA;
        }

        public SiparisListViewModel(string formMenuAd)
        {
            _stopWatch.Start();

            FormMenuAd = formMenuAd;
            _uow = new UnitOfWork();

            FormPermissions = _uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);

            var sipDurumlari = SIPARISSURECDURUM.SurecDurumListesiGetir();

            _siparisDurumlari.AddRange(sipDurumlari);

            Messenger.Default.Register<Siparis>(this, SiparisKayitMessageData);



        }

        public async void Load(bool planlamaKapatilanlariGoster, string strSiparisSurecDurum)
        {

            var x = NavBadgeSatisService
               .SiparisSurecSayilariniGrupluGetir(AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';'), strSiparisSurecDurum);

            NormalSiparisSayisi = x.GetValueOrDefault(strSiparisSurecDurum + "_0");
            KapasitifSiparisSayisi = x.GetValueOrDefault(strSiparisSurecDurum + "_1");


            var aktifHafta = CalenderUtil.GetWeekNumberFromDate(DateTime.Now);
            AktifYilHafta = DateTime.Now.Year.ToString() + "/" + aktifHafta.ToString().PadLeft(2, '0');

            DataIsLoading = true;

            ListeSiparisSurecDurum = strSiparisSurecDurum;

            _planlamaKapatilanlariGoster = planlamaKapatilanlariGoster;

            PlanlamaKapatılanSiparislerGoster = planlamaKapatilanlariGoster;

            SeciliAnaMenu = strSiparisSurecDurum;

            if (String.IsNullOrEmpty(strSiparisSurecDurum)) strSiparisSurecDurum = "Tümü";

            GridLayoutFileName1 = strSiparisSurecDurum.Replace(" ", string.Empty) + "1.xml";
            GridLayoutFileName2 = strSiparisSurecDurum.Replace(" ", string.Empty) + "2.xml";

            ExportService1.RestoreLayout(GridLayoutFileName1);

            ExportService2.RestoreLayout(GridLayoutFileName2);

            var sonuc = await _uow
                .SiparisDtoRepo
                .SiparisDTO_ListeGetirAsync(
                             strSiparisSurecDurum, 
                             KapasitifSiparislerGoster, 
                             NormalSiparislerGoster, 
                             PlanlamaKapatılanSiparislerGoster, 
                             null, 
                             AppPandap.AktifKullanici.KullaniciId
                             );

            SiparisListe = sonuc.ToObservableCollection();

            var dict = _uow.MuhasebeRepo.IrsaliyeSiparisOzetGetir();
            var dictKalem = _uow.MuhasebeRepo.IrsaliyeSiparisOzetGetirKalemBazli();
            var dictFaturaHafta = _uow.MuhasebeRepo.SiparisFaturaHaftaGetir();

            //foreach (var sip in SiparisListe)
            //{
            //    sip.FaturaEdilenHaftalar = dictFaturaHafta.GetValueOrDefault(sip.SiparisKod);
            //    sip.FaturaEdilenMiktar = dict.GetValueOrDefault(sip.SiparisKod);

            //    foreach (var k in sip.SiparisKalemDTO_List)
            //    {
            //        k.KalemFaturaMiktarlariKg = dictKalem.GetValueOrDefault(k.SiparisKalemKod);
            //    }

            //}


            SiparisListe.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            GridControlFiltreBitti();

            DataIsLoading = false;

            FormYuklenmeSuresi = _stopWatch.Elapsed.TotalSeconds;
        }

        private void GridControlFiltreBitti()
        {
            SiparisKalemDtoListe = SiparisListeVisibleData.SelectMany(siparis => siparis.SiparisKalemDTO_List,
                (siparisDTO, siparisKalemDTO) => new { siparisDTO, siparisKalemDTO })
                .Select(s =>
                {
                    //s.siparisKalemDTO.CariIsim = s.siparisDTO.CariIsim;
                    //s.siparisKalemDTO.UlkeKodIso = s.siparisDTO.UlkeKodIso;
                    //s.siparisKalemDTO.SevkHaftasi = s.siparisDTO.SevkHaftasi;
                    //s.siparisKalemDTO.TeslimHaftasi = s.siparisDTO.TeslimHaftasi;
                 

                    return s.siparisKalemDTO;
                })
                .ToObservableCollection<SiparisKalemDTO>();

            //AlasimDurumlari = SiparisKalemDtoListe
            //    .GroupBy(c => new { c.KullanimAlanTipAd })
            //    .Select(g =>
            //        new AlasimMiktarDto
            //        {
            //            AlasimAd = g.Key.KullanimAlanTipAd,
            //            MiktarKg = g.Sum(c => c.KalemIsyuku.GetValueOrDefault() / 1000)
            //        })
            //    .ToObservableCollection();
        }

        private void OnExcelExport1(object obj)
        {
            ExportService1.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void OnExcelExport2(object obj)
        {
            ExportService2.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void BolgeYoneticiOnayaGonder()
        {
            var uow = new UnitOfWork();
            var uow_ayarlar = new UnitOfWork();
            var ayar_data = uow_ayarlar.AppRepo.UygulamaBilgiGetir();
            var _siparis = uow.SiparisRepo.SiparisGetir(SeciliSiparisDto.SiparisKod);

            var uow1 = new UnitOfWork();
            var cariKod = _siparis.CariKod;

            //if (_siparis.KapasitifMi == true)
            //{
            //    _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto, SIPARISSURECDURUM.SATIS_BOLGEYONETICIONAYINDA);
            //    SiparisListe.Remove(SeciliSiparisDto);
            //    return;
            //}

            PandapCariService.NetsistenAlarakCariGuncelle(cariKod);

            PandapCari sipCari = PandapCariService.CariGetir(cariKod);
            PandapCari ikinciCarisi = null;

            if (cariKod.Contains("120-01-")) // Yurt içi
                ikinciCarisi = PandapCariService
                                       .VergiNumarasindanCariGetir(sipCari.VergiNumarasi)
                                       .Where(c => c.CariKod != cariKod)
                                       .FirstOrDefault();


            if (cariKod.Contains("120-98-") || cariKod.Contains("120-05-") || cariKod.Contains("120-06-"))  // yurt dışı
            {
                ikinciCarisi = PandapCariService
                                   .EximKodundanCariGetir(sipCari.EximBankKod.GetValueOrDefault())
                                   .Where(c => c.CariKod != cariKod)
                                   .FirstOrDefault();
            };


            var OnayliSiparisTutar = CariRiskService.MusteriOnayliSiparisFiyatlariGetir(cariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var YeniSiparislerToplamTutar = CariRiskService.SatisYeniKayitFiyatlariGetir(cariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var UretimTutar = CariRiskService.UretimEmriIsYuku_FiyatGetir(cariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var DepoTutar = CariRiskService.DepoPaletFiyatGetir(cariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var BekleyenSevkiyatEmirleriTutar = CariRiskService.SevkiyatEmirleriFiyatGetir(cariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var UretilecekOnayliSiparisTutar = OnayliSiparisTutar - (UretimTutar + DepoTutar + BekleyenSevkiyatEmirleriTutar);
            var AktifSevkEmriTutar = 0;

            var kapasitifMiktarKg = CariRiskService.SatisKapasitifFiyatGetir(cariKod).FirstOrDefault()?.Miktar_Kg ?? 0;


            sipCari.HesapAlanRiskleriYukle("Satış", YeniSiparislerToplamTutar, UretimTutar,
                                            DepoTutar, BekleyenSevkiyatEmirleriTutar,
                                            AktifSevkEmriTutar, UretilecekOnayliSiparisTutar);

            var risk_fark = sipCari.KullanilabilirLimit;


            if (risk_fark < 0)
            {
                var uyariMetin = "";

                LimitTalepFormReportModel limitRaporModel = new LimitTalepFormReportModel();
                limitRaporModel.TalepEdenKisi = AppPandap.AktifKullanici.AdSoyad;
                limitRaporModel.TalepEdenBirim = "SATIŞ";
                limitRaporModel.IstemTarihi = DateTime.Now;

                var talepMiktar = YeniSiparislerToplamTutar;

                uyariMetin = $"LÜTFEN {YeniSiparislerToplamTutar.ToString("n0")} " +
                    $"{ DovizHelper.SimgeyeDonustur(_siparis.TakipDovizTipKod)} LİMİT ONAYI ALINIZ";

                if (ikinciCarisi == null)
                {
                    limitRaporModel.LimitSutun1 = LimitTalepFormDataOlustur(sipCari, kapasitifMiktarKg, talepMiktar);
                }
                else
                {
                    limitRaporModel.LimitSutun1 = LimitTalepFormDataOlustur(sipCari, kapasitifMiktarKg, talepMiktar);
                    limitRaporModel.LimitSutun2 = LimitTalepFormDataOlustur(ikinciCarisi, kapasitifMiktarKg, talepMiktar);

                }

                MessageBox.Show(uyariMetin, "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                LimitTalepFormYazdir(limitRaporModel);

                return;

            }


            var yeniRisk = (risk_fark - YeniSiparislerToplamTutar);

            if (yeniRisk < 0)
            {
                var uyariMetin = "";

                LimitTalepFormReportModel limitRaporModel = new LimitTalepFormReportModel();
                limitRaporModel.TalepEdenKisi = AppPandap.AktifKullanici.AdSoyad;
                limitRaporModel.TalepEdenBirim = "SATIŞ";
                limitRaporModel.IstemTarihi = DateTime.Now;

                var talepMiktar = yeniRisk;

                uyariMetin = $"LÜTFEN {yeniRisk.ToString("n1")} " +
                    $"{ DovizHelper.SimgeyeDonustur(_siparis.TakipDovizTipKod)} LİMİT ONAYI ALINIZ";

                if (ikinciCarisi == null)
                {
                    limitRaporModel.LimitSutun1 = LimitTalepFormDataOlustur(sipCari, kapasitifMiktarKg, talepMiktar);
                }
                else
                {
                    limitRaporModel.LimitSutun1 = LimitTalepFormDataOlustur(sipCari, kapasitifMiktarKg, talepMiktar);
                    limitRaporModel.LimitSutun2 = LimitTalepFormDataOlustur(ikinciCarisi, kapasitifMiktarKg, talepMiktar);

                }

                MessageBox.Show(uyariMetin, "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                LimitTalepFormYazdir(limitRaporModel);

                return;

            }

            _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto, SIPARISSURECDURUM.SATIS_BOLGEYONETICIONAYINDA);
            SiparisListe.Remove(SeciliSiparisDto);
        }

        public void LimitTalepFormYazdir(LimitTalepFormReportModel limitTalepModel)
        {
            UnitOfWork uowRapor = new UnitOfWork();

            var dsObject = limitTalepModel;

            var raporTanim = uowRapor.RaporTanimRepo.RaporGetirFromId(14);
            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);

            return;
        }

        public LimitTalepFormDto LimitTalepFormDataOlustur(PandapCari pCari, decimal kapasitifMiktar, decimal talepMiktar)
        {

            var riskBilgi = new LimitTalepFormDto
            {
                IstemTarihi = DateTime.Now.Date,

                DovizKod = pCari.DovizAd,
                CariIsim = pCari.CariIsim,
                UlkeAd = pCari.UlkeAd,
                PlasiyerAdi = pCari.PandaMusteriSorumlusu,

                EximBankLimit = pCari.EximBankLimit,
                DbsLimit = pCari.DbsLimit,
                GarantiFactoringLimit = pCari.GarantiFactoringLimit,
                IngFactoringLimit = pCari.IngFactoringLimit,
                BankaTeminati = pCari.BankaTeminati,
                YoneticiSevkLimit = pCari.YoneticiSevkLimit,
                YoneticiSiparisLimit = pCari.YoneticiSiparisLimit,

                AcikHesap = pCari.AcikHesap,
                NetsisMusteriCekRiski = pCari.NetsisMusteriCekRiski,
                NetsisKendiCekRiski = pCari.NetsisKendiCekRiski,

                YeniSiparisRiski = pCari.SatisYeniKayitRiski.GetValueOrDefault(),
                UretimRiski = pCari.UretimRiski,
                MusteriDepoRiski = pCari.MusteriDepoRiski,
                SevkiyatEmirleriDepoRiski = pCari.MusteriSevkEmirleriRiski,
                AktifSevkEmriTutar = 0,
                UretilecekOnayliSiparislerTutar = pCari.UretilecekOnayliSiparislerTutar,


                ToplamLimit = pCari.ToplamLimit,
                ToplamRisk = pCari.ToplamRisk,
                KullanilabilirLimit = pCari.KullanilabilirLimit,



                TalepEdilenLimit = talepMiktar,

                KapasitifMiktar = kapasitifMiktar / 1000,

            };

            return riskBilgi;

        }




        public decimal SiparisKalemToplamFiyatGetir(Siparis sip)
        {
            UnitOfWork uowLmeBul = new UnitOfWork();
            var lmeTarih = DateTime.Now.AddDays(-1).Date;

            var lmeGunlukFiyat = LmeService.LmeFiyatGetirTarihten(lmeTarih);

            var x = sip.SiparisKalemleri;



            return 0;

        }

        private void SiparisEdit()
        {
            SiparisAc(KayitModu.Edit);
        }

        private void KarantinadanGeriAl()
        {
            _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto, SeciliSiparisDto.SiparisSurecDurumOnceki,
                SIPARISSURECDURUM.SIPARISKARANTINA);
            SiparisListe.Remove(SeciliSiparisDto);
        }

        private void KarantinayaAktar()
        {
            _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto, SIPARISSURECDURUM.SIPARISKARANTINA,
                SeciliAnaMenu);
            SiparisListe.Remove(SeciliSiparisDto);
        }

        private void Onayla(object onayCevap)
        {
            var onaylandi = onayCevap.ToString() == "Ok" ? true : false;

            if (SeciliSiparisDto.SiparisSurecDurum == SIPARISSURECDURUM.SATIS_BOLGEYONETICIONAYINDA)
            {
                _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto,
                    onaylandi ? SIPARISSURECDURUM.PLANLAMADA : SIPARISSURECDURUM.SATISTA);
            }

            if (SeciliSiparisDto.SiparisSurecDurum == SIPARISSURECDURUM.PLANLAMADA)
            {
                _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto,
                    onaylandi ? SIPARISSURECDURUM.SATIS_YONETICIONAYINDA : SIPARISSURECDURUM.SATISTA);
            }

            if (SeciliSiparisDto.SiparisSurecDurum == SIPARISSURECDURUM.SATIS_YONETICIONAYINDA)
            {
                _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto,
                    onaylandi ? SIPARISSURECDURUM.YONETICIONAYINDA : SIPARISSURECDURUM.SATISTA);
            }



            if (SeciliSiparisDto.SiparisSurecDurum == SIPARISSURECDURUM.YONETICIONAYINDA)
            {
                _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto,
                    onaylandi ? SIPARISSURECDURUM.MUSTERIONAYINDA : SIPARISSURECDURUM.SATISTA);
            }

            if (SeciliSiparisDto.SiparisSurecDurum == SIPARISSURECDURUM.MUSTERIONAYINDA)
            {
                _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto,
                    onaylandi ? SIPARISSURECDURUM.MUSTERIONAYLI : SIPARISSURECDURUM.SATISTA);
            }

            SiparisListe.Remove(SeciliSiparisDto);
        }

        private void OnYerlesimKaydet1(object obj)
        {
            ExportService1.SaveLayout(GridLayoutFileName1);
        }

        private void OnYerlesimKaydet2(object obj)
        {
            ExportService2.SaveLayout(GridLayoutFileName2);
        }

        private void PandapMessangerAc(SiparisDTO row)
        {
            AppMesaj.MesajFormAc(row);
        }

        private void PlanlamayaGonder()
        {
            _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto, SIPARISSURECDURUM.PLANLAMADA);
            SiparisListe.Remove(SeciliSiparisDto);
        }

        private void SiparisAc(KayitModu kayitModu, bool? kapasitifMi = null)
        {
            var vm = new SiparisViewModel(false);
            this.KayitModu = kayitModu;

            var title = "";

            if (this.KayitModu == KayitModu.Add)
            {
                vm.SiparisKayitModu = KayitModu.Add;

                var siparis = Siparis.SiparisOlustur("", AppPandap.AktifKullanici.KullaniciId);

                title = "Yeni Sipariş";
                vm.Load(siparis);
            }
            else
            {
                vm.SiparisKayitModu = KayitModu.Edit;

                var sip = _uow.SiparisRepo.SiparisGetirPure(SeciliSiparisDto.SiparisKod);

                title = SeciliSiparisDto.SiparisKod;

                vm.Load(sip);
            }

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SiparisView", vm);
            doc.Title = title;
            doc.Show();
        }

        private void KapasitiftenYeni()
        {

        }


        private void SiparisKapat()
        {
            //if (SeciliSiparisDto.AktifUretimEmriSayisi > 0)
            //{
            //    var mesaj = $"Siparişe ait {SeciliSiparisDto.AktifUretimEmriSayisi}  adet üretim emri bulunmaktadır." +
            //                Environment.NewLine + Environment.NewLine +
            //                "Siparişin kapatılabilmesi için üretim emirlerinin kapatılması gerekmektedir. " +
            //                Environment.NewLine +
            //                "Planlamadan kapatılmasını talep edebilirsiniz";

            //    MessageBoxService.ShowMessage(mesaj, "Pandap", MessageButton.OK, MessageIcon.Information);
            //    return;
            //}

            _uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDto, SIPARISSURECDURUM.KAPALISIPARIS, SeciliAnaMenu);
            SiparisListe.Remove(SeciliSiparisDto);
        }

        private void SiparisSil()
        {
            var sonuc = MessageBoxService.ShowMessage("1 Kayıt Silinecek", "Silinecek", MessageButton.OKCancel,
                MessageIcon.Error);

            if (sonuc == MessageResult.Cancel) return;

            _uow.SiparisRepo.Remove(SeciliSiparisDto);
            SiparisListe.Remove(SeciliSiparisDto);
        }

        private void SiparisYeni(bool kapasitifMi)
        {
            SiparisAc(KayitModu.Add, kapasitifMi);
        }



        //TODO bu kısmı kontrol et
        private void VerileriTazele()
        {
            this.Load(this._planlamaKapatilanlariGoster, ListeSiparisSurecDurum);
        }

        public int KalemKalanIsYuku(SiparisKalemDTO k)
        {
            //return k.Miktar_kg - k.UretimEmirleriDTO_List.Sum(p => p.BobinlerDTO_List.Sum(b => b.Agirlik_kg)).GetValueOrDefault();
            return 0;
        }

        public void SiparisKayitMessageData(Siparis siparis)
        {
            VerileriTazele();

        }

        public ObservableCollection<AlasimMiktarDto> AlasimDurumlari
        {
            get => _alasimDurumlari;
            set => SetProperty(ref _alasimDurumlari, value);
        }

        public bool DataIsLoading
        {
            get => _dataIsLoading;
            set => SetProperty(ref _dataIsLoading, value);
        }

        public string GridLayoutFileName1
        {
            get => _gridLayoutFileName1;
            set => SetProperty(ref _gridLayoutFileName1, value);
        }

        public string GridLayoutFileName2
        {
            get => _gridLayoutFileName2;
            set => SetProperty(ref _gridLayoutFileName2, value);
        }

        public bool KapasitifSiparislerGoster
        {
            get => _kapasitifSiparislerGoster;
            set
            {
                SetProperty(ref _kapasitifSiparislerGoster, value);
                this.Load(PlanlamaKapatılanSiparislerGoster, _listeSiparisSurecDurum);
            }
        }

        public KayitModu KayitModu { get; set; }

        public string ListeSiparisSurecDurum
        {
            get => _listeSiparisSurecDurum;
            set => SetProperty(ref _listeSiparisSurecDurum, value);
        }

        public bool NormalSiparislerGoster
        {
            get => _normalSiparislerGoster;
            set
            {
                SetProperty(ref _normalSiparislerGoster, value);
                Load(false, ListeSiparisSurecDurum);
            }
        }

        public bool PlanlamaKapatılanSiparislerGoster
        {
            get => _planlamaKapatılanSiparislerGoster;
            set => SetProperty(ref _planlamaKapatılanSiparislerGoster, value);
        }

        public bool SadeceAcikSiparisKalemleri
        {
            get => _sadeceAcikSiparisKalemleri;
            set => SetProperty(ref _sadeceAcikSiparisKalemleri, value);
        }

        public bool SadeceOnayliSiparisKalemleri
        {
            get => _sadeceOnayliSiparisKalemleri;
            set => SetProperty(ref _sadeceOnayliSiparisKalemleri, value);
        }

        public string SeciliAnaMenu { get; set; }

        public SiparisDTO SeciliSiparisDto
        {
            get => _seciliSiparisDto;
            set => SetProperty(ref _seciliSiparisDto, value);
        }

        public List<string> SiparisDurumlari
        {
            get => _siparisDurumlari;
            set => SetProperty(ref _siparisDurumlari, value);
        }

        public virtual IDialogService SiparisEditDialogService => ServiceContainer.GetService<IDialogService>("SiparisEditDialogService");

        public ObservableCollection<SiparisDTO> SiparisListe
        {
            get => _siparisListe;
            set
            {
                if (SetProperty(ref _siparisListe, value))
                {
                    SiparisListeVisibleData = SiparisListe.Select(c => c).ToObservableCollection();
                }
            }
        }

        public ObservableCollection<SiparisDTO> SiparisListeVisibleData
        {
            get => _siparisListeVisibleData;
            set => SetProperty(ref _siparisListeVisibleData, value);
        }



        public ObservableCollection<SiparisKalemDTO> SiparisKalemDtoListe
        { get => siparisKalemDtoListe; set => SetProperty(ref siparisKalemDtoListe, value); }
        public List<PandapCari> IlgiliCariListe { get; private set; }

        private ObservableCollection<SiparisKalemDTO> siparisKalemDtoListe;
        private double formYuklenmeSuresi;
        private string aktifYilHafta;
        private int kapasitifSiparisSayisi;
        private int normalSiparisSayisi;
    }
}