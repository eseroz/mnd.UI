using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Xpf.Reports.UserDesigner;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace mnd.UI.AppModules.RaporDesignerModule
{
    public class PandapRaporHelper
    {
        public static XtraReport XrRaporGetir(RaporTanim raporTanim)
        {
            if (raporTanim.RaporXmlStream == null)
                raporTanim.RaporXmlStream = PandapRaporHelper.VarsayilanRaporXmlTextGetir();

            StreamWriter sw = new StreamWriter(new MemoryStream());
            sw.Write(raporTanim.RaporXmlStream);
            sw.Flush();

            XtraReport xr = new XtraReport();
            xr.LoadLayout(sw.BaseStream);

            return xr;
        }


        public static void RaporDesignerShow(RaporTanim raporTanim, object dsMainObject, int width, int height, double zoom)
        {
            if (raporTanim.RaporXmlStream == null)
                raporTanim.RaporXmlStream = PandapRaporHelper.VarsayilanRaporXmlTextGetir();

            var xr = XrRaporGetir(raporTanim);
            var xr_subreportDataList = JsonConvert.DeserializeObject<List<EkRapor>>(raporTanim.SubReportJson ?? "");

            if (xr_subreportDataList != null)
            {
                UnitOfWork uow1 = new UnitOfWork();

                int i = 0;
                foreach (var item in xr_subreportDataList)
                {
                    i++;
                    var raporTanimSub1 = uow1.RaporTanimRepo.RaporGetirFromId(item.RaporId);
                    var xrSub1 = XrRaporGetir(raporTanimSub1);
                    var dsSubReportData = NesneIslemleri.NesneOzellikObjeGetir(dsMainObject, item.DataSource);
                    xrSub1.DataSource = dsSubReportData;

                    var sub1 = (XRSubreport)xr.FindControl("subreport" + i.ToString(), false);

                    sub1.ReportSource = xrSub1;
                    sub1.ReportSource.DataSource = dsSubReportData;
                    sub1.ReportSourceUrl = "";

                }

            }

            PandapRaporDesigner raporDesignerForm = new PandapRaporDesigner();
            raporDesignerForm.designer.View = new ReportDesignerClassicView();

            raporDesignerForm.designer.Commands = new CustomDesignerCommands();

            var doc = raporDesignerForm.designer.OpenDocument(xr);
            doc.Tag = raporTanim.Id;

            var ds = doc.ReportModel.DataSource as ObjectDataSource;

            if (ds != null)
                ds.DataSource = dsMainObject;
            else
                MessageBox.Show("Data Source Tanımı yapınız...");


            raporDesignerForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            raporDesignerForm.ShowDialog();
        }



        private static void SimpleRaporShow(RaporTanim raporTanim, object dsMainObject, int width, int height, double zoom)
        {
            var xr = XrRaporGetir(raporTanim);

            var dsFromFile = xr.DataSource as ObjectDataSource;

            dsFromFile.DataSource = dsMainObject;

            xr.DataSource = dsFromFile;
            xr.CreateDocument();

            PandapRaporSimpleViever rpSimpleForm = new PandapRaporSimpleViever();
            rpSimpleForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            rpSimpleForm.report_view_control.DocumentSource = xr;

            rpSimpleForm.report_view_control.ZoomFactor = zoom;
            rpSimpleForm.Width = width;
            rpSimpleForm.Height = height;

            rpSimpleForm.Show();
        }
        private static string VarsayilanRaporXmlTextGetir()
        {
            MemoryStream stream = new MemoryStream();
            XtraReport xr = new XtraReport();

            xr.SaveLayoutToXml(stream);

            stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            string xml_text = reader.ReadToEnd();

            xr.Dispose();
            stream.Dispose();
            reader.Dispose();

            return xml_text;
        }

        public static void ShowReport(RaporTanim raporTanim, object dsObject, int width, int height, double zoom)
        {
            if (AppPandap.RaporTasarimModuAktifMi == true)
                RaporDesignerShow(raporTanim, dsObject, width, height, zoom);
            else
                SimpleRaporShow(raporTanim, dsObject, width, height, zoom);
        }
    }
}