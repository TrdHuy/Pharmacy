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

        public override void ExecuteCommand()
        {
        }

        public override void ExecuteAlternativeCommand()
        {
        }

        public override void SetCompleteFlagAfterExecuteCommand()
        {
            IsCompleted = true;
        }
    }
}