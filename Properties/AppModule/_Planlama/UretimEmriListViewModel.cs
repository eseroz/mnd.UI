using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using Pandap.Logic.Helper;
using Pandap.Logic.Model;
using Pandap.Logic.Model.Satis;
using Pandap.Logic.Model.Uretim;
using Pandap.Logic.Persistence;

namespace Pandap.UI.AppModule._Planlama
{
    public class UretimEmriListViewModel : MyBindableBase
    {
        private UnitOfWork uow = new UnitOfWork();

        private UretimEmri seciliUretimEmri;
        private int _paketlameToleransYuzde;

        public UretimEmri SeciliUretimEmri
        {
            get { return seciliUretimEmri; }
            set { SetProperty(ref seciliUretimEmri, value); }
        }

        public int PaketlemeToleransYuzde { get => _paketlameToleransYuzde; set =>SetProperty(ref _paketlameToleransYuzde, value); }

        public DelegateCommand<object> PaketlemeToleransGuncelleCommand => new DelegateCommand<object>(OnPaketlemeToleransGuncelle, c => true);

        private void OnPaketlemeToleransGuncelle(object obj)
        {
            var uowKalite = new UnitOfWork();
            uowKalite.PlanlamaRepo.PaketlemeToleransGuncelle(PaketlemeToleransYuzde);
        }

        public ObservableCollection<UretimEmri> UretimEmirleri { get; set; }

        public DelegateCommand<object> KapatIslemCommand => new DelegateCommand<object>(onUretimEmriKapat, c => true);

        public async void onUretimEmriKapat(object obj)
        {
            var mesaj = "Üretimde" + SeciliUretimEmri.Uretim_UretimdekiMiktar + " bakiye bulunmaktadır \r\n Bakiye Siparişe eklensin mi";

            uow = new UnitOfWork();
            var uretimEmri = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(SeciliUretimEmri.UretimEmriKod);

            var mesajSonuc = MessageBox.Show(mesaj, "Pandap", MessageBoxButton.YesNoCancel);

            SiparisKalem s = uow.SiparisKalemRepo.SiparisKalemiGetir(SeciliUretimEmri.SiparisKalemKod);

            if (mesajSonuc == MessageBoxResult.Yes)
            {
                s.PLAN_PlanlanacakKalanMiktarToplam = s.PLAN_PlanlanacakKalanMiktarToplam + uretimEmri.Uretim_UretimdekiMiktar;
            }

            if (mesajSonuc == MessageBoxResult.No)
            {
                uretimEmri.KapatildiMi = true;
            }

            uow.Commit();
        }

        public UretimEmriListViewModel()
        {
            UretimEmirleri = uow.PlanlamaRepo.UretimEmirleriGetir()
                .Where(c => c.KapatildiMi == false || c.KapatildiMi == null)
                .OrderByDescending(c => c.UretimEmriKod)
                .ToObservableCollection();

            var uowKalite = new UnitOfWork();
            PaketlemeToleransYuzde = uowKalite.PlanlamaRepo.PaketlemeToleransGetir();
        }
    }
}