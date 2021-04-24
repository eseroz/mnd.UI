using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_App;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_SatinAlmaYeni;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.TalepViews
{
    public class TalepListVM : MyDxViewModelBase, IForm
    {


        TalepRepository repo = new TalepRepository();

        private ObservableCollection<Talep> _talepler;
        private ObservableCollection<SurecTanim> talepSurecItems;
        private bool surecIslemYetkiliMi;
        private bool surecIslemYetkiliMi1;

        public string FormMenuAd { get; set; }

        public ObservableCollection<Talep> Talepler { get => _talepler; set => SetProperty(ref _talepler, value); }

        public DelegateCommand YeniCommand => new DelegateCommand(OnYeniTalep, true);

        public DelegateCommand<TalepKalem> KalemHeaderCheckedCommand => new DelegateCommand<TalepKalem>(OnKalemHeaderChecked, true);
        public DelegateCommand<TalepKalem> KalemHeaderUnCheckedCommand => new DelegateCommand<TalepKalem>(OnKalemHeaderUnChecked, true);


        private void OnKalemHeaderChecked(TalepKalem obj)
        {
            var talep = Talepler.First(c => c.TalepId == obj.TalepId);

            foreach (var item in talep.TalepKalemler)
            {
                if (item.TeklifeAktarilmaTarihi == null)
                    item.SeciliMi = true;
            }
        }

        private void OnKalemHeaderUnChecked(TalepKalem obj)
        {

            var talep = Talepler.First(c => c.TalepId == obj.TalepId);
                
            foreach (var item in talep.TalepKalemler)
            {
                if (item.TeklifeAktarilmaTarihi == null)
                    item.SeciliMi = false;
            }

        }

        public bool TeklifFormuOlusturulabilirMi
        {
            get => teklifFormuOlusturulabilirMi;
            set => SetProperty(ref teklifFormuOlusturulabilirMi, value);
        }


        public DelegateCommand<Talep> SurecOnayCommand => new DelegateCommand<Talep>(OnSurecOnay, true);


        public bool IslemSutunAktifMi { get; set; }

        private void OnSurecOnay(Talep obj)
        {
            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();


            var yeniSurec = TalepSurecItems.Where(c => c.AdimSira == surec.OnayAdim).FirstOrDefault();

            repo.SurecDegistir(obj.TalepId, yeniSurec.SurecAdimKod);

            EkranYenile();
        }

        public DelegateCommand<Talep> SurecRedCommand => new DelegateCommand<Talep>(OnSurecRet, true);

        private void OnSurecRet(Talep obj)
        {
            if(obj.TalepSurecKonum== SATINALMA_SURECDURUM.TALEPKAYIT)
            {
                repo.TalepSil(obj);
                EkranYenile();
                return;
            }


            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();

            var yeniSurec = TalepSurecItems.Where(c => c.AdimSira == surec.RetAdim).FirstOrDefault();

            repo.SurecDegistir(obj.TalepId, yeniSurec.SurecAdimKod);

            EkranYenile();
        }

        public Talep SeciliTalep
        {
            get => seciliTalep;
            set
            {
                SetProperty(ref seciliTalep, value);

                if (seciliTalep == null) return;

                seciliTalep.PropertyChanged -= SeciliTalep_PropertyChanged;
                seciliTalep.PropertyChanged += SeciliTalep_PropertyChanged;
            }
        }

        private void SeciliTalep_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var t = sender as Talep;
           
          
        }

        public DelegateCommand EkranYenileCommand => new DelegateCommand(EkranYenile, true);

        public DelegateCommand<string> TeklifOlusturCommand => new DelegateCommand<string>(OnTalepAta, CanTalepAta);

        private bool CanTalepAta(string arg)
        {
            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();

            if (surec.YetkiliRoller.Contains(AppPandap.AktifKullanici.KullaniciRol)) return true;

            return false;
        }

        public List<Kullanici> SatinAlmaPersonelListe { get; set; }

       
        private void OnTalepAta(string satinAlmaPersonelAdSoyad)
        {
            var seciliKalemler0 = Talepler
                .SelectMany(fisDTO => fisDTO.TalepKalemler,
                 (fis, kalem) => new { fis, kalem })
                 .Select(s => new TalepKalem
                 {
                     TalepKalemId = s.kalem.TalepKalemId,
                     KalemTip = "TeklifKalem",
                     TalepId = s.fis.TalepId,
                     IlkTalepId = s.fis.TalepId,
                     IstenilenTarih = s.kalem.IstenilenTarih,
                     StokKod = s.kalem.StokKod,
                     StokAd = s.kalem.StokAd,
                     Miktar = s.kalem.Miktar,
                     TercihMarkaModel = s.kalem.TercihMarkaModel,
                     Birim = s.kalem.Birim,
                     Aciklama = s.kalem.Aciklama,
                     TeklifeAktarilmaTarihi = s.kalem.TeklifeAktarilmaTarihi,

                     AktarilanTeklifId = s.kalem.AktarilanTeklifId,
                     StokGrupKod = s.kalem.StokGrupKod,
                     StokGrupAd = s.fis.StokGrupAd,

                     RowGuid = s.kalem.RowGuid,
                     OkunmamisMesajSayisi = s.kalem.OkunmamisMesajSayisi,
                     SeciliMi = s.kalem.SeciliMi,

                     MesajSayisi = s.kalem.MesajSayisi
                 })
                 .ToList();

            var seciliKalemler= seciliKalemler0
                             .Where(c => c.SeciliMi == true && c.TeklifeAktarilmaTarihi==null)
                             .ToList();

            if (seciliKalemler.Count == 0)
            {
                MessageBox.Show("Teklife dahil edilecek kalemleri seçiniz");
                return;
            }

            TalepRepository repo = new TalepRepository();

            Talep talepTeklif = new Talep();
            talepTeklif.TalepTarihi = DateTime.Now;
            talepTeklif.Tip = "Teklif";
            talepTeklif.TalepSurecKonum = SATINALMA_SURECDURUM.TEKLIF_ISTEME;
            talepTeklif.RowGuid = Guid.NewGuid();
            talepTeklif.TeklifAtananSatinAlmaPersonel = satinAlmaPersonelAdSoyad;
           

            talepTeklif.TalepKalemler = new ObservableCollection<TalepKalem>();

            int i = 0;
            foreach (var item in seciliKalemler)
            {
                item.TeklifeAktarilmaTarihi = DateTime.Now;

                i++;
                var tkfKalem = new TalepKalem();
                tkfKalem.StokKod = item.StokKod;
                tkfKalem.StokAd = item.StokAd;
                tkfKalem.TercihMarkaModel = item.TercihMarkaModel;
                tkfKalem.Aciklama = item.Aciklama;
                tkfKalem.IstenilenTarih = item.IstenilenTarih;
                tkfKalem.Miktar = item.Miktar;
                tkfKalem.RowGuid = Guid.NewGuid();
                tkfKalem.Birim = item.Birim;
                tkfKalem.StokGrupKod = item.StokGrupKod;

                tkfKalem.IlkTalepId = item.IlkTalepId;
                tkfKalem.IsMerkeziAd = item.IsMerkeziAd;
                tkfKalem.KdvOran = item.KdvOran;

                tkfKalem.SiraNo = i;

                talepTeklif.TalepKalemler.Add(tkfKalem);
            }

            var talepId_liste = seciliKalemler.Select(c => c.TalepId).Distinct();

            List<Talep> KalemTalepleri = new List<Talep>();

            foreach (var id in talepId_liste)
            {
                var talep = Talepler.First(c => c.TalepId == id);
                KalemTalepleri.Add(talep);
            }

            talepTeklif.IsMerkeziAd = String.Join(";", KalemTalepleri.Select(c => c.IsMerkeziAd).Distinct());
            talepTeklif.TalepEdenAdSoyad = String.Join(";", KalemTalepleri.Select(c => c.TalepEdenAdSoyad).Distinct());
            talepTeklif.StokGrupAd = String.Join(";", KalemTalepleri.Select(c => c.StokGrupAd).Distinct());

            repo.TalepEkle(talepTeklif);
            repo.Kaydet();



            SatinAlmaDataServices.KalemTeklifeAktarilmaDurumGuncelle(seciliKalemler);

            foreach (var item in Talepler)
            {
                var teklifeDonusenKalemSayisi = item.TalepKalemler.Count(c => c.SeciliMi == true);
                if (item.TalepKalemler.Count == teklifeDonusenKalemSayisi)
                {
                    repo.SurecDegistir(item.TalepId, "TALEP_ALINDI");
                }
            }

            MessageBox.Show(talepTeklif.TalepId.ToString() + " Nolu teklif oluşturuldu");

            EkranYenile();
        }


        public bool SurecIslemYetkiliMi { get => surecIslemYetkiliMi1; set => SetProperty(ref surecIslemYetkiliMi1, value); }


        AppRepositorySA repoSurec = new AppRepositorySA();
        private Talep seciliTalep;


        private bool teklifFormuOlusturulabilirMi;


        private void EkranYenile()
        {

            var surec = TalepSurecItems.Where(c => c.Adim == FormMenuAd).FirstOrDefault();

            SurecIslemYetkiliMi = surec.YetkiliRoller.Contains(AppPandap.AktifKullanici.KullaniciRol);

            repo = new TalepRepository();
            Talepler = repo.TalepListesi("Talep", surec.SurecAdimKod);

            Talepler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
        }



        public ObservableCollection<SurecTanim> TalepSurecItems { get => talepSurecItems; set => SetProperty(ref talepSurecItems, value); }
        public DelegateCommand<Talep> DuzenleCommand => new DelegateCommand<Talep>(OnDuzenle, true);

        private void OnDuzenle(Talep obj)
        {
            if (obj.Tip == "Talep")
            {
                var vm = new TalepEditVM(obj.TalepId);

                var doc = AppPandap.pDocumentManagerService.CreateDocument("TalepEditView", vm);
                doc.DestroyOnClose = true;
                doc.Title = obj.TalepId;
                doc.Show();
            }



        }

        private void OnYeniTalep()
        {
            var vm = new TalepEditVM(0);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("TalepEditView", vm);
            doc.Title = "Yeni Talep";
            doc.Show();

        }


        public TalepListVM(string menuFormAd)
        {

            FormMenuAd = menuFormAd;
            TalepSurecItems = repoSurec.SurecListe("SatınAlma");

            EkranYenile();

            var seciliSurec = TalepSurecItems.First(c => c.Adim == FormMenuAd);

            TeklifFormuOlusturulabilirMi = seciliSurec.SurecAdimKod == SATINALMA_SURECDURUM.TALEP_ATAMA;

            IslemSutunAktifMi = !TeklifFormuOlusturulabilirMi;

            SatinAlmaPersonelListe = KullaniciDataServices.KullaniciGetirFromBirim("Satın Alma");

            Messenger.Default.Register<KayitEklendiEvent<Talep>>(this, OnKayitEklendi);
        }

        private void OnKayitEklendi(KayitEklendiEvent<Talep> obj)
        {
            EkranYenile();
        }

        public void Load()
        {

        }
    }
}
