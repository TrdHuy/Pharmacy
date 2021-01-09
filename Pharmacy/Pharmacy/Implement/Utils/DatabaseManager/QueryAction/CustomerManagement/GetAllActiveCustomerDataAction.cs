using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.CustomerManagement
{
    public class GetAllActiveCustomerDataAction : AbstractQueryAction
    {
        public GetAllActiveCustomerDataAction()
        {
            _action = GetAllActiveCustomerData;
        }

        private SQLQueryResult GetAllActiveCustomerData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblCustomers.
                    Where<tblCustomer>(cus => cus.IsActive).
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
