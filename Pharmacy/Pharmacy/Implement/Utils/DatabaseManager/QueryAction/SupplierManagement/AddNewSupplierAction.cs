using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.SupplierManagement
{
    public class AddNewSupplierAction : AbstractQueryAction
    {
        public AddNewSupplierAction()
        {
            _action = AddNewSupplier;
        }

        private SQLQueryResult AddNewSupplier(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblSupplier supplier = paramaters[0] as tblSupplier;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                var item = appDBContext.tblSuppliers.Where(o => o.SupplierName.Equals(supplier.SupplierName, StringComparison.OrdinalIgnoreCase)
                && o.Phone.Equals(supplier.Phone, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (item != null)
                {
                    item.Email = supplier.Email;
                    item.Address = supplier.Address;
                    item.SupplierDescription = supplier.SupplierDescription;
                    item.IsActive = true;
                }
                else
                {
                    appDBContext.tblSuppliers.Add(supplier);
                }
                appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
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
