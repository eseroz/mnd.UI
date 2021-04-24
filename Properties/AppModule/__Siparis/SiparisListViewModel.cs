using System;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using Pandap.Logic.Helper;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Model._DTOs;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._Mesajlasma;
using Pandap.UI.Helper;
using Pandap.Logic;

namespace Pandap.UI.AppModule.__Siparis
{
    public class SiparisListViewModel : MyDxViewModelBase
    {
        public DelegateCommand SiparisYeniCommand => new DelegateCommand(SiparisYeni, canSiparisYeni);
        public DelegateCommand SiparisEditCommand => new DelegateCommand(SiparisEdit, canSiparisEdit);
        public DelegateCommand SiparisSilCommand => new DelegateCommand(SiparisSil, canSiparisSil);

        public DelegateCommand VerileriTazeleCommand => new DelegateCommand(VerileriTazele, true);

        bool sadeceOnayliSiparisKalemleri = true;
        public bool SadeceOnayliSiparisKalemleri
        {
            get => sadeceOnayliSiparisKalemleri;
            set => SetProperty(ref sadeceOnayliSiparisKalemleri, value);
        }
        bool sadeceAcikSiparisKalemleri = true;
        public bool SadeceAcikSiparisKalemleri
        {
            get => sadeceAcikSiparisKalemleri;
            set => SetProperty(ref sadeceAcikSiparisKalemleri, value);
        }


        ObservableCollection<AlasimMiktarDto> alasimDurumlari;
        public ObservableCollection<AlasimMiktarDto> AlasimDurumlari
        {
            get => alasimDurumlari;
            set => SetProperty(ref alasimDurumlari, value);
        }


        private void VerileriTazele()
        {
            this.Load(this._planlamaKapatilanlariGoster);
        }

        public DelegateCommand PaketlenenMiktariGuncelleCommand => new DelegateCommand(ToplamPaketMiktariGuncelle, CanToplamPaketMiktariGuncelle);

        private bool CanToplamPaketMiktariGuncelle()
        {
            return (PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA_YONETICI
                    || PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA
                    || PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.YONETICI) &&
                    SeciliSiparisDTO != null;
        }

        private int _toplamPaketMiktari;


        public int ToplamPaketMiktari
        {
            get => _toplamPaketMiktari;
            set => SetProperty(ref _toplamPaketMiktari, value);
        }


        string gridLayoutFileName;
        public string GridLayoutFileName
        {
            get => gridLayoutFileName;
            set => SetProperty(ref gridLayoutFileName, value);

        }

        private void ToplamPaketMiktariGuncelle()
        {
            var maxpaketyuzde = PandapGlobal.UygulamaAyarlar.PaketMax_UEmri_yuzde;
            if (ToplamPaketMiktari > SeciliSiparisDTO.MiktarKg * (maxpaketyuzde + 100) / 100)
            {
                MessageBoxService.ShowMessage($"Paketlenecek miktar sipariş miktarının %{maxpaketyuzde} 'unu geçemez", "Pandap", MessageButton.OK, MessageIcon.Warning);
                return;
            }

            var cevap = MessageBoxService.ShowMessage("Paketleme miktarı güncellenecek.Onaylıyormusunuz", "Pandap", MessageButton.OKCancel, MessageIcon.Question);

            if (cevap != MessageResult.OK) return;

            UnitOfWork uow = new UnitOfWork();
            uow.SiparisRepo.SiparisPaketlenenToplamGuncelle(SeciliSiparisDTO.SiparisKod, ToplamPaketMiktari);

            SeciliSiparisDTO.PaketlenenMiktarKg = ToplamPaketMiktari;


            SeciliSiparisDTO.PaketlenenTumMiktarKg = ToplamPaketMiktari + SeciliSiparisDTO.BobinPaketToplamKg;
            SeciliSiparisDTO.KalanIsyukuKg = SeciliSiparisDTO.MiktarKg - SeciliSiparisDTO.PaketlenenTumMiktarKg;

            ToplamPaketMiktari = 0;

        }

        public DelegateCommand SiparisKapatCommand => new DelegateCommand(SiparisKapat, CanSiparisKapat);

        private bool CanSiparisKapat()
        {
            var yetkiliRolMu = PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA_YONETICI ||
                  PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.YONETICI;

            return yetkiliRolMu;
        }

