using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Utils
{
    public class MSW_UserDataManager
    {
        private static MSW_UserDataManager _instance;
        
        public static MSW_UserDataManager Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MSW_UserDataManager();
                }
                return _instance;
            }
        }

    }
}
