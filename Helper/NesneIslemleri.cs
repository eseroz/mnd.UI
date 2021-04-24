using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace mnd.UI.Helper
{
    public class NesneIslemleri
    {
        public static void OzellikDegerAta(object o, string ozellik, object deger)
        {
            PropertyInfo[] p = o.GetType().GetProperties();
            PropertyInfo pinfo = p.Where(c => c.Name.ToUpper() == ozellik.ToUpper()).FirstOrDefault();

            Type t = Nullable.GetUnderlyingType(pinfo.PropertyType) ?? pinfo.PropertyType;

            object safeValue = (deger == null) ? null : Convert.ChangeType(deger, t);

            pinfo.SetValue(o, safeValue, null);
        }

        public static object OzellikDegerAta1(object o, string ozellik, object deger)
        {
            PropertyInfo[] p = o.GetType().GetProperties();
            PropertyInfo pinfo = p.Where(c => c.Name.ToUpper() == ozellik.ToUpper()).FirstOrDefault();

            Type t = Nullable.GetUnderlyingType(pinfo.PropertyType) ?? pinfo.PropertyType;

            object safeValue = (deger == null) ? null : Convert.ChangeType(deger, t);

            pinfo.SetValue(o, safeValue, null);

            return o;
        }

        public static string NesneOzellikDegerGetir(object o, string ozellik)
        {
            PropertyInfo[] p = o.GetType().GetProperties();
            PropertyInfo pinfo = p.Where(c => c.Name.ToUpper() == ozellik.ToUpper()).FirstOrDefault();

            var u = pinfo.GetValue(o, null);

            if (u != null)
                return u.ToString();
            else
                return "";
        }

        public static object NesneOzellikObjeGetir(object o, string ozellik)
        {
            PropertyInfo[] p = o.GetType().GetProperties();
            PropertyInfo pinfo = p.Where(c => c.Name.ToUpper() == ozellik.ToUpper()).FirstOrDefault();

            var u = pinfo.GetValue(o, null);
            return u;
        }


        internal static List<string> NesneOzellikAdListe(object o)
        {
            var u = o.GetType().GetProperties().Select(p => p.Name).ToList();
            return u;
        }



    }
}