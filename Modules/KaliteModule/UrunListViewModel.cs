using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using mnd.Logic.Model.Stok;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.Helper;

namespace mnd.UI.Modules.KaliteModule
{
    public class UrunListViewModel : MyDxViewModelBase
    {
        private ObservableCollection<TBLIHRSTK> urunler;

        public ObservableCollection<TBLIHRSTK> Urunler
        {
            get => urunler;
            set => SetProperty(ref urunler, value);
        }

        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand KaydetCommand => new DelegateCommand(Kaydet);

        public DelegateCommand<TBLIHRSTK> NewItemAddedCommand => new DelegateCommand<TBLIHRSTK>(NewItemRowUpdated, c => true);


        private void NewItemRowUpdated(TBLIHRSTK obj)
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