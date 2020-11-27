using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.Utils.DatabaseManager;
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
        private ApplicationDataManager _applicationDataManager = ApplicationDataManager.Instance;
        private SQLQueryCustodian _observer;
        private Window _loginWindow;
        private LoginScreenWindowViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = (LoginScreenWindowViewModel)dataTransfer[0];

            //_viewModel.IsLoginButtonRunning = true;

            object[] dataFromView = (object[])dataTransfer[1];
            TextBox userNameTextEdit = (TextBox)dataFromView[0];
            PasswordBox userPasswordTextEdit = (PasswordBox)dataFromView[1];
            _loginWindow = (Window)dataFromView[2];

            string userName = userNameTextEdit.Text;
            string passWord = userPasswordTextEdit.Password;

            try
            {
                _observer = new SQLQueryCustodian(SQLQueryCallback);
                DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.CHECK_USER_AVAIL_CMD_KEY
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
                    CreateSessionID(result[0]);

                    MessageBox.Show("Login Success!");

                    MSWindow mSW = new MSWindow();
                    mSW.Show();
                    _loginWindow.Close();
                }
                else
                {
                    MessageBox.Show("Invaild user or password!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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

        private void CreateSessionID(tblUser curUser)
        {
            string connectionID = _applicationDataManager.GenerateConnectionID();
            string sessionID = DateTime.Now + "/" + curUser.Username + "/" + connectionID;
            _applicationDataManager.UpdateSessionInfo(connectionID, sessionID, curUser);

        }
    }
}
