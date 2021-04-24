using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    public class BobinBarkodModel
    {
        public int BobinNo { get; set; }
        public decimal Kalinlik { get; set; }
        public string Alasim { get; internal set; }
        public string QrData { get; internal set; }
    }
}
