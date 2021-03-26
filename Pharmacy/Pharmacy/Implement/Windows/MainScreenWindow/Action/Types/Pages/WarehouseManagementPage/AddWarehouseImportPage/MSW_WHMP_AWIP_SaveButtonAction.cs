using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Collections.Generic;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.AddWarehouseImportPage
{
    internal class MSW_WHMP_AWIP_SaveButtonAction : MSW_WHMP_AWIP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public MSW_WHMP_AWIP_SaveButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            if (!AWIPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại thông tin nhà cung cấp và danh sách thuốc nhập!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                AWIPViewModel.ButtonCommandOV.IsAddWarehouseImportButtonRunning = false;
                return;
            }

            tblWarehouseImport import = new tblWarehouseImport();
            import.ImportTime = DateTime.Now;
            import.SupplierID = AWIPViewModel.SelectedSupplier.SupplierID;
            import.ImportDescription = AWIPViewModel.NoteString.Trim();
            import.IsActive = true;
            import.TotalPrice = AWIPViewModel.TotalPrice;
            import.PurchasePrice = AWIPViewModel.PurchasedPrice;

            List<tblWarehouseImportDetail> details = new List<tblWarehouseImportDetail>();
            foreach (var item in AWIPViewModel.LstWarehouseImportDetail)
            {
                tblWarehouseImportDetail detail = new tblWarehouseImportDetail();
                detail.IsActive = true;
                detail.MedicineID = item.MedicineID;
                detail.Price = item.UnitPrice;
                detail.Quantity = item.Quantity;
                details.Add(detail);
            }

            import.tblWarehouseImportDetails = details;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_WAREHOUSE_IMPORT_CMD_KEY,
                PharmacyDefinitions.ADD_NEW_WAREHOUSE_IMPORT_DELAY_TIME,
                _sqlCmdObserver,
                import,
                AWIPViewModel.InvoiceImageURL);

            return;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Thêm thông tin nhập kho thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm thông tin nhập kho. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            AWIPViewModel.ButtonCommandOV.IsAddWarehouseImportButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.WAREHOUSE_MANAGEMENT_PAGE);
        }
    }
}