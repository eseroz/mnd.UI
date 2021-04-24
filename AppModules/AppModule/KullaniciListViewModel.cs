using DevExpress.Mvvm;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using mnd.UI.Modules;
using System.Collections.ObjectModel;

namespace mnd.UI.AppModules.AppModule
{
    public class KullaniciListViewModel : MyDxViewModelBase, IForm
    {
        private ObservableCollection<Kullanici> kullanicilar;

        public ObservableCollection<Kullanici> Kullanicilar
        {
            get => kullanicilar;
            set => SetProperty(ref kullanicilar, value);
        }

        private UnitOfWork uow = new UnitOfWork();

        [YetkiKontrol]
        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);

        public string GridLayoutFileName => "KullaniciList.xml";

        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(GridLayoutFileName);
        }

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }

        public DelegateCommand<Kullanici> NewItemAddedCommand => new DelegateCommand<Kullanici>(NewItemRowUpdated, c => true);

        private void NewItemRowUpdated(Kullanici obj)
        {
            uow.KullaniciRepo.KullaniciEkle(obj);
        }

        private void OnKaydet(object obj)
        {
            uow.Commit();
        }

        public KullaniciListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            Kullanicilar = uow.KullaniciRepo
                .KullanicilariGetir();

            ExportService.RestoreLayout(GridLayoutFileName);
        }
    }
}