using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.OtherPaymentsManagement
{
    public class ModifyOtherPaymentAction : AbstractQueryAction
    {
        public ModifyOtherPaymentAction()
        {
            _action = ModifyOtherPayment;
        }

        private SQLQueryResult ModifyOtherPayment(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblOtherPayment payment = paramaters[0] as tblOtherPayment;
            string imageFolder = paramaters[1] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var value = appDBContext.tblOtherPayments.Where(o => o.PaymentID == payment.PaymentID).FirstOrDefault();
                value.PaymentTime = payment.PaymentTime;
                value.PaymentType = payment.PaymentType;
                value.PaymentContent = payment.PaymentContent;
                value.TotalPrice = payment.TotalPrice;
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
