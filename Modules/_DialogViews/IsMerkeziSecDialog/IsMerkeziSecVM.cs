using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using mnd.Logic.BC_App;
using mnd.Logic.BC_App.Domain;
using mnd.UI.Helper;

namespace mnd.UI.Modules._DialogViews.IsMerkeziSecDialog
{
    public class IsMerkeziSecVm : MyDxViewModelBase
    {
        public string FormMenuAd { get; }
        public ObservableCollection<IsMerkezi> IsMerkezleri { get => isMerkezleri; set => SetProperty(ref isMerkezleri, value); }
        public ICurrentWindowService CurrentWindow => ServiceContainer.GetService<ICurrentWindowService>(ServiceSearchMode.LocalOnly);



        IsMerkeziRepository repo = new IsMerkeziRepository();
        private ObservableCollection<IsMerkezi> isMerkezleri;

        public DelegateCommand<IsMerkezi> SecCommand => new DelegateCommand<IsMerkezi>(OnSec, c => true);


        private void OnFormClosing()
        {
            Messenger.Default.Send<KayitSecildiEvent<IsMerkezi>>(null);
        }
        private void OnSec(IsMerkezi obj)
        {
            Messenger.Default.Send<KayitSecildiEvent<IsMerkezi>>(new KayitSecildiEvent<IsMerkezi>(obj));
            CurrentWindow.Close();

            
        }

        public IsMerkeziSecVm()
        {
            IsMerkezleri = repo.TumIsMerkezleriGetir();
        }


    }
}
