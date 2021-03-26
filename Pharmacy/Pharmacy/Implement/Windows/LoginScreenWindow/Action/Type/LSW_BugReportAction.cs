using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type
{
    class LSW_BugReportAction : LSW_ButtonAction
    {
        public LSW_BugReportAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
        }
    }
}
