using System.Collections.Generic;

namespace mnd.UI.Modules.TeklifModule
{
    public class LookUpData
    {
        public static List<LookUpItem> DonemGruplar()
        {

            List<LookUpItem> donemGruplar = new List<LookUpItem>();

            donemGruplar.Add(new LookUpItem { Id = "Çeyreklik", ParentId = "", Aciklama = "Çeyreklik", Aciklama_EN = "" });
            donemGruplar.Add(new LookUpItem { Id = "Aylik", ParentId = "", Aciklama = "Aylık", Aciklama_EN = "Monthly" });
            donemGruplar.Add(new LookUpItem { Id = "Yillik", ParentId = "", Aciklama = "Yıllık", Aciklama_EN = "Yearly" });
            donemGruplar.Add(new LookUpItem { Id = "Spot", ParentId = "", Aciklama = "Spot", Aciklama_EN = "Week" });

            return donemGruplar;
        }


        public static List<LookUpItem> Donemler()
        {
            List<LookUpItem> donemler = new List<LookUpItem>();


            donemler.Add(new LookUpItem { Id = "Ocak", ParentId = "Aylik", Aciklama = "Ocak", Aciklama_EN = "January" });
            donemler.Add(new LookUpItem { Id = "Şubat", ParentId = "Aylik", Aciklama = "Şubat", Aciklama_EN = "February" });
            donemler.Add(new LookUpItem { Id = "Mart", ParentId = "Aylik", Aciklama = "Mart", Aciklama_EN = "March" });
            donemler.Add(new LookUpItem { Id = "Nisan", ParentId = "Aylik", Aciklama = "Nisan", Aciklama_EN = "April" });
            donemler.Add(new LookUpItem { Id = "Mayıs", ParentId = "Aylik", Aciklama = "Mayıs", Aciklama_EN = "May" });
            donemler.Add(new LookUpItem { Id = "Haziran", ParentId = "Aylik", Aciklama = "Haziran", Aciklama_EN = "June" });
            donemler.Add(new LookUpItem { Id = "Temmuz", ParentId = "Aylik", Aciklama = "Temmuz", Aciklama_EN = "July" });
            donemler.Add(new LookUpItem { Id = "Ağustos", ParentId = "Aylik", Aciklama = "Ağustos", Aciklama_EN = "August" });
            donemler.Add(new LookUpItem { Id = "Eylül", ParentId = "Aylik", Aciklama = "Eylül", Aciklama_EN = "September" });
            donemler.Add(new LookUpItem { Id = "Ekim", ParentId = "Aylik", Aciklama = "Ekim", Aciklama_EN = "October" });
            donemler.Add(new LookUpItem { Id = "Kasım", ParentId = "Aylik", Aciklama = "Kasım", Aciklama_EN = "November" });
            donemler.Add(new LookUpItem { Id = "Aralık", ParentId = "Aylik", Aciklama = "Aralık", Aciklama_EN = "December" });


            donemler = new List<LookUpItem>();
            donemler.Add(new LookUpItem { Id = "Q1", ParentId = "Çeyreklik", Aciklama = "Q1", Aciklama_EN = "Q1" });
            donemler.Add(new LookUpItem { Id = "Q2", ParentId = "Çeyreklik", Aciklama = "Q2", Aciklama_EN = "Q2" });
            donemler.Add(new LookUpItem { Id = "Q3", ParentId = "Çeyreklik", Aciklama = "Q3", Aciklama_EN = "Q3" });
            donemler.Add(new LookUpItem { Id = "Q4", ParentId = "Çeyreklik", Aciklama = "Q4", Aciklama_EN = "Q4" });



            donemler.Add(new LookUpItem { Id = "Ocak", ParentId = "Aylik", Aciklama = "Ocak", Aciklama_EN = "January" });
            donemler.Add(new LookUpItem { Id = "Şubat", ParentId = "Aylik", Aciklama = "Şubat", Aciklama_EN = "February" });
            donemler.Add(new LookUpItem { Id = "Mart", ParentId = "Aylik", Aciklama = "Mart", Aciklama_EN = "March" });
            donemler.Add(new LookUpItem { Id = "Nisan", ParentId = "Aylik", Aciklama = "Nisan", Aciklama_EN = "April" });
            donemler.Add(new LookUpItem { Id = "Mayıs", ParentId = "Aylik", Aciklama = "Mayıs", Aciklama_EN = "May" });
            donemler.Add(new LookUpItem { Id = "Haziran", ParentId = "Aylik", Aciklama = "Haziran", Aciklama_EN = "June" });
            donemler.Add(new LookUpItem { Id = "Temmuz", ParentId = "Aylik", Aciklama = "Temmuz", Aciklama_EN = "July" });
            donemler.Add(new LookUpItem { Id = "Ağustos", ParentId = "Aylik", Aciklama = "Ağustos", Aciklama_EN = "August" });
            donemler.Add(new LookUpItem { Id = "Eylül", ParentId = "Aylik", Aciklama = "Eylül", Aciklama_EN = "September" });
            donemler.Add(new LookUpItem { Id = "Ekim", ParentId = "Aylik", Aciklama = "Ekim", Aciklama_EN = "October" });
            donemler.Add(new LookUpItem { Id = "Kasım", ParentId = "Aylik", Aciklama = "Kasım", Aciklama_EN = "November" });
            donemler.Add(new LookUpItem { Id = "Aralık", ParentId = "Aylik", Aciklama = "Aralık", Aciklama_EN = "December" });


            for (int i = 1; i <= 52; i++)
            {
                donemler.Add(new LookUpItem
                {
                    Id = i.ToString(),
                    ParentId = "Spot",
                    Aciklama = i.ToString() + ".Hafta",
                    Aciklama_EN = "week " + i.ToString()
                }); ;
            }



            return donemler;
        }



    }
}
