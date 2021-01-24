using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.WarehouseManagement
{
    public class AddNewWarehouseImportAction : AbstractQueryAction
    {
        public AddNewWarehouseImportAction()
        {
            _action = AddNewWarehouseImport;
        }

        private SQLQueryResult AddNewWarehouseImport(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblWarehouseImport import = paramaters[0] as tblWarehouseImport;
            string imageFolder = paramaters[1] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                appDBContext.tblWarehouseImports.Add(import);
                appDBContext.SaveChanges();
                if (imageFolder.Length > 0 && !SaveImageToFile(import.ImportID.ToString(), imageFolder, ImageType.WarehouseImport))
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                    return result;
                }
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
