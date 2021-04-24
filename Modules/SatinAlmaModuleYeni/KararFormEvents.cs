using mnd.Logic.BC_SatinAlmaYeni.Domain;

namespace mnd.UI.Modules.SatinAlmaModuleYeni
{
    public class YoneticiKararFormKaydedildiEvent
    {
        public Talep KararTalep { get; set; }
        public YoneticiKararFormKaydedildiEvent(Talep seciliTeklif)
        {
            KararTalep = seciliTeklif;
        }
    }
}
