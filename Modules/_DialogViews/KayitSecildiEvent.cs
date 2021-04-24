namespace mnd.UI.Modules._DialogViews
{
    public class KayitSecildiEvent<T>
    {
        public T SecilenKayit { get; set; }
        public KayitSecildiEvent(T kayit)
        {
            SecilenKayit = kayit;
        }
    }
}