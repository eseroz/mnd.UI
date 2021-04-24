using mnd.Logic.Model;

namespace mnd.UI.Modules
{
    public class FiltreItemModel : MyBindableBase
    {
        private string _itemNameMy;
        private int _badgeValue;

        public string ItemNameMy { get => _itemNameMy; set => SetProperty(ref _itemNameMy, value); }
        public int BadgeValue { get => _badgeValue; set => SetProperty(ref _badgeValue, value); }
    }
}