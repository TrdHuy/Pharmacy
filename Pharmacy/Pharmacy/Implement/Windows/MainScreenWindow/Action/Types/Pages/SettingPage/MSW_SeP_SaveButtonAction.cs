using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions.Entities;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;
using Pharmacy.Base.MVVM.ViewModels;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SettingPage
{
    internal class MSW_SeP_SaveButtonAction : MSW_SeP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public MSW_SeP_SaveButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
            var user = App.Current.CurrentUser;
            user.GetUserData().FontZoomRatio = SPViewModel.FontSizeRatio;
            user.UserDataJSON = user.GetUserDataJSON();

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.UPDATE_USER_INFO_CMD_KEY
                    , PharmacyDefinitions.SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME
                    , _sqlCmdObserver
                    , user
                    , App.Current.CurrentUser.Username
                    , "");
            return;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Cập nhật cài đặt thành công!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                PageHost.UpdateCurrentPageSource(PageSource.HOME_PAGE);
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi cập nhật thông tin!");
            }
            SPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
        }
    }
}
