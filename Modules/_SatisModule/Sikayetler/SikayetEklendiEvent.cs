using mnd.Logic.BC_Satis._Sikayet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules._SatisModule.Sikayetler
{
    public class SikayetEklendiEvent
    {
        public Sikayet Sikayet { get; set; }
        public SikayetEklendiEvent(Sikayet sikayet)
        {
            Sikayet = sikayet;
        }
    }
}
