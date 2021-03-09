using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.Utils
{
    public class PSW_DataFlowHost
    {
        private static PSW_DataFlowHost _instance;

        public static PSW_DataFlowHost Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PSW_DataFlowHost();
                }
                return _instance;
            }
        }

        private PSW_DataFlowHost()
        {

        }
    }
}
