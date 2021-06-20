using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Observable;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage
{
    internal class CustomerBillPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("CustomerBillPageViewModel");

        private SQLQueryCustodian _sqlCmdObserver;
        private bool _isEnableEdittingBill;
        private Visibility _deleteColumnVisibility;
        private ObservablePropertiesCollection<OrderDetailOV> _currentOrderDetails;

        [Bindable(true)]
        public tblOrder CurrentCustomerOrder { get; set; }

        [Bindable(true)]
        public MSW_CMP_CTP_CBP_MedicineOV MedicineOV { get; set; }

        [Bindable(true)]
        public MSW_CMP_CTP_CBP_ButtonCommandOV ButtonCommandOV { get; set; }

        [Bindable(true)]
        public ObservableCollection<tblMedicine> MedicineItemSource { get; set; }

        public bool IsOrderDetailsModified { get; private set; }
        public bool IsOrderModified
        {
            get
            {
                return IsOrderDetailsModified ||
                    CurrentCustomerOrder.PurchasePrice != MedicineOV?.PaidAmount;
            }
        }

        [Bindable(true)]
        public ObservablePropertiesCollection<OrderDetailOV> CurrentOrderDetails
        {
            get
            {
                return _currentOrderDetails;
            }
            set
            {
                _currentOrderDetails = value;
                InvalidateOwn();
            }
        }

        public bool IsAddOrderDetailCanPerform
        {
            get
            {
                return MedicineOV.CurrentSelectedMedicine != null
                    && !String.IsNullOrEmpty(MedicineOV.Quantity)
                    && !MedicineOV.Quantity.Equals("0");
            }
        }

        [Bindable(true)]
        public bool IsEnableEdittingBill
        {
            get
            {
                return _isEnableEdittingBill;
            }
            set
            {
                _isEnableEdittingBill = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
        public Visibility DeleteColumnVisibility
        {
            get
            {
                return _deleteColumnVisibility;
            }
            set
            {
                _deleteColumnVisibility = value;
                InvalidateOwn();
            }
        }

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CurrentCustomerOrder = MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder;

            MedicineOV = new MSW_CMP_CTP_CBP_MedicineOV(this);
            ButtonCommandOV = new MSW_CMP_CTP_CBP_ButtonCommandOV(this);

            DeleteColumnVisibility = IsEnableEdittingBill ? Visibility.Visible : Visibility.Collapsed;

            InstantiateItems();
        }

        protected override void OnInitialized()
        {
        }

        public override void OnLoaded(object sender)
        {
            CurrentCustomerOrder = MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder;
            DeleteColumnVisibility = IsEnableEdittingBill ? Visibility.Visible : Visibility.Collapsed;
            InstantinateOrderDetailItems();

            RefreshViewModel();
        }

        public void RefreshListOrder()
        {
            InstantinateOrderDetailItems();
            MedicineOV.PaidAmount = CurrentCustomerOrder.PurchasePrice;
            Invalidate("MedicineOV");
        }

        private void InstantiateItems()
        {
            InstantinateMedicineItems();
            InstantinateOrderDetailItems();
        }

        private void InstantinateOrderDetailItems()
        {
            IsOrderDetailsModified = false;
            CurrentOrderDetails = new ObservablePropertiesCollection<OrderDetailOV>();
            foreach (tblOrderDetail orderDetail in CurrentCustomerOrder.tblOrderDetails)
            {
                if (orderDetail.IsActive)
                {
                    var oDOV = new OrderDetailOV()
                    {
                        OrderDetailID = orderDetail.OrderDetailID,
                        MedicineName = orderDetail.tblMedicine.MedicineName,
                        MedicineID = orderDetail.tblMedicine.MedicineID,
                        MedicineUnitName = orderDetail.tblMedicine.tblMedicineUnit.MedicineUnitName,
                        Quantity = orderDetail.Quantity,
                        UnitPrice = orderDetail.UnitPrice,
                        TotalPrice = orderDetail.TotalPrice,
                        PromoPercent = orderDetail.PromoPercent,
                        UnitBidPrice = orderDetail.UnitBidPrice
                    };
                    CurrentOrderDetails.Add(oDOV);
                }
            }
            CurrentOrderDetails.CollectionChanged -= OrderDetailsCollectionChanged;
            CurrentOrderDetails.CollectionChanged += OrderDetailsCollectionChanged;
            CurrentOrderDetails.ItemPropertiesChanged -= ItemPropertiesChanged;
            CurrentOrderDetails.ItemPropertiesChanged += ItemPropertiesChanged;
        }

        private void ItemPropertiesChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsOrderDetailsModified = true;
            Invalidate("MedicineOV");
        }

        private void OrderDetailsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsOrderDetailsModified = true;
            Invalidate("MedicineOV");
        }

        private void InstantinateMedicineItems()
        {
            MedicineItemSource = new ObservableCollection<tblMedicine>();
            _sqlCmdObserver = new SQLQueryCustodian(SQLGetMedicineQueryCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_DATA_CMD_KEY
                    , _sqlCmdObserver);
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
}
