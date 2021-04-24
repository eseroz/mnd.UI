using DevExpress.Mvvm;
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.BC_Dokum.Model;
using mnd.Logic.BC_Satis.Data_LookUp.Model;
using mnd.Logic.BC_Satis.Repositories;
using mnd.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.PlanlamaModule.DokumBolum
{
    public class BobinEkleFormVM: MyBindableBase
    {
        LookUpDataService repoLookUp = new LookUpDataService();
        DokumRepository repo = new DokumRepository();
        private AlasimTip seciliAlasim;

        public List<AlasimTip> AlasimTipleri { get; }

        public AlasimTip SeciliAlasim
        {
            get => seciliAlasim;
            set
            {
                SetProperty(ref seciliAlasim, value);

                DokumKafileEdit.PlanKalinlik = seciliAlasim.DokumKalinlik;

            }
        }

        public DokumBobin DokumKafileEdit { get; set; }

        public BobinEkleFormVM()
        {
            AlasimTipleri = repoLookUp.AlasimTipleriGetir();


        }

    }
}
