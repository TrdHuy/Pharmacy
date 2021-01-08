using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class IsMedicineIDExistedAction : AbstractQueryAction
    {
        public IsMedicineIDExistedAction()
        {
            _action = IsMedicineIDExisted;
        }

        private SQLQueryResult IsMedicineIDExisted(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                string id = paramaters[0] as string;
                tblMedicine medicine = appDBContext.tblMedicines.Where(o => o.MedicineID == id).FirstOrDefault();

                result = new SQLQueryResult(medicine == null ? false : true, MessageQueryResult.Finished);
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen);
            }
            return result;
        }
    }
}
