using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.UserManagement
{
    public class CheckUserNameExistedAction : AbstractQueryAction
    {
        public CheckUserNameExistedAction()
        {
            _action = CheckUserNameExisted;
        }

        private SQLQueryResult CheckUserNameExisted(PharmacyDBContext appDBContext, object[] paramaters)
        {
            string name = paramaters[0].ToString();
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                var x = appDBContext.tblUsers.Where(user => user.Username.Equals(name)).
                    ToList();
                bool IsExisted = x.Count > 0;
                result = new SQLQueryResult(IsExisted, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                ShowErrorMessageBox(e);
            }
            return null;
        }
    }
}
