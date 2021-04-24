using System;

namespace mnd.UI.GyModules.MesajModule
{
    public class KayitSatirMesajEvent
    {
        public Guid RowGuid { get; set; }

        public KayitSatirMesajEvent(Guid rowGuid)
        {
            RowGuid = rowGuid;
        }
    }
}
