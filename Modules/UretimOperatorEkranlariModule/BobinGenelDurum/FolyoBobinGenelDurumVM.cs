
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.BC_Dokum.Model;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule.BobinGenelDurum
{

    public class FolyoBobinGenelDurumVM : MyDxViewModelBase
    {
        DokumRepository repo = new DokumRepository();
        private ObservableCollection<DokumBobin> dokumBobinListe;

        public ObservableCollection<DokumBobin> DokumBobinListe { get => dokumBobinListe; set => SetProperty(ref dokumBobinListe, value); }

        public FolyoBobinGenelDurumVM(string formAd)
        {
            DokumBobinListe = repo.DokumBobinBitisDepoGetir();
        }

    }
}

