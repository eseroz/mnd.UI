﻿using System.Windows;

namespace mnd.UI.Modules.KaliteModule
{
    /// <summary>
    /// Interaction logic for KaliteBobinWindow.xaml
    /// </summary>
    public partial class KaliteBobinWindow : Window
    {
        public KaliteBobinWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}