using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_OtherPaymentsManagementAction : MSW_ButtonAction
    {
        public MSW_OtherPaymentsManagementAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.OTHER_PAYMENT_MANAGEMENT_PAGE);
        }
    }
}
