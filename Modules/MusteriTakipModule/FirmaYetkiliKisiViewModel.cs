using DevExpress.Mvvm;
using mnd.Logic.BC_MusteriTakip.Data;
using mnd.Logic.BC_MusteriTakip.Domain;
using mnd.Logic.Model.Netsis;
using mnd.UI.Helper;
using System.Collections.Generic;

namespace mnd.UI.Modules.MusteriTakipModule
{
    public class FirmaYetkiliKisiViewModel : MyDxViewModelBase
    {
        private List<Unvan> _unvanlar;

        public bool IsNew { get; set; }

        public bool IsOk { get; set; }

        public CariEmailYeni EditFirmaTemsilci { get; set; }

        public DelegateCommand<string> KisiEkleOkCommand => new DelegateCommand<string>(OnKisiEkleOk, true);

        MusteriTakipRepository repo = new MusteriTakipRepository();

        public List<Unvan> Unvanlar { get => _unvanlar; set => SetProperty(ref _unvanlar, value); }
        public List<string> Birimler { get; }
        public List<string> Durumlar { get; }

        public FirmaYetkiliKisiViewModel(CariEmailYeni ftemsilci)
        {
            if (ftemsilci == null) IsNew = true;
            if (ftemsilci == null) ftemsilci = new CariEmailYeni();

            EditFirmaTemsilci = ftemsilci;



            Unvanlar = repo.UnvanlariGetir();

            Birimler = new List<string> { "Satın Alma", "Finans", "Kalite", "Lojistik", "Diğer" };

            Durumlar = new List<string> { "Öncelikli", "Normal", "Arsiv" };




        }


        private void OnKisiEkleOk(string cev)
        {
            IsOk = cev == "Ok";
            WindowService.Close();


        }


    }
}
