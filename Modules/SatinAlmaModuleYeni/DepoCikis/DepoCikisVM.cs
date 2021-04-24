using DevExpress.Mvvm;
using mnd.Logic.BC_App;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis
{
    public class DepoCikisVM : MyDxViewModelBase, IForm
    {
        private string _aramaMetin;

        StokTanimNetsisRepository repoStokTanim = new StokTanimNetsisRepository();
        DepoRepository repoDepoCikis = new DepoRepository();

        private vwStokTanim bulunanStok;

        public bool BarkodNoGir { get => barkodGir; set => SetProperty(ref barkodGir, value); }

        public bool MiktarGir { get => miktarGir; set => SetProperty(ref miktarGir, value); }

        private DepoCikisFisDTO depoFisi;
        private DepoCikisFisKalemDTO fisKalemEdit;
        private bool barkodGir;
        private bool miktarGir;
        private bool ısYeni;
        private bool yeniAktifMi;
        private bool kaydetAktifMi;

        public ObservableCollection<IsMerkezi> MasrafMerkezleri { get; set; }
        public vwStokTanim BulunanStok { get => bulunanStok; set => SetProperty(ref bulunanStok, value); }

        public DepoCikisFisDTO DepoCikisFisi { get => depoFisi; set => SetProperty(ref depoFisi, value); }

        public DepoCikisFisKalemDTO FisKalemEdit { get => fisKalemEdit; set => SetProperty(ref fisKalemEdit, value); }


        public DelegateCommand YeniDepoCikisFisiCommand => new DelegateCommand(OnYeniDepoCikisFisi, CanYeniFis);
        public DelegateCommand DepoCikisFisiKaydetCommand => new DelegateCommand(OnDepoCikisFisiKaydet, CanKaydet);
        public DelegateCommand DepoCikisFisiYazdirCommand => new DelegateCommand(OnDepoCikisFisiYazdir, CanYazdir);

        IsMerkeziRepository repo = new IsMerkeziRepository();

        private bool CanYazdir()
        {
            return DepoCikisFisi?.FisNo != "Yeni";
        }

        public DelegateCommand FisKalemEkleCommand => new DelegateCommand(OnFisKalemEkle, CanKaydet);
        public DelegateCommand<DepoCikisFisKalemDTO> KalemSilCommand => new DelegateCommand<DepoCikisFisKalemDTO>(OnKalemSil, CanKaydet);

        private bool CanKaydet(DepoCikisFisKalemDTO arg)
        {
            return CanKaydet();
        }

        public bool IsYeni { get => ısYeni; set => SetProperty(ref ısYeni, value); }
        private bool CanYeniFis()
        {
            return KayitModu == "Edit" || KayitModu == "Kaydedildi";
        }


        private bool CanKaydet()
        {
            return KayitModu == "Yeni";
        }


        public string AramaMetin
        {
            get
            {
                return _aramaMetin;
            }
            set
            {
                if (SetProperty(ref _aramaMetin, value))
                {
                    AraMetinFunction(_aramaMetin);
                }

            }
        }

        public string KayitModu { get; set; }

        public List<ItemInfoText> UniteListe { get; set; }

        public DepoCikisVM(string id, string mode)
        {
            KayitModu = mode;

            UniteListe = new List<ItemInfoText>();
            UniteListe.Add(new ItemInfoText { Text = "Bakım", Value = "Bakım" });
            UniteListe.Add(new ItemInfoText { Text = "İşletme", Value = "İşletme" });
            UniteListe.Add(new ItemInfoText { Text = "Kalite", Value = "Kalite" });
            UniteListe.Add(new ItemInfoText { Text = "İSG", Value = "İSG" });
            UniteListe.Add(new ItemInfoText { Text = "Ortak", Value = "Ortak" });


            var repoDc = new DepoRepository();



            MasrafMerkezleri = repo.TeknikDepoIsMerkezleriGetir();

            MasrafMerkezleri.Insert(0, new IsMerkezi { Kod = 0, Tanim = "Seçiniz" });


            if (KayitModu == "Edit") DepoCikisFisi = repoDc.FisGetir(id);

            if (KayitModu == "Yeni")
            {
                OnYeniDepoCikisFisi();
            }


        }

        private void OnYeniDepoCikisFisi()
        {
            KayitModu = "Yeni";

            repoDepoCikis = new DepoRepository();

            DepoCikisFisi = new DepoCikisFisDTO();
            DepoCikisFisi.FisNo = "Yeni";
            DepoCikisFisi.FisTarihi = DateTime.Now;

            FisKalemEdit = new DepoCikisFisKalemDTO();

            BarkodNoGir = false;
        }



        public DepoCikisVM(string FormAdi)
        {


        }


        private void OnKalemSil(DepoCikisFisKalemDTO obj)
        {
            DepoCikisFisi.KalemlerDTO.Remove(obj);
        }




        private void OnDepoCikisFisiKaydet()
        {

            var hataMesaj = "";

            if (String.IsNullOrEmpty(DepoCikisFisi.IlgiliUniteVarsayilan)) hataMesaj = "Çıkış Ünitesi girilmeli";

            if (String.IsNullOrEmpty(DepoCikisFisi.TeslimAlanKisi)) hataMesaj = "Teslim alan kişi girilmeli";

            if (DepoCikisFisi.TeslimAlanKisi?.Length > 15) hataMesaj = "Teslim alan kişi ismi 15 harfi geçemez";

            if (String.IsNullOrEmpty(DepoCikisFisi.TalepEdenKisi)) hataMesaj += Environment.NewLine + "Talep Edilen kişi girilmelidir";

            if (DepoCikisFisi.MasrafMerkeziKod == 0) hataMesaj += Environment.NewLine + "Masraf merkezini seçiniz";

            if (DepoCikisFisi.MasrafMerkeziKod != null && DepoCikisFisi.MasrafMerkeziKod == 0) hataMesaj += Environment.NewLine + "Masraf merkezini seçiniz";

            if (DepoCikisFisi.KalemlerDTO.Count == 0) hataMesaj += Environment.NewLine + "Stok eklemeden kayıt yapamazsınız";


            if (hataMesaj.Length > 0)
            {
                MessageBox.Show(hataMesaj, "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            try
            {
                if (DepoCikisFisi.FisNo == "Yeni") DepoCikisFisi.FisNo = repoDepoCikis.SonKayitNoGetir();
                repoDepoCikis.NetsisDepoCikisFisiEkle(DepoCikisFisi);

                repoDepoCikis.Kaydet();

                MessageBox.Show("Kayıt Yapıldı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);

                KayitModu = "Kaydedildi";

                Messenger.Default.Send<KayitIslemEvent<DepoCikisFisDTO>>(new KayitIslemEvent<DepoCikisFisDTO>(DepoCikisFisi));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException.Message);
            }



        }



        private void OnDepoCikisFisiYazdir()
        {
            var uow = new UnitOfWork();
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(45);

            DepoCikisFisi.MasrafMerkeziAd = MasrafMerkezleri.First(c => c.Kod == DepoCikisFisi.MasrafMerkeziKod).Tanim;

            var dsObject = DepoCikisFisi;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);

        }

        private void OnFisKalemEkle()
        {
            if (AramaMetin == null) return;

            var hataMesaj = "";

            if ( string.IsNullOrEmpty(DepoCikisFisi.IlgiliUniteVarsayilan)) hataMesaj = "Çıkış ünitesini seçiniz";
          

            if (String.IsNullOrEmpty(FisKalemEdit?.StokKodu)) hataMesaj = "Sok Kodu bulunamadı";
            if (FisKalemEdit?.CikisMiktar == 0) hataMesaj += Environment.NewLine + "Çıkan Miktarı Giriniz";

            var dahaOncedenEkliMi = DepoCikisFisi.KalemlerDTO.Any(c => c.StokKodu == FisKalemEdit.StokKodu);

            if (dahaOncedenEkliMi) hataMesaj += "Bu stok daha önceden eklenmiş.";

            var kalanMiktar = BulunanStok?.BAKIYE - FisKalemEdit?.CikisMiktar;
            if (kalanMiktar < 0)
            {
                hataMesaj += $"Bakiye 0 dan küçük olamaz. Güncel Bakiye : {BulunanStok.BAKIYE} \r\n Stok ile ilgili veri bilgilerini güncelletip sonra girişini yapınız.";
            }

            if (hataMesaj.Length > 0)
            {
                MessageBox.Show(hataMesaj, "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var yeniFisKalem = new DepoCikisFisKalemDTO();

            yeniFisKalem.StokKodu = FisKalemEdit.StokKodu;
            yeniFisKalem.CikisMiktar = FisKalemEdit.CikisMiktar;
            yeniFisKalem.StokAd = BulunanStok.STOKADI_TR;
            yeniFisKalem.OlcuBirimAd = BulunanStok.OLCU_BR1;
            yeniFisKalem.MasrafMerkeziKod = DepoCikisFisi.MasrafMerkeziKod;
            yeniFisKalem.IlgiliUnite = DepoCikisFisi.IlgiliUniteVarsayilan;



            DepoCikisFisi.KalemlerDTO.Add(yeniFisKalem);

            FisKalemEdit = new DepoCikisFisKalemDTO();

            BulunanStok = null;

            BarkodNoGir = true;
            MiktarGir = false;
        }


        public void Load()
        {

        }


        public async void AraMetinFunction(string metin)
        {
            await BulEkleAsync(metin);
        }

        private async Task BulEkleAsync(string aramaMetin)
        {
            if (aramaMetin == null) return;
            if (aramaMetin.Length == 0) return;

            int barkodNo;

            var uygun = int.TryParse(aramaMetin, out barkodNo);

            if (uygun == false)
            {
                AramaMetin = "";
                return;
            }


            BulunanStok = repoStokTanim.StokBul(aramaMetin);


            if (BulunanStok == null) return;

            FisKalemEdit = new DepoCikisFisKalemDTO();
            FisKalemEdit.StokKodu = aramaMetin;

            MiktarGir = true;
            BarkodNoGir = false;

            AramaMetin = "";
        }





    }
}
