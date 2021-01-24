using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class SetPromoDeactiveAction : AbstractQueryAction
    {
        public SetPromoDeactiveAction()
        {
            _action = SetPromoDeactive;
        }

        private SQLQueryResult SetPromoDeactive(PharmacyDBContext appDBContext, object[] paramaters)
        {
            int id = (int)paramaters[0];
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblPromoes.Where(o => o.PromoID == id).FirstOrDefault();
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
