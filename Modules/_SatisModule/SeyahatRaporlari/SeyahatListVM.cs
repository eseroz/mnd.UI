using DevExpress.Mvvm;
using mnd.Logic.BC_Satis._Seyahat;
using mnd.Logic.BC_Satis._Seyahat.DataServices;
using mnd.Logic.Model;
using mnd.UI.Modules._SatisModule.SeyahatRaporlari.Events;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules._SatisModule.SeyahatRaporlari
{
    public class SeyahatListVM : MyBindableBase
    {
        public DelegateCommand<int> SeyahatAddEditCommand => new DelegateCommand<int>(OnSeyahatEkleDuzenle, c => true);


        public DelegateCommand<int> GorusmeAddCommand => new DelegateCommand<int>
          (OnGorusmeEkle, c => true);

        public DelegateCommand<SeyahatGorusme> GorusmeEditCommand => new DelegateCommand<SeyahatGorusme>
           (OnGorusmeDuzenle, c => true);


        public DelegateCommand<SeyahatGorusme> GorusmeSilCommand => new DelegateCommand<SeyahatGorusme>
            (OnGorusmeSil, c => true);

        private void OnGorusmeSil(SeyahatGorusme gorusme)
        {
            var seciliSeyahat = SeyahatRaporList.First(c => c.Id == gorusme.SeyahatId);
            seciliSeyahat.Gorusmeler.Remove(gorusme);

            var guncel_seyahat = repo.SeyahatGetir(gorusme.SeyahatId);
            var gorusmeDb = guncel_seyahat.Gorusmeler.First(x => x.Id == gorusme.Id);

            guncel_seyahat.Gorusmeler.Remove(gorusmeDb);
            repo.Kaydet();


        }

        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);

        private void OnEkranYenile()
        {
            SeyahatRaporList = repo.SeyahatleriGetir();
        }

        SeyahatRepository repo = new SeyahatRepository();
        private Seyahat seciliSeyahat;
        private ObservableCollection<Seyahat> seyahatRaporList;

        private void OnSeyahatEkleDuzenle(int id)
        {
            var vm = new SeyahatEditVM(id);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SeyahatEditView", vm);
            doc.Title = "Yeni Seyahat";
            doc.DestroyOnClose = true;
            doc.Show();
        }

        private void OnGorusmeEkle(int seyahatId)
        {
            var vm = new GorusmeEditVM(seyahatId, 0);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("GorusmeEditView", vm);
            doc.Title = "Yeni Görüşme";
            doc.DestroyOnClose = true;
            doc.Show();
        }
        private void OnGorusmeDuzenle(SeyahatGorusme gorusme)
        {
            var vm = new GorusmeEditVM(gorusme.SeyahatId, gorusme.Id);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("GorusmeEditView", vm);
            doc.Title = "Yeni Görüşme";
            doc.DestroyOnClose = true;
            doc.Show();

        }

        public ObservableCollection<Seyahat> SeyahatRaporList { get => seyahatRaporList; set => SetProperty(ref seyahatRaporList, value); }


        public SeyahatListVM(string FormAd)
        {
            SeyahatRaporList = repo.SeyahatleriGetir();

            Messenger.Default.Register<SeyahatEklendiEvent>(this, OnSeyahatEklendi);
            Messenger.Default.Register<SeyahatGuncellendiEvent>(this, OnSeyahatGuncellendi);

        }

        private void OnSeyahatGuncellendi(SeyahatGuncellendiEvent obj)
        {
            var seyahat = SeyahatRaporList.FirstOrDefault(x => x.Id == obj.Seyahat.Id);

            var indexRow = SeyahatRaporList.IndexOf(seyahat);

            SeyahatRaporList.Remove(seyahat);

            var guncel_seyahat = repo.SeyahatGetirNoTrack(obj.Seyahat.Id);

            SeyahatRaporList.Insert(indexRow, guncel_seyahat);

            SeciliSeyahat = guncel_seyahat;


        }

        public Seyahat SeciliSeyahat { get => seciliSeyahat; set => SetProperty(ref seciliSeyahat, value); }

        private void OnSeyahatEklendi(SeyahatEklendiEvent obj)
        {
            SeyahatRaporList.Insert(0, obj.Seyahat);
            SeciliSeyahat = obj.Seyahat;
        }


    }
}
