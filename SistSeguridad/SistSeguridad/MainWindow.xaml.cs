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

namespace SistSeguridad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Scheduler MainScheduler = new Scheduler();
            Memory MainMemory = new Memory();

            MainScheduler.SystemMemory = MainMemory;
            SystemButtonPanel.SharedMemory = MainMemory;
            SystemDisplay.SharedMemory = MainMemory;

            MainScheduler.SystemButtonPanel = SystemButtonPanel;
            MainScheduler.SystemDisplay = SystemDisplay;
            MainScheduler.ArmedIndicator = ArmadaLED;
            MainScheduler.BatteryIndicator = BateriaLED;

            MainScheduler.CheckButtonsAsync();
            //polling.Wait();
        }
    }
}
