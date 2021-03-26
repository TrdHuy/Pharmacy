using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage
{
    internal class MSW_CMP_CTP_DebtsButtonAction : MSW_CMP_CTP_ButtonAction
    {
        public MSW_CMP_CTP_DebtsButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_DEBT_PAGE);
        }
    }
}
