using System;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Mvvm;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.Helper;

namespace mnd.UI.Modules.KaliteModule
{
    public class ReceteListViewModel : MyDxViewModelBase
    {
        private ObservableCollection<Recete> receteler;

        public ObservableCollection<Recete> Receteler
        {
            get => receteler;
            set => SetProperty(ref receteler, value);
        }

        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand KaydetCommand => new DelegateCommand(Kaydet);

        public DelegateCommand<Recete> KayitGuncellendiCommand => new DelegateCommand<Recete>(OnKayitGuncellendi, c => true);

      

        public DelegateCommand<Recete> YeniKayitOlusturuluyorCommand => new DelegateCommand<Recete>(OnYeniKayitOlusturuluyor, c => true);

        private void OnYeniKayitOlusturuluyor(Recete obj)
        {
            obj.Ekleyen = AppPandap.AktifKullanici.AdSoyad;
            obj.EklenmeTarihi = DateTime.Now;
            obj.RowGuid = Guid.NewGuid();
        }
        private void OnKayitGuncellendi(Recete item)
        {
            if (item.Id == 0)
            {
                uow.KaliteRepo.ReceteEkle(item);
                MessageBox.Show("Kayıt Eklendi", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                item.Guncelleyen = AppPandap.AktifKullanici.AdSoyad;
                item.GuncellenmeTarihi = DateTime.Now;

                MessageBox.Show("Kayıt Güncellendi", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
            }


            uow.Commit();

        }


        private void Kaydet()
        {
            MessageBox.Show("Kayıt Güncellendi", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
            uow.Commit();
        }

        public ReceteListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            Receteler = uow.KaliteRepo.ReceteListesiGetir();
        }
    }
}