using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    internal class MSW_CMP_CTP_CBP_EditEnablerButtonAction : MSW_CMP_CTP_CBP_ButtonAction
    {
        public MSW_CMP_CTP_CBP_EditEnablerButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            CBPViewModel.IsEnableEdittingBill = !CBPViewModel.IsEnableEdittingBill;

            CBPViewModel.DeleteColumnVisibility = CBPViewModel.IsEnableEdittingBill ?
                System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
    }
}
