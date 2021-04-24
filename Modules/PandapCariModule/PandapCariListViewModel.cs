using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.Helper;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.Logic.Services;
using mnd.UI.AppModules.AppModule;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.PandapCariModule
{

    public class PandapCariListViewModel : MyDxViewModelBase, IForm
    {
        public bool IsTabFormLoaded { get; set; } = false;

        public string FormUyariMesaj { get => formUyariMesaj; set => SetProperty(ref formUyariMesaj, value); }

        public string GridLayoutFileName => "PandapCari.xml";
        private ObservableCollection<PandapCari> pandapCari;

        public ObservableCollection<PandapCari> PandapCariler
        {
            get => pandapCari;
            set => SetProperty(ref pandapCari, value);
        }

        public List<string> RiskTakipYerListe { get; set; } = new List<string> { RISKHESAPSEKLI.GenelTakip, RISKHESAPSEKLI.Operasyon, RISKHESAPSEKLI.Satış };

        public string RiskTakipYer
        {
            get => riskTakipYer;
            set
            {
                SetProperty(ref riskTakipYer, value);

                if (IsTabFormLoaded == false) return;

                OnEkranYenile();

            }
        }


        // dxgrid selectionmode=row olacak
        public PandapCari SeciliPandapCari
        {
            get => seciliPandapCari;
            set
            {
                if (seciliPandapCari != null)
                    seciliPandapCari.PropertyChanged -= SeciliPandapCari_PropertyChanged;


                if (value == null) return;

                seciliPandapCari = value;
                seciliPandapCari.PropertyChanged += SeciliPandapCari_PropertyChanged;
            }
        }

        private void SeciliPandapCari_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChanged") return;
            if (e.PropertyName == "SonGuncelleyen") return;
            if (e.PropertyName == "SonGuncellemeTar") return;

            SeciliPandapCari.SonGuncelleyen = AppPandap.AktifKullanici.KullaniciId;
            SeciliPandapCari.SonGuncellemeTar = DateTime.Now;


        }

        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile, true);

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, true);

        private void OnFormLoaded()
        {
            IsTabFormLoaded = true;
            OnEkranYenile();
        }

        private void OnEkranYenile()
        {
            OnNetsistenGuncelle(null);
        }

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, true);



        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(GridLayoutFileName);
        }

        private UnitOfWork uow = new UnitOfWork();
        private string riskTakipYer;
        private string formUyariMesaj;
        private PandapCari seciliPandapCari;

        [YetkiKontrol]
        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => YetkiliMi_FromDb(nameof(KaydetCommand)));

     

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => YetkiliMi_FromDb(nameof(ExcelExportCommand)));


        private bool CanExcelExport(object arg)
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI;
        }

        public DelegateCommand<object> NetsistenGuncelleCommand => new DelegateCommand<object>(OnNetsistenGuncelle, c => true);

        private async void OnNetsistenGuncelle(object obj)
        {

            FormUyariMesaj = "Yükleniyor...";
            PandapCariler = null;


            var lmeGunlukFiyat = LmeService.LmeFiyatGetirTarihten(DateTime.Now.AddDays(-1).Date);

            if (lmeGunlukFiyat == null)
            {
                MessageBox.Show("Lme fiyatı girili değil", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PandapCariService.NetsistenAlarakCariGuncelle("Tümü");

            uow = new UnitOfWork();

            var bagliPlasiyerKodlari = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');

            var liste = await uow.PandapCariRepo.PandapCarileriGetirAsync();

            liste = liste
                       .Where(c => c.CariKod.StartsWith("120") && c.CariTip == "A")
                       .ToList()
                       .Where(c => bagliPlasiyerKodlari.Any(x => x.ToString() == c.PandaMusteriSorumluKod))
                       .Select(c =>
                       {
                           c.RiskTalepYer = RiskTakipYer;

                           return c;
                       }
                       )
                       .ToObservableCollection();

            PandapCariService.CariListeRiskBilgileriniYukle(liste);

            PandapCariler = liste;

            FormUyariMesaj = "";


        }

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "cari_export.xlsx");
        }

        private void OnKaydet(object obj)
        {
            uow.Commit();

            MessageBox.Show("Kayıt işlemi başarıyle gerçekleştirildi.", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void Load()
        {

        }

        public PandapCariListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
            RiskTakipYer = RISKHESAPSEKLI.GenelTakip;
            FormUyariMesaj = "";

            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);
        }


    }
}