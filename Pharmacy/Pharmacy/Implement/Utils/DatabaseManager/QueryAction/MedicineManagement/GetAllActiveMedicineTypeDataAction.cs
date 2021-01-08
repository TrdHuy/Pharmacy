using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction
{
    public class GetAllActiveMedicineTypeDataAction: AbstractQueryAction
    {
        public GetAllActiveMedicineTypeDataAction()
        {
            _action = GetAllActiveMedicineTypeData;
        }

        private SQLQueryResult GetAllActiveMedicineTypeData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                List<tblMedicineType> lstOutput;

                lstOutput = appDBContext.tblMedicineTypes.ToList();
                result = new SQLQueryResult(lstOutput, MessageQueryResult.Finished);
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
            }
            return result;
        }
    }
}
