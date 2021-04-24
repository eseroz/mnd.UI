using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_SatinAlmaYeni;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.Helper;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules._DialogViews.IsMerkeziSecDialog;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.TalepViews
{
    public class TalepEditVM : MyDxViewModelBase, IDocumentViewModel
    {
        TalepRepository repo = new TalepRepository();
        PersonelRepository repoPersonel = new PersonelRepository();

        private Talep seciliTalep;
        private ObservableCollection<IsMerkezi> ısMerkezleriSeviye2;

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet, true);

        public DelegateCommand YeniKalemCommand => new DelegateCommand(OnYeniKalem, true);

       

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, true);

        public DelegateCommand IsMerkeziSecCommand => new DelegateCommand(OnIsMerkeziSec, true);

        public DelegateCommand<TalepKalem> KalemSilCommand => new DelegateCommand<TalepKalem>(OnTalepKalemSil, true);

        private void OnTalepKalemSil(TalepKalem obj)
        {
            SeciliTalep.TalepKalemler.Remove(obj);
            int i = 0;
            foreach (var item in SeciliTalep.TalepKalemler)
            {
                i = i + 1;
                item.SiraNo = i;
            }
        }

        public List<StokGrupTanim> StokGruplari { get; set; }

        public DelegateCommand KapatCommand => new DelegateCommand(OnKapat);

        private void OnKapat()
        {

          
            AppPandap.pDocumentManagerService.ActiveDocument.Close();
        }

        private void OnIsMerkeziSec()
        {

            Messenger.Default.Register<_DialogViews.KayitSecildiEvent<IsMerkezi>>(this, OnIsMerkeziSecildi);

            IsMerkeziSec view = new IsMerkeziSec();
            IsMerkeziSecVm vm = new IsMerkeziSecVm();

            view.DataContext = vm;

            view.Show();
        }

        private void OnFormLoaded()
        {
          
        }


        private void OnYeniKalem()
        {
            Messenger.Default.Register<TalepKalemEklendiEvent>(this, KalemEklendi);
            Messenger.Default.Register<TalepKalemGuncellendiEvent>(this, KalemGuncellendi);

            TalepKalemView w = new TalepKalemView();
            TalepKalemVM vm = new TalepKalemVM(SeciliTalep.StokGrupKod);

            w.DataContext = vm;

            w.ShowDialog();
        }

        public ObservableCollection<IsMerkezi> IsMerkezleriSeviye1 { get; set; }

        public ObservableCollection<IsMerkezi> IsMerkezleriSeviye2 { get => ısMerkezleriSeviye2; set => SetProperty(ref ısMerkezleriSeviye2 , value); }

        public ObservableCollection<PersonelBilgi> TalepPersonelListe { get; set; }



        private void OnKaydet()
        {
            var yeniMi = SeciliTalep.TalepId == 0;

            SeciliTalep.TalepSurecKonum = SATINALMA_SURECDURUM.TALEPKAYIT;

            if (yeniMi)
            {
                SeciliTalep.Tip = "Talep";
               
                repo.TalepEkle(SeciliTalep);
            }

            SeciliTalep.TalepEdenAdSoyad = TalepPersonelListe.Where(c => c.TcKimlikNo == SeciliTalep.TalepEdenTc).First().AdSoyad;
            SeciliTalep.StokGrupAd = StokGruplari.First(c => c.StokGrupKod == SeciliTalep.StokGrupKod).StokGrupAd;
            


            repo.Kaydet();

            if(yeniMi)
                Messenger.Default.Send(new KayitEklendiEvent<Talep>(SeciliTalep));
            else
                Messenger.Default.Send<KayitGuncellendiEvent<IsMerkezi>>(null);

            AppPandap.pDocumentManagerService.ActiveDocument.Close();
        }

        public TalepEditVM(int talepId)
        {
            if (talepId == 0)
            {
                var talep = new Talep();
                talep.RowGuid = Guid.NewGuid();
                SeciliTalep = talep;
                SeciliTalep.TalepTarihi = DateTime.Now;
            }
            else
            {
                SeciliTalep = repo.TalepGetir(talepId);
            }

            TalepPersonelListe = repoPersonel.TalepPersonelListeGetir().OrderBy(c=>c.AdSoyad).ToObservableCollection();

            SeciliTalep.TalepKalemler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            SeciliTalep.PropertyChanged += SeciliTalep_PropertyChanged;

            StokGruplari = repo.StokGruplariGetir();


            SatinAlmaDataServices.StokTanimGuncelle();


        }

        private void OnIsMerkeziSecildi(_DialogViews.KayitSecildiEvent<IsMerkezi> obj)
        {
            SeciliTalep.IsMerkeziKod=obj.SecilenKayit.Kod;
            SeciliTalep.IsMerkeziAd = obj.SecilenKayit.Tanim;

            Messenger.Default.Unregister<_DialogViews.KayitSecildiEvent<IsMerkezi>>(this, OnIsMerkeziSecildi);
        }

        private void SeciliTalep_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
          
        }

        private void KalemGuncellendi(TalepKalemGuncellendiEvent obj)
        {
            Messenger.Default.Unregister<TalepKalemGuncellendiEvent>(this, KalemGuncellendi);
        }

        private void KalemEklendi(TalepKalemEklendiEvent obj)
        {
            var yeniSira = SeciliTalep.TalepKalemler.LastOrDefault() == null ? 1 : SeciliTalep.TalepKalemler.Last().SiraNo + 1;
            obj.TalepKalem.SiraNo = yeniSira;


            var yeniKalem = new TalepKalem();
            yeniKalem.RowGuid = Guid.NewGuid();
            yeniKalem.SiraNo = yeniSira;
            yeniKalem.StokKod = obj.TalepKalem.StokKod;
            yeniKalem.StokAd = obj.TalepKalem.StokAd;

            yeniKalem.StokGrupKod = obj.TalepKalem.StokGrupKod;


            yeniKalem.StokGrupAd = SeciliTalep.StokGrupAd;
            yeniKalem.IsMerkeziAd = SeciliTalep.IsMerkeziAd;
            yeniKalem.TalepEdenAdSoyad = SeciliTalep.TalepEdenAdSoyad;


            yeniKalem.Miktar = obj.TalepKalem.Miktar;
            yeniKalem.Birim = obj.TalepKalem.Birim;

            yeniKalem.IstenilenTarih = obj.TalepKalem.IstenilenTarih;

            yeniKalem.Aciklama = obj.TalepKalem.Aciklama;
            yeniKalem.TercihMarkaModel = obj.TalepKalem.TercihMarkaModel;
            yeniKalem.TalepZamaniDepoMiktar = obj.TalepKalem.TalepZamaniDepoMiktar;

            SeciliTalep.TalepKalemler.Add(yeniKalem);

            Messenger.Default.Unregister<TalepKalemEklendiEvent>(this, KalemEklendi);
        }

        public Talep SeciliTalep { get => seciliTalep; set => SetProperty(ref seciliTalep, value); }

        public object Title => "Talep";

        public bool Close()
        {
            return true;
        }
    }
}
