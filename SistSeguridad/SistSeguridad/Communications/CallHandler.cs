using SistSeguridad.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistSeguridad.Communications
{
    public class CallHandler
    {
        public Memory SharedMemory
        {
            get;set;
        }

        public CallInfo CallCompany()
        {
            CallInfo information = new CallInfo();

            information.CompanyPhoneNumber = SharedMemory.CompanyPhoneNumber;
            information.UserNumber = SharedMemory.UserNumber;
            information.Panic = SharedMemory.Panic;
            information.Fire = SharedMemory.Fire;
            information.SensorName = SharedMemory.ActiveSensor;
            information.Zone = SharedMemory.ActiveZone;

            return information;
        }
    }

    public class CallInfo
    {
        public string UserNumber
        {
            set; get;
        }

        public string CompanyPhoneNumber
        {
            get; set;
        }

        public bool Fire
        {
            get; set;
        }

        public bool Panic
        {
            get; set;
        }

        public string SensorName
        {
            get; set;
        }

        public string Zone
        {
            get; set;
        }
    }
}
