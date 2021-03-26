using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.SupplierDebtPage
{
    internal class MSW_SMP_SDP_CancelButtonAction : MSW_SMP_SDP_ButtonAction
    {
        public MSW_SMP_SDP_CancelButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_IMPORT_HISTORY_PAGE);

        }
    }
}