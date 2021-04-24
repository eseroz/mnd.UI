using DevExpress.Mvvm;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.UI.Helper;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules._DialogViews.StokSecimDialog
{
    public class StokListViewModel : MyDxViewModelBase
    {
        public string FormMenuAd { get; }
        public ObservableCollection<vwStokTanim> StokTanimlar { get => stokTanimlar; set => SetProperty(ref stokTanimlar, value); }
        public ICurrentWindowService CurrentWindow => ServiceContainer.GetService<ICurrentWindowService>(ServiceSearchMode.LocalOnly);



        StokTanimRepository repo = new StokTanimRepository();
        private ObservableCollection<vwStokTanim> stokTanimlar;

        public DelegateCommand<vwStokTanim> SecCommand => new DelegateCommand<vwStokTanim>(OnSec, c => true);


        private void OnFormClosing()
        {
            Messenger.Default.Send<StokSecildiEvent>(null);
        }
        private void OnSec(vwStokTanim obj)
        {
            Messenger.Default.Send<StokSecildiEvent>(new StokSecildiEvent(obj));
            CurrentWindow.Close();
        }

        public StokListViewModel(string grupFiltre)
        {
            StokTanimlar = repo.StokListesi(grupFiltre);


            StokTanimNetsisRepository repoStokNetsis = new StokTanimNetsisRepository();
            var miktarlar = repoStokNetsis.StokMiktarlariGetir();



            foreach (var item in StokTanimlar)
            {

                var stokmiktar = miktarlar.Where(c => c.StokKod == item.STOK_KODU).FirstOrDefault();

                if (stokmiktar != null)
                    item.BAKIYE = stokmiktar.Bakiye;
                else
                    item.BAKIYE = 0;
            }


        }


    }
}
