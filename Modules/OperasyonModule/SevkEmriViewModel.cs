using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic.Helper;
using mnd.Logic.Model.Operasyon;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.Logic.Services;
using mnd.Logic.Services._DTOs;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;
using mnd.UI.Modules._DialogViews.MusteriSecimDialog;
using mnd.UI.Modules.TeklifModule.MessangerEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.OperasyonModule
{

    public class LimitTalepFormReportModel
    {
        public LimitTalepFormDto LimitSutun1 { get; set; }
        public LimitTalepFormDto LimitSutun2 { get; set; }

        public DateTime IstemTarihi { get; set; }

        public string TalepEdenKisi { get; set; }
        public string TalepEdenBirim { get; set; }
    }
    public class LimitTalepFormDto
    {
        public string CariIsim { get; set; }
        public string UlkeAd { get; set; }
        public string PlasiyerAdi { get; set; }
        public string DovizKod { get; set; }

        public decimal? EximBankLimit { get; set; }

        public decimal? DbsLimit { get; set; }
        public decimal? GarantiFactoringLimit { get; set; }
        public decimal? IngFactoringLimit { get; set; }
        public decimal? BankaTeminati { get; set; }
        public decimal? YoneticiSevkLimit { get; set; }

        public decimal? YoneticiSiparisLimit { get; set; }
        public decimal? AcikHesap { get; set; }
        public decimal? NetsisMusteriCekRiski { get; set; }
        public decimal? NetsisKendiCekRiski { get; set; }
        public decimal? MusteriDepoRiski { get; set; }
        public decimal? UretimRiski { get; set; }

        public decimal? ToplamLimit { get; set; }
        public decimal? ToplamRisk { get; set; }
        public decimal? KullanilabilirLimit { get; set; }

        public decimal TalepEdilenLimit { get; set; }
        public DateTime IstemTarihi { get; set; }
        public decimal? SevkiyatEmirleriDepoRiski { get; set; }
        public decimal AktifSevkEmriTutar { get; set; }
        public decimal YeniSiparisRiski { get; set; }
        public decimal KapasitifMiktar { get; set; }
        public string TalepEdenKisi { get; set; }
        public string TalepEdenBirim { get; set; }
        public decimal? UretilecekOnayliSiparislerTutar { get; set; }

    }
    public class LimitTalepFormModel
    {
        public string CariKod { get; set; }
        public string CariAd { get; set; }
        public decimal KullanilabilirLimit { get; set; }
        public decimal SevkEmriTutar { get; set; }
        public decimal SeciliPaletGenelToplam { get; set; }
        public string DovizTipKod { get; set; }
        public decimal? EximBankLimit { get; set; }
        public decimal? DbsLimit { get; set; }
        public decimal? GarantiFactoringLimit { get; set; }
        public decimal? IngFactoringLimit { get; set; }
        public decimal? BankaTeminati { get; set; }
        public decimal? YoneticiLimit { get; set; }
        public decimal? AcikHesap { get; set; }
        public decimal? NetsisMusteriCekRiski { get; set; }
        public decimal? NetsisKendiCekRiski { get; set; }
        public decimal? MusteriDepoRiski { get; set; }
        public decimal? ToplamLimit { get; set; }
        public decimal? ToplamRisk { get; set; }
        public string CariIsim { get; set; }
        public string UlkeAd { get; set; }
        public string PlasiyerAdi { get; set; }
        public decimal? SevkiyatEmirleriRiski { get; set; }
        public decimal? OncekiKullanilabilirLimit { get; set; }
        public decimal AktifSevkEmriTutar { get; set; }
        public decimal UretimRiskTutar { get; set; }
        public decimal YeniSiparisRiski { get; set; }
    }

    public class UlasimTip
    {
        public string UlasimTipAd { get; set; }
    }

    public class SevkEmriViewModel : MyDxViewModelBase, IForm
    {

        UnitOfWork uowSevkiyat = new UnitOfWork();
        UnitOfWork uowDaraGuncelle = new UnitOfWork();
        public List<string> DovizCinsleri { get; set; } = new List<string> { "", "TL", "EUR", "USD", "GBP" };
        public DelegateCommand<object> MusteriSecCommand => new DelegateCommand<object>(OnMusteriSec, c => true);

        private void OnMusteriSec(object obj)
        {
            MusteriSecView vw = new MusteriSecView();
            MusteriSecVM vm = new MusteriSecVM();

            vw.DataContext = vm;

            Messenger.Default.Register<MusteriSecildiEvent>(this, MusteriSecildi);

            vw.ShowDialog();
        }

        private void MusteriSecildi(MusteriSecildiEvent obj)
        {
            if (obj == null)
            {
                Messenger.Default.Unregister<MusteriSecildiEvent>(this, MusteriSecildi);
                return;
            }

            SeciliSevkiyatEmri.FaturaKesimCariKod = obj?.Musteri.CariKod;


        }


        public bool TumDepoGelsinMi
        {
            get => _tumDepoGetir; set
            {
                _tumDepoGetir = value;

                UnitOfWork uowDepo = new UnitOfWork();

                if (_tumDepoGetir == true)
                {

                    MamulDepoStoklar = uowDepo.PlanlamaRepo.MamulDepoTumunuGetir(lmeYoksaGunluktenAlinsinMi: false).ToObservableCollection();
                }
                else
                    MamulDepoStoklar = uowDepo.PlanlamaRepo.MamulDepoStoklariGetir(lmeYoksaGunluktenAlinsinMi: false).ToObservableCollection();


            }
        }

        public SevkiyatEmri SevkiyatEmri { get; set; }

        public IrsaliyePalet SeciliIrsaliyePalet
        {
            get => _seciliIrsaliyePalet;
            set
            {
                _seciliIrsaliyePalet = value;

                if (_seciliIrsaliyePalet != null)
                {
                    _seciliIrsaliyePalet.PropertyChanged -= _seciliIrsaliyePalet_PropertyChanged;
                    _seciliIrsaliyePalet.PropertyChanged += _seciliIrsaliyePalet_PropertyChanged;
                }

            }
        }

        private void _seciliIrsaliyePalet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SeciliIrsaliyePalet.PaletDara_Kg))
            {
                var palet = uowDaraGuncelle.PlanlamaRepo.PaletGetir(SeciliIrsaliyePalet.PaletId);
                palet.PaletDara_Kg = SeciliIrsaliyePalet.PaletDara_Kg;

                SeciliIrsaliyePalet.PaletBrut_Kg = SeciliIrsaliyePalet.PaletNet_Kg + SeciliIrsaliyePalet.PaletDara_Kg;

                ToplamlariGuncelle();

                uowDaraGuncelle.Commit();

            }
        }

        public DelegateCommand<MamulDepoStokDto> PaletiSeykiyataEkleCommand => new DelegateCommand<MamulDepoStokDto>(PaletiSeykiyataEkle, true);

        public DelegateCommand<IrsaliyePalet> PaletCikarCommand => new DelegateCommand<IrsaliyePalet>(PaletCikar, true);

        public DelegateCommand<CancelEventArgs> KaydetKapatCommand => new DelegateCommand<CancelEventArgs>(KaydetKapat);
        public DelegateCommand<CancelEventArgs> IptalCommand => new DelegateCommand<CancelEventArgs>(Iptal);

        private List<UlasimTip> ulasimTipleri = null;

        public List<UlasimTip> UlasimTipleri
        {
            get
            {
                if (ulasimTipleri == null)
                {
                    ulasimTipleri = new List<UlasimTip>();
                    ulasimTipleri.Add(new UlasimTip { UlasimTipAd = ULASIMTIP.Karayolu });
                    ulasimTipleri.Add(new UlasimTip { UlasimTipAd = ULASIMTIP.Denizyolu });
                }

                return ulasimTipleri;
            }
        }



        UnitOfWork uowRisk = new UnitOfWork();
        private void PaletiSeykiyataEkle(MamulDepoStokDto p)
        {
            if (SeciliSevkiyatEmri.SevkSurecDurum == SEVKSURECKONUM.MUHASEBE)
            {
                MessageBox.Show("Muhasebeye aktarınalan sevk emirlerine palet ekleyemezsiniz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            var depoCariIrsaliye = SeciliSevkiyatEmri.CariIrsaliyeler.Where(c => c.CariKod == p.CariKod).SingleOrDefault();

            if (depoCariIrsaliye == null)
            {
                depoCariIrsaliye = new Logic.Model.Operasyon.Irsaliye();
                depoCariIrsaliye.RowGuid = Guid.NewGuid();
                depoCariIrsaliye.CariKod = p.CariKod;
                depoCariIrsaliye.SevkiyatEmriId = SeciliSevkiyatEmri.SevkiyatEmriId;
                depoCariIrsaliye.CariAd = p.CariIsim;
                depoCariIrsaliye.NetsiseAktarimDurum = IRSALIYEDURUM.BEKLIYOR;

                SeciliSevkiyatEmri.CariIrsaliyeler.Add(depoCariIrsaliye);

                SeciliSevkiyatEmri.FaturaKesimCariKod = p.CariKod;
            }

            var irsPalet = new IrsaliyePalet();

            irsPalet.CariKod = depoCariIrsaliye.CariKod;

            irsPalet.StogaGirisTarih = p.DepoKabulTarihi.GetValueOrDefault();
            irsPalet.PaletId = p.PaletId;
            irsPalet.NetsisStokKod = p.AlasimTipKod + "-" + p.SertlikTipKod;

            irsPalet.FirinNo = p.FirinNo.GetValueOrDefault();
            irsPalet.TavNo = p.TavNo.GetValueOrDefault();
            irsPalet.SehpaNo = p.SehpaNo.GetValueOrDefault();

            irsPalet.UrunKod = p.UrunKod;
            irsPalet.BobinlerBirlesik = p.BobinlerBirlesik;
            irsPalet.Kalinlik = p.Kalinlik_micron.GetValueOrDefault();
            irsPalet.En = p.En_mm.GetValueOrDefault();
            irsPalet.Alasim = p.AlasimTipKod;
            irsPalet.Sertlik = p.SertlikTipKod;
            irsPalet.Ambalaj = "Palet";
            irsPalet.Metraj = p.Metraj;
            irsPalet.RuloDisCap = p.RuloIcCap;
            irsPalet.KartNo = p.KartNo;

            irsPalet.PaletNet_Kg = p.PaletNet_Kg.GetValueOrDefault();
            irsPalet.PaletDara_Kg = p.PaletDara_Kg.GetValueOrDefault();
            irsPalet.PaletBrut_Kg = p.PaletBrut_Kg.GetValueOrDefault();
            irsPalet.PaletEbat = p.PaletEbat;

            irsPalet.SiparisKod = p.SiparisKod;

            irsPalet.FirmaSiparisNo = p.FirmaSiparisNo;
            irsPalet.FirmaUrunNo = p.FirmaUrunNo;
            irsPalet.FiyatKalemKod = p.FiyatKalemKod;
            irsPalet.UretimEmriKod = p.UretimEmriKod;

            irsPalet.Aciklama = p.Aciklama;
            irsPalet.PaletSayisi = 1;
            irsPalet.MasuraSayisi = p.MasuraSayisi;
            irsPalet.BobinlerBirlesik = p.BobinlerBirlesik;


            irsPalet.UrunFaturaAd = "Aluminyum Folyo " + p.GrupKey;
            irsPalet.UrunFaturaAd_YD = "Aluminum Foil Roll " + p.GrupKey;

            irsPalet.LfxKod = p.LfxKod;



            irsPalet.GTip = p.GTip;
            irsPalet.GTipSatirKod = p.GTipSatirKod;

            irsPalet.DovizTipKod = p.DovizTipKod;
            irsPalet.NetsisDovizTipId = p.NetsisDovizTipId;
            irsPalet.NetsisSatisFaturaTipId = p.NetsisSatisFaturaTipId;
            irsPalet.NetsisTeslimTipId = p.NetsisTeslimTipId;


            irsPalet.LmeBF_Ton = p.LmeBF_Ton.GetValueOrDefault();


            irsPalet.IscilikBF_Ton = p.IscilikBF_Ton.GetValueOrDefault();
            irsPalet.KulceBF_Ton = p.KulceBF_Ton.GetValueOrDefault();
            irsPalet.IscilikVadeFarkiOran = p.IscilikVF_Oran;
            irsPalet.KdvOran = (decimal)p.KdvOran.GetValueOrDefault();

            irsPalet.PaletGenelToplamGuncelle();  // palet toplamları 

            depoCariIrsaliye.IrsaliyePaletler.Add(irsPalet);

            MamulDepoStoklar.Remove(p);

            uowSevkiyat.SevkiyatEmirRepo.PaletDurumDegistir(p.PaletId, PALETKONUM.SEVKEMRI);

            ToplamlariGuncelle();


        }

        private void ToplamlariGuncelle()
        {

            foreach (var irs in SeciliSevkiyatEmri.CariIrsaliyeler)
            {
                irs.OnPropertyChanged(nameof(irs.IrsPaletNet_TKg));
                irs.OnPropertyChanged(nameof(irs.IrsPaletDara_TKg));
                irs.OnPropertyChanged(nameof(irs.IrsPaletBrut_TKg));
            }

            SeciliSevkiyatEmri.OnPropertyChanged(nameof(SeciliSevkiyatEmri.KantarFark));

        }

        private void PaletCikar(IrsaliyePalet cariPalet)
        {
            if (SeciliSevkiyatEmri.SevkSurecDurum == SEVKSURECKONUM.MUHASEBE)
            {
                MessageBox.Show("Muhasebeye aktarınalan sevk emirlerinden palet çıkaramazsınız", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }



            Irsaliye seciliIrs = null;

            seciliIrs = cariPalet.CariKod != null ? SeciliSevkiyatEmri.CariIrsaliyeler.First(c => c.CariKod == cariPalet.CariKod) : null;

            if (seciliIrs == null) seciliIrs = SeciliSevkiyatEmri.CariIrsaliyeler.First(c => c.IrsaliyeId == cariPalet.IrsaliyeId);

            seciliIrs.IrsaliyePaletler.Remove(cariPalet);


            if (seciliIrs.IrsaliyePaletler.Count() == 0)
                SeciliSevkiyatEmri.CariIrsaliyeler.Remove(seciliIrs);

            var uowPlan = new UnitOfWork();

            var p = uowPlan.PlanlamaRepo.MamulDepoStokGetir(lmeYoksaGunluktenAlinsinMi: false, cariPalet.PaletId);
            p.SevkiyatEmriId = null;

            uowSevkiyat.SevkiyatEmirRepo.PaletDurumDegistir(p.PaletId, PALETKONUM.DEPO);
            uowSevkiyat.SevkiyatEmirRepo.PaletiSevkiyattanCikar(p.PaletId);

            MamulDepoStoklar.Add(p);

            ToplamlariGuncelle();

        }


        public ObservableCollection<MamulDepoStokDto> MamulDepoStoklar
        { get => _mamulDepoStoklar; set => SetProperty(ref _mamulDepoStoklar, value); }



        public DelegateCommand FormLoadedCommand => new DelegateCommand(FormLoaded);

        private void FormLoaded()
        {
            var uowDepo = new UnitOfWork();
            MamulDepoStoklar = uowDepo.PlanlamaRepo.MamulDepoStoklariGetir(lmeYoksaGunluktenAlinsinMi: false).ToObservableCollection();

        }

        public DelegateCommand<CancelEventArgs> FormClosingCommand => new DelegateCommand<CancelEventArgs>(FormClosing);

        public MessageBoxResult Result { get; set; }


        private void KaydetKapat(CancelEventArgs e)
        {
            var sevkEmriGuncelDurum = uowSevkiyat.SevkiyatEmirRepo.SevkEmriDurumGetir(SeciliSevkiyatEmri.SevkiyatEmriId);
            var irs = SeciliSevkiyatEmri.CariIrsaliyeler.FirstOrDefault();

            try
            {
                irs.UlasimTip = SeciliSevkiyatEmri.UlasimTip;
                irs.KontainerNo = SeciliSevkiyatEmri.KontainerNo;
            }
            catch (Exception)
            {
                ;
            }

            if (sevkEmriGuncelDurum == SEVKSURECKONUM.MUHASEBE)
            {
                MessageBox.Show("Sadece sevk emri üst bilgileri kaydedilecektir.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                uowSevkiyat.Commit();
                FormKapat();
                return;
            }

            try
            {
                uowSevkiyat.Commit();
            }
            catch (Exception)
            {


            }


            if (SeciliSevkiyatEmri == null) return;



            if (SeciliSevkiyatEmri.CariIrsaliyeler.Count == 0)
            {
                Result = MessageBoxResult.OK;
                FormKapat();
                return;
            }

            var sevkiyat_ekli_paletIdler = SeciliSevkiyatEmri.CariIrsaliyeler.SelectMany(ir => ir.IrsaliyePaletler)
                            .Select(ir => ir.PaletId)
                            .ToList<int>();

            SevkiyatService.Paletler_SevkEmriId_Ata(sevkiyat_ekli_paletIdler, SeciliSevkiyatEmri.SevkiyatEmriId, PALETKONUM.AKTIFSEVKEMRI);


            var fatCariKod = SeciliSevkiyatEmri.FaturaKesimCariKod;
            var sipCariKod = SeciliSevkiyatEmri.CariIrsaliyeler.FirstOrDefault().CariKod;

            PandapCari ikinciCari = null;




            PandapCariService.NetsistenAlarakCariGuncelle(fatCariKod);

            PandapCari sipCari = PandapCariService.CariGetir(sipCariKod);
            PandapCari fatCari = PandapCariService.CariGetir(fatCariKod);


            if (fatCariKod != null && fatCariKod.Contains("120-01-")) // Yurt içi
                ikinciCari = PandapCariService
                                       .VergiNumarasindanCariGetir(fatCari.VergiNumarasi)
                                       .Where(c => c.CariKod != fatCariKod)
                                       .FirstOrDefault();



            irs.FaturaKesimCariKod = SeciliSevkiyatEmri.FaturaKesimCariKod;
            SeciliSevkiyatEmri.RiskVarMi = RiskKontrol(sipCari, fatCari, ikinciCari);
            uowSevkiyat.Commit();

            SevkiyatService.SevkiyatEmri_Paletlerin_KonumDegistir(SeciliSevkiyatEmri.SevkiyatEmriId, PALETKONUM.SEVKEMRI);


            Result = MessageBoxResult.OK;

            FormKapat();

        }

        private bool RiskKontrol(PandapCari sipCari, PandapCari fatCari, PandapCari ikinci)
        {
            bool risklimi = false;

            string fatCariKod = fatCari.CariKod;
            string sipCariKod = sipCari.CariKod;

            if (fatCari.EximBitisTarih.HasValue)
            {
                var gun = fatCari.EximBitisTarih.Value - DateTime.Now.Date;
                if (gun.TotalDays <= 10)
                {
                    MessageBox.Show("Exim tarihi " + gun.TotalDays + " gün geçmiş");
                    return true;

                }
            }

            var fat_YeniSiparislerToplamRisk = CariRiskService.SatisYeniKayitFiyatlariGetir(fatCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var fat_UretimRiski = CariRiskService.UretimEmriIsYuku_FiyatGetir(fatCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var fat_DepoRiski = CariRiskService.DepoPaletFiyatGetir(fatCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var fat_BekleyenSevkiyatEmirleriRiski = CariRiskService.SevkiyatEmirleriFiyatGetir(fatCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var fat_AktifSevkEmriTutar = CariRiskService.AktifSevkiyatEmriFiyatGetir(SeciliSevkiyatEmri.SevkiyatEmriId, fatCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;

            var sip_YeniSiparislerToplamRisk = CariRiskService.SatisYeniKayitFiyatlariGetir(sipCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var sip_UretimRiski = CariRiskService.UretimEmriIsYuku_FiyatGetir(sipCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var sip_DepoRiski = CariRiskService.DepoPaletFiyatGetir(sipCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var sip_BekleyenSevkiyatEmirleriRiski = CariRiskService.SevkiyatEmirleriFiyatGetir(sipCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;
            var sip_AktifSevkEmriTutar = CariRiskService.AktifSevkiyatEmriFiyatGetir(SeciliSevkiyatEmri.SevkiyatEmriId, sipCariKod).FirstOrDefault()?.GenelToplamTutar ?? 0;


            fatCari.HesapAlanRiskleriYukle("Operasyon", fat_YeniSiparislerToplamRisk, fat_UretimRiski, fat_DepoRiski, fat_BekleyenSevkiyatEmirleriRiski, fat_AktifSevkEmriTutar, 0);
            sipCari.HesapAlanRiskleriYukle("Operasyon", sip_YeniSiparislerToplamRisk, sip_UretimRiski, sip_DepoRiski, sip_BekleyenSevkiyatEmirleriRiski, sip_AktifSevkEmriTutar, 0);


            var farkliCariKod = fatCari.CariKod != sipCari.CariKod;
            var risk_fark = farkliCariKod ? fatCari.KullanilabilirLimit : sipCari.KullanilabilirLimit;


            if (risk_fark < 0)
            {
                var uyariMetin = "";

                LimitTalepFormReportModel limitRaporModel = new LimitTalepFormReportModel();
                limitRaporModel.TalepEdenKisi = AppPandap.AktifKullanici.AdSoyad;
                limitRaporModel.TalepEdenBirim = "LOJİSTİK";
                limitRaporModel.IstemTarihi = DateTime.Now;

                if (farkliCariKod)
                {
                    uyariMetin = $"LÜTFEN {fatCari.CariIsim} İÇİN {Environment.NewLine} {risk_fark.ToString("n0")}" +
                    $" { DovizHelper.SimgeyeDonustur(fatCari.DovizAd)} LİMİT ONAYI ALINIZ";

                    limitRaporModel.LimitSutun1 = LimitTalepFormDataOlustur(fatCari);
                    limitRaporModel.LimitSutun2 = LimitTalepFormDataOlustur(sipCari);
                }
                else
                {
                    limitRaporModel.LimitSutun1 = LimitTalepFormDataOlustur(sipCari);

                    uyariMetin = $"LÜTFEN {sipCari.CariIsim} İÇİN {Environment.NewLine} {risk_fark.ToString("n0")}" +
                    $" { DovizHelper.SimgeyeDonustur(fatCari.DovizAd)} LİMİT ONAYI ALINIZ";
                }

                if (ikinci != null)
                {
                    limitRaporModel.LimitSutun2 = LimitTalepFormDataOlustur(ikinci);
                }


                MessageBox.Show(uyariMetin, "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);

                LimitTalepFormYazdir(limitRaporModel);

                risklimi = true;

            }

            return risklimi;
        }

        public LimitTalepFormDto LimitTalepFormDataOlustur(PandapCari pCari)
        {

            var riskBilgi = new LimitTalepFormDto
            {

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
                MusteriDepoRiski = pCari.MusteriDepoRiski,
                SevkiyatEmirleriDepoRiski = pCari.MusteriSevkEmirleriRiski,
                AktifSevkEmriTutar = pCari.AktifSevkEmriRiski,
                UretimRiski = pCari.UretimRiski,


                ToplamLimit = pCari.ToplamLimit,
                ToplamRisk = pCari.ToplamRisk,
                KullanilabilirLimit = pCari.KullanilabilirLimit,


                TalepEdilenLimit = pCari.KullanilabilirLimit,


            };

            return riskBilgi;

        }

        public void LimitTalepFormYazdir(LimitTalepFormReportModel limitTalepModel)
        {
            UnitOfWork uowRapor = new UnitOfWork();

            var dsObject = limitTalepModel;

            var raporTanim = uowRapor.RaporTanimRepo.RaporGetirFromId(13);
            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);

            return;
        }



        private void Iptal(CancelEventArgs obj)
        {
            Result = MessageBoxResult.Cancel;
            FormKapat();
        }

        public void FormKapat()
        {
            var currentWindow = ((CurrentWindowService)ServiceContainer.GetService<ICurrentWindowService>()).Window;
            ServiceContainer.GetService<ICurrentWindowService>().Close();
        }

        private MamulDepoStokDto seciliMamulDepoStok;

        public MamulDepoStokDto SeciliMamulDepoStok
        {
            get => seciliMamulDepoStok;
            set => SetProperty(ref seciliMamulDepoStok, value);
        }

        private SevkiyatEmri seciliSevkiyatEmri;
        private ObservableCollection<MamulDepoStokDto> _mamulDepoStoklar;
        private IrsaliyePalet _seciliIrsaliyePalet;
        private bool _tumDepoGetir;

        public SevkiyatEmri SeciliSevkiyatEmri
        {
            get => seciliSevkiyatEmri;
            set
            {
                SetProperty(ref seciliSevkiyatEmri, value);
            }
        }



        public SevkiyatEmri Load(int _seciliSevkiyatEmriId)
        {
            if (_seciliSevkiyatEmriId == 0)
            {
                var yeniSevkEmri = new SevkiyatEmri();
                yeniSevkEmri.RowGuid = Guid.NewGuid();
                yeniSevkEmri.SevkSurecDurum = SEVKSURECKONUM.OPERASYON;
                yeniSevkEmri.Lojistik = AppPandap.AktifKullanici.AdSoyad;
                yeniSevkEmri.AmbarSorumlusu = uowSevkiyat.KullaniciRepo.RoleGoreIlkKullaniciGetir(KULLANICIROLLERI.MAMULDEPO).AdSoyad;
                yeniSevkEmri.Muhasebe = uowSevkiyat.KullaniciRepo.RoleGoreIlkKullaniciGetir(KULLANICIROLLERI.MUHASEBE).AdSoyad;
                yeniSevkEmri.SevkiyatTarihi = DateTime.Now;
                SeciliSevkiyatEmri = yeniSevkEmri;

                uowSevkiyat.SevkiyatEmirRepo.SevkEmriEkle(SeciliSevkiyatEmri);

            }
            else
                SeciliSevkiyatEmri = uowSevkiyat.SevkiyatEmirRepo.SevkiyatEmriGetirFromId(_seciliSevkiyatEmriId);

            return SeciliSevkiyatEmri;
        }

        public void Load()
        {
        }


        private void FormClosing(CancelEventArgs obj)
        {
        }


    }
}