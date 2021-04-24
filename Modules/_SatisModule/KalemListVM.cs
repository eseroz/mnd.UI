using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using mnd.Logic.Model;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.UI.GyModules.MesajModule;

namespace mnd.UI.Modules._SatisModule
{
    public class KalemListVM : MyBindableBase
    {
        private UnitOfWork uow = new UnitOfWork();

        private ObservableCollection<SiparisKalem> kalemler;

        public ObservableCollection<SiparisKalem> Kalemler
        {
            get => kalemler;
            set => SetProperty(ref kalemler, value);
        }

        //KalemList_GridControl.xml
        public DelegateCommand<SiparisKalem> PandapMessangerAcCommand => new DelegateCommand<SiparisKalem>(PandapMessangerAc);

        public void KapatilmisKalemleriYukle()
        {
            uow = new UnitOfWork();
            Kalemler = uow.SiparisKalemRepo.KapatilmisSiparisKalemleriIleEmirleriGetir();
        }

        private void PandapMessangerAc(SiparisKalem row)
        {
            AppMesaj.MesajFormAc(row);
        }
    }
}