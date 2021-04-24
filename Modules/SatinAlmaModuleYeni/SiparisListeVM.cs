using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.Persistence.Repositories;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules.SatinAlmaModuleYeni.RaporCikti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.SatinAlmaModuleYeni
{
    public class IrsaliyeCari
    {
        public string CariKod { get; set; }
        public string CariIsim { get; set; }



    }

    public class IrsaliyeUstBilgi
    {
        public string IrsaliyeNo { get; set; }
        public DateTime? IrsaliyeTarihi { get; set; }
        public string CariKod { get; set; }

    }

    public class SiparisListeVM : MyDxViewModelBase, IForm
    {


        TalepRepository talep_repo = new TalepRepository();


        public ObservableCollection<Talep> Siparisler { get => siparisler; set => SetProperty(ref siparisler, value); }

        public bool IrsaliyeModuAktifMi { get => ırsaliyeOlusturAktifMi; set => SetProperty(ref ırsaliyeOlusturAktifMi, value); }

        public bool ListeModuAktifMi { get => listeModuAktifMi; set => SetProperty(ref listeModuAktifMi, value); }

        public DelegateCommand<Talep> SiparisiBaslatCommand => new DelegateCommand<Talep>(OnSiparisiBaslat, CanSiparisiBaslat);




        private bool CanSiparisiBaslat(Talep siparis)
        {
            if (siparis == null) return false;

            return
                siparis.TalepSurecKonum == SATINALMA_SURECDURUM.ONAYLANAN_SIPARISLER &&
                (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATINALMA ||
                AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATINALMA_YONETICI ||
                AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI);
        }

        public DelegateCommand IrsaliyeHazirlaCommand => new DelegateCommand(OnIrsaliyeHazirlaFormShow, true);

        public DelegateCommand IrsaliyeHazirlaOkCommand => new DelegateCommand(OnIrsaliyeHazirlaOk, true);

        public bool IslemSutunAktifMi { get; set; }

        public DelegateCommand<TalepKalem> CheckedCommand => new DelegateCommand<TalepKalem>(OnSecimYapildi, true);

        private void OnSecimYapildi(TalepKalem obj)
        {
            var seciliFisKalemler = Siparisler
                .SelectMany(fisDTO => fisDTO.TalepKalemler,
                 (fis, kalem) => new { fis, kalem })
                 .Where(c => c.kalem.SeciliMi == true)
                 .ToList();

            SeciliKayitSayisi = seciliFisKalemler.Count();
        }

        public DelegateCommand IrsaliyeHazirlaCancelCommand => new DelegateCommand(OnIrsaliyeHazirlaCancel, true);

        public int SeciliKayitSayisi { get => seciliKayitSayisi; set => SetProperty(ref seciliKayitSayisi, value); }

        private void OnIrsaliyeHazirlaCancel()
        {
            IrsaliyePopupOpen = false;

            ListeModuAktifMi = true;
            IrsaliyeModuAktifMi = false;


        }

        private void OnIrsaliyeHazirlaOk()
        {
            IrsaliyePopupOpen = false;

            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();
            Siparisler = talep_repo.CariSiparisListesi(IrsaliyeUstBilgi.CariKod, surec.SurecAdimKod);


            IrsaliyeModuAktifMi = true;
            ListeModuAktifMi = false;
        }

        public DelegateCommand IrsaliyeOlusturCommand => new DelegateCommand(OnIrsaliyeOlustur, true);

        public DelegateCommand IrsaliyeVazgecCommand => new DelegateCommand(OnIrsaliyeVazgec, true);

        public IrsaliyeUstBilgi IrsaliyeUstBilgi { get => ırsaliyeUstBilgi; set => SetProperty(ref ırsaliyeUstBilgi, value); }

        public DelegateCommand<Talep> SiparisTamamlandiCommand => new DelegateCommand<Talep>(OnSiparisiTamamla, CanSiparisTamamla);


        public DelegateCommand<Talep> KararFormGosterCommand => new DelegateCommand<Talep>(OnKararFormuGoster);


        private void OnKararFormuGoster(Talep siparis)
        {

            var vm = new KararFormVM(siparis.TeklifNo.GetValueOrDefault(), "sadeceokunur");

            var doc = AppPandap.pDocumentManagerService.CreateDocument("KararFormView", vm);
            doc.DestroyOnClose = true;
            doc.Title = siparis.TalepId;
            doc.Show();

        }


        private bool CanSiparisTamamla(Talep siparis)
        {
            return
              siparis?.TalepSurecKonum == SATINALMA_SURECDURUM.VERILEN_SIPARISLER &&
              (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.HDEPO_SORUMLUSU ||
              AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.HDEPO_UZM_USER ||
              AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI);
        }

        private void OnSiparisiTamamla(Talep obj)
        {
            talep_repo.SurecDegistir(obj.TalepId, SATINALMA_SURECDURUM.IRSALIYE_OLUSTURULANLAR);
            EkranYenile();
        }

        public bool IrsaliyePopupOpen
        {
            get => ırsaliyePopupOpen; set
            {
                SetProperty(ref ırsaliyePopupOpen, value);

            }
        }


        private void OnIrsaliyeVazgec()
        {
            EkranYenile();

            ListeModuAktifMi = true;
            IrsaliyeModuAktifMi = false;
        }

        public List<IrsaliyeCari> BekleyenSiparisCarileri
        {
            get => bekleyenSiparisCarileri;
            set => SetProperty(ref bekleyenSiparisCarileri, value);
        }




        private void OnIrsaliyeHazirlaFormShow()
        {
            IrsaliyePopupOpen = true;
            IrsaliyeUstBilgi = new IrsaliyeUstBilgi();

        }

        public TalepKalem SeciliTalepKalem
        {
            get => seciliTalepKalem;
            set
            {
                seciliTalepKalem = value;

            }
        }

        private void OnIrsaliyeOlustur()
        {
            var cevap = MessageBox.Show("İrsaliye oluşturulacak onaylıyor musunuz", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (cevap == MessageBoxResult.Cancel) return;


            var seciliFisKalemler = Siparisler
                   .SelectMany(fisDTO => fisDTO.TalepKalemler,
                    (fis, kalem) => new { fis, kalem })
                    .Where(c => c.kalem.SeciliMi == true)
                    .ToList();

            if (seciliFisKalemler.Count == 0)
            {
                MessageBox.Show("Teklife dahil edilecek kalemleri seçiniz");
                return;
            }

            // irsaliye aktarma işleri

            //DepoRepository repoNetsisAktarma = new DepoRepository();
            //DepoCikisFisDTO irsaliyeFis = new DepoCikisFisDTO();

            //irsaliyeFis.FisNo = IrsaliyeUstBilgi.IrsaliyeNo;
            //irsaliyeFis.FisTarihi = IrsaliyeUstBilgi.IrsaliyeTarihi.GetValueOrDefault();

            //irsaliyeFis.CariKod = seciliFisKalemler.First().fis.OnaylananFirmaKod;

            foreach (var fis_kalem in seciliFisKalemler)
            {
                var kalem = new DepoCikisFisKalemDTO();
                kalem.GirisMiktar = fis_kalem.kalem.SiparisGelenMiktar.GetValueOrDefault();
                kalem.StokKodu = fis_kalem.kalem.StokKod;
                kalem.StokAd = fis_kalem.kalem.StokAd;
                kalem.MasrafMerkeziKod = 0;
                kalem.MasrafMerkeziAd = fis_kalem.kalem.IsMerkeziAd;
                kalem.KdvOran = fis_kalem.kalem.KdvOran;



                //irsaliyeFis.KalemlerDTO.Add(kalem);
            }

            //repoNetsisAktarma.NetsisDepoGirisFisiEkle(irsaliyeFis);

            //repoNetsisAktarma.Kaydet();

            //foreach (var fis_kalem in seciliFisKalemler)
            //{
            //    fis_kalem.kalem.IrsaliyeNo = IrsaliyeUstBilgi.IrsaliyeNo;
            //    fis_kalem.kalem.IrsaliyeTarihi = IrsaliyeUstBilgi.IrsaliyeTarihi;
            //}

            talep_repo.Kaydet();


            MessageBox.Show("Kayıt işlemi tamamlandı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);

            IrsaliyeModuAktifMi = false;
            ListeModuAktifMi = true;


        }

        private void OnSiparisiBaslat(Talep obj)
        {
            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();
            var yeniSurec = TalepSurecItems.Where(c => c.AdimSira == surec.OnayAdim).FirstOrDefault();

            if (surec.SurecAdimKod == SATINALMA_SURECDURUM.ONAYLANAN_SIPARISLER)
            {
                talep_repo.SurecDegistir(obj.TalepId, SATINALMA_SURECDURUM.VERILEN_SIPARISLER);
                talep_repo.SiparisTarihAta(obj.TalepId, DateTime.Now);
            }


            EkranYenile();
        }

        public Talep SeciliTalep { get => seciliTalep; set => SetProperty(ref seciliTalep, value); }

        public DelegateCommand EkranYenileCommand => new DelegateCommand(EkranYenile, true);

        public DelegateCommand<Talep> SiparisFormYazdirCommand => new DelegateCommand<Talep>(OnSiparisFormYazdir);


        private void OnSiparisFormYazdir(Talep obj)
        {
            RaporTanimRepository repo = new RaporTanimRepository();

            var raporId = 58;

            var raporTanim = repo.RaporGetirFromId(raporId);

            var model = TalepToSiparisServis.TalepToSiparisDto(SeciliTalep);

            var l = new List<SatinAlma_SiparisDto>();
            l.Add(model);

            var dsObject = l;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }


        public bool SurecIslemYetkiliMi { get => surecIslemYetkiliMi; set => SetProperty(ref surecIslemYetkiliMi, value); }


        AppRepositorySA repoSurec = new AppRepositorySA();

        private ObservableCollection<Talep> siparisler;

        private bool surecIslemYetkiliMi;
        private ObservableCollection<SurecTanim> talepSurecItems;
        private Talep seciliTalep;
        private List<IrsaliyeCari> bekleyenSiparisCarileri;
        private bool ırsaliyeOlusturAktifMi;
        private bool listeModuAktifMi;
        private IrsaliyeUstBilgi ırsaliyeUstBilgi;
        private bool ırsaliyePopupOpen;
        private int seciliKayitSayisi;
        private TalepKalem seciliTalepKalem;

        private void EkranYenile()
        {

            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();


            SurecIslemYetkiliMi = surec.YetkiliRoller.Contains(AppPandap.AktifKullanici.KullaniciRol);


            Siparisler = talep_repo.TalepListesi("Sipariş", surec.SurecAdimKod);

            Siparisler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            BekleyenSiparisCarileri = Siparisler
                .Select(c => new IrsaliyeCari { CariKod = c.OnaylananFirmaKod, CariIsim = c.OnaylananFirmaAd })
                .ToList()
                .GroupBy(o => new { o.CariKod, o.CariIsim })
                .Select(c => new IrsaliyeCari { CariKod = c.Key.CariKod, CariIsim = c.Key.CariIsim })
                .ToList();

        }



        public ObservableCollection<SurecTanim> TalepSurecItems { get => talepSurecItems; set => SetProperty(ref talepSurecItems, value); }
        public bool SiparisFormGorunsunMu { get; }

        public SiparisListeVM(string menuFormAd)
        {

            FormMenuAd = menuFormAd;
            TalepSurecItems = repoSurec.SurecListe("SatınAlma");

            var seciliSurec = TalepSurecItems.First(c => c.Adim == FormMenuAd);

            SiparisFormGorunsunMu = true;

            ListeModuAktifMi = seciliSurec.SurecAdimKod == SATINALMA_SURECDURUM.VERILEN_SIPARISLER;

            EkranYenile();

        }

        public void Load()
        {

        }
    }
}
