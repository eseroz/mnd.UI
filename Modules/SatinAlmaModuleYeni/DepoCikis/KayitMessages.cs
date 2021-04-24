namespace mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis
{
    public class KayitIslemEvent<T>
    {
        public KayitIslemEvent(T kayit, string islemTip = "Add")
        {
            Kayit = kayit;
            IslemTip = islemTip;
        }
        public T Kayit { get; set; }
        public string IslemTip { get; }
    }
}
