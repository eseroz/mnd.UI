using DevExpress.Mvvm;
using mnd.Logic.Helper;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using mnd.UI.Modules;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace mnd.UI.AppModules.AppModule
{
    public class MenuYetkiViewModel : MyDxViewModelBase, IForm
    {
        private ObservableCollection<FormKomutYetki> _navCommandList;

        private ObservableCollection<FormMenuItem> _navMenuList;

        private FormMenuItem _seciliNavMenu;
        private readonly UnitOfWork _uow = new UnitOfWork();

        public MenuYetkiViewModel(string menuFormAd)
        {
            FormMenuAd = menuFormAd;
        }

        private void AltNewItemRowUpdated(FormKomutYetki obj)
        {
            obj.FormAd = SeciliNavMenu.FormAd;
            _uow.AppRepo.CommandYetkiEkle(obj);
            _uow.Commit();
        }

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void OnKaydet(object obj)
        {
            _uow.Commit();
        }

        private void UstNewItemRowUpdated(FormMenuItem obj)
        {
            _uow.NavMenuRepo.Ekle(obj);
            _uow.Commit();
        }

        public void Load()
        {
            NavMenuList = _uow.NavMenuRepo.NavMenuItemGetir();

            foreach (var nm in NavMenuList)
            {
                if (nm.VM_Name != null) YetkiCommandsSaveDb(nm);
            }

            _uow.Commit();
        }

        public void YetkiCommandsSaveDb(FormMenuItem menuItem)
        {
            var formCommandListe = new ObservableCollection<FormKomutYetki>();

            var allDbCommandList = _uow.AppRepo.FormPermissions(menuItem.FormAd);


            var t = Type.GetType("mnd.UI." + menuItem.VM_Name);


            PropertyInfo[] pList = t.GetProperties();

            foreach (var p in pList)
            {
                if (p.GetCustomAttributes(typeof(YetkiKontrolAttribute)).Any())
                    formCommandListe.Add(new FormKomutYetki { Komut = p.Name, FormAd = menuItem.FormAd, YetkiliRoller = "-" });
            }

            foreach (var item in formCommandListe)
            {
                var varMi = allDbCommandList.Any(c => c.FormAd == item.FormAd && c.Komut == item.Komut);
                if (!varMi) _uow.AppRepo.CommandYetkiEkle(item);
            }

            //db de fazlalık olan yetkileri sil
            foreach (var dbCommandItem in allDbCommandList)
            {
                var varMi = formCommandListe.Any(c => c.FormAd == dbCommandItem.FormAd && c.Komut == dbCommandItem.Komut);
                if (!varMi) _uow.AppRepo.CommandYetkiSil(dbCommandItem);
            }
        }

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public ObservableCollection<FormKomutYetki> NavCommandList
        {
            get => _navCommandList;
            set => SetProperty(ref _navCommandList, value);
        }

        public ObservableCollection<FormMenuItem> NavMenuList
        {
            get => _navMenuList;
            set => SetProperty(ref _navMenuList, value);
        }

        public DelegateCommand<FormMenuItem> NewItemAddedCommand1 => new DelegateCommand<FormMenuItem>(UstNewItemRowUpdated, true);

        public DelegateCommand<FormKomutYetki> NewItemAddedCommand2 => new DelegateCommand<FormKomutYetki>(AltNewItemRowUpdated, c => true);

        public FormMenuItem SeciliNavMenu
        {
            get => _seciliNavMenu;
            set
            {
                SetProperty(ref _seciliNavMenu, value);

                NavCommandList = _uow.AppRepo.AllFormPermissions()
                    .Where(c => c.FormAd == _seciliNavMenu?.FormAd)
                    .OrderBy(c => c.Komut)
                    .ToObservableCollection();
            }
        }
    }
}