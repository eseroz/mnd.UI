using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.PivotGrid;
using Microsoft.Win32;
using Pandap.Logic.Helper;
using Pandap.Logic.Model;
using Pandap.Logic.Model.App;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._Mesajlasma;

namespace Pandap.UI.AppModule._Pano
{
    public class PanoViewModel : MyBindableBase
    {
        private ObservableCollection<Duyuru> duyurular;

        public ObservableCollection<Duyuru> Duyurular
        {
            get => duyurular;
            set => SetProperty(ref duyurular, value);
        }

        private List<SatisRapor> satisRapor;

        public List<SatisRapor> SatisRapor
        {
            get => satisRapor;
            set => SetProperty(ref satisRapor, value);
        }

        public bool SatisRaporGorunsunMu
        {
            get
            {
                return PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.OPERASYON ||
                    PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.YONETICI
                    ;
            }
        }

        public DelegateCommand<Duyuru> PandapMessangerAcCommand => new DelegateCommand<Duyuru>(PandapMessangerAc);
        public DelegateCommand<PivotGridControl> PivotExceleAktarCommand => new DelegateCommand<PivotGridControl>(OnPivotExceleAktar);

        public DelegateCommand<TableView> GridControlExceleAktarCommand => new DelegateCommand<TableView>(OnGridControlExceleAktar);

        private void OnGridControlExceleAktar(TableView tw)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.FileName = "export.xlsx";
            saveFileDialog.Filter = "excel dosyası (.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                tw.ExportToXlsx(saveFileDialog.FileName);
            }
        }

        private void OnPivotExceleAktar(PivotGridControl pvt)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.FileName = "export.xlsx";
            saveFileDialog.Filter = "excel dosyası (.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                pvt.ExportToXlsx(saveFileDialog.FileName);
            }
        }

        public DelegateCommand YenileCommand => new DelegateCommand(Yenile);

        private void Yenile()
        {
            Duyurular = uow.DuyurularRepo.DuyuruVeSayilariGetir();

            SatisRapor = uow.SiparisRepo.SiparisRaporGetir();
        }

        private void PandapMessangerAc(Duyuru row)
        {
            MesajlasmaViewModel v = new MesajlasmaViewModel(row.RowGuid, PandapGlobal.AktifKullanici.AdSoyad);
            MesajlasmaWindow w = new MesajlasmaWindow();
            w.DataContext = v;
            w.Show();
        }

        private UnitOfWork uow = new UnitOfWork();

        public PanoViewModel()
        {
            Duyurular = uow.DuyurularRepo.DuyuruVeSayilariGetir();

            if (SatisRaporGorunsunMu == false) return;
            SatisRapor = uow.SiparisRepo.SiparisRaporGetir();
        }
    }
}