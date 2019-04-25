﻿using SistSeguridad.DataHandling;
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

                    if (SystemButtonPanel.Panic)
                    {
                        PanicTimer++;
                        if(PanicTimer >= 20)
                        {
                            ExecUIMethod(SystemDisplay.EnableAlarm);
                        }
                    }
                    else if (SystemButtonPanel.Fire)
                    {
                        PanicTimer = 0;
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
                        if (string.IsNullOrEmpty(Mode))
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
