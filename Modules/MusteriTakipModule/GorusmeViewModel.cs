using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using mnd.Common.Helpers;
using mnd.Logic.BC_MusteriTakip.Data;
using mnd.Logic.BC_MusteriTakip.Domain;
using mnd.Logic.Model._Ref;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence.Repositories;
using mnd.UI.Helper;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace mnd.UI.Modules.MusteriTakipModule
{
    public class GorusmeViewModel : MyDxViewModelBase
    {

        public string FormMenuAd { get; set; }
        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, () => true);
        public DelegateCommand FormUnLoadedCommand => new DelegateCommand(OnFormUnLoaded);
        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet, CanKaydet);
        public DelegateCommand KisiEkleFormAcCommand => new DelegateCommand(OnKisiEkleFormAc, () => true);
        private void OnKisiEkleFormAc()
        {
            Thread.Sleep(100);
            TempCariGorusmeKisi = new FirmaTemsilci();
            IsOpenKisiEkleForm = true;
        }
        public FirmaTemsilci TempCariGorusmeKisi { get => _tempHavuzKisi; set => SetProperty(ref _tempHavuzKisi, value); }
        public bool IsOpenKisiEkleForm { get => _isOpenKisiEkleForm; set => SetProperty(ref _isOpenKisiEkleForm, value); }
        public DelegateCommand<FirmaTemsilci> KisiEkleOkCommand => new DelegateCommand<FirmaTemsilci>(OnKisiEkleOk, true);
        private void OnKisiEkleOk(FirmaTemsilci obj)
        {
            try
            {
                var kisi = new FirmaTemsilci();
                kisi.Email = TempCariGorusmeKisi.Email;
                kisi.Tel = TempCariGorusmeKisi.Tel;
                kisi.AdSoyad = TempCariGorusmeKisi.AdSoyad;
                kisi.Unvan = TempCariGorusmeKisi.Unvan;
                kisi.CariKod = Gorusme.MusteriCariKod;
                MusteriTakipService.KaydetYadaEkle(kisi);
                CariGorusmeKisiListe.Add(kisi);
                SeciliCariEmail = kisi;
                IsOpenKisiEkleForm = false;
            }
            catch (Exception ex)
            {
                var hata = "Hata:" + ex.Message + Environment.NewLine + "Detay:" + ex.InnerException?.Message;
                MessageBox.Show(hata);
            }
        }
        public DelegateCommand<FirmaTemsilci> KisiEkleCancelCommand => new DelegateCommand<FirmaTemsilci>(OnKisiEkleCancel, true);
        private void OnKisiEkleCancel(FirmaTemsilci obj)
        {
            TempCariGorusmeKisi = null;
            IsOpenKisiEkleForm = false;
        }
        public bool SnackBarIsActive { get => _snackBarIsActive; set => SetProperty(ref _snackBarIsActive, value); }
        public string SnackBarMesaj { get => _snackbarMesaj; set => SetProperty(ref _snackbarMesaj, value); }
        public ObservableCollection<KullanimAlanTip> KullanimAlanlari { get => _kullanimAlanlari; set => SetProperty(ref _kullanimAlanlari, value); }
        public FirmaTemsilci SeciliCariEmail
        {
            get => _seciliCariEmail;
            set
            {
                if (SetProperty(ref _seciliCariEmail, value))
                {
                    if (_seciliCariEmail != null)
                    {
                        Gorusme.GorusulenKisiTel = _seciliCariEmail.Tel;
                        Gorusme.GorusulenKisiEmail = _seciliCariEmail.Email;
                        Gorusme.GorusulenKisiUnvan = _seciliCariEmail.Unvan;
                    }
                    else
                    {
                        Gorusme.GorusulenKisiTel = "";
                        Gorusme.GorusulenKisiEmail = "";
                        Gorusme.GorusulenKisiUnvan = "";
                    }
                }
            }
        }
        public PandapCariDto SeciliMusteri
        {
            get => _seciliMusteri;
            set
            {
                SetProperty(ref _seciliMusteri, value);

                if (IsLoaded)
                {
                    Gorusme.MusteriCariKod = _seciliMusteri.CariKod;
                    Gorusme.CariIsim = _seciliMusteri.CariIsim;
                    Gorusme.GorusulenKisi = _seciliMusteri.AliciFirma_YetkiliAdSoyad;
                    Gorusme.UlkeAd = _seciliMusteri.UlkeAd;
                    Gorusme.UlkeKod = _seciliMusteri.UlkeKod;
                    Gorusme.PlasiyerKod = _seciliMusteri.PlasiyerKod;
                    Gorusme.PlasiyerAdSoyad = _seciliMusteri.PlasiyerAd;
                    Gorusme.GorusulenKisiEmail = _seciliMusteri.AliciFirma_YetkiliEmail;
                    Gorusme.GorusulenKisiTel = _seciliMusteri.AliciFirma_YetkiliTel;
                    Gorusme.PandaTemsilcisi = _seciliMusteri.PandaTemsilcisi;

                    Gorusme.KullanimAlanTipKod = _seciliMusteri.KullanimAlanTipKod;

                    Gorusme.GorusulenKisiNetsisId = _seciliMusteri.AliciFirma_YetkiliNetsisId.GetValueOrDefault();

                    CariGorusmeKisiListe = MusteriTakipService.FirmaTemsilcileriniGetir(_seciliMusteri.CariKod);

                    if (Gorusme?.MusteriCariKod != null)
                    {
                        CariGorusmeKisiListe = MusteriTakipService.FirmaTemsilcileriniGetir(Gorusme.MusteriCariKod);
                        if (CariGorusmeKisiListe.Count == 0) CariGorusmeKisiListe.Add(new FirmaTemsilci { AdSoyad = "" });
                    }
                }
            }
        }
        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));
        private bool CanKaydet()
        {
            return IsEditable;
        }
        public DelegateCommand<EntityLog> EntityLogGosterCommand => new DelegateCommand<EntityLog>(OnEntityLogGoster);
        private void OnEntityLogGoster(EntityLog obj)
        {
            var gorusmeKaydi = EntityLogService.LogdanEntityGetir<Gorusme>(obj.Id);

            GorusmeViewModel vm = new GorusmeViewModel(gorusmeKaydi);

            var doc = AppPandap.DokumanOlustur("GorusmeView", vm, gorusmeKaydi.GorusulenKisi + "-" + obj.KayitTarihi);
            doc.Show();
        }

        public bool IsLoaded { get; set; }

        public List<GorusmeTip> GorusmeTipleri { get => _gorusmeTipleri; set => SetProperty(ref _gorusmeTipleri, value); }
        public List<GorusmeKonuTip> GorusmeKonuTipleri { get => _gorusmeKonuTipleri; set => SetProperty(ref _gorusmeKonuTipleri, value); }

        public List<Unvan> Unvanlar { get => _unvanlar; set => SetProperty(ref _unvanlar, value); }

        MusteriTakipRepository repo = new MusteriTakipRepository();
        private Gorusme _gorusme;
        private List<GorusmeTip> _gorusmeTipleri;
        private List<GorusmeKonuTip> _gorusmeKonuTipleri;
        private ObservableCollection<EntityLog> _entityLogs;
        private bool _snackBarIsActive;
        private string _snackbarMesaj;

        public Gorusme Gorusme {
            get => _gorusme;
            set => SetProperty(ref _gorusme, value);
        }
        public bool IsEditable { get; }
        public KayitModu KayitMod { get; set; }

        private int ModelHashCode { get; set; }
        public ObservableCollection<EntityLog> EntityLogs { get => _entityLogs; set => SetProperty(ref _entityLogs, value); }


        public ObservableCollection<FirmaTemsilci> CariGorusmeKisiListe
        {
            get => _cariGorusmeKisiListe;
            set => SetProperty(ref _cariGorusmeKisiListe, value);
        }

        public GorusmeViewModel(int id)
        {
            GorusmeTipleri = repo.GorusmeTipleriGetir();
            GorusmeKonuTipleri = repo.GorusmeKonuTipleriGetir();
            Unvanlar = repo.UnvanlariGetir();
            KullanimAlanlari = repoKullanimAlanlari.KullanimAlanlariniGetir();


            KayitMod = id == 0 ? KayitModu.Add : KayitModu.Edit;

            if (KayitMod == KayitModu.Edit)
            {
                Gorusme = repo.GorusmeGetir(id);
            }


            if (Gorusme?.MusteriCariKod != null)
            {
                CariGorusmeKisiListe = MusteriTakipService.FirmaTemsilcileriniGetir(Gorusme.MusteriCariKod);
                if (CariGorusmeKisiListe.Count == 0) CariGorusmeKisiListe.Add(new FirmaTemsilci { AdSoyad = "" });
            }

            ModelHashCode = JsonConvert.SerializeObject(Gorusme).GetHashCode();
            IsEditable = true;
        }

        public GorusmeViewModel(Gorusme gorusme)
        {


            GorusmeTipleri = repo.GorusmeTipleriGetir();
            GorusmeKonuTipleri = repo.GorusmeKonuTipleriGetir();
            Unvanlar = repo.UnvanlariGetir();
            KullanimAlanlari = repoKullanimAlanlari.KullanimAlanlariniGetir();


            Gorusme = gorusme;



            if (Gorusme.MusteriCariKod != null)
            {
                CariGorusmeKisiListe = MusteriTakipService.FirmaTemsilcileriniGetir(Gorusme.MusteriCariKod);
                if (CariGorusmeKisiListe.Count == 0) CariGorusmeKisiListe.Add(new FirmaTemsilci());
            }

            IsEditable = false;
        }

        public async void OnFormLoaded()
        {
            if (IsLoaded) return;



            EntityLogs = EntityLogService.EntityLogKayitlariGetir(Gorusme.RowGuid);

            var bagliPlasiyerKodlari = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');

            PandapCariler = repoPandapCari.PandapCarileriDetayliGetir_light(bagliPlasiyerKodlari);

            if (Gorusme?.MusteriCariKod != null)
            {
                CariGorusmeKisiListe = MusteriTakipService.FirmaTemsilcileriniGetir(Gorusme.MusteriCariKod);
                if (CariGorusmeKisiListe.Count == 0) CariGorusmeKisiListe.Add(new FirmaTemsilci { AdSoyad = "" });
            }

            IsLoaded = true;
        }

        PandapCariRepository repoPandapCari = new PandapCariRepository();
        LookUpRepository repoKullanimAlanlari = new LookUpRepository();


        private ObservableCollection<PandapCariDto> pandapCari;
        private PandapCariDto _seciliMusteri;
        private ObservableCollection<KullanimAlanTip> _kullanimAlanlari;
        private ObservableCollection<FirmaTemsilci> _cariGorusmeKisiListe;
        private FirmaTemsilci _seciliCariEmail;
        private bool _isOpenKisiEkleForm;
        private FirmaTemsilci _tempHavuzKisi;
        private List<Unvan> _unvanlar;

        public ObservableCollection<PandapCariDto> PandapCariler
        {
            get => pandapCari;
            set => SetProperty(ref pandapCari, value);
        }



        private void OnKaydet()
        {
            if (Gorusme.GorusmeDetay != null)
            {
                if (Gorusme.GorusmeDetay.Length > 2000)
                {
                    MessageBox.Show("Görüşme detayı 2000 harften fazla olamaz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            Gorusme.Ekleyen = AppPandap.AktifKullanici.KullaniciId;
            Gorusme.PlasiyerKod = SeciliMusteri.PlasiyerKod;
            Gorusme.GorusulenKisi = SeciliCariEmail.AdSoyad;

            SeciliCariEmail.Tel = Gorusme.GorusulenKisiTel;
            SeciliCariEmail.Unvan = Gorusme.GorusulenKisiUnvan;
            SeciliCariEmail.Email = Gorusme.GorusulenKisiEmail;

            var hata_text = ValidateGorusme(Gorusme);

            if (hata_text != "")
            {
                MessageBox.Show(hata_text, "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (KayitMod != KayitModu.Add && KayitDegistiMi == false)
            {
                BoundMessageQueue.Enqueue("Değişiklik Yok", true);
                return;
            }

            if (KayitMod == KayitModu.Edit && KayitDegistiMi == false)
            {
                BoundMessageQueue.Enqueue("Değişiklik Yok", true);
                return;
            }


            var cev = MessageBox.Show("Değişiklikleri kaydetmek istiyormusunuz", "Pandap",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);




            if (cev == MessageBoxResult.OK)
            {

                try
                {
                    if (KayitMod == KayitModu.Add)
                    {
                        Gorusme.UlkeAd = _seciliMusteri.UlkeAd;
                        Gorusme.UlkeKod = _seciliMusteri.UlkeKod;
                        Gorusme.CariIsim = _seciliMusteri.CariIsim;
                        Gorusme.PlasiyerKod = _seciliMusteri.PlasiyerKod;
                        Gorusme.PlasiyerAdSoyad = _seciliMusteri.PlasiyerAd;
                        Gorusme.PandaTemsilcisi = _seciliMusteri.PandaTemsilcisi;


                        repo.GorusmeEkle(Gorusme);

                        MusteriTakipService.KaydetYadaEkle(SeciliCariEmail);

                        repo.Kaydet();
                        var entityLog = EntityLogService.EntityLogla(Gorusme);
                        EntityLogs.Add(entityLog);
                        Messenger.Default.Send(new KayitMesaj(Gorusme, KayitIslemTip.Eklendi));

                    }
                    else
                    {
                        MusteriTakipService.KaydetYadaEkle(SeciliCariEmail);

                        repo.Kaydet();

                        var entityLog = EntityLogService.EntityLogla(Gorusme);
                        EntityLogs.Add(entityLog);

                        Messenger.Default.Send(new KayitMesaj(Gorusme, KayitIslemTip.Degisti));
                        BoundMessageQueue.Enqueue("kayıt tamam", true);
                    }


                    ModelHashCode = JsonConvert.SerializeObject(Gorusme).GetHashCode();
                    KayitMod = KayitModu.Edit;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }



            }


        }

        private string ValidateGorusme(Gorusme gorusme)
        {

            var hataText = "";

            if (String.IsNullOrEmpty(Gorusme.GorusulenKisi))
                hataText += "Görüşülen kişi adı boş olamaz" + Environment.NewLine;

            if (String.IsNullOrEmpty(Gorusme.GorusulenKisiEmail))
                hataText += "Görüşülen kişi maili boş olamaz" + Environment.NewLine;

            if (String.IsNullOrEmpty(Gorusme.GorusulenKisiTel))
                hataText += "Görüşülen kişi telefonunu boş olamaz" + Environment.NewLine;

            if (String.IsNullOrEmpty(Gorusme.GorusulenKisiUnvan))
                hataText += "Görüşülen kişi ünvan olamaz" + Environment.NewLine;
            return hataText;


        }

        private void OnFormUnLoaded()
        {
            //if (KayitDegistiMi && IsEditable == true)
            //{
            //    var cev = MessageBox.Show("Değişiklikleri kaydetmek istiyormusunuz", "Pandap",
            //      MessageBoxButton.OKCancel, MessageBoxImage.Question);

            //    if (cev == MessageBoxResult.OK)
            //    {
            //        repo.Kaydet();
            //        ModelHashCode = JsonConvert.SerializeObject(Gorusme).GetHashCode();
            //        KayitMod = KayitModu.Edit;

            //        MessageBox.Show("Başarıyla kayıt yapıldı");

            //        Messenger.Default.Send(new KayitMesaj(Gorusme, KayitIslemTip.Degisti));

            //        var entityLog = EntityLogService.EntityLogla(Gorusme);
            //        EntityLogs.Add(entityLog);
            //    }
            //}

        }

        public bool KayitDegistiMi
        {
            get => JsonConvert.SerializeObject(Gorusme).GetHashCode() != ModelHashCode;
        }


    }
}
