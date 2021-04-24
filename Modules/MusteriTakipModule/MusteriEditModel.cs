using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Pdf;
using mnd.Common;
using mnd.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.MusteriTakipModule
{
    public class MusteriEditModel : BindableBase 
    {
        private string plasiyerKod;

        public string Tel { get => GetValue<string>(); set => SetValue(value); }
        public string Adres { get => GetValue<string>(); set => SetValue(value); }
        public string KullanimAlanTipKod { get => GetValue<string>(); set => SetValue(value); }
        public int? YillikTonaj { get => GetValue<int?>(); set => SetValue(value); }
        public string PlasiyerKod { get => GetValue<string>(); set => SetValue(value); }
        public string AgentId { get => GetValue<string>(); set => SetValue(value); }
        public string SahaSorumlusuId { get => GetValue<string>(); set => SetValue(value); }

        public string KontratDonemTip { get => GetValue<string>(); set => SetValue(value); }

        public string Email { get => GetValue<string>(); set => SetValue(value); }
        public string Web { get => GetValue<string>(); set => SetValue(value); }
        public string CariSevkAdres { get => GetValue<string>(); set => SetValue(value); }

        public string KontratDonemDeger
        {
            get => GetValue<string>(); set
            {
                SetValue(value);
            }
        }


        public string Durumu { get => GetValue<string>(); set => SetValue(value); }


        public string KontratDonemDeger_Cegrek1 { get => GetValue<string>(); set => SetValue(value); }
        public string KontratDonemDeger_Cegrek2 { get => GetValue<string>(); set => SetValue(value); }
        public string KontratDonemDeger_Cegrek3 { get => GetValue<string>(); set => SetValue(value); }
        public string KontratDonemDeger_Cegrek4 { get => GetValue<string>(); set => SetValue(value); }
        public string KontratDonemDeger_Cegrek_Tum => String.Join(";", KontratDonemDeger_Cegrek1, KontratDonemDeger_Cegrek2, KontratDonemDeger_Cegrek3, KontratDonemDeger_Cegrek4);


        public string KontratDonemBirlesik => KontratDonemTip + "-" + KontratDonemDeger;

        public string FirmaGrupTip { get => GetValue<string>(); set => SetValue(value); }
    }
}
