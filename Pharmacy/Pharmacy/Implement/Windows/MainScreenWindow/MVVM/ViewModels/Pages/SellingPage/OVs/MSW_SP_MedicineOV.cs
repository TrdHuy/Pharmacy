using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage.OVs
{
    public class MSW_SP_MedicineOV : AbstractViewModel
    {
        private AbstractViewModel _parentModel;
        private tblMedicine _curSelectedMedicine;
        private double _paidAmount;

        public string[] MedicineFilterPathList { get; set; } = new string[] { "MedicineName", "MedicineID" };
        public string Quantity { get; set; }
        public double MedicineCost
        {
            get
            {
                if ((_parentModel as SellingPageViewModel).CustomerOrderDetailItemSource.Count > 0)
                {
                    var cost = (_parentModel as SellingPageViewModel).CustomerOrderDetailItemSource.Sum(o => o.TotalPrice);
                    return Convert.ToDouble(cost);
                }
                return 0;
            }
            set
            {

            }
        }
        public double DebtCost
        {
            get
            {
                if ((_parentModel as SellingPageViewModel).CustomerOV.CurrentSelectedCustomer != null)
                {
                    var totalCost = (_parentModel as SellingPageViewModel).CustomerOV.CurrentSelectedCustomer.tblOrders.Sum(o => o.TotalPrice);
                    var purchaseCost = (_parentModel as SellingPageViewModel).CustomerOV.CurrentSelectedCustomer.tblOrders.Sum(o => o.PurchasePrice);
                    var debt = totalCost - purchaseCost;
                    return Convert.ToDouble(debt);
                }

                return 0;
            }
            set
            {
            }
        }
        public double TotalCost
        {
            get
            {
                return DebtCost + MedicineCost;
            }
            set
            {

            }
        }
        public double PaidAmount
        {
            get
            {
                return _paidAmount;
            }
            set
            {
                _paidAmount = value;
                InvalidateOwn();
                Invalidate("RestAmount");
            }
        }
        public double RestAmount
        {
            get
            {
                return TotalCost - PaidAmount;
            }
            set
            {

            }
        }

        public tblMedicine CurrentSelectedMedicine
        {
            get
            {
                return _curSelectedMedicine;
            }
            set
            {
                _curSelectedMedicine = value;
                InvalidateOwn();
            }
        }

        public MSW_SP_MedicineOV(AbstractViewModel parentVM)
        {
            _parentModel = parentVM;
        }

        protected override void InitPropertiesRegistry()
        {
        }

        public void RefreshViewModel()
        {
            MedicineCost = 0;
            PaidAmount = 0;
            CurrentSelectedMedicine = null;
            Quantity = "";

            Invalidate("DebtCost");
            Invalidate("TotalCost");
            Invalidate("RestAmount");
        }
    }
}
