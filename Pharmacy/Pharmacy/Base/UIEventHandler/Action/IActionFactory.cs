using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface IActionFactory
    {
        IAction CreateAction(object obj);
    }
}
