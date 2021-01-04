using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage
{
    public class MSW_PIP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private PersonalInfoPageViewModel _viewModel;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private tblUser modifiedInfo;
        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as PersonalInfoPageViewModel;
            if (!_viewModel.IsSaveButtonCanPerform)
            {
                MessageBox.Show("Kiểm tra lại các trường bị sai trên!");
                _viewModel.IsSaveButtonRunning = false;
                return false;
            }

            modifiedInfo = new tblUser();
            modifiedInfo.Username = _viewModel.CurrentUser.Username;
            modifiedInfo.FullName = _viewModel.FullNameText;
            modifiedInfo.Address = _viewModel.AdressText;
            modifiedInfo.Phone = _viewModel.PhoneText;
            modifiedInfo.Email = _viewModel.EmailText;
            modifiedInfo.Link = _viewModel.LinkText;
            modifiedInfo.Job = _viewModel.CurrentUser.Job;
            modifiedInfo.Password = String.IsNullOrEmpty(_viewModel.NewPassword) ?
                App.Current.CurrentUser.Password : _viewModel.NewPassword;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.UPDATE_USER_INFO_CMD_KEY
                    , PharmacyDefinitions.SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME
                    , _sqlCmdObserver
                    , modifiedInfo, App.Current.CurrentUser.Username);
            return true;
        }

        private void SQLQueryCallback(SQLQueryResult result)
        {
            if (result.Result != null)
            {
                tblUser x = result.Result as tblUser;

                App.Current.CurrentUser.FullName = x.FullName;
                App.Current.CurrentUser.Address = x.Address;
                App.Current.CurrentUser.Password = x.Password;
                App.Current.CurrentUser.Phone = x.Phone;
                App.Current.CurrentUser.Email = x.Email;
                App.Current.CurrentUser.Link = x.Link;

                _pageHost.UpdateCurrentPageSource(PageSource.HomePage);
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật thông tin!");
            }
            _viewModel.IsSaveButtonRunning = false;
        }
    }
}
