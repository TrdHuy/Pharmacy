using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.DiscountByMedicinePage
{
    public class MSW_MMP_DBMP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private SQLQueryCustodian _sqlCmdObserver;
        private DiscountByMedicinePageViewModel _viewModel;
        private bool _doRefresh;

        public bool Execute(object[] dataTransfer)
        {
            _doRefresh = false;
            _viewModel = dataTransfer[0] as DiscountByMedicinePageViewModel;
            if (!_viewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                _viewModel.IsSaveButtonRunning = false;
                return false;
            }

            tblPromo promo = new tblPromo();
            promo.PromoPercent = int.Parse(_viewModel.PromoPercent.Trim());
            promo.PromoDescription = _viewModel.PromoDescription;
            promo.MedicineID = _viewModel.MedicineInfo.MedicineID;
            promo.CustomerID = _viewModel.LstCustomer[_viewModel.SelectedCustomer].CustomerID;
            promo.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.ADD_MODIFY_PROMO_CMD_KEY,
                _sqlCmdObserver,
                promo);

            if (_doRefresh)
            {
                _viewModel.RefreshPage();
                return true;
            }
            return false;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Lưu thông tin khuyến mãi thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
                _doRefresh = true;
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi lưu thông tin khuyến mãi. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            _viewModel.IsSaveButtonRunning = false;
        }
    }
}