using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.UserManagement
{
    public class CheckUserAvailAction : AbstractQueryAction
    {
        public CheckUserAvailAction()
        {
            _action = CheckUserAvail;
        }

        private SQLQueryResult CheckUserAvail(PharmacyDBContext appDBContext, object[] paramaters)
        {
            string name = paramaters[0].ToString();
            string pass = paramaters[1].ToString();

            try
            {
                var x = appDBContext.tblUsers.Where(user => user.Username.Equals(name)
                && user.Password.Equals(pass)).ToList();

                SQLQueryResult result = new SQLQueryResult(x, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
            }
            return null;
        }
    }
}
