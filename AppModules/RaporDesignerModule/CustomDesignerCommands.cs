using DevExpress.Xpf.Reports.UserDesigner;
using mnd.Logic.Persistence;
using System.IO;

namespace mnd.UI.AppModules.RaporDesignerModule
{
    public class CustomDesignerCommands : ReportDesignerCommands
    {
        public CustomDesignerCommands()
        {
        }

        protected override void SaveDocument()
        {
            UnitOfWork uow = new UnitOfWork();

            int id = (int)base.Designer.ActiveDocument.Tag;
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(id);

            MemoryStream stream = new MemoryStream();

            base.Designer.ActiveDocument.Report.SaveLayoutToXml(stream);

            stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            string text = reader.ReadToEnd();

            raporTanim.RaporXmlStream = text;

            uow.RaporTanimRepo.RaporTanimKaydet(raporTanim);
        }
    }
}