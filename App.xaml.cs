using AutoMapper;
using DevExpress.Xpf.Core;
using Microsoft.Extensions.Logging;
using mnd.Logic.BC_MusteriTakip.Domain;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.BC_Satis._Teklif;
using mnd.Logic.Model.Operasyon;
using mnd.Logic.Model.Satis;
using mnd.UI.Modules.TeklifModule.Models;
using mnd.UI.UI_TEST;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace mnd.UI
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("tr-TR");

            var v = CultureInfo.CurrentCulture.NumberFormat;



            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SiparisKalem, SiparisKalem>();
                cfg.CreateMap<SevkiyatEmri, SevkiyatEmri>();
                cfg.CreateMap<Irsaliye, Irsaliye>();
                cfg.CreateMap<IrsaliyePalet, IrsaliyePalet>();

                cfg.CreateMap<Gorusme, Gorusme>();
                cfg.CreateMap<GorusmeTip, GorusmeTip>();
                cfg.CreateMap<GorusmeKonuTip, GorusmeKonuTip>();

                cfg.CreateMap<Teklif, TeklifListModel>().ReverseMap();

                cfg.CreateMap<Teklif, TeklifEditModel>().ReverseMap();

                cfg.CreateMap<TeklifKalem, TeklifKalemEditModel>().ReverseMap();

                cfg.CreateMap<TeklifKalemEditModel, TeklifKalemEditModel>().ReverseMap();

            });


            Application.Current.Exit += Current_Exit;




        }


        private void Current_Exit(object sender, ExitEventArgs e)
        {

        }

        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {

            Debug.WriteLine("Yüklenen dll : " + DateTime.Now.ToString() + args.LoadedAssembly.FullName);

        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage("tr-TR")));

            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);

            await ThemeManager.PreloadThemeResourceAsync("Office2019Colorful");

            MainWindow startWindow = new MainWindow();
            startWindow.Show();


        }

        public void TestStart()
        {
            TestForm f = new TestForm();
            f.Show();
        }



        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
        }
    }
}