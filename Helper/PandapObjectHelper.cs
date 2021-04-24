using Newtonsoft.Json;

namespace mnd.UI.Helper
{
    public class PandapObjectHelper
    {
        public static T CopyObject<T>(T _obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(_obj));
        }
    }
}