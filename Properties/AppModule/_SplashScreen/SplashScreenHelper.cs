using DevExpress.Xpf.Core;

namespace Pandap.UI.AppModule._SplashScreen
{
    public class SplashScreenHelper
    {
        // birden fazla oluşturulması engellenmiş
        // contractur yasaklanmış
        // singleton pattern

        private static SplashScreenHelper instance = null;

        public static SplashScreenHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new SplashScreenHelper();

                return instance;
            }
        }

        private SplashScreenHelper()
        { }

        private WaitIndicator indicator = new WaitIndicator();

        public void ShowSplashScreen()
        {
            if (!DevExpress.Xpf.Core.DXSplashScreen.IsActive)
            {
                DevExpress.Xpf.Core.DXSplashScreen.Show<SplashScreenWindow1>();
            }
        }

        public void ShowLoadingScreen()
        {
            if (!DevExpress.Xpf.Core.DXSplashScreen.IsActive)
                DevExpress.Xpf.Core.DXSplashScreen.Show<SplashYukleniyor>();
        }

        public void HideSplashScreen()
        {
            if (DevExpress.Xpf.Core.DXSplashScreen.IsActive)
                DevExpress.Xpf.Core.DXSplashScreen.Close();
        }
    }
}