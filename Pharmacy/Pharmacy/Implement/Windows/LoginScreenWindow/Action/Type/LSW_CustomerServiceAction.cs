using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type
{
    internal class LSW_CustomerServiceAction : LSW_ButtonAction
    {
        public LSW_CustomerServiceAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
        }
    }
}
