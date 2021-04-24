using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using Pandap.Logic.Model;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._Mesajlasma;

namespace Pandap.UI.AppModule.__SiparisKalem
{
    public class KalemListVM : MyBindableBase
    {
        private UnitOfWork uow = new UnitOfWork();

        private ObservableCollection<SiparisKalem> kalemler;

        public ObservableCollection<SiparisKalem> Kalemler
        {
            get => kalemler;
            set => SetProperty(ref kalemler, value);
        }

        public DelegateCommand<SiparisKalem> PandapMessangerAcCommand => new DelegateCommand<SiparisKalem>(PandapMessangerAc);

        public void KapatilmisKalemleriYukle()
        {
            uow = new UnitOfWork();
            Kalemler = uow.SiparisKalemRepo.KapatilmisSiparisKalemleriIleEmirleriGetir();
        }

        private void PandapMessangerAc(SiparisKalem row)
        {
            MesajlasmaViewModel v = new MesajlasmaViewModel(row.RowGuid.Value, PandapGlobal.AktifKullanici.AdSoyad);
            MesajlasmaWindow w = new MesajlasmaWindow();
            w.DataContext = v;
            w.Show();
        }
    }
}