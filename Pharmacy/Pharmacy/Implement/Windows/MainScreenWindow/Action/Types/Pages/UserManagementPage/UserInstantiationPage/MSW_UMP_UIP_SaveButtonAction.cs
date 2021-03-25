using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserInstantiationPage
{
    internal class MSW_UMP_UIP_SaveButtonAction : MSW_UMP_UIP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private tblUser _newUserInfo;

        public MSW_UMP_UIP_SaveButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            if (!UIPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                UIPViewModel.IsSaveButtonRunning = false;
                return;
            }

            if (!IsUseDefaultPassword())
            {
                return;
            }

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_NEW_USER_CMD_KEY,
                PharmacyDefinitions.SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME,
                _sqlCmdObserver,
                _newUserInfo,
                UIPViewModel.UserImageFileName);

            return;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Thêm tài khoản mới thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm tài khoản mới. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            UIPViewModel.IsSaveButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.USER_MANAGEMENT_PAGE);
        }

        private bool IsUseDefaultPassword()
        {
            _newUserInfo = new tblUser();
            _newUserInfo.Username = UIPViewModel.UserNameText;
            _newUserInfo.IsAdmin = false;
            _newUserInfo.IsActive = true;
            _newUserInfo.FullName = UIPViewModel.FullNameText;
            _newUserInfo.Address = UIPViewModel.AdressText;
            _newUserInfo.Link = UIPViewModel.LinkText;
            _newUserInfo.Phone = UIPViewModel.PhoneText;
            _newUserInfo.Job = UIPViewModel.JobText;
            _newUserInfo.Email = UIPViewModel.EmailText;

            if (String.IsNullOrEmpty(UIPViewModel?.NewPassword))
            {
                var queryRes = App.Current.ShowApplicationMessageBox("Trường mật khẩu bị bỏ trống, bạn muốn dùng mật khẩu mặc định là \"abc@13579\"?",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo!");

                if (queryRes == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
                {
                    _newUserInfo.Password = "abc@13579";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                _newUserInfo.Password = UIPViewModel.NewPassword;
            }
            return true;
        }
    }
}