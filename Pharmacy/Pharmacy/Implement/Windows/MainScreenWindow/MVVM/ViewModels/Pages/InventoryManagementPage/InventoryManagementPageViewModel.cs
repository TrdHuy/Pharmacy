﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pharmacy.Implement.Utils.DatabaseManager;
using System.Linq;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage.OVs;
using System.Dynamic;
using System;
using Pharmacy.Implement.Utils.Definitions;
using System.ComponentModel;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage
{
    internal class InventoryManagementPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("InventoryManagementPageViewModel");
        private ObservableCollection<object> _inventoryDataSource;
        private ObservableCollection<tblMedicine> _medicineItemSource;
        private ObservableCollection<tblMedicineType> _lstMedicineType;
        private bool _isDataGridLoading;

        protected override Logger logger => L;

        [Bindable(true)]
        public MSW_IMP_ButtonCommandOV ButtonCommandOV { get; set; }
        [Bindable(true)]
        public MSW_IMP_MedicineOV MedicineOV { get; set; }
        [Bindable(true)]
        public MSW_IMP_EventCommandOV EventCommandOV { get; set; }
        [Bindable(true)]
        public ObservableCollection<tblMedicineType> LstMedicineType
        {
            get
            {
                return _lstMedicineType;
            }
            set
            {
                _lstMedicineType = value;
                InvalidateOwn();
            }
        }
        [Bindable(true)]
        public ObservableCollection<tblMedicine> MedicineItemSource
        {
            get
            {
                return _medicineItemSource;
            }
            set
            {
                _medicineItemSource = value;
                InvalidateOwn();
            }
        }
        [Bindable(true)]
        public ObservableCollection<object> InventoryDataSource
        {
            get
            {
                return _inventoryDataSource;
            }
            set
            {
                _inventoryDataSource = value;
                InvalidateOwn();
            }
        }
        [Bindable(true)]
        public bool IsDataGridLoading
        {
            get
            {
                return _isDataGridLoading;
            }
            set
            {
                _isDataGridLoading = value;
                InvalidateOwn();
            }
        }

        
        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_IMP_ButtonCommandOV(this);
            MedicineOV = new MSW_IMP_MedicineOV(this);
            EventCommandOV = new MSW_IMP_EventCommandOV(this);
            InitData();

        }

        protected override void OnInitialized()
        {
        }

        private void InitData()
        {
            GetListOfMedicineType();
            GetMedicineItems();
            GetInventoryData();
        }

        private void GetInventoryData()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    InventoryDataSource = (ObservableCollection<object>)(queryResult.Result);
                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Lỗi load dữ liệu kho hàng, vui lòng mở lại ứng dụng hoặc liên hệ CSKH để biết thêm thông tin!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                        OwnerWindow.MainScreen,
                        "Lỗi!");
                }
            });
            DbManager.Instance.ExecuteQueryAsync(true, SQLCommandKey.GET_INVENTORY_DATA_CMD_KEY,
               PharmacyDefinitions.ADD_NEW_CUSTOMER_DELAY_TIME,
               _sqlCmdObserver);
        }

        private void GetListOfMedicineType()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstMedicineType = new ObservableCollection<tblMedicineType>(queryResult.Result as List<tblMedicineType>);
                    LstMedicineType.Add(new tblMedicineType() { MedicineTypeName = "Tất cả" });
                }
                else
                {
                    LstMedicineType = new ObservableCollection<tblMedicineType>();
                }
            });

            DbManager.Instance.ExecuteQueryAsync(true, SQLCommandKey.GET_ALL_MEDICINE_TYPE_DATA_CMD_KEY,
                PharmacyDefinitions.ADD_NEW_CUSTOMER_DELAY_TIME,
                _sqlCmdObserver);
        }

        private void GetMedicineItems()
        {
            MedicineItemSource = new ObservableCollection<tblMedicine>();
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    MedicineItemSource = new ObservableCollection<tblMedicine>((queryResult.Result as List<tblMedicine>).OrderBy(o => o.MedicineName));
                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Lỗi load dữ liệu thuốc, vui lòng mở lại ứng dụng hoặc liên hệ CSKH để biết thêm thông tin!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                        OwnerWindow.MainScreen,
                        "Lỗi!");
                }
            });

            DbManager.Instance.ExecuteQueryAsync(true, SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_DATA_CMD_KEY,
               PharmacyDefinitions.ADD_NEW_CUSTOMER_DELAY_TIME,
               _sqlCmdObserver);

        }
    }
}
