﻿using SistSeguridad.Communications;
using SistSeguridad.DataHandling;
using SistSeguridad.InputOutput;
using SistSeguridad.Simulator;
using SistSeguridad.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace SistSeguridad.TaskScheduling
{
    public class Scheduler
    {
        private long PanicTimer;
        private long FireTimer;

        private bool ChangePass;
        private bool ConfirmPass;

        private bool BatteryControl;

        private bool AlarmTriggered;

        private int passCounter = 0;

        private System.Timers.Timer timer;

        public Scheduler()
        {
            SystemSoundPlayer = new AudioAlertHandler();
            SystemCallHandler = new CallHandler();
        }

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

        public AudioAlertHandler SystemSoundPlayer
        {
            get;set;
        }

        public CallHandler SystemCallHandler
        {
            get; set;
        }

        public event EventHandler<CallEventArgs> AlarmActivated;

        private void OnAlarmActivated()
        {
            if(SystemCallHandler.SharedMemory == null)
            {
                SystemCallHandler.SharedMemory = SystemMemory;
            }
            CallEventArgs eventArgs = new CallEventArgs();
            eventArgs.Information = SystemCallHandler.CallCompany();
            AlarmActivated?.Invoke(this, eventArgs);
        }

        private void OnAlarmDeactivated()
        {
           
            CallEventArgs eventArgs = new CallEventArgs();
            eventArgs.Information = new CallInfo();
            AlarmActivated?.Invoke(this, eventArgs);
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
        


        public string ChangePassword(string sequence)
        {
            string tempPass = null;
            string strRegex = @"^\d{4}$";
            Regex myRegex = new Regex(strRegex, RegexOptions.None);

            foreach (Match myMatch in myRegex.Matches(sequence))
            {
                if (myMatch.Success)
                {
                    tempPass = sequence;
                    break;
                }
            }

            return tempPass;
        }

        public void ProcessSensorAlarm(object sender, AlarmEventArgs e)
        {
            if ((Mode == "0") || (Mode == e.Zone.ToString()))
            {
                AlarmTriggered = true;

                if (e.Name == "Sensor 0")
                {
                    timer = new System.Timers.Timer(30000);

                    // Hook up the Elapsed event for the timer.
                    timer.Elapsed += OnTimedEvent;

                    timer.Enabled = true;

                    ExecUIMethod(SystemSoundPlayer.StartSmallAlert);

                }
                else
                {
                    SystemMemory.ActiveSensor = e.Name;
                    SystemMemory.ActiveZone = e.Zone.ToString();
                    ExecUIMethod(SystemSoundPlayer.ActivateAlarm);
                    ExecUIMethod(SystemDisplay.EnableAlarm);
                    ExecUIMethod(OnAlarmActivated);
                }
            }

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            ExecUIMethod(SystemDisplay.EnableAlarm);
            ExecUIMethod(SystemSoundPlayer.StopSmallAlert);
            ExecUIMethod(SystemSoundPlayer.ActivateAlarm);
            ExecUIMethod(OnAlarmActivated);
            AlarmTriggered = true;            
            timer.Stop();
            timer.Enabled = false;            
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

        private void ExecUIMethod(Action method)
        {
            SystemMemory.Updated = false;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                          new Action(() => method()));
        }

        public async void CheckButtonsAsync()
        {
            string tempPass = null;

            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);

                    if (SystemButtonPanel.Panic)
                    {
                        FireTimer = 0;
                        PanicTimer++;
                        if(PanicTimer == 20)
                        {
                            ExecUIMethod(SystemDisplay.EnableAlarm);
                            ExecUIMethod(SystemSoundPlayer.ActivateAlarm);                            
                            ExecUIMethod(SystemDisplay.DisableError);
                            AlarmTriggered = true;
                            SystemMemory.Panic = true;
                            SystemButtonPanel.Panic = false;
                            ExecUIMethod(OnAlarmActivated);
                        }
                    }
                    else if (SystemButtonPanel.Fire)
                    {
                        PanicTimer = 0;
                        FireTimer++;
                        if (FireTimer == 20)
                        {
                            ExecUIMethod(SystemDisplay.EnableAlarm);
                            ExecUIMethod(SystemSoundPlayer.ActivateAlarm);
                            ExecUIMethod(SystemDisplay.DisableError);
                            AlarmTriggered = true;
                            SystemMemory.Fire = true;
                            SystemButtonPanel.Fire = false;
                            ExecUIMethod(OnAlarmActivated);
                        }
                    }
                    else if(SystemButtonPanel.Escape)
                    {
                        ExecUIMethod(SystemDisplay.Clear);
                        ExecUIMethod(SystemDisplay.DisableError);
                        SystemMemory.Clear();
                        SystemButtonPanel.Escape = false;
                        SystemButtonPanel.Enter = false;
                        ChangePass = false;
                        ConfirmPass = false;
                        PanicTimer = 0;
                        FireTimer = 0;
                    }
                    else if (SystemButtonPanel.Enter)
                    {
                        SystemButtonPanel.Enter = false;
                        ExecUIMethod(SystemDisplay.DisableError);
                        if (AlarmTriggered)
                        {
                            if (ValidatePassword(SystemMemory.CurrentMessage))
                            {
                                ExecUIMethod(SystemDisplay.DisableAlarm);
                                ExecUIMethod(SystemSoundPlayer.DeactivateAlarm);
                                ExecUIMethod(ArmedIndicator.LedOff);
                                ExecUIMethod(SystemDisplay.DisableArmed);
                                SystemMemory.ClearAlarms();
                                ExecUIMethod(OnAlarmDeactivated);

                                AlarmTriggered = false;    
                                
                                if(timer != null)
                                {
                                    timer.Stop();
                                    timer.Enabled = false;
                                }
                            }
                            else
                            {
                                ExecUIMethod(SystemDisplay.EnableError);
                            }

                            ExecUIMethod(SystemDisplay.Clear);
                        }
                        else if (ChangePass)
                        {
                            if (!ConfirmPass)
                            {
                                tempPass = ChangePassword(SystemMemory.CurrentMessage);
                                ExecUIMethod(SystemDisplay.Clear);

                                if (string.IsNullOrEmpty(tempPass))
                                {
                                    ChangePass = false;                                   
                                    ExecUIMethod(SystemDisplay.EnableError);
                                }
                                else
                                {
                                    ChangePass = true;
                                    ConfirmPass = true;
                                }
                            }
                            else
                            {
                                if(tempPass == SystemMemory.CurrentMessage)
                                {
                                    SystemMemory.Password = tempPass;
                                    ExecUIMethod(SystemDisplay.Clear);
                                }
                                else
                                {
                                    ExecUIMethod(SystemDisplay.Clear);
                                    ExecUIMethod(SystemDisplay.EnableError);
                                }

                                ChangePass = false;
                                ConfirmPass = false;
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
                                SystemMemory.Clear();
                            }
                            else
                            {
                                ExecUIMethod(SystemDisplay.Clear);
                            }
                            
                            SystemMemory.Clear();
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
                                passCounter = 0;
                            }
                            else
                            {
                                ExecUIMethod(SystemDisplay.EnableError);
                                passCounter++;
                                if (passCounter>=3)
                                {
                                    passCounter = 0;
                                    ExecUIMethod(SystemDisplay.EnableAlarm);
                                    ExecUIMethod(SystemSoundPlayer.ActivateAlarm);
                                    ExecUIMethod(SystemDisplay.DisableError);
                                    AlarmTriggered = true;
                                    SystemMemory.ClearAlarms();
                                    SystemMemory.Panic = false;
                                    SystemButtonPanel.Panic = false;
                                    ExecUIMethod(OnAlarmActivated);
                                }
                            }

                            SystemMemory.Clear();
                            ExecUIMethod(SystemDisplay.Clear);
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
