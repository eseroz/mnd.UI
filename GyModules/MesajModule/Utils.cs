using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace mnd.UI.GyModules.MesajModule
{
    public class Utils
    {
        public static void BlobdanDosyaAç(BlobInfo b)
        {
            FileStream fs = new FileStream(Path.GetTempPath() + "\\" + b.FileName, FileMode.Create);
            fs.Write(b.Buffer, 0, System.Convert.ToInt32(b.Buffer.Length));
            fs.Close();
            ProcessStartInfo psi = new ProcessStartInfo(Path.GetTempPath() + "\\" + b.FileName);
            Process.Start(psi);
        }

        public class ImageUtils
        {
            public static Image ImageFromBase64(string data)
            {
                byte[] binaryData = Convert.FromBase64String(data);

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(binaryData);
                bi.EndInit();

                Image img = new Image();
                img.Source = bi;

                return img;
            }
        }
    }
}