using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    internal class MSW_CMP_CTP_CBP_DeleteOrderDetailButtonAction : MSW_CMP_CTP_CBP_ButtonAction
    {
        private DataGrid orderDetailDataGrid;

        public MSW_CMP_CTP_CBP_DeleteOrderDetailButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
            base.ExecuteCommand();

            orderDetailDataGrid = DataTransfer[1] as DataGrid;

            CBPViewModel.CurrentOrderDetails.RemoveAt(orderDetailDataGrid.SelectedIndex);
        }
    }
}
