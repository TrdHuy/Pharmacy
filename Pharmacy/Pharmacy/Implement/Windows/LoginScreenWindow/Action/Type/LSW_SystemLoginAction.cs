using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.ViewModels;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type
{
    public class LSW_SystemLoginAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _observer;
        private LoginScreenWindowViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = (LoginScreenWindowViewModel)dataTransfer[0];

            //_viewModel.IsLoginButtonRunning = true;

            object[] dataFromView = (object[])dataTransfer[1];
            TextBox userNameTextEdit = (TextBox)dataFromView[0];
            PasswordBox userPasswordTextEdit = (PasswordBox)dataFromView[1];

            string userName = userNameTextEdit.Text;
            string passWord = userPasswordTextEdit.Password;

            try
            {
                _observer = new SQLQueryCustodian(SQLQueryCallback);
                DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.CHECK_USER_AVAIL_CMD_KEY
                    , PharmacyDefinitions.LOGIN_BUTTON_PERFORM_DELAY_TIME
                    , _observer
                    , userName, passWord);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            return true;
        }

        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            List<tblUser> result = (List<tblUser>)queryResult.Result;
            try
            {
                int count = result.Count();
                if (count == 1)
                {
                    App.Current.SessionIDInstansiation(result[0]);
                    SaveUserName(result[0].Username);
                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Thông tin đăng nhập không chính xác!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                        OwnerWindow.LoginScreen);
                }
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message,
                       HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                       HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                       OwnerWindow.LoginScreen);
            }
            finally
            {
                _observer.Updated = true;
                if (_viewModel != null)
                {
                    _viewModel.IsLoginButtonRunning = false;
                }
            }
        }

        private void SaveUserName(string userName)
        {
            if (_viewModel.IsUserRemember)
            {
                _viewModel.UserName = userName;
            }
        }
    }
}
