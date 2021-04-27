using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_MusteriTakip.Domain;
using mnd.Logic.BC_Operasyon;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.Logic.Services;
using mnd.UI.AppModules.AppModule;
using mnd.UI.Helper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.MusteriTakipModule
{
    public class MusteriListViewModel : MyDxViewModelBase
    {
        private bool _isLoading;
        private string formUyariMesaj;
        private ObservableCollection<PandapCari> pandapCari;
        private PandapCari seciliPandapCari;
        private UnitOfWork uow = new UnitOfWork();


        //public DelegateCommand DegisikligiKaydetCommand => new DelegateCommand(OnDegisikligiKaydet);

        //private void OnDegisikligiKaydet()
        //{
        //    uow.Commit();
        //    MessageBox.Show("Değişiklikler Kaydedildi!");

        //}

        public DelegateCommand<Gorusme> DuzenleCommand => new DelegateCommand<Gorusme>(OnDuzenle);
        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile, () => true);
        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, () => true);

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => YetkiliMi_FromDb(nameof(ExcelExportCommand)));


        [YetkiKontrol]
        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public DelegateCommand<PandapCari> MusteriBilgiAcCommand => new DelegateCommand<PandapCari>(OnMusteriBilgiAc);

    


        public bool FormLoaded { get; private set; }

        public string FormUyariMesaj { get => formUyariMesaj; set => SetProperty(ref formUyariMesaj, value); }

        public string GridLayoutFileName => "MusteriListe.xml";

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ObservableCollection<PandapCari> PandapCariler
        {
            get => pandapCari;
            set => SetProperty(ref pandapCari, value);
        }

        public PandapCari SeciliPandapCari
        {
            get => seciliPandapCari;
            set
            {
                if (FormLoaded == false) return;

                var oldValue = seciliPandapCari;

                if (SetProperty(ref seciliPandapCari, value) == true)
                {
                    if (oldValue != null) oldValue.PropertyChanged -= SeciliPandapCari_PropertyChanged;

                    if (value != null) value.PropertyChanged += SeciliPandapCari_PropertyChanged;
                }
            }
        }

        public bool TonajlariGorebilirMi
        {
            get => AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI
                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATINALMA_YONETICI;
        }


        public MusteriListViewModel()
        {
            if (FormLoaded) return;
        }

        public MusteriListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);
        }

        private void OnDuzenle(Gorusme gorusme)
        {
            GorusmeViewModel vm = new GorusmeViewModel(gorusme.Id);
            var doc = AppPandap.DokumanOlustur("GorusmeView", vm, gorusme.GorusulenKisi);
            doc.Show();
        }

        private void OnEkranYenile()
        {
            OnNetsistenGuncelle(null);
        }

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void OnFormLoaded()
        {
            try
            {
                ExportService.RestoreLayout(GridLayoutFileName);
                OnNetsistenGuncelle(null);
                FormLoaded = true;
            }
            catch (System.Exception x)
            {
                MessageBox.Show(x.Message);
            }

        }

        private void OnKaydet(object obj)
        {
            uow.Commit();
        }

        private void OnMusteriBilgiAc(PandapCari cariBilgi)
        {
            MusteriDetayView view = new MusteriDetayView();
            MusteriDetayViewModel vm = new MusteriDetayViewModel(cariBilgi.CariKod);

            view.DataContext = vm;

            var doc = AppPandap.DokumanOlustur("MusteriDetayView", vm, cariBilgi.CariIsim);
            doc.Show();
        }

        private async void OnNetsistenGuncelle(object obj)
        {
            FormUyariMesaj = "Yükleniyor";
            OperasyonSevkiyatRepository repo = new OperasyonSevkiyatRepository();

            PandapCariService.NetsistenAlarakCariGuncelle("Tümü");

            uow = new UnitOfWork();

            string[] bagliPlasiyerKodlari = null;

            if (AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari != null)
            {
                bagliPlasiyerKodlari = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');
            }else{
                bagliPlasiyerKodlari = uow.PlasiyerRepo.PlasiyerKodlari();
            }

            PandapCariler = await uow.PandapCariRepo.PandapCarileriBagliPlasiyerlereGoreGetir(bagliPlasiyerKodlari, AppPandap.AktifKullanici.KullaniciRol);



            FormUyariMesaj = "";
        }

        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(GridLayoutFileName);
        }

        private void SeciliPandapCari_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SeciliPandapCari.ToDo))
            {
                SeciliPandapCari.ToDoGuncelleyen = AppPandap.AktifKullanici.KullaniciId;
            }

            uow.Commit();
        }
    }
}