using mnd.Logic.BC_Dashboard;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mnd.UI.Modules.SatinAlmaModuleYeni.Stoklar;

namespace mnd.UI.Modules.Dashboard
{
    public class ExwMaliyetVM
    {
        MaliyetRaporServices servis = new MaliyetRaporServices();

        public List<Exw> ExwMaliyetListe { get; set; }
        public ExwMaliyetVM(string form)
        {

            var dovizKurlari = NetsisService.NetsisBelirliTarihtenSonrakiDovizKurlariniGetir(DateTime.Now.AddYears(-3));

            ExwMaliyetListe =servis.ExwMaliyetGetir();

            foreach (var item in ExwMaliyetListe)
            {
                if(item.UlkeAd== "TÜRKIYE")
                {
                    var parite = StokListViewModel.PariteGetir("TL",
                    item.IrsaliyeTarihi.GetValueOrDefault(), dovizKurlari, item.FaturaDovizTip);

                    item.NakliyeFiyatiDVZ = item.NakliyeFiyati * parite;
                }
                else
                {
                    item.NakliyeFiyatiDVZ = Math.Round(item.NakliyeFiyati.GetValueOrDefault(),0);

                }

               
            }




        }

    }
}
