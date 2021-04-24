using DevExpress.Mvvm;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules._SatisModule
{
    public class LmeGunlukListViewModel : IForm
    {
        private ObservableCollection<LmeGunluk> _lmeGunlukListe;

        public ObservableCollection<LmeGunluk> LmeGunlukListe { get => _lmeGunlukListe; set => _lmeGunlukListe = value; }

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);



        public string FormMenuAd { get; }

        private UnitOfWork uow = new UnitOfWork();

        public LmeGunlukListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;

            LmeGunlukListe = uow.SiparisRepo.LmeGunlukListeGetir();

            LmeGunlukListe.CollectionChanged += LmeGunlukListe_CollectionChanged;

        }

        private void LmeGunlukListe_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var entity = e.NewItems[0] as LmeGunluk;

            uow.SiparisRepo.LmeGunlukEkle(entity);
            uow.Commit();

        }

        public void Load()
        {

        }

        private void OnKaydet(object obj)
        {
            uow.Commit();
        }
    }
}
