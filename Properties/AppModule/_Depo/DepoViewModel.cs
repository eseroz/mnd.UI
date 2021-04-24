using DevExpress.Mvvm;
using Pandap.Logic.Model;
using Pandap.Logic.Model._DTOs;
using Pandap.Logic.Model.Uretim;
using Pandap.Logic.Persistence;
using Pandap.UI.Helper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Pandap.UI.AppModule._Depo
{
    public class DepoInfo : MyBindableBase
    {
        private int _depoToplam;
        private int _musteriStokToplam;
        private decimal _ortalamaEn;
        private decimal _ortalamaKalinlik;
        private int _pandaStok;
        public int DepoToplam { get => _depoToplam; set => SetProperty(ref _depoToplam, value); }
        public int MusteriStokToplam { get => _musteriStokToplam; set => SetProperty(ref _musteriStokToplam, value); }
        public decimal OrtalamaEn { get => _ortalamaEn; set => SetProperty(ref _ortalamaEn, value); }
        public decimal OrtalamaKalinlik { get => _ortalamaKalinlik; set => SetProperty(ref _ortalamaKalinlik, value); }
        public int PandaStok { get => _pandaStok; set => SetProperty(ref _pandaStok, value); }


        int aktifAyToplam;
        public int AktifAyToplam
        {
            get => aktifAyToplam;
            set => SetProperty(ref aktifAyToplam, value);
        }
    }

    public class DepoViewModel : MyBindableBase
    {

        public ObservableCollection<MamulDepoStokDto> _sevkEdilenlerListe;

        public ObservableCollection<Palet> depodakiPaletler;

        public ObservableCollection<MamulDepoStokDto> mamulDepoStokDtoListe;

        private Palet _depoSeciliPalet;

        private MamulDepoStokDto seciliMamulDepoStok;

        private MamulDepoStokDto seciliSevkEdilen;

        private int seciliTabIndex;

        private UnitOfWork uow = new UnitOfWork();

        public DepoViewModel()
        {
            SeciliTabIndex = 0;

            DepodakiPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetir();

            DepoDurum = new DepoInfo();
        }

        public string SevkiyatBaslik=> DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("tr"))
            + " " + DateTime.Now.Year.ToString() + " Sevkiyat T.";


        public ObservableCollection<Palet> DepodakiPaletler
        {
            get => depodakiPaletler; set => SetProperty(ref depodakiPaletler, value);
        }

        public DepoInfo DepoDurum { get; set; }

        public DelegateCommand<object> DepoKarantinayaGonderCommand => new DelegateCommand<object>(OnDepoKarantinayaGonder, CanKarantinayaGonder);


        public DelegateCommand<object> DepoRedCommand => new DelegateCommand<object>(OnDepoRed, CanRed);

        public Palet DepoSeciliPalet
        {
            get { return _depoSeciliPalet; }
            set => SetProperty(ref _depoSeciliPalet, value);
        }

        public DelegateCommand<object> DepoyaKabulCommand => new DelegateCommand<object>(OnDepoyaKabul, CanDepoyaKabul);

        public bool FormLoaded { get; set; }

        public DelegateCommand InitCommand => new DelegateCommand(FormLoad, true);

        public ObservableCollection<MamulDepoStokDto> MamulDepoStoklar
        {
            get => mamulDepoStokDtoListe; set => SetProperty(ref mamulDepoStokDtoListe, value);
        }

        public MamulDepoStokDto SeciliMamulDepoStok
        {
            get => seciliMamulDepoStok;
            set
            {
                if (SetProperty(ref seciliMamulDepoStok, value))
                {
                    if (SeciliMamulDepoStok == null) return;

                    SeciliMamulDepoStok.PropertyChanged -= SeciliMamulDepoStok_PropertyChanged;
                    SeciliMamulDepoStok.PropertyChanged += SeciliMamulDepoStok_PropertyChanged;
                };
            }
        }

        public MamulDepoStokDto SeciliSevkEdilen
        {
            get => seciliSevkEdilen;


            set
            {
                if (SetProperty(ref seciliSevkEdilen, value))
                {
                    if (SeciliSevkEdilen == null) return;

                    SeciliSevkEdilen.PropertyChanged -= SeciliSevkEdilen_PropertyChanged;
                    SeciliSevkEdilen.PropertyChanged += SeciliSevkEdilen_PropertyChanged;
                };
            }
        }

        public int SeciliTabIndex
        {
            get
            {
                return seciliTabIndex;
            }
            set
            {
                if (SetProperty(ref seciliTabIndex, value))
                {
                    if (value == 0) DepodakiPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetir();

                    if (value == 1)
                    {
                        MamulDepoStoklar = uow.PlanlamaRepo.MamulDepoStoklariGetir();
                        DepoDurum.DepoToplam = MamulDepoStoklar.Sum(c => c.Agirlik_kg).Value;

                        DepoDurum.AktifAyToplam = uow.PlanlamaRepo.AktifAyToplamAgirlikGetir();

                        DepoDurum.PandaStok = MamulDepoStoklar.Where(c => c.CariKod == GlobalConst.PANDACARI)
                            .Sum(c => c.Agirlik_kg).Value;

                        DepoDurum.MusteriStokToplam = DepoDurum.DepoToplam - DepoDurum.PandaStok;

                        DepoDurum.OrtalamaEn = MamulDepoStoklar
                                            .Average(c => c.En_mm).Value;

                        DepoDurum.OrtalamaKalinlik = MamulDepoStoklar
                                          .Average(c => c.Kalinlik_micron).Value;
                    }

                    if (value == 2) SevkEdilenlerListe = uow.PlanlamaRepo.SevkEdilenPaletler();
                }
            }
        }

        public ObservableCollection<MamulDepoStokDto> SevkEdilenlerListe
        {
            get => _sevkEdilenlerListe; set => SetProperty(ref _sevkEdilenlerListe, value);
        }

        public DelegateCommand<object> SevkiyatGeriAlIslemCommand => new DelegateCommand<object>(OnSevkiyatGeriAlIslem, CanSevkiyatGeriAlIslem);

        public DelegateCommand<object> SevkiyatIslemCommand => new DelegateCommand<object>(OnSevkiyatIslem, CanSevkiyatIslem);


        public DelegateCommand<object> YenidenUretimeGonderCommand => new DelegateCommand<object>(OnYenidenUretimeGonder, true);

        private  void OnYenidenUretimeGonder(object obj)
        {
            if (SeciliMamulDepoStok == null) return;

            var secilenSayi = MamulDepoStoklar.Count(c => c.Sec == true);

            if (secilenSayi == 0)
            {
                MessageBox.Show("Üretime geri gidecek edilecek malzeme seçiniz");
            }

            MessageBoxResult cevap = MessageBox.Show("Yeniden üretim işlemini onaylıyormusunuz?", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (cevap == MessageBoxResult.Cancel) return;

            foreach (var item in mamulDepoStokDtoListe)
            {
                if (item.Sec == true)
                {
                    var _palet = uow.PlanlamaRepo.PaletGetir(item.PaletNo.Value);

                    _palet.SevkiyatTarihi = null;
                    _palet.DepoyaAktarilmaOnayTarihi = null;
                    
                }
            }

            uow.Commit();

            DepodakiPaletler = uow.PlanlamaRepo.Depo_OnayliPaletleriGetir();
            MamulDepoStoklar = uow.PlanlamaRepo.MamulDepoStoklariGetir();
            SevkEdilenlerListe = uow.PlanlamaRepo.SevkEdilenPaletler();
        }

        private bool CanDepoyaKabul(object arg)
        {
            return SeciliTabIndex == 0;
        }

        private bool CanKarantinayaGonder(object arg)
        {
            return SeciliTabIndex == 2;
        }

        private bool CanRed(object arg)
        {
            return SeciliTabIndex == 1;
        }

        private bool CanSevkiyatGeriAlIslem(object arg)
        {
            return true;
        }

        private bool CanSevkiyatIslem(object arg)
        {
            return true;
        }

        private void FormLoad()
        {
            FormLoaded = true;
        }

        private void IrsaliyeNoKaydet(object obj)
        {
            uow.PlanlamaRepo.PaletIrsaliyeNoKaydet(SeciliSevkEdilen.PaletNo, SeciliSevkEdilen.IrsaliyeNo);
        }

        private void OnDepoKarantinayaGonder(object obj)
        {
        }

        private void OnDepoRed(object obj)
        {
        }

        private  void OnDepoyaKabul(object obj)
        {
            DepoSeciliPalet.DepoyaAktarilmaOnayTarihi = DateTime.Now;
            uow.Commit();

            DepodakiPaletler = uow.PlanlamaRepo.DepoOnayiBekleyenPaletleriGetir();
        }

        private  void OnSevkiyatGeriAlIslem(object obj)
        {
            var secilenSayi = _sevkEdilenlerListe.Count(c => c.Sec == true);

            if (secilenSayi == 0)
            {
                MessageBox.Show("Sevk edilecek malzeme seçiniz");
                return;
            }

            MessageBoxResult cevap = MessageBox.Show("Sevkiyat Geri Alma işlemini onaylıyormusunuz?", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (cevap == MessageBoxResult.Cancel) return;

            foreach (var item in _sevkEdilenlerListe)
            {
                if (item.Sec == true)
                {
                    var _palet = uow.PlanlamaRepo.PaletGetir(item.PaletNo.Value);

                    _palet.SevkiyatTarihi = null;
                }
            }

            var x = uow.Commit();

            DepodakiPaletler = uow.PlanlamaRepo.Depo_OnayliPaletleriGetir();
            MamulDepoStoklar = uow.PlanlamaRepo.MamulDepoStoklariGetir();
            SevkEdilenlerListe = uow.PlanlamaRepo.SevkEdilenPaletler();
        }

        private  void OnSevkiyatIslem(object obj)
        {
            if (SeciliMamulDepoStok == null) return;

            var secilenSayi = MamulDepoStoklar.Count(c => c.Sec == true);

            if (secilenSayi == 0)
            {
                MessageBox.Show("Sevk edilecek malzeme seçiniz");
            }

            MessageBoxResult cevap = MessageBox.Show("Sevk işlemini onaylıyormusunuz?", "Pandap", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (cevap == MessageBoxResult.Cancel) return;

            foreach (var item in mamulDepoStokDtoListe)
            {
                if (item.Sec == true)
                {
                    var _palet = uow.PlanlamaRepo.PaletGetir(item.PaletNo.Value);

                    _palet.SevkiyatTarihi = DateTime.Now.Date;
                }
            }

            var x = uow.Commit();

            DepodakiPaletler = uow.PlanlamaRepo.Depo_OnayliPaletleriGetir();
            MamulDepoStoklar = uow.PlanlamaRepo.MamulDepoStoklariGetir();
            SevkEdilenlerListe = uow.PlanlamaRepo.SevkEdilenPaletler();
        }

        private void PaletAciklamaKaydet(object obj)
        {
            uow.PlanlamaRepo.PaletAciklamaKaydet(SeciliMamulDepoStok.PaletNo, SeciliMamulDepoStok.Aciklama);
        }
        private void SeciliMamulDepoStok_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SeciliMamulDepoStok.Aciklama))
            {
                PaletAciklamaKaydet(null);
            };
        }
        private void SeciliSevkEdilen_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SeciliSevkEdilen.IrsaliyeNo))
            {
                IrsaliyeNoKaydet(null);
            };
        }
    }
}