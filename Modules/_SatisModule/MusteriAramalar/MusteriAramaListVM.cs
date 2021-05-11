using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    public class MusteriAramaListVM : MyDxViewModelBase
    {
        private UnitOfWork uow = new UnitOfWork();
        PotansiyelDisiRepository repo = new PotansiyelDisiRepository();

        public string PotansiyelPopupBoxMenuText { 
            get => potansiyelPopupBoxMenuText;
            set { 
                SetProperty(ref potansiyelPopupBoxMenuText, value);
            }
        }

        public string[] bagliPlasiyerKodlari = null;
        public string MusteriGrubuAdi
        {
            get => musteriGrubuAdi;
            set
            {
                SetProperty(ref musteriGrubuAdi, value);
                if (musteriGrubuAdi == "Potansiyel")
                {
                    PotansiyelPopupBoxMenuText = "Potansiyel Dışı Yap";
                }
                else
                {
                    PotansiyelPopupBoxMenuText = "Potansiyel Yap";
                }
            }
        }

        private bool yoneticiMi;
        public bool YoneticiMi
        {
            get
            {
                yoneticiMi = (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI);
                return yoneticiMi;
            }
            set { SetProperty(ref yoneticiMi, value); }
        }

        public DelegateCommand<object> PotansiyelDisiYapCommand => new DelegateCommand<object>(OnPotansiyelDisiYap, true);
        private void OnPotansiyelDisiYap(object _row)
        {
            var musteri = (PotansiyelDisiMusteri)_row;
            if (PotansiyelPopupBoxMenuText == "Potansiyel Dışı Yap")
            {
                musteri.MusteriGrubuAdi = "Potansiyel Disi";
                PotansyelMusteriListesi.Remove(musteri);
            }

            if (PotansiyelPopupBoxMenuText == "Potansiyel Yap")
            {
                musteri.MusteriGrubuAdi = "Potansiyel";
                PotansyelMusteriListesi.Remove(musteri);
            }
            repo.Kaydet();

        }
        public DelegateCommand<object> AramaAddEditCommand => new DelegateCommand<object>(OnAramaEkleDuzenle, true);
        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);
        public DelegateCommand YerlesimiKaydetCommand => new DelegateCommand(YerlesimiKaydet);
        private ObservableCollection<PotansiyelDisiMusteri> potansyelMusteriListesi;
        public ObservableCollection<PotansiyelDisiMusteri> PotansyelMusteriListesi
        {
            get => potansyelMusteriListesi;
            set => SetProperty(ref potansyelMusteriListesi, value);
        }
        private PotansiyelDisiMusteri seciliPotansiyelDisiMusteri;
        public PotansiyelDisiMusteri SeciliPotansiyelDisiMusteri
        {
            get => seciliPotansiyelDisiMusteri;
            set
            {
                SetProperty(ref seciliPotansiyelDisiMusteri, value);
            }
        }
        private CustomColum plasiyerColumn;

        private string musteriGrubuAdi;
        private string potansiyelPopupBoxMenuText;

        public CustomColum PlasiyerColumn
        {
            get
            {
                plasiyerColumn = new CustomColum();
                if (YoneticiMi)
                {
                    plasiyerColumn.Visible = true;
                    plasiyerColumn.GroupIndex = 0;
                }
                else
                {
                    plasiyerColumn.Visible = false;
                    plasiyerColumn.GroupIndex = -1;
                }
                return plasiyerColumn;
            }
            set => SetProperty(ref plasiyerColumn, value);
        }
        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");
        public MusteriAramaListVM(string FormAd)
        {

            MusteriGrubuAdi = FormAd.Trim();



            //if (MusteriGrubuAdi == "Potansiyel")
            //{
            //    Potansiyel = Visibility.Hidden;
            //    PotansiyelDisi = Visibility.Visible;
            //}

            //if (MusteriGrubuAdi == "Potansiyel Disi")
            //{
            //    Potansiyel = Visibility.Visible;
            //    PotansiyelDisi = Visibility.Hidden;
            //}

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
        public void YerlesimiKaydet()
        {
            ExportService1.SaveLayout("MusteriAramaList.xml");
        }
        private void OnEkranYenile()
        {
            repo = new PotansiyelDisiRepository();
            PotansyelMusteriListesi = new ObservableCollection<PotansiyelDisiMusteri>();
            PotansyelMusteriListesi = repo.PTD_Aramalari_Getir(bagliPlasiyerKodlari, MusteriGrubuAdi);
        }
        private void OnAramaEkleDuzenle(object _row)
        {


            if (_row == null)
            {

                _row = new PotansiyelDisiMusteriArama();

                PotansiyelDisiMusteriArama _seciliarama = (PotansiyelDisiMusteriArama)_row;
                PTD_AramaEditVM vm = new PTD_AramaEditVM(repo, PotansyelMusteriListesi);

                vm.Ulkeler = repo.UlkeleriGetir();

                vm.MusteriGrubuAdi = MusteriGrubuAdi;
                vm.KayitEditMi = false;
                vm.SeciliPotansiyelDisiMusteriArama = _seciliarama;
                var musteri = repo.getMusteri(_seciliarama.PotansiyelDisiMusteriId);
                vm.SeciliMusteri = musteri;
                IDocument doc = AppPandap.pDocumentManagerService.CreateDocument("PTD_AramaEditView", vm);

                vm.AramaEditDocument = doc;


                doc.Title = MusteriGrubuAdi + "> Yeni Arama";

                doc.DestroyOnClose = true;
                doc.Show();

            }
            else
            {

                PotansiyelDisiMusteriArama _seciliarama = (PotansiyelDisiMusteriArama)_row;
                PTD_AramaEditVM vm = new PTD_AramaEditVM(repo, PotansyelMusteriListesi);
                vm.Ulkeler = repo.UlkeleriGetir();
                vm.MusteriGrubuAdi = MusteriGrubuAdi;

                var musteri = repo.getMusteri(_seciliarama.PotansiyelDisiMusteriId);
                vm.SeciliPotansiyelDisiMusteriArama = _seciliarama;


                var musterin = repo.getMusteri(_seciliarama.PotansiyelDisiMusteriId);
                vm.SeciliMusteri = musterin;


                //var ulke = vm.Ulkeler.Where(p => p.UlkeAdi == musterin.UlkeAdi).FirstOrDefault();
                //vm.SeciliUlke = ulke;

                vm.KayitEditMi = true;
                IDocument doc = AppPandap.pDocumentManagerService.CreateDocument("PTD_AramaEditView", vm);
                vm.AramaEditDocument = doc;

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
