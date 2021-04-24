using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Uretim;
using mnd.Logic.BC_Uretim.SH_RotaModel;
using mnd.UI.Helper;
using mnd.UI.Modules.PlanlamaModule.RotaKartBolum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.PlanlamaModule.ShRotaKartBolum
{
    public class ShRotaKartListVM : MyDxViewModelBase
    {
        ShRotaKartTrackingRepository repo = new ShRotaKartTrackingRepository();
        private int seciliYil;
        private List<ShRotaKart> rotaKartlari;

        public DelegateCommand<ShRotaKart> DuzenleCommand => new DelegateCommand<ShRotaKart>(OnDuzenle, true);

        public ObservableCollection<int> Yillar { get; set; }
        private void OnDuzenle(ShRotaKart obj)
        {
            ShRotaKartView _view = new ShRotaKartView();

            var vm = new ShRotaKartVM(obj.KartNo, KayitModu.Edit, null);

            _view.DataContext = vm;

            _view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _view.Show();
        }

        public List<ShRotaKart> RotaKartlari { get => rotaKartlari; set => SetProperty(ref rotaKartlari , value); }

        public ShRotaKart SeciliKayit { get; set; }

        public int SeciliYil
        {
            get => seciliYil; set
            {
                seciliYil = value;

                SetProperty(ref seciliYil, value);

                RotaKartlari = repo.RotaKartlariGetirYildan(value);

                SeciliKayit = RotaKartlari.Count > 0 ? RotaKartlari.Last() : null;
            }
        }

        public ShRotaKartListVM(string form)
        {
            seciliYil = DateTime.Now.Year;

            Yillar = new ObservableCollection<int>();

            for (int i = 2019; i <= SeciliYil + 1; i++)
            {
                Yillar.Add(i);
            }

            RotaKartlari = repo.RotaKartlariGetirYildan(seciliYil);


        }


    }
}
