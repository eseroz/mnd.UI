using mnd.UI.Modules._DialogViews.MusteriSecimDialog;

namespace mnd.UI.Modules.TeklifModule.MessangerEvents
{
    public class MusteriSecildiEvent
    {
        public MusteriItemModel Musteri { get; set; }
        public MusteriSecildiEvent(MusteriItemModel musteriItem)
        {
            Musteri = musteriItem;
        }
    }
}
