using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs
{
    public class MSW_WHMP_WarehouseImportDetailOV : BaseViewModel
    {
        public string MedicineName { get; set; }
        public string MedicineID { get; set; }
        public string MedicineUnitName { get; set; }
        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
