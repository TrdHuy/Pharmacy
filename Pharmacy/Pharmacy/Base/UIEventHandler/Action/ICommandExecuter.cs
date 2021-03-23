using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.UIEventHandler.Action
{
    interface ICommandExecuter : IAction
    {
        /// <summary>
        /// Kiểm tra liệu lệnh này có thể thực thi hay không 
        /// </summary>
        bool CanExecute(object[] dataTransfer);

    }
}
