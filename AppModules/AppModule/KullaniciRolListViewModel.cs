using DevExpress.Mvvm;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using mnd.UI.Modules;
using System.Collections.ObjectModel;

namespace mnd.UI.AppModules.AppModule
{
    public class KullaniciRolListViewModel : MyDxViewModelBase, IForm
    {
        private ObservableCollection<KullaniciRol> kullaniciRolleri;

        public ObservableCollection<KullaniciRol> KullaniciRolleri
        {
            get => kullaniciRolleri;
            set => SetProperty(ref kullaniciRolleri, value);
        }

        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public DelegateCommand<KullaniciRol> NewItemAddedCommand => new DelegateCommand<KullaniciRol>(NewItemRowUpdated, c => true);

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void NewItemRowUpdated(KullaniciRol obj)
        {
            uow.KullaniciRepo.KullaniciRolEkle(obj);
        }

        private void OnKaydet(object obj)
        {
            uow.Commit();
        }

        public KullaniciRolListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            KullaniciRolleri = uow.KullaniciRepo.KullaniciRolleriGetir();
        }
    }
}