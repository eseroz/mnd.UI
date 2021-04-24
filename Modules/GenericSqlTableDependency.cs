using DevExpress.Mvvm;
using mnd.Common;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace mnd.UI.Modules
{

    public class SiparisSurecDurumDto
    {
        public string SiparisKod { get; set; }

        public string SiparisSurecDurum { get; set; }

    }

    public class LmeOnayDto
    {
        public string RefNo { get; set; }
        public string LmeKayitSurecDurum { get; set; }
        public string LmeKayitSurecDurumOnceki { get; set; }
    }

    public class HaftalikPlanDTO
    {
        public string Id { get; set; }
        public string Pazartesi { get; set; }
        public string Sali { get; set; }
        public string Carsamba { get; set; }
        public string Persembe { get; set; }
        public string Cuma { get; set; }
        public string Cumartesi { get; set; }
        public string Pazar { get; set; }

        public string UpdateUserId { get; set; }


    }

    public class SqlTable_Dependency<T> where T : class, new()
    {
        public static SqlTable_Dependency<T> Default { get; set; }

        SqlTableDependency<T> tdp;

        public string GonderenKullaniciId { get; private set; }

        static SqlTable_Dependency()
        {

            Default = new SqlTable_Dependency<T>();
        }

        public void Basla(string tabloAdi, string sema, string gonderen_kullanici_id)
        {

            if (tdp == null)
                tdp = new SqlTableDependency<T>(GlobalSettings.Default.SqlCnnString, tabloAdi, sema);

            GonderenKullaniciId = gonderen_kullanici_id;

            tdp.OnChanged += TableDependency_Changed;
            tdp.Start();
        }

        private void TableDependency_Changed(object sender, RecordChangedEventArgs<T> e)
        {
            if (e.ChangeType == ChangeType.Update)
            {
                //var updateUserId = e.Entity.GetType().GetProperty("UpdateUserId").GetValue(e.Entity)?.ToString();

                //if (updateUserId != null && AppPandap.AktifKullanici.KullaniciId == updateUserId) return;

                Messenger.Default.Send<T>(e.Entity);
            }
        }


        public void Durdur()
        {
            //tdp.OnChanged -= TableDependency_Changed;
            //tdp.Stop();
        }

        ~SqlTable_Dependency()
        {

        }

    }
}
