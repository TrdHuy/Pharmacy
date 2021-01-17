using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.WarehouseManagement
{
    public class GetAllActiveWarehouseImportDataAction : AbstractQueryAction
    {
        public GetAllActiveWarehouseImportDataAction()
        {
            _action = GetAllActiveWarehouseImportData;
        }

        private SQLQueryResult GetAllActiveWarehouseImportData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                List<tblWarehouseImport> lstOutput = appDBContext.tblWarehouseImports
                        .Where(o => o.IsActive)
                        .OrderByDescending(o=>o.ImportTime)
                        .ToList();
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
