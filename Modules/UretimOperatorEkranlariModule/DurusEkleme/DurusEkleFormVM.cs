using DevExpress.Mvvm;
using Newtonsoft.Json;
using mnd.Logic.BC_Uretim;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule.DurusEkleme
{
    public class DurusEkleFormVM : MyDxViewModelBase
    {
     
        public ObservableCollection<MakinaDurusTanim> MakinaDurusKodlari { get; set; }
        public MakinaAktiviteKayit MakinaDurusHareketEdit { get => makinaDurusHareketEdit; set => SetProperty(ref makinaDurusHareketEdit, value); }

        public ObservableCollection<MakinaAktiviteKayit> RunSonrasiIslemKayitListe { get => son5Duruş; set => SetProperty(ref son5Duruş, value); }

        public DelegateCommand<object> DurusEkleCommand => new DelegateCommand<object>(OnDurusEkle);

        public DelegateCommand<object> BaslatCommand => new DelegateCommand<object>(OnBaslat);

        public bool Ok { get; set; }

      //  public int DurusKalanDk => AralikDkToplam - RunSonrasiIslemKayitListe.Sum(c => c.SüreDkGiris).GetValueOrDefault();

        public int DurusKalanDk => 100;

        private void OnBaslat(object obj)
        {
            if (RunSonrasiIslemKayitListe.Count == 0)
            {
                MessageBox.Show("Duruş Kodu Ekleyiniz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.Ok = true;
            WindowService.Close();
        }


     
        private ObservableCollection<MakinaAktiviteKayit> son5Duruş;
        private MakinaAktiviteKayit makinaDurusHareketEdit;
        private MakinaAktiviteKayit ilkRun;
        private MakinaAktiviteKayit sonRun;

        public MakinaAktiviteKayit Run1 { get => ilkRun; set => SetProperty(ref ilkRun, value); }
        public MakinaAktiviteKayit Run2 { get => sonRun; set =>SetProperty(ref sonRun , value); }

        public int AralikDkToplam => 100;

 //       public int AralikDkToplam => (int)(Run2.BaşlangıçSaati - Run1.BitişSaati).Value.TotalMinutes;

        public DurusEkleFormVM(MakinaAktiviteKayit ilkRun, MakinaAktiviteKayit sonRun)
        {
            Run1 = ilkRun;
            Run2 = sonRun;
        }
        public DurusEkleFormVM()
        {

        }

        private void OnDurusEkle(object obj)
        {

            //var kontrol = MakinaDurusHareketEdit.BaşlangıçSaati > MakinaDurusHareketEdit.BitişSaati;

            //if(kontrol)
            //{
            //    MessageBox.Show("Başlangıç Saati Bitiş Saatinden Büyük olamaz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}


            var sonIslem = RunSonrasiIslemKayitListe.LastOrDefault();

            if (sonIslem == null) sonIslem = Run1;

            MakinaDurusHareketEdit.BaşlangıçSaati = sonIslem.BitişSaati.Value.AddMinutes(0);
            MakinaDurusHareketEdit.BitişSaati = MakinaDurusHareketEdit.BaşlangıçSaati.Value
                                                .AddMinutes(MakinaDurusHareketEdit.SüreDkGiris.GetValueOrDefault());

            if (MakinaDurusHareketEdit.BitişSaati > Run2.BaşlangıçSaati)
            {
                MessageBox.Show("Bitiş saati yeni RUN başlangıç saatini geçiyor");
                return;
            }


            var durusToplamGenislikPixel = 558;


            MakinaDurusHareketEdit.RowGuid = Guid.NewGuid();
            MakinaDurusHareketEdit.MakinaKisaAd = "DH1";
            MakinaDurusHareketEdit.Operatör = AppPandap.AktifKullanici.KullaniciId;

            var yeniDurusHareket = JsonConvert.DeserializeObject<MakinaAktiviteKayit>(JsonConvert.SerializeObject(MakinaDurusHareketEdit));

            yeniDurusHareket.ZamanKutuWidth = (int)((double)((double)MakinaDurusHareketEdit.SüreDkGiris / AralikDkToplam) * durusToplamGenislikPixel) - 30;


            RunSonrasiIslemKayitListe.Add(yeniDurusHareket);
            MakinaDurusHareketEdit = new MakinaAktiviteKayit();
            MakinaDurusHareketEdit.BaşlangıçSaati = yeniDurusHareket.BitişSaati;

            OnPropertyChanged(nameof(DurusKalanDk));



        }
    }
}
