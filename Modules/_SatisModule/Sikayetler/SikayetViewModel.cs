using DevExpress.Mvvm;
using mnd.Logic.BC_Satis._Sikayet;
using mnd.Logic.BC_Satis._Sikayet.DataServices;
using mnd.UI.Modules._DialogViews.MusteriSecimDialog;
using mnd.UI.Modules.TeklifModule.MessangerEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules._SatisModule.Sikayetler
{
    public class SikayetViewModel
    {
        public DelegateCommand<object> MusteriSecCommand => new DelegateCommand<object>(OnMusteriSec, c => true);

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet, true);

        public List<SikayetBolum> SeciliBolumler { get; set; } = new List<SikayetBolum>();


        public List<SikayetKonuKategori> KonuKategorileri { get; set; }


        public SikayetViewModel()
        {
            SikayetDataService = new SikayetDataService();

            SikayetBolumleri = SikayetDataService.SikayetBolumleriGetir().Select(c => c.BolumAd).ToList();
            KonuKategorileri = SikayetDataService.SikayetKonuKategorileriGetir();
        }

        private void OnKaydet()
        {
            SeciliSikayet.SikayetKonuBolumleriMetin =
                        String.Join(";", SeciliSikayet.SeciliSikayetBolumleri.ToList());

            if (SeciliSikayet.Id == 0)
            {
                repo.Kaydet();
                repo.Ekle(SeciliSikayet);
                repo.Kaydet();

                var obj = new SikayetEklendiEvent(SeciliSikayet);


                Messenger.Default.Send(obj);
            }
            else
            {
                repo.Kaydet();
            }
            MessageBox.Show("Şikayet Kaydedildi", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        SikayetRepository repo = new SikayetRepository();
        public Sikayet SeciliSikayet { get; set; }
        public SikayetDataService SikayetDataService { get; }
        public List<String> SikayetBolumleri { get; }

        public void Load(int id)
        {
            if (id != 0)
                SeciliSikayet = repo.SikayetGetir(id);
            else
            {
                SeciliSikayet = new Sikayet();
                SeciliSikayet.SurecKonum = "Taslak";
                SeciliSikayet.RowGuid = Guid.NewGuid();
                SeciliSikayet.SikayetTarihi = DateTime.Now;
            }

            if (SeciliSikayet.SikayetKonuBolumleriMetin == null) SeciliSikayet.SikayetKonuBolumleriMetin = "";
            SeciliSikayet.SeciliSikayetBolumleri = SeciliSikayet.SikayetKonuBolumleriMetin.Split(';').Cast<Object>().ToList();

        }

        private void OnMusteriSec(object obj)
        {
            MusteriSecView vw = new MusteriSecView();
            MusteriSecVM vm = new MusteriSecVM();

            vw.DataContext = vm;

            Messenger.Default.Register<MusteriSecildiEvent>(this, MusteriSecildi);

            vw.ShowDialog();
        }

        private void MusteriSecildi(MusteriSecildiEvent obj)
        {
            if (obj == null)
            {
                Messenger.Default.Unregister<MusteriSecildiEvent>(this, MusteriSecildi);
                return;
            }

            this.SeciliSikayet.SikayetEdenFirmaKod = obj?.Musteri.CariKod;
            this.SeciliSikayet.SikayetFirmaAd = obj?.Musteri.CariAd;




        }
    }
}
