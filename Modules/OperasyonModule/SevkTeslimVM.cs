using mnd.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.OperasyonModule
{
    public class SevkTeslimVM : MyBindableBase
    {
        private DateTime teslimTarihi;
        public DateTime TeslimTarihi { get => teslimTarihi; set => SetProperty(ref teslimTarihi, value); }

    }
}
