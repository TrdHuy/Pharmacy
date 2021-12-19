using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement.OVs
{
    public class MSW_MMP_MedicineOV : BaseViewModel
    {
        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string MedicineTypeName { get; set; }
        public int MedicineTypeID { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskingPrice { get; set; }
        public string Suppliers { get; set; }

        public MSW_MMP_MedicineOV(string medicineID, string medicineName, string medicineTypeName, int medicineTypeID, decimal bidPrice, decimal askingPrice, List<tblMedicineSupplier> lstSuppliers)
        {
            MedicineID = medicineID;
            MedicineName = medicineName;
            MedicineTypeID = medicineTypeID;
            MedicineTypeName = medicineTypeName;
            BidPrice = bidPrice;
            AskingPrice = askingPrice;

            if (lstSuppliers.Where(o => o.IsActive).Count() > 0)
            {
                StringBuilder supplier = new StringBuilder();
                foreach (var item in lstSuppliers.Where(o => o.IsActive))
                {
                    supplier.Append(item.tblSupplier.SupplierName + ", ");
                }
                Suppliers = supplier.ToString(0, supplier.Length - 2);
            }
            else
            {
                Suppliers = "";
            }
        }
    }
}
