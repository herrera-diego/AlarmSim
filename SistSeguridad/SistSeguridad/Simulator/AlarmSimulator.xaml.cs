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
using System.Windows.Shapes;

namespace SistSeguridad.Simulator
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class AlarmSimulator : Window
    {
        public AlarmSimulator()
        {
            InitializeComponent();
            Sensor0.Zona1.Visibility = Visibility.Hidden;
        }

        public event EventHandler<AlarmEventArgs> AlarmActivated;

        public event EventHandler BatteryAlert;

        private void OnAlarmActivated(object sender, AlarmEventArgs e)
        {
            AlarmActivated?.Invoke(this, e);
        }

        private void OnBattery(object sender, EventArgs e)
        {
            BatteryAlert?.Invoke(this, e);
        }

        public void ProcessCall(object sender, EventArgs e)
        {
            //CallInfo.CallInfo = 
        }
    }
}
