using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DevExpress.Mvvm;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using mnd.UI.Helper;

namespace mnd.UI.Modules.MuhasebeModule
{
    public class BankaListViewModel : MyDxViewModelBase, IForm
    {
        private UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<Banka> _bankalar;

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public BankaListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
            Bankalar = uow.BankaRepo.BankalariGetir();

            Bankalar.CollectionChanged += Bankalar_CollectionChanged;
        }

        private void Bankalar_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                uow.BankaRepo.Ekle(e.NewItems[0] as Banka);
            else
                uow.BankaRepo.Sil(e.OldItems[0] as Banka);
        }

        public ObservableCollection<Banka> Bankalar { get => _bankalar; set => SetProperty(ref _bankalar, value); }

        public void Load()
        {

        }

        private void OnKaydet(object obj)
        {
            uow.Commit();
        }
    }
}