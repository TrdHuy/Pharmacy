using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class GetAllActiveSuppliersOfMedicineDataAction : AbstractQueryAction
    {
        public GetAllActiveSuppliersOfMedicineDataAction()
        {
            _action = GetAllActiveSuppliersOfMedicineData;
        }

        private SQLQueryResult GetAllActiveSuppliersOfMedicineData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var medicineID = paramaters[0] as string;
                List<tblMedicineSupplier> lstOutput = appDBContext.tblMedicineSuppliers
                        .Where(o => o.IsActive && o.MedicineID == medicineID)
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
