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
    public class P_UretimEmriListVM : MyBindableBase
    {
        private ObservableCollection<PlanlamaTakipDto> planlamaDtoListe;
        private string seciliMenu;
        private PlanlamaTakipDto seciliPlanlamaTakipDto;
        private Yerlesim seciliYerlesim;
        private UnitOfWork uow = new UnitOfWork();
        private ObservableCollection<Yerlesim> yerlesimler;
        public ObservableCollection<PlanlamaTakipDto> SeciliPlanlamaTakipDtos { get; set; }

        public DelegateCommand KartGosterCommand => new DelegateCommand(OnRotaKartGoster, true);

        public DelegateCommand<object> KapatIslemCommand => new DelegateCommand<object>(OnUretimEmriKapat, c => true);

        public DelegateCommand UretimEmirleriGosterCommand => new DelegateCommand(OnUretimEmirleriGoster, true);


        public DelegateCommand EkraniYenileCommand => new DelegateCommand(OnEkraniYenile, true);

        private void OnEkraniYenile()
        {
            uow = new UnitOfWork();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirUretimEmirleriPlanlamaTakipDto();
        }

        public P_UretimEmriListVM()
        {
            Yerlesimler = new ObservableCollection<Yerlesim>();

            yerlesimler.Add(new Yerlesim("1", "P_UretimEmirleri-1.xml"));
            yerlesimler.Add(new Yerlesim("2", "P_UretimEmirleri-2.xml"));
            yerlesimler.Add(new Yerlesim("3", "P_UretimEmirleri-3.xml"));

            SeciliYerlesim = yerlesimler.First();

            PlanlamaDtoListe = uow.PlanlamaRepo.GetirUretimEmirleriPlanlamaTakipDto();
        }

        public ObservableCollection<PlanlamaTakipDto> PlanlamaDtoListe
        {
            get => planlamaDtoListe;
            set => SetProperty(ref planlamaDtoListe, value);
        }

        public string SeciliMenu
        {
            get => seciliMenu;
            set => SetProperty(ref seciliMenu, value);
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

        public  void OnUretimEmriKapat(object obj)
        {
            if (SeciliPlanlamaTakipDto.UretimEmriKod == "")
            {
                MessageBox.Show("Üretim Emri Kodu Bulunamadı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var mesaj = "Üretimde" + SeciliPlanlamaTakipDto.UretimdekiMiktar + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

            uow = new UnitOfWork();
            var uretimEmri = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliPlanlamaTakipDto.UretimEmriKod);

            var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel);

            SiparisKalem s = uow.SiparisKalemRepo.SiparisKalemiGetir(SeciliPlanlamaTakipDto.SiparisKalemKod);

            if (mesajSonuc == MessageBoxResult.Yes)
            {
                s.PLAN_PlanlanacakKalanMiktarToplam = s.PLAN_PlanlanacakKalanMiktarToplam + uretimEmri.Uretim_UretimdekiMiktar;
            }

            if (mesajSonuc == MessageBoxResult.No)
            {
                SeciliPlanlamaTakipDto.KapatildiMi = true;
                uretimEmri.KapatildiMi = true;
              
            }

            uow.Commit();

        }

    

        private void OnRotaKartGoster()
        {
            UretimEmriView _view = new UretimEmriView();

            _view.DataContext = new UretimEmriViewModel(seciliPlanlamaTakipDto.SiparisKalemKod, seciliPlanlamaTakipDto.UretimEmriKod);

            _view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _view.Show();
        }

        private void OnUretimEmirleriGoster()
        {
            uow = new UnitOfWork();
            SeciliYerlesim = Yerlesimler.Where(c => c.Ad == "1").First();
            PlanlamaDtoListe = uow.PlanlamaRepo.GetirUretimEmirleriPlanlamaTakipDto();
            
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