using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs
{
    public class OrderDetailOV : BaseViewModel
    {
        public event NotifyOrderDetailPropertyChanged OnOrderDetailPropertyChanged;

        private string _medicineName;
        private string _medicineID;
        private string _medicineUnitName;
        private double _quantity;
        private string _quantityToString;
        private string _promoPercentToString;

        private decimal _unitPrice;
        private decimal _totalPrice;
        private double _promoPercent;

        public long OrderDetailID { get; set; } = -1;
        public string MedicineName
        {
            get
            {
                return _medicineName;
            }
            set
            {
                _medicineName = value;
                InvalidateOwn();
            }
        }
        public string MedicineID
        {
            get
            {
                return _medicineID;
            }
            set
            {
                _medicineID = value;
                InvalidateOwn();
            }
        }
        public string MedicineUnitName
        {
            get
            {
                return _medicineUnitName;
            }
            set
            {
                _medicineUnitName = value;
                InvalidateOwn();
            }
        }
        public double Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                UpdateTotalPrice();
                InvalidateOwn();
            }
        }

        public string QuantityToString
        {
            get
            {
                return _quantityToString;
            }
            set
            {
                _quantityToString = value;
                try
                {
                    Quantity = Convert.ToDouble(_quantityToString);
                }
                catch
                {
                    Quantity = 0d;
                }
                InvalidateOwn();
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                _unitPrice = value;
                UpdateTotalPrice();
                InvalidateOwn();
            }
        }
        public decimal TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
                InvalidateOwn();
            }
        }
        public double PromoPercent
        {
            get
            {
                return _promoPercent;
            }
            set
            {
                _promoPercent = value;
                UpdateTotalPrice();
                InvalidateOwn();
            }
        }
        public string PromoPercentToString
        {
            get
            {
                return _promoPercentToString;
            }
            set
            {
                _promoPercentToString = value;
                try
                {
                    PromoPercent = Convert.ToDouble(_promoPercentToString);
                }
                catch
                {
                    PromoPercent = 0d;
                }
                InvalidateOwn();
            }
        }
        public decimal UnitBidPrice { get; set; }

        private void UpdateTotalPrice()
        {
            var oldValue = TotalPrice;
            var newValue = Convert.ToDecimal(Convert.ToDouble(_quantity) *
                  Convert.ToDouble(_unitPrice) *
                  (100 - _promoPercent) / 100);
            TotalPrice = newValue;

            if (OnOrderDetailPropertyChanged != null)
            {
                OnOrderDetailPropertyChanged(this, new OrderDetailPropertyChangedEventArgs(
                    oldValue,
                    newValue,
                    TotalPrice.GetType().Name));
            }
        }
    }

    public delegate void NotifyOrderDetailPropertyChanged(object sender, OrderDetailPropertyChangedEventArgs e);
    public class OrderDetailPropertyChangedEventArgs : RoutedEventArgs
    {
        private object newValue;
        private object oldValue;
        private string propertyName;

        public OrderDetailPropertyChangedEventArgs(object oldVal, object newVal, string propName)
        {
            propertyName = propName;
            newValue = newVal;
            oldValue = oldVal;
        }

        public object OldValue
        {
            get
            {
                return oldValue;
            }
        }
        public object NewValue
        {
            get
            {
                return newValue;
            }
        }
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
        }
    }
}
