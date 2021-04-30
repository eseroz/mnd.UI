using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    public class MusteriGrubu
    {
        public string MusteriGrubuAdı { get; set; }
    }

    public class PTD_AramaEditVM : MyBindableBase
    {
        public PotansiyelDisiRepository repo { get => repo1; set => SetProperty(ref repo1, value); }
        public ObservableCollection<PotansiyelDisiMusteri> PotansiyelMusteriListesi
        {
            get
            {
                potansiyelMusteriListesi = repo.PTD_Aramalari_Getir(AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';'), MusteriGrubuAdi);
                return potansiyelMusteriListesi;
            }
            set => SetProperty(ref potansiyelMusteriListesi, value);
        }
        public List<P_UlkeSabit> Ulkeler { get { return repo.UlkeleriGetir(); } }
        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);
        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);

        private ICommand<string> _Musteri_ProcessNewValue;
        public ICommand<string> Musteri_ProcessNewValue
        {
            get
            {
                if (_Musteri_ProcessNewValue == null)
                    _Musteri_ProcessNewValue = new DelegateCommand<string>(Musteri_AddNewValue);
                return _Musteri_ProcessNewValue;
            }
        }
        private void Musteri_AddNewValue(string newValue)
        {
            var comm = MessageBox.Show("Yeni Firma Eklensin mi?",
                  "Yeni Firma Oluştur",
                 System.Windows.MessageBoxButton.YesNo,
                  MessageBoxImage.Question);
            if (comm == MessageBoxResult.Yes)
            {
                if (PotansiyelMusteriListesi.Where(p => p.MusteriUnvan == newValue).Count() == 0)
                {
                    var yeniMüsteri = new PotansiyelDisiMusteri { 
                        MusteriUnvan = newValue, 
                        PlasiyerAd = AppPandap.AktifKullanici.AdSoyad,
                        PlasiyerKod = AppPandap.AktifKullanici.PlasiyerKod,
                        MusteriGrubuAdi = MusteriGrubuAdi 
                    };

                    PotansiyelMusteriListesi.Add(yeniMüsteri);
                    repo.MusteriEkle(yeniMüsteri);
                }
            }
        }
        public PotansiyelDisiMusteriArama SeciliPotansiyelDisiMusteriArama
        {
            get => seciliPotansiyelDisiMusteriArama;
            set {
                SetProperty(ref seciliPotansiyelDisiMusteriArama, value);
                if (seciliPotansiyelDisiMusteriArama != null)
                {
                    seciliPotansiyelDisiMusteriArama.PotansiyelDisiMusteri = new PotansiyelDisiMusteri { };
                }
            }
        }

        public PotansiyelDisiMusteri SeciliPotansiyelMusteri {
            get {
                seciliPotansiyelMusteri = SeciliPotansiyelDisiMusteriArama.PotansiyelDisiMusteri;
                return seciliPotansiyelMusteri; }
            set => SetProperty(ref seciliPotansiyelMusteri, value);
        }
        public bool YoneticiMi { get { return (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI); } }
        public bool YetkiliMi { get { return !(AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI); } }
        public string MusteriGrubuAdi { get; set; }
        public PTD_AramaEditVM(PotansiyelDisiRepository _repo)
        {
            repo = _repo;
        }

        private void OnKaydet()
        {
            var hata = ValidateForm();
            if (hata.Length > 0)
            {
                MessageBox.Show(hata, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SeciliPotansiyelDisiMusteriArama.PlasiyerKod = AppPandap.AktifKullanici.PlasiyerKod;

            if (SeciliPotansiyelDisiMusteriArama.Id == 0)
            {
                repo.AramaEkle(SeciliPotansiyelDisiMusteriArama);
                repo.Kaydet();
                Messenger.Default.Send(new PTD_MusteriAramaEklendiEvents(SeciliPotansiyelDisiMusteriArama));
            }
            else
            {
                repo.Kaydet();
                Messenger.Default.Send(new PTD_MusteriAramaGuncellendiEvent(SeciliPotansiyelDisiMusteriArama));
            }

            AppPandap.pDocumentManagerService.ActiveDocument.Close();
        }
        private void OnIptal()
        {
            AppPandap.pDocumentManagerService.ActiveDocument.Close();
        }
        private string ValidateForm()
        {
            var hata = "";
            if (SeciliPotansiyelDisiMusteriArama.Tarih == null) hata += "Tarih boş olamaz" + Environment.NewLine;
            //if (SeciliArama.MusteriUnvan == null) hata += "Müşteri Ünvanı boş olamaz" + Environment.NewLine;
            //if (String.IsNullOrEmpty(SeciliArama.UlkeAdi)) hata += "Ülke boş olamaz" + Environment.NewLine;

            if (SeciliPotansiyelDisiMusteriArama.GorusulenKisiAdi == null) hata += "Görüşülen kişi adı boş olamaz" + Environment.NewLine;
            if (SeciliPotansiyelDisiMusteriArama.GorusulenKisiEposta == null && SeciliPotansiyelDisiMusteriArama.GorusulenKisiTelefon == null) hata += "Görüşülen kişiye ait en az bir adet iletişim bilgisi girmelisiniz." + Environment.NewLine;

            //if (EditModel.GorusulenKisiGorevi == null) hata += "Görüşülen kişi görevi boş olamaz" + Environment.NewLine;

            if (SeciliPotansiyelDisiMusteriArama.Konu == null) hata += "Konu boş olamaz" + Environment.NewLine;
            if (SeciliPotansiyelDisiMusteriArama.KonuDetay == null) hata += "Konu detay boş olamaz" + Environment.NewLine;



            return hata;
        }

        private PotansiyelDisiMusteriArama seciliPotansiyelDisiMusteriArama;
        private ObservableCollection<PotansiyelDisiMusteri> potansiyelMusteriListesi;
        private PotansiyelDisiRepository repo1;
        private PotansiyelDisiMusteri seciliPotansiyelMusteri;
    }
}