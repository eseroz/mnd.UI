using DevExpress.Mvvm;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using mnd.UI.Modules;
using System.Collections.ObjectModel;

namespace mnd.UI.AppModules.AppModule
{
    public class UygulamaAyarViewModel : MyDxViewModelBase, IForm
    {
        private UnitOfWork uow = new UnitOfWork();

        private ObservableCollection<Ayarlar> appAyarlar;

        public ObservableCollection<Ayarlar> AppAyarlar
        {
            get => appAyarlar;
            set => SetProperty(ref appAyarlar, value);
        }

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void OnKaydet(object obj)
        {
            uow.Commit();
        }

        public UygulamaAyarViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            AppAyarlar = uow.AppRepo.AyarlariGetir();
        }
    }
}