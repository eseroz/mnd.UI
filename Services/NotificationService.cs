using mnd.Logic.BC_MusteriTakip.Domain;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using mnd.UI.Modules.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Services
{
    public static class NotificationService
    {



        public static void SuresiGecenGorusme()
        {
            var _dc = new PandapContext();

            var PandapCariler = _dc.PandapCaris.ToList();

            var GorusulmeyenMusteriler = new List<Gorusme>();

            foreach (var cari in PandapCariler)
            {
                if (cari.Gorusmeler.LastOrDefault()?.GorusmeTarih != null)
                {
                    int yeniGun = 0;
                    TimeSpan? gun = (cari.Gorusmeler.LastOrDefault()?.GorusmeTarih - DateTime.Now);
                    if (gun.Value.TotalDays < 0) yeniGun = (int)gun.Value.TotalDays * -1;

                    if (yeniGun >= 14)
                    {
                        GorusulmeyenMusteriler.Add(cari.Gorusmeler.LastOrDefault());
                    }
                }
            }

            var GorusulmeyenMusteriBildirimiKullanicilari = _dc.KullaniciBildirim.Where(p=>p.GorusulmeyenMusteriSonBildirimTarihi < DateTime.Now).FirstOrDefault();

            var Plasiyerler = _dc.Kullanicilar.ToList();

            foreach (var Plasiyer in Plasiyerler)
            {

                var gorusulmeyenMusteriler = GorusulmeyenMusteriler.Where(p => p.PlasiyerKod == Plasiyer.PlasiyerKod).ToList();


                string musteriler = "<table><thead><tr><th>#</th><th>MÜŞTERİ</th><th>SON GÖRÜŞME TARİHİ</th></tr></thead>";

                musteriler = musteriler + "<body>";


                int sira = 0;
                foreach (var musteri in gorusulmeyenMusteriler)
                {
                    sira++;
                    string yeniMusteri = "<tr><td>" + sira.ToString() + "</td><td>" + musteri.CariIsim + "</td><td>" + musteri.GorusmeTarih + "</td></tr>";
                    musteriler = musteriler + yeniMusteri;
                }

                musteriler = musteriler + "</body></table>";

                string icerik =
                    "PLASİYER ADI : " + Plasiyer.AdSoyad + Environment.NewLine +
                    "SON İKİ HAFTADIR GÖRÜŞME YAPMADIĞI MÜŞTERİLER :" + Environment.NewLine +
                    musteriler;




                var sonuc = SendMail.Send(
                    "mndapp@mndgida.com.tr",
                    "Puq50786",
                    "CRM Bilgilendirme",
                    "bt2@pandaalu.com.tr",
                    "Günlük Müşteri Görüşmeleri Raporu", icerik, "");


            }



        }


    }
}
