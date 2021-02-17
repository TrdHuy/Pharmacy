using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.SupplierManagement
{
    public class ModifySupplierAction : AbstractQueryAction
    {
        public ModifySupplierAction()
        {
            _action = ModifySupplier;
        }

        private SQLQueryResult ModifySupplier(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblSupplier supplier = paramaters[0] as tblSupplier;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                var item = appDBContext.tblSuppliers.Where(o => o.SupplierID == supplier.SupplierID).FirstOrDefault();
                item.SupplierName = supplier.SupplierName;
                item.Phone = supplier.Phone;
                item.Email = supplier.Email;
                item.Address = supplier.Address;
                item.SupplierDescription = supplier.SupplierDescription;
                item.IsActive = true;

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
