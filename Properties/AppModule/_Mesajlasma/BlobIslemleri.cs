using System.Diagnostics;
using System.IO;

namespace Pandap.UI.AppModule._Mesajlasma
{
    public class PandaUtil
    {
        public static void BlobdanDosyaAç(BlobInfo b)
        {
            FileStream fs = new FileStream(Path.GetTempPath() + "\\" + b.FileName, FileMode.Create);
            fs.Write(b.Buffer, 0, System.Convert.ToInt32(b.Buffer.Length));
            fs.Close();
            ProcessStartInfo psi = new ProcessStartInfo(Path.GetTempPath() + "\\" + b.FileName);
            Process.Start(psi);
        }
    }

    public class BlobInfo
    {
        public string FileName;
        public byte[] Buffer;
    }
}