using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_InvoiceManagementAction : MSW_NavigationButtonAction
    {
        public MSW_InvoiceManagementAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
        protected override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.INVOICE_MANAGEMENT_PAGE);
        }
    }
}
