using AutoMapper;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic.BC_MusteriTakip.Data;
using mnd.Logic.BC_MusteriTakip.Domain;
using mnd.Logic.BC_Satis._Seyahat;
using mnd.Logic.BC_Satis._Teklif;
using mnd.Logic.BC_Satis.Data_LookUp.Model;
using mnd.Logic.Model.App;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;
using mnd.Logic.Persistence.Repositories;
using mnd.Logic.Services;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;
using mnd.UI.Modules._DialogViews.MusteriSecimDialog;
using mnd.UI.Modules._SatisModule;
using mnd.UI.Modules.TeklifModule.MessangerEvents;
using mnd.UI.Modules.TeklifModule.Models;
using mnd.UI.Modules.TeklifModule.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

namespace mnd.UI.Modules.TeklifModule
{
    public class TeklifViewModel : MyDxViewModelBase, IDocumentViewModel
    {
        #region Delegates
        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);
        public DelegateCommand<object> SilCommand => new DelegateCommand<object>(OnSil);

        private void OnSil(object obj)
        {
           
        }

        public DelegateCommand<object> YeniKalemCommand => new DelegateCommand<object>(OnYeniKalem, c => true);

        public DelegateCommand<TeklifKalemEditModel> DuzenleCommand => new DelegateCommand<TeklifKalemEditModel>(OnDuzenle, c => true);

        public DelegateCommand<object> YeniTeklifCommand => new DelegateCommand<object>(OnYeniTeklif, c => true);

        public DelegateCommand<object> MusteriSecCommand => new DelegateCommand<object>(OnMusteriSec, c => true);

        public DelegateCommand ProformaViewCommand => new DelegateCommand(OnProformaViewCommand);

