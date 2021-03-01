using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions.Entities;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SettingPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SettingPage
{
    public class MSW_SeP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SettingPageViewModel _viewModel;
        private SQLQueryCustodian _sqlCmdObserver;
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as SettingPageViewModel;
            var user = App.Current.CurrentUser;
            user.GetUserData().FontZoomRatio = _viewModel.FontSizeRatio;
            user.UserDataJSON = user.GetUserDataJSON();

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.UPDATE_USER_INFO_CMD_KEY
                    , PharmacyDefinitions.SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME
                    , _sqlCmdObserver
                    , user
                    , App.Current.CurrentUser.Username
                    , "");
            return true;
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
                _pageHost.UpdateCurrentPageSource(PageSource.HomePage);
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi cập nhật thông tin!");
            }
            _viewModel.ButtonCommandOV.IsSaveButtonRunning = false;
        }
    }
}
