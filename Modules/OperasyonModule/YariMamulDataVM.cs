using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using mnd.Logic.BC_App;
using mnd.Logic.BC_Operasyon;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.OperasyonModule
{
    public class YariMamulDataVM : MyDxViewModelBase
    {
        private DateTime seciliGun;

        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));
        public DelegateCommand FormUnLoadedCommand => new DelegateCommand(OnFormUnLoaded);

        public ObservableCollection<YariMamulHatData> YariMamulHatData { get => yariMamulHatData; set => SetProperty(ref yariMamulHatData, value); }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);

        YariMamulRepository repo = new YariMamulRepository();
        private ObservableCollection<YariMamulHatData> yariMamulHatData;

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded);

        public DateTime SeciliGun
        {
            get => seciliGun;
            set
            {
                SetProperty(ref seciliGun, value);
                YariMamulHatData = repo.YariMamulDataGetir(seciliGun, HatListesi);
            }
        }

        public List<string> HatListesi { get; set; }


        public YariMamulDataVM()
        {

        }
        public YariMamulDataVM(string FormAd)
        {
            IsMerkeziRepository repo = new IsMerkeziRepository();
            HatListesi = repo.IsMerkeziUretimMakinalariGetir()
                               .Select(c => c.Tanim)
                               .ToList();

        }

        private void OnFormLoaded()
        {
            SeciliGun = DateTime.Now.Date.AddDays(-1);
        }


        private void OnKaydet()
        {
            if (seciliGun.Date >= DateTime.Now.Date)
            {
                MessageBox.Show("Bugün ve ileri bir tarihe veri girişi yapamazsınız", "Pandap", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            this.YariMamulHatData.ToList().ForEach(c =>
            {
                c.UpdateUserId = AppPandap.AktifKullanici.KullaniciId;
                c.SonKayitTarihi = DateTime.Now.Date;
            });

            repo.Kaydet();

            var mesaj = "Kayıt İşlemi Başarıyla Tamamlandı";
            BoundMessageQueue.Enqueue(mesaj, true);
        }

        public bool FormLoaded { get; set; }

        private void OnFormUnLoaded()
        {


        }



    }
}
