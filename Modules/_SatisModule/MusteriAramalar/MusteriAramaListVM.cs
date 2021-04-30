using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.Logic.Persistence;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    public class MusteriAramaListVM : MyBindableBase
    {
        private UnitOfWork uow = new UnitOfWork();
        PotansiyelDisiRepository repo = new PotansiyelDisiRepository();

        public string[] bagliPlasiyerKodlari = null;
        public string MusteriGrubuAdi { get; set; }
        private bool yoneticiMi;
        public bool YoneticiMi { get => yoneticiMi; set => SetProperty(ref yoneticiMi, value); }

        private ObservableCollection<PotansiyelDisiMusteri> potansyelMusteriListesi;
        private PotansiyelDisiMusteri seciliPotansiyelDisiMusteri;
        private PotansiyelDisiMusteriArama seciliArama;

        public ObservableCollection<PotansiyelDisiMusteri> PotansyelMusteriListesi
        {
            get => potansyelMusteriListesi;
            set => SetProperty(ref potansyelMusteriListesi, value);
        }

        public PotansiyelDisiMusteri SeciliPotansiyelDisiMusteri
        {
            get => seciliPotansiyelDisiMusteri;
            set => SetProperty(ref seciliPotansiyelDisiMusteri, value);
        }

        public DelegateCommand<object> AramaAddEditCommand => new DelegateCommand<object>(OnAramaEkleDuzenle, true);
        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);

        public MusteriAramaListVM(string FormAd)
        {
            MusteriGrubuAdi = FormAd;

            YoneticiMi = (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI);

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
            repo.Kaydet();
            Messenger.Default.Send(new PTD_MusteriAramaGuncellendiEvent(SeciliArama));
            //PotansyelMusteriListesi = new ObservableCollection<PotansiyelMusteriDTO>();
            //PotansyelMusteriListesi = repo.PTD_Aramalari_Getir(bagliPlasiyerKodlari, MusteriGrubuAdi);
        }
        public PotansiyelDisiMusteriArama SeciliArama { get => seciliArama; set => SetProperty(ref seciliArama, value); }
        private void OnAramaEkleDuzenle(object _row)
        {

            PotansiyelDisiMusteriArama _seciliarama = (PotansiyelDisiMusteriArama)_row;
            SeciliArama = _seciliarama;
            PTD_AramaEditVM vm = new PTD_AramaEditVM(repo);
            vm.SeciliPotansiyelDisiMusteriArama = SeciliArama;            

            IDocument doc = AppPandap.pDocumentManagerService.CreateDocument("PTD_AramaEditView", vm);
            doc.Title = MusteriGrubuAdi + ">" + _seciliarama.PotansiyelDisiMusteri.MusteriUnvan + " Arama geçmişi..";


            doc.DestroyOnClose = true;
            doc.Show();
        }


        private void OnAramaGuncellendi(PTD_MusteriAramaGuncellendiEvent obj)
        {
            //var arama = PotansyelMusteriler.FirstOrDefault(x => x.Id == obj.Arama.Id);

            //var indexRow = PotansyelMusteriler.IndexOf(arama);

            //MusteriAramalar.Remove(arama);

            //var guncel_seyahat = repo.Ptd_AramaGetirNoTrack(obj.Arama.Id);

            //MusteriAramalar.Insert(indexRow, guncel_seyahat);

            //SeciliArama = guncel_seyahat;
        }
        private void OnAramaEklendi(PTD_MusteriAramaEklendiEvents obj)
        {
            //MusteriAramalar.Insert(0, obj.Arama);
            //SeciliArama = obj.Arama;
        }
    }
}
