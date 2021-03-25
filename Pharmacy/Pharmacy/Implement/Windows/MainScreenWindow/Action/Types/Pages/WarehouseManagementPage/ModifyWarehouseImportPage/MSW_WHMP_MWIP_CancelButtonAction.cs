using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ModifyWarehouseImportPage
{
    internal class MSW_WHMP_MWIP_CancelButtonAction :MSW_WHMP_MWIP_ButtonAction
    {
        public MSW_WHMP_MWIP_CancelButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.WAREHOUSE_MANAGEMENT_PAGE);
        }
    }
}