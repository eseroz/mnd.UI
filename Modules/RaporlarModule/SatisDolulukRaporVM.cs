using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using mnd.Logic.QueryModel;
using mnd.Logic.QueryModel.Repositories;
using mnd.UI.Helper;

namespace mnd.UI.Modules.RaporlarModule
{
    public class SatisDolulukRaporVM : MyDxViewModelBase
    {
        public DelegateCommand<object> EkraniGuncelleCommand => new DelegateCommand<object>(OnEkraniGuncelle, c => true);

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public DelegateCommand<vwSiparisDolulukSon> NewItemAddedCommand => new DelegateCommand<vwSiparisDolulukSon>(NewItemRowUpdated, c => true);

        //SevkEdilen_GridControl.xml

        private SatisDolulukRepository repo = new SatisDolulukRepository();

        private ObservableCollection<vwSiparisDolulukSon> satisDolulukDurum;

        public SatisDolulukRaporVM(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
           // SatisDolulukDurum = repo.VwSiparisDolulukGetir();
        }

        public ObservableCollection<vwSiparisDolulukSon> SatisDolulukDurum
        {
            get => satisDolulukDurum;
            set => SetProperty(ref satisDolulukDurum, value);
        }

        public vwSiparisDolulukSon SeciliSatisDoluluk { get; set; }

        public void NewItemRowUpdated(vwSiparisDolulukSon item)
        {
            repo.Guncelle(item);
        }

        private void OnEkraniGuncelle(object obj)
        {
            //repo.UpsertSiparisDoluluk();
            //SatisDolulukDurum = repo.VwSiparisDolulukGetir();
        }

        private void OnKaydet(object obj)
        {
            repo.Guncelle(SeciliSatisDoluluk);
        }
    }
}