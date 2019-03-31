using SistSeguridad.DataHandling;
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

        public bool Mode0Sequence(string sequence)
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

        public bool Mode1Sequence(string sequence)
        {
            string strRegex = @"^\d{4}\#";
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
                        ExecUIMethod(SystemDisplay.ClearDisplay);
                        SystemMemory.Clear();
                        SystemButtonPanel.Escape = false;
                    }
                    else if (SystemButtonPanel.Enter)
                    {
                        if (Mode0Sequence(SystemMemory.CurrentMessage)|| Mode0Sequence(SystemMemory.CurrentMessage))
                        {
                            ExecUIMethod(ArmedIndicator.LedOn);
                        }
                        SystemMemory.Clear();
                        SystemButtonPanel.Enter = false;
                    }
                    else if (SystemButtonPanel.ButtonPressed)
                    {                                                                 
                        ExecUIMethod(SystemDisplay.ShowOnDisplay);

                        SystemButtonPanel.ButtonPressed = false;
                    }

                }
                
            });

           
        }
    }
}
