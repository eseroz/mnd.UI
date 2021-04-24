using System;
using System.Linq;

namespace Pandap.UI.AppModule._Planlama
{
    public class UretimEmriMakineAsama
    {
        public string KenarKesme { get; set; }
        public string Bombe { get; set; }
        public int Ek { get; set; }


        public string EzmeYuzde
        {
            get
            {
                return ((1 - ProsesMin.GetValueOrDefault() / ProsesMax.GetValueOrDefault())*100).ToString("n1");
            }
          
        }


        public DateTime FiiliBaslamaZaman { get; set; }
        public DateTime FiiliBitisZaman { get; set; }
        public string Makine { get; set; }
        public decimal Miktar { get; set; }
        public string Notlar { get; set; }
        public string Operator { get; set; }
        public decimal? ProsesMax { get; set; }
        public decimal? ProsesMin { get; set; }

        public string Ra { get; set; }
        public int SatirNo { get; set; }
        public DateTime Tarih { get; set; }
    }
}