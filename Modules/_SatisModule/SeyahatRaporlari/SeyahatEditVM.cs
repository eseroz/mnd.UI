using DevExpress.Mvvm;
using mnd.Logic.BC_Satis._Seyahat;
using mnd.Logic.BC_Satis._Seyahat.DataServices;
using mnd.UI.Modules._SatisModule.SeyahatRaporlari.Events;

using System;
using System.Collections.Generic;
using System.Windows;

namespace mnd.UI.Modules._SatisModule.SeyahatRaporlari
{
    public class SeyahatEditVM
    {
        private int id;

        public Seyahat EditModel { get; set; }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);
        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);

        public List<UlkeSabit> Ulkeler { get; }

        SeyahatRepository repo = new SeyahatRepository();
        private void OnIptal()
        {
            AppPandap.pDocumentManagerService.ActiveDocument.Close();
        }

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
                Messenger.Default.Send(new SeyahatEklendiEvent(EditModel));
            }
            else
            {
                repo.Kaydet();
                Messenger.Default.Send(new SeyahatGuncellendiEvent(EditModel));
            }

            AppPandap.pDocumentManagerService.ActiveDocument.Close();


        }

        public SeyahatEditVM(int id)
        {
            this.id = id;
            Ulkeler = repo.UlkeleriGetir();

            if (id == 0)
            {
                EditModel = new Seyahat();
                EditModel.Ekleyen = AppPandap.AktifKullanici.KullaniciId;
            }
            else
                EditModel = repo.SeyahatGetir(this.id);


        }

        public string ValidateForm()
        {
            var hata = "";
            if (EditModel.BaslangicTarihi == null) hata += "Başlangıç tarihi boş olamaz" + Environment.NewLine;
            if (EditModel.BitisTarihi == null) hata += "Bitiş tarihi boş olamaz" + Environment.NewLine;
            if (String.IsNullOrEmpty(EditModel.UlkeAd)) hata += "Ülke boş olamaz" + Environment.NewLine;

            return hata;

        }
    }
}
