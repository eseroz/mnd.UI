using System;

namespace mnd.UI.Helper
{
    public class PandapFormulServis
    {
        public static double RuloAgirligiMax_Metrajdan(double metre, double kalinlik, double en)
        {
            var agirlik = metre * kalinlik * en * 2.73 / 1000000;

            agirlik = Math.Round(agirlik, 0);

            return agirlik;
        }

        public static double RuloAgirligiMax_Rulodan(double rulo_ic_cap, double rulo_en, double rulo_dis_cap_max)
        {
            double a = Math.Pow(rulo_dis_cap_max / 20, 2);
            double b = Math.Pow(rulo_ic_cap / 20, 2);

            double x = a * 3.14 * 2.71 * rulo_en / 10;
            double y = b * 3.14 * 2.73 * rulo_en / 10;

            var agirlik = (x - y) / 1000;

            agirlik = Math.Round(agirlik, 0);
            return agirlik;
        }

        public static double Metraj_Rulodan(double rulo_en, double rulo_agirlik_max, double rulo_kalinlik)
        {
            var metraj = rulo_agirlik_max * 1000000 / (rulo_en * rulo_kalinlik * 2.71);

            metraj = Math.Round(metraj, 0);

            return metraj;
        }

        public static int RuloDiscapMax_Rulodan(double rulo_agirlik_max, double rulo_iccap, double rulo_en)
        {
            double a = (rulo_agirlik_max * 1000) / 2.73;

            double b = (rulo_en * 0.1);

            if (a == 0 || b == 0) return 0;

            double c = Math.Pow(rulo_iccap / 20, 2);

            var ruloDiscap = Math.Pow(a / b / 3.14 + c, 0.5) * 2 * 10;

            ruloDiscap = Math.Round(ruloDiscap, 0);

            return Convert.ToInt32(ruloDiscap);
        }
    }
}