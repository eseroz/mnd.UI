using DevExpress.Mvvm;
using mnd.UI.Modules.TeklifModule.MessangerEvents;
using mnd.UI.Modules.TeklifModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.TeklifModule
{


    public class LookUpItem
    {
        private string aciklama;

        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Aciklama
        {
            get
            {
                if (IsEN == true) return Aciklama_EN;
                return aciklama;
            }

            set
            {
                aciklama = value;
            }
        }
        public string Aciklama_EN { get;  set; }

        public bool IsEN { get; set; }
    }
}
