using System;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;

namespace mnd.UI.Modules.PlanlamaModule
{
    public class PlanlamaKalemListViewModel : MyDxViewModelBase, IForm
    {
        private UnitOfWork uow = new UnitOfWork();

        //PlanlamaKalemList_GridControl
        public DelegateCommand VerileriGuncelleCommand => new DelegateCommand(VerileriGuncelle, true);

        public DelegateCommand<SiparisKalem> PandapMessangerAcCommand => new DelegateCommand<SiparisKalem>(PandapMessangerAc);

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, true);

        public string GridYerlesimDosyaAd => $"planlama_kalemlist.xml";
        public string GridExportDosyaAd => $"planlama_kalemlist_{DateTime.Now.ToString("dd/MM/yyyy_HH_mm")}.xls";

        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(GridYerlesimDosyaAd);
        }

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, GridExportDosyaAd);
        }

        public DelegateCommand<string> KalemIslemCommand => new DelegateCommand<string>(OnKalemIslem, canKalemIslem);

        private bool canKalemIslem(string arg)
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA
                       || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;
        }

        public void Load()
        {
            Kalemler = uow.SiparisKalemRepo.AktifSiparisKalemleriIleEmirleriGetir();

            ExportService.RestoreLayout(GridYerlesimDosyaAd);
        }

        private async void OnKalemIslem(string islem)
        {
            //if (islem == "Kapat")
            //{
            //    uow = new UnitOfWork();

            //    //var mesaj = "Üretimde " + SeciliKalem.PLAN_UretimdekiMiktarToplam + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

            //    var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            //    if (mesajSonuc == MessageBoxResult.Yes)
            //    {
            //        SeciliKalem.PLAN_PlanlanacakKalanMiktarToplam = SeciliKalem.PLAN_PlanlanacakKalanMiktarToplam + SeciliKalem.PLAN_UretimdekiMiktarToplam;

            //        SeciliKalem.UretimEmirleri.ForEach(c => { c.KapatildiMi = true; });
            //        uow.Commit();
            //    }

            //    if (mesajSonuc == MessageBoxResult.No)
            //    {
            //        var kalem = uow.SiparisKalemRepo.SiparisKalemiGetir(SeciliKalem.SiparisKalemKod);
            //        kalem.PLAN_KalemKapatildiMi = true;
            //        kalem.PLAN_KalemKapatilmaTarihi = DateTime.Now;
            //        kalem.UretimEmirleri.ForEach(c => { c.KapatildiMi = true; });

            //        uow.Commit();
            //    }

            //    Kalemler = uow.SiparisKalemRepo.AktifSiparisKalemleriIleEmirleriGetir();
            //}
          
        }

        private void PandapMessangerAc(SiparisKalem row)
        {
            AppMesaj.MesajFormAc(row);
        }

        private ObservableCollection<SiparisKalem> kalemler;

        public ObservableCollection<SiparisKalem> Kalemler
        {
            get => kalemler;
            set => SetProperty(ref kalemler, value);
        }

        private ObservableCollection<SiparisKalem> seciliKalemler = new ObservableCollection<SiparisKalem>();

        public ObservableCollection<SiparisKalem> SeciliKalemler
        {
            get => seciliKalemler;
            set => SetProperty(ref seciliKalemler, value);
        }

        private SiparisKalem seciliKalem;

        public SiparisKalem SeciliKalem
        {
            get => seciliKalem;
            set => SetProperty(ref seciliKalem, value);
        }

        public PlanlamaKalemListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void VerileriGuncelle()
        {
            uow = new UnitOfWork();
            Kalemler = uow.SiparisKalemRepo.AktifSiparisKalemleriIleEmirleriGetir();
        }

        private bool canPlanla()
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA
                        || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;
        }
    }
}