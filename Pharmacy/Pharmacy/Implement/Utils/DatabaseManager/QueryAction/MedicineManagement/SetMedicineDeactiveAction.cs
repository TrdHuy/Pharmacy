using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class SetMedicineDeactiveAction : AbstractQueryAction
    {
        public SetMedicineDeactiveAction()
        {
            _action = SetMedicineDeactive;
        }

        private SQLQueryResult SetMedicineDeactive(PharmacyDBContext appDBContext, object[] paramaters)
        {
            string id = paramaters[0].ToString();
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblMedicines.Where(user => user.MedicineID.Equals(id)).
                    First();
                x.IsActive = false;
                appDBContext.SaveChanges();
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
