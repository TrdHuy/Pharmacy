using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.OVs
{
    public class MSW_SMP_SupplierDebtOV : BaseViewModel
    {
        public long ImportID { get; set; }
        public DateTime ImportTime { get; set; }
        public string DebtType { get; set; }
        public decimal PurchasedDebt { get; set; }
        public string Description { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }
    }
}
