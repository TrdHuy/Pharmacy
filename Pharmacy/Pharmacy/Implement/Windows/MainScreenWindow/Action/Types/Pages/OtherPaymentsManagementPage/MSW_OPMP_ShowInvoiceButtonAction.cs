using Pharmacy.Implement.Utils;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage
{
    internal class MSW_OPMP_ShowInvoiceButtonAction : MSW_OPMP_ButtonAction
    {

        public MSW_OPMP_ShowInvoiceButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            DataGrid ctrl = DataTransfer[1] as DataGrid;

            FileIOUtil.ShowBitmapFromName(OPMPViewModel.OtherPaymentItemSource[ctrl.SelectedIndex].PaymentID.ToString(), FileIOUtil.OTHER_PAYMENT_IMAGE_FOLDER_NAME);
        }
    }
}