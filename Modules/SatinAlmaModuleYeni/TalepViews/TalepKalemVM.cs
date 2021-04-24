using System;
using System.Collections.Generic;
using DevExpress.Mvvm;
using mnd.Logic.BC_SatinAlmaYeni;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.UI.Helper;
using mnd.UI.Modules._DialogViews.StokSecimDialog;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.TalepViews
{
    public class TalepKalemVM : MyDxViewModelBase
    {
        private TalepKalem seciliKalem;

        public DelegateCommand StokSecCommand => new DelegateCommand(OnStokSec, true);
        public ICurrentWindowService CurrentWindow => ServiceContainer.GetService<ICurrentWindowService>(ServiceSearchMode.LocalOnly);

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);

        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);

        private void OnIptal()
        {
            CurrentWindow.Close();
        }

        public List<OlcuBirim> OlcuBirimleri => SatinAlmaDataServices.OlcuBirimleriGetir();
        public TalepKalem SeciliKalem { get => seciliKalem; set => SetProperty(ref seciliKalem , value); }

        public string StokGrupKod { get; set; }

        public TalepKalemVM(string stokGrup)
        {
            this.StokGrupKod = stokGrup;

            Messenger.Default.Register<StokSecildiEvent>(this, OnStokSecildi);

            SeciliKalem = new TalepKalem();
            SeciliKalem.TalepKalemId = 0;
            SeciliKalem.RowGuid = Guid.NewGuid();
        }

        private void OnStokSecildi(StokSecildiEvent obj)
        {
            SeciliKalem.StokKod = obj.StokTanim.STOK_KODU;
            SeciliKalem.StokAd = obj.StokTanim.STOKADI_TR;
            SeciliKalem.TalepZamaniDepoMiktar = obj.StokTanim.BAKIYE;
        }

        void OnStokSec()
        {
            StokListView view = new StokListView();
            StokListViewModel vm = new StokListViewModel(StokGrupKod);

            view.DataContext = vm;

            view.Show();
        }

        private void OnKaydet()
        {
            if (SeciliKalem.TalepKalemId == 0)
                Messenger.Default.Send<TalepKalemEklendiEvent>(new TalepKalemEklendiEvent(SeciliKalem));
            else
                Messenger.Default.Send<TalepKalemGuncellendiEvent>(new TalepKalemGuncellendiEvent(SeciliKalem));

            WindowService.Close();

        }
    }
}
