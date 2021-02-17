using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.OtherPaymentsManagement
{
    public class GetAllActiveOtherPaymentDataAction : AbstractQueryAction
    {
        public GetAllActiveOtherPaymentDataAction()
        {
            _action = GetAllActiveOtherPaymentData;
        }

        private SQLQueryResult GetAllActiveOtherPaymentData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                List<tblOtherPayment> lstOutput = appDBContext.tblOtherPayments
                        .Where(o => o.IsActive)
                        .OrderByDescending(o=>o.PaymentTime)
                        .ToList();
                result = new SQLQueryResult(lstOutput, MessageQueryResult.Done);
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
