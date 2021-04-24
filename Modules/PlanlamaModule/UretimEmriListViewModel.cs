using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using mnd.Logic.Helper;
using mnd.Logic.Model.Satis;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.Helper;

namespace mnd.UI.Modules.PlanlamaModule
{
    public class UretimEmriListViewModel : MyDxViewModelBase
    {
        //PlanlamaKalemList
        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand<object> YerlesimKaydetCommand => new DelegateCommand<object>(OnYerlesimKaydet, c => true);
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        private string GridLayoutFileName = "UretimEmriList.xml";

        private void OnYerlesimKaydet(object obj)
        {
            ExportService.SaveLayout(GridLayoutFileName);
        }

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "UretimEmriList.xls");
        }


        private UretimEmri seciliUretimEmri;
        private int _paketlameToleransYuzde;

        public UretimEmri SeciliUretimEmri
        {
            get { return seciliUretimEmri; }
            set { SetProperty(ref seciliUretimEmri, value); }
        }

        public UretimEmriListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            UretimEmirleri = uow.PlanlamaRepo.UretimEmirleriGetir()
              .Where(c => c.KapatildiMi == false || c.KapatildiMi == null)
              .OrderByDescending(c => c.UretimEmriKod)
              .ToObservableCollection();

            var uowKalite = new UnitOfWork();

            ExportService.RestoreLayout(GridLayoutFileName);
        }

        private ObservableCollection<UretimEmri> uretimEmirleri;

        public ObservableCollection<UretimEmri> UretimEmirleri
        {
            get => uretimEmirleri;
            set => SetProperty(ref uretimEmirleri, value);
        }

        public DelegateCommand<object> KapatIslemCommand => new DelegateCommand<object>(onUretimEmriKapat, c => true);

        public void onUretimEmriKapat(object obj)
        {
            var mesaj = "Üretimde" + SeciliUretimEmri.UretimPlanYuruyen + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

            uow = new UnitOfWork();
            var uretimEmri = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliUretimEmri.UretimEmriKod);

            var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel);

            SiparisKalem s = uow.SiparisKalemRepo.SiparisKalemiGetir(SeciliUretimEmri.SiparisKalemKod);

            if (mesajSonuc == MessageBoxResult.Yes)
            {
                //s.PLAN_PlanlanacakKalanMiktarToplam = s.PLAN_PlanlanacakKalanMiktarToplam + uretimEmri.UretimPlanYuruyen;
            }

            if (mesajSonuc == MessageBoxResult.No)
            {
                uretimEmri.KapatildiMi = true;
            }

            uow.Commit();
        }
    }
}