using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserModificationPage
{
    public class MSW_UMP_UMoP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private UserModificationPageViewModel _viewModel;
        private tblUser modifiedInfo;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as UserModificationPageViewModel;
            if (!_viewModel.IsSaveButtonCanPerform)
            {
                MessageBox.Show("Kiểm tra lại các trường bị sai trên!");
                _viewModel.IsSaveButtonRunning = false;
                return false;
            }

            modifiedInfo = new tblUser();
            modifiedInfo.FullName = _viewModel.FullNameText;
            modifiedInfo.Address = _viewModel.AdressText;
            modifiedInfo.Phone = _viewModel.PhoneText;
            modifiedInfo.Email = _viewModel.EmailText;
            modifiedInfo.Link = _viewModel.LinkText;
            modifiedInfo.Password = String.IsNullOrEmpty(_viewModel.NewPassword) ?
                App.Current.CurrentUser.Password : _viewModel.NewPassword;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.UPDATE_USER_INFO_CMD_KEY
                    , PharmacyDefinitions.SAVE_USER_MODIFIED_INFO_BUTTON_PERFORM_DELAY_TIME
                    , _sqlCmdObserver
                    , modifiedInfo, _viewModel.UserNameTextBeforeChanged);
            return true;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            _viewModel.IsSaveButtonRunning = false;
        }
    }
}