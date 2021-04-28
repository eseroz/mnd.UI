using DevExpress.Mvvm;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Windows.Input;

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
                    PotansiyelMusteriListesi.Add(new PotansiyelMusteriDTO { MusteriUnvan = newValue });
                    SecilenPotansiyelMusteri = PotansiyelMusteriListesi.Where(p => p.MusteriUnvan == newValue).FirstOrDefault();
                }
            }
        }
        public PotansiyelDisiMusteriArama SeciliArama
        {
            get => seciliArama;
            set
            {
                SetProperty(ref seciliArama, value);
                EditModel = SeciliArama;

            }
        }
        public PotansiyelMusteriDTO SecilenPotansiyelMusteri
        {
            get => secilenPotansiyelMusteri;
            set
            {
                SetProperty(ref secilenPotansiyelMusteri, value);

                if (secilenPotansiyelMusteri != null)
                {

                    List<PotansiyelDisiMusteriArama> liste = secilenPotansiyelMusteri.MusteriAramalarDTO;

                    if (liste.Count > 0)
                    {
                        var LastRecord = liste[liste.Count - 1];

                        EditModel.UlkeAdi = secilenPotansiyelMusteri.UlkeAdi;
                        EditModel.UlkeKodu = secilenPotansiyelMusteri.UlkeKodu;
                        EditModel.GorusulenKisiAdi = LastRecord.GorusulenKisiAdi;
                        EditModel.GorusulenKisiEposta = LastRecord.GorusulenKisiEposta;
                        EditModel.GorusulenKisiGorevi = LastRecord.GorusulenKisiGorevi;
                        EditModel.GorusulenKisiTelefon = LastRecord.GorusulenKisiTelefon;

                        SeciliUlke = Ulkeler.Where(p => p.UlkeKodu == secilenPotansiyelMusteri.UlkeKodu).FirstOrDefault();
                    }
                }
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
        private PotansiyelMusteriDTO secilenPotansiyelMusteri;
        private PotansiyelDisiMusteriArama seciliArama;
        private string uC_Title;

        public string UC_Title { get => uC_Title; set => SetProperty(ref uC_Title, value); }

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
        public string MusteriGrubuAdi
        {
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
            if (EditModel.MusteriUnvan == null) hata += "Müşteri Ünvanı boş olamaz" + Environment.NewLine;
            if (String.IsNullOrEmpty(EditModel.UlkeAdi)) hata += "Ülke boş olamaz" + Environment.NewLine;

            if (EditModel.GorusulenKisiAdi == null) hata += "Görüşülen kişi adı boş olamaz" + Environment.NewLine;
            if (EditModel.GorusulenKisiEposta == null) hata += "Görüşülen kişi eposta adresi boş olamaz" + Environment.NewLine;
            if (EditModel.GorusulenKisiTelefon == null) hata += "Görüşülen kişi telefon numarası boş olamaz" + Environment.NewLine;
            if (EditModel.GorusulenKisiGorevi == null) hata += "Görüşülen kişi görevi boş olamaz" + Environment.NewLine;

            if (EditModel.Konu == null) hata += "Konu boş olamaz" + Environment.NewLine;
            if (EditModel.KonuDetay == null) hata += "Konu detay boş olamaz" + Environment.NewLine;



            return hata;
        }
        public PTD_AramaEditVM(int Id = 0)
        {
            PotansiyelMusteriListesi = new ObservableCollection<PotansiyelMusteriDTO>();

            PotansiyelMusteriListesi = repo.PTD_Aramalari_Getir(AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';'), MusteriGrubuAdi);
            UC_Title = "Yeni Arama";
            if (Id == 0)
            {
                EditModel = new PotansiyelDisiMusteriArama();
                EditModel.Ekleyen = AppPandap.AktifKullanici.KullaniciId;
            }
            else
            {
                EditModel = repo.Ptd_AramaGetir(Id);
                UC_Title = EditModel.MusteriGrubuAdı +"=>"+ EditModel.MusteriUnvan + " Arama Geçmişi..";
            }

            Ulkeler = repo.UlkeleriGetir();
        }
    }
}