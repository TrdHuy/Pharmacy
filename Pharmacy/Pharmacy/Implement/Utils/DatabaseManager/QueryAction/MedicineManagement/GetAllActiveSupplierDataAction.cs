using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class GetAllActiveSupplierDataAction : AbstractQueryAction
    {
        public GetAllActiveSupplierDataAction()
        {
            _action = GetAllActiveSupplierData;
        }

        private SQLQueryResult GetAllActiveSupplierData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                List<tblSupplier> lstOutput = appDBContext.tblSuppliers.Where(o => o.IsActive).OrderBy(o => o.SupplierName).ToList();
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
