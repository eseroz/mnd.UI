using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using Pandap.Logic.Helper;
using Pandap.Logic.Model;
using Pandap.Logic.Model.Uretim;
using Pandap.Logic.Model._DTOs;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._Mesajlasma;
using Pandap.UI.AppModule._RaporDesigner;
using Pandap.UI.Helper;
using System;

namespace Pandap.UI.AppModule._Kalite
{
    public class KaliteViewModel : MyBindableBase
    {
        private UnitOfWork uow = new UnitOfWork();

        private UretimEmri seciliUretimEmri;

        public UretimEmri SeciliUretimEmri
        {
            get { return seciliUretimEmri; }
            set { SetProperty(ref seciliUretimEmri, value); }
        }

        public DelegateCommand<UretimEmri> SertifikaYazdirCommand => new DelegateCommand<UretimEmri>(SertifikaYazdir);
        public DelegateCommand SertifikaTasarimCommand => new DelegateCommand(SertifikaTasarla);

        public DelegateCommand<Bobin> PandaStogaCevirCommand => new DelegateCommand<Bobin>(PandaStogaCevir,CanPandaStogaCevir);

        private bool CanPandaStogaCevir(Bobin arg)
        {
            var _bobin = arg as Bobin;

            

            if (_bobin == null) return false;

            return _bobin.CariKod_Onceki == null;


        }

        private async void PandaStogaCevir(Bobin bobin)
        {
            MessageBoxResult cevap=
            MessageBox.Show("Panda Stoğa Aktarılıyor. Onaylıyor musunuz", "Pandap", MessageBoxButton.OKCancel,MessageBoxImage.Question);

            if(cevap==MessageBoxResult.OK)
            {
                bobin.CariKod_Onceki = bobin.CariKod;
                bobin.CariKod = GlobalConst.PANDACARI;

                uow.Commit();
            }
        
        }

        private void SertifikaTasarla()
        {
            RaporDesigner1 reportDesignWindow = new RaporDesigner1();

            reportDesignWindow.designer.DocumentOpened += (s, e) =>
            {
                var v = e.Document;
                var ds = v.ReportModel.DataSource as ObjectDataSource;

                if (ds != null)
                {
                    var sertifikaDtoListe = new List<KaliteSertifikaDto>();
                    sertifikaDtoListe.Add(SeciliUretimEmri.ToKaliteSertifikaDto());
                    ds.DataSource = sertifikaDtoListe;
                }
                else
                    MessageBox.Show("Data Source Tanımı yapınız...");
            };

            reportDesignWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            reportDesignWindow.Show();
        }

        public void SertifikaYazdir(UretimEmri uretimEmri)
        {
            var kk = uretimEmri.ToKaliteSertifikaDto();

            var reportPath = @"Content\KaliteFormCikti.repx";

            var fs = new FileStream(reportPath, FileMode.Open);
            MemoryStream ms = new MemoryStream();

            fs.CopyTo(ms);

            XtraReport v = XtraReport.FromStream(ms, true);
            v.Landscape = true;

            var ds = v.DataSource as ObjectDataSource;

            if (ds != null)
                ds.DataSource = uretimEmri.ToKaliteSertifikaDto();
            else
                MessageBox.Show("Data Source Tanımlı değil");

            PandapRaporSimpleViever view = new PandapRaporSimpleViever();
            view.report_view_control.DocumentSource = v;
            view.report_view_control.ZoomFactor = 0.85;
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            view.Width = 990; view.Height = 780;

            v.CreateDocument();

            view.ShowDialog();

            fs.Close();
        }

        public DelegateCommand<Bobin> PandapMessangerAcCommand => new DelegateCommand<Bobin>(PandapMessangerAc);

        private void PandapMessangerAc(Bobin row)
        {
            MesajlasmaViewModel v = new MesajlasmaViewModel(row.RowGuid.Value, PandapGlobal.AktifKullanici.AdSoyad);
            MesajlasmaWindow w = new MesajlasmaWindow();
            w.DataContext = v;
            w.ShowDialog();
        }

        private bool isOpenBobinDialog;

        public bool IsOpenBobinDialog
        {
            get => isOpenBobinDialog;
            set => SetProperty(ref isOpenBobinDialog, value);
        }

        public ObservableCollection<UretimEmri> UretimEmirleri { get; set; }

        public DelegateCommand<object> OpenBobinDialogCommand => new DelegateCommand<object>(OnOpenBobinDialog, CanOpenBobinDialog);

        private bool CanOpenBobinDialog(object arg)
        {
            return SeciliUretimEmri != null;
        }

        public DelegateCommand<string> CloseBobinDialogCommand => new DelegateCommand<string>(OnCloseBobinDialog, c => true);

        public DelegateCommand<object> BobinSilCommand => new DelegateCommand<object>(OnBobinSil, CanBobinSil);

        private bool CanBobinSil(object arg)
        {
            var _bobin = arg as Bobin;

            if (_bobin == null) return false;

            return _bobin.PaletId == null;
        }

        private Bobin tempBobin;

        public Bobin TempBobin { get => tempBobin; set => SetProperty(ref tempBobin, value); }

        private KaliteBobinViewModel kaliteBobinViewModel;

        private void OnOpenBobinDialog(object obj)
        {
            var urunData = SeciliUretimEmri.SiparisKalemKodNav;

            if (SeciliUretimEmri.KaliteStandartlari_Json == null)
            {
                var kaliteAralik = uow.KaliteRepo.VarsayilanKaliteAralikGetir(urunData.KullanimAlanTipKod, urunData.AlasimTipKod, urunData.SertlikTipKod, urunData.Kalinlik_micron);

                if (kaliteAralik == null)
                {
                    MessageBox.Show("Bu ürün için aralıklar tanımlı değil");
                    return;
                }

                SeciliUretimEmri.KaliteSinirlari = kaliteAralik;
            }
            else
            {
                SeciliUretimEmri.KaliteSinirlari = JsonConvert.DeserializeObject<KaliteStandart>(SeciliUretimEmri.KaliteStandartlari_Json);
            }

            kaliteBobinViewModel = new KaliteBobinViewModel();
            kaliteBobinViewModel.AktifUretimEmri = SeciliUretimEmri;


            kaliteBobinViewModel.KapatCommand = CloseBobinDialogCommand;

            KaliteBobinWindow kaliteWindow = new KaliteBobinWindow();



            kaliteWindow.DataContext = kaliteBobinViewModel;

            kaliteWindow.Show();
        }

        private async void OnCloseBobinDialog(string obj)
        {
            kaliteBobinViewModel.AktifUretimEmri.KaliteStandartlari_Json = JsonConvert.SerializeObject(kaliteBobinViewModel.AktifUretimEmri.KaliteSinirlari);
           uow.Commit();
        }

        private async void OnBobinSil(object obj)
        {
            var bobin = (Bobin)obj;
            SeciliUretimEmri.UretimBobinler.Remove(bobin);

           uow.Commit();
        }

        public KaliteViewModel()
        {
            UretimEmirleri = uow.PlanlamaRepo.UretimEmirleriGetir()
                .Where(c => c.KapatildiMi == false || c.KapatildiMi == null)
                .OrderByDescending(c => c.UretimEmriKod)
                .ToObservableCollection();
        }
    }
}