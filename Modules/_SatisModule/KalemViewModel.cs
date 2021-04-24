using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.Model;
using mnd.Logic.Model._Ref;
using mnd.Logic.Model.Satis;
using mnd.Logic.Model.Stok;
using mnd.Logic.Model.Uretim;
using mnd.UI.AppModules.AppModule;
using mnd.UI.Modules.TeklifModule.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace mnd.UI.Modules._SatisModule
{
    public class KalemViewModel : MyBindableBase
    {
        private SiparisKalem tempKalem;

        private KayitModu kayitModu;

        public DelegateCommand MusteriUrunKodundan_UrunBilgileriGetirCommand => new DelegateCommand(OnMusteriUrunKodundan_UrunBilgileriGetir);

        private void OnMusteriUrunKodundan_UrunBilgileriGetir()
        {
            if (TempKalem != null && TempKalem.MusteriUrunKodu != null)
            {

                var sipKalem = SiparisService.UrunKodundanSonKalemGetir(TempKalem.MusteriUrunKodu, TempKalem.SiparisKalemKod);



                if (sipKalem == null)
                {
                    MessageBox.Show("Böyle bir müşteri ürün kodu bulunamadı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (sipKalem != null)
                {
                    tempKalem.UrunKod = sipKalem.UrunKod;

                    //tempKalem.Kalinlik_micron = sipKalem.Kalinlik_micron;
                    //tempKalem.KalinlikArti_yuzde = sipKalem.KalinlikArti_yuzde;
                    //tempKalem.KalinlikEksi_yuzde = sipKalem.KalinlikEksi_yuzde;

                    //tempKalem.MasuraTipKod = sipKalem.MasuraTipKod;

                    //tempKalem.AmbalajKafesOlcu = sipKalem.AmbalajKafesOlcu;

                    //tempKalem.Metraj_mt = sipKalem.Metraj_mt;
                    //tempKalem.MetrajArti_mt = sipKalem.MetrajArti_mt;
                    //tempKalem.MetrajEksi_mt = sipKalem.MetrajEksi_mt;


                    //tempKalem.MaxEk = sipKalem.MaxEk;

                    //tempKalem.En_mm = sipKalem.En_mm;
                    //tempKalem.EnArti_mm = sipKalem.EnArti_mm;
                    //tempKalem.EnEksi_mm = sipKalem.EnEksi_mm;

                    //tempKalem.RuloDiscapMax_mm = sipKalem.RuloDiscapMax_mm;
                    //tempKalem.RuloDiscapMin_mm = sipKalem.RuloDiscapMin_mm;
                    //tempKalem.RuloIcCap_mm = sipKalem.RuloIcCap_mm;

                    //tempKalem.RuloAgirligiMin_kg = sipKalem.RuloAgirligiMin_kg;
                    //tempKalem.RuloAgirligiMax_kg = sipKalem.RuloAgirligiMax_kg;

                    //tempKalem.AmbalajTipKod = sipKalem.AmbalajTipKod;

                    //tempKalem.KaydiriciOraniMin_mg_m2 = sipKalem.KaydiriciOraniMin_mg_m2;
                    //tempKalem.KaydiriciOraniMax_mg_m2 = sipKalem.KaydiriciOraniMax_mg_m2;

                }
            }
        }

        public KayitModu KayitModu
        {
            get => kayitModu;
            set => SetProperty(ref kayitModu, value);

        }

        public string DovizTipKod { get => _dovizTipKod; set => _dovizTipKod = value; }


        public TBLIHRSTK SeciliUrun
        {
            get => _seciliUrun;
            set
            {
                if (value == null) return; // cancelde hata verdi

                if (SetProperty(ref _seciliUrun, value) == true)
                {

                    //this.TempKalem.AlasimTipKod = value.AlasimKod;
                    //this.TempKalem.KullanimAlanTipKod = value.UrunGrubu;
                    //this.TempKalem.YuzeyTipKod = value.YuzeyKod;
                    //this.TempKalem.SertlikTipKod = value.Sertlik;

                }
            }
        }

        public DelegateCommand<string> FormulSecCommand { get; set; }

        public SiparisKalem TempKalem
        {
            get => tempKalem;
            set
            {
                SetProperty(ref tempKalem, value);
            }
        }



        #region LookUpdata Properties


        private ObservableCollection<TBLIHRSTK> urunler;
        private TBLIHRSTK _seciliUrun;

        public ObservableCollection<TBLIHRSTK> Urunler
        {
            get => urunler;
            set => SetProperty(ref urunler, value);
        }

        //private ObservableCollection<LmeTip> lmeTipleri;

        //public ObservableCollection<LmeTip> LmeTipleri
        //{
        //    get => lmeTipleri;
        //    set => SetProperty(ref lmeTipleri, value);
        //}

        private ObservableCollection<DovizTip> dovizTipleri;

        public ObservableCollection<DovizTip> DovizTipleri
        {
            get => dovizTipleri;
            set => SetProperty(ref dovizTipleri, value);
        }

        //private ObservableCollection<KulcePrimTip> kulceTipleri;

        //public ObservableCollection<KulcePrimTip> KulceTipleri
        //{
        //    get => kulceTipleri;
        //    set => SetProperty(ref kulceTipleri, value);
        //}

        private ObservableCollection<BirimTip> birimler;

        public ObservableCollection<BirimTip> BirimTipleri
        {
            get => birimler;
            set => SetProperty(ref birimler, value);
        }


        //private ObservableCollection<SertlikTip> sertlikTipleri;

        //public ObservableCollection<SertlikTip> SertlikTipleri
        //{
        //    get => sertlikTipleri;
        //    set => SetProperty(ref sertlikTipleri, value);
        //}


        private ObservableCollection<AmbalajTip> ambalajTipleri;

        public ObservableCollection<AmbalajTip> AmbalajTipleri
        {
            get => ambalajTipleri;
            set => SetProperty(ref ambalajTipleri, value);
        }


        private string _dovizTipKod;



        #endregion LookUpdata Properties

        //[YetkiKontrol]
        //public DelegateCommand LmxFixTemizleCommand => new DelegateCommand(LmeFixTemizle, CanLmeFixTemizle);

        //private bool CanLmeFixTemizle()
        //{
        //    return true;
        //}

        //private void LmeFixTemizle()
        //{
        //    TempKalem.LmeBaglamaKod = null;
        //    OnPropertyChanged(TempKalem.LmeBaglamaKod);

        //}

        public KalemViewModel(SiparisKalem kalem)
        {

            FormulSecCommand = new DelegateCommand<string>(formulSec);
            TempKalem = kalem;

        }

        public KalemViewModel()
        {

        }



        public void formulSec(string formul)
        {
            //StokKalemDetay.AktifFormul =(FormulEnum)Enum.Parse(typeof(FormulEnum), formul);
        }


    }
}