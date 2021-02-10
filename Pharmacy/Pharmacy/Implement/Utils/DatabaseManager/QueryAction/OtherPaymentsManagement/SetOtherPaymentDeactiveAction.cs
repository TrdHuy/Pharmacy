using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.OtherPaymentsManagement
{
    public class SetOtherPaymentDeactiveAction : AbstractQueryAction
    {
        public SetOtherPaymentDeactiveAction()
        {
            _action = SetOtherPaymentDeactive;
        }

        private SQLQueryResult SetOtherPaymentDeactive(PharmacyDBContext appDBContext, object[] paramaters)
        {
            long id = (long)paramaters[0];
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblOtherPayments.Where(o => o.PaymentID == id).FirstOrDefault();
                x.IsActive = false;
                appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
            }
            return result;
        }
    }
}
