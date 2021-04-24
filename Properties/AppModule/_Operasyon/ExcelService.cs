using DevExpress.Spreadsheet;
using Pandap.Logic.Model.App;
using Pandap.Logic.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandap.UI.AppModule._Operasyon
{
    public class ExcelService
    {
       
        public ExcelService()
        {

        }

        internal static Tuple<bool, string,List<ExcelImportTanim>> SutunBasliklariYerindeMi(string formAdi, Worksheet _worksheet)
        {
            UnitOfWork uow = new UnitOfWork();

            var sutunListe= uow.AppRepo.ExcelImportTanimlar(formAdi);

            string sonucBilgi = "";
           

            foreach (var soru in sutunListe)
            {
                string sutunBaslik = _worksheet.Cells[soru.ExcelBaslikHücreKonum].Value.ToString();

                if (sutunBaslik != soru.DbTabloSutunBaslik)
                {
                    string bilgi = soru.DbTabloSutunBaslik + " Sutun Başlığı veya Hücre Konumu Değiştirilemez" +
                        "\r\nBaşlığın Bulunması Gereken Hücre: " + soru.ExcelBaslikHücreKonum +
                         "\r\n(Orjinal Excel Dosyasına Bakınız)";

                    sonucBilgi = bilgi; break;
                }
            }

            bool isValid = sonucBilgi == "";


            return new Tuple<bool, string, List<ExcelImportTanim>>(isValid, sonucBilgi, sutunListe);

        }
    }
}
