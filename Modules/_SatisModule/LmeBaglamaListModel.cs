using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using mnd.Common.Helpers;
using mnd.Logic.Helper;
using mnd.Logic.Model;
using mnd.Logic.Model.Netsis;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.AppModule;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;

namespace mnd.UI.Modules._SatisModule
{

    public class LmeDurum : MyBindableBase
    {
        public static FiltreItemModel Taslak = new FiltreItemModel { ItemNameMy = nameof(Taslak), BadgeValue = 0 };
        public static FiltreItemModel OnayBekliyor = new FiltreItemModel { ItemNameMy = nameof(OnayBekliyor), BadgeValue = 0 };
        public static FiltreItemModel Onaylandi = new FiltreItemModel { ItemNameMy = nameof(Onaylandi), BadgeValue = 0 };
        public static FiltreItemModel Arşiv = new FiltreItemModel { ItemNameMy = nameof(Arşiv), BadgeValue = 0 };

        public static FiltreItemModel FaturaAşımı = new FiltreItemModel { ItemNameMy = nameof(FaturaAşımı), BadgeValue = 0 };
        public List<FiltreItemModel> Liste => new List<FiltreItemModel> { Taslak, OnayBekliyor, Onaylandi, Arşiv, FaturaAşımı };


        private FiltreItemModel _seciliMenu;
        public FiltreItemModel SeciliMenu
        {
            get => _seciliMenu; set
            {
                SetProperty(ref _seciliMenu, value);
            }
        }

    }

