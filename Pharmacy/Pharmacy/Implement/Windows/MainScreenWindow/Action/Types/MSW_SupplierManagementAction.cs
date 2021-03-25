using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_SupplierManagementAction : MSW_ButtonAction
    {
        public MSW_SupplierManagementAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_MANAGEMENT_PAGE);

        }
    }
}
