using System;
using System.Windows;
using DevExpress.Xpf.Core;

namespace Pandap.UI.AppModule._SplashScreen
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow1.xaml
    /// </summary>
    public partial class SplashScreenWindow1 : Window, ISplashScreen
    {
        public SplashScreenWindow1()
        {
            InitializeComponent();
            this.board.Completed += OnAnimationCompleted;
        }

        #region ISplashScreen

        public void Progress(double value)
        {
            progressBar.Value = value;
        }

        public void CloseSplashScreen()
        {
            this.board.Begin(this);
        }

        public void SetProgressState(bool isIndeterminate)
        {
            progressBar.IsIndeterminate = isIndeterminate;
        }

        #endregion ISplashScreen

        #region Event Handlers

        private void OnAnimationCompleted(object sender, EventArgs e)
        {
            this.board.Completed -= OnAnimationCompleted;
            this.Close();
        }

        #endregion Event Handlers
    }
}