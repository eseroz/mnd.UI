using DevExpress.Mvvm;
using mnd.Logic.BC_App;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_Uretim;
using System.Collections.Generic;
using System.Windows;

namespace mnd.UI.AppModules.AppModule
{
    public class MakinaParcaTanimVM
    {
        MakinaParcaRepository repo = new MakinaParcaRepository();
        IsMerkeziRepository repoIsMerkezi = new IsMerkeziRepository();
        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet);

        public List<MakinaParca> MakinaParcalari { get; set; }

        public List<IsMerkezi> IsMerkezleri { get; set; }

        public MakinaParcaTanimVM(string formad)
        {
            MakinaParcalari = repo.MakinaParcalarigetir();

            IsMerkezleri = repoIsMerkezi.MakinalariGetir();
        }


        private void OnKaydet(object obj)
        {
            repo.Kaydet();
            MessageBox.Show("Kaydedildi", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Information);
        }
    }
}
