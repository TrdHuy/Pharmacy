using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerDebtsPage.OVs
{
    public class MSW_CMP_CTP_CDP_CustomerDebtOV : BaseViewModel
    {
        public long OrderID { get; set; }
        public DateTime OrderTime { get; set; }
        public string DebtType { get; set; }
        public decimal PurchasedDebt { get; set; }
        public string Description { get; set; }

    }
}
