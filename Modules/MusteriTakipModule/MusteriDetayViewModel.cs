using DevExpress.Mvvm;
using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic;
using mnd.Logic.BC_MusteriTakip.Data;
using mnd.Logic.BC_MusteriTakip.Domain;
using mnd.Logic.Model;
using mnd.Logic.Model._Ref;
using mnd.Logic.Model.App;
using mnd.Logic.Model.Netsis;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence.Repositories;
using mnd.Logic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;


namespace mnd.UI.Modules.MusteriTakipModule
{
    public class MusteriDetayViewModel : MyBindableBase
    {
        public PandapCariDto Musteri { get; set; }

        public MusteriEditModel EditMusteri { get => _editMusteri; set => SetProperty(ref _editMusteri, value); }

        public DelegateCommand KisiEkleFormAcCommand => new DelegateCommand(OnKisiEkleFormAc);

        public DelegateCommand<CariEmailYeni> KisiDuzenleFormAcCommand => new DelegateCommand<CariEmailYeni>(OnKisiDuzenleFormAc);

        public DelegateCommand MailAcCommand => new DelegateCommand(OnMailAc);
        public DelegateCommand WebSiteAcCommand => new DelegateCommand(OnWebSiteAc);

        private void OnWebSiteAc()
        {
            var web = Musteri.Web;
            System.Diagnostics.Process.Start(web);
        }

        private void OnMailAc()
        {
            var mail = Musteri.Email;

            var mailtoUri = $"/c ipm.note /m {mail}";

              ProcessStartInfo startInfo = new ProcessStartInfo("OUTLOOK.exe");

            startInfo.Arguments = mailtoUri;
            Process.Start(startInfo);


        }

        public DelegateCommand GorusmeEkleFormAcCommand => new DelegateCommand(OnGorusmeEkleFormAc, () => true);

        public DelegateCommand DosyaEkleCommand => new DelegateCommand(DosyaEkle);

        public List<string> Sektorler { get; set; }
        public List<string> MusteriDurumlar { get; }

        private void OnGorusmeEkleFormAc()
        {
            GorusmeViewModel vm = new GorusmeViewModel(0);
            vm.KayitMod = KayitModu.Add;
            var yeniGorusme = new Gorusme();
            yeniGorusme.RowGuid = Guid.NewGuid();

            yeniGorusme.GorusmeTarih = DateTime.Now;
            yeniGorusme.RandevuTarih = DateTime.Now;
            yeniGorusme.ModifiedDate = DateTime.Now;


            yeniGorusme.MusteriCariKod = Musteri.CariKod;

            vm.Gorusme = yeniGorusme;


            var doc = AppPandap.DokumanOlustur("GorusmeView", vm, "Yeni Kayıt");
            doc.Show();
        }

        public string MusteriKod { get; set; }

        public bool IsEditMode { get => _isEditMode; set => SetProperty(ref _isEditMode, value); }

        PandapCariRepository repoPandapCari;
        MusteriTakipRepository repoMusteriTakip;
        private bool _isEditMode;
        private MusteriEditModel _editMusteri;
        private bool kontratDonemiCegrekMi;
        private bool kontratDonemiYillikMi;

        public DelegateCommand<Gorusme> DuzenleCommand => new DelegateCommand<Gorusme>(OnDuzenle);
        public DelegateCommand MusteriDataDuzenleCommand => new DelegateCommand(OnMusteriDataDuzenle);
        public DelegateCommand MusteriDataGuncelleCommand => new DelegateCommand(OnMusteriDataGuncelle);


        public bool KontratDonemiCegrekMi { get => kontratDonemiCegrekMi; set => SetProperty(ref kontratDonemiCegrekMi, value); }

        public bool KontratDonemiYillikMi { get => kontratDonemiYillikMi; set => SetProperty(ref kontratDonemiYillikMi, value); }


        public ObservableCollection<KullanimAlanTip> KullanimAlanlari { get; set; }
        public List<Kullanici> Plasiyerler { get; }
        public List<Kullanici> SahaSorumlulari { get; }
        public List<Kullanici> Agentlar { get; }


