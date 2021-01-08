using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Utils
{
    public class MSW_DataFlowHost
    {
        private static MSW_DataFlowHost _instance;

        public static MSW_DataFlowHost Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MSW_DataFlowHost();
                }
                return _instance;
            }
        }
        public tblUser CurrentModifiedUser { get; set; }
        public tblCustomer CurrentModifiedCustomer { get; set; }
        public tblMedicine CurrentModifiedMedicine { get; set; }

        private MSW_DataFlowHost()
        {

        }


        
    }
}
