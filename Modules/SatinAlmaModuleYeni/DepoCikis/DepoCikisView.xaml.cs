using System.Windows.Controls;
using System.Windows.Input;

namespace mnd.UI.Modules.SatinAlmaModuleYeni.DepoCikis
{
    /// <summary>
    /// Interaction logic for DepoCikisView.xaml
    /// </summary>
    public partial class DepoCikisView : UserControl
    {
        public DepoCikisView()
        {
            InitializeComponent();
        }

        private void TxtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                KeyEventArgs e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource,
                                  0, Key.Tab);
                e1.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(e1);
            }
        }
    }
}
