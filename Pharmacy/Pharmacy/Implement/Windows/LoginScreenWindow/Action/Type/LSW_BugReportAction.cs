using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type
{
    class LSW_BugReportAction : LSW_ButtonAction
    {
        public LSW_BugReportAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
        }
    }
}
