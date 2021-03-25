using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerModificationPage
{
    internal class MSW_CMP_CMoP_SaveButtonAction : MSW_CMP_CMoP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public MSW_CMP_CMoP_SaveButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            if (!CMoPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                CMoPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
                return;
            }


            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.UPDATE_CUSTOMER_INFO_CMD_KEY,
                PharmacyDefinitions.SAVE_CUSTOMER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME,
                _sqlCmdObserver,
                CMoPViewModel.CurrentModifiedCustomer,
                CMoPViewModel.CustomerImageFileName);

            return;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Cập nhật thông tin khách hàng thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi cập nhật thông tin khách hàng. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            CMoPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_MANAGEMENT_PAGE);
        }

    }
}
