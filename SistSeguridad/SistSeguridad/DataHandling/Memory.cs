using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistSeguridad.DataHandling
{
    public class Memory
    {
        public Memory()
        {
            Password = "1234";
        }

        public bool Updated
        {
            get; set;
        }

        public string CurrentMessage
        {
            get;
            set;
        }

        public string UserNumber
        {
            set; get;
        }

        public string CompanyPhoneNumber
        {
            get;set;
        }

        public bool Fire
        {
            get;set;
        }

        public bool Panic
        {
            get;set;
        }

        public string ActiveSensor
        {
            get; set;
        }

        public string ActiveZone
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string DefaultPassword
        {
            get;
            set;
        }
        public void Clear()
        {
            CurrentMessage = string.Empty;
            Updated = false;
        }

        public void ClearAlarms()
        {
            ActiveSensor = string.Empty;
            ActiveZone = string.Empty;
            Panic = false;
            Fire = false;
        }
    }
}
