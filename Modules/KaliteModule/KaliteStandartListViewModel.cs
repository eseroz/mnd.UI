using DevExpress.Mvvm;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using System.Collections.ObjectModel;

namespace mnd.UI.Modules.KaliteModule
{
    public class KaliteStandartListViewModel : MyDxViewModelBase
    {
        private ObservableCollection<KaliteStandart> kaliteStandartlari;

        public ObservableCollection<KaliteStandart> KaliteStandartlari
        {
            get => kaliteStandartlari;
            set => SetProperty(ref kaliteStandartlari, value);
        }

        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand KaydetCommand => new DelegateCommand(Kaydet);

        private void Kaydet()
        {
            uow.Commit();
        }

        public DelegateCommand<KaliteStandart> NewItemAddedCommand => new DelegateCommand<KaliteStandart>(NewItemRowUpdated, c => true);

        private void NewItemRowUpdated(KaliteStandart obj)
        {
            uow.KaliteRepo.KaliteStandartEkle(obj);
        }

        public KaliteStandartListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            KaliteStandartlari = uow.KaliteRepo.KaliteStandartlariGetir();
        }
    }
}