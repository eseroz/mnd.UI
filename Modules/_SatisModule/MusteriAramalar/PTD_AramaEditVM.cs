using DevExpress.Mvvm;
using mnd.Logic.BC_Satis._PotansiyelDisi;
using mnd.Logic.Model;
using mnd.UI.Modules._SatisModule.MusteriAramalar.Events;
using System;
using System.Collections.Generic;
using System.Windows;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    public class MusteriGrubu
    {
        public string MusteriGrubuAdı { get; set; }
    }
    public class PTD_AramaEditVM: MyBindableBase
    {
        private int id;
        public List<P_UlkeSabit> Ulkeler { get; }
        public PotansiyelDisiMusteriArama EditModel { get; set; }

        public List<MusteriGrubu> MusteriGrupListesi {
            get => musteriGrupListesi;
            set { 
               SetProperty(ref musteriGrupListesi, value);
            }
        }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);
        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);

        PotansiyelDisiRepository repo = new PotansiyelDisiRepository();
        private List<MusteriGrubu> musteriGrupListesi;

        private void OnKaydet()
        {
            var hata = ValidateForm();
            if (hata.Length > 0)
            {
                MessageBox.Show(hata, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (EditModel.Id == 0)
            {
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

        public PTD_AramaEditVM(int aramaId)
        {

            MusteriGrupListesi = new List<MusteriGrubu>();
            MusteriGrupListesi.Add(new MusteriGrubu { MusteriGrubuAdı = "Potansiyel" });
            MusteriGrupListesi.Add(new MusteriGrubu { MusteriGrubuAdı = "Potansiyel Dışı" });

            if (aramaId == 0)
            {
                EditModel = new PotansiyelDisiMusteriArama();
                EditModel.Ekleyen = AppPandap.AktifKullanici.KullaniciId;
            }
            else
            {
                EditModel = repo.Ptd_AramaGetir(aramaId);
            }


            Ulkeler = repo.UlkeleriGetir();
        }
    }
}