    public class LmeBaglamaListModel : MyDxViewModelBase, IForm
    {
        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));

        private UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<LmeBaglama> _lmeBaglamaList;
        private LmeBaglama _seciliLmeBaglama;

        private UnitOfWork uowRapor = new UnitOfWork();

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public DelegateCommand<object> YeniCommand => new DelegateCommand<object>(OnYeni, c => true);

        public DelegateCommand<object> DuzenleCommand => new DelegateCommand<object>(OnDuzenle, c => true);

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, () => true);

        public bool IsSnackbarActive { get => _isSnackbarActive; set => SetProperty(ref _isSnackbarActive, value); }

        private void OnFormLoaded()
        {
            if (AppPandap.UygulamaAyarlar.ServiceBrokerAktifMi)
                SqlTable_Dependency<LmeOnayDto>.Default.Basla("LmeBaglama", "Satis", AppPandap.AktifKullanici.KullaniciId);

            Messenger.Default.Register<LmeOnayDto>(this, LmeOnayDegisti);


        }

      
        public DelegateCommand FormUnLoadedCommand => new DelegateCommand(OnFormUnLoaded);

        private void OnFormUnLoaded()
        {
            SqlTable_Dependency<LmeOnayDto>.Default.Durdur();
            Messenger.Default.Unregister<LmeOnayDto>(this, LmeOnayDegisti);
        }

        private void LmeOnayDegisti(LmeOnayDto c)
        {
            var mesaj = c.RefNo + " nolu lfx durumu " + c.LmeKayitSurecDurum + " a dönüştürüldü...";
            BoundMessageQueue.Enqueue(mesaj, true);
            EkranTazele();
        }

        public DelegateCommand<object> SilCommand => new DelegateCommand<object>(OnSil, CanSil);

        private bool CanSil(object arg)
        {
            if (SeciliLmeBaglama == null) return false;

            return SeciliLmeBaglama.LmeKayitSurecDurum == LmeDurum.Taslak.ItemNameMy;
        }

        private void OnSil(object obj)
        {
            if (SeciliLmeBaglama != null) LmeBaglamaList.Remove(SeciliLmeBaglama);

            uow.SiparisRepo.LmeBaglamaSil(SeciliLmeBaglama);

        }

        public DelegateCommand<LmeBaglama> SatisDestek_OnayaGonderCommand => new DelegateCommand<LmeBaglama>(SatisDestek_OnayaGonder, CanSatisDestek_OnayaGonder);

        private bool CanSatisDestek_OnayaGonder(LmeBaglama arg)
        {
            var taslakSayisi = LmeBaglamaList.Count;

            return (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATIS_DESTEK || 
                AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATIS) || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.SATISYONETICI_BOLGE
                &&
                taslakSayisi != 0 &&
                LmeBaglamaList?.FirstOrDefault()?.LmeKayitSurecDurum == LmeDurum.Taslak.ItemNameMy;
        }

        public DelegateCommand<LmeBaglama> PlanY_OnaylaCommand => new DelegateCommand<LmeBaglama>(PlanY_Onayla, Can_PlanY_Onayla);

        private bool Can_PlanY_Onayla(LmeBaglama arg)
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI ||
                    AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI ||
                    AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MUHASEBE_YONETICI;
        }

        public DelegateCommand<LmeBaglama> PlanY_ReddetCommand => new DelegateCommand<LmeBaglama>(PlanY_Reddet, Can_PlanY_Reddet);

        private bool Can_PlanY_Reddet(LmeBaglama arg)
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI
                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI
                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MUHASEBE_YONETICI;


        }

        public DelegateCommand<LmeBaglama> PlanY_ArsiveGonderCommand => new DelegateCommand<LmeBaglama>(PlanY_ArsiveGonder, Can_PlanY_Arsiv);

        private bool Can_PlanY_Arsiv(LmeBaglama arg)
        {
            return
                 AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI
                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.MUHASEBE_YONETICI
                || AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;

        }



        private void SatisDestek_OnayaGonder(LmeBaglama obj)
        {
            uow.SiparisRepo.LmeSurecDurumDegistir(LmeDurum.OnayBekliyor.ItemNameMy, obj.RefNo);

            EkranTazele();
        }

        private void PlanY_Onayla(LmeBaglama obj)
        {
            uow.SiparisRepo.LmeSurecDurumDegistir(LmeDurum.Onaylandi.ItemNameMy, obj.RefNo);

            EkranTazele();
        }


        private void PlanY_Reddet(LmeBaglama obj)
        {
            uow.SiparisRepo.LmeSurecDurumDegistir(LmeDurum.Taslak.ItemNameMy, obj.RefNo);
            EkranTazele();
        }



        private void PlanY_ArsiveGonder(LmeBaglama obj)
        {
            uow.SiparisRepo.LmeSurecDurumDegistir(LmeDurum.Arşiv.ItemNameMy, obj.RefNo);
            EkranTazele();

        }


        public DelegateCommand SorguSecCommand => new DelegateCommand(OnSorgula, true);



        private void OnDuzenle(object obj)
        {
            SeciliLmeBaglama.IsEditRowMy = true;
        }

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, true);

        public DelegateCommand<object> SiparisAcCommand => new DelegateCommand<object>(SiparisiAc, true);

        private void SiparisiAc(object satir)
        {
            var siparisKod = "";

            var uow1 = new UnitOfWork();

            if (satir is SiparisKalem) siparisKod = (satir as SiparisKalem).SiparisKod;

            //if (satir is LmeBaglama) siparisKod = (satir as LmeBaglama).BagliSiparisKodlari;


            var vm = new SiparisViewModel();
            var sip = uow1.SiparisRepo.SiparisGetir(siparisKod);

            vm.Load(sip);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SiparisView", vm);
            doc.Title = sip.SiparisKod;
            doc.Show();
        }



        public LmeBaglamaListModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;

            LmeInfo = new LmeDurum();

            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);

            uow.SiparisRepo.UpdateSiparisKalemKapasitifDurum();

        }

        public LmeBaglamaListModel()
        {
            LmeInfo = new LmeDurum();// design time data için


        }

        public void Load()
        {

            Musteriler = uow.SiparisRepo.NetsistenCariKartlariGetir();

            LmeInfo.SeciliMenu = LmeDurum.Taslak;

            EkranTazele();


        }



        private void OnExcelExport(object obj)
        {

            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }


        private void OnYeni(object obj)
        {
            var obj1 = new LmeBaglama { RefNo = "Yeni" };
            obj1.IsEditRowMy = true;
            obj1.RefNo = "Yeni";
            obj1.Hazirlayan = AppPandap.AktifKullanici.KullaniciId;
            obj1.LmeKayitSurecDurum = LmeDurum.Taslak.ItemNameMy;
            obj1.OnayliMi = false;
            obj1.KilitliMi = false;
            obj1.RowGuid = Guid.NewGuid();

            LmeBaglamaList.Add(obj1);

            SeciliLmeBaglama = obj1;
        }

        public DelegateCommand<object> FormYazdirCommand => new DelegateCommand<object>(OnFormYazdir, c => true);

        public DelegateCommand<LmeBaglama> PandapMessangerAcCommand => new DelegateCommand<LmeBaglama>(PandapMessangerAc);

        public DelegateCommand EkranTazeleCommand => new DelegateCommand(EkranTazele);



        private void OnSorgula()
        {
            uow = new UnitOfWork();

            var kullaniciId = AppPandap.AktifKullanici.KullaniciId;

            LmeBaglamaList = uow.SiparisRepo.LmeBaglamaListeGetir(kullaniciId)
                .Where(c => c.LmeKayitSurecDurum == LmeInfo.SeciliMenu.ItemNameMy)
                .ToObservableCollection();

            LmeBaglamaList = LfxPaketMiktariGuncelle(LmeBaglamaList).ToObservableCollection();

            LmeBaglamaList = LfxFaturaMiktariGuncelle(LmeBaglamaList).ToObservableCollection();

            LmeBaglamaList.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

        }

        private ObservableCollection<LmeBaglama> LfxPaketMiktariGuncelle(ObservableCollection<LmeBaglama> lmeBaglamaList)
        {
            var uow5 = new UnitOfWork();
            Dictionary<string, int> dict = uow5.SiparisRepo.LmeBaglamaFaturaAsimListeGetir();


            foreach (var item in lmeBaglamaList)
            {
                item.LfxToplamPaketlenen = dict.GetValueOrDefault(item.RefNo);
            }

            return lmeBaglamaList;

        }


        private ObservableCollection<LmeBaglama> LfxFaturaMiktariGuncelle(ObservableCollection<LmeBaglama> lmeBaglamaList)
        {
            var uow5 = new UnitOfWork();
            Dictionary<string, int> dict = uow5.MuhasebeRepo.FaturaEdilenLfxKgGetir();


            foreach (var item in lmeBaglamaList)
            {
                item.FaturaLfx_kg = dict.GetValueOrDefault(item.RefNo);
            }
            return lmeBaglamaList;

        }


        private void SurecGrupSayilariGuncelle()
        {
            Dictionary<string, int> surecSayilari = uow.SiparisRepo.LmeSurecSayilariGetir();

            LmeInfo.Liste[0].BadgeValue = surecSayilari.GetValueOrDefault(LmeDurum.Taslak.ItemNameMy);
            LmeInfo.Liste[1].BadgeValue = surecSayilari.GetValueOrDefault(LmeDurum.OnayBekliyor.ItemNameMy);
            LmeInfo.Liste[2].BadgeValue = surecSayilari.GetValueOrDefault(LmeDurum.Onaylandi.ItemNameMy);
            LmeInfo.Liste[3].BadgeValue = surecSayilari.GetValueOrDefault(LmeDurum.Arşiv.ItemNameMy);

        }

        public bool OnaylamaYetkisiVarMi { get => AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI; }


        private void EkranTazele()
        {
            OnSorgula();

            SurecGrupSayilariGuncelle();

        }

        private void PandapMessangerAc(LmeBaglama row)
        {
            AppMesaj.MesajFormAc(row);
        }


        public DelegateCommand<LmeBaglama> RemoveRowCommand => new DelegateCommand<LmeBaglama>(OnLmeSil, CanOnLmeSil);

        private bool CanOnLmeSil(LmeBaglama lmeRow)
        {
            //if (lmeRow == null) return false;

            //if (AppPandap.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI)
            //    return true;

            //if (lmeRow.BaglantiLfx_kg == lmeRow.SiparisLfx_Bakiye_kg)
            //    return true;
            //else
                return false;

        }

        public bool IsReadOnlyColumn
        {
            get
            {
                if (SeciliLmeBaglama == null) return false;

                var x = (SeciliLmeBaglama.KilitliMi == true || SeciliLmeBaglama.OnayliMi == true) &&
                        AppPandap.KullaniciRol != KULLANICIROLLERI.PLANLAMA_YONETICI;

                return x;

            }

        }

        private void OnLmeSil(LmeBaglama lmeRow)
        {
            LmeBaglamaList.Remove(lmeRow);
        }

        public LmeBaglama SeciliLmeBaglama
        {
            get => _seciliLmeBaglama;
            set
            {
                if (_seciliLmeBaglama != null) _seciliLmeBaglama.PropertyChanged -= _seciliLmeBaglama_PropertyChanged;

                if (value == null) return;

                value.PropertyChanged += _seciliLmeBaglama_PropertyChanged;

                SetProperty(ref _seciliLmeBaglama, value);

                OnPropertyChanged(nameof(IsReadOnlyColumn));

            }
        }

        public DelegateCommand<LmeBaglama> InitNewItemCommand => new DelegateCommand<LmeBaglama>(InitNewItem, c => true);

        private void InitNewItem(LmeBaglama obj)
        {
            SeciliLmeBaglama = obj;

            obj.RefNo = "Yeni";
            obj.Hazirlayan = AppPandap.AktifKullanici.KullaniciId;
            obj.OnayliMi = false;
            obj.KilitliMi = false;
            obj.RowGuid = Guid.NewGuid();
        }

        private void _seciliLmeBaglama_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == nameof(SeciliLmeBaglama.BaglantiLfx_kg) && _seciliLmeBaglama.RefNo == "Yeni")
            //{
            //    OnPropertyChanged(nameof(_seciliLmeBaglama.SiparisLfx_Bakiye_kg));

            //}

            if (e.PropertyName == nameof(SeciliLmeBaglama.MusteriKod) && _seciliLmeBaglama.RefNo == "Yeni")
            {
                _seciliLmeBaglama.DovizTipKod = Musteriler.First(c => c.CariKod == _seciliLmeBaglama.MusteriKod).DovizAd;
            }


            if (e.PropertyName == nameof(SeciliLmeBaglama.OnayliMi) && _seciliLmeBaglama.RefNo != "Yeni")
            {
                if (_seciliLmeBaglama.OnayliMi == true)
                {
                    SeciliLmeBaglama.Onaylayan = AppPandap.AktifKullanici.KullaniciId;
                    SeciliLmeBaglama.OnayTarihi = DateTime.Now;
                }
                else
                {
                    SeciliLmeBaglama.Onaylayan = null;
                    SeciliLmeBaglama.OnayTarihi = null;
                }
            }


            if (e.PropertyName == nameof(SeciliLmeBaglama.KilitliMi) && _seciliLmeBaglama.RefNo != "Yeni")
            {
                if (_seciliLmeBaglama.KilitliMi == true)
                {
                    SeciliLmeBaglama.KilitlemeTarihi = DateTime.Now;
                }
                else
                {
                    SeciliLmeBaglama.KilitlemeTarihi = null;
                }
            }

        }

        private void OnFormYazdir(object obj)
        {
            var raporTanim = uowRapor.RaporTanimRepo.RaporGetirFromId(12);


            //var siparisKodlari= SeciliLmeBaglama.SiparisKalemLmeDtoListe.Select(c => c.SiparisKod).Distinct().ToArray();

            string firmaSiparisNumaralari = "";


            //foreach (var item in siparisKodlari)
            //{
            //    var firmaSip= uowRapor.SiparisRepo.SiparisGetirUst(item);
            //    firmaSiparisNumaralari += firmaSip.FirmaSiparisNo + " ;";
            //}

            SeciliLmeBaglama.FirmaSiparisNumaralari = firmaSiparisNumaralari.TrimEnd(';');

            var dsObject = SeciliLmeBaglama;



            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }




        public ObservableCollection<LmeBaglama> LmeBaglamaList { get => _lmeBaglamaList; set => SetProperty(ref _lmeBaglamaList, value); }

        ObservableCollection<CariKart> musteriler;
        private bool _isSnackbarActive;

        public ObservableCollection<CariKart> Musteriler
        {
            get => musteriler;
            set => SetProperty(ref musteriler, value);
        }
        public LmeDurum LmeInfo { get; }


        private void OnKaydet(object obj)
        {
            var uowYeni = new UnitOfWork();
            var lfxKod = "";

            var sonLFX_Satir = uowYeni.SiparisRepo.SonLmeKaydiGetir();

            if (sonLFX_Satir == null)
                lfxKod = "LFX1000";
            else
                lfxKod = sonLFX_Satir.RefNo;


            int i = 0;

            foreach (var item in LmeBaglamaList)
            {
                if (item.RefNo == "Yeni")
                {
                    i = i + 1;
                    item.RefNo = "LFX" + (int.Parse(lfxKod.Replace("LFX", "")) + i);

                    uow.SiparisRepo.LmeBaglamaEkle(item);
                }
                item.IsEditRowMy = false;
            }

            uow.Commit();

        }




    }
}