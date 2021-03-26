using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_OtherPaymentsManagementAction : MSW_NavigationButtonAction
    {
        public MSW_OtherPaymentsManagementAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
        protected override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.OTHER_PAYMENT_MANAGEMENT_PAGE);
        }
    }
}
