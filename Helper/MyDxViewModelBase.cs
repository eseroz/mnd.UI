using DevExpress.Mvvm;
using mnd.Logic.Model;
using mnd.Logic.Model.App;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Helper
{
    public abstract class MyDxViewModelBase : MyBindableBase, ISupportServices, ISupportParentViewModel
    {
        public ObservableCollection<FormKomutYetki> FormPermissions { get; set; }

        public string FormMenuAd { get; set; }

        private IServiceContainer serviceContainer = null;

        public IServiceContainer ServiceContainer
        {
            get
            {
                if (serviceContainer == null)
                    serviceContainer = new ServiceContainer(this);
                return serviceContainer;
            }
        }

        IServiceContainer ISupportServices.ServiceContainer { get { return ServiceContainer; } }
        object ISupportParentViewModel.ParentViewModel { get; set; }

        public IMessageBoxService MessageBoxService
        {
            get { return ServiceContainer.GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); }
        }

        public IMessenger MessangerBoxService
        {
            get { return ServiceContainer.GetService<IMessenger>(ServiceSearchMode.PreferParents); }
        }

        public IDialogService DialogService
        {
            get { return ServiceContainer.GetService<IDialogService>(ServiceSearchMode.PreferParents); }
        }

        public ICurrentWindowService WindowService
        {
            get { return ServiceContainer.GetService<ICurrentWindowService>(ServiceSearchMode.PreferParents); }
        }


        public IExportService ExportService
        {
            get => ServiceContainer.GetService<IExportService>(ServiceSearchMode.PreferParents);
        }

        protected bool YetkiliMi_FromDb(string komut)
        {
            var yetki = FormPermissions.Any(c => c.Komut == komut);
            return yetki;
        }
    }
}