using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_Uretim;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.Dashboard
{
    public class MakinaMaliyetService
    {

        public List<MakinaPerformansDTO> MakinaPerformansRaporDataGetir( DateTime raporTarihi)
        {
            MakinaPerformansRepository repo = new MakinaPerformansRepository();

            var performansVerileri = repo.PerformansTabloGetir(raporTarihi.Date, raporTarihi.Date, "");
            return performansVerileri;
        }


        public bool MailGonder(MailTanim mailTanim, string aciklamaEk,  string htmlTablo)
        {
            var mailKime = mailTanim.Kimlere;
            var mailKonu = mailTanim.KonuAciklama + "-" + aciklamaEk;
            var mailIcerik = htmlTablo;

            var cev = SendMail.Send(mailTanim.GonderenHesap, mailTanim.HesapParola, "", mailKime,
                mailKonu, mailIcerik, "");

    
            if(cev==false)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry(mailTanim.GonderenHesap + " Mail Gönderilemedi", EventLogEntryType.Information, 101, 1);
                }
            }
           

            return cev;

        }

        public string PerformansHtmlTabloOlustur(List<MakinaPerformansDTO> dataTablo, DateTime raporTarihi)
        {

            if (dataTablo.Count == 0) return raporTarihi.ToLongDateString() + " tarihi için veri bulunamamıştır.";

            var thStyle = "text-align:center;background:black;color:white;";

            var table_ust = $@"<table style='width:600px;border:1px solid'>
              <tr>
                <th style='text-align:center;background:black;color:white;width:200px'>MAKİNA ADI</th>
                <th style='{thStyle}'>GİREN KG</th>
                <th style='{thStyle}'>ÇIKAN KG</th>
                <th style='{thStyle}'>HURDA KG</th>
                <th style='{thStyle}'>HURDA %</th>
                <th style='{thStyle}'>RUN SAAT</th>
                <th style='{thStyle}'>BAKIM SAAT</th>
                <th style='{thStyle}'>İŞLETME SAAT</th>
                <th style='{thStyle}'>İDARİ SAAT</th>
                <th style='{thStyle}'>TOPLAM SAAT</th>
              </tr>";

            var table_rowData = "";
            foreach (var item in dataTablo)
            {
                var row = $@"
                      <tr>
                        <th style='text-align:left'>{item.MakinaAd}</th>
                        <th style='text-align:right'>{item.Giren_kg.ToString("N0")}</th>
                        <th style='text-align:right'>{item.Cikan_kg.ToString("N0")}</th>
                        <th style='text-align:right'>{item.Hurda_kg.ToString("N0")}</th>
                        <th style='text-align:right'>{Math.Round(item.HurdaYuzde, 1).ToString("N1")}</th>
                        
                        <th style='text-align:right'>{Math.Round(item.Run_hr, 1).ToString("N1")}</th>
                        <th style='text-align:right'>{Math.Round(item.BakimDurus_hr, 1).ToString("N1")}</th>
                        <th style='text-align:right'>{Math.Round(item.IsletmeDurus_hr, 1).ToString("N1")}</th>
                        <th style='text-align:right'>{Math.Round(item.IdariDurus_hr, 1).ToString("N1")}</th>
                        <th style='text-align:right'>{Math.Round(item.ToplamDurus_hr, 1).ToString("N1")}</th>

                 </tr>";

                table_rowData += row;
            }

            var globalStyle = @"<style>td,th,table {border:1px solid black}</style>";
            var htmlTable = globalStyle + table_ust + table_rowData + "</table>";

            return htmlTable;
        }

     
    }
}
