using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage
{
    internal class MSW_OPMP_ModifyOtherPaymentButtonAction : MSW_OPMP_ButtonAction
    {

        public MSW_OPMP_ModifyOtherPaymentButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        public override void ExecuteCommand()
        {
            base.ExecuteCommand();

            DataGrid ctrl = DataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentSelectedOtherPayment = OPMPViewModel.OtherPaymentItemSource[ctrl.SelectedIndex];
            PageHost.UpdateCurrentPageSource(PageSource.MODIFY_OTHER_PAYMENT_PAGE);
        }
    }
}