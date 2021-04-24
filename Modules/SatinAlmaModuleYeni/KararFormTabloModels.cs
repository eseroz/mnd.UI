using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace mnd.UI.Modules.SatinAlmaModuleYeni
{
    public class KararFormModel : MyBindableBase
    {
        private ObservableCollection<FirmaSutunDataModel> tabloTeklifSutunlari;

        public KararFormModel()
        {
            TabloTeklifSatirlari = new List<FirmaTeklifSatirModel>();
            TabloTeklifSutunlari = new ObservableCollection<FirmaSutunDataModel>();
        }

        public List<FirmaTeklifSatirModel> TabloTeklifSatirlari { get; set; }

        public ObservableCollection<FirmaSutunDataModel> TabloTeklifSutunlari
        {
            get => tabloTeklifSutunlari;
            set => SetProperty(ref tabloTeklifSutunlari, value);
        }
    }

    public class FirmaSutunDataModel : MyBindableBase
    {
        private string odemeSekli;
        private string nakliyeDurum;
        private string dovizTip;
        private string cariAd;
        private int _mesajSayisi;
        private int _okunmamisMesajSayisi;
        private decimal? ındirimMiktari;

        public string CariKod { get; set; }
        public string CariAd { get => cariAd; set => SetProperty(ref cariAd, value); }

        public string DovizTip { get => dovizTip; set => SetProperty(ref dovizTip, value); }
        public string OdemeSekli { get => odemeSekli; set => SetProperty(ref odemeSekli, value); }
        public string NakliyeDurum { get => nakliyeDurum; set => SetProperty(ref nakliyeDurum, value); }

        public decimal? IndirimMiktari { get => ındirimMiktari; set => SetProperty(ref ındirimMiktari, value); }

        public int MesajSayisi { get => _mesajSayisi; set => SetProperty(ref _mesajSayisi, value); }

        public int OkunmamisMesajSayisi { get => _okunmamisMesajSayisi; set => SetProperty(ref _okunmamisMesajSayisi, value); }
        public Guid RowGuid { get; internal set; }
    }

    public class FirmaHucreDataModel : MyBindableBase
    {
        private decimal? birimFiyat;
        private bool satinAlmaTercihiMi;
        private bool yoneticiTercihiMi;
        private decimal? miktar;

        public FirmaHucreDataModel(DateTime talepTarihi, decimal miktar)
        {
            T_Miktar = miktar;
            T_TalepTarihi = talepTarihi;
        }

        public DateTime T_TalepTarihi { get; set; }
        public string T_OrtakDovizCinsi { get; set; }

        public string T_DovizTip { get; set; }

        public decimal? T_Miktar
        {
            get => miktar; set
            {
                SetProperty(ref miktar, value);
                OnPropertyChanged(nameof(ToplamFiyat));
            }
        }

        public decimal? BirimFiyat
        {
            get => birimFiyat;
            set
            {
                SetProperty(ref birimFiyat, value);
                OnPropertyChanged(nameof(ToplamFiyat));
            }
        }

        public decimal? ToplamFiyat
        {
            get
            {
                decimal dovizDeger = 1;

                if (!String.IsNullOrEmpty(T_OrtakDovizCinsi))
                {
                    dovizDeger = KararFormVM.PariteGetir(T_DovizTip, T_TalepTarihi, T_OrtakDovizCinsi);
                }

                return BirimFiyat * T_Miktar * dovizDeger;
            }
        }

        public string Marka { get; set; }
        public DateTime? TeslimTarihi { get; set; }
        public bool SatinAlmaTercihiMi { get => satinAlmaTercihiMi; set => SetProperty(ref satinAlmaTercihiMi, value); }

        public bool YoneticiTercihiMi { get => yoneticiTercihiMi; set => SetProperty(ref yoneticiTercihiMi, value); }
        public string CariKod { get; set; }
        public string CariAd { get; internal set; }
    }

    public class FirmaTeklifSatirModel : MyBindableBase
    {
        private string stokAd;
        private string stokKod;
        private decimal miktar;
        private string sonAlimFirmaAd;
        private string sonAlimFiyat;
        private FirmaHucreDataModel firma1_DataModel;
        private FirmaHucreDataModel firma2_DataModel;
        private Guid _rowGuid;
        private int _mesajSayisi;
        private int _okunmamisMesajSayisi;
        private FirmaHucreDataModel firma3_DataModel;
        private FirmaHucreDataModel firma4_DataModel;
        private FirmaHucreDataModel firma5_DataModel;
        private bool? yoneticiIptal;

        public Talep SeciliTeklif { get; }

        public FirmaTeklifSatirModel(Talep teklif)
        {
            SeciliTeklif = teklif;
        }

        public int KalemId { get; set; }
        public string StokAd { get => stokAd; set => SetProperty(ref stokAd, value); }

        public string StokKod { get => stokKod; set => SetProperty(ref stokKod, value); }

        public string Aciklama { get; set; }

        public decimal Miktar
        {
            get => miktar;
            set
            {
                SetProperty(ref miktar, value);

                if (Firma1_DataModel != null) { Firma1_DataModel.T_Miktar = miktar; }
                if (Firma2_DataModel != null) { Firma2_DataModel.T_Miktar = miktar; }
                if (Firma3_DataModel != null) Firma3_DataModel.T_Miktar = miktar;
                if (Firma4_DataModel != null) Firma4_DataModel.T_Miktar = miktar;
                if (Firma5_DataModel != null) Firma5_DataModel.T_Miktar = miktar;

            }
        }

        public string OlcuBirimi { get; set; }

        public bool? YoneticiKalemIptalMi { get => yoneticiIptal; set => SetProperty(ref yoneticiIptal, value); }

        public DateTime? IstenilenTarih { get; set; }

        public string SonAlimFirmaAd { get => sonAlimFirmaAd; set => SetProperty(ref sonAlimFirmaAd, value); }
        public string SonAlimFiyat { get => sonAlimFiyat; set => SetProperty(ref sonAlimFiyat, value); }
        public string SonAlimFirmaCariKod { get; internal set; }

        public decimal GuncelStokMiktari { get; set; }

        public string IsMerkezi { get; set; }
        public string TalepEdenAdSoyad { get; set; }

        public FirmaHucreDataModel Firma1_DataModel { get => firma1_DataModel; set => SetProperty(ref firma1_DataModel, value); }
        public FirmaHucreDataModel Firma2_DataModel { get => firma2_DataModel; set => SetProperty(ref firma2_DataModel, value); }

        public FirmaHucreDataModel Firma3_DataModel { get => firma3_DataModel; set => SetProperty(ref firma3_DataModel, value); }

        public FirmaHucreDataModel Firma4_DataModel { get => firma4_DataModel; set => SetProperty(ref firma4_DataModel, value); }

        public FirmaHucreDataModel Firma5_DataModel { get => firma5_DataModel; set => SetProperty(ref firma5_DataModel, value); }

        public string SecilenFirmaCariKod { get; set; }
        public string SecilenFirmaCariAd { get; set; }

        public int MesajSayisi { get => _mesajSayisi; set => SetProperty(ref _mesajSayisi, value); }

        public int OkunmamisMesajSayisi { get => _okunmamisMesajSayisi; set => SetProperty(ref _okunmamisMesajSayisi, value); }

        public Guid RowGuid { get => _rowGuid; set => SetProperty(ref _rowGuid, value); }
        public FirmaHucreDataModel SeciliHucreModel { get; internal set; }
        public string StokGrupKod { get; internal set; }
        public string StokGrupAd { get; internal set; }
        public string TercihMarkaModel { get; internal set; }
        public int? IlkTalepId { get; internal set; }
        public Guid IlkTalepRowGuid { get; internal set; }
    }
}