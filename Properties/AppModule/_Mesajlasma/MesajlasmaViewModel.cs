using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using Pandap.Logic.Model.App;
using Pandap.Logic.Persistence;

namespace Pandap.UI.AppModule._Mesajlasma
{
    public class MesajlasmaViewModel : MyMessangerBindableBase
    {
       

      
        private ObservableCollection<Mesaj> mesajlar;

        public ObservableCollection<Mesaj> Mesajlar
        {
            get => mesajlar;
            set => SetProperty(ref mesajlar, value);
        }

        public Guid AktifRowGuid { get; set; }

        private Mesaj _seciliMesaj;

        public Mesaj SeciliMesaj
        {
            get => _seciliMesaj;
            set => SetProperty(ref _seciliMesaj, value);
        }

        private Mesaj _aktifMesaj;

        public Mesaj AktifMesaj
        {
            get => _aktifMesaj;
            set => SetProperty(ref _aktifMesaj, value);
        }

        public DelegateCommand DosyaEkleCommand => new DelegateCommand(DosyaEkle);
        public DelegateCommand<Mesaj> DosyaAcCommand => new DelegateCommand<Mesaj>(DosyaAc);
        public DelegateCommand SendMessageCommand => new DelegateCommand(MesajEkle);

        public string GonderenAdSoyad { get; set; }
        public string AlanAdSoyad { get; set; }

        public MesajlasmaViewModel()
        {
            //parameterless gerekli olan yerler için
        }

        public MesajlasmaViewModel(Guid rowGuid, string gonderenAdSoyad)
        {
            AktifRowGuid = rowGuid;
            AktifMesaj = new Mesaj();
            Mesajlar = new ObservableCollection<Mesaj>();

            GonderenAdSoyad = gonderenAdSoyad;

            MesajlariYukle(AktifRowGuid, GonderenAdSoyad);


        }

        private  void MesajlariYukle(Guid aktifDosyaNo, string gonderenKullanici)
        {
            var  uow = new UnitOfWork();
            mesajlar =uow.MesajRepo.MesajlariGetirFromEntityRef(AktifRowGuid);


            foreach (var mesaj in mesajlar)
            {
                mesaj.GidenMi = mesaj.Gonderen == gonderenKullanici ? true : false;
                mesaj.DokumanIcerik = uow.MesajRepo.DokumanIcerikGetir(mesaj.Id);

                if(!String.IsNullOrEmpty(mesaj.Okuyanlar) && !mesaj.Okuyanlar.Contains(PandapGlobal.AktifKullanici.KullaniciId)){
                    mesaj.Okuyanlar += ";" + PandapGlobal.AktifKullanici.KullaniciId;
                }

            }

           uow.Commit();

            if (mesajlar.Count > 0) SeciliMesaj = mesajlar.Last();
            OnPropertyChanged(nameof(Mesajlar));

           
        }

        public void DosyaEkle()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();

            var result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                byte[] by = File.ReadAllBytes(filename);

                var fileExtension = Path.GetExtension(filename);

                AktifMesaj.DokumanIcerik = fileExtension + ";" + Convert.ToBase64String(by);
                AktifMesaj.DokumanAdi = dlg.SafeFileName;

                MesajEkle();
            }
        }

        private void DosyaAc(Mesaj mesaj)
        {
            var  uow = new UnitOfWork();
            mesaj.DokumanIcerik = uow.MesajRepo.DokumanIcerikGetir(mesaj.Id);

            if (mesaj.DokumanIcerik == null)
            {
                MessageBox.Show("Dosya içeriği eksik", "Pandap", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var inx = mesaj.DokumanIcerik.IndexOf(';');
            var fileExtension = mesaj.DokumanIcerik.Substring(0, inx);
            var fileData = mesaj.DokumanIcerik.Substring(inx + 1, mesaj.DokumanIcerik.Length - inx - 1);

            byte[] bytedizi = Convert.FromBase64String(fileData);

            var b = new BlobInfo
            {
                Buffer = bytedizi,
                FileName = Guid.NewGuid().ToString() + fileExtension
            };

            PandaUtil.BlobdanDosyaAç(b);
        }



        private async void MesajEkle()
        {
            var uow = new UnitOfWork();
            var dokumentIcerik = AktifMesaj.DokumanIcerik;

            var yeniMesaj = new Mesaj
            {
                Gonderen = GonderenAdSoyad,
                MesajIcerik = AktifMesaj.MesajIcerik,
                RefEntityGuid = AktifRowGuid,
                MesajTarihi = DateTime.Now,
                GidenMi = true,
                DokumanAdi = AktifMesaj.DokumanAdi,
                DokumanIcerik = AktifMesaj.DokumanIcerik,
                Okuyanlar=PandapGlobal.AktifKullanici.KullaniciId
            };

            Mesajlar.Add(yeniMesaj);
            uow.MesajRepo.MesajEkle(yeniMesaj);

           uow.Commit();

            AktifMesaj.DokumanIcerik = null;
            AktifMesaj.MesajIcerik = string.Empty;

            AktifMesaj.DokumanAdi = null;

        }
    }
}