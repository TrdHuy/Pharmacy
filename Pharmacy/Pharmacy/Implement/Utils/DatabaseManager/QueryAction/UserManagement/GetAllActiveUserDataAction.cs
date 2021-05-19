using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.UserManagement
{
    public class GetAllActiveUserDataAction : AbstractQueryAction
    {
        public GetAllActiveUserDataAction()
        {
            _action = GetAllActiveUserData;
        }

        private SQLQueryResult GetAllActiveUserData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblUsers.
                    Where<tblUser>(user => user.IsActive).OrderByDescending(o => o.IsAdmin).ThenBy(o => o.Username).
                    ToList();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (Exception e)
            {
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                ShowErrorMessageBox(e);
            }
            return result;
        }
    }
}
