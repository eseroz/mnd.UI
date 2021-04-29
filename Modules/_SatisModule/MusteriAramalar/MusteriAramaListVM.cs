using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.Logic.Persistence;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;
using System.Collections.ObjectModel;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    public class MusteriAramaListVM : MyBindableBase
    {
        public string[] bagliPlasiyerKodlari = null;
        private UnitOfWork uow = new UnitOfWork();
        PotansiyelDisiRepository repo = new PotansiyelDisiRepository();

        private ObservableCollection<PotansiyelMusteriDTO> potansyelMusteriler;
        public ObservableCollection<PotansiyelMusteriDTO> PotansyelMusteriler
        {
            get => potansyelMusteriler;
            set => SetProperty(ref potansyelMusteriler, value);
        }

        private PotansiyelMusteriDTO seciliarama;

        public PotansiyelMusteriDTO SeciliArama { get => seciliarama; set => SetProperty(ref seciliarama, value); }

        #region Delegates
        public DelegateCommand<string> AramaAddEditCommand => new DelegateCommand<string>(OnAramaEkleDuzenle, true);
        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);
        #endregion

        private void OnEkranYenile()
        {
            PotansyelMusteriler = new ObservableCollection<PotansiyelMusteriDTO>();
            PotansyelMusteriler = repo.PTD_Aramalari_Getir(bagliPlasiyerKodlari, MusteriGrubuAdi);
        }
        private void OnAramaEkleDuzenle(string id)
        {

            PTD_AramaEditVM vm;
            IDocument doc = null;
         

            if (id != "0")
            {
                int Id = int.Parse(id);
                vm = new PTD_AramaEditVM(Id);
                vm.MusteriGrubuAdi = MusteriGrubuAdi;
                vm.UC_Title = MusteriGrubuAdi + ">>" + vm.EditModel.MusteriUnvan + " Arama geçmişi..";
                vm.PotansiyelMusteriListesi = new ObservableCollection<PotansiyelMusteriDTO>();

                doc = AppPandap.pDocumentManagerService.CreateDocument("PTD_AramaEditView", vm);
                doc.Title = vm.UC_Title;
            }
            else
            {
                vm = new PTD_AramaEditVM();
                vm.MusteriGrubuAdi = MusteriGrubuAdi;
                vm.PotansiyelMusteriListesi = new ObservableCollection<PotansiyelMusteriDTO>();

                doc = AppPandap.pDocumentManagerService.CreateDocument("PTD_AramaEditView", vm);
                doc.Title = "Yeni Arama";
            }



            doc.DestroyOnClose = true;
            doc.Show();
        }
        public string MusteriGrubuAdi { get; set; }


   
        public MusteriAramaListVM(string FormAd)
        {
            YoneticiMi = (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI);
            
            MusteriGrubuAdi = FormAd;
            if (AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari != null)
            {
                bagliPlasiyerKodlari = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');
            }
            else
            {
                bagliPlasiyerKodlari = uow.PlasiyerRepo.PlasiyerKodlari();
            }




            PotansyelMusteriler = repo.PTD_Aramalari_Getir(bagliPlasiyerKodlari, MusteriGrubuAdi);

            Messenger.Default.Register<PTD_MusteriAramaEklendiEvents>(this, OnAramaEklendi);
            Messenger.Default.Register<PTD_MusteriAramaGuncellendiEvent>(this, OnAramaGuncellendi);

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

        private bool yoneticiMi;

        public bool YoneticiMi { get => yoneticiMi; set => SetProperty(ref yoneticiMi, value); }
    }
}
