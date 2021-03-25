using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.SupplierImportHistoryPage
{
    internal class MSW_SMP_SIHP_ShowSupplierDebButtonAction : MSW_SMP_SIHP_ButtonAction
    {
        public MSW_SMP_SIHP_ShowSupplierDebButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_DEBT_PAGE);
        }
    }
}