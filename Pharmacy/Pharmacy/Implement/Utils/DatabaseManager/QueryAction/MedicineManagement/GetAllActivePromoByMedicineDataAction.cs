using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class GetAllActivePromoByMedicineDataAction : AbstractQueryAction
    {
        public GetAllActivePromoByMedicineDataAction()
        {
            _action = GetAllActivePromoByMedicineData;
        }

        private SQLQueryResult GetAllActivePromoByMedicineData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                string MedicineID = paramaters[0] as string;
                List<tblPromo> lstOutput = appDBContext.tblPromoes.Where(o => o.MedicineID == MedicineID && o.IsActive).ToList();
                result = new SQLQueryResult(lstOutput, MessageQueryResult.Done);
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
            }
            return result;
        }
    }
}
