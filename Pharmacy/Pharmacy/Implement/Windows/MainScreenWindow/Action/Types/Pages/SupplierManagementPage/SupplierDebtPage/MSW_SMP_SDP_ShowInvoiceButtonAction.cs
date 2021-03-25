using Pharmacy.Implement.Utils;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.SupplierDebtPage
{
    internal class MSW_SMP_SDP_ShowInvoiceButtonAction : MSW_SMP_SDP_ButtonAction
    {
        public MSW_SMP_SDP_ShowInvoiceButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
            DataGrid ctrl = DataTransfer[1] as DataGrid;

            FileIOUtil.ShowBitmapFromName(SDPViewModel.LstDebt[ctrl.SelectedIndex].ImportID.ToString(), FileIOUtil.WAREHOUSE_IMPORT_IMAGE_FOLDER_NAME);

        }
    }
}