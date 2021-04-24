using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using mnd.Logic.Model.App;
using mnd.Logic.Model.Operasyon;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mnd.UI.Modules.OperasyonModule
{

    public class LmeInfo : MyDxViewModelBase
    {
        private decimal _lmeBirimFiyat;
        private int _yil;
        private int _ay;
        private string _dovizCinsi;

        public string KalemKod { get; set; }
        public decimal LmeBirimFiyat { get => _lmeBirimFiyat; set => SetProperty(ref _lmeBirimFiyat, value); }
        public string LmeDurumKod { get; set; }
        public string LmeNot { get; set; }
        public string LfxKod { get; set; }

        public int Yil { get => _yil; set => SetProperty(ref _yil, value); }
        public int Ay { get => _ay; set => SetProperty(ref _ay, value); }

        public string DovizTip { get => _dovizCinsi; set => SetProperty(ref _dovizCinsi, value); }

    }


    public class IrsaliyeDetayViewModel : MyDxViewModelBase
    {



        public DelegateCommand<LmeInfo> LmeOrtalamaGetirCommand => new DelegateCommand<LmeInfo>(OnLmeOrtalamaGetir, true);

        public DelegateCommand FiyatlariYenidenHesaplaCommand => new DelegateCommand(OnYenidenFiyatHesapla, true);

        private void OnYenidenFiyatHesapla()
        {
            var uowKalem = new UnitOfWork();


            foreach (var irsPalet in SeciliCariIrsaliye.IrsaliyePaletler)
            {

                var fiyatKalem = uowKalem.SiparisKalemRepo.SiparisKalemiGetir(irsPalet.FiyatKalemKod);

                //irsPalet.LmeBF_Ton = fiyatKalem.LmeTutar.GetValueOrDefault();
                //irsPalet.IscilikBF_Ton = fiyatKalem.IscilikTutar.GetValueOrDefault();
                //irsPalet.KulceBF_Ton = fiyatKalem.KulceTutar.GetValueOrDefault();
                //irsPalet.IscilikVadeFarkiOran = fiyatKalem.IscilikVadeFarkiOran;

                irsPalet.KdvOran = (decimal)fiyatKalem.KdvOran.GetValueOrDefault();

                irsPalet.PaletGenelToplamGuncelle();
            }

            uowIrst.Commit();

            IrsaliyeVerileriniYukle(SeciliCariIrsaliye.IrsaliyeId);
        }

        private void OnLmeOrtalamaGetir(LmeInfo obj)
        {
            var lmeOrtalamalar = uowIrst.SiparisRepo.LmeOrtalamalariGetir();

            var lmeOrtalama = lmeOrtalamalar.Where(c => c.Yil == obj.Yil && c.Ay == obj.Ay).FirstOrDefault();

            var dovizTip = "Usd";
            var dataSutunAd = obj.LmeDurumKod + "_" + dovizTip;

            SeciliLmeInfo.LmeBirimFiyat = (decimal)lmeOrtalama.GetType().GetProperty(dataSutunAd).GetValue(this);

        }

        private LmeInfo _seciliKalemKod;


        public LmeInfo SeciliLmeInfo { get => _seciliKalemKod; set => SetProperty(ref _seciliKalemKod, value); }


        private void OnLmeUygula()
        {
            foreach (var lmeKalem in LmeBilgiler)
            {
                var lmeUygulanacakPaletler = SeciliCariIrsaliye.IrsaliyePaletler
                                      .Where(c => c.FiyatKalemKod == lmeKalem.KalemKod)
                                      .ToList();

                foreach (var irs_palet in lmeUygulanacakPaletler)
                {
                    irs_palet.LmeBF_Ton = lmeKalem.LmeBirimFiyat;
                    irs_palet.LfxKod = lmeKalem.LfxKod;
                    irs_palet.PaletGenelToplamGuncelle();
                }
            }

        }


        public Irsaliye SeciliCariIrsaliye { get; set; }

        UnitOfWork uowIrst = new UnitOfWork();

        public ObservableCollection<LmeInfo> LmeBilgiler { get; set; }


        public IrsaliyeDetayViewModel(int irsaliyeId)
        {
            IrsaliyeVerileriniYukle(irsaliyeId);
        }


        public void IrsaliyeVerileriniYukle(int irsaliyeId)
        {
            var uowLookUp = new UnitOfWork();

            SeciliCariIrsaliye = uowIrst.SevkiyatEmirRepo.CariIrsaliyeGetir(irsaliyeId);

            var IrsaliyeKalemKodlariGruplu = SeciliCariIrsaliye.IrsaliyePaletler.Select(c => new { c.FiyatKalemKod, c.LmeBF_Ton })
                                            .Distinct()
                                            .ToList();

            LmeBilgiler = new ObservableCollection<LmeInfo>();


            foreach (var grupKalem in IrsaliyeKalemKodlariGruplu)
            {
                var lmeInfo = new LmeInfo();

                var sipariskalem = uowIrst.SiparisKalemRepo.SiparisKalemiGetir(grupKalem.FiyatKalemKod);

                //if (grupKalem.LmeBF_Ton == 0)
                //    lmeInfo.LmeBirimFiyat = sipariskalem.LmeTutar.GetValueOrDefault();
                //else
                //    lmeInfo.LmeBirimFiyat = grupKalem.LmeBF_Ton;

                //lmeInfo.KalemKod = grupKalem.FiyatKalemKod;

                //lmeInfo.LmeDurumKod = sipariskalem.SiparisNav.LmeDurumKod;
                //lmeInfo.LmeNot = sipariskalem.SiparisNav.LmeBaglamaNot;
                //lmeInfo.LfxKod = sipariskalem.LmeBaglamaKod;
                //LmeBilgiler.Add(lmeInfo);
            }


            Bankalar = uowLookUp.BankaRepo.AktifBankalariGetir();


            var sipKodlari = SeciliCariIrsaliye.SiparisKodlariBirlesik.Split(';').OrderBy(c => c.ToString()).ToArray();
            var sonSipKod = sipKodlari[sipKodlari.Length - 1];



            var sip_irs = uowLookUp.SiparisRepo.SiparisGetirIrsaliyeIcin(sonSipKod);

           

            var firmaAdi = sip_irs.CariKartNavigation.CariIsim;

            if (SeciliCariIrsaliye.FaturaAdresi == null) SeciliCariIrsaliye.FaturaAdresi = firmaAdi + Environment.NewLine+ sip_irs.FaturaAdresi;

            var irsaliyeAdresi1 = sip_irs.IrsaliyeAdresi?.ToLower().Contains("same") == true ? SeciliCariIrsaliye.FaturaAdresi : sip_irs.IrsaliyeAdresi;

            if (SeciliCariIrsaliye.IrsaliyeAdresi1 == null) SeciliCariIrsaliye.IrsaliyeAdresi1 = irsaliyeAdresi1;
            if (SeciliCariIrsaliye.IrsaliyeAdresi2 == null) SeciliCariIrsaliye.IrsaliyeAdresi2 = sip_irs.IrsaliyeAdresi2;


            if (SeciliCariIrsaliye.OdemeSekli == null) SeciliCariIrsaliye.OdemeSekli = sip_irs?.OdemeTipKodNavigation?.Aciklama_En + " " + sip_irs.OdemeAciklama;
            if (SeciliCariIrsaliye.TeslimSekli == null) SeciliCariIrsaliye.TeslimSekli = sip_irs.TeslimTipKodNavigation.Aciklama;

            if (SeciliCariIrsaliye.OdemeBankaKod == null)
            {
                Irsaliye irsaliyeSon = uowLookUp.SevkiyatEmirRepo.BirOncekiCariIrsaliyeGetir(SeciliCariIrsaliye.CariKod, SeciliCariIrsaliye.IrsaliyeId);
                if (irsaliyeSon != null) SeciliCariIrsaliye.OdemeBankaKod = irsaliyeSon.OdemeBankaKod;
            }


            if (SeciliCariIrsaliye.FirmaIlgiliKisiAdSoyad == null) SeciliCariIrsaliye.FirmaIlgiliKisiAdSoyad = sip_irs?.CariEmailNavigation?.YetkiliKisi;
            if (SeciliCariIrsaliye.FirmaIlgiliKisiEposta == null) SeciliCariIrsaliye.FirmaIlgiliKisiEposta = sip_irs?.CariEmailNavigation?.Email;

            //TODO refactor yap
            if (SeciliCariIrsaliye.FirmaIlgiliKisiTel == null) SeciliCariIrsaliye.FirmaIlgiliKisiTel = sip_irs?.CariKartNavigation.CariTel;


            SeciliCariIrsaliye.FaturaDovizTip = sip_irs.TakipDovizTipKod;
            SeciliCariIrsaliye.FaturaDovizTipSimge = sip_irs.TakipDovizTipKodNavigation.Simge;
            SeciliCariIrsaliye.FaturaDovizTipNetsisKod = sip_irs.TakipDovizTipKodNavigation.NetsisId;
        }


        public DelegateCommand<CancelEventArgs> KaydetKapatCommand => new DelegateCommand<CancelEventArgs>(KaydetKapat);
        public DelegateCommand<CancelEventArgs> IptalCommand => new DelegateCommand<CancelEventArgs>(Iptal);

        public MessageBoxResult Result { get; set; }

        public ObservableCollection<Banka> Bankalar { get; set; }

        private void KaydetKapat(CancelEventArgs obj)
        {



            Result = MessageBoxResult.OK;

            var seciliBanka = Bankalar.Where(c => c.BankaKod == SeciliCariIrsaliye.OdemeBankaKod).FirstOrDefault();

            SeciliCariIrsaliye.OdemeBankaAd = seciliBanka.BankaAd;
            SeciliCariIrsaliye.OdemeBankaSubeAd = seciliBanka.Sube;
            SeciliCariIrsaliye.OdemeBankaSubeKod = seciliBanka.SubeKodu;
            SeciliCariIrsaliye.OdemeBankaIBAN = seciliBanka.Iban;
            SeciliCariIrsaliye.OdemeBankaSwiftCode = seciliBanka.SwiftKod;
            SeciliCariIrsaliye.OdemeBankaHakSahip = seciliBanka.HesapSahibi;
            SeciliCariIrsaliye.OdemeBankaDovizTip = seciliBanka.ParaCinsi;

            OnLmeUygula();

            uowIrst.Commit();

            var currentWindow = ((CurrentWindowService)ServiceContainer.GetService<ICurrentWindowService>()).Window;
            ServiceContainer.GetService<ICurrentWindowService>().Close();
        }



        private void Iptal(CancelEventArgs obj)
        {
            Result = MessageBoxResult.Cancel;

            var currentWindow = ((CurrentWindowService)ServiceContainer.GetService<ICurrentWindowService>()).Window;
            ServiceContainer.GetService<ICurrentWindowService>().Close();
        }


    }
}
