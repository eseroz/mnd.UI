using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mnd.UI.Modules._SatisModule.MusteriAramalar
{
    /// <summary>
    /// Interaction logic for PTD_AramaEditView.xaml
    /// </summary>
    public partial class PTD_AramaEditView : UserControl
    {
        public PTD_AramaEditView()
        {
            PTD_AramaEditVM vm = new PTD_AramaEditVM(0);
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
