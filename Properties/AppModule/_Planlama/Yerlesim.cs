﻿namespace Pandap.UI.AppModule._Planlama
{
    public class Yerlesim
    {
        public string Ad { get; set; }
        public string XmlDosyaAd { get; set; }
        public Yerlesim(string ad, string xmlDosyaAd)
        {
            Ad = ad;
            XmlDosyaAd = xmlDosyaAd;
        }

        public override string ToString()
        {
            return Ad;
        }
    }
        
    
}