        private void SiparisKapat()
        {
            if (SeciliSiparisDTO.AktifUretimEmriSayisi > 0)
            {
                var mesaj = $"Siparişe ait { SeciliSiparisDTO.AktifUretimEmriSayisi}  adet üretim emri bulunmaktadır." + Environment.NewLine + Environment.NewLine +
                             "Siparişin kapatılabilmesi için üretim emirlerinin kapatılması gerekmektedir. " + Environment.NewLine +
                             "Planlamadan kapatılmasını talep edebilirsiniz";

                MessageBoxService.ShowMessage(mesaj, "Pandap", MessageButton.OK, MessageIcon.Information);
                return;
            }

            uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.KAPALISIPARIS, SeciliAnaMenu);
            SiparisListe.Remove(SeciliSiparisDTO);
        }

        public DelegateCommand<object> YerlesimleriKaydetCommand => new DelegateCommand<object>(YerlesimKaydet, true);

        private void YerlesimKaydet(object o)
        {
        }

        public DelegateCommand KarantinayaAktarCommand => new DelegateCommand(KarantinayaAktar, CanKarantinayaAktar);

        private bool CanKarantinayaAktar()
        {
            var yetki = SeciliAnaMenu == SiparisSurecDurum.MUSTERIONAYLI
                      || SeciliAnaMenu == SiparisSurecDurum.PLANLAMADA
                       || SeciliAnaMenu == SiparisSurecDurum.MUSTERIONAYINDA
                       || SeciliAnaMenu == SiparisSurecDurum.YONETICIONAYINDA

                        || SeciliAnaMenu == SiparisSurecDurum.SATISTA;
            return yetki && ((PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA_YONETICI
                            || PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.YONETICI));
        }

        public DelegateCommand KarantinadanGeriAlCommand => new DelegateCommand(KarantinadanGeriAl, canKarantinadanGeriAl);

        private bool canKarantinadanGeriAl()
        {
            return SeciliAnaMenu == SiparisSurecDurum.SIPARISKARANTINA && ((PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA_YONETICI
                            || PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.YONETICI));
        }

