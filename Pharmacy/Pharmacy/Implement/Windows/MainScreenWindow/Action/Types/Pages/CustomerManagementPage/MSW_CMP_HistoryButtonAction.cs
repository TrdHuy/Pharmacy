using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Windows.Controls;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage
{
    internal class MSW_CMP_HistoryButtonAction : MSW_CMP_ButtonAction
    {
        public MSW_CMP_HistoryButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);

            DataGrid ctrl = DataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedCustomer = ctrl.SelectedItem as tblCustomer;
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_TRANSACTION_HISTORY_PAGE);
        }
    }
}
