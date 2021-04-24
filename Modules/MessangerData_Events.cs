using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules
{
    public class KayitSecildiEvent<T>
    {
        public T SecilenKayit { get; set; }
        public KayitSecildiEvent(T kayit)
        {
            SecilenKayit = kayit;
        }
    }

    public class KayitEklendiEvent<T>
    {
        public T EklenenKayit { get; set; }
        public KayitEklendiEvent(T kayit)
        {
            EklenenKayit = kayit;
        }
    }


    public class KayitGuncellendiEvent<T>
    {
        public T GuncellenenKayit { get; set; }
        public KayitGuncellendiEvent(T kayit)
        {
            GuncellenenKayit = kayit;
        }
    }

    public class KayitSilindiEvent<T>
    {
        public T SilinenKayit { get; set; }
        public KayitSilindiEvent(T kayit)
        {
            SilinenKayit = kayit;
        }
    }

}
