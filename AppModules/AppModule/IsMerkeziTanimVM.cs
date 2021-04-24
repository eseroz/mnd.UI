using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using mnd.Logic.BC_App;
using mnd.Logic.BC_App.Domain;
using mnd.UI.Helper;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace mnd.UI.AppModules.AppModule
{

    public class HandleToIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            return ".";

            var handle = (int)values[0] + 1;
            var grid = (GridControl)values[1];


            if (handle < 0) return "";

            return grid.GetRowVisibleIndexByHandle(handle).ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class IsMerkeziTanimVM : MyDxViewModelBase
    {
        public ObservableCollection<IsMerkezi> IsMerkezleri { get => ısMerkezleri; set => SetProperty(ref ısMerkezleri, value); }
        IsMerkeziRepository repo = new IsMerkeziRepository();
        private ObservableCollection<IsMerkezi> ısMerkezleri;

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet);

        public DelegateCommand<IsMerkezi> KalemSilCommand => new DelegateCommand<IsMerkezi>(OnKalemSil);


        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }
        public DelegateCommand YenileCommand => new DelegateCommand(OnYenile);

        private void OnYenile()
        {
            repo = new IsMerkeziRepository();

            Yukle();
        }

        private void OnKalemSil(IsMerkezi obj)
        {

            var cev = MessageBox.Show("Kayıt siliniyor...", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (cev == MessageBoxResult.OK)
            {
                IsMerkezleri.Remove(obj);

            }


        }

        private void OnKaydet(object obj)
        {

            try
            {
                foreach (var item in IsMerkezleri)
                {
                    if (item.KayitModu == "Yeni") repo.Ekle(item);
                    item.KayitModu = "";

                }

                repo.Kaydet();
                MessageBox.Show("Kaydedildi", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }


        private void OnYeni(object obj)
        {
            var obj1 = new IsMerkezi { Kod = 0, ParentId = 0 };

            IsMerkezleri.Add(obj1);

        }
        public IsMerkeziTanimVM(string FormAd)
        {
            Yukle();
        }

        public void Yukle()
        {

            IsMerkezleri = repo.TumIsMerkezleriGetir();
            IsMerkezleri.CollectionChanged -= IsMerkezleri_CollectionChanged;

            IsMerkezleri.CollectionChanged += IsMerkezleri_CollectionChanged;
        }

        private void IsMerkezleri_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var item = e.NewItems[0] as IsMerkezi;
                item.KayitModu = "Yeni";
            }


            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var item = e.OldItems[0] as IsMerkezi;

                if (item.KayitModu == "Yeni") return;

                repo.Sil(item);
                repo.Kaydet();
            }

        }
    }
}
