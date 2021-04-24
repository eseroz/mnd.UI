using DevExpress.Mvvm;

namespace mnd.UI.GyModules.MesajModule
{
    public class AppMesaj
    {
        public static DelegateCommand<object> GyMesajAcCommand => new DelegateCommand<object>(MesajFormAc);


        public static void MesajFormAc(object row)
        {
            var w = new MesajlasmaWindow
            {
                DataContext = new MesajlasmaViewModel(row, AppPandap.AktifKullanici.KullaniciId)


            };

            w.Owner = AppPandap.MainWindow;
            w.Show();
        }

    }
}
