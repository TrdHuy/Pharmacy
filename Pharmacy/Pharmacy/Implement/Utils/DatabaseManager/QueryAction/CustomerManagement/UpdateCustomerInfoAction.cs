using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction
{
    public class UpdateCustomerInfoAction : AbstractQueryAction
    {
        public UpdateCustomerInfoAction()
        {
            _action = UpdateCustomerInfo;
        }

        private SQLQueryResult UpdateCustomerInfo(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblCustomer modifiedCustomer = paramaters[0] as tblCustomer;
            string imageFolder = paramaters[1] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                var x = appDBContext.tblCustomers.Where<tblCustomer>(cus => cus.CustomerID == modifiedCustomer.CustomerID).First();
                x.CustomerName = modifiedCustomer.CustomerName;
                x.Address = modifiedCustomer.Address;
                x.Phone = modifiedCustomer.Phone;
                x.Email = modifiedCustomer.Email;
                x.CustomerDescription = modifiedCustomer.CustomerDescription;
                
                if (!SaveImageToFile(x.CustomerID.ToString(), imageFolder, ImageType.Customer))
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                    return result;
                }

                appDBContext.SaveChanges();
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
            }

            return result;
        }
    }
}
