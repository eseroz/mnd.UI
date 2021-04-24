using mnd.Common.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;

namespace mnd.UI.Converters
{
    public class FormulRenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var formulRenk = (FormulEnum)value;
            var parametre = (string)parameter;

            string formulAlanRenk = "Yellow";
            string sonucRenk = "LightGreen";

            if (parameter == null) return null;

            if ((formulRenk == FormulEnum.Discap_Rulodan) && parametre.Contains(FormulEnum.Discap_Rulodan.ToString()))
            {
                if (parametre.Contains(FormulEnum.Discap_Rulodan.ToString() + "_Sonuc")) return sonucRenk;
                return formulAlanRenk;
            };

            if ((formulRenk == FormulEnum.RuloAgirligiMax_Rulodan) && parametre.Contains(FormulEnum.RuloAgirligiMax_Rulodan.ToString()))
            {
                if (parametre.Contains(FormulEnum.RuloAgirligiMax_Rulodan + "_Sonuc")) return sonucRenk;
                return formulAlanRenk;
            }

            if ((formulRenk == FormulEnum.RuloAgirligiMax_Metrajdan) && parametre.Contains(FormulEnum.RuloAgirligiMax_Metrajdan.ToString()))
            {
                if (parametre.Contains(FormulEnum.RuloAgirligiMax_Metrajdan + "_Sonuc")) return sonucRenk;
                return formulAlanRenk;
            }

            if ((formulRenk == FormulEnum.Metraj_Rulodan) && parametre.Contains(FormulEnum.Metraj_Rulodan.ToString()))
            {
                if (parametre.Contains(FormulEnum.Metraj_Rulodan.ToString() + "_Sonuc")) return sonucRenk;
                return formulAlanRenk;
            }

            return null; throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}