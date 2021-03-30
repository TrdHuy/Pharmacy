using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System.Linq;
using System.Windows.Controls;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerDebtsPage
{
    internal class MSW_CMP_CTP_CDP_BillDisplayButtonAction : MSW_CMP_CTP_CDP_ButtonAction
    {
        public MSW_CMP_CTP_CDP_BillDisplayButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            DataGrid ctrl = DataTransfer[0] as DataGrid;

            MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder = CDPViewModel.OrderItemSource.Where(o => o.OrderID == CDPViewModel.DebtItemSource[ctrl.SelectedIndex].OrderID).FirstOrDefault();
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_BILL_PAGE);
        }
    }
}
