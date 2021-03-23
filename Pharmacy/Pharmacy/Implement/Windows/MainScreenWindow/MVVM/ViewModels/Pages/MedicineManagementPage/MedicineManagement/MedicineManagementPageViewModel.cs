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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Utils;
using Pharmacy.Base.UIEventHandler.Action;
using System.Windows;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement
{
    public class MedicineManagementPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("MedicineManagementPageViewModel");

        public ObservableCollection<tblMedicine> MedicineItemSource { get; set; }
        public MSW_MMP_ButtonCommandOV ButtonCommandOV { get; set; }
        public RunInputCommand FilterMedicineTypeCommand { get; set; }
        public EventHandleCommand ShowMedicineInfoCommand { get; set; }
        public EventHandleCommand SearchTextChangedCommand { get; set; }
        public string FilterText { get; set; } = "";

        public Visibility AdminToolboxsVisibility
        {
            get
            {
                return App.Current.CurrentUser.IsAdmin ?
                   Visibility.Visible : Visibility.Collapsed;
            }
        }

        private TimeSpan DELAY_TIME_TO_UPDATE_FILTER = TimeSpan.FromMilliseconds(500);
        private DispatcherTimer _timerUpdateFilter;
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private List<int> _lstMedicineTypeFilter = new List<int>();
        private List<tblMedicineType> _lstMedicineType = new List<tblMedicineType>();

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            MedicineItemSource = new ObservableCollection<tblMedicine>();
            ButtonCommandOV = new MSW_MMP_ButtonCommandOV(this);
            FilterMedicineTypeCommand = new RunInputCommand(FilterMedicineTypeClickEvent);
            SearchTextChangedCommand = new EventHandleCommand(SearchTextChangedEvent);
            ShowMedicineInfoCommand = new EventHandleCommand(ShowMedicineInfoEvent);
            InstantiateItems();
        }

        protected override void OnInitialized()
        {
        }

        private void InstantiateItems()
        {
            AddAllMedicineTypeToFilterList();
            GetMedicineList();
        }

        private void AddAllMedicineTypeToFilterList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    foreach (var type in queryResult.Result as List<tblMedicineType>)
                    {
                        _lstMedicineTypeFilter.Add(type.MedicineTypeID);
                        _lstMedicineType.Add(type);
                    }
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_MEDICINE_TYPE_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void GetMedicineList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    MedicineItemSource = new ObservableCollection<tblMedicine>(queryResult.Result as List<tblMedicine>);
                    Invalidate("MedicineItemSource");
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void SearchTextChangedEvent(object sender, EventArgs e, object paramaters)
        {
            DataGrid dg = (DataGrid)((object[])paramaters)[0];

            DoFilter(dg);
        }
        private void FilterMedicineTypeClickEvent(object obj)
        {
            object[] param = obj as object[];
            CheckBox cbx = param[0] as CheckBox;
            DataGrid dataGrid = param[1] as DataGrid;

            if (cbx.IsChecked == false)
            {
                _lstMedicineTypeFilter.Remove(_lstMedicineType.Where(o => o.MedicineTypeName == (string)cbx.Content).FirstOrDefault().MedicineTypeID);
            }
            else
            {
                _lstMedicineTypeFilter.Add(_lstMedicineType.Where(o => o.MedicineTypeName == (string)cbx.Content).FirstOrDefault().MedicineTypeID);
            }

            DoFilter(dataGrid);
        }

        private void DoFilter(DataGrid dataGrid)
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
                dataGrid.Items.Filter = new Predicate<object>(medicine => FilterMedicineList(medicine as tblMedicine, FilterText));
            };

            _timerUpdateFilter.Start();
        }

        private bool FilterMedicineList(tblMedicine medicine, string filterText)
        {
            return (SearchByID(medicine, filterText) || SearchByName(medicine, filterText) || SearchBySupplierName(medicine, filterText)) && FilterByType(medicine, filterText);
        }

        private bool SearchByID(tblMedicine medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_MEDICINE_BY_ID ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(medicine.MedicineID, filterText, CompareOptions.IgnoreCase) >= 0) : false;
        }

        private bool SearchByName(tblMedicine medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_MEDICINE_BY_NAME ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(medicine.MedicineName, filterText, CompareOptions.IgnoreCase) >= 0) : false;
        }

        private bool SearchBySupplierName(tblMedicine medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_MEDICINE_BY_SUPPLIER_NAME ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(medicine.tblSupplier.SupplierName, filterText, CompareOptions.IgnoreCase) >= 0) : false;
        }

        private bool FilterByType(tblMedicine medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_FILTER_MEDICINE_BY_TYPE ? (_lstMedicineTypeFilter.Contains(medicine.MedicineTypeID)) : false;
        }

        private void ShowMedicineInfoEvent(object sender, EventArgs e, object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_SHOW_INFO_BUTTON
                , dataTransfer);
        }

      
    }
}
