using DevExpress.Mvvm;
using Newtonsoft.Json;
using mnd.Common.Helpers;
using mnd.Common.NetsisModel;
using mnd.Logic.Helper;
using mnd.Logic.Model.Muhasebe;
using mnd.Logic.Model.Operasyon;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.AppModules.SplashScreenModule;
using mnd.UI.Helper;
using mnd.UI.Modules._SatisModule;
using mnd.UI.Modules.OperasyonModule;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mnd.UI.Modules.MuhasebeModule
{
    public class IrsaliyeKalem
    {

        public int IrsaliyeId { get; set; }
        public string CariAd { get; set; }
        public decimal LmeTutar { get; internal set; }
        public string Doviz { get; internal set; }
        public int PaletNet_Kg { get; internal set; }
        public decimal LmeBF { get; internal set; }
        public DateTime? IrsaliyeTarihi { get; internal set; }
        public decimal IscilikTutar { get; internal set; }
        public string PlasiyerAd { get; internal set; }
        public decimal IscilikBF { get; internal set; }
        public decimal BirimFiyat { get; internal set; }
        public decimal ToplamFiyat { get; internal set; }
        public string UlkeAd { get; internal set; }
        public decimal ToplamFiyatSeciliDoviz { get; set; }
        public decimal Parite { get; internal set; }
        public string Alasim { get; internal set; }
        public decimal? Kalinlik { get; internal set; }
        public string KullanimAlani { get; internal set; }
        public decimal? En { get; internal set; }
        public string KalinlikGrup { get; set; }
    }

    public class IrsaliyeListViewModel : MyDxViewModelBase
    {


        public DelegateCommand<object> IrsaliyeDetayGosterCommand => new DelegateCommand<object>(OnIrsaliyeDetayGoster, CanIrsaliyeDetayGoster);
        public virtual IWindowService WinDialogService => ServiceContainer.GetService<IWindowService>();




        private bool CanIrsaliyeDetayGoster(object arg)
        {

            if (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MUHASEBE
                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MUHASEBE_YONETICI
                ) return true;

            return false; ;
        }

        private void OnIrsaliyeDetayGoster(object obj)
        {

            UnitOfWork uow1 = new UnitOfWork();
            IrsaliyeDetayViewModel vm = new IrsaliyeDetayViewModel(SeciliCariIrsaliye.IrsaliyeId);

            WinDialogService.Show(nameof(IrsaliyeDetayView), vm);

            OnEkraniTazele(null);
        }

        public DelegateCommand<string> BekleyenIrsaliyelerCommand => new DelegateCommand<string>(OnBekleyenIrsaliyeleriGetir, true);

        public string SeciliFiltre { get; set; }

        private void OnBekleyenIrsaliyeleriGetir(string obj)
        {
            uow = new UnitOfWork();
            Irsaliyeler = uow.MuhasebeRepo.IrsaliyeleriGetir(IRSALIYEDURUM.BEKLIYOR)
                .OrderByDescending(c => c.IrsaliyeId).ToObservableCollection();
            SeciliFiltre = IRSALIYEDURUM.BEKLIYOR;
        }

        public DelegateCommand<string> AktarilanIrsaliyelerCommand => new DelegateCommand<string>(OnAktarilanIrsaliyeleriGetir, true);

        private void OnAktarilanIrsaliyeleriGetir(string obj)
        {
            uow = new UnitOfWork();
            Irsaliyeler = uow.MuhasebeRepo.IrsaliyeleriGetir(IRSALIYEDURUM.AKTARILDI)
                 .OrderByDescending(c => c.IrsaliyeId).ToObservableCollection();
            SeciliFiltre = IRSALIYEDURUM.AKTARILDI;
        }

        public ObservableCollection<Irsaliye> Irsaliyeler { get => _irsaliyeler; set => SetProperty(ref _irsaliyeler, value); }

        UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<Irsaliye> _irsaliyeler;
        private Irsaliye _seciliCariIrsaliye;

        MuhasebeService muhasebeService = new MuhasebeService();


        public DelegateCommand<object> EkraniTazeleCommand => new DelegateCommand<object>(OnEkraniTazele, true);



        private void OnEkraniTazele(object obj)
        {

            uow = new UnitOfWork();

            Irsaliyeler = uow.MuhasebeRepo.IrsaliyeleriGetir(SeciliFiltre)
                 .OrderByDescending(c => c.IrsaliyeId).ToObservableCollection();


        }





        public Irsaliye SeciliCariIrsaliye
        {
            get { return _seciliCariIrsaliye; }
            set
            {
                if (_seciliCariIrsaliye == value) return;
                _seciliCariIrsaliye = value;

                if (_seciliCariIrsaliye != null)
                {
                    _seciliCariIrsaliye.IrsaliyePaletGruplu = IrsaliyeService.IrsaliyePaletGrupla(_seciliCariIrsaliye);

                    _seciliCariIrsaliye.PropertyChanged -= _seciliIrsaliye_PropertyChanged;
                    _seciliCariIrsaliye.PropertyChanged += _seciliIrsaliye_PropertyChanged;
                }

            }
        }

        public DelegateCommand<object> NetsiseAktarCommand => new DelegateCommand<object>(OnNetsiseAktar, CanNetsiseAktar);

        private bool CanNetsiseAktar(object arg)
        {
            return true;
        }


        public void PaletGTipGuncelle()
        {

            if (_seciliCariIrsaliye.DIIB_No == null) return;
            foreach (var irsPalet in _seciliCariIrsaliye.IrsaliyePaletler)
            {
                var diib = muhasebeService.
                                 DahildeIslemeIzinBelgeleriGetir()
                                 .First(c => c.BelgeNo == SeciliCariIrsaliye.DIIB_No);

                irsPalet.GTipSatirKod = muhasebeService.Diib_SatirKodGetir(diib, irsPalet.Kalinlik.GetValueOrDefault());
                irsPalet.GTip = diib.BelgeAciklama;
                irsPalet.GTipNo = muhasebeService.Diib_GTipNoGetir(diib, irsPalet.Kalinlik.GetValueOrDefault());

            }
            uow.Commit();
        }

        private void _seciliIrsaliye_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            PaletGTipGuncelle();


        }

        public DelegateCommand EFaturaGosterCommand => new DelegateCommand(OnEFaturaGoster, true);

        public DelegateCommand PackingListOlusturCommand => new DelegateCommand(OnPackingListOlustur, true);

        public DelegateCommand CommercialInvoceOlusturCommand => new DelegateCommand(OnCommercialInvoceOlustur, true);

        public List<FinansalGarantor> FinansalGarantorler { get; set; }

        public List<DahildeIslemeIzinBelge> DahildeIslemeIzinBelgeleri { get; set; }

        private void OnCommercialInvoceOlustur()
        {
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(4);

            var uowBanka = new UnitOfWork();

            if (_seciliCariIrsaliye.OdemeBankaKod == null)
            {
                MessageBox.Show("Banka kayıtlı değil", "Pandap", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }


            _seciliCariIrsaliye.BankaRaporCiktiHtml = uowBanka.BankaRepo.BankaGetir(_seciliCariIrsaliye.OdemeBankaKod).BankaRaporCiktiHtml;

            string cukiKaliteMetin = ModuleStrings.CukiKaliteMetin;
            _seciliCariIrsaliye.KaliteMetin = SeciliCariIrsaliye.CariKod == ModuleStrings.CukiCariKod ? cukiKaliteMetin : null;

            var dsObject = SeciliCariIrsaliye;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        private void OnPackingListOlustur()
        {
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(3);

            var dsObject = SeciliCariIrsaliye;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        private void OnEFaturaGoster()
        {
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(11);

            var dsObject = GetNetsisIrsaliye();

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        public DelegateCommand<object> SiparisAcCommand => new DelegateCommand<object>(SiparisiAc, true);

        private void SiparisiAc(object satirKalemKod)
        {
            var siparisKod = satirKalemKod.ToString().Split('/')[0];
            SiparisService.SiparisAc(siparisKod);
        }

        public IrsaliyeListViewModel(string formAdi)
        {
            FinansalGarantorler = muhasebeService.FinansalGarantorleriGetir();
            DahildeIslemeIzinBelgeleri = muhasebeService.DahildeIslemeIzinBelgeleriGetir();
        }

        public void Load()
        {
            uow = new UnitOfWork();

            Irsaliyeler = uow.MuhasebeRepo.IrsaliyeleriGetir(IRSALIYEDURUM.BEKLIYOR);

            SeciliFiltre = IRSALIYEDURUM.BEKLIYOR;

            MuhasebeService muhasebeService = new MuhasebeService();

            FinansalGarantorler = muhasebeService.FinansalGarantorleriGetir();
            DahildeIslemeIzinBelgeleri = muhasebeService.DahildeIslemeIzinBelgeleriGetir();

        }

        private async void OnNetsiseAktar(object obj)
        {
            IrsaliyeService.IrsaliyePaletGrupla(_seciliCariIrsaliye);

            PaletGTipGuncelle();

            var kontrol = SeciliCariIrsaliye.FinansalGarantorAd == "" || SeciliCariIrsaliye.DIIB_No == ""
                || SeciliCariIrsaliye.NetsisIrsaliyeNo == null || SeciliCariIrsaliye.IrsaliyeTarihi==null;

            if (kontrol)
            {
                MessageBox.Show("Gerekli alanları doldurunuz", "Pandap", MessageBoxButton.OK);
                return;
            }

            NetsisIrsaliye netsisIrst = null;

            try
            {
                netsisIrst = GetNetsisIrsaliye();
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
                return;
            }


            var sonuc = await NetsiseKaydet(netsisIrst);

            if (sonuc.Contains("Hata") || sonuc.Contains("yetkisiz"))
            {
                MessageBox.Show(sonuc, "MNDApp", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ----------------------------------------


            SeciliCariIrsaliye.NetsiseAktarimDurum = IRSALIYEDURUM.AKTARILDI;
            SeciliCariIrsaliye.NetsiseAktarimDurumTarih = DateTime.Now;

            uow.Commit();


            var uowPaletUpdate = new UnitOfWork();
            foreach (var item in SeciliCariIrsaliye.IrsaliyePaletler)
            {
                uow.PlanlamaRepo.PaletSevkTarihGuncelle(item.PaletId, DateTime.Now,
                    SeciliCariIrsaliye.SevkiyatEmriId, SeciliCariIrsaliye.NetsisIrsaliyeNo);
            }

            OnBekleyenIrsaliyeleriGetir(null);

            MessageBox.Show("Netsise başarıyla aktarma yapıldı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);


        }

        public NetsisIrsaliye GetNetsisIrsaliye()
        {
            var uow2 = new UnitOfWork();

            var uowSevkEmri = new UnitOfWork();

            var irsaliye = SeciliCariIrsaliye;


            var nakliyeFiyati = uowSevkEmri.SevkiyatEmirRepo.SevkiyatEmriGetirFromId(irsaliye.SevkiyatEmriId).NakliyeFiyati;



            var sipKodlari = SeciliCariIrsaliye.SiparisKodlariBirlesik.Split(';').OrderBy(c => c.ToString()).ToArray();
            var sonSipKod = sipKodlari[sipKodlari.Length - 1];

            var sip_irs = uow2.SiparisRepo.SiparisGetirIrsaliyeIcin(sonSipKod);

            var satisTipKod = sip_irs.SatisTipKod;
            var plasiyerKod = sip_irs.CariKartNavigation.PlasiyerKod;
            var siparisTarih = sip_irs.SiparisTarih.Value.Date;


            if (sip_irs.TakipDovizTipKodNavigation == null)
                throw new Exception($"{sip_irs} nolu sipariş için takip döviz cinsi girili değil.");


            var dovizTipKod = sip_irs.TakipDovizTipKodNavigation.DovizTipKod;
            var dovizTipSimge = sip_irs.TakipDovizTipKodNavigation.Simge;
            var dovizTipNetsisId = sip_irs.TakipDovizTipKodNavigation.NetsisId.Value;


            var teslimTipNetsisId = sip_irs.TeslimTipKodNavigation.NetsisId.Value;
            var satisTipNetsisId = sip_irs.SatisTipKodNavigation.NetsisId.GetValueOrDefault();
            var satisTipOzelKod1 = sip_irs.SatisTipKodNavigation.OzelKod1;


            var KACGUNONCEKIKUR = 1;

            var aktifHaftaGun = DateTime.Now.Date.DayOfWeek;

            if (aktifHaftaGun == DayOfWeek.Monday) KACGUNONCEKIKUR = 3;
            if (aktifHaftaGun == DayOfWeek.Saturday) KACGUNONCEKIKUR = 2;


            var dovizTarihi = DateTime.Now.AddDays(KACGUNONCEKIKUR * -1).Date;

            double dovizKurDeger = 1;

            try
            {
                if (dovizTipKod != "TL")
                    dovizKurDeger = NetsisService.NetsisDovizKuruGetir(dovizTipKod, dovizTarihi);
            }
            catch (Exception ex1)
            {
                throw new Exception($"{dovizTarihi.Date.ToShortDateString()}-{dovizTipKod} için Netsis Döviz Kur Bilgisi Alınamadı.", ex1);
            }

            var netsisDovizKur = dovizKurDeger;


            NetsisIrsaliye netsisIrsaliye = new NetsisIrsaliye();

            netsisIrsaliye.NetsisKullaniciAdi = AppPandap.AktifKullanici.NetsisKullaniciAdi;
            netsisIrsaliye.NetsisParola = AppPandap.AktifKullanici.NetsisParola;

            netsisIrsaliye.Ust = new Ust();
            netsisIrsaliye.FKalemleri = new List<FKalem>();

            netsisIrsaliye.Ust.CariKod = irsaliye.FaturaKesimCariKod == null ? irsaliye.CariKod : irsaliye.FaturaKesimCariKod;
            netsisIrsaliye.Ust.PlasiyerKod = plasiyerKod;
            netsisIrsaliye.Ust.Proje_Kodu = "1";
            netsisIrsaliye.Ust.KDV_DAHILMI = false;

            netsisIrsaliye.Ust.FATIRS_NO = irsaliye.NetsisIrsaliyeNo.PadLeft(15, '0');
            netsisIrsaliye.Ust.EXPORTREFNO = irsaliye.NetsisIrsaliyeNo;

            netsisIrsaliye.Ust.TIPI = Convert.ToInt16(satisTipNetsisId); ;

            if (satisTipKod == "YD")
            {
                netsisIrsaliye.Ust.Aciklama = "İhracat";
                netsisIrsaliye.Ust.TIPI = (int)TFaturaTipi_Pandap.ft_YurtDisi;
                netsisIrsaliye.Ust.EXPORTTYPE = teslimTipNetsisId;
                netsisIrsaliye.Ust.KOD1 = "X";
            }

            if (satisTipKod == "YI")
            {
                netsisIrsaliye.Ust.KOD1 = satisTipOzelKod1;
                netsisIrsaliye.Ust.Aciklama = "S";
                netsisIrsaliye.Ust.KOD1 = "Y";
            }

            if (irsaliye.FaturaKesimCariKod != irsaliye.CariKod && satisTipKod == "YD")
            {
                netsisIrsaliye.Ust.KOD1 = "I";
            };

            var irsaliyeTarihi = SeciliCariIrsaliye.IrsaliyeTarihi == null ? DateTime.Now : SeciliCariIrsaliye.IrsaliyeTarihi.Value;

            netsisIrsaliye.Ust.Tarih = irsaliyeTarihi;
            netsisIrsaliye.Ust.SIPARIS_TEST = irsaliyeTarihi;
            netsisIrsaliye.Ust.FiiliTarih = irsaliyeTarihi;
            netsisIrsaliye.Ust.FIYATTARIHI = irsaliyeTarihi;

            netsisIrsaliye.Ust.KOD2 = "G";


            int i = 0;
            foreach (var k in irsaliye.IrsaliyePaletGruplu)
            {
                i = i + 1;

                var fkalem = new FKalem();

                fkalem._KdvOran = (double)k.KdvOran;
                fkalem._KdvTutar = (double)k.PaletKdvTutar;
                fkalem._GTip = k.GTip;
                fkalem._GtipSatirKod = k.GTipSatirKod;
                fkalem._GTipNo = k.GTipNo;

                fkalem._UrunFaturaAd = k.UrunFaturaAd;
                fkalem._KapOlculeri = k.Kalinlik + "x" + k.En;
                fkalem._KapCinsi = "Pallet";
                fkalem._KapAdet = k.PaletSayisi;
                fkalem._GonderilmeSekli = irsaliye.UlasimTip;

                fkalem._OdemeYeri = "   ";  // kontrol et


                fkalem._Lme = k.LmeBF_Ton;
                fkalem._Iscilik = k.IscilikBF_Ton;
                fkalem._DovizYipSimge = dovizTipSimge;

                fkalem._SatirId = i;


                fkalem.NetsisStokKod = k.NetsisStokKod;
                fkalem.STra_GCMIK = k.PaletNet_Kg;
                fkalem.Olcubr = 1;
                fkalem.Stra_FiyatBirimi = 1;

                if (dovizTipNetsisId == 0) //yerli
                {
                    fkalem.STra_BF = (double)k.BirimFiyat_Kg;
                    fkalem.STra_NF = (double)k.PaletToplamTutar;
                }
                else
                {
                    fkalem.STra_DOVTIP = dovizTipNetsisId;

                    fkalem.STra_DOVFIAT = (double)k.BirimFiyat_Kg;  // doviz birim fiyat
                    fkalem.STra_BF = (double)k.BirimFiyat_Kg * netsisDovizKur;
                }


                fkalem.Stra_FiiliTar = siparisTarih;
                fkalem.STra_testar = siparisTarih;
                fkalem.DEPO_KODU = 20;

                fkalem.SatirBaziAcik1 = k.Kalinlik.Value.ToString("N2");
                fkalem.SatirBaziAcik2 = k.En.Value.ToString("N2");

                fkalem.SatirBaziAcik10 = irsaliye.IrsaliyeId;

                netsisIrsaliye.FKalemleri.Add(fkalem);
            }



            var lmeIscilikMetin = netsisIrsaliye.FKalemleri.GroupBy(c => new { c._Lme, c._Iscilik })
                        .Select(c => new { LmeIscilikMetin = "LME:" + c.Key._Lme.ToString("n2") + " + " + c.Key._Iscilik.ToString("n0"), SatirMetin = string.Join(",", c.Select(l => l._SatirId)) })
                        .ToArray();

            var gtip_ler = netsisIrsaliye.FKalemleri.GroupBy(c => new { c._GTip })
                        .Select(c => new { c.Key._GTip, SatirMetin = string.Join(",", c.Select(l => l._SatirId)) })
                        .ToArray();

            var gtip_satirkod_lar = netsisIrsaliye.FKalemleri.GroupBy(c => new { c._GtipSatirKod })
                    .Select(c => new { c.Key._GtipSatirKod, SatirMetin = string.Join(",", c.Select(l => l._SatirId)) })
                    .ToArray();


            var gtip_no_lar = netsisIrsaliye.FKalemleri.GroupBy(c => new { c._GTipNo })
                  .Select(c => new { c.Key._GTipNo, SatirMetin = string.Join(",", c.Select(l => l._SatirId)) })
                  .ToArray();

            netsisIrsaliye.Ust.EKACK1 = $"({dovizTipSimge}) " + string.Join(", ", lmeIscilikMetin.Select(d => $"({d.SatirMetin}" + "." + d.LmeIscilikMetin + ")"));

            netsisIrsaliye.Ust.EKACK7 = irsaliye.FinansalGarantorAd;

            netsisIrsaliye.Ust.EKACK8 = satisTipKod == "YD" ? string.Join(", ", gtip_ler.Select(d => d.SatirMetin + "->" + d._GTip)) : "";

            netsisIrsaliye.Ust.EKACK9 = satisTipKod == "YD" ? irsaliye.TeslimSekli : ""; //m1

            netsisIrsaliye.Ust.EKACK10 = satisTipKod == "YD" ? ""
                                        + "FOB:" + (irsaliye.IrsaliyeToplamTutar - nakliyeFiyati.GetValueOrDefault()).ToString("n2") + dovizTipSimge
                                        + " NET:" + irsaliye.IrsaliyePaletler.Sum(c => c.PaletNet_Kg).ToString("n0") + "kg"
                                        + " GROSS :" + irsaliye.IrsaliyePaletler.Sum(c => c.PaletBrut_Kg).ToString("n0") + "kg"
                                        : "";

            netsisIrsaliye.Ust.EKACK11 = satisTipKod == "YD" ? "PAYMENT TERMS :" + irsaliye.OdemeSekli : "ÖDEME TARİHİ :" + irsaliye.OdemeSekli;

            netsisIrsaliye.Ust.EKACK12 = satisTipKod == "YD" ? "TOTAL NUMBERS OF CONTAINER : " + irsaliye.IrsaliyePaletler.Count.ToString() + " PALLETS COUNTRY OF ORIGIN: TURKEY" : "";
            netsisIrsaliye.Ust.EKACK13 = "BANK :" + irsaliye.OdemeBankaAd + " BRANCH: " + irsaliye.OdemeBankaSubeAd;
            netsisIrsaliye.Ust.EKACK14 = "IBAN CODE:" + irsaliye.OdemeBankaIBAN;

            netsisIrsaliye.Ust.EKACK15 = satisTipKod == "YD" ? "SATIR KODU:" + string.Join(", ", gtip_satirkod_lar.Select(d => d.SatirMetin + "->" + d._GtipSatirKod)) : "";

            netsisIrsaliye.Ust.EKACK16 = satisTipKod == "YD" ? "GTIP NO:" + string.Join(", ", gtip_no_lar.Select(d => d.SatirMetin + "->" + d._GTipNo)) : "";



            return netsisIrsaliye;
        }

        private async Task<string> NetsiseKaydet(NetsisIrsaliye netsisIrsaliye)
        {

            //HttpClient client_test = new HttpClient();
            //client_test.BaseAddress = new Uri(AppPandap.WebApiNetsisPath);
            //var response1 = await client_test.GetStringAsync("api/netsis");


            var sonuc = "";

            SplashScreenHelper.Instance.ShowLoadingScreen();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            
            client.BaseAddress = new Uri(AppPandap.WebApiNetsisPath);
           

            var netsisobj_str = JsonConvert.SerializeObject(netsisIrsaliye).ToString();

            try
            {
                var response = await client.PostAsync("api/netsis",
                       new StringContent(netsisobj_str, Encoding.UTF8, "application/json"));

                sonuc = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                sonuc = "Hata:" + "Sunucuya bağlanılamadı" + Environment.NewLine + ex.Message;
            }


            SplashScreenHelper.Instance.HideSplashScreen();

            return sonuc;
        }



    }
}
