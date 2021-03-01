using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs
{
    public class OrderDetailOV : BaseViewModel
    {
        private string _medicineName;
        private string _medicineID;
        private string _medicineUnitName;
        private double _quantity;
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
                InvalidateOwn();
            }
        }
    }
}
