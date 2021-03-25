using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserModificationPage
{
    internal class MSW_UMP_UMoP_SaveButtonAction : MSW_UMP_UMoP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private tblUser modifiedInfo;

        public MSW_UMP_UMoP_SaveButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            if (!UMoPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                UMoPViewModel.ButtomCommandOV.IsSaveButtonRunning = false;
                return;
            }

            modifiedInfo = new tblUser();
            modifiedInfo.FullName = UMoPViewModel.FullNameText;
            modifiedInfo.Address = UMoPViewModel.AddressText;
            modifiedInfo.Phone = UMoPViewModel.PhoneText;
            modifiedInfo.Email = UMoPViewModel.EmailText;
            modifiedInfo.Link = UMoPViewModel.LinkText;
            modifiedInfo.Job = UMoPViewModel.JobText;
            modifiedInfo.Password = String.IsNullOrEmpty(UMoPViewModel.NewPassword) ?
                App.Current.CurrentUser.Password : UMoPViewModel.NewPassword;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.UPDATE_USER_INFO_CMD_KEY
                    , PharmacyDefinitions.SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME
                    , _sqlCmdObserver
                    , modifiedInfo
                    , UMoPViewModel.UserNameText
                    , UMoPViewModel.UserImageFileName);
            return;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Thay đổi thông tin thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi! Không thể thay đổi thông tin. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            UMoPViewModel.ButtomCommandOV.IsSaveButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.USER_MANAGEMENT_PAGE);
        }
    }
}