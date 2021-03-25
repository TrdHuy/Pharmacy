using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage.OVs;
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
    public class SellingPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("SellingPageViewModel");

        private SQLQueryCustodian _sqlCmdObserver;
        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private bool _isAddOrderDeatailButtonRunning = false;
        private bool _isInstantiateNewOrderButtonRunning = false;
        private string _orderDescription;

        public ObservableCollection<tblCustomer> CustomerItemSource { get; set; }
        public ObservableCollection<tblMedicine> MedicineItemSource { get; set; }
        public ObservableCollection<OrderDetailOV> CustomerOrderDetailItemSource { get; set; }

        public RunInputCommand AddOrderDetailCommand { get; set; }
        public RunInputCommand RemoveOrderDetailCommand { get; set; }
        public RunInputCommand InstantiateOrderCommand { get; set; }
        public RunInputCommand RefreshSellingPageCommand { get; set; }
        
        public MSW_SP_CustomerOV CustomerOV { get; set; }
        public MSW_SP_MedicineOV MedicineOV { get; set; }

        public bool IsInstantiateNewOrderButtonRunning
        {
            get
            {

                return _isInstantiateNewOrderButtonRunning;
            }
            set
            {
                _isInstantiateNewOrderButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
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
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public string OrderDescription
        {
            get
            {
                return _orderDescription;
            }
            set
            {
                _orderDescription = value;
                InvalidateOwn();
            }
        }
        public bool IsAddOrderDetailCanPerform
        {
            get
            {
                return MedicineOV.CurrentSelectedMedicine != null
                    && !String.IsNullOrEmpty(CustomerOV.CustomerPhone)
                    && !String.IsNullOrEmpty(CustomerOV.CustomerName)
                    && !String.IsNullOrEmpty(MedicineOV.Quantity) 
                    && !MedicineOV.Quantity.Equals("0");
            }
        }

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CustomerOV = new MSW_SP_CustomerOV(this);
            MedicineOV = new MSW_SP_MedicineOV(this);

            InstantiateItems();
            AddOrderDetailCommand = new RunInputCommand(AddOrderDetailButtonClickEvent);
            RemoveOrderDetailCommand = new RunInputCommand(RemoveOrderDetailButtonClickEvent);
            InstantiateOrderCommand = new RunInputCommand(InstantiateOrderButtonClickEvent);
            RefreshSellingPageCommand = new RunInputCommand(RefreshSellingPagClickEvent);
        }

        protected override void OnInitialized()
        {
        }

        public void RefreshViewModel(bool re_Customer = true, bool re_Medicine = true)
        {
            CustomerOrderDetailItemSource.Clear();

            OrderDescription = "";
            if (re_Medicine)
            {
                MedicineOV.RefreshViewModel();
            }

            if (re_Customer)
            {
                CustomerOV.RefreshViewModel();
            }

        }

        private void RefreshSellingPagClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SP_REFRESH_BUTTON
                , dataTransfer);
        }

        private void InstantiateOrderButtonClickEvent(object paramaters)
        {
            IsInstantiateNewOrderButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SP_INSTANTIATE_BUTTON
                , dataTransfer
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
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
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
        }

        private void InstantiateItems()
        {
            InstantiateCustomerItems();
            InstantiateMedicineItems();
            InstantiateCustomerOrderDetailItems();
        }

        private void InstantiateCustomerOrderDetailItems()
        {
            CustomerOrderDetailItemSource = new ObservableCollection<OrderDetailOV>();
            CustomerOrderDetailItemSource.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CustomerOrderList_CollectionChanged);

        }

        private void CustomerOrderList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Invalidate(CustomerOV,"IsCustomerChooserEnable");
            Invalidate(MedicineOV,"MedicineCost");
            Invalidate(MedicineOV,"TotalCost");
            Invalidate(MedicineOV,"RestAmount");
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

}
