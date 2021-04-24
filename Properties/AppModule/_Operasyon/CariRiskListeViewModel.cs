using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Spreadsheet;
using Pandap.Logic.Model;
using Pandap.Logic.Model.Netsis;
using Pandap.Logic.Model.Operasyon;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._Planlama;
using Pandap.UI.AppModule._SplashScreen;
using Pandap.UI.Helper;
using Pandap.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pandap.UI.AppModule._Operasyon
{
    public class CariRiskListeViewModel: MyDxViewModelBase
    {
        UnitOfWork uow = new UnitOfWork();

        public DelegateCommand DetayCommand => new DelegateCommand(OnDetay, true);

        public DelegateCommand ExcelImportCommand => new DelegateCommand(OnExcelImport, true);


        public DelegateCommand VerileriGuncelleCommand => new DelegateCommand(OnEkranGuncelle, true);

        private async void OnEkranGuncelle()
        {
            SplashScreenHelper.Instance.ShowLoadingScreen();
           
            foreach (var tableRow in CariRiskListe)
            {
                uow.OperasyonRepo.PandapNetsis_CariRisk_Guncelle(tableRow);


                var dovizliDeger = NetsisService.NetsisDovizKuruGetir(tableRow.DovizTuru.Replace("-",""), DateTime.Now);
                uow.OperasyonRepo.NetsisRiskLimitGuncelle(tableRow.CariKod,Convert.ToDecimal(dovizliDeger));
            }

            SplashScreenHelper.Instance.HideSplashScreen();

        }

        protected IOpenFileDialogService OpenFileDialogService { get { return this.GetService<IOpenFileDialogService>(); } }

        ObservableCollection<Yerlesim> yerlesimler;
        public ObservableCollection<Yerlesim> Yerlesimler
        {
            get => yerlesimler;
            set => SetProperty(ref yerlesimler, value);
        }

        public CariRiskListeViewModel()
        {
            Yerlesimler = new ObservableCollection<Yerlesim>();

            yerlesimler.Add(new Yerlesim("1", "RiskYerlesim-1.xml"));
            yerlesimler.Add(new Yerlesim("2", "RiskYerlesim-2.xml"));
            yerlesimler.Add(new Yerlesim("3", "RiskYerlesim-3.xml"));

            SeciliYerlesim = yerlesimler.First();

            SplashScreenHelper.Instance.ShowLoadingScreen();
        }

      


        private async void OnExcelImport()
        {
            OpenFileDialogService.Filter = "Excel Dosyaları|*.xls;*.xlsx;";
            OpenFileDialogService.FilterIndex = 1;
            OpenFileDialogService.Title = "Dosya Seç";

            var cevap = OpenFileDialogService.ShowDialog();

            if (cevap == false) return;

            Workbook workbook = new Workbook();

            bool b = workbook.LoadDocument(OpenFileDialogService.File.GetFullName());

            Worksheet worksheet1 = workbook.Worksheets[0];

            var tubleData = ExcelService.SutunBasliklariYerindeMi("OperasyonRisk", worksheet1);

            if (tubleData.Item1 == false)
            {
                MessageBox.Show(tubleData.Item2, "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var sutunListe = tubleData.Item3;
            var maxSatir = 10000;

            var keyExcelSutun = sutunListe.Where(c => c.IsExcelKey == true).FirstOrDefault().ExcelBaslikHücreKonum;


            SplashScreenHelper.Instance.ShowLoadingScreen();

            int eklenenKayitSayisi = 0;

            for (int satirId = 2; satirId < maxSatir; satirId++)
            {
                var keyExcelValue = worksheet1.Cells[keyExcelSutun.Substring(0, 1) + satirId].Value.ToString();

                if (String.IsNullOrEmpty(keyExcelValue)) {
                    eklenenKayitSayisi = satirId - 2;
                    break;
                }

                var tableRow = CariRiskListe.FirstOrDefault(c => c.CariKod == keyExcelValue);

                if (tableRow == null) continue;

                foreach (var soru in sutunListe)
                {
                    string hücreKonumHarf = soru.ExcelBaslikHücreKonum.Substring(0, 1);
                    string hücreKonum = hücreKonumHarf + satirId.ToString();

                    DevExpress.Spreadsheet.CellValue hucreDeger = worksheet1.Cells[hücreKonum].Value;

                    if (soru.IsText.GetValueOrDefault())
                        NesneIslemleri.OzellikDegerAta(tableRow, soru.DbTabloSutunBaslik, hucreDeger.TextValue);

                    if (soru.IsNumeric.GetValueOrDefault())
                    {
                        double? ndeger = hucreDeger.NumericValue;
                        if (hucreDeger.TextValue?.Trim().Length == 0) ndeger = null;
                       
                        NesneIslemleri.OzellikDegerAta(tableRow, soru.DbTabloSutunBaslik, ndeger);
                    }
                }

             


            }

            SplashScreenHelper.Instance.HideSplashScreen();
            MessageBox.Show(eklenenKayitSayisi.ToString() + " Adet Kayıt Güncellendi");

           

        }

        Yerlesim seciliYerlesim;
        public Yerlesim SeciliYerlesim
        {
            get => seciliYerlesim;
            set => SetProperty(ref seciliYerlesim, value);
        }


        public DelegateCommand<string> RiskPopupCloseCommand => new DelegateCommand<string>(OnRispPopupClose, true);

        private void OnRispPopupClose(string cevap)
        {
            RiskDetayPopupIsOpen = false;

            if (cevap == "cancel") return;

            uow.OperasyonRepo.PandapNetsis_CariRisk_Guncelle(SeciliNetsis_CariRiskKontrol);


           
            //uow.OperasyonRepo.NetsisRiskLimitGuncelle(SeciliNetsis_CariRiskKontrol);

            var risk_db=  uow.OperasyonRepo.Netsis_CariRisk_Getir_CariKoddan(seciliNetsis_CariRiskKontrol.CariKod);

            SeciliNetsis_CariRiskKontrol.RiskLimiti = risk_db.RiskLimiti.GetValueOrDefault();
            SeciliNetsis_CariRiskKontrol.KullanilabilirRisk = risk_db.KullanilabilirRisk.GetValueOrDefault();
        

        }

        bool riskDetayPupupIsOpen;
        public bool RiskDetayPopupIsOpen
        {
            get => riskDetayPupupIsOpen;
            set => SetProperty(ref riskDetayPupupIsOpen, value);
        }


        private void OnDetay()
        {
            RiskDetayPopupIsOpen = true;
        }

        NetsisRiskSon1 seciliNetsis_CariRiskKontrol;
        public NetsisRiskSon1 SeciliNetsis_CariRiskKontrol
        {
            get => seciliNetsis_CariRiskKontrol;
            set => SetProperty(ref seciliNetsis_CariRiskKontrol, value);
        }

        public DelegateCommand FormLoadedCommand => new DelegateCommand(FormLoadedAsync);

        private async void FormLoadedAsync()
        {
            CariRiskListe = await uow.OperasyonRepo.Netsis_CariRisk_Getir();
            SplashScreenHelper.Instance.HideSplashScreen();
        }

        ObservableCollection<NetsisRiskSon1> cariRiskListe;
        public ObservableCollection<NetsisRiskSon1> CariRiskListe
        {
            get => cariRiskListe;
            set => SetProperty(ref cariRiskListe, value);
        }
    

    }
}
