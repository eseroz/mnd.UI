using mnd.Logic.Persistence;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mnd.UI.GyModules.MesajModule;
using DevExpress.Mvvm;
using mnd.UI.AppModules.AppModule;
using mnd.Logic.Helper;
using mnd.Logic.Model.Uretim;

namespace mnd.UI.Modules._SatisModule.KapasiteTakip
{
    public class KapasiteTakipListVM : MyDxViewModelBase
    {
        private List<PlanlamaTakipDto> planlamaDtoListe;
        private UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<PlanlamaTakipDto> planlanacaklar;

        public ObservableCollection<PlanlamaTakipDto> Planlanacaklar { get => planlanacaklar; 
            set => SetProperty(ref planlanacaklar , value); }

        [YetkiKontrol]
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => YetkiliMi_FromDb(nameof(ExcelExportCommand)));


        [YetkiKontrol]
        public DelegateCommand<object> EkraniYenileCommand => new DelegateCommand<object>(OnEkraniYenile, c => true);

        private void OnEkraniYenile(object obj)
        {
            VerileriYukle();
        }

        public KapasiteTakipListVM(string menuAd)
        {
            FormMenuAd = menuAd;
            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);

            VerileriYukle();
        }

        public void VerileriYukle()
        {
            uow = new UnitOfWork();

            var planTumu = uow.PlanlamaRepo.GetirKalemPlanlamaTakip_ReelDto();

            var _planKapasitif = uow.PlanlamaRepo.GetirKalemPlanlamaTakip_KapasitifDto()
                .ToList();


            var planReel = planTumu.Where(c => c.KapasitifDurum == "Reel").ToList();



            planReel.AddRange(_planKapasitif);



            Planlanacaklar = planReel
                                 .Select(c => new PlanlamaTakipDto
                                 {
                                     KapasitifDurum = c.KapasitifDurum,
                                     HaftalikTonaj = c.HaftalikTonaj,
                                     MusteriAd = c.MusteriAd,
                                     SiparisDurum = c.SiparisDurum,
                                     SevkHafta = c.SevkHafta,
                                     SevkYilAy = c.SevkYilAy,
                                     SiparisKod = c.SiparisKod,
                                     SiparisKalemKod = c.SiparisKalemKod,
                                     KullanimAlani = c.KullanimAlani,
                                     Bakiye = c.Bakiye < 0 ? 0 : c.Bakiye,
                                     Alasim=c.Alasim,
                                     UretimdeYuruyenMiktar = c.UretimdeYuruyenMiktar,
                                     ToplamPlanlanacakVeUretimdeki = (c.Bakiye < 0 ? 0 : c.Bakiye) + c.UretimdeYuruyenMiktar,

                                 })
                                 .ToObservableCollection();

        }

        private void OnExcelExport(object obj)
        {
            var tarih = DateTime.Now.ToString("ddMMyyyy_HHmm");

            ExportService.ExportTo(ExportType.XLSX, $"KapasiteExp {tarih}.xlsx");
        }


    }
}
