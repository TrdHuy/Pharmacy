using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.DiscountByMedicinePage
{
    internal class MSW_MMP_DBMP_SaveButtonAction : MSW_MMP_DBMP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private bool _doRefresh;

        public MSW_MMP_DBMP_SaveButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            _doRefresh = false;
            if (!DBMPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Cảnh báo!");
                DBMPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
                return;
            }

            tblPromo promo = new tblPromo();
            promo.PromoPercent = DBMPViewModel.PromoPercent;
            promo.PromoDescription = DBMPViewModel.PromoDescription;
            promo.MedicineID = DBMPViewModel.MedicineInfo.MedicineID;
            promo.CustomerID = DBMPViewModel.LstCustomer[DBMPViewModel.SelectedCustomer].CustomerID;
            promo.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.ADD_MODIFY_PROMO_CMD_KEY,
                _sqlCmdObserver,
                promo);

            if (_doRefresh)
            {
                DBMPViewModel.RefreshPage();
                return;
            }
            return;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Lưu thông tin khuyến mãi thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo");
                _doRefresh = true;
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi lưu thông tin khuyến mãi. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Lỗi!");
            }
            DBMPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
        }
    }
}