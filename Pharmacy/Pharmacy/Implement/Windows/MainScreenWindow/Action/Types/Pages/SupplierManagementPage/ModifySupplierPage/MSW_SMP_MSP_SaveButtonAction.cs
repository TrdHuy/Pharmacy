using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.ModifySupplierPage
{
    internal class MSW_SMP_MSP_SaveButtonAction : MSW_SMP_MSP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public MSW_SMP_MSP_SaveButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            if (!MSPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại tên và số điện thoại của nhà cung cấp!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                MSPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
                return;
            }

            tblSupplier supplier = new tblSupplier();
            supplier.SupplierID = MSPViewModel.SupplierInfo.SupplierID;
            supplier.SupplierName = MSPViewModel.SupplierName;
            supplier.Phone = MSPViewModel.Phone;
            supplier.Email = MSPViewModel.Email;
            supplier.Address = MSPViewModel.Address;
            supplier.SupplierDescription = MSPViewModel.Description;
            supplier.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.MODIFY_SUPPLIER_CMD_KEY,
                PharmacyDefinitions.MODIFY_SUPPLIER_DELAY_TIME,
                _sqlCmdObserver,
                supplier);

            return;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Chỉnh sửa nhà cung cấp thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi chỉnh sửa nhà cung cấp. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            MSPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_MANAGEMENT_PAGE);
        }
    }
}