using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage.OVs
{
    public class MSW_SP_CustomerOV : AbstractViewModel
    {
        private AbstractViewModel _parentModel;
        private string _customerName;
        private string _customerAddress;
        private string _customerPhone;
        private tblCustomer _curSelectedCustomer;

        public tblCustomer CurrentSelectedCustomer
        {
            get
            {
                return _curSelectedCustomer;
            }
            set
            {
                _curSelectedCustomer = value;

                InvalidateOwn();
                Invalidate("CustomerAddress");
                Invalidate((_parentModel as SellingPageViewModel).MedicineOV, "DebtCost");
                Invalidate("IsAdressTextBoxEnable");
            }
        }
        public string CustomerPhone
        {
            get
            {
                return _customerPhone;
            }
            set
            {
                _customerPhone = value;
                InvalidateOwn();
                Invalidate("IsAdressTextBoxEnable");

            }
        }
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
                InvalidateOwn();
                Invalidate("IsAdressTextBoxEnable");

            }
        }
        public string CustomerAddress
        {
            get
            {
                if (CurrentSelectedCustomer != null)
                {
                    return CurrentSelectedCustomer.Address;
                }
                else
                {
                    return _customerAddress;
                }
            }
            set
            {
                _customerAddress = value;
                InvalidateOwn();
            }
        }
        public bool IsAdressTextBoxEnable
        {
            get
            {
                return CurrentSelectedCustomer == null && IsCustomerChooserEnable;
            }
        }
        public bool IsCustomerChooserEnable
        {
            get
            {
                return (_parentModel as SellingPageViewModel).CustomerOrderDetailItemSource.Count <= 0;
            }
        }

        public MSW_SP_CustomerOV(AbstractViewModel parentVM)
        {
            _parentModel = parentVM;
        }

        public void RefreshViewModel()
        {
            CurrentSelectedCustomer = null;
            CustomerName = "";
            CustomerPhone = "";
            CustomerAddress = "";
            Invalidate("IsCustomerChooserEnable");
            Invalidate("IsAdressTextBoxEnable");
        }

        protected override void InitPropertiesRegistry()
        {
        }
    }
}
