using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.AddOtherPaymentPage
{
    internal class MSW_OPMP_AOPP_CancelButtonAction : MSW_OPMP_AOPP_ButtonAction
    {
        public MSW_OPMP_AOPP_CancelButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.OTHER_PAYMENT_MANAGEMENT_PAGE);

        }
    }
}