        public void DosyaEkle()
        {
            CariDokuman dokuman = new CariDokuman();
            dokuman.RowGuid = Guid.NewGuid();
            dokuman.Ekleyen = AppPandap.AktifKullanici.AdSoyad;
            dokuman.Tarih = DateTime.Now;

            var dlg = new Microsoft.Win32.OpenFileDialog();

            var result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                byte[] by = File.ReadAllBytes(filename);

                var fileExtension = Path.GetExtension(filename);


                dokuman.DokumanIcerik = fileExtension + ";" + Convert.ToBase64String(by);
                dokuman.DosyaAd = dlg.SafeFileName;

                Musteri.CariDokumanlar.Add(dokuman);
            }
        }

        private void OnMusteriDataDuzenle()
        {
            EditMusteri = new MusteriEditModel();

            EditMusteri.Tel = Musteri.Tel;
            EditMusteri.Adres = Musteri.Adres;
            EditMusteri.Web = Musteri.Web;
            EditMusteri.Email = Musteri.Email;
            EditMusteri.CariSevkAdres = Musteri.SevkAdres;

            EditMusteri.YillikTonaj = Musteri.YillikTonaj;
            EditMusteri.KullanimAlanTipKod = KullanimAlanlari.FirstOrDefault(c => c.Aciklama_EN == Musteri.KullanimAlanAd)?.KullanimAlanKod;
            EditMusteri.PlasiyerKod = Musteri.PlasiyerKod;



            EditMusteri.AgentId = Musteri.AgentId;
            EditMusteri.SahaSorumlusuId = Musteri.PandaSahaSorumlusuId;
            EditMusteri.KontratDonemTip = Musteri.KontratDonemTip;
            EditMusteri.KontratDonemDeger = Musteri.KontratDonemDeger;

            EditMusteri.Durumu = Musteri.Durumu;

            EditMusteri.PropertyChanged += EditMusteri_PropertyChanged;

            EditMusteri.FirmaGrupTip = Musteri.FirmaGrupTip;

            IsEditMode = true;

            KontratDonemleriAlt.Clear();


            if (EditMusteri.KontratDonemTip == "Yıllık")
            {
                var aylar = new List<string> { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

                foreach (var item in aylar)
                {
                    KontratDonemleriAlt.Add(item);
                }

                KontratDonemiYillikMi = true;

            }

            if (EditMusteri.KontratDonemTip == "Çeyrek")
            {
                var aylar = new List<string> { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

                foreach (var item in aylar)
                {
                    KontratDonemleriAlt.Add(item);
                }

                var l = EditMusteri.KontratDonemDeger.Split(';');


                EditMusteri.KontratDonemDeger_Cegrek1 = l.ElementAtOrDefault(0);
                EditMusteri.KontratDonemDeger_Cegrek2 = l.ElementAtOrDefault(1);
                EditMusteri.KontratDonemDeger_Cegrek3 = l.ElementAtOrDefault(2);
                EditMusteri.KontratDonemDeger_Cegrek4 = l.ElementAtOrDefault(3);


                KontratDonemiCegrekMi = true;
            }

        }

        private void EditMusteri_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {


            if (e.PropertyName == "KontratDonemTip")
            {

                KontratDonemiCegrekMi = false;
                KontratDonemiYillikMi = false;

                KontratDonemleriAlt.Clear();

                if (EditMusteri.KontratDonemTip == "Yıllık")
                {
                    var aylar = new List<string> { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

                    foreach (var item in aylar)
                    {
                        KontratDonemleriAlt.Add(item);
                    }

                    KontratDonemiYillikMi = true;

                    EditMusteri.KontratDonemDeger = "";
                }

                if (EditMusteri.KontratDonemTip == "Çeyrek")
                {
                    KontratDonemiCegrekMi = false;
                    KontratDonemiYillikMi = false;

                    var aylar = new List<string> { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

                    foreach (var item in aylar)
                    {
                        KontratDonemleriAlt.Add(item);
                    }

                    KontratDonemiCegrekMi = true;

                    EditMusteri.KontratDonemDeger = EditMusteri.KontratDonemDeger ?? "";

                    var l = EditMusteri.KontratDonemDeger.Split(';');

                    EditMusteri.KontratDonemDeger_Cegrek1 = l.ElementAtOrDefault(0);
                    EditMusteri.KontratDonemDeger_Cegrek2 = l.ElementAtOrDefault(1) ?? "";
                    EditMusteri.KontratDonemDeger_Cegrek3 = l.ElementAtOrDefault(2) ?? "";
                    EditMusteri.KontratDonemDeger_Cegrek4 = l.ElementAtOrDefault(3) ?? "";

                }

                if (EditMusteri.KontratDonemTip == "Spot")
                {
                    EditMusteri.KontratDonemDeger = "";
                }


            }
        }

        private void OnKisiEkleFormAc()
        {
            var kisi = new CariEmailYeni();
            kisi.CariKod = Musteri.CariKod;

            FirmaYetkiliKisiView view = new FirmaYetkiliKisiView();

            FirmaYetkiliKisiViewModel vm = new FirmaYetkiliKisiViewModel(kisi);

            view.DataContext = vm;


            view.ShowDialog();

            if (vm.IsOk)
            {
                MusteriTakipService.CariEmailKaydetYadaEkle(kisi);

                var ft = new CariEmailYeni
                {
                    Id = kisi.Id,
                    YetkiliKisi = kisi.YetkiliKisi.FromNetsisCollation(),
                    Email = kisi.Email,
                    Tel = kisi.Tel,
                    Unvan = kisi.Unvan,
                    Birim = kisi.Birim,
                    Durum = kisi.Durum
                };

                Musteri.CariEmailListe.Add(ft);
            }


        }


        private void OnKisiDuzenleFormAc(CariEmailYeni kisi)
        {
            FirmaYetkiliKisiView view = new FirmaYetkiliKisiView();

            FirmaYetkiliKisiViewModel vm = new FirmaYetkiliKisiViewModel(kisi);

            view.DataContext = vm;


            view.ShowDialog();

            if (vm.IsOk)
            {
                MusteriTakipService.CariEmailKaydetYadaEkle(kisi);
            }
        }

        public List<String> KontratDonemleri { get; set; }

        public ObservableCollection<String> KontratDonemleriAlt { get; set; }

        private void OnMusteriDataGuncelle()
        {
            Musteri.Tel = EditMusteri.Tel;
            Musteri.Adres = EditMusteri.Adres;
            Musteri.YillikTonaj = EditMusteri.YillikTonaj;
            Musteri.KullanimAlanAd = KullanimAlanlari.FirstOrDefault(c => c.KullanimAlanKod == EditMusteri.KullanimAlanTipKod)?.Aciklama_EN;
            Musteri.MusteriSorumlusu = Plasiyerler.FirstOrDefault(c => c.PlasiyerKod == EditMusteri.PlasiyerKod)?.AdSoyad;
            Musteri.PlasiyerKod = EditMusteri.PlasiyerKod;

            Musteri.AgentId = EditMusteri.AgentId;
            Musteri.Agent = Agentlar.FirstOrDefault(c => c.KullaniciId == EditMusteri.AgentId)?.AdSoyad;

            Musteri.PandaSahaSorumlusuId = EditMusteri.SahaSorumlusuId;
            Musteri.PandaSahaSorumlusu = SahaSorumlulari.FirstOrDefault(c => c.KullaniciId == EditMusteri.SahaSorumlusuId)?.AdSoyad;

            Musteri.KontratDonemTip = EditMusteri.KontratDonemTip;
            Musteri.KontratDonemDeger = EditMusteri.KontratDonemDeger;
            Musteri.FirmaGrupTip = EditMusteri.FirmaGrupTip;

            Musteri.Durumu = EditMusteri.Durumu;
            Musteri.Web = EditMusteri.Web;
            Musteri.Email = EditMusteri.Email;
            Musteri.SevkAdres = EditMusteri.CariSevkAdres;


            MusteriTakipService servis = new MusteriTakipService();


            var kontratDonemDeger = Musteri.KontratDonemDeger;

            if (Musteri.KontratDonemTip == "Çeyrek")
            {
                kontratDonemDeger = EditMusteri.KontratDonemDeger_Cegrek_Tum;
            }


            //try
            //{
                Musteri.KontratDonemDeger = kontratDonemDeger;
                servis.PandaCariGuncelle(Musteri);

                int sonuc = servis.NetsisMusteriBilgileriGuncelle
                (
                    Musteri.CariKod,
                    EditMusteri.Tel,
                    EditMusteri.Adres,
                    EditMusteri.KullanimAlanTipKod,
                    EditMusteri.YillikTonaj,
                    EditMusteri.PlasiyerKod,
                    EditMusteri.AgentId,
                    EditMusteri.FirmaGrupTip,
                    EditMusteri.Durumu

                 );

                IsEditMode = false;
            //}
            //catch (Exception ex)
            //{
            //    var hata = "Hata:" + ex.Message + Environment.NewLine + "Detay:" + ex.InnerException?.Message;
            //    MessageBox.Show(hata);
            //}



        }



        private void OnDuzenle(Gorusme gorusme)
        {
            GorusmeViewModel vm = new GorusmeViewModel(gorusme.Id);
            var doc = AppPandap.DokumanOlustur("GorusmeView", vm, gorusme.GorusulenKisi);
            doc.Show();
        }


        public List<YaslandirilmisFatura> YaslandirilmisFaturalar { get; set; }

        public void FormLoaded()
        {

        }

        private void OnGorusmeKayitMesaj(KayitMesaj obj)
        {
            Musteri.Gorusmeler.Add((Gorusme)obj.Kayit);
        }


        public MusteriDetayViewModel()
        {
            IsEditMode = false;
        }

        public MusteriDetayViewModel(string cariKod)
        {
            MusteriKod = cariKod;

            repoPandapCari = new PandapCariRepository();
            repoMusteriTakip = new MusteriTakipRepository();

            Musteri = repoPandapCari.PandapCariDetayGetir(MusteriKod);


            LookUpRepository rep = new LookUpRepository();

            if (Musteri.KullanimAlanTipKod != "")
            {
                var kullanimTip = rep.KullanimAlanlarini(Musteri.KullanimAlanTipKod);

                Musteri.KullanimAlanAd = kullanimTip.Aciklama_EN;
            }

            Musteri.DovizTipSimge = DovizHelper.SimgeyeDonustur(Musteri.DovizTipAd);

            YaslandirilmisFaturalar = repoMusteriTakip.YaslandirilmisFaturalariGetir(Musteri.CariKod, DovizHelper.NetsisDovizKodGetir(Musteri.DovizTipAd));

            KullanimAlanlari = rep.KullanimAlanlariniGetir();

            Plasiyerler = KullaniciService.PlasiyerleriGetir();

            SahaSorumlulari = KullaniciService.SahaSorumlulariGetir();
            SahaSorumlulari.Insert(0, new Kullanici { KullaniciId = "", AdSoyad = "" });


            Agentlar = KullaniciService.AgentlariGetir();
            Agentlar.Insert(0, new Kullanici { KullaniciId = "", AdSoyad = "" });

            Musteri.Agent = Agentlar.FirstOrDefault(c => c.KullaniciId == Musteri.AgentId)?.AdSoyad;
            Musteri.PandaSahaSorumlusu = SahaSorumlulari.FirstOrDefault(c => c.KullaniciId == Musteri.PandaSahaSorumlusuId)?.AdSoyad;

            Messenger.Default.Register<KayitMesaj>(this, OnGorusmeKayitMesaj);


            KontratDonemleri = new List<string> { "Yıllık", "Çeyrek", "Spot" };

            KontratDonemleriAlt = new ObservableCollection<string>();

            Sektorler = new List<string> { "Industri", "Packagin" };

            MusteriDurumlar = new List<string> { "Potansiy", "Customer" };
        }

    }
}
