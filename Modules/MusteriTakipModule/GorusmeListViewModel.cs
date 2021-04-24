using AutoMapper;
using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_MusteriTakip.Data;
using mnd.Logic.BC_MusteriTakip.Domain;
using mnd.Logic.Helper;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.AppModule;
using mnd.UI.AppModules.AppModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.MusteriTakipModule
{
    public class GorusmeListViewModel : MyDxViewModelBase, IForm
    {
        MusteriTakipRepository repo = new MusteriTakipRepository();
        private bool _isLoading;
        private ObservableCollection<Gorusme> _gorusmeler;
        private Gorusme _seciliGorusme;

        public DelegateCommand YeniGorusmeEkleCommand => new DelegateCommand(OnYeniGorusmeEkle);

        public DelegateCommand<Gorusme> DuzenleCommand => new DelegateCommand<Gorusme>(OnDuzenle);

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, true);

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);
        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>();

        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);

        public Gorusme SeciliGorusme { get => _seciliGorusme; set => SetProperty(ref _seciliGorusme, value); }

        public string GridYerlesimDosyaAd => "Gorusme1.xml";

        private UnitOfWork uow = new UnitOfWork();

        private async void OnEkranYenile()
        {
            IsLoading = true;


            var glist = await repo.GorusmeleriGetir();

            Gorusmeler = glist.ToObservableCollection();
            Gorusmeler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
            IsLoading = false;
        }

        private void OnYerlesimKaydet(object obj)
        {
            ExportService1.SaveLayout(GridYerlesimDosyaAd);
        }

        private void OnExcelExport(object obj)
        {
            ExportService1.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void OnYeniGorusmeEkle()
        {
            GorusmeViewModel vm = new GorusmeViewModel(0);
            vm.KayitMod = KayitModu.Add;
            var yeniGorusme = new Gorusme();
            yeniGorusme.RowGuid = Guid.NewGuid();

            yeniGorusme.GorusmeTarih = DateTime.Now;
            yeniGorusme.RandevuTarih = DateTime.Now;
            yeniGorusme.ModifiedDate = DateTime.Now;


            if (SeciliGorusme != null) yeniGorusme.MusteriCariKod = SeciliGorusme.MusteriCariKod;

            vm.Gorusme = yeniGorusme;

            var doc = AppPandap.DokumanOlustur("GorusmeView", vm, "Yeni Kayıt");
            doc.Show();
        }

        private void OnDuzenle(Gorusme gorusme)
        {
            GorusmeViewModel vm = new GorusmeViewModel(gorusme.Id);
            var doc = AppPandap.DokumanOlustur("GorusmeView", vm, gorusme.GorusulenKisi);
            doc.Show();

        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }


        public ObservableCollection<Gorusme> Gorusmeler { get => _gorusmeler; set => SetProperty(ref _gorusmeler, value); }

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, () => true);

        public bool IsUserFormLoaded { get; set; }


        public GorusmeListViewModel()
        {
            IsUserFormLoaded = false;
        }

        public GorusmeListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;

            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);
        }

        public async void OnFormLoaded()
        {
            if (IsUserFormLoaded) return;

            IsLoading = true;

            if (Gorusmeler != null)
                Gorusmeler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            ExportService1.RestoreLayout(GridYerlesimDosyaAd);

            uow = new UnitOfWork();

            var bagliPlasiyerKodlari = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');

            var glist = await repo.GorusmeleriPlasiyereGoreGetir(bagliPlasiyerKodlari);

            Gorusmeler = glist.ToObservableCollection();

            Gorusmeler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            IsLoading = false;

            IsUserFormLoaded = true;
        }

        public void Load()
        {
            Messenger.Default.Register<KayitMesaj>(this, OnMessage);
            Messenger.Default.Register<KayitSatirMesajEvent>(this, OnSatirMesajEklendi);
        }

        private void OnSatirMesajEklendi(KayitSatirMesajEvent satirMesajEvent)
        {
            var mesajEklenenKayit = Gorusmeler.FirstOrDefault(c => c.RowGuid == satirMesajEvent.RowGuid);

            if (mesajEklenenKayit != null)
            {
                mesajEklenenKayit.MesajSayisiniGuncelle(AppPandap.AktifKullanici.KullaniciId);
            }

        }

        void OnMessage(KayitMesaj message)
        {
            var kayit = (Gorusme)message.Kayit;

            if (message.KayitIslemTip == KayitIslemTip.Degisti)
            {
                var dbGorusme = repo.GorusmeGetir_NoTrack(kayit.Id);
                var aktif_gorusme = Gorusmeler.FirstOrDefault(c => c.Id == dbGorusme.Id);

                Mapper.Map(dbGorusme, aktif_gorusme);

                aktif_gorusme.MesajSayisiniGuncelle(AppPandap.AktifKullanici.KullaniciId);
            }

            if (message.KayitIslemTip == KayitIslemTip.Eklendi)
            {
                Gorusmeler.Add(kayit);
                kayit.MesajSayisiniGuncelle(AppPandap.AktifKullanici.KullaniciId);
            }
        }
    }
}
