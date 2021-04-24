using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using mnd.Logic.BC_Kalite;
using mnd.Logic.BC_Kalite.QueryDTOs;
using mnd.Logic.Helper;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.AppModule;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.KaliteModule
{
    public class RedMalzemeListVM : MyDxViewModelBase
    {
        KaliteRedMalzemeService redMalzemeService = new KaliteRedMalzemeService();
        private bool _isLoading;
        private string formUyariMesaj;

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, () => true);

        [YetkiKontrol]
        public DelegateCommand<object> YeniCommand => new DelegateCommand<object>(OnYeni,
           c => YetkiliMi_FromDb(nameof(YeniCommand)));
        public DelegateCommand<RedMalzemeDto> DuzenleCommand => new DelegateCommand<RedMalzemeDto>(OnDuzenle);
        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);

        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>();

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport,
           c => YetkiliMi_FromDb(nameof(ExcelExportCommand)));

        public DelegateCommand<object> YerlesimKaydetCommand =>
            new DelegateCommand<object>(OnYerlesimKaydet);

        public string GridYerlesimDosyaAd => "RedMalzemeTablo.xml";

        public string FormUyariMesaj { get => formUyariMesaj; set => SetProperty(ref formUyariMesaj, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));

        public ObservableCollection<RedMalzemeDto> RedMalzemeListesi
        {
            get => redMalzemeListesi;
            set => SetProperty(ref redMalzemeListesi, value);
        }

        private UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<RedMalzemeDto> redMalzemeListesi;
        private RedMalzemeDto seciliRedMalzemeDto;

        public RedMalzemeDto SeciliRedMalzemeDto { get => seciliRedMalzemeDto; set => SetProperty(ref seciliRedMalzemeDto , value); }

        public RedMalzemeListVM(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);
        }

        private void OnYerlesimKaydet(object obj) => ExportService1.SaveLayout(GridYerlesimDosyaAd);
        private void OnExcelExport(object obj) => ExportService1.ExportTo(ExportType.XLSX,
            $"{GridYerlesimDosyaAd.Replace(".xml", "")} {DateTime.Now.ToString("ddMMyyyy_HHmm")}.xlsx");

        private  void OnFormLoaded()
        {
            OnEkranYenile();
            ExportService1.RestoreLayout(GridYerlesimDosyaAd);

          
           

        }



        private async void OnEkranYenile()
        {
            IsLoading = true;
            FormUyariMesaj = "Yükleniyor...";

            var glist = await redMalzemeService.RedMalzemeListesi();

            RedMalzemeListesi = glist.ToObservableCollection();
            RedMalzemeListesi.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

            IsLoading = false;
            FormUyariMesaj = "";

            BoundMessageQueue.Enqueue("Veriler yenilendi", true);
            SeciliRedMalzemeDto = RedMalzemeListesi.Last();

        }


        private void OnYeni(object o)
        {
            var vm = new RedMalzemeVM();
            vm.Load(0);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("RedMalzemeView", vm);
            doc.Title = "Yeni Kayıt";
            doc.DestroyOnClose = true;
            doc.Show();
        }



        private void OnDuzenle(RedMalzemeDto obj)
        {
            var vm = new RedMalzemeVM();

            vm.Load(obj.Id);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("RedMalzemeView", vm);
            doc.DestroyOnClose = true;
            doc.Title = obj.Id + "-" + obj.Musteri;
            doc.Show();
        }




    }
}
