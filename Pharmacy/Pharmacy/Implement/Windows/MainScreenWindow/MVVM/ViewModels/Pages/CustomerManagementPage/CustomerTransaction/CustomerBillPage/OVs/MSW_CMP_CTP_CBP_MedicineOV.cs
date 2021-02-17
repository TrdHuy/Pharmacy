using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage.OVs
{
    public class MSW_CMP_CTP_CBP_MedicineOV : AbstractViewModel
    {
        private tblMedicine _currentSelectedMedicine;
        private string _medicineTextSearch;
        private string _quantity;
        private AbstractViewModel _parentVM;
        private decimal _paidAmount;

        public string[] MedicineFilterPathList { get; set; } = new string[] { "MedicineName", "MedicineID" };

        public MSW_CMP_CTP_CBP_MedicineOV(AbstractViewModel parentVM)
        {
            _parentVM = parentVM;
        }
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
        public decimal MedicineCost
        {
            get
            {
                if ((_parentVM as CustomerBillPageViewModel).CurrentOrderDetails.Count > 0)
                {
                    var cost = (_parentVM as CustomerBillPageViewModel).CurrentOrderDetails.Sum(o => o.TotalPrice);
                    return Convert.ToDecimal(cost);
                }
                return 0;
            }
            set
            {

            }
        }
        public decimal DebtCost
        {
            get
            {
                // Công nợ phải được tính theo các hóa đơn trước hóa đơn hiện tại
                if ((_parentVM as CustomerBillPageViewModel).CurrentCustomerOrder != null)
                {

                    var currentOrder = (_parentVM as CustomerBillPageViewModel).CurrentCustomerOrder;

                    var totalCost = (_parentVM as CustomerBillPageViewModel).CurrentCustomerOrder.tblCustomer.tblOrders
                        .Where((od) => od.IsActive)
                        .Sum(x =>
                        {
                            if (DateTime.Compare(x.OrderTime, currentOrder.OrderTime) < 0)
                            {
                                return x.TotalPrice;
                            }
                            return 0;
                        });

                    var purchaseCost = (_parentVM as CustomerBillPageViewModel).CurrentCustomerOrder.tblCustomer.tblOrders
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

    }
}
