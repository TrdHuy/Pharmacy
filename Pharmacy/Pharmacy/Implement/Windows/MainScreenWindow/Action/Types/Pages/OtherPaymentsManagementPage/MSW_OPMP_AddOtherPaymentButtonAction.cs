using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage
{
    internal class MSW_OPMP_AddOtherPaymentButtonAction : MSW_OPMP_ButtonAction
    {
        public MSW_OPMP_AddOtherPaymentButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.ADD_OTHER_PAYMENT_PAGE);
        }
    }
}
