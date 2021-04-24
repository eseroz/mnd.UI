using DevExpress.Mvvm;
using mnd.Logic.BC_Uretim;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;

namespace mnd.UI.Modules.Dashboard
{

    public class MakinaMaliyetVM : MyDxViewModelBase
    {
        private DateTime bitisTarihi;
        private DateTime baslamaTarihi;

        public List<MakinaPerformansDTO> MakinaPerformanslari
        {
            get => makinaPerformanslari;
            set => SetProperty(ref makinaPerformanslari, value);
        }

        public DateTime BaslamaTarihi { get => baslamaTarihi; set => SetProperty(ref baslamaTarihi, value); }
        public DateTime BitisTarihi { get => bitisTarihi; set => SetProperty(ref bitisTarihi, value); }

        public string Alasim { get => alasim; set => SetProperty(ref alasim, value); }


        public DelegateCommand SorgulaCommand => new DelegateCommand(OnSorgula);




        private string alasim;
        private List<MakinaPerformansDTO> makinaPerformanslari;

        private void OnSorgula()
        {
            MakinaPerformansRepository repo = new MakinaPerformansRepository();
            var sonuc = repo.PerformansTabloGetir(baslamaTarihi, bitisTarihi, "");
            MakinaPerformanslari = sonuc;
        }

        public MakinaMaliyetVM(string form)
        {
            BaslamaTarihi = DateTime.Now.Date;
            BitisTarihi = DateTime.Now.Date;
            Alasim = "8100";

            OnSorgula();
        }



    }
}
