using DevExpress.Xpf.Editors;
using Pharmacy.Base.UIEventHandler.Action;
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

        public bool Execute(object[] dataTransfer)
        {
            object[] dataFromView = (object[]) dataTransfer[1];
            TextEdit userNameTextEdit = (TextEdit) dataFromView[0];
            PasswordBoxEdit userPasswordTextEdit = (PasswordBoxEdit)dataFromView[1];

            string userName = userNameTextEdit.Text;
            string passWord = userPasswordTextEdit.Text;

            SqlConnection sqlCon = new SqlConnection(DataConectionPath);
            try
            {
                if(sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                String querry = "SELECT COUNT(1) FROM tblUser WHERE UserName=@Username AND UserPassword=@Password";
                SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Username", userName);
                sqlCommand.Parameters.AddWithValue("@Password", passWord);

                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if(count == 1)
                {
                    MessageBox.Show("Login Success!");
                    MSWindow mSW = new MSWindow();
                    mSW.Show();
                }
                else
                {
                    MessageBox.Show("Login fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            return true;
        }
    }
}
