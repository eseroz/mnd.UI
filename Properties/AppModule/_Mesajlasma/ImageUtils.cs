using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pandap.UI.AppModule._Mesajlasma
{
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