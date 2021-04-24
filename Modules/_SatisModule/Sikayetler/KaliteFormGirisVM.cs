using mnd.Logic.BC_Satis._Sikayet;

namespace mnd.UI.Modules._SatisModule.Sikayetler
{
    public class KaliteFormGirisVM
    {
        public Sikayet SeciliSikayet { get; set; }

        public KaliteFormGirisVM(Sikayet sikayet)
        {
            SeciliSikayet = sikayet;
        }


    }
}
