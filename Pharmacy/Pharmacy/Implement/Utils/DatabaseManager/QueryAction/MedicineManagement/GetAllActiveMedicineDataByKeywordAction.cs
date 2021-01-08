using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class GetAllActiveMedicineDataByKeywordAction :AbstractQueryAction
    {
        public GetAllActiveMedicineDataByKeywordAction()
        {
            _action = GetAllActiveMedicineDataByKeyword;
        }

        private SQLQueryResult GetAllActiveMedicineDataByKeyword(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                string content = paramaters[0] as string;
                List<int> lstMedicineType = paramaters[1] as List<int>;
                List<tblMedicine> lstOutput;

                if (content.Length == 0)
                {
                    lstOutput = appDBContext.tblMedicines
                        .Where(o => o.IsActive && lstMedicineType.Contains(o.MedicineTypeID))
                        .ToList();
                }
                else
                {
                    lstOutput = appDBContext.tblMedicines
                        .Where(o => o.IsActive
                        && (o.MedicineName.Contains(content) || o.MedicineID.Contains(content))
                        && lstMedicineType.Contains(o.MedicineTypeID))
                        .ToList();
                }
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
