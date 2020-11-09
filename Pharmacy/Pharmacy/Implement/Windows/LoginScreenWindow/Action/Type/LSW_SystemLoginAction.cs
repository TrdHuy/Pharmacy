using DevExpress.Xpf.Editors;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type
{
    class LSW_SystemLoginAction : Base.UIEventHandler.Action.IAction
    {
        private const string DataConectionPath = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\PharmacyDB.mdf;Integrated Security=True";
        private SQLQueryCustodian _observer;

        public bool Execute(object[] dataTransfer)
        {
            object[] dataFromView = (object[])dataTransfer[1];
            TextEdit userNameTextEdit = (TextEdit)dataFromView[0];
            PasswordBoxEdit userPasswordTextEdit = (PasswordBoxEdit)dataFromView[1];

            string userName = userNameTextEdit.Text;
            string passWord = userPasswordTextEdit.Text;

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

        private void SQLQueryCallback(object queryResult)
        {
            try
            {
                int count = (((SQLQueryResult)queryResult).Result as IEnumerable<DataRow>).Count();
                if(count == 1)
                {
                    MessageBox.Show("Login Success!");
                    MSWindow mSW = new MSWindow();
                    mSW.Show();
                }
                else
                {
                    MessageBox.Show("Invaild user or password!");
                }
                _observer.Updated = true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }
    }
}
