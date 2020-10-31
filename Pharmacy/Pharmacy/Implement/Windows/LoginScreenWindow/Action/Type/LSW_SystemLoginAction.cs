using Pharmacy.Base.UIEventHandler.Action;
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
    class LSW_SystemLoginAction : IAction
    {
        private const string DataConectionPath = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\PharmacyDB.mdf;Integrated Security=True";

        public bool Execute(object[] dataTransfer)
        {
            string userName = "huyTran";
            string passWord = "123456";

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
