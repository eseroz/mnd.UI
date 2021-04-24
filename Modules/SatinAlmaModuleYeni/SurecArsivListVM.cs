using DevExpress.Mvvm;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using System.Collections.ObjectModel;

namespace mnd.UI.Modules.SatinAlmaModuleYeni
{
    public class SurecArsivListVM : MyDxViewModelBase, IForm
    {


        TalepRepository repo = new TalepRepository();

        private ObservableCollection<Talep> _talepler;
        private ObservableCollection<SurecTanim> talepSurecItems;
        private bool surecIslemYetkiliMi;
        private bool surecIslemYetkiliMi1;

        public string FormMenuAd { get; set; }

        public ObservableCollection<Talep> Talepler { get => _talepler; set => SetProperty(ref _talepler, value); }



        public Talep SeciliTalep { get => seciliTalep; set => SetProperty(ref seciliTalep, value); }

        public DelegateCommand EkranYenileCommand => new DelegateCommand(EkranYenile, true);

        private Talep seciliTalep;


        private bool teklifFormuOlusturulabilirMi;


        private void EkranYenile()
        {
            Talepler = repo.TalepListesi(null, "ARSIV");
            Talepler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
        }




        public SurecArsivListVM(string menuFormAd)
        {

            FormMenuAd = menuFormAd;
            EkranYenile();

        }

        public void Load()
        {

        }
    }
}
