using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.WarehouseManagement
{
    public class SetWarehouseImportDeactiveAction : AbstractQueryAction
    {
        public SetWarehouseImportDeactiveAction()
        {
            _action = SetWarehouseImportDeactive;
        }

        private SQLQueryResult SetWarehouseImportDeactive(PharmacyDBContext appDBContext, object[] paramaters)
        {
            long id = (long)paramaters[0];
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblWarehouseImports.Where(o => o.ImportID == id).FirstOrDefault();
                x.IsActive = false;
                foreach (var item in x.tblWarehouseImportDetails)
                {
                    item.IsActive = false;
                }
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
