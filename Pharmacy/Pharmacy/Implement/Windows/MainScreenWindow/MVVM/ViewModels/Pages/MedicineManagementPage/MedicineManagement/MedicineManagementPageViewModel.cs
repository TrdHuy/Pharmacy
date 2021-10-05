using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Utils.DatabaseManager;
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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement.OVs;
using System.Windows;
using System.Text;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement
{
    internal class MedicineManagementPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("MedicineManagementPageViewModel");

        public ObservableCollection<MSW_MMP_MedicineOV> MedicineItemSource { get; set; }
        public MSW_MMP_ButtonCommandOV ButtonCommandOV { get; set; }
        public CommandModel FilterMedicineTypeCommand { get; set; }
        public EventCommandModel SearchTextChangedCommand { get; set; }
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
        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private List<int> _lstMedicineTypeFilter = new List<int>();
        private List<tblMedicineType> _lstMedicineType = new List<tblMedicineType>();
        public ObservableCollection<tblMedicine> LstMedicine;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            MedicineItemSource = new ObservableCollection<MSW_MMP_MedicineOV>();
            ButtonCommandOV = new MSW_MMP_ButtonCommandOV(this);

            FilterMedicineTypeCommand = new CommandModel(FilterMedicineTypeClickEvent);
            SearchTextChangedCommand = new EventCommandModel(SearchTextChangedEvent);

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
                    LstMedicine = new ObservableCollection<tblMedicine>((queryResult.Result as List<tblMedicine>).OrderBy(o => o.tblMedicineType.MedicineTypeName).ThenBy(o => o.MedicineName));
                    foreach (var item in LstMedicine)
                    {
                        MedicineItemSource.Add(new MSW_MMP_MedicineOV(item.MedicineID, item.MedicineName, item.tblMedicineType.MedicineTypeName,
                            item.MedicineTypeID, item.BidPrice, item.AskingPrice, item.tblMedicineSuppliers.ToList()));
                    }
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
                dataGrid.Items.Filter = new Predicate<object>(medicine => FilterMedicineList(medicine as MSW_MMP_MedicineOV, FilterText));
            };

            _timerUpdateFilter.Start();
        }

        private bool FilterMedicineList(MSW_MMP_MedicineOV medicine, string filterText)
        {
            return (SearchByID(medicine, filterText) || SearchByName(medicine, filterText) || SearchBySupplierName(medicine, filterText)) && FilterByType(medicine, filterText);
        }

        private bool SearchByID(MSW_MMP_MedicineOV medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_MEDICINE_BY_ID ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(medicine.MedicineID, filterText, CompareOptions.IgnoreCase) >= 0) : false;
        }

        private bool SearchByName(MSW_MMP_MedicineOV medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_MEDICINE_BY_NAME ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(medicine.MedicineName, filterText, CompareOptions.IgnoreCase) >= 0) : false;
        }

        private bool SearchBySupplierName(MSW_MMP_MedicineOV medicine, string filterText)
        {
            if (RUNE.IS_SUPPORT_SEARCH_MEDICINE_BY_SUPPLIER_NAME)
            {
                return CultureInfo.CurrentCulture.CompareInfo.IndexOf(medicine.Suppliers, filterText, CompareOptions.IgnoreCase) >= 0;
            }
            return false;
        }

        private bool FilterByType(MSW_MMP_MedicineOV medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_FILTER_MEDICINE_BY_TYPE ? _lstMedicineTypeFilter.Contains(medicine.MedicineTypeID) : false;
        }
    }
}
