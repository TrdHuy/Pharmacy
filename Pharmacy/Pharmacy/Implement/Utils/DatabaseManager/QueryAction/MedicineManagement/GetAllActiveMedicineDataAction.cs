using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class GetAllActiveMedicineDataAction :AbstractQueryAction
    {
        public GetAllActiveMedicineDataAction()
        {
            _action = GetAllActiveMedicineData;
        }

        private SQLQueryResult GetAllActiveMedicineData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                List<tblMedicine> lstOutput = appDBContext.tblMedicines
                        .Where(o => o.IsActive)
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
