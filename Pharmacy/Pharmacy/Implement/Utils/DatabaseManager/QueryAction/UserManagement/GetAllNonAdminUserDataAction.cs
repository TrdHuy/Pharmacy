using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.UserManagement
{
    public class GetAllNonAdminUserDataAction : AbstractQueryAction
    {
        public GetAllNonAdminUserDataAction()
        {
            _action = GetAllNonAdminUserData;
        }

        private SQLQueryResult GetAllNonAdminUserData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblUsers.
                    Where<tblUser>(user => !user.IsAdmin).
                    ToList();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
            }
            return result;
        }
    }
}
