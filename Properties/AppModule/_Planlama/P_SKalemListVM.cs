using DevExpress.Mvvm;
using Pandap.Logic.Model;
using Pandap.Logic.Model._DTOs;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Persistence;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Pandap.UI.AppModule._Planlama
{
    public class P_SKalemListVM : MyBindableBase
    {
        private ObservableCollection<PlanlamaTakipDto> planlamaDtoListe;
        private PlanlamaTakipDto seciliPlanlamaTakipDto;
        private Yerlesim seciliYerlesim;
        private UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<Yerlesim> yerlesimler;
        public ObservableCollection<PlanlamaTakipDto> SeciliPlanlamaTakipDtos { get; set; }

        public DelegateCommand PlanlaCommand => new DelegateCommand(OnPlanla, canPlanla);
        public DelegateCommand KartGosterCommand => new DelegateCommand(OnRotaKartGoster, true);

        public DelegateCommand<object> KapatIslemCommand => new DelegateCommand<object>(OnKalemKapat, c => true);

        public DelegateCommand SiparisKalemleriGosterCommand => new DelegateCommand(OnSiparisKalemleriGoster, true);

        public DelegateCommand YardimCommand => new DelegateCommand(OnYardimGoster, true);

        public DelegateCommand EkraniYenileCommand => new DelegateCommand(OnEkraniYenile, true);

        private void OnEkraniYenile()
        {
            uow = new UnitOfWork();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirKalemPlanlamaTakipDto();
        }

        private void OnYardimGoster()
        {
        }

        public P_SKalemListVM()
        {
            Yerlesimler = new ObservableCollection<Yerlesim>();

            yerlesimler.Add(new Yerlesim("1", "P_SiparisKalemList1-1.xml"));
            yerlesimler.Add(new Yerlesim("2", "P_SiparisKalemList1-2.xml"));
            yerlesimler.Add(new Yerlesim("3", "P_SiparisKalemList1-3.xml"));

            SeciliYerlesim = yerlesimler.First();

            PlanlamaDtoListe = uow.PlanlamaRepo.GetirKalemPlanlamaTakipDto();
        }

        public ObservableCollection<PlanlamaTakipDto> PlanlamaDtoListe
        {
            get => planlamaDtoListe;
            set => SetProperty(ref planlamaDtoListe, value);
        }

        public PlanlamaTakipDto SeciliPlanlamaTakipDto
        {
            get => seciliPlanlamaTakipDto;
            set
            {
                if (SetProperty(ref seciliPlanlamaTakipDto, value))
                {
                    if (seciliPlanlamaTakipDto == null) return;

                    seciliPlanlamaTakipDto.PropertyChanged -= SeciliPlanlamaTakipDto_PropertyChanged;
                    seciliPlanlamaTakipDto.PropertyChanged += SeciliPlanlamaTakipDto_PropertyChanged;
                }
            }
        }

        public Yerlesim SeciliYerlesim
        {
            get => seciliYerlesim;
            set => SetProperty(ref seciliYerlesim, value);
        }

        public ObservableCollection<Yerlesim> Yerlesimler
        {
            get => yerlesimler;
            set => SetProperty(ref yerlesimler, value);
        }

        public  void OnKalemKapat(object obj)
        {
           
            var mesaj = "Üretimde" + SeciliPlanlamaTakipDto.UretimdekiMiktar + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

            var uow1 = new UnitOfWork();
            var uretimEmri = uow1.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliPlanlamaTakipDto.UretimEmriKod);

            var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel);

            SiparisKalem seciliKalem = uow1.SiparisKalemRepo.SiparisKalemiGetir(SeciliPlanlamaTakipDto.SiparisKalemKod);

            if (mesajSonuc == MessageBoxResult.Yes)
            {
                seciliKalem.PLAN_PlanlanacakKalanMiktarToplam = seciliKalem.PLAN_PlanlanacakKalanMiktarToplam + uretimEmri.Uretim_UretimdekiMiktar;

                seciliKalem.UretimEmirleri.ForEach(c => { c.KapatildiMi = true; });
                uow1.Commit();

                SeciliPlanlamaTakipDto.KapatildiMi = true;
            }

            if (mesajSonuc == MessageBoxResult.No)
            {
                SeciliPlanlamaTakipDto.KapatildiMi = true;

                seciliKalem.PLAN_KalemKapatildiMi = true;
                seciliKalem.PLAN_KalemKapatilmaTarihi = DateTime.Now;
                seciliKalem.UretimEmirleri.ForEach(c => { c.KapatildiMi = true; });

                 uow1.Commit();
            }

            uow1.Dispose();
        }

        private bool canPlanla()
        {
            if (SeciliPlanlamaTakipDto == null) return false;

            return !SeciliPlanlamaTakipDto.KapatildiMi;
        }

        private void OnRotaKartGoster()
        {
            UretimEmriView _view = new UretimEmriView();

            _view.DataContext = new UretimEmriViewModel(seciliPlanlamaTakipDto.SiparisKalemKod, seciliPlanlamaTakipDto.UretimEmriKod);

            _view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _view.Show();
        }

        private void OnPlanla()
        {
            UretimEmriView _view = new UretimEmriView();
            _view.DataContext = new UretimEmriViewModel(seciliPlanlamaTakipDto.SiparisKalemKod, null);

            _view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _view.Show();
        }

        private void OnSiparisKalemleriGoster()
        {
            uow = new UnitOfWork();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirKalemPlanlamaTakipDto();
        }

        private void SeciliPlanlamaTakipDto_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChanged") return;

            if (e.PropertyName == nameof(seciliPlanlamaTakipDto.AmbalajKafesOlcu))
            {
                uow.PlanlamaRepo.KalemKafesOlcuKaydet(seciliPlanlamaTakipDto.SiparisKalemKod, seciliPlanlamaTakipDto.AmbalajKafesOlcu);
                return;
            }

            uow.PlanlamaRepo.UretimEmriBilgileriKaydet(SeciliPlanlamaTakipDto);
        }
    }
}