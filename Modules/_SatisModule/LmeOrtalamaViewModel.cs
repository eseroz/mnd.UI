using DevExpress.Mvvm;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mnd.UI.GyModules.MesajModule;


namespace mnd.UI.Modules._SatisModule
{
    public class LmeOrtalamaViewModel : MyDxViewModelBase
    {

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        private void OnKaydet(object obj)
        {
            uow.Commit();
        }

        public ObservableCollection<LmeOrtalamaVeri> LmeOrtalamaVeriler { get; set; }

        public LmeOrtalamaView SeciliLmeOrtalamaVeri { get; set; }
        UnitOfWork uow = new UnitOfWork();

        public LmeOrtalamaViewModel(string formMenuAd)
        {
            LmeOrtalamaVeriler = uow.SiparisRepo.LmeOrtalamalariGetir();

            LmeOrtalamaVeriler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            FormMenuAd = formMenuAd;
        }

        public void Load()
        {

        }
    }
}
