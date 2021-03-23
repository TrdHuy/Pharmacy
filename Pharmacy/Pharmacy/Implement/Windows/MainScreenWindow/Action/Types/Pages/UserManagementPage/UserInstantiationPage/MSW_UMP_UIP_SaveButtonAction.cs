using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserInstantiationPage
{
    public class MSW_UMP_UIP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private UserInstantiationPageViewModel _viewModel;
        private tblUser _newUserInfo;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as UserInstantiationPageViewModel;
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

            if (!IsUseDefaultPassword())
            {
                _viewModel.IsSaveButtonRunning = false;
                return false;
            }

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_NEW_USER_CMD_KEY,
                PharmacyDefinitions.SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME,
                _sqlCmdObserver,
                _newUserInfo,
                _viewModel.UserImageFileName);

            return true;
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
            _viewModel.IsSaveButtonRunning = false;
            _pageHost.UpdateCurrentPageSource(PageSource.USER_MANAGEMENT_PAGE);
        }

        private bool IsUseDefaultPassword()
        {
            _newUserInfo = new tblUser();
            _newUserInfo.Username = _viewModel.UserNameText;
            _newUserInfo.IsAdmin = false;
            _newUserInfo.IsActive = true;
            _newUserInfo.FullName = _viewModel.FullNameText;
            _newUserInfo.Address = _viewModel.AdressText;
            _newUserInfo.Link = _viewModel.LinkText;
            _newUserInfo.Phone = _viewModel.PhoneText;
            _newUserInfo.Job = _viewModel.JobText;
            _newUserInfo.Email = _viewModel.EmailText;

            if (String.IsNullOrEmpty(_viewModel?.NewPassword))
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
                _newUserInfo.Password = _viewModel.NewPassword;
            }
            return true;
        }
    }
}