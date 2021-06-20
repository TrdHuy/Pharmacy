using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs
{
    public class CustomerOrderOV : BaseViewModel
    {
        private tblOrder _customerOrder;

        public CustomerOrderOV(tblOrder order)
        {
            _customerOrder = order;
        }

        public string OrderTime
        {
            get
            {
                return _customerOrder.OrderTime.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public string CustomerName
        {
            get
            { return _customerOrder.tblCustomer.CustomerName; }
        }

        public string OrderDetail
        {
            get
            {
                var val = _customerOrder.tblOrderDetails;
                string result = "";
                result += string.Join("\n", val.Where((content) => content.IsActive).
                        Select((content) => content.tblMedicine.MedicineName));
                return result;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return _customerOrder.TotalPrice;
            }
        }

        public tblOrder Order
        {
            get
            {
                return _customerOrder;
            }
        }
    }
}
