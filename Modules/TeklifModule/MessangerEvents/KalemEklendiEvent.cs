using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mnd.UI.Modules.TeklifModule.Models;

namespace mnd.UI.Modules.TeklifModule.MessangerEvents
{
    public class KalemEklendiEvent
    {
        public TeklifKalemEditModel TeklifKalemEditModel { get; set; }
        public KalemEklendiEvent(TeklifKalemEditModel kalem)
        {
            TeklifKalemEditModel = kalem;
        }
    }

    public class KalemGuncellendiEvent
    {
        public TeklifKalemEditModel TeklifKalemEditModel { get; set; }
        public KalemGuncellendiEvent(TeklifKalemEditModel kalem)
        {
            TeklifKalemEditModel = kalem;
        }
    }


}
