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
    internal class BaseViewModelCommandExecuter : AbstractViewModelCommandExecuter
    {
        public BaseViewModelCommandExecuter(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger)
        {
        }
        public BaseViewModelCommandExecuter(string actionName, string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionName, actionID, builderID, viewModel, logger)
        {
        }

        protected override bool CanExecute(object dataTransfer)
        {
            return true;
        }

        protected override void ExecuteCommand()
        {
        }

        protected override void ExecuteAlternativeCommand()
        {
        }

        protected override void SetCompleteFlagAfterExecuteCommand()
        {
            IsCompleted = true;
        }

        protected override void ExecuteOnDestroy()
        {
        }

        protected override void ExecuteOnCancel()
        {
        }
    }
}