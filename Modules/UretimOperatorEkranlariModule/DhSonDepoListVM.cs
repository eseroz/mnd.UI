using DevExpress.Mvvm;
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.BC_Dokum.Model;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    public class DhSonDepoListVM : MyDxViewModelBase
    {
        DokumRepository repo = new DokumRepository();
        private ObservableCollection<DokumBobin> dokumKafileListe;

        public ObservableCollection<DokumBobin> DokumKafileListe { get => dokumKafileListe; set => SetProperty(ref dokumKafileListe, value); }

        public DhSonDepoListVM(string formAd)
        {
            DokumKafileListe = repo.DokumBobinBitisDepoGetir();
        }

    }
}
