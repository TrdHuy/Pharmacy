using Pharmacy.Base.MVVM.ViewModels;
using System;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.SupplierDebt.OVs
{
    public class MSW_SMP_SDP_SupplierDebtOV : BaseViewModel
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
