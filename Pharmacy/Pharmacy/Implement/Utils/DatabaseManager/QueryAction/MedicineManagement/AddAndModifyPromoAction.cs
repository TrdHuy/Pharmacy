using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class AddAndModifyPromoAction : AbstractQueryAction
    {
        public AddAndModifyPromoAction()
        {
            _action = AddAndModifyPromo;
        }

        private SQLQueryResult AddAndModifyPromo(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblPromo promoInfo = paramaters[0] as tblPromo;
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                tblPromo promo = appDBContext.tblPromoes.Where(o => o.CustomerID == promoInfo.CustomerID && o.MedicineID == promoInfo.MedicineID).FirstOrDefault();
                if (promo == null) //Promo is not exist
                {
                    appDBContext.tblPromoes.Add(promoInfo);
                }
                else //Promo deleted or Update Promo info
                {
                    promo.PromoPercent = promoInfo.PromoPercent;
                    promo.PromoDescription = promoInfo.PromoDescription;
                    promo.IsActive = true;
                }
                appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
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
