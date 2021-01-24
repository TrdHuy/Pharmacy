using Pharmacy.Base.MVVM.ViewModels;
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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage
{
    public class WarehouseManagementPageViewModel : AbstractViewModel
    {
        public ObservableCollection<tblWarehouseImport> WarehouseImportItemSource { get; set; }
        public RunInputCommand AddNewWarehouseImportButtonCommand { get; set; }
        public RunInputCommand EditWarehouseImportButtonCommand { get; set; }
        public RunInputCommand DeleteWarehouseImportButtonCommand { get; set; }
        public RunInputCommand ShowInvoiceButtonCommand { get; set; }
        public EventHandleCommand FilterChangedCommand { get; set; }
        public EventHandleCommand ShowWarehouseImportInfoCommand { get; set; }

        private TimeSpan DELAY_TIME_TO_UPDATE_FILTER = TimeSpan.FromMilliseconds(500);
        private DispatcherTimer _timerUpdateFilter;
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        protected override void InitPropertiesRegistry()
        {
        }

        public WarehouseManagementPageViewModel()
        {
            WarehouseImportItemSource = new ObservableCollection<tblWarehouseImport>();
            AddNewWarehouseImportButtonCommand = new RunInputCommand(AddNewWarehouseImportButtonClickEvent);
            DeleteWarehouseImportButtonCommand = new RunInputCommand(DeleteWarehouseImportButtonClickEvent);
            EditWarehouseImportButtonCommand = new RunInputCommand(EditWarehouseImportButtonClickEvent);
            ShowInvoiceButtonCommand = new RunInputCommand(ShowInvoiceButtonClickEvent);
            FilterChangedCommand = new EventHandleCommand(FilterChangedEvent);
            ShowWarehouseImportInfoCommand = new EventHandleCommand(ShowWarehouseImportInfoEvent);
            InstantiateItems();
        }

        private void InstantiateItems()
        {
            GetWarehouseImportList();
        }

        private void GetWarehouseImportList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    WarehouseImportItemSource = new ObservableCollection<tblWarehouseImport>(queryResult.Result as List<tblWarehouseImport>);
                    Invalidate("WarehouseImportItemSource");
                }
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
                dataGrid.Items.Filter = new Predicate<object>(item => FilterWarehouseImportInfoList(item as tblWarehouseImport, filterText, startDate, endDate));
            };

            _timerUpdateFilter.Start();
        }

        private bool FilterWarehouseImportInfoList(tblWarehouseImport item, string filterText, DateTime? startDate, DateTime? endDate)
        {
            return (SearchByMedicineID(item, filterText) || SearchByMedicineName(item, filterText) || SearchBySupplierName(item, filterText))
                && FilterByStartDate(item, startDate)
                && FilterByEndDate(item, endDate);
        }

        private bool FilterByEndDate(tblWarehouseImport item, DateTime? endDate)
        {
            if (endDate == null) return true;
            else
            {
                return RUNE.IS_SUPPORT_FILTER_WAREHOUSE_IMPORT_BY_END_DATE
                ? item.ImportTime <= endDate
                : false;
            }
        }

        private bool FilterByStartDate(tblWarehouseImport item, DateTime? startDate)
        {
            if (startDate == null) return true;
            else
            {
                return RUNE.IS_SUPPORT_FILTER_WAREHOUSE_IMPORT_BY_START_DATE
                ? item.ImportTime >= startDate
                : false;
            }
        }

        private bool SearchBySupplierName(tblWarehouseImport item, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_SUPPLIER_NAME
                ? CultureInfo.CurrentCulture.CompareInfo.IndexOf(item.tblSupplier.SupplierName, filterText, CompareOptions.IgnoreCase) >= 0
                : false;
        }

        private bool SearchByMedicineID(tblWarehouseImport item, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_ID
                ? item.tblWarehouseImportDetails.Where(o => CultureInfo.CurrentCulture.CompareInfo.IndexOf(o.MedicineID, filterText, CompareOptions.IgnoreCase) >= 0).FirstOrDefault() != null
                : false;
        }

        private bool SearchByMedicineName(tblWarehouseImport item, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_WAREHOUSE_IMPORT_BY_MEDICINE_NAME
                ? item.tblWarehouseImportDetails.Where(o => CultureInfo.CurrentCulture.CompareInfo.IndexOf(o.tblMedicine.MedicineName, filterText, CompareOptions.IgnoreCase) >= 0).FirstOrDefault() != null
                : false;
        }

        private void ShowWarehouseImportInfoEvent(object sender, EventArgs e, object paramaters)
        {
            //object[] dataTransfer = new object[2];
            //dataTransfer[0] = this;
            //dataTransfer[1] = paramaters;
            //_keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
            //    , KeyFeatureTag.KEY_TAG_MSW_MMP_SHOW_INFO_BUTTON
            //    , dataTransfer);
        }

        private void AddNewWarehouseImportButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_ADD_BUTTON
                , dataTransfer);
        }

        private void DeleteWarehouseImportButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_DELETE_BUTTON
                , dataTransfer);
        }

        private void EditWarehouseImportButtonClickEvent(object paramaters)
        {
            //object[] dataTransfer = new object[2];
            //dataTransfer[0] = this;
            //dataTransfer[1] = paramaters;
            //_keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
            //    , KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON
            //    , dataTransfer);
        }

        private void ShowInvoiceButtonClickEvent(object paramaters)
        {
            //object[] dataTransfer = new object[2];
            //dataTransfer[0] = this;
            //dataTransfer[1] = paramaters;
            //_keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
            //    , KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON
            //    , dataTransfer);
        }
    }
}
