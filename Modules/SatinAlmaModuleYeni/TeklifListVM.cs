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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules.SatinAlmaModuleYeni
{
    public class TeklifListVM : MyDxViewModelBase, IForm
    {


        TalepRepository repo = new TalepRepository();

        private ObservableCollection<SurecTanim> talepSurecItems;



        public string FormMenuAd { get; set; }

        public ObservableCollection<Talep> Teklifler { get => teklifler; set => SetProperty(ref teklifler, value); }


        public DelegateCommand<Talep> SurecOnayCommand => new DelegateCommand<Talep>(OnSurecOnay, true);

        private void OnSurecOnay(Talep obj)
        {
            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();

            if (surec.SurecAdimKod == SATINALMA_SURECDURUM.YONETICI_ONAYINDA)
            {

                var repoYeni = new TalepRepository();
                var seciliTeklif = repoYeni.TalepGetir(SeciliTalep.TalepId);

                var olusacakSiparisler = SatinAlmaService.TekliftenSiparisleriOlustur(seciliTeklif);

                foreach (var sip in olusacakSiparisler)
                {
                    repo.TalepEkle(sip);
                }

                repo.SurecDegistir(obj.TalepId, "ARSIV");
                EkranYenile();
                return;
            }

            var yeniSurec = TalepSurecItems.Where(c => c.AdimSira == surec.OnayAdim).FirstOrDefault();

            repo.SurecDegistir(obj.TalepId, yeniSurec.SurecAdimKod);

            EkranYenile();
        }

        public DelegateCommand<Talep> SurecRedCommand => new DelegateCommand<Talep>(OnSurecRet, true);

        private void OnSurecRet(Talep obj)
        {
            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();

            var yeniSurec = TalepSurecItems.Where(c => c.AdimSira == surec.RetAdim).FirstOrDefault();

            repo.SurecDegistir(obj.TalepId, yeniSurec.SurecAdimKod);

            EkranYenile();
        }

        public Talep SeciliTalep { get => seciliTalep; set => SetProperty(ref seciliTalep, value); }

        public DelegateCommand EkranYenileCommand => new DelegateCommand(EkranYenile, true);

        public DelegateCommand<Talep> TeklifIstekFormYazdirCommand => new DelegateCommand<Talep>(OnTeklifIstekFormYazdir);

        public DelegateCommand<Talep> KararFormGosterCommand => new DelegateCommand<Talep>(OnKararFormuGoster);


        private void OnKararFormuGoster(Talep obj)
        {
            Messenger.Default.Register<YoneticiKararFormKaydedildiEvent>(this, OnKayitEklendi); // karar formu yönetici onayı

            var vm = new KararFormVM(obj.TalepId, SeciliSurec.SurecAdimKod);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("KararFormView", vm);
            doc.DestroyOnClose = true;
            doc.Title = obj.TalepId;
            doc.Show();

        }

        private void OnTeklifIstekFormYazdir(Talep obj)
        {
            RaporTanimRepository repo = new RaporTanimRepository();

            var raporId = 56;

            var raporTanim = repo.RaporGetirFromId(raporId);

            var model = TalepToSiparisServis.TalepToIstekFormDto(SeciliTalep);

            var l = new List<SatinAlma_SiparisDto>();
            l.Add(model);

            var dsObject = l;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }



        AppRepositorySA repoSurec = new AppRepositorySA();
        private ObservableCollection<Talep> teklifler;
        private Talep seciliTalep;
        private bool formGostermeYetkiliMi;

        private void EkranYenile()
        {

            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();

            SurecIslemYetkiliMi = surec.YetkiliRoller.Contains(AppPandap.AktifKullanici.KullaniciRol);


            Teklifler = repo.TalepListesi("Teklif", surec.SurecAdimKod);

            Teklifler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);



            foreach (var item in Teklifler)
            {
                item.TeklifKararFormGorebilirMi = SurecIslemYetkiliMi;
            }


            if (surec.SurecAdimKod == SATINALMA_SURECDURUM.TEKLIF_ISTEME)
            {
                foreach (var item in Teklifler)
                {
                    item.TeklifKararFormGorebilirMi =
                        item.TeklifAtananSatinAlmaPersonel == AppPandap.AktifKullanici.AdSoyad ||
                        AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATINALMA_YONETICI ||
                        AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI;
                }

            }


        }



        public ObservableCollection<SurecTanim> TalepSurecItems { get => talepSurecItems; set => SetProperty(ref talepSurecItems, value); }


        public bool SurecIslemYetkiliMi { get; private set; }
        public SurecTanim SeciliSurec { get; }
        public bool TeklifIstemFormuGorunsunMu { get; }


        public TeklifListVM(string menuFormAd)
        {



            FormMenuAd = menuFormAd;
            TalepSurecItems = repoSurec.SurecListe("SatınAlma");

            EkranYenile();

            SeciliSurec = TalepSurecItems.First(c => c.Adim == FormMenuAd);

            TeklifIstemFormuGorunsunMu = SeciliSurec.SurecAdimKod == SATINALMA_SURECDURUM.TEKLIF_ISTEME;




        }

        private void OnKayitEklendi(YoneticiKararFormKaydedildiEvent obj)
        {
            EkranYenile();

            Messenger.Default.Unregister<YoneticiKararFormKaydedildiEvent>(this, OnKayitEklendi); // karar formu yönetici onayı
        }


        public void Load()
        {

        }
    }
}
