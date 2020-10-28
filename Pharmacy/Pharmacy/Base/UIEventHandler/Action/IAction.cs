using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.UIEventHandler.Action
{
    interface IAction
    {
        /// <summary>
        /// Triển khai action cho 1 đối tượng  được định nghĩa trước
        /// </summary>
        bool Execute(object[] dataTransfer);
    }
}
