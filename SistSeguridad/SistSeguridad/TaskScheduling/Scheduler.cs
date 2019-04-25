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
        private long PanicTimer;
        private long FireTimer;

        private bool ChangePass;

        private bool BatteryControl;

        private bool AlarmTriggered;

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
            if (string.Compare(sequence, SystemMemory.Password + "*") == 0)
            {
                Mode = "0";
                return true;
            }
            else if(string.Compare(sequence, SystemMemory.Password + "#") == 0)
            {
                Mode = "1";
                return true;
            }
            else if (string.Compare(sequence, SystemMemory.Password + "*#") == 0)
            {
                Mode = null;
                ChangePass = true;
                return false;
            }
            else
            {
                Mode = null;
                return false;
            }
        }

        public bool ValidatePassword(string sequence)
        {
            if (string.Compare(sequence, SystemMemory.Password) == 0)
            {
                Mode = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Disarm()
        {

        }
        


        public void ChangePassword(string sequence)
        {
            string strRegex = @"^\d{4}$";
            Regex myRegex = new Regex(strRegex, RegexOptions.None);

            foreach (Match myMatch in myRegex.Matches(sequence))
            {
                if (myMatch.Success)
                {
                    SystemMemory.Password = sequence;
                }
            }
        }

        public void ProcessSensorAlarm(object sender, AlarmEventArgs e)
        {
            if ((Mode == "0") || (Mode == e.Zone.ToString()))
            {
                ExecUIMethod(SystemDisplay.EnableAlarm);
                AlarmTriggered = true;
            }

        }

        public  void ProcessBatteryAlarm(object sender, EventArgs e)
        {
            BatteryControl = !BatteryControl;
            if (BatteryControl)
            {
                ExecUIMethod(BatteryIndicator.LedOn);
                ExecUIMethod(SystemDisplay.EnableBattery);
            }
            else
            {
                ExecUIMethod(BatteryIndicator.LedOff);
                ExecUIMethod(SystemDisplay.DisableBattery);
            }
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

        public async void CheckButtonsAsync()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);

                    if (SystemButtonPanel.Panic)
                    {
                        FireTimer = 0;
                        PanicTimer++;
                        if(PanicTimer >= 20)
                        {
                            ExecUIMethod(SystemDisplay.EnableAlarm);
                            AlarmTriggered = true;
                        }
                    }
                    else if (SystemButtonPanel.Fire)
                    {
                        PanicTimer = 0;
                        FireTimer++;
                        if (FireTimer >= 20)
                        {
                            ExecUIMethod(SystemDisplay.EnableAlarm);
                            AlarmTriggered = true;
                        }
                    }
                    else if(SystemButtonPanel.Escape)
                    {
                        ExecUIMethod(SystemDisplay.Clear);
                        SystemMemory.Clear();
                        SystemButtonPanel.Escape = false;
                        PanicTimer = 0;
                        FireTimer = 0;
                    }
                    else if (SystemButtonPanel.Enter)
                    {
                        if(ChangePass)
                        {
                            ChangePassword(SystemMemory.CurrentMessage);
                            ChangePass = false;
                        }
                        else if(AlarmTriggered)
                        {
                            if (ValidatePassword(SystemMemory.CurrentMessage))
                            {                                
                                ExecUIMethod(SystemDisplay.DisableAlarm);
                                AlarmTriggered = false;
                            }
                        }
                        else if (string.IsNullOrEmpty(Mode))
                        {
                            if (ValidateArmedSequence(SystemMemory.CurrentMessage))
                            {
                                ExecUIMethod(ArmedIndicator.LedOn);
                                ExecUIMethod(SystemDisplay.EnableArmed);
                                SystemMemory.CurrentMessage = "Modo " + Mode;
                                ExecUIMethod(SystemDisplay.Show);
                                SystemMemory.CurrentMessage = "";
                            }
                            else
                            {
                                ExecUIMethod(SystemDisplay.Clear);
                            }
                            
                            SystemMemory.Clear();
                            SystemButtonPanel.Enter = false;
                            PanicTimer = 0;
                            FireTimer = 0;
                        }
                        else
                        {                           
                            if (ValidatePassword(SystemMemory.CurrentMessage))
                            {
                                ExecUIMethod(ArmedIndicator.LedOff);
                                ExecUIMethod(SystemDisplay.DisableArmed);
                                ExecUIMethod(SystemDisplay.DisableAlarm);
                                AlarmTriggered = false;
                            }

                            SystemMemory.Clear();
                            SystemButtonPanel.Enter = false;
                            ExecUIMethod(SystemDisplay.Clear);
                            SystemMemory.CurrentMessage = "";
                            PanicTimer = 0;
                            FireTimer = 0;
                        }
                    }
                    else if (SystemButtonPanel.ButtonPressed)
                    {                                                                 
                        ExecUIMethod(SystemDisplay.Show);

                        SystemButtonPanel.ButtonPressed = false;
                        PanicTimer = 0;
                        FireTimer = 0;
                    }

                }
                
            });

           
        }
    }
}
