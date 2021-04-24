using mnd.Logic.BC_Satis.Data_LookUp.Model;
using mnd.Logic.BC_Satis.Repositories;
using mnd.Logic.Model.App;
using mnd.Logic.Model.Stok;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.TeklifModule.Services
{
     public class LookupTables 
     {

        public static LookupTables Default;

        static LookupTables()
        {
            Default = new LookupTables();
        }


        public List<OdemeTip> OdemeTipleri { get; set; }
        public List<SatisTip> SatisTipleri { get; set; }
        public List<LmeTip> LmeTipleri { get; set; }
        public List<TeslimTip> TeslimTipleri { get; set; }
        public List<BirimTip> BirimTipleri { get; set; }
        public List<DovizTip> DovizTipleri { get; set; }

        public List<GumrukTip> GumrukTipleri { get; set; }


     public ObservableCollection<Banka> BankaHesaplari { get; set; }
        public List<AlasimTip> AlasimTipleri { get; set; }
        public List<AmbalajTip> AmbalajTipleri { get; set; }
        public List<KulcePrimTip> KulcePrimTipleri { get; set; }
        public List<KullanimAlanTip> KullanimAlanTipleri { get; set; }
        public List<MasuraTip> MasuraTipleri { get; set; }
        public List<SertlikTip> SertlikTipleri { get; set; }
        public List<YuzeyTip> YuzeyTipleri { get; set; }
        public List<TBLIHRSTK> Urunler { get;  set; }

        public List<TasimaSekli> TasimaSekilleri { get; set; }

        public LookupTables()
        {
            Yukle();
        }

        public void Yukle()
        {
            LookUpDataService repo = new LookUpDataService();



            OdemeTipleri = repo.OdemeTipleriGetir();
            SatisTipleri = repo.SatisTipleriGetir();
            //LmeTipleri = repo.LmeTipleriGetir();
            TeslimTipleri = repo.TeslimTipleriGetir();
            BirimTipleri = repo.BiriTipleriGetir();
            DovizTipleri = repo.DovizTipleriGetir();
            GumrukTipleri = repo.GumrukTipleriGetir();

            //AlasimTipleri = repo.AlasimTipleriGetir();
            //AmbalajTipleri = repo.AmbalajTipleriGetir();
            //KulcePrimTipleri = repo.KulcePrimTipleriGetir();
            //KullanimAlanTipleri = repo.KullanimAlanTipleriGetir();
            //MasuraTipleri = repo.MasuraTipleriGetir();
            //SertlikTipleri = repo.SertlikTipleriGetir();
            //YuzeyTipleri = repo.YuzeyTipleriGetir();

            Urunler = repo.UrunleriGetir();
            BankaHesaplari = repo.BankaHesaplariGetir();

            TasimaSekilleri = repo.TasimaSekilleriGetir();
        }



        private string OdemeTipleriGetir(string id)
        {
            return OdemeTipleri.Where(r => r.OdemeTipKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }

        private string SatisTipleriGetir(string id)
        {
            return SatisTipleri.Where(r => r.SatisTipKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }

        private string LmeTipleriGetir(string id)
        {
            return LmeTipleri.Where(r => r.LmeTipKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }

        private string TeslimTipleriGetir(string id)
        {
            return TeslimTipleri.Where(r => r.TeslimTipKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }

        private string BiriTipleriGetir(string id)
        {
            return BirimTipleri.Where(r => r.BirimTipKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }

        private string DovizipleriGetir(string id)
        {
            return DovizTipleri.Where(r => r.DovizTipKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }

        private string AlasimTipleriGetir(string id)
        {
            return AlasimTipleri.Where(r => r.AlasimKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }

        private string AmbalajTipleriGetir(string id)
        {
            return AmbalajTipleri.Where(r => r.AmbalajKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }


        private string KulcePrimTipleriGetir(string id)
        {
            return KulcePrimTipleri.Where(r => r.KulcePrimTipKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }



        private string KullanimAlanTipleriGetir(string id)
        {
            return KullanimAlanTipleri.Where(r => r.KullanimAlanKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }


        private string MasuraTipleriGetir(string id)
        {
            return MasuraTipleri.Where(r => r.MasuraKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }


        private string SertlikTipleriGetir(string id)
        {
            return SertlikTipleri.Where(r => r.SertlikKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }


        private string YuzeyTipleriGetir(string id)
        {
            return YuzeyTipleri.Where(r => r.YuzeyKod == id).Select(r => r.Aciklama).FirstOrDefault();
        }


        private List<TasimaSekli> TasimaTipleriGetir(string id)
        {
            return TasimaSekilleri.ToList();
        }

    }
}
