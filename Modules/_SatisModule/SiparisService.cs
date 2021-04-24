using DevExpress.Mvvm;
using mnd.Logic.Model.Satis;
using mnd.Logic.Persistence;

namespace mnd.UI.Modules._SatisModule
{
    public class SiparisService
    {
        public static void SiparisAc(string sipkod)
        {
            var uow1 = new UnitOfWork();
            var vm = new SiparisViewModel();
            var sip = uow1.SiparisRepo.SiparisGetir(sipkod);

            vm.Load(sip);

            var doc = AppPandap.pDocumentManagerService.CreateDocument("SiparisView", vm);
            doc.Title = sip.SiparisKod;
            doc.Show();
        }

        public static SiparisKalem UrunKodundanSonKalemGetir(string musteriUrunKodu, string siparisKalemKod)
        {
            var uow1 = new UnitOfWork();
            var siparisKalem = uow1.SiparisKalemRepo.MusteriUrunKodundan_SiparisKalemiGetir(musteriUrunKodu, siparisKalemKod);
            return siparisKalem;
            uow1.Dispose();
        }


    }
}
