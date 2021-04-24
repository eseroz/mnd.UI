using DevExpress.Mvvm;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.UI.Modules._SatisModule.SeyahatRaporlari.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    public class MusteriAramaListVM : MyBindableBase
    {
        public ObservableCollection<PotansiyelDisiMusteriArama>  MusteriAramalar
                    { get => musteriAramalar; set => SetProperty(ref musteriAramalar,value); }

        PotansiyelDisiRepository repo = new PotansiyelDisiRepository();
        private PotansiyelDisiMusteriArama seciliarama;
        private ObservableCollection<PotansiyelDisiMusteriArama> musteriAramalar;

        public DelegateCommand<int> AramaAddEditCommand => new DelegateCommand<int>(OnAramaEkleDuzenle, c => true);

        public PotansiyelDisiMusteriArama SeciliArama { get => seciliarama; set => SetProperty(ref seciliarama, value); }


        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);

        private void OnEkranYenile()
        {
            MusteriAramalar = repo.PTD_Aramalari_Getir();
        }
        private void OnAramaEkleDuzenle(int id)
        {
            var vm = new PTD_AramaEditVM(id);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("PTD_AramaEditView", vm);
            doc.Title = "Yeni Arama";
            doc.DestroyOnClose = true;
            doc.Show();
        }

        public MusteriAramaListVM(string FormAd)
        {
            MusteriAramalar = repo.PTD_Aramalari_Getir();

            Messenger.Default.Register<PTD_MusteriAramaEklendiEvents>(this, OnAramaEklendi);
            Messenger.Default.Register<PTD_MusteriAramaGuncellendiEvent>(this, OnAramaGuncellendi);

        }

        private void OnAramaGuncellendi(PTD_MusteriAramaGuncellendiEvent obj)
        {
            var arama = MusteriAramalar.FirstOrDefault(x => x.Id == obj.Arama.Id);

            var indexRow = MusteriAramalar.IndexOf(arama);

            MusteriAramalar.Remove(arama);

            var guncel_seyahat = repo.Ptd_AramaGetirNoTrack(obj.Arama.Id);

            MusteriAramalar.Insert(indexRow, guncel_seyahat);

            SeciliArama = guncel_seyahat;
        }

        private void OnAramaEklendi(PTD_MusteriAramaEklendiEvents obj)
        {
            MusteriAramalar.Insert(0, obj.Arama);
            SeciliArama = obj.Arama;
        }
    }
}
