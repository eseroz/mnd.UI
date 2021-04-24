using mnd.Logic.Model;
using System.Collections.Generic;

namespace mnd.UI
{

    public class MenuItem : MyBindableBase
    {

        public virtual string Name { get; set; }
        public virtual string Icon { get; set; }
        public string ViewModelName { get; set; }
        public string ViewName { get; set; }
        public string ParameterObj { get; internal set; }
        private int deger;
        private bool? ısBadge;

        public int Deger
        {
            get => deger;
            set => SetProperty(ref deger, value);
        }

        public bool? IsBadge { get => ısBadge; set => SetProperty(ref ısBadge, value); }
        public decimal MenuId { get; set; }
        public string Caption { get; set; }

        public string YetkiliRoller { get; set; }



        public List<MenuItem> SubItems { get; set; }
    }
}
