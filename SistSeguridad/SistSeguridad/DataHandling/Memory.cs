using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistSeguridad.DataHandling
{
    public class Memory
    {
        public string CurrentMessage
        {
            get;
            set;
        }

        public bool Updated
        {
            get;set;
        }

        public void Clear()
        {
            CurrentMessage = string.Empty;
            Updated = false;
        }
    }
}
