using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using Pandap.Logic.Helper;
using Pandap.Logic.Model;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Model.Uretim;
using Pandap.Logic.Model._Ref;

namespace Pandap.UI.AppModule.__SiparisKalem
{
    public class KalemViewModel : MyBindableBase
    {
        private SiparisKalem siparisKalem;

        private KayitModu kayitModu;

        public KayitModu KayitModu
        {
            get => kayitModu;
            set
            {
                SetProperty(ref kayitModu, value);
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public DelegateCommand<string> FormulSecCommand { get; set; }

        public SiparisKalem SiparisKalem
        {
            get => siparisKalem;
            set => SetProperty(ref siparisKalem, value);
        }

        private bool isVisible;

        public bool IsVisible
        {
            get => KayitModu != KayitModu.Varsayılan;
            set => SetProperty(ref isVisible, value);
        }

        #region LookUpdata Properties

        private ObservableCollection<LmeTip> lmeTipleri;

        public ObservableCollection<LmeTip> LmeTipleri
        {
            get => lmeTipleri;
            set => SetProperty(ref lmeTipleri, value);
        }

        private ObservableCollection<DovizTip> dovizTipleri;

        public ObservableCollection<DovizTip> DovizTipleri
        {
            get => dovizTipleri;
            set => SetProperty(ref dovizTipleri, value);
        }

        private ObservableCollection<KulcePrimTip> kulceTipleri;

        public ObservableCollection<KulcePrimTip> KulceTipleri
        {
            get => kulceTipleri;
            set => SetProperty(ref kulceTipleri, value);
        }

        private ObservableCollection<BirimTip> birimler;

        public ObservableCollection<BirimTip> BirimTipleri
        {
            get => birimler;
            set => SetProperty(ref birimler, value);
        }

        private ObservableCollection<AlasimTip> alasimlar;

        public ObservableCollection<AlasimTip> Alasimlar
        {
            get => alasimlar;
            set => SetProperty(ref alasimlar, value);
        }

        private ObservableCollection<MasuraTip> masuraTipleri;

        public ObservableCollection<MasuraTip> MasuraTipleri
        {
            get => masuraTipleri;
            set => SetProperty(ref masuraTipleri, value);
        }

        private ObservableCollection<YuzeyTip> yuzeyTipleri;

        public ObservableCollection<YuzeyTip> YuzeyTipleri
        {
            get => yuzeyTipleri;
            set => SetProperty(ref yuzeyTipleri, value);
        }

        private ObservableCollection<SertlikTip> sertlikTipleri;

        public ObservableCollection<SertlikTip> SertlikTipleri
        {
            get => sertlikTipleri;
            set => SetProperty(ref sertlikTipleri, value);
        }

        private ObservableCollection<KullanimAlanTip> kullanimAlanlari;

        public ObservableCollection<KullanimAlanTip> KullanimAlanlari
        {
            get => kullanimAlanlari;
            set => SetProperty(ref kullanimAlanlari, value);
        }

        private ObservableCollection<AmbalajTip> ambalajTipleri;

        public ObservableCollection<AmbalajTip> AmbalajTipleri
        {
            get => ambalajTipleri;
            set => SetProperty(ref ambalajTipleri, value);
        }

        private ObservableCollection<GumrukTip> gumrukTipleri;

        public ObservableCollection<GumrukTip> GumrukTipleri
        {
            get => gumrukTipleri;
            set => SetProperty(ref gumrukTipleri, value);
        }

        private ObservableCollection<Urun> urunler;

        public ObservableCollection<Urun> Urunler
        {
            get => urunler;
            set => SetProperty(ref urunler, value);
        }

        #endregion LookUpdata Properties

        public KalemViewModel()
        {
            FormulSecCommand = new DelegateCommand<string>(formulSec);
        }

        public void formulSec(string formul)
        {
            //StokKalemDetay.AktifFormul =(FormulEnum)Enum.Parse(typeof(FormulEnum), formul);
        }
    }
}