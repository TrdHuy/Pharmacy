using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ShowWarehouseImportInfoPage
{
    internal class MSW_WHMP_SWIIP_ShowInvoiceButtonAction : MSW_WHMP_SWIP_ButtonAction
    {
        public MSW_WHMP_SWIIP_ShowInvoiceButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            FileIOUtil.ShowBitmapFromName(SWIPViewModel.ImportInfo.ImportID.ToString(), FileIOUtil.WAREHOUSE_IMPORT_IMAGE_FOLDER_NAME);
        }
    }
}