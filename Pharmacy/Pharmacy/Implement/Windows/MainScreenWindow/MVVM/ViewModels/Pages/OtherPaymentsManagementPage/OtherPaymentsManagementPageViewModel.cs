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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs;
using HPSolutionCCDevPackage.netFramework;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage
{
    public class OtherPaymentsManagementPageViewModel : AbstractViewModel
    {
        public ObservableCollection<tblOtherPayment> OtherPaymentItemSource { get; set; }
        public RunInputCommand AddNewOtherPaymentButtonCommand { get; set; }
        public RunInputCommand EditOtherPaymentButtonCommand { get; set; }
        public RunInputCommand DeleteOtherPaymentButtonCommand { get; set; }
        public RunInputCommand ShowInvoiceButtonCommand { get; set; }
        public EventHandleCommand FilterChangedCommand { get; set; }
        public EventHandleCommand ShowOtherPaymentInfoCommand { get; set; }

        private IActionListener _keyActionListener = KeyActionListener.Instance;

        protected override void InitPropertiesRegistry()
        {
        }

        public OtherPaymentsManagementPageViewModel()
        {
            AddNewOtherPaymentButtonCommand = new RunInputCommand(AddNewAddNewOtherPaymentButtonClickEvent);
            DeleteOtherPaymentButtonCommand = new RunInputCommand(DeleteOtherPaymentButtonClickEvent);
            EditOtherPaymentButtonCommand = new RunInputCommand(EditEditOtherPaymentButtonClickEvent);
            ShowInvoiceButtonCommand = new RunInputCommand(ShowInvoiceButtonClickEvent);
            FilterChangedCommand = new EventHandleCommand(FilterChangedEvent);
            ShowOtherPaymentInfoCommand = new EventHandleCommand(ShowOtherPaymentInfoEvent);
            InstantiateItems();
        }

        private void InstantiateItems()
        {
            GetOtherPaymentList();
        }

        private void GetOtherPaymentList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                OtherPaymentItemSource = new ObservableCollection<tblOtherPayment>(queryResult.Result as List<tblOtherPayment>);
                Invalidate("OtherPaymentItemSource");
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_OTHER_PAYMENT_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void FilterChangedEvent(object sender, EventArgs e, object paramaters)
        {
            HorusBox cbxFilterText = (paramaters as object[])[0] as HorusBox;
            DatePicker dprStartDateFilter = (paramaters as object[])[1] as DatePicker;
            DatePicker dprEndDateFilter = (paramaters as object[])[2] as DatePicker;
            DataGrid dataGrid = (paramaters as object[])[3] as DataGrid;
            DoFilter(cbxFilterText.SelectedIndex, dprStartDateFilter.SelectedDate, dprEndDateFilter.SelectedDate, dataGrid);
        }

        private void DoFilter(int selectedTypeIndex, DateTime? startDate, DateTime? endDate, DataGrid dataGrid)
        {
            dataGrid.Items.Filter = new Predicate<object>(item => FilterWarehouseImportInfoList(item as tblOtherPayment, selectedTypeIndex, startDate, endDate));
        }

        private bool FilterWarehouseImportInfoList(tblOtherPayment item, int selectedTypeIndex, DateTime? startDate, DateTime? endDate)
        {
            return SearchByType(item, selectedTypeIndex)
                && FilterByStartDate(item, startDate)
                && FilterByEndDate(item, endDate);
        }

        private bool FilterByEndDate(tblOtherPayment item, DateTime? endDate)
        {
            if (endDate == null) return true;
            else
            {
                return RUNE.IS_SUPPORT_FILTER_OTHER_PAYMENT_BY_END_DATE
                ? item.PaymentTime <= endDate
                : false;
            }
        }

        private bool FilterByStartDate(tblOtherPayment item, DateTime? startDate)
        {
            if (startDate == null) return true;
            else
            {
                return RUNE.IS_SUPPORT_FILTER_OTHER_PAYMENT_BY_START_DATE
                ? item.PaymentTime >= startDate
                : false;
            }
        }

        private bool SearchByType(tblOtherPayment item, int selectedTypeIndex)
        {
            if (RUNE.IS_SUPPORT_SEARCH_OTHER_PAYMENT_BY_TYPE)
            {
                if (selectedTypeIndex == 0)
                    return true;
                else if (item.PaymentType == 0 && selectedTypeIndex == 1)
                    return true;
                else if (item.PaymentType == 1 && selectedTypeIndex == 2)
                    return true;
            }
            return false;
        }

        private void ShowOtherPaymentInfoEvent(object sender, EventArgs e, object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = (paramaters as object[])[0];
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OPMP_EDIT_BUTTON
                , dataTransfer);
        }

        private void AddNewAddNewOtherPaymentButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OPMP_ADD_BUTTON
                , dataTransfer);
        }

        private void DeleteOtherPaymentButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OPMP_DELETE_BUTTON
                , dataTransfer);
        }

        private void EditEditOtherPaymentButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OPMP_EDIT_BUTTON
                , dataTransfer);
        }

        private void ShowInvoiceButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OPMP_SHOW_INVOICE_BUTTON
                , dataTransfer);
        }
    }
}
