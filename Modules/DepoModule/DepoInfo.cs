using mnd.Logic.Model;

namespace mnd.UI.Modules.DepoModule
{
    public class DepoInfo : MyBindableBase
    {
        private int _depoToplam;
        private int _musteriStokToplam;
        private decimal _ortalamaEn;
        private decimal _ortalamaKalinlik;
        private int _pandaStok;
        public int DepoToplam { get => _depoToplam; set => SetProperty(ref _depoToplam, value); }
        public int MusteriStokToplam { get => _musteriStokToplam; set => SetProperty(ref _musteriStokToplam, value); }
        public decimal OrtalamaEn { get => _ortalamaEn; set => SetProperty(ref _ortalamaEn, value); }
        public decimal OrtalamaKalinlik { get => _ortalamaKalinlik; set => SetProperty(ref _ortalamaKalinlik, value); }
        public int PandaStok { get => _pandaStok; set => SetProperty(ref _pandaStok, value); }

        private int aktifAyToplam;

        public int AktifAyToplam
        {
            get => aktifAyToplam;
            set => SetProperty(ref aktifAyToplam, value);
        }
    }
}