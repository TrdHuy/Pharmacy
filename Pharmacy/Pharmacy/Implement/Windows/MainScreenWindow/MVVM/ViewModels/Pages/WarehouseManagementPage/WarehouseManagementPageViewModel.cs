﻿using Pharmacy.Base.MVVM.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler.Listener;
using System.Windows.Controls;
using System.Linq;
using System;
using Pharmacy.Implement.UIEventHandler;
using System.Windows.Threading;
using Pharmacy.Config;
using System.Globalization;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage
{
    internal class WarehouseManagementPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("WarehouseManagementPageViewModel");

        public ObservableCollection<MSW_WHMP_WarehouseImportOV> WarehouseImportItemSource { get; set; }
        public MSW_WHMP_ButtonCommandOV ButtonCommandOV { get; set; }
        public EventCommandModel FilterChangedCommand { get; set; }
        public EventCommandModel ShowWarehouseImportInfoCommand { get; set; }
        public List<tblWarehouseImport> LstWarehouseImport { get; set; }

        private TimeSpan DELAY_TIME_TO_UPDATE_FILTER = TimeSpan.FromMilliseconds(500);
        private DispatcherTimer _timerUpdateFilter;
        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_WHMP_ButtonCommandOV(this);
            FilterChangedCommand = new EventCommandModel(FilterChangedEvent);
            ShowWarehouseImportInfoCommand = new EventCommandModel(ShowWarehouseImportInfoEvent);
            InstantiateItems();
        }

        protected override void OnInitialized()
        {
        }

        private void InstantiateItems()
        {
            GetWarehouseImportList();
        }

        private void GetWarehouseImportList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                WarehouseImportItemSource = new ObservableCollection<MSW_WHMP_WarehouseImportOV>();
                LstWarehouseImport = queryResult.Result as List<tblWarehouseImport>;

                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    foreach (var item in queryResult.Result as List<tblWarehouseImport>)
                    {
                        var detail = new MSW_WHMP_WarehouseImportOV();
                        detail.ImportID = item.ImportID;
                        detail.ImportTime = item.ImportTime;
                        detail.ImportDescription = item.ImportDescription;
                        detail.IsActive = item.IsActive;
                        detail.PurchasePrice = item.PurchasePrice;
                        detail.TotalPrice = item.TotalPrice;
                        detail.SupplierID = item.SupplierID;
                        detail.SupplierName = item.tblSupplier.SupplierName;
                        detail.tblWarehouseImportDetails = item.tblWarehouseImportDetails.Where(o => o.IsActive).ToList();

                        WarehouseImportItemSource.Add(detail);
                    }
                }
                Invalidate("WarehouseImportItemSource");
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_WAREHOUSE_IMPORT_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void FilterChangedEvent(object sender, EventArgs e, object paramaters)
        {
            TextBox txtFilterText = (paramaters as object[])[0] as TextBox;
            DatePicker dprStartDateFilter = (paramaters as object[])[1] as DatePicker;
            DatePicker dprEndDateFilter = (paramaters as object[])[2] as DatePicker;
            DataGrid dataGrid = (paramaters as object[])[3] as DataGrid;
            DoFilter(txtFilterText.Text, dprStartDateFilter.SelectedDate, dprEndDateFilter.SelectedDate, dataGrid);
        }

        private void DoFilter(string filterText, DateTime? startDate, DateTime? endDate, DataGrid dataGrid)
        {
            if (_timerUpdateFilter != null && _timerUpdateFilter.IsEnabled)
            {
                _timerUpdateFilter.Stop();
            }

            _timerUpdateFilter = new DispatcherTimer();
            _timerUpdateFilter.Interval = DELAY_TIME_TO_UPDATE_FILTER;
            _timerUpdateFilter.Tick += (sender, e) =>
            {
                _timerUpdateFilter.Stop();
                dataGrid.Items.Filter = new Predicate<object>(item => FilterWarehouseImportInfoList(item as MSW_WHMP_WarehouseImportOV, filterText, startDate, endDate));
            };

            _timerUpdateFilter.Start();
        }

        private bool FilterWarehouseImportInfoList(MSW_WHMP_WarehouseImportOV item, string filterText, DateTime? startDate, DateTime? endDate)
        {
            return (SearchByMedicineID(item, filterText) || SearchByMedicineName(item, filterText) || SearchBySupplierName(item, filterText))
                && FilterByStartDate(item, startDate)
                && FilterByEndDate(item, endDate);
        }

        private bool FilterByEndDate(MSW_WHMP_WarehouseImportOV item, DateTime? endDate)
        {
            if (endDate == null) return true;
            else
            {
                return RUNE.IS_SUPPORT_FILTER_WAREHOUSE_IMPORT_BY_END_DATE
                ? item.ImportTime <= endDate
                : false;
            }
        }

        private bool FilterByStartDate(MSW_WHMP_WarehouseImportOV item, DateTime? startDate)
        {
            if (startDate == null) return true;
            else
            {
                return RUNE.IS_SUPPORT_FILTER_WAREHOUSE_IMPORT_BY_START_DATE
                ? item.ImportTime >= startDate
                : false;
            }
        }

        private bool SearchBySupplierName(MSW_WHMP_WarehouseImportOV item, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_SUPPLIER_NAME
                ? CultureInfo.CurrentCulture.CompareInfo.IndexOf(item.SupplierName, filterText, CompareOptions.IgnoreCase) >= 0
                : false;
        }

        private bool SearchByMedicineID(MSW_WHMP_WarehouseImportOV item, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_ID
                ? item.tblWarehouseImportDetails.Where(o => CultureInfo.CurrentCulture.CompareInfo.IndexOf(o.MedicineID, filterText, CompareOptions.IgnoreCase) >= 0).FirstOrDefault() != null
                : false;
        }

        private bool SearchByMedicineName(MSW_WHMP_WarehouseImportOV item, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_NAME
                ? item.tblWarehouseImportDetails.Where(o => CultureInfo.CurrentCulture.CompareInfo.IndexOf(o.tblMedicine.MedicineName, filterText, CompareOptions.IgnoreCase) >= 0).FirstOrDefault() != null
                : false;
        }

        private void ShowWarehouseImportInfoEvent(object sender, EventArgs e, object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(this
                , logger
                , WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_SHOW_INFO_BUTTON
                , dataTransfer);
        }

    }
}
