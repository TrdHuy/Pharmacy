using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage.OVs
{
    public class MSW_CMP_CTP_CBP_MedicineOV : BaseViewModel
    {
        private tblMedicine _currentSelectedMedicine;
        private string _medicineTextSearch;
        private string _quantity;
        private decimal _paidAmount;

        public string[] MedicineFilterPathList { get; set; } = new string[] { "MedicineName", "MedicineID" };

        public MSW_CMP_CTP_CBP_MedicineOV(BaseViewModel parentVM) : base(parentVM)
        {
            PaidAmount = (ParentsModel as CustomerBillPageViewModel).CurrentCustomerOrder.PurchasePrice;
        }

        [Bindable(true)]
        public string MedicineTextSearch
        {
            get
            {
                return _medicineTextSearch;
            }
            set
            {
                
                _medicineTextSearch = value;
                InvalidateOwn();
            }
        }

       
        [Bindable(true)]
        public tblMedicine CurrentSelectedMedicine
        {
            get
            {
                return _currentSelectedMedicine;
            }
            set
            {
                _currentSelectedMedicine = value;
                InvalidateOwn();
            }
        }
        
        [Bindable(true)]
        public string Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                InvalidateOwn();
            }
        }
       
        [Bindable(true)]
        public decimal MedicineCost
        {
            get
            {
                if ((ParentsModel as CustomerBillPageViewModel).CurrentOrderDetails.Count > 0)
                {
                    var cost = (ParentsModel as CustomerBillPageViewModel).CurrentOrderDetails.Sum(o => o.TotalPrice);
                    return Convert.ToDecimal(cost);
                }
                return 0;
            }
            set
            {

            }
        }

        [Bindable(true)]
        public decimal DebtCost
        {
            get
            {
                // Công nợ phải được tính theo các hóa đơn trước hóa đơn hiện tại
                if ((ParentsModel as CustomerBillPageViewModel).CurrentCustomerOrder != null)
                {

                    var currentOrder = (ParentsModel as CustomerBillPageViewModel).CurrentCustomerOrder;

                    var totalCost = (ParentsModel as CustomerBillPageViewModel).CurrentCustomerOrder.tblCustomer.tblOrders
                        .Where((od) => od.IsActive)
                        .Sum(x =>
                        {
                            if (DateTime.Compare(x.OrderTime, currentOrder.OrderTime) < 0)
                            {
                                return x.TotalPrice;
                            }
                            return 0;
                        });

                    var purchaseCost = (ParentsModel as CustomerBillPageViewModel).CurrentCustomerOrder.tblCustomer.tblOrders
                        .Where((od) => od.IsActive)
                        .Sum(x =>
                    {
                        if (DateTime.Compare(x.OrderTime, currentOrder.OrderTime) < 0)
                        {
                            return x.PurchasePrice;
                        }
                        return 0;
                    });
                    var debt = totalCost - purchaseCost;
                    return Convert.ToDecimal(debt);
                }

                return 0;
            }
            set
            {
            }
        }

        [Bindable(true)]
        public decimal TotalCost
        {
            get
            {
                return DebtCost + MedicineCost;
            }
            set
            {

            }
        }
        
        [Bindable(true)]
        public decimal PaidAmount
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

        [Bindable(true)]
        public decimal RestAmount
        {
            get
            {
                return TotalCost - PaidAmount;
            }
            set
            {

            }
        }

        public override void RefreshViewModel()
        {
            _medicineTextSearch = String.Empty;
            _currentSelectedMedicine = null;
        }
    }
}
