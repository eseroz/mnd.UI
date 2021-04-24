using DevExpress.Spreadsheet;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using System;
using System.Collections.Generic;

namespace mnd.UI.Modules._Services
{
    public class ExcelService
    {
        public ExcelService()
        {
        }

        internal static Tuple<bool, string, List<ExcelImportTanim>> SutunBasliklariYerindeMi(string formAdi, Worksheet _worksheet)
        {
            UnitOfWork uow = new UnitOfWork();

            var sutunListe = uow.AppRepo.ExcelImportTanimlar(formAdi);

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