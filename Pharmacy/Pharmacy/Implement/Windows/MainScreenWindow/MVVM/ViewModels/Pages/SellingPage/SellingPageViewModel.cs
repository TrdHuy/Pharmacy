using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage
{
    public class SellingPageViewModel : AbstractViewModel
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private string _customerName;
        private string _customerPhone;
        private tblCustomer _curSelectedCustomer;
        private tblMedicine _curSelectedMedicine;
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private string _customerAddress;
        private double _paidAmount;
        private bool _isAddOrderDeatailButtonRunning = false;

        public ObservableCollection<tblCustomer> CustomerItemSource { get; set; }
        public ObservableCollection<tblMedicine> MedicineItemSource { get; set; }
        public ObservableCollection<OrderDetailVO> CustomerOrderDetailItemSource { get; set; }
        public RunInputCommand AddOrderDetailCommand { get; set; }
        public RunInputCommand RemoveOrderDetailCommand { get; set; }
        public RunInputCommand InstantiateOrderCommand { get; set; }
        public string[] MedicineFilterPathList { get; set; } = new string[] { "MedicineName", "MedicineID" };
        public bool ForceAssignCurentSelectedUser { get; set; }

        public bool IsAddOrderDeatailButtonRunning
        {
            get
            {

                return _isAddOrderDeatailButtonRunning;
            }
            set
            {
                _isAddOrderDeatailButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }
        public string Quantity { get; set; }
        public double MedicineCost
        {
            get
            {
                if (CustomerOrderDetailItemSource.Count > 0)
                {
                    var cost = CustomerOrderDetailItemSource.Sum(o => o.TotalPrice);
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
                if (CurrentSelectedCustomer != null)
                {
                    var totalCost = CurrentSelectedCustomer.tblOrders.Sum(o => o.TotalPrice);
                    var purchaseCost = CurrentSelectedCustomer.tblOrders.Sum(o => o.PurchasePrice);
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

        public bool IsCustomerChooserEnable
        {
            get
            {
                return CustomerOrderDetailItemSource.Count <= 0;
            }
        }
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
                Invalidate("DebtCost");
                Invalidate("IsAdressTextBoxEnable");
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
        public bool IsAdressTextBoxEnable
        {
            get
            {
                return CurrentSelectedCustomer == null && IsCustomerChooserEnable;
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

        public bool IsAddOrderDetailCanPerform
        {
            get
            {
                return CurrentSelectedMedicine != null
                    && !String.IsNullOrEmpty(CustomerPhone)
                    && !String.IsNullOrEmpty(CustomerName);
            }
        }

        protected override void InitPropertiesRegistry()
        {
        }

        public SellingPageViewModel()
        {
            InstantiateItems();
            AddOrderDetailCommand = new RunInputCommand(AddOrderDetailButtonClickEvent);
            RemoveOrderDetailCommand = new RunInputCommand(RemoveOrderDetailButtonClickEvent);
            InstantiateOrderCommand = new RunInputCommand(InstantiateOrderButtonClickEvent);
        }

        public void RefreshViewModel(bool re_Customer = true, bool re_Medicine = true)
        {
            MedicineCost = 0;
            PaidAmount = 0;

            if (re_Medicine)
            {
                CurrentSelectedMedicine = null;
                CustomerOrderDetailItemSource.Clear();
                Quantity = "";

            }

            if (re_Customer)
            {
                CurrentSelectedCustomer = null;
                CustomerName = "";
                CustomerPhone = "";
                CustomerAddress = "";
            }

            Invalidate("DebtCost");
            Invalidate("TotalCost");
            Invalidate("RestAmount");
            Invalidate("IsCustomerChooserEnable");
            Invalidate("IsAdressTextBoxEnable");

        }

        private void InstantiateOrderButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SP_INSTANTIATE_BUTTON
                , dataTransfer);
        }

        private void RemoveOrderDetailButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SP_REMOVE_BUTTON
                , dataTransfer);
        }

        private void AddOrderDetailButtonClickEvent(object paramaters)
        {
            IsAddOrderDeatailButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SP_ADD_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }

        private void InstantiateItems()
        {
            InstantiateCustomerItems();
            InstantiateMedicineItems();
            InstantiateCustomerOrderDetailItems();

        }

        private void InstantiateCustomerOrderDetailItems()
        {
            CustomerOrderDetailItemSource = new ObservableCollection<OrderDetailVO>();
            CustomerOrderDetailItemSource.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CustomerOrderList_CollectionChanged);

        }

        private void CustomerOrderList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Invalidate("IsCustomerChooserEnable");
            Invalidate("MedicineCost");
            Invalidate("TotalCost");
            Invalidate("RestAmount");
        }

        private void InstantiateMedicineItems()
        {
            MedicineItemSource = new ObservableCollection<tblMedicine>();
            _sqlCmdObserver = new SQLQueryCustodian(SQLGetMedicineQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void InstantiateCustomerItems()
        {
            CustomerItemSource = new ObservableCollection<tblCustomer>();
            _sqlCmdObserver = new SQLQueryCustodian(SQLGetCustomerQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_CMD_KEY
                    , _sqlCmdObserver);
        }


        private void SQLGetCustomerQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                var listCustomers = queryResult.Result as List<tblCustomer>;
                CustomerItemSource = new ObservableCollection<tblCustomer>(listCustomers);
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi load dữ liệu khách hàng, vui lòng mở lại ứng dụng hoặc liên hệ CSKH để biết thêm thông tin!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
            }
        }

        private void SQLGetMedicineQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                MedicineItemSource = new ObservableCollection<tblMedicine>(queryResult.Result as List<tblMedicine>);
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi load dữ liệu thuốc, vui lòng mở lại ứng dụng hoặc liên hệ CSKH để biết thêm thông tin!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
            }
        }

    }

    public class OrderDetailVO : AbstractViewModel
    {

        public string MedicineName { get; set; }
        public string MedicineID { get; set; }
        public string MedicineUnitName { get; set; }
        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public double PromoPercent { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }
    }
}
