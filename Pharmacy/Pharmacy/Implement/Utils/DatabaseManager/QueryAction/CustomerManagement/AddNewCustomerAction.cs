using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.CustomerManagement
{
    public class AddNewCustomerAction : AbstractQueryAction
    {
        public AddNewCustomerAction()
        {
            _action = AddNewCustomer;
        }

        private SQLQueryResult AddNewCustomer(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblCustomer newCustomer = paramaters[0] as tblCustomer;
            string imageFolder = paramaters[1] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                appDBContext.tblCustomers.Add(newCustomer);

                if(!SaveImageToFile((appDBContext.tblCustomers.ToList().Count + 1).ToString(), imageFolder, ImageType.Customer))
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                    return result;
                }

                appDBContext.SaveChanges();
                result = new SQLQueryResult(newCustomer, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
            }

            return result;
        }
    }
}
