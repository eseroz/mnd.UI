using DevExpress.Mvvm;
using mnd.Logic.BC_Satis._Seyahat;
using mnd.Logic.BC_Satis._Seyahat.DataServices;
using mnd.UI.Modules._SatisModule.SeyahatRaporlari.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mnd.UI.Modules._SatisModule.SeyahatRaporlari
{
    public class GorusmeEditVM
    {
        private int id;


        public SeyahatGorusme EditModel { get; set; }

        public Seyahat Seyahat { get; set; }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);
        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);

        SeyahatRepository repo = new SeyahatRepository();
        private void OnKaydet()
        {
            var hata = ValidateForm();
            if (hata.Length > 0)
            {
                MessageBox.Show(hata, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            repo.Kaydet();

            Messenger.Default.Send(new SeyahatGuncellendiEvent(Seyahat));

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
            if (String.IsNullOrEmpty(EditModel.Sehir)) hata += "Şehir boş olamaz" + Environment.NewLine;
            if (EditModel.AlinacakAksiyon == null) hata += "Aksiyon boş olamaz" + Environment.NewLine;

            return hata;
        }

        public GorusmeEditVM(int seyahatId, int gorusmeId)
        {
            this.id = seyahatId;

            Seyahat = repo.SeyahatGetir(seyahatId);

            if (gorusmeId == 0)
            {
                EditModel = new SeyahatGorusme();
                Seyahat.Gorusmeler.Add(EditModel);
            }
            else
            {
                EditModel = Seyahat.Gorusmeler.First(x => x.Id == gorusmeId);
            }
               
        }
    }
}
