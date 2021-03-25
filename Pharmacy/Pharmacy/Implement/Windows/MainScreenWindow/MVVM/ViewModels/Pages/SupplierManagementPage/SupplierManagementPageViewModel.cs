using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.UIEventHandler.Listener;
using System.Windows.Controls;
using System;
using Pharmacy.Implement.UIEventHandler;
using System.Windows.Threading;
using Pharmacy.Config;
using System.Globalization;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage
{
    internal class SupplierManagementPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("SupplierManagementPageViewModel");

        public ObservableCollection<tblSupplier> SupplierItemSource { get; set; }
        public MSW_SMP_ButtonCommandOV ButtonCommandOV { get; set; }
        public EventCommandModel FilterChangedCommand { get; set; }
        public EventCommandModel ShowSupplierInfoCommand { get; set; }

        private TimeSpan DELAY_TIME_TO_UPDATE_FILTER = TimeSpan.FromMilliseconds(500);
        private DispatcherTimer _timerUpdateFilter;
        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_SMP_ButtonCommandOV(this);
            FilterChangedCommand = new EventCommandModel(FilterChangedEvent);
            ShowSupplierInfoCommand = new EventCommandModel(ShowSupplierInfoEvent);
            InstantiateItems();
        }

        protected override void OnInitialized()
        {
        }

        private void InstantiateItems()
        {
            GetSupplierList();
        }

        private void GetSupplierList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    SupplierItemSource = new ObservableCollection<tblSupplier>(queryResult.Result as List<tblSupplier>);
                }
                else
                {
                    SupplierItemSource = new ObservableCollection<tblSupplier>();
                }
                Invalidate("SupplierItemSource");
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_SUPPLIER_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void FilterChangedEvent(object sender, EventArgs e, object paramaters)
        {
            TextBox txtFilterText = (paramaters as object[])[0] as TextBox;
            DataGrid dataGrid = (paramaters as object[])[1] as DataGrid;
            DoFilter(txtFilterText.Text, dataGrid);
        }

        private void DoFilter(string filterText, DataGrid dataGrid)
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
                dataGrid.Items.Filter = new Predicate<object>(item => FilterSupplierList(item as tblSupplier, filterText));
            };

            _timerUpdateFilter.Start();
        }

        private bool FilterSupplierList(tblSupplier item, string filterText)
        {
            return SearchBySupplierName(item, filterText) || SearchBySupplierPhone(item, filterText);
        }

        private bool SearchBySupplierName(tblSupplier item, string filterText)
        {
            return RUNE.IS_SUPPORT_FILTER_SUPPLIER_BY_NAME
                ? CultureInfo.CurrentCulture.CompareInfo.IndexOf(item.SupplierName, filterText, CompareOptions.IgnoreCase) >= 0
                : false;
        }

        private bool SearchBySupplierPhone(tblSupplier item, string filterText)
        {
            return RUNE.IS_SUPPORT_FILTER_SUPPLIER_BY_PHONE
                ? CultureInfo.CurrentCulture.CompareInfo.IndexOf(item.Phone, filterText, CompareOptions.IgnoreCase) >= 0
                : false;
        }

        private void ShowSupplierInfoEvent(object sender, EventArgs e, object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = (paramaters as object[])[0];
            _keyActionListener.OnKey(this
                , logger
                , WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SMP_EDIT_BUTTON
                , dataTransfer);
        }


    }
}
