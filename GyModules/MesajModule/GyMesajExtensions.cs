using mnd.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mnd.UI.GyModules.MesajModule
{
    public static class GyMesaj_Extension
    {
        public static void MesajSayilariniGuncelle<T>(this IEnumerable<T> data, string kullaniciId)
        {
            GyMesajRepository repo = new GyMesajRepository();

            var mesajSayilari = repo.MesajSayilariGetir(kullaniciId);

            data.ToList().ForEach(c =>
            {

                var rowGuid = (Guid)c.GetType().GetProperty("RowGuid").GetValue(c);

                var mesajSayisi = mesajSayilari.ContainsKey(rowGuid) ? mesajSayilari[rowGuid].ToplamMesajSayisi : 0;
                var okunmamisMesajSayisi = mesajSayilari.ContainsKey(rowGuid) ? mesajSayilari[rowGuid].OkunmamisMesajSayisi : 0;

                c.GetType().GetProperty("MesajSayisi").SetValue(c, mesajSayisi);
                c.GetType().GetProperty("OkunmamisMesajSayisi").SetValue(c, okunmamisMesajSayisi);
            });

        }

        public static void MesajSayisiniGuncelle<T>(this T data, string kullaniciId)
        {
            GyMesajRepository repo = new GyMesajRepository();

            var mesajSayilari = repo.MesajSayilariGetir(kullaniciId);
            var c = data;

            var rowGuid = (Guid)c.GetType().GetProperty("RowGuid").GetValue(c);
            var mesajSayisi = mesajSayilari.ContainsKey(rowGuid) ? mesajSayilari[rowGuid].ToplamMesajSayisi : 0;
            var okunmamisMesajSayisi = mesajSayilari.ContainsKey(rowGuid) ? mesajSayilari[rowGuid].OkunmamisMesajSayisi : 0;

            c.GetType().GetProperty("MesajSayisi").SetValue(c, mesajSayisi);
            c.GetType().GetProperty("OkunmamisMesajSayisi").SetValue(c, okunmamisMesajSayisi);


        }

    }

}
