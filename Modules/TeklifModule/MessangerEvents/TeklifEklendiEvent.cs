using mnd.UI.Modules.TeklifModule.Models;

namespace mnd.UI.Modules.TeklifModule.MessangerEvents
{
    public class TeklifEklendiEvent
    {
        public TeklifEklendiEvent(TeklifEditModel teklif)
        {
            Teklif = teklif;
        }
        public TeklifEditModel Teklif { get; set; }
    }
}
