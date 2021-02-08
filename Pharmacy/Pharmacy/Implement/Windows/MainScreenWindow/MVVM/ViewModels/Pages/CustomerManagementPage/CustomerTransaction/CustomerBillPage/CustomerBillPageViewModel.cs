﻿using HPSolutionCCDevPackage.netFramework;
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
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage
{
    public class CustomerBillPageViewModel : AbstractViewModel
    {
        private static Logger logger = new Logger("CustomerBillPageViewModel");

        private SQLQueryCustodian _sqlCmdObserver;
        private bool _isEnableEdittingBill;
        private Visibility _deleteColumnVisibility;
        private ObservablePropertiesCollection<OrderDetailOV> _currentOrderDetails;

        public tblOrder CurrentCustomerOrder { get; set; }
        public MSW_CMP_CTP_CBP_MedicineOV MedicineOV { get; set; }
        public MSW_CMP_CTP_CBP_ButtonCommandOV ButtonCommandOV { get; set; }
        public ObservableCollection<tblMedicine> MedicineItemSource { get; set; }
        public bool IsOrderDetailsModified { get; private set; }

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


        public CustomerBillPageViewModel()
        {
            logger.I("Instantinating CustomerBillPageViewModel");

            MedicineOV = new MSW_CMP_CTP_CBP_MedicineOV(this);
            ButtonCommandOV = new MSW_CMP_CTP_CBP_ButtonCommandOV(this);

            CurrentCustomerOrder = MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder;

            DeleteColumnVisibility = IsEnableEdittingBill ? Visibility.Visible : Visibility.Collapsed;

            InstantiateItems();

            logger.I("Instantinated CustomerBillPageViewModel");
        }

        public void RefreshListOrder()
        {
            InstantinateOrderDetailItems();
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
                        PromoPercent = GetPromo(orderDetail.tblMedicine)
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

        private double GetPromo(tblMedicine medicine)
        {
            tblPromo appliedPromo = new tblPromo();
            var customer = CurrentCustomerOrder.tblCustomer;
            if (customer != null)
            {
                foreach (tblPromo promo in customer.tblPromoes)
                {
                    if (promo.MedicineID == medicine.MedicineID)
                    {
                        appliedPromo = promo;
                        break;
                    }
                }
            }

            return appliedPromo.PromoPercent;
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