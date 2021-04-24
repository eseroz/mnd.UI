using Newtonsoft.Json;
using mnd.Common.EntityHelpers;
using System.Collections.Generic;

namespace mnd.UI.Modules.Dashboard.MakinaPLC
{
    public partial class MakinaIsimListeSonuc
    {
        [JsonProperty("browseResults")]
        public List<MakinaIds> BrowseResults { get; set; }


        public MakinaIsimListeSonuc()
        {
            BrowseResults = new List<MakinaIds>();
        }
    }



    public class MakinaIds
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }


    public partial class MakinaDataSonuc
    {
        [JsonProperty("readResults")]
        public List<MakinaData> ReadResults { get; set; }


        public MakinaDataSonuc()
        {
            ReadResults = new List<MakinaData>();
        }
    }

    public partial class MakinaData : MyBindableBaseLite
    {
        public string MakinaKod { get; set; }

        private double v;
        private string ıd;

        [JsonProperty("id")]
        public string Id { get => ıd; set => SetProperty(ref ıd, value); }

        [JsonProperty("s")]
        public bool S { get; set; }

        [JsonProperty("r")]
        public string R { get; set; }

        [JsonProperty("v")]
        public double V { get => v; set => SetProperty(ref v, value); }

        [JsonProperty("t")]
        public long T { get; set; }
    }
}
