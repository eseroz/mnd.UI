using mnd.Logic.BC_Satis._PotansiyelDisi;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar.Events
{
    public class PTD_MusteriAramaEklendiEvents
    {
        public PotansiyelDisiMusteriArama Arama { get; set; }
        public PTD_MusteriAramaEklendiEvents(PotansiyelDisiMusteriArama arama)
        {
            Arama = arama;
        }
    }

    public class PTD_MusteriAramaGuncellendiEvent
    {
        public PotansiyelDisiMusteriArama Arama { get; set; }
        public PTD_MusteriAramaGuncellendiEvent(PotansiyelDisiMusteriArama arama)
        {
            Arama = arama;
        }
    }
}
