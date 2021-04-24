using mnd.Logic.Model;
using System.Collections.Generic;

namespace mnd.UI.Modules.TeklifModule
{
    public class TeklifIslemVM : MyBindableBase
    {

        private string islemDurum;
        private string retNeden;
        private List<LookUpItem> retNedenleri;
        private List<LookUpItem> ıslemler;
        private string ıslemAciklama;

        public List<LookUpItem> RetNedenleri { get => retNedenleri; set => SetProperty(ref retNedenleri, value); }
        public List<LookUpItem> Islemler { get => ıslemler; set => SetProperty(ref ıslemler, value); }



        public string RetNeden { get => retNeden; set => SetProperty(ref retNeden, value); }

        public string IslemAciklama { get => ıslemAciklama; set => SetProperty(ref ıslemAciklama, value); }



        public TeklifIslemVM()
        {
            RetNedenleri = new List<LookUpItem>();
            Islemler = new List<LookUpItem>();

            RetNedenleri.Add(new LookUpItem { Id = "Fiyat", Aciklama = "Fiyat" });
            RetNedenleri.Add(new LookUpItem { Id = "Termin", Aciklama = "Termin Tarihi" });
            RetNedenleri.Add(new LookUpItem { Id = "Kalite", Aciklama = "Kalite" });
            RetNedenleri.Add(new LookUpItem { Id = "Üretilebilirlik", Aciklama = "Üretilebilirlik" });


        }



    }
}
