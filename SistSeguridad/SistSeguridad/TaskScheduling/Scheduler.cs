using SistSeguridad.DataHandling;
using SistSeguridad.Simulator;
using SistSeguridad.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SistSeguridad.TaskScheduling
{
    public class Scheduler
    {

        public string Mode
        {
            get;set;
        }
        public Memory SystemMemory
        {
            get;set;
        }

        public LedControl BatteryIndicator
        {
            get; set;
        }

        public LedControl ArmedIndicator
        {
            get; set;
        }

        public ButtonPanel SystemButtonPanel
        {
            get; set;
        }

        public Display SystemDisplay
        {
            get; set;
        }

        public bool ValidateArmedSequence(string sequence)
        {
            string strRegex = @"^\d{4}\*";
            Regex myRegex = new Regex(strRegex, RegexOptions.None);

            foreach (Match myMatch in myRegex.Matches(sequence))
            {
                if (myMatch.Success)
                {
                    return true;
                }
            }

            return false;
        }  

        public void Disarm()
        {

        }
        


        public void ChangePassword(string sequence)
        {

        }

        public delegate void SensorAlarm(Sensor sensor);
        public delegate void FireAlarm();
        public delegate void BatteryAlarm();
        public delegate void PanicAlarm();
        public delegate void DamageAlarm();

        public event SensorAlarm SensorEvent;     
        public event BatteryAlarm BatteryEvent;       
        public event PanicAlarm PanicEvent;     
        public event FireAlarm FireEvent;
        public event DamageAlarm DamageEvent;

        public void ProcessSensorAlarm(Sensor sensor)
        {

        }

        public  void ProcessBatteryAlarm()
        {

        }

        public void ProcessFireAlarm()
        {

        }
        public void ProcessPanicAlarm()
        {

        }

        public void ProcessDamageAlarm()
        {

        }

        public void StartTimer(int time)
        {

        }

        private void ExecUIMethod(Action method)
        {
            SystemMemory.Updated = false;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                          new Action(() => method()));
        }

        public async Task CheckButtonsAsync()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);

                    if(SystemButtonPanel.Escape)
                    {
                        ExecUIMethod(SystemDisplay.Clear);
                        SystemMemory.Clear();
                        SystemButtonPanel.Escape = false;
                    }
                    else if (SystemButtonPanel.Enter)
                    {
                        if (ValidateArmedSequence(SystemMemory.CurrentMessage))
                        {
                            ExecUIMethod(ArmedIndicator.LedOn);
                        }
                        SystemMemory.Clear();
                        SystemButtonPanel.Enter = false;
                    }
                    else if (SystemButtonPanel.ButtonPressed)
                    {                                                                 
                        ExecUIMethod(SystemDisplay.Show);

                        SystemButtonPanel.ButtonPressed = false;
                    }

                }
                
            });

           
        }
    }
}
