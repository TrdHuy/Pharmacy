using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.MedicineManagement
{
    public class AddNewMedicineAction : AbstractQueryAction
    {
        public AddNewMedicineAction()
        {
            _action = AddNewMedicine;
        }

        private SQLQueryResult AddNewMedicine(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblMedicine medicine = paramaters[0] as tblMedicine;
            string imageFolder = paramaters[1] as string;
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                appDBContext.tblMedicines.Add(medicine);
                if (!SaveImageToFile(medicine.MedicineID, imageFolder, ImageType.Medicine))
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                    return result;
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
