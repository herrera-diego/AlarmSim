using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SistSeguridad.InputOutput
{
    public class AudioAlertHandler
    {
        private SoundPlayer AlertPlayer = new SoundPlayer();
        private SoundPlayer AlarmPlayer = new SoundPlayer(); 
        private readonly string AlarmFile = AppDomain.CurrentDomain.BaseDirectory
                                            + @"InputOutput\AudioFiles\AlarmSound.wav";
        private readonly string AlertFile = AppDomain.CurrentDomain.BaseDirectory
                                            + @"InputOutput\AudioFiles\AlertSound.wav";

        public AudioAlertHandler()
        {           
            AlertPlayer.SoundLocation = AlertFile;
            AlarmPlayer.SoundLocation = AlarmFile;
        }

        public void ActivateAlarm()
        {
            AlarmPlayer.PlayLooping();
        }

        public void DeactivateAlarm()
        {
            AlarmPlayer.Stop();
        }

        public void StartSmallAlert()
        {
            AlertPlayer.PlayLooping();
        }

        public void StopSmallAlert()
        {
            AlertPlayer.Stop();
        }
    }
}
