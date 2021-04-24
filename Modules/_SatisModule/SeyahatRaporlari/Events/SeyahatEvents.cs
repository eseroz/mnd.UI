
using mnd.Logic.BC_Satis._Seyahat;

namespace mnd.UI.Modules._SatisModule.SeyahatRaporlari.Events
{
    public class SeyahatEklendiEvent
    {
        public Seyahat Seyahat { get; set; }
        public SeyahatEklendiEvent(Seyahat seyahat)
        {
            Seyahat = seyahat;
        }
    }

    public class SeyahatGuncellendiEvent
    {
        public Seyahat Seyahat { get; set; }
        public SeyahatGuncellendiEvent(Seyahat seyahat)
        {
            Seyahat = seyahat;
        }
    }
}
