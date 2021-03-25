using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Types
{
    internal class BaseDestroyableViewModelCommandExecuter : AbstractDestroyableViewModelCommandExecuter
    {
        public BaseDestroyableViewModelCommandExecuter(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
        }
    }
}
