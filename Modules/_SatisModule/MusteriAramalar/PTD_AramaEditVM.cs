using DevExpress.Mvvm;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    public class MusteriGrubu
    {
        public string MusteriGrubuAdı { get; set; }
    }

    public class PTD_AramaEditVM : MyBindableBase
    {
        private int id;
        public List<P_UlkeSabit> Ulkeler { get; }
        public PotansiyelDisiMusteriArama EditModel
        {
            get => editModel;
            set { SetProperty(ref editModel, value); }
        }

        public List<MusteriGrubu> MusteriGrupListesi
        {
            get => musteriGrupListesi;
            set
            {
                SetProperty(ref musteriGrupListesi, value);
            }
        }

        public ObservableCollection<PotansiyelMusteriDTO> PotansiyelMusteriListesi
        {
            get => potansiyelMusteriListesi;
            set
            {
                SetProperty(ref potansiyelMusteriListesi, value);
                potansiyelMusteriListesi = repo.PTD_Aramalari_Getir(AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';'), MusteriGrubuAdi);
            }
        }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);
        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);

        PotansiyelDisiRepository repo = new PotansiyelDisiRepository();
        private List<MusteriGrubu> musteriGrupListesi;

        private P_UlkeSabit seciliUlke;
        private PotansiyelDisiMusteriArama editModel;
        private ObservableCollection<PotansiyelMusteriDTO> potansiyelMusteriListesi;
        private string musteriGrubuAdi;

        public P_UlkeSabit SeciliUlke
        {
            get => seciliUlke;
            set
            {
                SetProperty(ref seciliUlke, value);
            }
        }

        private void OnKaydet()
        {
            var hata = ValidateForm();
            if (hata.Length > 0)
            {
                MessageBox.Show(hata, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EditModel.MusteriGrubuAdı = MusteriGrubuAdi;

            if (EditModel.Id == 0)
            {
                EditModel.PlasiyerKod = AppPandap.AktifKullanici.PlasiyerKod;
                repo.Ekle(EditModel);
                repo.Kaydet();
                Messenger.Default.Send(new PTD_MusteriAramaEklendiEvents(EditModel));
            }
            else
            {
                repo.Kaydet();
                Messenger.Default.Send(new PTD_MusteriAramaGuncellendiEvent(EditModel));
            }

            AppPandap.pDocumentManagerService.ActiveDocument.Close();


        }

        public string MusteriGrubuAdi { 
            get => musteriGrubuAdi;
            set => SetProperty(ref musteriGrubuAdi, value);
        }

        private void OnIptal()
        {
            AppPandap.pDocumentManagerService.ActiveDocument.Close();
        }

        private string ValidateForm()
        {
            var hata = "";
            if (EditModel.Tarih == null) hata += "Tarih boş olamaz" + Environment.NewLine;
            if (EditModel.KonuDetay == null) hata += "Konu detay boş olamaz" + Environment.NewLine;
            if (String.IsNullOrEmpty(EditModel.UlkeAdi)) hata += "Ülke boş olamaz" + Environment.NewLine;


            return hata;
        }

        public PTD_AramaEditVM()
        {

        }
        public PTD_AramaEditVM(string _musteriGrubuAdı, int Id)
        {
            PotansiyelMusteriListesi = new ObservableCollection<PotansiyelMusteriDTO>();
            MusteriGrubuAdi = _musteriGrubuAdı;
            PotansiyelMusteriListesi = repo.PTD_Aramalari_Getir(AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';'), MusteriGrubuAdi);

            if (Id == 0)
            {
                EditModel = new PotansiyelDisiMusteriArama();
                EditModel.Ekleyen = AppPandap.AktifKullanici.KullaniciId;
            }
            else
            {
                EditModel = repo.Ptd_AramaGetir(Id);
            }


            Ulkeler = repo.UlkeleriGetir();
        }
    }
}
