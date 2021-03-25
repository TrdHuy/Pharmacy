using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.AddOtherPaymentPage
{
    internal class MSW_OPMP_AOPP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected AddOtherPaymentPageViewModel AOPPViewModel
        {
            get
            {
                return ViewModel as AddOtherPaymentPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_OPMP_AOPP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}
