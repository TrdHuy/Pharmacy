using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.CustomerManagement
{
    public class SetCustomerDeactiveAction : AbstractQueryAction
    {
        public SetCustomerDeactiveAction()
        {
            _action = SetCustomerDeactive;
        }

        private SQLQueryResult SetCustomerDeactive(PharmacyDBContext appDBContext, object[] paramaters)
        {
            int cusID = Convert.ToInt32(paramaters[0]);
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblCustomers.Where(cus => cus.CustomerID == cusID).
                    First();
                x.IsActive = false;
                appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message);
            }
            finally
            {
            }
            return result;
        }
    }
}
