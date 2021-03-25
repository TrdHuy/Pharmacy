using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage
{
    internal class MSW_CMP_CTP_ReturnButtonAction : MSW_CMP_CTP_ButtonAction
    {
        public MSW_CMP_CTP_ReturnButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_MANAGEMENT_PAGE);
        }
    }
}
