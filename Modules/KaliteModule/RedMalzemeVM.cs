using DevExpress.Mvvm;
using mnd.Logic.BC_Kalite;
using mnd.Logic.BC_Kalite.Domain;
using mnd.Logic.BC_Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mnd.UI.Modules.KaliteModule
{
    public class RedMalzemeVM
    {
        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet, true);

        //public List<SikayetBolum> SeciliBolumler { get; set; } = new List<SikayetBolum>();

        //public List<SikayetKonuKategori> KonuKategorileri { get; set; }


        public KaliteRedMalzeme SeciliRedMalzeme { get; set; }

        KaliteRedMalzemeService redMalzemeService = new KaliteRedMalzemeService();
        UretimKaliteService uretimKaliteService = new UretimKaliteService();

        KaliteRedMalzemeRepository repo = new KaliteRedMalzemeRepository();
        public RedMalzemeVM()
        {
           

            //SikayetBolumleri = SikayetDataService.SikayetBolumleriGetir().Select(c => c.BolumAd).ToList();
            //KonuKategorileri = SikayetDataService.SikayetKonuKategorileriGetir();
        }

        public void Load(int id)
        {
            if (id != 0)
                SeciliRedMalzeme = repo.GetirById(id);
            else
            {
                SeciliRedMalzeme = new KaliteRedMalzeme();
                SeciliRedMalzeme.RowGuid = Guid.NewGuid();
                SeciliRedMalzeme.EklenmeTarihi = DateTime.Now;
            }

            SeciliRedMalzeme.PropertyChanged += SeciliRedMalzeme_PropertyChanged;

            
        }

        private void SeciliRedMalzeme_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if(e.PropertyName=="UretimEmriKod")
            //{
            //    if (SeciliRedMalzeme.UretimEmriKod == "") return;

            //    var uretimEmriDto = uretimKaliteService.KaliteUretimEmriGetir(SeciliRedMalzeme.UretimEmriKod);

            //    if(uretimEmriDto==null)
            //    {
            //        MessageBox.Show("Üretim Emri Bulunamadı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }

            //    SeciliRedMalzeme.Musteri = uretimEmriDto.Musteri;
            //    SeciliRedMalzeme.En = uretimEmriDto.En.ToString();
            //    SeciliRedMalzeme.Kalinlik = uretimEmriDto.Kalinlik.ToString();
            //    SeciliRedMalzeme.Alasim = uretimEmriDto.Alasim;
            //    SeciliRedMalzeme.Kondusyon = uretimEmriDto.Kondusyon;
            //    SeciliRedMalzeme.KartNo = uretimEmriDto.KartNo;


            //}

            //if (e.PropertyName == "KartNo")
            //{
            //    if (SeciliRedMalzeme.KartNo == "") return;

               

            //    var uretimEmriDto = uretimKaliteService.KaliteUretimEmriGetirFromKartNo(SeciliRedMalzeme.KartNo);

            //    if (uretimEmriDto == null)
            //    {
            //        MessageBox.Show("Kart Bulunamadı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }

            //    SeciliRedMalzeme.Musteri = uretimEmriDto.Musteri;
            //    SeciliRedMalzeme.En = uretimEmriDto.En.ToString();
            //    SeciliRedMalzeme.Kalinlik = uretimEmriDto.Kalinlik.ToString();
            //    SeciliRedMalzeme.Alasim = uretimEmriDto.Alasim;
            //    SeciliRedMalzeme.Kondusyon = uretimEmriDto.Kondusyon;
            //    SeciliRedMalzeme.UretimEmriKod = uretimEmriDto.UretimEmriKod;


            //}
        }

        private void OnKaydet()
        {
            

            if (SeciliRedMalzeme.Id == 0)
            {
                repo.Kaydet();
                repo.Ekle(SeciliRedMalzeme);
                repo.Kaydet();

                //var obj = new SikayetEklendiEvent(SeciliRedMalzeme);


                //Messenger.Default.Send(obj);
            }
            else
            {
                repo.Kaydet();
            }
            MessageBox.Show("Kayıt işlemi tamamlandı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);

        }

   
     

      
    }
}


