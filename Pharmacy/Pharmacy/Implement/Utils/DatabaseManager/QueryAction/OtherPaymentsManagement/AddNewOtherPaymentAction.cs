using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.OtherPaymentsManagement
{
    public class AddNewOtherPaymentAction : AbstractQueryAction
    {
        public AddNewOtherPaymentAction()
        {
            _action = AddNewOtherPayment;
        }

        private SQLQueryResult AddNewOtherPayment(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblOtherPayment payment = paramaters[0] as tblOtherPayment;
            string imageFolder = paramaters[1] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                appDBContext.tblOtherPayments.Add(payment);
                appDBContext.SaveChanges();
                if (imageFolder.Length > 0 && !SaveImageToFile(payment.PaymentID.ToString(), imageFolder, ImageType.OtherPayment))
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                    return result;
                }
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