        private void KarantinayaAktar()
        {
            uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.SIPARISKARANTINA, SeciliAnaMenu);
            SiparisListe.Remove(SeciliSiparisDTO);
        }

        private void KarantinadanGeriAl()
        {
            // Bir önceki süreçte nerdeyse oraya geri alınacak--------------
            uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SeciliSiparisDTO.SiparisSurecDurumOnceki, SiparisSurecDurum.SIPARISKARANTINA);
            SiparisListe.Remove(SeciliSiparisDTO);
        }

        private bool dataIsLoading;

        public bool DataIsLoading
        {
            get => dataIsLoading;
            set => SetProperty(ref dataIsLoading, value);
        }

        public virtual IDialogService SiparisEditDialogService { get { return ServiceContainer.GetService<IDialogService>("SiparisEditDialogService"); } }

        private bool canSiparisSil()
        {
            if (SeciliSiparisDTO == null) return false;

            var kalemListe = SeciliSiparisDTO.SiparisKalemDTO_List.ToList();
            return !kalemListe.Any(c => c.UretimEmirleri.Count > 0) && SeciliSiparisDTO.SiparisSurecDurum == SiparisSurecDurum.SATISTA;
        }

        private void SiparisSil()
        {
            var sonuc = MessageBoxService.ShowMessage("1 Kayıt Silinecek", "Silinecek", MessageButton.OKCancel, MessageIcon.Error);

            if (sonuc == MessageResult.Cancel) return;

            uow.SiparisRepo.Remove(SeciliSiparisDTO);
            SiparisListe.Remove(SeciliSiparisDTO);
        }

        public DelegateCommand<object> OnayCommand => new DelegateCommand<object>(Onayla, canOnayla);
        public DelegateCommand PlanlamayaGonderCommand => new DelegateCommand(PlanlamayaGonder, canPlanlamayaGonder);

        public DelegateCommand<SiparisDTO> PandapMessangerAcCommand => new DelegateCommand<SiparisDTO>(PandapMessangerAc);

        private readonly UnitOfWork uow;

        public string SeciliAnaMenu { get; set; }

        private ObservableCollection<SiparisDTO> _siparisListe;

        public ObservableCollection<SiparisDTO> SiparisListe
        {
            get => _siparisListe;
            set
            {
              
               if( SetProperty(ref _siparisListe, value))
                {
                   
                    AlasimDurumlari = uow.SiparisKalemRepo.AlasimMiktarlariGetir(SeciliAnaMenu);
             
                }
            }
        }

        private SiparisDTO _seciliSiparisDto;

        public SiparisDTO SeciliSiparisDTO
        {
            get => _seciliSiparisDto;
            set => SetProperty(ref _seciliSiparisDto, value);
        }

        public bool FiyatSutunGorunsunMu => PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.YONETICI ||
                                            PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA_YONETICI ||
                                            PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.SATIS;

        private ObservableCollection<string> _siparisDurumlari = new ObservableCollection<string>();

        public ObservableCollection<string> SiparisDurumlari
        {
            get => _siparisDurumlari;
            set => SetProperty(ref _siparisDurumlari, value);
        }

        public SiparisListViewModel()
        {
            uow = new UnitOfWork();

            _siparisDurumlari.Add(SiparisSurecDurum.SATISTA);
            _siparisDurumlari.Add(SiparisSurecDurum.PLANLAMADA);
            _siparisDurumlari.Add(SiparisSurecDurum.YONETICIONAYINDA);
            _siparisDurumlari.Add(SiparisSurecDurum.MUSTERIONAYINDA);
            _siparisDurumlari.Add(SiparisSurecDurum.MUSTERIONAYLI);
            _siparisDurumlari.Add(SiparisSurecDurum.OPERASYONDA);
            _siparisDurumlari.Add(SiparisSurecDurum.URETIMDE);

            Messenger.Default.Register<Siparis>(this, SiparisKayitMessageData);


            PandapSqlDependency.SqlMesajOnChanged += (s, e) =>
            {
                if (e.Entity.RefEntityGuid == PandapGlobal.UygulamaAyarlar.RowGuid.GetValueOrDefault()) return;

                var sDto = SiparisListe.FirstOrDefault(c => c.RowGuid == e.Entity.RefEntityGuid);
                if (sDto != null)
                {
                    sDto.MesajSayisi += 1;

                    if(e.Entity.Gonderen!=PandapGlobal.AktifKullanici.AdSoyad)
                        sDto.OkunmamisMesajSayisi += 1;
                }
            };

            var x = this.GridLayoutFileName;

        }

        public  void SiparisKayitMessageData(Siparis siparis)
        {
            //var sonuc = await uow.SiparisRepo.SiparisDTO_ListeGetirAsync(PandapGlobal.AktifKullanici.BagliKullanicilar.Split(';'),
            //               "Tümü", null, PlanlamaKapatılanSiparislerGoster,siparis.SiparisKod);

            //if (sonuc == null) return;

            ////--------------------------------------------------------
            //Mapper.Map<SiparisDTO, SiparisDTO>(sonuc[0], SeciliSiparisDTO);
        }

        private bool PlanlamayaGonderebilirMi()
        {
            var s = PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.SATIS &&
               SeciliAnaMenu == SiparisSurecDurum.SATISTA;

            return s;
        }

        private bool canPlanlamayaGonder()
        {
            return PlanlamayaGonderebilirMi();
        }

        private void PlanlamayaGonder()
        {
            uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.PLANLAMADA);
            SiparisListe.Remove(SeciliSiparisDTO);
        }

        private bool canOnayla(object arg)
        {
            return OnayIsteniyorMu();
        }

        private bool OnayIsteniyorMu()
        {
            if (SeciliAnaMenu == SiparisSurecDurum.PLANLAMADA)
            {
                return PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA || PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA_YONETICI;
            }

            if (SeciliAnaMenu == SiparisSurecDurum.YONETICIONAYINDA)
            {
                return PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.YONETICI;
            }

            if (SeciliAnaMenu == SiparisSurecDurum.MUSTERIONAYINDA)
            {
                return PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.SATIS;
            }

            return false;
        }

        private void Onayla(object onayCevap)
        {
            var onaylandi = onayCevap.ToString() == "Onaylandı" ? true : false;

            if (SeciliSiparisDTO.SiparisSurecDurum == SiparisSurecDurum.PLANLAMADA)
            {
                if (onaylandi)
                    uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.YONETICIONAYINDA);
                else
                    uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.SATISTA);
            }

            if (SeciliSiparisDTO.SiparisSurecDurum == SiparisSurecDurum.YONETICIONAYINDA)
            {
                if (onaylandi)
                    uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.MUSTERIONAYINDA);
                else
                    uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.SATISTA);
            }

            if (SeciliSiparisDTO.SiparisSurecDurum == SiparisSurecDurum.MUSTERIONAYINDA)
            {
                if (onaylandi)
                    uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.MUSTERIONAYLI);
                else
                    uow.SiparisRepo.SiparisSurecDurumDegistir(SeciliSiparisDTO, SiparisSurecDurum.SATISTA);
            }

            SiparisListe.Remove(SeciliSiparisDTO);
        }

        private bool kapasitifSiparislerGoster = false;

        public bool KapasitifSiparislerGoster
        {
            get => kapasitifSiparislerGoster;
            set
            {
                SetProperty(ref kapasitifSiparislerGoster, value);
                this.Load(PlanlamaKapatılanSiparislerGoster);
            }
        }

        private bool normalSiparislerGoster = true;

        public bool NormalSiparislerGoster
        {
            get => normalSiparislerGoster;
            set
            {
                SetProperty(ref normalSiparislerGoster, value);
                this.Load(PlanlamaKapatılanSiparislerGoster);
            }
        }

        private bool planlamaKapatılanSiparislerGoster;

        public bool PlanlamaKapatılanSiparislerGoster
        {
            get => planlamaKapatılanSiparislerGoster;
            set
            {
                SetProperty(ref planlamaKapatılanSiparislerGoster, value);
            }
        }

        private string listeSiparisSurecDurum;

        public string ListeSiparisSurecDurum
        {
            get
            {
                return listeSiparisSurecDurum;
            }
            set
            {
                SetProperty(ref listeSiparisSurecDurum, value);
            }
        }


        bool _planlamaKapatilanlariGoster;
        public async void Load(bool planlamaKapatilanlariGoster)
        {
            DataIsLoading = true;

            _planlamaKapatilanlariGoster = planlamaKapatilanlariGoster;

            PlanlamaKapatılanSiparislerGoster = planlamaKapatilanlariGoster;

            string strSiparisSurecDurum = ListeSiparisSurecDurum ?? "Tümü";

            bool? kapasitifNormalTumu = null;

            if (KapasitifSiparislerGoster == true && NormalSiparislerGoster == false) kapasitifNormalTumu = true;
            if (KapasitifSiparislerGoster == false && NormalSiparislerGoster == true) kapasitifNormalTumu = false;
            if (KapasitifSiparislerGoster == NormalSiparislerGoster == true) kapasitifNormalTumu = null;

            SeciliAnaMenu = strSiparisSurecDurum;

            if (strSiparisSurecDurum == "SEVKEHAZIR") strSiparisSurecDurum = "Tümü";

            var sonuc = await uow.SiparisDtoRepo.SiparisDTO_ListeGetirAsync(PandapGlobal.AktifKullanici.BagliKullanicilar.Split(';'),
                                strSiparisSurecDurum, kapasitifNormalTumu, PlanlamaKapatılanSiparislerGoster,null,PandapGlobal.AktifKullanici.KullaniciId);

            SiparisListe = sonuc.ToObservableCollection();

            if(SeciliAnaMenu== "SEVKEHAZIR")
                SiparisListe = SiparisListe.Where(c => c.DepodaUrunSayisi > 0).ToObservableCollection();
        

            OnPropertyChanged(nameof(SiparisIslemVarmi));

            DataIsLoading = false;

        }

        private void PandapMessangerAc(SiparisDTO row)
        {
            MesajlasmaViewModel v = new MesajlasmaViewModel(row.RowGuid, PandapGlobal.AktifKullanici.AdSoyad);
            row.OkunmamisMesajSayisi = 0;
      
            MesajlasmaWindow w = new MesajlasmaWindow();
            w.DataContext = v;

            w.ShowDialog();

           

        }

        private void SiparisYeni()
        {
            SiparisAc(KayitModu.Yeni);
        }

        private bool canSiparisYeni() => PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.SATIS;

        private void SiparisEdit()
        {
            if (PandapGlobal.AktifKullanici.KullaniciRol != KullaniciRolleri.PLANLAMA)
            {
                SiparisAc(KayitModu.Edit);
            }
        }

        private bool canSiparisEdit() => SeciliSiparisDTO != null && PandapGlobal.KullaniciRol != KullaniciRolleri.PLANLAMA;

        public bool SiparisIslemVarmi
        {
            get
            {
                var s = OnayIsteniyorMu() || PlanlamayaGonderebilirMi();

                return s;
            }
        }

        public KayitModu KayitModu { get; set; }




        private void SiparisAc(KayitModu kayitModu)
        {
            SiparisViewModel _siparisViewModel = new SiparisViewModel();
            this.KayitModu = kayitModu;

            if (this.KayitModu == KayitModu.Yeni)
            {
                _siparisViewModel.SiparisKayitModu = KayitModu.Yeni;
                _siparisViewModel.Load(null);
            }
            else
            {
                _siparisViewModel.SiparisKayitModu = KayitModu.Edit;
                _siparisViewModel.Load(SeciliSiparisDTO.SiparisKod);
            }

            var sonuc = SiparisEditDialogService.ShowDialog(
                    dialogCommands: null,
                    title: "Siparis Düzenlenle",
                    viewModel: _siparisViewModel,
                    documentType: "SiparisView"
            );
        }
    }
}