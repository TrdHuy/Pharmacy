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
    class LSW_SystemLoginAction : Base.UIEventHandler.Action.IAction
    {
        private const string DataConectionPath = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\PharmacyDB.mdf;Integrated Security=True";
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
            try
            {
                int count = ((List<Users>)queryResult.Result).Count();
                if (count == 1)
                {
                    MessageBox.Show("Login Success!");
                    MSWindow mSW = new MSWindow();
                    mSW.Show();
                    _loginWindow.Close();
                }
                else
                {
                    MessageBox.Show("Invaild user or password!");
                }
                _observer.Updated = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (_viewModel != null)
                {
                    _viewModel.IsLoginButtonRunning = false;
                }
            }


        }
    }
}
