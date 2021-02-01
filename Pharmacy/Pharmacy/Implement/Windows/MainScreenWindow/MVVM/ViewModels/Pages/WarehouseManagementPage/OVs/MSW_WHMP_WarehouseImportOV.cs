using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs
{
    public class MSW_WHMP_WarehouseImportOV : AbstractViewModel
    {
        public long ImportID { get; set; }
        public DateTime ImportTime { get; set; }
        public bool IsActive { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string ImportDescription { get; set; }
        public List<tblWarehouseImportDetail> tblWarehouseImportDetails { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }
    }
}
