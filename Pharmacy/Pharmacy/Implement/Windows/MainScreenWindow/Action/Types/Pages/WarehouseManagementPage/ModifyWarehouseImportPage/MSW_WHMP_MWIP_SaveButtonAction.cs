using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Collections.Generic;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ModifyWarehouseImportPage
{
    internal class MSW_WHMP_MWIP_SaveButtonAction : MSW_WHMP_MWIP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public MSW_WHMP_MWIP_SaveButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {

            tblWarehouseImport import = MWIPViewModel.ImportInfo;
            import.ImportDescription = MWIPViewModel.NoteString.Trim();
            import.TotalPrice = MWIPViewModel.TotalPrice;
            import.PurchasePrice = MWIPViewModel.PurchasedPrice;

            List<tblWarehouseImportDetail> details = new List<tblWarehouseImportDetail>();
            foreach (var item in MWIPViewModel.LstWarehouseImportDetail)
            {
                tblWarehouseImportDetail detail = new tblWarehouseImportDetail();
                detail.IsActive = true;
                detail.MedicineID = item.MedicineID;
                detail.Price = item.UnitPrice;
                detail.Quantity = item.Quantity;
                details.Add(detail);
            }

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.MODIFY_WAREHOUSE_IMPORT_CMD_KEY,
                PharmacyDefinitions.MODIFY_WAREHOUSE_IMPORT_DELAY_TIME,
                _sqlCmdObserver,
                import,
                details,
                MWIPViewModel.InvoiceImageURL);

            return;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Chỉnh sửa thông tin nhập kho thành công",
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
            MWIPViewModel.ButtonCommandOV.IsAddWarehouseImportButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.WAREHOUSE_MANAGEMENT_PAGE);
        }
    }
}