using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction
{
    class SetUserDeactiveAction : AbstractQueryAction
    {
        public SetUserDeactiveAction()
        {
            _action = SetUserDeactive;
        }

        private SQLQueryResult SetUserDeactive(PharmacyDBContext appDBContext, object[] paramaters)
        {
            string name = paramaters[0].ToString();
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblUsers.Where(user => user.Username.Equals(name)).
                    First();
                x.IsActive = false;
                appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
            }
            return result;
        }
    }
}
