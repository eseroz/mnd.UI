using mnd.UI.Helper;

namespace mnd.UI.Modules._DialogViews.MusteriSecimDialog
{
    public class MusteriItemModel : MyDxViewModelBase
    {
        private string cariKod;
        private string ulkeKod;
        private string cariAd;

        public string CariKod { get => cariKod; set => SetProperty(ref cariKod, value); }
        public string CariAd { get => cariAd; set => SetProperty(ref cariAd, value); }

        public string DovizTipKod { get; set; }
        public string UlkeKod { get => ulkeKod; set => SetProperty(ref ulkeKod, value); }
        public string PlasiyerKod { get; internal set; }
        public string Sektor { get; internal set; }
    }
}
