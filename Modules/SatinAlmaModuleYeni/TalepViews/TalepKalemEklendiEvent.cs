using mnd.Logic.BC_SatinAlmaYeni.Domain;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.TalepViews
{
    public class TalepKalemEklendiEvent
    {
        public TalepKalem TalepKalem { get; set; }
        public TalepKalemEklendiEvent(TalepKalem kalem)
        {
            TalepKalem = kalem;
        }
    }

    public class TalepKalemGuncellendiEvent
    {
        public TalepKalem TalepKalem { get; set; }
        public TalepKalemGuncellendiEvent(TalepKalem kalem)
        {
            TalepKalem = kalem;
        }
    }
}