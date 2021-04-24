using DevExpress.Mvvm;
using Pandap.Logic.Model;
using Pandap.Logic.QueryModel;
using Pandap.Logic.QueryModel.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandap.UI.AppModule.Raporlar
{
    public class SatisDolulukRaporModel: MyBindableBase
    {
        SatisDolulukRepository repo = new SatisDolulukRepository();

        ObservableCollection<vwSiparisDolulukSon> satisDolulukDurum;
        public ObservableCollection<vwSiparisDolulukSon> SatisDolulukDurum
        {
            get => satisDolulukDurum;
            set => SetProperty(ref satisDolulukDurum, value);
        }

        public SatisDolulukRaporModel()
        {
            SatisDolulukDurum = repo.VwSiparisDolulukGetir();
        }

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);
        public DelegateCommand<object> EkraniGuncelleCommand => new DelegateCommand<object>(OnEkraniGuncelle, c => true);

        private void OnEkraniGuncelle(object obj)
        {
            repo.UpsertSiparisDoluluk();
            SatisDolulukDurum = repo.VwSiparisDolulukGetir();
        }

        public DelegateCommand<vwSiparisDolulukSon> NewItemAddedCommand => new DelegateCommand<vwSiparisDolulukSon>(NewItemRowUpdated, c => true);

        private  void OnKaydet(object obj)
        {

          
            repo.Guncelle(SeciliSatisDoluluk);
        }


        public  void NewItemRowUpdated(vwSiparisDolulukSon item)
        {
            repo.Guncelle(item);
        }


        public vwSiparisDolulukSon SeciliSatisDoluluk { get; set; }
    }
}
