using DevExpress.Data;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using mnd.Common.Helpers;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.Helper;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules._DialogViews.MusteriSecimDialog;
using mnd.UI.Modules.SatinAlmaModuleYeni.Stoklar;
using mnd.UI.Modules.TeklifModule.MessangerEvents;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.SatinAlmaModuleYeni
{
    public class KararFormVM : MyDxViewModelBase
    {
        private TalepRepository repo = new TalepRepository();
        private ObservableCollection<FirmaTeklifSatirModel> firmaTeklifler;
        private string ortakDovizCinsi;

        public string GeciciCariAd { get => geciciCariAd; set => SetProperty(ref geciciCariAd, value); }

        public ObservableCollection<FirmaTeklifSatirModel> FirmaTeklifler { get => firmaTeklifler; set => SetProperty(ref firmaTeklifler, value); }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet, CanKaydet);

        public bool SaltOkunurMu { get; set; }

        private bool CanKaydet()
        {
            if (SaltOkunurMu) return false;

            return true;
        }

        public DelegateCommand<CustomSummaryEventArgs> FirmaCustomTotalCommand => new DelegateCommand<CustomSummaryEventArgs>(OnFirmaCustomTotal, true);

        public DelegateCommand<FirmaSutunDataModel> FirmaSilCommand => new DelegateCommand<FirmaSutunDataModel>(OnFirmaSil);

        private void OnFirmaSil(FirmaSutunDataModel obj)
        {
            var sutunId=KararFormModel.TabloTeklifSutunlari.IndexOf(obj)+1;

            foreach (var teklifSatir in KararFormModel.TabloTeklifSatirlari)
            {
                NesneIslemleri.OzellikDegerAta(teklifSatir, $"Firma{sutunId}_DataModel", null);
            }

            var sutun = KararFormModel.TabloTeklifSutunlari.First(f => f.CariKod == obj.CariKod);
            sutun.CariAd = "";
            sutun.CariKod = "";
            sutun.DovizTip = "";
            sutun.OdemeSekli = "";
            sutun.NakliyeDurum = "";
       

        }

        private decimal genel_toplam;

        private void OnFirmaCustomTotal(CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start) genel_toplam = 0;

            var row = (FirmaTeklifSatirModel)e.Row;

            if (e.IsTotalSummary)
            {
                if (((GridSummaryItem)e.Item).FieldName == "Firma1_DataModel.ToplamFiyat" && row.Firma1_DataModel != null)
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Calculate) genel_toplam += row.Firma1_DataModel.ToplamFiyat.GetValueOrDefault();
                    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        e.TotalValue = genel_toplam - KararFormModel.TabloTeklifSutunlari[0].IndirimMiktari.GetValueOrDefault();
                    }
                }

                if (((GridSummaryItem)e.Item).FieldName == "Firma2_DataModel.ToplamFiyat" && row.Firma2_DataModel != null)
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Calculate) genel_toplam += row.Firma2_DataModel.ToplamFiyat.GetValueOrDefault();
                    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        e.TotalValue = genel_toplam - KararFormModel.TabloTeklifSutunlari[1].IndirimMiktari.GetValueOrDefault();
                    }
                }

                if (((GridSummaryItem)e.Item).FieldName == "Firma3_DataModel.ToplamFiyat" && row.Firma3_DataModel != null)
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Calculate) genel_toplam += row.Firma3_DataModel.ToplamFiyat.GetValueOrDefault();
                    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        e.TotalValue = genel_toplam - KararFormModel.TabloTeklifSutunlari[2].IndirimMiktari.GetValueOrDefault();
                    }
                }

                if (((GridSummaryItem)e.Item).FieldName == "Firma4_DataModel.ToplamFiyat" && row.Firma4_DataModel != null)
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Calculate) genel_toplam += row.Firma4_DataModel.ToplamFiyat.GetValueOrDefault();
                    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        e.TotalValue = genel_toplam - KararFormModel.TabloTeklifSutunlari[3].IndirimMiktari.GetValueOrDefault();
                    }
                }

                if (((GridSummaryItem)e.Item).FieldName == "Firma5_DataModel.ToplamFiyat" && row.Firma5_DataModel != null)
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Calculate) genel_toplam += row.Firma5_DataModel.ToplamFiyat.GetValueOrDefault();
                    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        e.TotalValue = genel_toplam - KararFormModel.TabloTeklifSutunlari[4].IndirimMiktari.GetValueOrDefault();
                    }
                }
            }
        }

        public DelegateCommand TedarikciEkleCommand => new DelegateCommand(OnTedarikciEkle, true);



        public DelegateCommand TedarikciTanimlaCommand => new DelegateCommand(OnTedarikçiTanimla, true);

        public double FirmaSutunGenisligi { get => firmaSutunGenisligi; set => SetProperty(ref firmaSutunGenisligi, value); }

        public bool FirmaSutunVisible1 { get => firmaSutunVisible1; set => SetProperty(ref firmaSutunVisible1, value); }
        public bool FirmaSutunVisible2 { get => firmaSutunVisible2; set => SetProperty(ref firmaSutunVisible2, value); }
        public bool FirmaSutunVisible3 { get => firmaSutunVisible3; set => SetProperty(ref firmaSutunVisible3, value); }
        public bool FirmaSutunVisible4 { get => firmaSutunVisible4; set => SetProperty(ref firmaSutunVisible4, value); }
        public bool FirmaSutunVisible5 { get => firmaSutunVisible5; set => SetProperty(ref firmaSutunVisible5, value); }

        public DelegateCommand<TableView> UpdateSelectedEntityCommand => new DelegateCommand<TableView>(OnUpdateSelectedEntity, true);

        public DelegateCommand<string> OncekiFiyatlariGosterCommand => new DelegateCommand<string>(OncekiFiyatlariGoster);

        private void OncekiFiyatlariGoster(string stokKod)
        {
            StokTanimNetsisRepository repo1 = new StokTanimNetsisRepository();
            var fiyatListe = repo1.StokAlimFiyatlariGetirFromStokKod(stokKod);

            StokFiyatListeWindow w = new StokFiyatListeWindow();
            w.DataContext = fiyatListe;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.ShowDialog();
        }

        private void OnUpdateSelectedEntity(TableView tw)
        {
            tw.Grid.UpdateTotalSummary();
        }

        private void OnTedarikçiTanimla()
        {
            AppRepositorySA appRape = new AppRepositorySA();

            var liste = appRape.GeciciCariListe().Select(c => new MusteriItemModel
            {
                CariKod = c.CariKod,
                CariAd = c.CariAd,
                UlkeKod = c.UlkeKod
            })
            .ToObservableCollection();

            MusteriSecView vw = new MusteriSecView();
            MusteriSecVM vm = new MusteriSecVM("SatinAlma", liste);

            vw.DataContext = vm;

            Messenger.Default.Register<MusteriSecildiEvent>(this, OnTedarikciSecildi);

            vw.ShowDialog();
        }



        private void OnTedarikciEkle()
        {
            MusteriSecView vw = new MusteriSecView();
            MusteriSecVM vm = new MusteriSecVM("SatinAlma");

            vw.DataContext = vm;

            Messenger.Default.Register<MusteriSecildiEvent>(this, OnTedarikciSecildi);

            vw.ShowDialog();
        }

        private void OnTedarikciSecildi(MusteriSecildiEvent obj)
        {
            Messenger.Default.Unregister<MusteriSecildiEvent>(this, OnTedarikciSecildi);

            if (obj == null) return;

            TercihlerAktifMi = false;

            OnPropertyChanged(nameof(TercihlerAktifMi));
            var firmaData = obj.Musteri;

            var sutun = new FirmaSutunDataModel();

            sutun.CariKod = firmaData.CariKod;
            sutun.CariAd = firmaData.CariAd;
            sutun.NakliyeDurum = "";
            sutun.OdemeSekli = "";

            sutun.DovizTip = firmaData.DovizTipKod;
            sutun.RowGuid = Guid.NewGuid();

            KararFormModel.TabloTeklifSutunlari.Add(sutun);

            var ilgiliHucreId = KararFormModel.TabloTeklifSutunlari.Count;

            foreach (var satir in KararFormModel.TabloTeklifSatirlari)
            {
                var kalemTeklifHucre = new FirmaHucreDataModel(SeciliTeklif.TalepTarihi, satir.Miktar);

                kalemTeklifHucre.CariKod = sutun.CariKod;
                kalemTeklifHucre.CariAd = sutun.CariAd;

                NesneIslemleri.OzellikDegerAta(satir, $"Firma{ilgiliHucreId}_DataModel", kalemTeklifHucre);
            }

            SutunGenislikleriniAyarla();
        }

        public DelegateCommand<FirmaSutunDataModel> MesajGosterCommand => new DelegateCommand<FirmaSutunDataModel>(OnMesajGoster, true);

        private void OnMesajGoster(FirmaSutunDataModel obj)
        {
            AppMesaj.MesajFormAc(obj);
        }

        public List<string> DovizCinsleri { get; set; } = new List<string> { "", "TL", "EUR", "USD", "GBP" };

        public List<string> NakliyeDurumlari { get; set; } = new List<string> { "Nakliye Dahil", "Nakliye Hariç" };

        public List<string> VadeListe { get; set; } = new List<string> { "", "Peşin", "30 Gün", "60 Gün", "90 Gün","120 Gün","150 Gün","120-150 Gün" };

        public string OrtakDovizCinsi
        {
            get
            { return ortakDovizCinsi; }
            set
            {
                SetProperty(ref ortakDovizCinsi, value);
            }
        }

        public string SeciliSurecKod { get; set; }

        public KararFormVM(int teklifId, string seciliSurecKod)
        {
          


            SeciliSurecKod = seciliSurecKod;
            FirmaTeklifler = new ObservableCollection<FirmaTeklifSatirModel>();

            OrtakDovizCinsi = "";

            if (teklifId == 0)
            {
                var talep = new Talep();
                SeciliTeklif = talep;
                SeciliTeklif.TalepTarihi = DateTime.Now;
            }
            else
            {
                SeciliTeklif = repo.TalepGetir(teklifId);
            }

            KararTablosuYukle();

            if (seciliSurecKod == SATINALMA_SURECDURUM.TEKLIF_ISTEME) TercihlerAktifMi = false;

            if (seciliSurecKod == SATINALMA_SURECDURUM.KARAR_FORMU_ONAY)
            {
                TercihlerAktifMi = true;
                SatinAlmaYoneticiAktifMi = true;
            }

            if (seciliSurecKod == SATINALMA_SURECDURUM.YONETICI_ONAYINDA)
            {
                TercihlerAktifMi = true;
                KararYoneticiAktifMi = true;
            }

            TedarikciEklenebilirMi = seciliSurecKod == SATINALMA_SURECDURUM.TEKLIF_ISTEME;

            if (seciliSurecKod == "sadeceokunur") SaltOkunurMu = true;
        }

        private bool gelismisModAktifMi;
        private KararFormModel kararFormModel;
        private double firmaSutunGenisligi;
        private List<bool> firmaSutunVisible = new List<bool> { false, false, false, false, false, false };
        private bool firmaSutunVisible1;
        private bool firmaSutunVisible2;
        private bool firmaSutunVisible3;
        private bool firmaSutunVisible4;
        private bool firmaSutunVisible5;
        private bool tercihlerAktifMi;
        private string geciciCariAd;

        public KararFormModel KararFormModel { get => kararFormModel; set => SetProperty(ref kararFormModel, value); }

        private void KararTablosuYukle()
        {
            KararFormModel = SatinAlmaService.TeklifToKararModel(SeciliTeklif);

            TalepRepository repo = new TalepRepository();

            foreach (var item in KararFormModel.TabloTeklifSatirlari)
            {
                var id=repo.TalepRowGuidGetir(item.IlkTalepId.GetValueOrDefault());

                if (id != null) item.IlkTalepRowGuid = id.GetValueOrDefault();


                // orjinal  talep de mesaj ekleri gösterebilmek için geçici çözüm
                item.RowGuid = item.IlkTalepRowGuid;
            }

            SutunGenislikleriniAyarla();

            KararFormModel.TabloTeklifSatirlari.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);



            KararFormModel.TabloTeklifSutunlari.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
        }

        public void SutunGenislikleriniAyarla()
        {
            var sutunSayisi = KararFormModel.TabloTeklifSutunlari.Count;

            for (int i = 1; i <= 5; i++)
                NesneIslemleri.OzellikDegerAta(this, $"FirmaSutunVisible" + i, false);

            for (int i = 1; i <= sutunSayisi; i++)
                NesneIslemleri.OzellikDegerAta(this, $"FirmaSutunVisible" + i, true);

            if (sutunSayisi == 0) return;

            if (sutunSayisi == 5)
                FirmaSutunGenisligi = 155 + (int)(5 - sutunSayisi);
            else
                FirmaSutunGenisligi = 155 + (int)(5 - sutunSayisi) * 25;
        }

        public Talep SeciliTeklif { get; }

        public bool TercihlerAktifMi { get => tercihlerAktifMi; set => SetProperty(ref tercihlerAktifMi, value); }
        public bool SatinAlmaYoneticiAktifMi { get; }
        public bool KararYoneticiAktifMi { get; }
        public bool TedarikciEklenebilirMi { get; }

        private void OnKaydet()
        {
            foreach (var kalem in SeciliTeklif.TalepKalemler)
            {
                kalem.TalepKalemTeklifler.Clear();
                repo.Kaydet();
            }

            SatinAlmaService.KararFormMapSeciliTeklif(KararFormModel, SeciliTeklif,repo);

            repo.Kaydet();

            if (SeciliSurecKod == SATINALMA_SURECDURUM.YONETICI_ONAYINDA)
            {

                foreach (var item in this.KararFormModel.TabloTeklifSatirlari)
                {
                    var satirYoneticiOnayliFirmaSayisi = 0;
                    if (item?.Firma1_DataModel?.YoneticiTercihiMi == true) satirYoneticiOnayliFirmaSayisi += 1;
                    if (item?.Firma2_DataModel?.YoneticiTercihiMi == true) satirYoneticiOnayliFirmaSayisi += 1;
                    if (item?.Firma3_DataModel?.YoneticiTercihiMi == true) satirYoneticiOnayliFirmaSayisi += 1;
                    if (item?.Firma4_DataModel?.YoneticiTercihiMi == true) satirYoneticiOnayliFirmaSayisi += 1;
                    if (item?.Firma5_DataModel?.YoneticiTercihiMi == true) satirYoneticiOnayliFirmaSayisi += 1;

                    if (satirYoneticiOnayliFirmaSayisi != 0 && item.YoneticiKalemIptalMi == true)
                    {
                        MessageBox.Show(item.StokAd + " için alım işlemini iptal ettiniz. Satın alma yönetici onayı için seçim yapılamaz",
                            "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (satirYoneticiOnayliFirmaSayisi==0 && item.YoneticiKalemIptalMi==false)
                    {
                        MessageBox.Show(item.StokAd + " için seçim yapmadınız", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (satirYoneticiOnayliFirmaSayisi > 1)
                    {
                        MessageBox.Show(item.StokAd + " için 1 'den fazla seçim yaptınız", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                

                }

            


                var repoYeni = new TalepRepository();
                var olusacakSiparisler = SatinAlmaService.TekliftenSiparisleriOlustur(SeciliTeklif);

                foreach (var sip in olusacakSiparisler)
                {
                    repo.TalepEkle(sip);
                }

                repo.SurecDegistir(SeciliTeklif.TalepId, "SIPARIS_YONETICI_ONAYLADI");

                Messenger.Default.Send(new YoneticiKararFormKaydedildiEvent(SeciliTeklif));
            }

            AppPandap.pDocumentManagerService.ActiveDocument.Close();
        }

        public static decimal PariteGetir(string orjinal_DovizTipi, DateTime dovizKurTarih, string donusturulecek_DovizTipi = "EUR")
        {
            if (orjinal_DovizTipi == "TL" && donusturulecek_DovizTipi == "TL") return 1;

            var dovizKuruOrj = NetsisService.NetsisDovizKuruGetir(orjinal_DovizTipi, dovizKurTarih);

            if (donusturulecek_DovizTipi == "TL") return Convert.ToDecimal(dovizKuruOrj);

            var dovizKuruDonusecek = NetsisService.NetsisDovizKuruGetir(donusturulecek_DovizTipi, dovizKurTarih);

            decimal parite = 1;

            parite = (decimal)(dovizKuruOrj / dovizKuruDonusecek);
            return Math.Round(parite, 4);
        }
    }
}