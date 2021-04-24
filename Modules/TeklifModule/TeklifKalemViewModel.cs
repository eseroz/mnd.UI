using DevExpress.Mvvm;
using mnd.Common;
using mnd.Logic.BC_Satis.Data_LookUp.Model;
using mnd.Logic.Helper;
using mnd.Logic.Model;
using mnd.Logic.Model._Ref;
using mnd.Logic.Model.Stok;
using mnd.UI.Helper;
using mnd.UI.Modules.TeklifModule.MessangerEvents;
using mnd.UI.Modules.TeklifModule.Models;
using mnd.UI.Modules.TeklifModule.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules.TeklifModule
{
    public class TeklifKalemViewModel : MyDxViewModelBase
    {

        private TBLIHRSTK seciliUrun;
        private TeklifKalemEditModel seciliTeklifKalem;
        private List<NakliyeDurumTip> nakliyeDurumTipleri;
        private List<DonemGrup> donemGrupListesi;
        private List<Donem> donemListesi;
        public TeklifKalemEditModel SeciliTeklifKalem {
            get { return seciliTeklifKalem; }
            set {
                SetProperty(ref seciliTeklifKalem, value);
            } 
        }

        public List<DonemGrup> DonemGrupListesi { get => donemGrupListesi; set => SetProperty(ref donemGrupListesi, value); }
        public List<Donem> DonemListesi { get => donemListesi; set => SetProperty(ref donemListesi, value); }

        public TBLIHRSTK SeciliUrun
        {
            get => seciliUrun; set
            {
                SetProperty(ref seciliUrun, value);

                SeciliTeklifKalem.SatisFiyati = SeciliUrun.BirimFiyat;
                SeciliTeklifKalem.UrunAdiEN = SeciliUrun.UrunAdiEN;
                SeciliTeklifKalem.UrunAdiTR = SeciliUrun.UrunAdiTR;
                SeciliTeklifKalem.L = SeciliUrun.L;
                SeciliTeklifKalem.W = SeciliUrun.W;
                SeciliTeklifKalem.H = SeciliUrun.H;
                SeciliTeklifKalem.GROSS = SeciliUrun.GROSS;
                SeciliTeklifKalem.M3 = SeciliUrun.M3;
                SeciliTeklifKalem.NETKG = SeciliUrun.NETKG;
                SeciliTeklifKalem.GR = SeciliUrun.GR;
                SeciliTeklifKalem.PCS = SeciliUrun.PCS;
                SeciliTeklifKalem.BOX = SeciliUrun.BOX;
            }
        }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);

        public DelegateCommand IptalCommand => new DelegateCommand(OnIptal);

        private void OnIptal()
        {
            WindowService.Close();
        }

        public List<TBLIHRSTK> Urunler { get; }

        public List<GumrukTip> Proforma_HSCODE_Liste { get; }

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded, () => true);

        public List<NakliyeDurumTip> NakliyeDurumTipleri { get => nakliyeDurumTipleri; set => SetProperty(ref nakliyeDurumTipleri, value); }

        public string CariDovizTipSimge => DovizHelper.SimgeyeDonustur(CariDovizTipKod);

        public string CariDovizTipKod { get; set; }

        public TeklifKalemViewModel(TeklifKalemEditModel teklifKalem, string cariDovizTipKod)
        {
            CariDovizTipKod = cariDovizTipKod;
            SeciliTeklifKalem = teklifKalem;
            SeciliTeklifKalem.PropertyChanged += SeciliTeklifKalem_PropertyChanged;
            Urunler = LookupTables.Default.Urunler;
            Proforma_HSCODE_Liste = LookupTables.Default.GumrukTipleri;
            SeciliTeklifKalem.TeslimTarihi = DateTime.Now.Date;
            NakliyeDurumTipleri = new List<NakliyeDurumTip>();
            NakliyeDurumTipleri.Add(new NakliyeDurumTip { NakliyeDurumTipAdi = "Dahil" });
            NakliyeDurumTipleri.Add(new NakliyeDurumTip { NakliyeDurumTipAdi = "Hariç" });
            DonemGrupListesi = new List<DonemGrup>();

            DonemGrupListesi.Add(new DonemGrup { DonemGrupAdi = "Çeyreklik" });
            DonemGrupListesi.Add(new DonemGrup { DonemGrupAdi = "Aylık" });
            DonemGrupListesi.Add(new DonemGrup { DonemGrupAdi = "Yıllık" });
            DonemGrupListesi.Add(new DonemGrup { DonemGrupAdi = "Spot" });

            DonemListesi = new List<Donem>();

            DonemDoldur();
        }
        private void DonemDoldur()
        {
            DonemListesi = new List<Donem>();
            DonemListesi.Add(new Donem { DonemAdi = "1. Çeyrek", DonemGrupAdi = "Çeyreklik" });
            DonemListesi.Add(new Donem { DonemAdi = "2. Çeyrek", DonemGrupAdi = "Çeyreklik" });
            DonemListesi.Add(new Donem { DonemAdi = "3. Çeyrek", DonemGrupAdi = "Çeyreklik" });
            DonemListesi.Add(new Donem { DonemAdi = "4. Çeyrek", DonemGrupAdi = "Çeyreklik" });

            DonemListesi.Add(new Donem { DonemAdi = "Ocak", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Şubat", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Mart", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Nisan", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Mayıs", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Haziran", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Temmuz", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Ağustos", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Eylül", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Ekim", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Kasım", DonemGrupAdi = "Aylık" });
            DonemListesi.Add(new Donem { DonemAdi = "Aralık", DonemGrupAdi = "Aylık" });

            for (int i = 1; i < 53; i++)
            {
                DonemListesi.Add(new Donem { DonemAdi = i + ". Hafta", DonemGrupAdi = "Spot" });
            }
        }
        private void OnFormLoaded()
        {

        }

        private void SeciliTeklifKalem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DonemGrup")
            {
                DonemDoldur();
                DonemListesi = DonemListesi.Where(p=>p.DonemGrupAdi == SeciliTeklifKalem.DonemGrup).ToList();
            }
               
            if (e.PropertyName == "Miktar" || e.PropertyName == "SatisFiyati")
            {
                if (SeciliUrun != null)
                {
                    var toplamTutar = seciliTeklifKalem.SatisFiyati * seciliTeklifKalem.Miktar;
                    SeciliTeklifKalem.Tutar = (decimal)toplamTutar;

                    if (SeciliTeklifKalem.SatisFiyati != SeciliUrun.BirimFiyat)
                    {
                        decimal tutarNormal = (decimal)SeciliUrun.BirimFiyat * SeciliTeklifKalem.Miktar;
                        decimal tutarPlasiyer = SeciliTeklifKalem.Tutar;

                        SeciliTeklifKalem.Butce = tutarPlasiyer - tutarNormal;
                    }
                    else
                    {
                        SeciliTeklifKalem.Butce = 0;
                    }
                }
            }
        }

        private void OnKaydet()
        {
            var x = 1;
            if (SeciliTeklifKalem.TeklifKalemSiraKod == "Yeni")
                Messenger.Default.Send<KalemEklendiEvent>(new KalemEklendiEvent(SeciliTeklifKalem));
            else
                Messenger.Default.Send<KalemGuncellendiEvent>(new KalemGuncellendiEvent(SeciliTeklifKalem));

            WindowService.Close();

        }
    }

    public class DonemGrup : MyDxViewModelBase
    {
        private string donemGrupAdi;
        public string DonemGrupAdi { get => donemGrupAdi; set => SetProperty(ref donemGrupAdi, value); }

    }

    public class Donem : MyDxViewModelBase
    {
        private string donemGrupAdi;
        private string donemAdi;
        public string DonemAdi { get => donemAdi; set => SetProperty(ref donemAdi, value); }
        public string DonemGrupAdi { get => donemGrupAdi; set => SetProperty(ref donemGrupAdi, value); }
    }
}
