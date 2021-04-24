using DevExpress.Mvvm;
using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.UI.Helper;
using mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip
{
    public class ProformaEditVM : MyDxViewModelBase
    {
        private string textFormatDoviz;
        private bool solSutunEnabled;
        private bool sagSutunEnabled;

        public KulceProforma KulceProforma { get; set; }
        public string KayitModu { get; internal set; }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);

        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);

        public bool SolSutunEnabled { get => solSutunEnabled; set => SetProperty(ref solSutunEnabled, value); }
        public bool SagSutunEnabled { get => sagSutunEnabled; set => SetProperty(ref sagSutunEnabled, value); }

        public string TextFormatDoviz { get => textFormatDoviz; set => SetProperty(ref textFormatDoviz, value); }

        public ProformaEditVM(KulceProforma kulceProforma)
        {
            SolSutunEnabled = false;
            SagSutunEnabled = false;

            SolSutunEnabled = AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;

            SagSutunEnabled = AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MUHASEBE_YONETICI
                                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MUHASEBE;



            if (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI)
            {
                SolSutunEnabled = true;
                SagSutunEnabled = true;
            }

            KulceProforma = kulceProforma;


            KulceProforma.PropertyChanged += KulceProforma_PropertyChanged;

            TextFormatDoviz = DovizHelper.SimgeyeDonustur(KulceProforma.DovizTip);
            KulceProforma.HesaplamalariYap();

        }



        private void KulceProforma_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DovizTip")
            {
                TextFormatDoviz = DovizHelper.SimgeyeDonustur(KulceProforma.DovizTip);

                KulceProforma.HesaplamalariYap();
            }
        }

        private void OnIptal()
        {
            WindowService.Close();
        }

        private void OnKaydet()
        {

            Messenger.Default.Send<KayitIslemEvent<KulceProforma>>(new KayitIslemEvent<KulceProforma>(KulceProforma, KayitModu));
            WindowService.Close();

        }
    }
}
