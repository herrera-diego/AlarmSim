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
using SistSeguridad.TaskScheduling;
using SistSeguridad.UserControls;
using SistSeguridad.DataHandling;
using System.Threading;
using SistSeguridad.Simulator;

namespace SistSeguridad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Scheduler MainScheduler;
        private Memory MainMemory;
        private AlarmSimulator Simulator;

        public MainWindow()
        {
            InitializeComponent();

            MainScheduler = new Scheduler();
            MainMemory = new Memory();

            MainScheduler.SystemMemory = MainMemory;
            SystemButtonPanel.SharedMemory = MainMemory;
            SystemDisplay.SharedMemory = MainMemory;

            MainScheduler.SystemButtonPanel = SystemButtonPanel;
            MainScheduler.SystemDisplay = SystemDisplay;
            MainScheduler.ArmedIndicator = ArmadaLED;
            MainScheduler.BatteryIndicator = BateriaLED;

            MainScheduler.CheckButtonsAsync();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.S:
                        if ((Simulator == null) || (!Simulator.IsLoaded))
                        {
                            Simulator = new AlarmSimulator();
                            Simulator.AlarmActivated += MainScheduler.ProcessSensorAlarm;
                            Simulator.BatteryAlert += MainScheduler.ProcessBatteryAlarm;
                            MainScheduler.AlarmActivated += Simulator.ProcessCall;
                            Simulator.Show();
                        }
                        break;
                    // Other cases ...
                    default:
                        break;
                }
            }
            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Focus();
        }
    }
}
