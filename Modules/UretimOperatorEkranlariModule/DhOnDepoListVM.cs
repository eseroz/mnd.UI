using DevExpress.Mvvm;
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.BC_Dokum.Model;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    public class DhOnDepoListVM : MyDxViewModelBase
    {
        DokumRepository repo = new DokumRepository();
        private ObservableCollection<DokumBobin> dokumBobinListe;
        private ObservableCollection<BobinItemVM> bobinItemVmListe;

        public ObservableCollection<DokumBobin> DokumBobinListe { get => dokumBobinListe; set => SetProperty(ref dokumBobinListe, value); }


        public DelegateCommand<object> EkranYenileCommand => new DelegateCommand<object>(OnYenile);

        private void OnYenile(object obj)
        {
            DokumBobinListe = repo.DhOnDepoGetir();
        }

        public DhOnDepoListVM(string formAd)
        {

            DokumBobinListe = repo.DhOnDepoGetir();
        }

    }
}
