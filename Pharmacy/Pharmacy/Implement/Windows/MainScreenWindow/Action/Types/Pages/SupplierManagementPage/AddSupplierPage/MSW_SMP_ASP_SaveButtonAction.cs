using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.AddSupplierPage
{
    internal class MSW_SMP_ASP_SaveButtonAction : MSW_SMP_ASP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        public MSW_SMP_ASP_SaveButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            if (!ASPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại tên và số điện thoại của nhà cung cấp!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Cảnh báo!");
                ASPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
                return;
            }

            tblSupplier supplier = new tblSupplier();
            supplier.SupplierName = ASPViewModel.SupplierName;
            supplier.Phone = ASPViewModel.Phone;
            supplier.Email = ASPViewModel.Email;
            supplier.Address = ASPViewModel.Address;
            supplier.SupplierDescription = ASPViewModel.Description;
            supplier.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_SUPPLIER_CMD_KEY,
                PharmacyDefinitions.ADD_NEW_SUPPLIER_DELAY_TIME,
                _sqlCmdObserver,
                supplier);

            return;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Thêm nhà cung cấp mới thành công!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm nhà cung cấp mới. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Lỗi!");
            }
            ASPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_MANAGEMENT_PAGE);
        }
    }
}