using mnd.Logic.BC_SatinAlmaYeni.Domain;

namespace mnd.UI.Modules._DialogViews.StokSecimDialog
{
    public class StokSecildiEvent
    {
        public vwStokTanim StokTanim { get; set; }
        public StokSecildiEvent(vwStokTanim stokTanim)
        {
            StokTanim = stokTanim;
        }
    }
}