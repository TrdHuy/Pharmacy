using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction
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
                    Where<tblUser>(user => user.IsActive).
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
