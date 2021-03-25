using Pharmacy.Implement.Utils;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.SupplierImportHistoryPage
{
    internal class MSW_SMP_SIHP_ShowInvoiceButtonAction : MSW_SMP_SIHP_ButtonAction
    {

        public MSW_SMP_SIHP_ShowInvoiceButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {

            if (SIHPViewModel.ImportInfo == null)
            {
                App.Current.ShowApplicationMessageBox("Vui lòng chọn 1 giao dịch để xem hóa đơn!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                return;
            }

            FileIOUtil.ShowBitmapFromName(SIHPViewModel.ImportInfo.ImportID.ToString(), FileIOUtil.WAREHOUSE_IMPORT_IMAGE_FOLDER_NAME);

            return;
        }
    }
}