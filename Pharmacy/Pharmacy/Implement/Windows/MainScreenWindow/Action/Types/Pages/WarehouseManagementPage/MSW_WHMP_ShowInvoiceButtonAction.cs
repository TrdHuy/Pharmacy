using Pharmacy.Implement.Utils;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage
{
    internal class MSW_WHMP_ShowInvoiceButtonAction : MSW_WHMP_ButtonAction
    {
        public MSW_WHMP_ShowInvoiceButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
            DataGrid ctrl = DataTransfer[1] as DataGrid;

            FileIOUtil.ShowBitmapFromName(WHMPViewModel.LstWarehouseImport[ctrl.SelectedIndex].ImportID.ToString(), FileIOUtil.WAREHOUSE_IMPORT_IMAGE_FOLDER_NAME);
            return;
        }
    }
}