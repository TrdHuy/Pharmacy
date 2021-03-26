using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage
{
    internal class MSW_CMP_CTP_BillButtonAction : MSW_CMP_CTP_ButtonAction
    {
        public MSW_CMP_CTP_BillButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
            if (CTHPViewModel.CurrentSelectedOrder == null)
            {
                App.Current.ShowApplicationMessageBox("Vui lòng chọn 1 giao dịch để xem hóa đơn",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                return;
            }

            MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder = CTHPViewModel.CurrentSelectedOrder;
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_BILL_PAGE);

            return;
        }
    }
}