        private void OnProformaViewCommand()
        {
            PandapCari cariInfo = null;

            bool potansiyelMusteriMi = SeciliTeklif.PotansiyelCariAd != null && SeciliTeklif.PotansiyelCariAd.Length > 1;

            LookUpData look = new LookUpData();

            if (potansiyelMusteriMi)
            {
                cariInfo = new PandapCari { UlkeKod = "US", CariIsim = SeciliTeklif.PotansiyelCariAd };
            }
            else
            {
                cariInfo = PandapCariService.CariGetir(SeciliTeklif.CariKod);
            }

            var lang = cariInfo.UlkeKod.ToUpper() == "TR" ? "TR" : "EN";

            try
            {
                var dovizTipSimge = DovizHelper.SimgeyeDonustur(SeciliTeklif.CariDovizTipKod);

                TeklifCiktiFormDto dto = new TeklifCiktiFormDto();


                dto.FirmaSorumluAd = SeciliTeklif.IletisimKisiAdSoyad;
                dto.FirmaIlgiliKisiTel = SeciliTeklif.IletisimKisiTel;
                dto.FirmaIlgiliKisiMail = SeciliTeklif.IletisimKisiMail;

                dto.Incoterms = SeciliTeklif.TeslimTipKod + " " + SeciliTeklif.TeslimYeri;

                dto.CariAd = potansiyelMusteriMi ? SeciliTeklif.PotansiyelCariAd : SeciliTeklif.CariAd;

                var odemeSekli = LookupTables.Default.OdemeTipleri.First(c => c.OdemeTipKod == SeciliTeklif.OdemeSekliKod);

                var odemeSekli_lang = lang == "EN" ? odemeSekli.Aciklama_EN : odemeSekli.Aciklama;

                //if (lang == "EN")
                //    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay + " / " + odemeSekli_lang;
                //else
                //    dto.PaymentTerms = odemeSekli_lang + " / " + SeciliTeklif.OdemeSekliDetay;

                if (lang == "EN")
                    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay;
                else
                    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay;

                string teslim_not = "";

                if (SeciliTeklif.TeslimNot != null)
                {
                    teslim_not = "/" + SeciliTeklif.TeslimNot;
                }
                dto.Delivery = SeciliTeklif.TeslimTipKod + "/" + teslim_not;

                dto.TeklifTarih = SeciliTeklif.TeklifTarih;
                dto.TeklifSonTarih = SeciliTeklif.SonGecerlilikTarihi;

                dto.OfferRef = SeciliTeklif.PlasiyerTeklifSiraKod;
                dto.ValidTill = "End of " + SeciliTeklif.SonGecerlilikTarihi.ToShortDateString();

                dto.SatisTemsilcisiAdSoyad = SeciliTeklif.SatisTemsilcisiAdSoyad;
                dto.SatisTemsilcisiMail = SeciliTeklif.SatisTemsilcisiMail;
                dto.TasimaSekliAdi = SeciliTeklif.TasimaSekliAdi_EN;
                dto.GidecegiUlke = SeciliTeklif.GidecegiUlke;
                dto.CariAdres = cariInfo.CariAdres;
                dto.CariTelefon = cariInfo.CariTel;
                dto.CariFax = cariInfo.Fax;
                dto.ProformaNo = SeciliTeklif.TeklifSiraKod;
                dto.TeklifGenelNot = SeciliTeklif.TeklifGenelNot;

                var banka = LookupTables.Default.BankaHesaplari.Where(p => p.BankaKod == SeciliTeklif.BankaHesapKod).FirstOrDefault();

                decimal Quantity = 0;
                decimal TotalPrice = 0;

                dto.BankaAdi = banka.BankaAd;
                dto.BankaSube = banka.Sube;
                dto.BankaHesapListeAd = banka.BankaHesapListeAd;
                dto.FooterCompanyName = "MND KAHVALTILIK GIDA SAN. ve TIC. A.Ş.";
                dto.FooterAddress = "İSTANBUL YOLU 33. KM. BİTİK KÖYÜ GİRİŞİ, 06980, KAHRAMANKAZAN-ANKARA/TÜRKİYE";
                dto.FooterBank = banka.BankaAd;
                dto.FooterAccountNr = banka.SubeKodu + " " + banka.Hesap;
                dto.FooterIbanNr = banka.Iban;
                dto.FooterSwiftCode = banka.SwiftKod;

                dto.MndFaturaBaslik = "MND KAHVALTILIK GIDA SAN.VE TİC.A.Ş.";
                dto.MndAdsress = "Istanbul Yolu 33.Km.Bitik Köyü Girisi 06980 Kahramankazan-ANKARA/TURKEY ";
                dto.MndTelefon = "Tel: +90(312) 814 5590 (pbx)";
                dto.MndFax = "Fax: +90(312) 818 6214";


                dto.HesapNo = banka.Hesap;
                dto.DovizTipi = banka.ParaCinsi;

                foreach (var teklifKalemItem in SeciliTeklif.TeklifKalemlerDTO)
                {
                    TeklifCiktiKalemDto tk = new TeklifCiktiKalemDto
                    {
                        BOX = teklifKalemItem.BOX,
                        Butce = teklifKalemItem.Butce,
                        CRTN = teklifKalemItem.CRTN,
                        GR = teklifKalemItem.GR,
                        GROSS = teklifKalemItem.GROSS,
                        H = teklifKalemItem.H,
                        L = teklifKalemItem.L,
                        M3 = teklifKalemItem.M3,
                        Miktar = teklifKalemItem.Miktar,
                        NETKG = teklifKalemItem.NETKG,
                        PCS = teklifKalemItem.PCS,
                        SatisFiyati = (decimal)teklifKalemItem.SatisFiyati,
                        TeklifKalemNot = teklifKalemItem.TeklifKalemNot,
                        TeklifKalemSiraKod = teklifKalemItem.TeklifKalemSiraKod,
                        TeklifSiraKod = teklifKalemItem.TeklifSiraKod,
                        TeslimTarihi = teklifKalemItem.TeslimTarihi,
                        Tutar = teklifKalemItem.Tutar,
                        UrunAdiEN = teklifKalemItem.UrunAdiEN,
                        UrunAdiTR = teklifKalemItem.UrunAdiTR,
                        UrunKod = teklifKalemItem.UrunKod,
                        W = teklifKalemItem.W
                    };

                    Quantity = Quantity + tk.Miktar;
                    TotalPrice = TotalPrice + tk.Tutar;

                    dto.TeklifCiktiKalemDtoList.Add(tk);
                }

                var uow = new UnitOfWork();

                dto.Quantity = Quantity.ToString();
                dto.TeklifGenelToplamStr = "$" + TotalPrice.ToString();

                var raporTanim = lang == "TR" ? uow.RaporTanimRepo.RaporGetirFromId(31) : uow.RaporTanimRepo.RaporGetirFromId(32);

                var dsObject = dto;

                PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        public DelegateCommand TeklifFormuViewCommand => new DelegateCommand(OnTeklifFormuView);

        private void OnTeklifFormuView()
        {
            PandapCari cariInfo = null;

            bool potansiyelMusteriMi = SeciliTeklif.PotansiyelCariAd != null && SeciliTeklif.PotansiyelCariAd.Length > 1;

            LookUpData look = new LookUpData();

            if (potansiyelMusteriMi)
            {
                cariInfo = new PandapCari { UlkeKod = "US", CariIsim = SeciliTeklif.PotansiyelCariAd };
            }
            else
            {
                cariInfo = PandapCariService.CariGetir(SeciliTeklif.CariKod);
            }

            var lang = cariInfo.UlkeKod.ToUpper() == "TR" ? "TR" : "EN";

            try
            {
                var dovizTipSimge = DovizHelper.SimgeyeDonustur(SeciliTeklif.CariDovizTipKod);

                TeklifCiktiFormDto dto = new TeklifCiktiFormDto();


                dto.FirmaSorumluAd = SeciliTeklif.IletisimKisiAdSoyad;
                dto.FirmaIlgiliKisiTel = SeciliTeklif.IletisimKisiTel;
                dto.FirmaIlgiliKisiMail = SeciliTeklif.IletisimKisiMail;

                dto.Incoterms = SeciliTeklif.TeslimTipKod + " " + SeciliTeklif.TeslimYeri;

                dto.CariAd = potansiyelMusteriMi ? SeciliTeklif.PotansiyelCariAd : SeciliTeklif.CariAd;

                var odemeSekli = LookupTables.Default.OdemeTipleri.First(c => c.OdemeTipKod == SeciliTeklif.OdemeSekliKod);

                var odemeSekli_lang = lang == "EN" ? odemeSekli.Aciklama_EN : odemeSekli.Aciklama;

                //if (lang == "EN")
                //    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay + " / " + odemeSekli_lang;
                //else
                //    dto.PaymentTerms = odemeSekli_lang + " / " + SeciliTeklif.OdemeSekliDetay;

                if (lang == "EN")
                    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay;
                else
                    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay;

                string teslim_not = "";

                if (SeciliTeklif.TeslimNot != null)
                {
                    teslim_not = "/" + SeciliTeklif.TeslimNot;
                }
                dto.Delivery = SeciliTeklif.TeslimTipKod + "/" + teslim_not;

                dto.TeklifTarih = SeciliTeklif.TeklifTarih;
                dto.TeklifSonTarih = SeciliTeklif.SonGecerlilikTarihi;

                dto.OfferRef = SeciliTeklif.PlasiyerTeklifSiraKod;
                dto.ValidTill = "End of " + SeciliTeklif.SonGecerlilikTarihi.ToShortDateString();

                dto.SatisTemsilcisiAdSoyad = SeciliTeklif.SatisTemsilcisiAdSoyad;
                dto.SatisTemsilcisiMail = SeciliTeklif.SatisTemsilcisiMail;
                dto.TasimaSekliAdi = SeciliTeklif.TasimaSekliAdi_EN;
                dto.GidecegiUlke = SeciliTeklif.GidecegiUlke;
                dto.CariAdres = cariInfo.CariAdres;
                dto.CariTelefon = cariInfo.CariTel;
                dto.CariFax = cariInfo.Fax;
                dto.ProformaNo = SeciliTeklif.TeklifSiraKod;
                dto.TeklifGenelNot = SeciliTeklif.TeklifGenelNot;

                dto.MndFaturaBaslik = "MND KAHVALTILIK GIDA SAN.VE TİC.A.Ş.";
                dto.MndAdsress = "Istanbul Yolu 33.Km.Bitik Köyü Girisi 06980 Kahramankazan-ANKARA/TURKEY ";
                dto.MndTelefon = "Tel: +90(312) 814 5590 (pbx)";
                dto.MndFax = "Fax: +90(312) 818 6214";

                var banka = LookupTables.Default.BankaHesaplari.Where(p => p.BankaKod == SeciliTeklif.BankaHesapKod).FirstOrDefault();

                decimal Quantity = 0;
                decimal TotalPrice = 0;

                dto.BankaAdi = banka.BankaAd;
                dto.BankaSube = banka.Sube;
                dto.BankaHesapListeAd = banka.BankaHesapListeAd;
                dto.FooterCompanyName = "MND KAHVALTILIK GIDA SAN. ve TIC. A.Ş.";
                dto.FooterAddress = "İSTANBUL YOLU 33. KM. BİTİK KÖYÜ GİRİŞİ, 06980, Kahramankazan-ANKARA/TÜRKİYE";
                dto.FooterBank = banka.BankaAd;
                dto.FooterAccountNr = banka.SubeKodu + " " + banka.Hesap;
                dto.FooterIbanNr = banka.Iban;
                dto.FooterSwiftCode = banka.SwiftKod;



                dto.HesapNo = banka.Hesap;
                dto.DovizTipi = banka.ParaCinsi;

                foreach (var teklifKalemItem in SeciliTeklif.TeklifKalemlerDTO)
                {
                    TeklifCiktiKalemDto tk = new TeklifCiktiKalemDto
                    {
                        BOX = teklifKalemItem.BOX,
                        Butce = teklifKalemItem.Butce,
                        CRTN = teklifKalemItem.CRTN,
                        GR = teklifKalemItem.GR,
                        GROSS = teklifKalemItem.GROSS,
                        H = teklifKalemItem.H,
                        L = teklifKalemItem.L,
                        M3 = teklifKalemItem.M3,
                        Miktar = teklifKalemItem.Miktar,
                        NETKG = teklifKalemItem.NETKG,
                        PCS = teklifKalemItem.PCS,
                        SatisFiyati = (decimal)teklifKalemItem.SatisFiyati,
                        TeklifKalemNot = teklifKalemItem.TeklifKalemNot,
                        TeklifKalemSiraKod = teklifKalemItem.TeklifKalemSiraKod,
                        TeklifSiraKod = teklifKalemItem.TeklifSiraKod,
                        TeslimTarihi = teklifKalemItem.TeslimTarihi,
                        Tutar = teklifKalemItem.Tutar,
                        UrunAdiEN = teklifKalemItem.UrunAdiEN,
                        UrunAdiTR = teklifKalemItem.UrunAdiTR,
                        UrunKod = teklifKalemItem.UrunKod,
                        W = teklifKalemItem.W
                    };

                    Quantity = Quantity + tk.Miktar;
                    TotalPrice = TotalPrice + tk.Tutar;

                    dto.TeklifCiktiKalemDtoList.Add(tk);
                }

                var uow = new UnitOfWork();

                dto.Quantity = Quantity.ToString();
                dto.TeklifGenelToplamStr = "$" + TotalPrice.ToString();

                var raporTanim = lang == "TR" ? uow.RaporTanimRepo.RaporGetirFromId(71) : uow.RaporTanimRepo.RaporGetirFromId(3);

                var dsObject = dto;

                PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public DelegateCommand PackingListCommand => new DelegateCommand(OnPackingListViewCommand);

        private void OnPackingListViewCommand()
        {
            PandapCari cariInfo = null;

            bool potansiyelMusteriMi = SeciliTeklif.PotansiyelCariAd != null && SeciliTeklif.PotansiyelCariAd.Length > 1;

            LookUpData look = new LookUpData();

            if (potansiyelMusteriMi)
            {
                cariInfo = new PandapCari { UlkeKod = "US", CariIsim = SeciliTeklif.PotansiyelCariAd };
            }
            else
            {
                cariInfo = PandapCariService.CariGetir(SeciliTeklif.CariKod);
            }

            var lang = cariInfo.UlkeKod.ToUpper() == "TR" ? "TR" : "EN";

            try
            {
                var dovizTipSimge = DovizHelper.SimgeyeDonustur(SeciliTeklif.CariDovizTipKod);

                TeklifCiktiFormDto dto = new TeklifCiktiFormDto();


                dto.FirmaSorumluAd = SeciliTeklif.IletisimKisiAdSoyad;
                dto.FirmaIlgiliKisiTel = SeciliTeklif.IletisimKisiTel;
                dto.FirmaIlgiliKisiMail = SeciliTeklif.IletisimKisiMail;

                dto.Incoterms = SeciliTeklif.TeslimTipKod + " " + SeciliTeklif.TeslimYeri;

                dto.CariAd = potansiyelMusteriMi ? SeciliTeklif.PotansiyelCariAd : SeciliTeklif.CariAd;

                var odemeSekli = LookupTables.Default.OdemeTipleri.First(c => c.OdemeTipKod == SeciliTeklif.OdemeSekliKod);

                var odemeSekli_lang = lang == "EN" ? odemeSekli.Aciklama_EN : odemeSekli.Aciklama;

                //if (lang == "EN")
                //    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay + " / " + odemeSekli_lang;
                //else
                //    dto.PaymentTerms = odemeSekli_lang + " / " + SeciliTeklif.OdemeSekliDetay;

                if (lang == "EN")
                    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay;
                else
                    dto.PaymentTerms = SeciliTeklif.OdemeSekliDetay;

                string teslim_not = "";

                if (SeciliTeklif.TeslimNot != null)
                {
                    teslim_not = "/" + SeciliTeklif.TeslimNot;
                }
                dto.Delivery = SeciliTeklif.TeslimTipKod + "/" + teslim_not;

                dto.TeklifTarih = SeciliTeklif.TeklifTarih;
                dto.TeklifSonTarih = SeciliTeklif.SonGecerlilikTarihi;

                dto.OfferRef = SeciliTeklif.PlasiyerTeklifSiraKod;
                dto.ValidTill = "End of " + SeciliTeklif.SonGecerlilikTarihi.ToShortDateString();

                dto.SatisTemsilcisiAdSoyad = SeciliTeklif.SatisTemsilcisiAdSoyad;
                dto.SatisTemsilcisiMail = SeciliTeklif.SatisTemsilcisiMail;
                dto.TasimaSekliAdi = SeciliTeklif.TasimaSekliAdi_EN;
                dto.GidecegiUlke = SeciliTeklif.GidecegiUlke;
                dto.CariAdres = cariInfo.CariAdres;
                dto.CariTelefon = cariInfo.CariTel;
                dto.CariFax = cariInfo.Fax;
                dto.ProformaNo = SeciliTeklif.TeklifSiraKod;
                dto.TeklifGenelNot = SeciliTeklif.TeklifGenelNot;

                dto.MndFaturaBaslik = "MND KAHVALTILIK GIDA SAN.VE TİC.A.Ş.";
                dto.MndAdsress = "Istanbul Yolu 33.Km.Bitik Köyü Girisi 06980 Kahramankazan-ANKARA/TURKEY ";
                dto.MndTelefon = "Tel: +90(312) 814 5590 (pbx)";
                dto.MndFax = "Fax: +90(312) 818 6214";

                var banka = LookupTables.Default.BankaHesaplari.Where(p => p.BankaKod == SeciliTeklif.BankaHesapKod).FirstOrDefault();

                decimal Quantity = 0;
                decimal TotalPrice = 0;

                dto.BankaAdi = banka.BankaAd;
                dto.BankaSube = banka.Sube;
                dto.BankaHesapListeAd = banka.BankaHesapListeAd;
                dto.FooterCompanyName = "MND KAHVALTILIK GIDA SAN. ve TIC. A.Ş.";
                dto.FooterAddress = "İSTANBUL YOLU 33. KM. BİTİK KÖYÜ GİRİŞİ, 06980, Kahramankazan-ANKARA/TÜRKİYE";
                dto.FooterBank = banka.BankaAd;
                dto.FooterAccountNr = banka.SubeKodu + " " + banka.Hesap;
                dto.FooterIbanNr = banka.Iban;
                dto.FooterSwiftCode = banka.SwiftKod;



                dto.HesapNo = banka.Hesap;
                dto.DovizTipi = banka.ParaCinsi;

                foreach (var teklifKalemItem in SeciliTeklif.TeklifKalemlerDTO)
                {
                    TeklifCiktiKalemDto tk = new TeklifCiktiKalemDto
                    {
                        BOX = teklifKalemItem.BOX,
                        Butce = teklifKalemItem.Butce,
                        CRTN = teklifKalemItem.CRTN,
                        GR = teklifKalemItem.GR,
                        GROSS = teklifKalemItem.GROSS,
                        H = teklifKalemItem.H,
                        L = teklifKalemItem.L,
                        M3 = teklifKalemItem.M3,
                        Miktar = teklifKalemItem.Miktar,
                        NETKG = teklifKalemItem.NETKG,
                        PCS = teklifKalemItem.PCS,
                        SatisFiyati = (decimal)teklifKalemItem.SatisFiyati,
                        TeklifKalemNot = teklifKalemItem.TeklifKalemNot,
                        TeklifKalemSiraKod = teklifKalemItem.TeklifKalemSiraKod,
                        TeklifSiraKod = teklifKalemItem.TeklifSiraKod,
                        TeslimTarihi = teklifKalemItem.TeslimTarihi,
                        Tutar = teklifKalemItem.Tutar,
                        UrunAdiEN = teklifKalemItem.UrunAdiEN,
                        UrunAdiTR = teklifKalemItem.UrunAdiTR,
                        UrunKod = teklifKalemItem.UrunKod,
                        W = teklifKalemItem.W,
                        
                      
                    };

                    Quantity = Quantity + tk.Miktar;
                    TotalPrice = TotalPrice + tk.Tutar;

                    dto.TeklifCiktiKalemDtoList.Add(tk);
                }

                var uow = new UnitOfWork();

                dto.Quantity = Quantity.ToString();
                dto.TeklifGenelToplamStr = "$" + TotalPrice.ToString();

                var raporTanim = lang == "TR" ? uow.RaporTanimRepo.RaporGetirFromId(71) : uow.RaporTanimRepo.RaporGetirFromId(3);

                var dsObject = dto;

                PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded);

        public DelegateCommand KisiEkleFormAcCommand => new DelegateCommand(OnKisiEkleFormAc, () => true);

        public FirmaTemsilci TempCariGorusmeKisi { get => _tempHavuzKisi; set => SetProperty(ref _tempHavuzKisi, value); }
        public DelegateCommand<FirmaTemsilci> KisiEkleCancelCommand => new DelegateCommand<FirmaTemsilci>(OnKisiEkleCancel, true);
        public DelegateCommand<FirmaTemsilci> KisiEkleOkCommand => new DelegateCommand<FirmaTemsilci>(OnKisiEkleOk, true);
        #endregion
        public bool IsOpenKisiEkleForm { get => _isOpenKisiEkleForm; set => SetProperty(ref _isOpenKisiEkleForm, value); }

        public List<Unvan> Unvanlar { get => _unvanlar; set => SetProperty(ref _unvanlar, value); }

        public DelegateCommand<string> SipariseDonusturCommand => new DelegateCommand<string>(SipariseDonustur);


        private void SipariseDonustur(string param)
        {         
            var sonSiparisNo = service.SiparisSonKayitNoGetir();

            var siparisNo = "P";
            var yil = DateTime.Now.Year;
            if (sonSiparisNo == null)            
                siparisNo = "P-" + yil + "-1000";
            else
                siparisNo = sonSiparisNo;            

            siparisNo = (int.Parse(siparisNo.Split('-')[1]) + 1).ToString();
            siparisNo = "P" + yil + siparisNo;

            var siparis = Siparis.SiparisOlustur(SeciliTeklif.TeklifSiraKod, AppPandap.AktifKullanici.KullaniciId);

            siparis.RowGuid = new Guid();
            siparis.SiparisSurecDurum = SIPARISSURECDURUM.SATISTA;
            siparis.CariKod = SeciliTeklif.CariKod;
            siparis.OdemeTipKod = SeciliTeklif.OdemeSekliKod;
            siparis.OdemeAciklama = SeciliTeklif.OdemeSekliDetay;
            siparis.OdemeBankaKod = SeciliTeklif.BankaHesapKod;
            siparis.TeslimTipKod = SeciliTeklif.TeslimTipKod;
            siparis.TeslimSehir = SeciliTeklif.TeslimYeri;
            siparis.FaturaDovizTipKod = SeciliTeklif.CariDovizTipKod;

            var vm = new SiparisViewModel();
            vm.SiparisKayitModu = KayitModu.Add;

            vm.Load(siparis);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SiparisView", vm);
            doc.Title = siparisNo;
            doc.Show();
        }

        private void OnKisiEkleFormAc()
        {
            Thread.Sleep(100);
            TempCariGorusmeKisi = new FirmaTemsilci();
            IsOpenKisiEkleForm = true;
        }
        private void OnKisiEkleOk(FirmaTemsilci obj)
        {
            try
            {
                var kisi = new FirmaTemsilci();
                kisi.Email = TempCariGorusmeKisi.Email;
                kisi.Tel = TempCariGorusmeKisi.Tel;
                kisi.AdSoyad = TempCariGorusmeKisi.AdSoyad;
                kisi.Unvan = TempCariGorusmeKisi.Unvan;
                kisi.CariKod = SeciliTeklif.CariKod;

                MusteriTakipService.KaydetYadaEkle(kisi);

                CariGorusmeKisiListe.Add(kisi);

                SeciliCariEmail = kisi;

                IsOpenKisiEkleForm = false;
            }
            catch (Exception ex)
            {
                var hata = "Hata:" + ex.Message + Environment.NewLine + "Detay:" + ex.InnerException?.Message;
                MessageBox.Show(hata);
            }


        }

        private void OnKisiEkleCancel(FirmaTemsilci obj)
        {
            TempCariGorusmeKisi = null;
            IsOpenKisiEkleForm = false;
        }
        public string SeciliTeklif_Orj_Json { get; set; }

        private void OnFormLoaded()
        {
            SeciliTeklif_Orj_Json = JsonConvert.SerializeObject(SeciliTeklif);
        }

        public ObservableCollection<FirmaTemsilci> CariGorusmeKisiListe
        {
            get => _cariGorusmeKisiListe;
            set => SetProperty(ref _cariGorusmeKisiListe, value);
        }

        public bool FormDegistiMi {
            get
            {
                return SeciliTeklif_Orj_Json != JsonConvert.SerializeObject(SeciliTeklif);
            }
          
       }
  
        public FirmaTemsilci SeciliCariEmail
        {
            get => _seciliCariEmail;
            set
            {
                if (SetProperty(ref _seciliCariEmail, value))
                {
                    if (_seciliCariEmail != null)
                    {
                        SeciliTeklif.IletisimKisiAdSoyad = _seciliCariEmail.AdSoyad;
                        SeciliTeklif.IletisimKisiMail = _seciliCariEmail.Email;
                        SeciliTeklif.IletisimKisiUnvan = _seciliCariEmail.Unvan;
                        SeciliTeklif.IletisimKisiTel = _seciliCariEmail.Tel;
                    }
                    else
                    {
                        SeciliTeklif.IletisimKisiAdSoyad ="";
                        SeciliTeklif.IletisimKisiMail = "";
                        SeciliTeklif.IletisimKisiUnvan = "";
                        SeciliTeklif.IletisimKisiTel = "";
                    }
                }



            }
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

            this.SeciliTeklif.CariKod = obj?.Musteri.CariKod;
            this.SeciliTeklif.CariAd = obj?.Musteri.CariAd;
            this.SeciliTeklif.CariDovizTipKod = obj?.Musteri.DovizTipKod;

         
            CariGorusmeKisiListe = MusteriTakipService.FirmaTemsilcileriniGetir(SeciliTeklif.CariKod);
            if (CariGorusmeKisiListe.Count == 0) CariGorusmeKisiListe.Add(new FirmaTemsilci { AdSoyad = "" });

           

        }

        TeklifDataService service = new TeklifDataService();
        private void OnYeniTeklif(object obj)
        {
            var vm = new TeklifViewModel();

            vm.Load("Yeni");


            var doc = AppPandap.pDocumentManagerService.CreateDocument("TeklifView", vm);
            doc.Title = "Yeni";
            doc.Show();

        
        }

        public TeklifViewModel()
        {
            Messenger.Default.Register<KalemEklendiEvent>(this, KalemEklendi);
            Messenger.Default.Register<KalemGuncellendiEvent>(this, KalemGuncellendi);

            OdemeTipleri = LookupTables.Default.OdemeTipleri;
             TeslimTipleri = LookupTables.Default.TeslimTipleri;
            DovizTipleri = LookupTables.Default.DovizTipleri;
            OlcuBirimleri = LookupTables.Default.BirimTipleri;
            TasimaSekilleri = LookupTables.Default.TasimaSekilleri;
            BankaHesaplari = LookupTables.Default.BankaHesaplari;


        }

        private void KalemGuncellendi(KalemGuncellendiEvent obj)
        {
            Mapper.Map(obj.TeklifKalemEditModel, SeciliTeklifKalemEditModel);
        }

        private void KalemEklendi(KalemEklendiEvent obj)
        {
            string yeniTeklifKalemKod = "";
            var sonKalem = SeciliTeklif.TeklifKalemlerDTO.LastOrDefault();
            int sonsayi = 0;

            if (sonKalem == null)
            {
                sonsayi = 0;
            }
            else
            {
                sonsayi = int.Parse(sonKalem.TeklifKalemSiraKod.Split('/')[3]);                
            }

            yeniTeklifKalemKod = SeciliTeklif.TeklifSiraKod + "/" + (sonsayi + 1);

            obj.TeklifKalemEditModel.TeklifKalemSiraKod = yeniTeklifKalemKod;

            SeciliTeklif.TeklifKalemlerDTO.Add(obj.TeklifKalemEditModel);
        }

        public ObservableCollection<Banka> BankaHesaplari { get; set; }
        public List<OdemeTip> OdemeTipleri { get; set; }
        public List<LmeTip> LmeBelirlemeTipleri { get; }
        public List<TeslimTip> TeslimTipleri { get; }

        public List<DovizTip> DovizTipleri { get; }
        public List<BirimTip> OlcuBirimleri { get; }
    
        public List<GumrukTip> Proforma_HSCODE_Liste { get; }
        public List<TasimaSekli> TasimaSekilleri { get; }

        private void OnDuzenle(TeklifKalemEditModel obj)
        {
            var duzenlecekKalemKopya = new TeklifKalemEditModel();

            Mapper.Map<TeklifKalemEditModel,TeklifKalemEditModel>(obj, duzenlecekKalemKopya);

            TeklifKalemViewModel vm = new TeklifKalemViewModel(duzenlecekKalemKopya,SeciliTeklif.CariDovizTipKod);

            TeklifKalemView window = new TeklifKalemView();

            window.DataContext = vm;
            window.ShowDialog();
        }

        private void OnYeniKalem(object obj)
        {
           
            if (SeciliTeklif.TeklifSiraKod == "Yeni")
            {
                MessageBox.Show("Önce teklifi kaydediniz");
                return;
            }

            var teklifKalemEditModel = new TeklifKalemEditModel();

            TeklifKalemViewModel vm = new TeklifKalemViewModel(teklifKalemEditModel, SeciliTeklif.CariDovizTipKod);
            vm.SeciliTeklifKalem = teklifKalemEditModel;
            vm.SeciliTeklifKalem.TeklifSiraKod = SeciliTeklif.TeklifSiraKod;
            vm.SeciliTeklifKalem.TeklifKalemSiraKod = "Yeni";
            vm.SeciliTeklifKalem.TeslimYil = DateTime.Now.Year.ToString();
            TeklifKalemView window = new TeklifKalemView();
            window.DataContext = vm;
            window.ShowDialog();
        }

        private void OnKaydet(object obj)
        {

            if (SeciliTeklif.TeklifSiraKod == "Yeni")
            {
                UlkeRepository ulkeRepo = new UlkeRepository(new Logic.BC_App.Data.AppDataContext());
                string ulkeKodu = ulkeRepo.UlkeKoduVer(SeciliTeklif.GidecegiUlke);

                var yeniNo = "";
                int sayi = 0;
                var sonKayit = service.SonKayitNoGetir();

                if (sonKayit == null)
                {
                    sayi = 100;

                } else {
                    sayi = (int.Parse(sonKayit.Split('/')[2]) + 1);
                }


                yeniNo = DateTime.Now.Year.ToString() + "/" + ulkeKodu + "/" + sayi;

                SeciliTeklif.ProformaNo = yeniNo;
                SeciliTeklif.TeklifSiraKod = yeniNo;
                SeciliTeklif.TeklifDurum = "Bekliyor";
                SeciliTeklif.SatisTemsilcisiAdSoyad = AppPandap.AktifKullanici.AdSoyad;
                SeciliTeklif.SatisTemsilcisiMail = AppPandap.AktifKullanici.Email;

                SeciliTeklif.CreateUserId = AppPandap.AktifKullanici.KullaniciId;
                SeciliTeklif.CreateTime = DateTime.Now;

                SeciliTeklif.RowGuid = Guid.NewGuid();

                Messenger.Default.Send<TeklifEklendiEvent>(new TeklifEklendiEvent(SeciliTeklif));

            }

            var tasimasekli = TasimaSekilleri.Where(p => p.TasimaSekliAdi_EN == SeciliTeklif.TasimaSekliAdi_EN).FirstOrDefault();

            SeciliTeklif.TasimaSekli = tasimasekli.TasimaSekliAdi;
            SeciliTeklif.TasimaSekliAdi_EN = tasimasekli.TasimaSekliAdi_EN;

            SeciliTeklif.UpdateUserId= AppPandap.AktifKullanici.KullaniciId;
            SeciliTeklif.UpdateTime = DateTime.Now;

            service.Kaydet(SeciliTeklif);

            SeciliTeklif_Orj_Json = JsonConvert.SerializeObject(SeciliTeklif);

        }

        public TeklifEditModel SeciliTeklif { get; set; }

        public TeklifKalemEditModel SeciliTeklifKalemEditModel { get => seciliTeklifKalemEditModel; set => seciliTeklifKalemEditModel = value; }

        public object Title => "throw new NotImplementedException()";

        TeklifRepository teklifRepo = new TeklifRepository();
        private TeklifKalemEditModel seciliTeklifKalemEditModel;
        private ObservableCollection<FirmaTemsilci> _cariGorusmeKisiListe;
        private FirmaTemsilci _seciliCariEmail;
        private FirmaTemsilci _tempHavuzKisi;
        private bool _isOpenKisiEkleForm;
        private List<Unvan> _unvanlar;


        public List<UlkeSabit> Ulkeler { get; set; }
        public void Load(string teklifKod)
        {
            if (teklifKod == "Yeni")
            {
                SeciliTeklif = new TeklifEditModel();
                SeciliTeklif.TeklifSiraKod = "Yeni";
                SeciliTeklif.TeklifTarih = DateTime.Now.Date;
                SeciliTeklif.SonGecerlilikTarihi = DateTime.Now.Date;

                var gunlukKayitSayisi = service.PlasiyerBazliKayitSayisi(AppPandap.AktifKullanici.PlasiyerKod);

                var kisaAd = AppPandap.AktifKullanici.SatisTeklifUserKod;

                if(kisaAd==null)
                {
                    MessageBox.Show("Teklif oluşturmak için kullanıcıya teklif kısa kod tanımlayın ve formu kapatıp tekrar açın.");
                    return;
                }

                var plasiyerKisa = kisaAd + (gunlukKayitSayisi + 1).ToString().PadLeft(2,'0');

                SeciliTeklif.PlasiyerTeklifSiraKod = DateTime.Now.Date.ToString("yyyyMMdd") + "/" +  plasiyerKisa;


            }
            else
            {
                SeciliTeklif = service.TeklifGetir(teklifKod);
            }

            CariGorusmeKisiListe = MusteriTakipService.FirmaTemsilcileriniGetir(SeciliTeklif.CariKod);
            if (CariGorusmeKisiListe.Count == 0) CariGorusmeKisiListe.Add(new FirmaTemsilci { AdSoyad = "" });

            MusteriTakipRepository repo = new MusteriTakipRepository();
            Unvanlar = repo.UnvanlariGetir();

              Ulkeler = new List<UlkeSabit>();

            UnitOfWork uw = new UnitOfWork();
            Ulkeler = uw.UlkeRepo.UlkeleriGetir();

    }

        public bool Close()
        {
            Debug.Print((GC.GetTotalMemory(true) / 1024).ToString());

            if(FormDegistiMi)
            {
                MessageResult cev= MessageBoxService.ShowMessage("Değişiklikleri kaydetmek istiyormusunuz?","Kayıt",MessageButton.YesNoCancel);

                if(cev== MessageResult.Yes)
                {
                    OnKaydet(null);
                    return true;
                }

                if (cev == MessageResult.No) return true;

                if (cev == MessageResult.Cancel) return false;

            }

            return true;
        }
    }
}