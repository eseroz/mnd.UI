using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using Pandap.Logic.Helper;
using Pandap.Logic.Model;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._Mesajlasma;
using Pandap.UI.Services;

namespace Pandap.UI.AppModule._Planlama
{
    public class PlanlamaKalemListViewModel : MyBindableBase
    {
        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand PlanlaCommand => new DelegateCommand(onPlanla, canPlanla);
        public DelegateCommand VerileriGuncelleCommand => new DelegateCommand(VerileriGuncelle, true);
        public DelegateCommand<SiparisKalem> PandapMessangerAcCommand => new DelegateCommand<SiparisKalem>(PandapMessangerAc);

        public DelegateCommand<string> KalemIslemCommand => new DelegateCommand<string>(OnKalemIslem, canKalemIslem);

        private bool canKalemIslem(string arg)
        {
            return PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA
                       || PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA_YONETICI;
        }

        private async void OnKalemIslem(string islem)
        {
            if (islem == "Kapat")
            {
                uow = new UnitOfWork();

                var mesaj = "Üretimde " + SeciliKalem.PLAN_UretimdekiMiktarToplam + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

                var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (mesajSonuc == MessageBoxResult.Yes)
                {
                    SeciliKalem.PLAN_PlanlanacakKalanMiktarToplam = SeciliKalem.PLAN_PlanlanacakKalanMiktarToplam + SeciliKalem.PLAN_UretimdekiMiktarToplam;

                    SeciliKalem.UretimEmirleri.ForEach(c => { c.KapatildiMi = true; });
                   uow.Commit();
                }

                if (mesajSonuc == MessageBoxResult.No)
                {
                    var kalem = uow.SiparisKalemRepo.SiparisKalemiGetir(SeciliKalem.SiparisKalemKod);
                    kalem.PLAN_KalemKapatildiMi = true;
                    kalem.PLAN_KalemKapatilmaTarihi = DateTime.Now;
                    kalem.UretimEmirleri.ForEach(c => { c.KapatildiMi = true; });

                   uow.Commit();
                }

                Kalemler = uow.SiparisKalemRepo.AktifSiparisKalemleriIleEmirleriGetir();
            }
        }

        private void PandapMessangerAc(SiparisKalem row)
        {
            MesajlasmaViewModel v = new MesajlasmaViewModel(row.RowGuid.Value, PandapGlobal.AktifKullanici.AdSoyad);
            MesajlasmaWindow w = new MesajlasmaWindow();
            w.DataContext = v;
            w.Show();
        }

        private void onPlanla()
        {
            //PlanlamaRotaKartiTabViewModel _viewModel = new PlanlamaRotaKartiTabViewModel();
            //_viewModel.RotaKartlari = new ObservableCollection<UretimEmriViewModel>();

            //SeciliKalemler.ToList().ForEach(k =>
            //{
            //    UretimEmriViewModel uretimEmriViewModel = UretimEmriService.Create_UretimEmriViewModel_FromKalem(k);

            //    var emirKod = uow.PlanlamaRepo.YeniUretimEmriKodGetir_SiparisKalemden(k.SiparisKalemKod);

            //    uretimEmriViewModel.UretimEmriDTO.UretimEmriKod = emirKod;

            //    _viewModel.RotaKartlari.Add(uretimEmriViewModel);
            //});

            //PlanlamaRotaKartiTabView _view = new PlanlamaRotaKartiTabView();
            //_view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //_view.DataContext = _viewModel;

            //_view.Show();
        }

        private ObservableCollection<SiparisKalem> kalemler;

        public ObservableCollection<SiparisKalem> Kalemler
        {
            get => kalemler;
            set => SetProperty(ref kalemler, value);
        }

        private ObservableCollection<SiparisKalem> seciliKalemler = new ObservableCollection<SiparisKalem>();

        public ObservableCollection<SiparisKalem> SeciliKalemler
        {
            get => seciliKalemler;
            set => SetProperty(ref seciliKalemler, value);
        }

        private SiparisKalem seciliKalem;

        public SiparisKalem SeciliKalem
        {
            get => seciliKalem;
            set => SetProperty(ref seciliKalem, value);
        }

        public PlanlamaKalemListViewModel(bool sadece_uretimdekiler)
        {
            Kalemler = uow.SiparisKalemRepo.AktifSiparisKalemleriIleEmirleriGetir();
        }

        public void VerileriGuncelle()
        {
            uow = new UnitOfWork();
            Kalemler = uow.SiparisKalemRepo.AktifSiparisKalemleriIleEmirleriGetir();
        }

        private bool canPlanla()
        {
            return PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA
                        || PandapGlobal.AktifKullanici.KullaniciRol == KullaniciRolleri.PLANLAMA_YONETICI;
        }
    }
}