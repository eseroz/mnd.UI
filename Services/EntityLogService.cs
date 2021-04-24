using Newtonsoft.Json;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence.Repositories;
using System;
using System.Collections.ObjectModel;

namespace mnd.UI.Services
{
    public class EntityLogService
    {
        public static ObservableCollection<EntityLog> EntityLogKayitlariGetir(Guid guid)
        {
            AppEntityLogRepository repo = new AppEntityLogRepository();
            var entityLogs = repo.GetEntityLogsFromGuidWithoutJsonStream(guid);
            return entityLogs;
        }

        public static T LogdanEntityGetir<T>(int id)
        {

            AppEntityLogRepository repo = new AppEntityLogRepository();
            var jsonString = repo.GetEntityLogsFromGuidJsonStream(id);

            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static void EntityLogEkleKaydet(EntityLog log)
        {
            AppEntityLogRepository repo = new AppEntityLogRepository();
            repo.EkleKaydet(log);
        }

        public static EntityLog EntityLogla<T>(T c)
        {
            var rowGuid = (Guid)c.GetType().GetProperty("RowGuid").GetValue(c);

            EntityLog log = new EntityLog
            {
                EntityRowGuid = rowGuid,
                EntityJsonStream = JsonConvert.SerializeObject(c),
                KullaniciAdSoyad = AppPandap.AktifKullanici.KullaniciId,
                KayitTarihi = DateTime.Now
            };


            EntityLogService.EntityLogEkleKaydet(log);

            return log;
        }
    }
}
