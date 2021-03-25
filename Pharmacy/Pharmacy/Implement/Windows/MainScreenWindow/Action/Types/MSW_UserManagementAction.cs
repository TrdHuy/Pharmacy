using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_UserManagementAction : MSW_ButtonAction
    {
        public MSW_UserManagementAction(ILogger logger) : base(logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.USER_MANAGEMENT_PAGE);

        }
    }
}
