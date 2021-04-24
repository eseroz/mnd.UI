using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mnd.Logic.BC_Dokum.Model;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    public class BobinCikartildiEvent
    {
        public BobinCikartildiEvent(DokumBobin bobin)
        {
            Bobin = bobin;
        }
        public DokumBobin Bobin { get; set; }
    }

    public class BobinIslemIptalEvent
    {
        public BobinIslemIptalEvent(DokumBobin bobin)
        {
            Bobin = bobin;
        }
        public DokumBobin Bobin { get; set; }
    }

    public class BobinUpdateEvent
    {
        public BobinUpdateEvent(DokumBobin bobin)
        {
            Bobin = bobin;
        }
        public DokumBobin Bobin { get; set; }
    }
}
