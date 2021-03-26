using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage.ModifyOtherPayment;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.ModifyOtherPaymentPage
{
    internal class MSW_OPMP_MOPP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected ModifyOtherPaymentPageViewModel MOPPViewModel
        {
            get
            {
                return ViewModel as ModifyOtherPaymentPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_OPMP_MOPP_ButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
        }
    }
}
