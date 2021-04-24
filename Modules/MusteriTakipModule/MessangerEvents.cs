namespace mnd.UI.Modules.MusteriTakipModule
{
    public enum KayitIslemTip { Eklendi, Silindi, Degisti }

    public class KayitMesaj
    {

        public KayitIslemTip KayitIslemTip { get; private set; }
        public object Kayit { get; set; }

        public KayitMesaj(object kayit, KayitIslemTip messageType)
        {
            Kayit = kayit;
            KayitIslemTip = messageType;
        }
    }
}
