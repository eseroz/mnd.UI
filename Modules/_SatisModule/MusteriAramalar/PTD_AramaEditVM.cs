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
            get => potansiyelMusteriListesi;
            set => SetProperty(ref potansiyelMusteriListesi, value);
        }
        public List<P_UlkeSabit> Ulkeler { get; set; }
        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);
        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);
        public DelegateCommand FormLoadedCommand => new DelegateCommand(FormLoaded);
        public DelegateCommand FormUnLoadedCommand => new DelegateCommand(FormUnLoaded);
        public DelegateCommand ClosingCommand => new DelegateCommand(Closing);
        private void Closing()
        {
           
        }
        public P_UlkeSabit SeciliUlke
        {
            get => seciliUlke;
            set
            {
                SetProperty(ref seciliUlke, value);

                //gökmen abi beni neden aramıyor olabilir? bilmiyorum ve anlayamıyorum.
            }
        }
        public bool KayitEditMi { get; set; }
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
            var dialogResult = MessageBox.Show("Yeni Firma Eklensin mi?",
                  "Yeni Firma Oluştur",
                 System.Windows.MessageBoxButton.YesNo,
                  MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                if (PotansiyelMusteriListesi.Where(p => p.MusteriUnvan == newValue.Trim()).Count() == 0)
                {
                    var yeniMüsteri = new PotansiyelDisiMusteri
                    {
                        MusteriUnvan = newValue.Trim(),
                        PlasiyerAd = AppPandap.AktifKullanici.AdSoyad,
                        PlasiyerKod = AppPandap.AktifKullanici.PlasiyerKod,
                        MusteriGrubuAdi = MusteriGrubuAdi
                    };
                    //yeniMüsteri.PotansiyelDisiMusteriArama.Add(SeciliPotansiyelDisiMusteriArama);
                    PotansiyelMusteriListesi.Add(yeniMüsteri);
                    repo.MusteriEkle(yeniMüsteri);
                    SeciliMusteri = yeniMüsteri;
                    yeniMüsteri.PotansiyelDisiMusteriArama.Add(SeciliPotansiyelDisiMusteriArama);
                }
            }
        }
        public PotansiyelDisiMusteri SeciliMusteri
        {
            get {           
                return seciliMusteri;
            }
            set {
                seciliMusteri = value;

                if (seciliMusteri != null)
                {

                    var sonArama = repo.Ptd_SonAramaGetir(seciliMusteri.Id);
                    if (sonArama != null)
                    {
                        SeciliPotansiyelDisiMusteriArama.GorusulenKisiAdi = sonArama.GorusulenKisiAdi;
                        SeciliPotansiyelDisiMusteriArama.GorusulenKisiEposta = sonArama.GorusulenKisiEposta;
                        SeciliPotansiyelDisiMusteriArama.GorusulenKisiGorevi = sonArama.GorusulenKisiGorevi;
                        SeciliPotansiyelDisiMusteriArama.GorusulenKisiTelefon = sonArama.GorusulenKisiTelefon;
                        SeciliPotansiyelDisiMusteriArama.UlkeKodu = seciliMusteri.UlkeKodu;
                        //seciliMusteri.UlkeKodu = SeciliUlke.UlkeKodu;
                        //seciliMusteri.UlkeAdi = SeciliUlke.UlkeAdi;


                        //var ulke = repo.getUlke(seciliMusteri.UlkeKodu);
                        //SeciliUlke = ulke;
                        //if (SeciliUlke != null)
                        //{
                        //    seciliMusteri.UlkeKodu = SeciliUlke.UlkeKodu;
                        //    seciliMusteri.UlkeAdi = SeciliUlke.UlkeAdi;
                        //}
                    }
                }
                SetProperty(ref seciliMusteri, value);
            }
        }
        public PotansiyelDisiMusteriArama SeciliPotansiyelDisiMusteriArama
        {
            get => seciliPotansiyelDisiMusteriArama;
            set
            {
                SetProperty(ref seciliPotansiyelDisiMusteriArama, value);
            }
        }
        public bool YoneticiMi { get { return (AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI); } }
        public bool YetkiliMi { get { return !(AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.YONETICI); } }
        public IDocument AramaEditDocument { get; set; }
        public bool MusteriLookupReadyOnly { 
            get {
                if (YoneticiMi) return YoneticiMi;
                if (KayitEditMi) return KayitEditMi;
                
                return false;
            }
        }
        public string MusteriGrubuAdi { get => musteriGrubuAdi; set => SetProperty(ref musteriGrubuAdi, value); }
        public ObservableCollection<PotansiyelDisiMusteri> GelenMusteriListesi { 
            get => gelenMusteriListesi;
            set => SetProperty(ref gelenMusteriListesi, value);
        }
        public PTD_AramaEditVM(PotansiyelDisiRepository _repo, ObservableCollection<PotansiyelDisiMusteri> _gelenMusteriListesi)
        {
            GelenMusteriListesi = _gelenMusteriListesi;
            repo = _repo;
        }
        private void FormLoaded()
        {
            PotansiyelMusteriListesi = repo.PTD_Aramalari_Getir(AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';'), MusteriGrubuAdi);
            
            //SeciliPotansiyelDisiMusteri = SeciliPotansiyelDisiMusteriArama.PotansiyelDisiMusteri;
            //var x1 = SeciliPotansiyelDisiMusteri;
            //var x2 = SeciliPotansiyelDisiMusteriArama.PotansiyelDisiMusteriId;
            //var x3 = SeciliPotansiyelMusteri;
        }
        private void FormUnLoaded()
        {
            if (repo.DegisiklikVarMi())
            {
                var dialogResult = MessageBox.Show("Değişiklikler kaydedilsin mi?",
                    "Bilgi",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (dialogResult)
                {
                    case MessageBoxResult.Yes:
                        repo.Kaydet();
                        break;
                    case MessageBoxResult.No:
                        //repo.Dispose();
                        break;
                    case MessageBoxResult.Cancel:
                        AramaEditDocument.Show();
                        break;
                }

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

            SeciliPotansiyelDisiMusteriArama.PlasiyerKod = AppPandap.AktifKullanici.PlasiyerKod;
            SeciliPotansiyelDisiMusteriArama.MusteriUnvan = SeciliMusteri.MusteriUnvan;
            SeciliMusteri.UlkeKodu = SeciliUlke.UlkeKodu;
            SeciliMusteri.UlkeAdi = SeciliUlke.UlkeAdi;
            var musteri = GelenMusteriListesi.FirstOrDefault(p => p.Id == SeciliMusteri.Id);

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
            if (SeciliPotansiyelDisiMusteriArama.Tarih == null) hata += "Tarih alanı boş olamaz" + Environment.NewLine;
            //if (SeciliArama.MusteriUnvan == null) hata += "Müşteri Ünvanı boş olamaz" + Environment.NewLine;
            //if (String.IsNullOrEmpty(SeciliArama.UlkeAdi)) hata += "Ülke boş olamaz" + Environment.NewLine;

            if (SeciliPotansiyelDisiMusteriArama.GorusulenKisiAdi == null) hata += "Görüşülen kişi adı alanı boş olamaz" + Environment.NewLine;
            if (SeciliPotansiyelDisiMusteriArama.GorusulenKisiEposta == null && SeciliPotansiyelDisiMusteriArama.GorusulenKisiTelefon == null) hata += "Görüşülen kişiye ait en az bir adet iletişim bilgisi girmelisiniz." + Environment.NewLine;

            //if (EditModel.GorusulenKisiGorevi == null) hata += "Görüşülen kişi görevi boş olamaz" + Environment.NewLine;

            if (SeciliPotansiyelDisiMusteriArama.Konu == null) hata += "Konu alanı boş olamaz" + Environment.NewLine;
            if (SeciliPotansiyelDisiMusteriArama.KonuDetay == null) hata += "Detay alanı boş olamaz" + Environment.NewLine;



            return hata;
        }
        private PotansiyelDisiMusteriArama seciliPotansiyelDisiMusteriArama;
        private ObservableCollection<PotansiyelDisiMusteri> potansiyelMusteriListesi;
        private PotansiyelDisiRepository repo1;
        private string musteriGrubuAdi;
        private PotansiyelDisiMusteri seciliMusteri;
        private P_UlkeSabit seciliUlke;
        private ObservableCollection<PotansiyelDisiMusteri> gelenMusteriListesi;
    }
}