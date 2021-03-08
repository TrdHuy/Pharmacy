using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Collections.Generic;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ModifyWarehouseImportPage
{
    public class MSW_WHMP_MWIP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private ModifyWarehouseImportPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as ModifyWarehouseImportPageViewModel;

            tblWarehouseImport import = _viewModel.ImportInfo;
            import.ImportDescription = _viewModel.NoteString.Trim();
            import.TotalPrice = _viewModel.TotalPrice;
            import.PurchasePrice = _viewModel.PurchasedPrice;

            List<tblWarehouseImportDetail> details = new List<tblWarehouseImportDetail>();
            foreach (var item in _viewModel.LstWarehouseImportDetail)
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
                _viewModel.InvoiceImageURL);

            return true;
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
            _viewModel.IsAddWarehouseImportButtonRunning = false;
            _pageHost.UpdateCurrentPageSource(PageSource.WAREHOUSE_MANAGEMENT_PAGE);
        }
    }
}