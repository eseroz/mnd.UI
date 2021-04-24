using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace mnd.UI.Modules.FinansModule
{
    public class ImageConverterFinans : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Panda") return "./FirmaIcons/image1.png";
            if (value.ToString() == "PandaVan") return "./FirmaIcons/image1_1.png";
            if (value.ToString() == "MndGıda") return "./FirmaIcons/image2.png";
            if (value.ToString() == "Bauxide") return "./FirmaIcons/image3.png";

            if (value.ToString() == "Panab") return "./FirmaIcons/image4.png";
            if (value.ToString() == "Pantech") return "./FirmaIcons/image5.png";
            if (value.ToString() == "Seherli") return "./FirmaIcons/image6.png";

            if (value.ToString() == "PanabTek") return "./FirmaIcons/image7.png";

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
