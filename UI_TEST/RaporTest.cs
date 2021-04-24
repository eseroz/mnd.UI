using mnd.Logic.Persistence;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Services;

namespace mnd.UI.UI_TEST
{

    public class RaporTest
    {
        public static void YazdirTest()
        {
            var uow = new UnitOfWork();
            var raporTanimMain = uow.RaporTanimRepo.RaporGetirFromId(9);


            var dsObject = UretimEmriService.Create_UretimEmriDTO_FromUretimEmriKod("P19-1802/03-U1");


            PandapRaporHelper.RaporDesignerShow(raporTanimMain, dsObject, raporTanimMain.Width, raporTanimMain.Height, raporTanimMain.ZoomFaktor);
        }
    }
}
