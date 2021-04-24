using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.Helper;

namespace mnd.UI.Modules.KaliteModule
{
    public class UrunListViewModel : MyDxViewModelBase
    {
        private ObservableCollection<Urun> urunler;

        public ObservableCollection<Urun> Urunler
        {
            get => urunler;
            set => SetProperty(ref urunler, value);
        }

        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand KaydetCommand => new DelegateCommand(Kaydet);

        public DelegateCommand<Urun> NewItemAddedCommand => new DelegateCommand<Urun>(NewItemRowUpdated, c => true);

        private void NewItemRowUpdated(Urun obj)
        {
            uow.KaliteRepo.UrunEkle(obj);
        }

        private void Kaydet()
        {
            uow.Commit();
        }

        public UrunListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            Urunler = uow.KaliteRepo.UrunListesiGetir();
        }
    }
}