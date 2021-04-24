using DevExpress.Mvvm;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.UI.Helper;
using mnd.UI.Modules._DialogViews.MusteriSecimDialog;
using mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis;
using mnd.UI.Modules.TeklifModule.MessangerEvents;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip
{
    public class KulceKontratEditVM: MyDxViewModelBase
    {
        public KulceKontrat KulceKontrat { get; set; }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);

        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);
        public DelegateCommand<object> MusteriSecCommand => new DelegateCommand<object>(OnMusteriSec, c => true);

        public string KayitModu { get; internal set; }

        private void OnIptal()
        {
            WindowService.Close();
        }

        private void OnKaydet()
        {

            Messenger.Default.Send<KayitIslemEvent<KulceKontrat>>(new KayitIslemEvent<KulceKontrat>(KulceKontrat,KayitModu));
            WindowService.Close();

        }
        private void OnMusteriSec(object obj)
        {
            MusteriSecView vw = new MusteriSecView();
            MusteriSecVM vm = new MusteriSecVM("320");

            vw.DataContext = vm;

            Messenger.Default.Register<MusteriSecildiEvent>(this, MusteriSecildi);

            vw.ShowDialog();
        }

        private void MusteriSecildi(MusteriSecildiEvent obj)
        {
            if (obj == null)
            {
                Messenger.Default.Unregister<MusteriSecildiEvent>(this, MusteriSecildi);
                return;
            }

            this.KulceKontrat.CariKod = obj?.Musteri.CariKod;
            this.KulceKontrat.CariIsim = obj?.Musteri.CariAd;
          
        }


        public KulceKontratEditVM()
        {

        }
    }
}
