using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.Logic.Persistence;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    public class MusteriAramaListVM : MyBindableBase
    {
        private ObservableCollection<PotansiyelDisiMusteri> potansyelMusteriListesi;
        private PotansiyelDisiMusteri seciliPotansiyelDisiMusteri;
        private Visibility potansiyelDisi;
        private Visibility potansiyel;

        private UnitOfWork uow = new UnitOfWork();
        PotansiyelDisiRepository repo = new PotansiyelDisiRepository();

        public string[] bagliPlasiyerKodlari = null;
        public string MusteriGrubuAdi { get; set; }
        public Visibility Potansiyel { get => potansiyel; set => SetProperty(ref potansiyel, value); }
        public Visibility PotansiyelDisi { get => potansiyelDisi; set => SetProperty(ref potansiyelDisi, value); }

        private bool yoneticiMi;
        public bool YoneticiMi
        {
            get
            {
                yoneticiMi = (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI); return yoneticiMi;
            }
            set { SetProperty(ref yoneticiMi, value); }
        }

        public ObservableCollection<PotansiyelDisiMusteri> PotansyelMusteriListesi
        {
            get => potansyelMusteriListesi;
            set => SetProperty(ref potansyelMusteriListesi, value);
        }

        public PotansiyelDisiMusteri SeciliPotansiyelDisiMusteri
        {
            get => seciliPotansiyelDisiMusteri;
            set
            {
                SetProperty(ref seciliPotansiyelDisiMusteri, value);
            }
        }

        public DelegateCommand<object> PotansiyelYapCommand => new DelegateCommand<object>(OnPotansiyelYap, true);

        private void OnPotansiyelYap(object _row)
        {
            var musteri = (PotansiyelDisiMusteri)_row;
            musteri.MusteriGrubuAdi = "Potansiyel";
            PotansyelMusteriListesi.Remove(musteri);
            repo.Kaydet();
        }

        public DelegateCommand<object> PotansiyelDisiYapCommand => new DelegateCommand<object>(OnPotansiyelDisiYap, true);

        private void OnPotansiyelDisiYap(object _row)
        {
            var musteri = (PotansiyelDisiMusteri)_row;
            musteri.MusteriGrubuAdi = "Potansiyel Disi";
            PotansyelMusteriListesi.Remove(musteri);
            repo.Kaydet();
        }

        public DelegateCommand<object> AramaAddEditCommand => new DelegateCommand<object>(OnAramaEkleDuzenle, true);
        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);

        public MusteriAramaListVM(string FormAd)
        {
            MusteriGrubuAdi = FormAd.Trim();
            Potansiyel = Visibility.Hidden;
            PotansiyelDisi = Visibility.Hidden;

            if (MusteriGrubuAdi == "Potansiyel") {
                Potansiyel = Visibility.Hidden;
                PotansiyelDisi = Visibility.Visible;
            }

            if (MusteriGrubuAdi == "Potansiyel Disi") {
                Potansiyel = Visibility.Visible;
                PotansiyelDisi = Visibility.Hidden; 
            }

            if (AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari != null)
            {
                bagliPlasiyerKodlari = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');
            }
            else
            {
                bagliPlasiyerKodlari = uow.PlasiyerRepo.PlasiyerKodlari();
            }

            PotansyelMusteriListesi = repo.PTD_Aramalari_Getir(bagliPlasiyerKodlari, MusteriGrubuAdi);

            Messenger.Default.Register<PTD_MusteriAramaEklendiEvents>(this, OnAramaEklendi);
            Messenger.Default.Register<PTD_MusteriAramaGuncellendiEvent>(this, OnAramaGuncellendi);

        }
        private void OnEkranYenile()
        {
            PotansyelMusteriListesi = new ObservableCollection<PotansiyelDisiMusteri>();
            PotansyelMusteriListesi = repo.PTD_Aramalari_Getir(bagliPlasiyerKodlari, MusteriGrubuAdi);
        }
        private void OnAramaEkleDuzenle(object _row)
        {
            if (_row == null) {

                _row = new PotansiyelDisiMusteriArama();

                PotansiyelDisiMusteriArama _seciliarama = (PotansiyelDisiMusteriArama)_row;
                PTD_AramaEditVM vm = new PTD_AramaEditVM(repo, PotansyelMusteriListesi);
                vm.MusteriGrubuAdi = MusteriGrubuAdi;
                vm.KayitEditMi = false;
                vm.SeciliPotansiyelDisiMusteriArama = _seciliarama;
                IDocument doc = AppPandap.pDocumentManagerService.CreateDocument("PTD_AramaEditView", vm);



                doc.Title = MusteriGrubuAdi + "> Yeni Arama"; 
                    
                doc.DestroyOnClose = true;
                doc.Show();

            } else {

                PotansiyelDisiMusteriArama _seciliarama = (PotansiyelDisiMusteriArama)_row;
                PTD_AramaEditVM vm = new PTD_AramaEditVM(repo, PotansyelMusteriListesi);
                vm.MusteriGrubuAdi = MusteriGrubuAdi;

                var musteri = repo.getMusteri(_seciliarama.PotansiyelDisiMusteriId);
                vm.SeciliPotansiyelDisiMusteriArama = _seciliarama;
                
                vm.KayitEditMi = true;
                IDocument doc = AppPandap.pDocumentManagerService.CreateDocument("PTD_AramaEditView", vm);  
                            
                doc.Title = MusteriGrubuAdi + "> " + musteri.MusteriUnvan;

                doc.DestroyOnClose = true;
                doc.Show();

            } 
        }

        private void OnAramaGuncellendi(PTD_MusteriAramaGuncellendiEvent obj)
        {
            //bunlar ne?????
            //var arama = PotansyelMusteriListesi.FirstOrDefault(x => x.Id == obj.Arama.Id);

            //var indexRow = PotansyelMusteriListesi.IndexOf(arama);

            //MusteriAramalar.Remove(arama);

            //var guncel= repo.Ptd_AramaGetirNoTrack(obj.Arama.Id);

            //PotansyelMusteriListesi.Insert(indexRow, guncel);

            //SeciliArama = guncel_seyahat;
        }
        private void OnAramaEklendi(PTD_MusteriAramaEklendiEvents obj)
        {     //bunlar ne????? 
            //MusteriAramalar.Insert(0, obj.Arama);
            //SeciliArama = obj.Arama;
        }
    }
}
