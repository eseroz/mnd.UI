using System;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.Helper;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.KulceKontratTakip
{
    public class KulceKontratListModel : MyDxViewModelBase, IForm
    {
        public ObservableCollection<KulceKontrat> KulceKontratlar { get; set; }

        KulceKontratRepository repo = new KulceKontratRepository();
        private KulceKontratDonem seciliKontratDonem;

        public DelegateCommand YeniCommand => new DelegateCommand(OnYeniKulceKontrat, true);

        public DelegateCommand<KulceKontrat> DuzenleCommand => new DelegateCommand<KulceKontrat>(OnDuzenleKulceKontrat, true);

        public DelegateCommand<KulceProforma> ProformaEditCommand => new DelegateCommand<KulceProforma>(OnProformaEdit, c => true);

        public DelegateCommand<object> ProformaEkleCommand => new DelegateCommand<object>(OnProformaEkle, true);

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport1, true);

        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");
        private void OnExcelExport1(object obj)
        {
            ExportService1.ExportTo(ExportType.WYSIWYG, "KülçeFiyatlar");
        }

        public KulceKontrat SeciliKontrat { get; set; }

        public KulceKontratDonem SeciliKontratDonem { get => seciliKontratDonem; set =>SetProperty(ref seciliKontratDonem , value); }

        public KulceProforma SeciliProforma { get; set; }



        public KulceKontratListModel(string menuFormAd)
        {
            FormMenuAd = menuFormAd;
            KulceKontratlar = repo.KulceKontratlariGetir();

            KulceKontratlar.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            var proformalar = KulceKontratlar
                            .SelectMany(foo => foo.KulceKontratDonemler)
                            .SelectMany(bar => bar.KulceProformalar)
                            .ToObservableCollection();


            proformalar.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            Messenger.Default.Register<KayitIslemEvent<KulceKontrat>>(this, KontratIslem);

            Messenger.Default.Register<KayitIslemEvent<KulceProforma>>(this, ProformaIslem);


        }

        private void OnYeniKulceKontrat()
        {
            KulceKontratEditVM vm = new KulceKontratEditVM();
           
            vm.KayitModu = "Add";
            vm.KulceKontrat = new KulceKontrat();
            vm.KulceKontrat.RowGuid = Guid.NewGuid();
            vm.KulceKontrat.KontratTarihi = DateTime.Now.Date;
            vm.KulceKontrat.Ekleyen = AppPandap.AktifKullanici.KullaniciId;

            KulceKontratEditView view = new KulceKontratEditView();
            view.DataContext = vm;

            view.ShowDialog();
        }
        private void OnDuzenleKulceKontrat(KulceKontrat obj)
        {
            KulceKontratEditVM vm = new KulceKontratEditVM();
            vm.KayitModu = "Edit";
            vm.KulceKontrat = obj;

            KulceKontratEditView view = new KulceKontratEditView();
            view.DataContext = vm;

            view.ShowDialog();
        }
        private void KontratIslem(KayitIslemEvent<KulceKontrat> obj)
        {
            if (obj.IslemTip == "Add")
            {
                var aylikTon = obj.Kayit.MiktarTon / obj.Kayit.PartiDonemAy;

                var yil = obj.Kayit.PartiDonemYil;
                var baslangicAy = obj.Kayit.PartiDonemBaslangicAy;
              

                for (int i = 0; i < obj.Kayit.PartiDonemAy; i++)
                {
                    var tarih = new DateTime(yil, baslangicAy, 1);

                    var yeniTarih=tarih.AddMonths(i);

                    var donem = new KulceKontratDonem
                    {
                        Yil = yeniTarih.Year,
                        Ay = yeniTarih.Month,
                        MiktarTon = aylikTon
                    };

                 
                    obj.Kayit.KulceKontratDonemler.Add(donem);
                }

               
                KulceKontratlar.Add(obj.Kayit);
                repo.KulceKontratEkle(obj.Kayit);
            }

            if (obj.IslemTip == "Edit")
            {
                SeciliKontrat = obj.Kayit;
            }

            repo.Kaydet();
        }




        private void OnProformaEkle(object o)
        {
            var kontrat = KulceKontratlar.Where(c => c.Id == SeciliKontratDonem.KulceKontratId).First();

            var kulceProforma = new KulceProforma();
            kulceProforma.RowGuid = Guid.NewGuid();

            kulceProforma.PrimTon_BF = kontrat.Prim;
            kulceProforma.Parite = 1;

            kulceProforma.ProformaTarih = DateTime.Now;

            ProformaEditVM vm = new ProformaEditVM(kulceProforma);
            vm.KayitModu = "Add";

            ProformaEditView view = new ProformaEditView();
            view.DataContext = vm;

            view.ShowDialog();
        }

        private void OnProformaEdit(KulceProforma obj)
        {
            ProformaEditVM vm = new ProformaEditVM(obj);
            vm.KayitModu = "Edit";
       
            ProformaEditView view = new ProformaEditView();
            view.DataContext = vm;
            view.ShowDialog();
        }

        private void ProformaIslem(KayitIslemEvent<KulceProforma> obj)
        {
            if (obj.IslemTip == "Add")
            {
                SeciliKontratDonem.KulceProformalar.Add(obj.Kayit);
                SeciliKontrat.HesaplamariAta();
            }

            if (obj.IslemTip == "Edit")
            {
                SeciliKontrat.HesaplamariAta();
            }

            repo.Kaydet();


        }




        public void Load()
        {

        }
    }
}
