using System;
using System.Windows;

namespace mnd.UI.Modules.PaketlemeModule
{
    public partial class KantarSonucWindow : Window
    {
        KantarSerialPort kantar;
        public KantarSonucWindow(KantarSerialPort kantarPort)
        {
            InitializeComponent();
            kantar = kantarPort;
            kantar.pandap_dataReceiveEvent += K_dataReceiveEvent;
        }

        public KantarSerialPort KantarPort { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void K_dataReceiveEvent(string data)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                decimal dtonaj = decimal.Parse(data.ToString()) / 10;

                if (AppPandap.AktifKullanici.KullaniciId == "paketleme2") // ikinci kantar hesabı
                    dtonaj = dtonaj * 10;

                int i_tonaj_sonuc = Convert.ToInt32(Math.Round(dtonaj, 0, MidpointRounding.AwayFromZero));

                txtKantarSonuc.Text = i_tonaj_sonuc.ToString();
            }));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
