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

namespace SistSeguridad.Simulator
{
    /// <summary>
    /// Interaction logic for Bateria.xaml
    /// </summary>
    public partial class Bateria : UserControl
    {
        public Bateria()
        {
            InitializeComponent();
        }

        public event EventHandler BatteryAlert;

        private void OnBattery(object sender, RoutedEventArgs e)
        {
            BatteryAlert?.Invoke(this, new EventArgs());
        }
    }
}
