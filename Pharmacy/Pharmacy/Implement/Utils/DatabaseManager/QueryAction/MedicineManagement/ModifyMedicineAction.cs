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
    public class ModifyMedicineAction : AbstractQueryAction
    {
        public ModifyMedicineAction()
        {
            _action = ModifyMedicine;
        }

        private SQLQueryResult ModifyMedicine(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblMedicine updateMedicine = paramaters[0] as tblMedicine;
            string imageFolder = paramaters[1] as string;
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                tblMedicine currentMedicine = MSW_DataFlowHost.Current.CurrentModifiedMedicine;
                tblMedicine medicine = appDBContext.tblMedicines.Where(o => o.MedicineID == currentMedicine.MedicineID).FirstOrDefault();
                medicine.MedicineName = updateMedicine.MedicineName;
                medicine.MedicineTypeID = updateMedicine.MedicineTypeID;
                medicine.MedicineUnitID = updateMedicine.MedicineUnitID;
                medicine.SupplierID = updateMedicine.SupplierID;
                medicine.BidPrice = updateMedicine.BidPrice;
                medicine.AskingPrice = updateMedicine.AskingPrice;
                medicine.MedicineDescription = updateMedicine.MedicineDescription;
                medicine.IsActive = true;

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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }
    }
}
