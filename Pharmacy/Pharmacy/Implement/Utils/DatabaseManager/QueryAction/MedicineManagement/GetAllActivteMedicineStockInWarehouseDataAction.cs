using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class GetAllActivteMedicineStockInWarehouseDataAction : AbstractQueryAction
    {
        public GetAllActivteMedicineStockInWarehouseDataAction()
        {
            _action = GetAllActivteMedicineStockInWarehouseData;
        }

        private SQLQueryResult GetAllActivteMedicineStockInWarehouseData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            string id = paramaters[0].ToString();
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                List<tblWarehouseImportDetail> lstOutput = new List<tblWarehouseImportDetail>();

                foreach (var import in appDBContext.tblWarehouseImports.Where(o=>o.IsActive).OrderByDescending(o=>o.ImportTime))
                {
                    foreach (var stock in import.tblWarehouseImportDetails.Where(o=>o.MedicineID==id && o.IsActive))
                    {
                        lstOutput.Add(stock);
                    }
                }
                result = new SQLQueryResult(lstOutput, MessageQueryResult.Done);
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
