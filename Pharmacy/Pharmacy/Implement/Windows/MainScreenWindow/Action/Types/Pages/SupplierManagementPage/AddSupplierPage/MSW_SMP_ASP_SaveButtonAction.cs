using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.AddSupplierPage
{
    public class MSW_SMP_ASP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private AddSupplierPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as AddSupplierPageViewModel;
            if (!_viewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại tên và số điện thoại của nhà cung cấp!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                _viewModel.IsSaveButtonRunning = false;
                return false;
            }

            tblSupplier supplier = new tblSupplier();
            supplier.SupplierName = _viewModel.SupplierName;
            supplier.Phone = _viewModel.Phone;
            supplier.Email = _viewModel.Email;
            supplier.Address = _viewModel.Address;
            supplier.SupplierDescription = _viewModel.Description;
            supplier.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_SUPPLIER_CMD_KEY,
                PharmacyDefinitions.ADD_NEW_SUPPLIER_DELAY_TIME,
                _sqlCmdObserver,
                supplier);

            return true;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Thêm nhà cung cấp mới thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm nhà cung cấp mới. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            _viewModel.IsSaveButtonRunning = false;
            _pageHost.UpdateCurrentPageSource(PageSource.SUPPLIER_MANAGEMENT_PAGE);
        }
    }
}