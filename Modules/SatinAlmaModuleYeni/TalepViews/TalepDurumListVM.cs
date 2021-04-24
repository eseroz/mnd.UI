using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using mnd.UI.GyModules.MesajModule;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.TalepViews
{
    public class TalepDurumListVM: MyDxViewModelBase, IForm
    {
        TalepRepository repo = new TalepRepository();

        private ObservableCollection<TalepDTO> _talepler;


        public ObservableCollection<TalepDTO> Talepler { get => _talepler; set => SetProperty(ref _talepler, value); }

        public bool IslemSutunAktifMi { get; set; }

    

        public TalepDTO SeciliTalep
        {
            get => seciliTalep;
            set
            {
                SetProperty(ref seciliTalep, value);
            }
        }


        public DelegateCommand EkranYenileCommand => new DelegateCommand(EkranYenile, true);



        AppRepositorySA repoSurec = new AppRepositorySA();
        private TalepDTO seciliTalep;

        private void EkranYenile()
        {

            repo = new TalepRepository();
            Talepler = repo.TalepDurumListesi();

            Talepler.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
        }
  


        public TalepDurumListVM(string menuFormAd)
        {
            FormMenuAd = menuFormAd;
            EkranYenile();

        }

        private void OnKayitEklendi(KayitEklendiEvent<TalepDTO> obj)
        {
            EkranYenile();
        }

        public void Load()
        {

        }
    }
}
