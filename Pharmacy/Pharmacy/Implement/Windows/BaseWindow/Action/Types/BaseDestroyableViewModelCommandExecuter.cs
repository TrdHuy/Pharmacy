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
        public BaseDestroyableViewModelCommandExecuter(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger)
        {
        }

        public override void ExecuteAlternativeCommand()
        {
        }

        public override void ExecuteCommand()
        {
        }

        protected override void ExecuteOnDestroy()
        {
            // set flag IsCancle to true
            base.ExecuteOnDestroy();
        }

        public override void SetCompleteFlagAfterExecuteCommand()
        {
            IsCompleted = true;
        }
    }
}
