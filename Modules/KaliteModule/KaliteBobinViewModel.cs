using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using mnd.Logic.Model;
using mnd.Logic.Model.Uretim;

namespace mnd.UI.Modules.KaliteModule
{
    public class KaliteBobinViewModel : MyBindableBase
    {
        public ObservableCollection<Bobin> Bobinler { get; set; }
        public Bobin TempBobin { get; set; }
        public UretimEmri AktifUretimEmri { get; set; }

        public DelegateCommand<string> KapatCommand { get; set; }

        private Bobin _seciliBobin;

        public Bobin SeciliBobin
        {
            get => _seciliBobin;
            set
            {
                if (SetProperty(ref _seciliBobin, value))
                {
                    if (SeciliBobin == null) return;

                    SeciliBobin.KaliteSinirlari = AktifUretimEmri.KaliteSinirlari;

                    //TODO : Fh_Çıkış no kart noya eşitmi?
                    SeciliBobin.FH_CikisNo = AktifUretimEmri.KartNo;
                    SeciliBobin.SertifikaNumarasi = AktifUretimEmri.UretimEmriKod;
                    SeciliBobin.PropertyChanged += SeciliBobin_PropertyChanged;
                }
            }
        }

        private void SeciliBobin_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (SeciliBobin != null)
            {
                SeciliBobin.IsValidModel();

                SeciliBobin.CariKod = AktifUretimEmri.SiparisKalemKodNav.SiparisNav.CariKod;
                SeciliBobin.UretimEmriKod = AktifUretimEmri.UretimEmriKod;
            }
        }

        public KaliteBobinViewModel()
        {
        }
    };
}