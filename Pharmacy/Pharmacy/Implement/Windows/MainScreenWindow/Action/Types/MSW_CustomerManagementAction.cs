using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_CustomerManagementAction : MSW_ButtonAction
    {
        public MSW_CustomerManagementAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_MANAGEMENT_PAGE);
        }
    }
}
