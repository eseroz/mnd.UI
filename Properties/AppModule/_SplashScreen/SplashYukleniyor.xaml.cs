using System;
using System.Windows;
using DevExpress.Xpf.Core;

namespace Pandap.UI.AppModule._SplashScreen
{
    /// <summary>
    /// Interaction logic for SplashYukleniyor.xaml
    /// </summary>
    public partial class SplashYukleniyor : Window, ISplashScreen
    {
        public SplashYukleniyor()
        {
            InitializeComponent();
        }

        #region ISplashScreen

        public void Progress(double value)
        {
        }

        public void CloseSplashScreen()
        {
            this.Close();
        }

        public void SetProgressState(bool isIndeterminate)
        {
        }

        #endregion ISplashScreen

        #region Event Handlers

        private void OnAnimationCompleted(object sender, EventArgs e)
        {
        }

        #endregion Event Handlers
    }
}