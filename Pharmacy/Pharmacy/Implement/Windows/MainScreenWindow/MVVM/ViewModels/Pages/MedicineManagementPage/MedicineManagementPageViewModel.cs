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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage
{
    public class MedicineManagementPageViewModel : AbstractViewModel
    {
        public ObservableCollection<tblMedicine> MedicineItemSource { get; set; }
        public RunInputCommand ExcelImportButtonCommand { get; set; }
        public RunInputCommand PrintMedicineListButtonCommand { get; set; }
        public RunInputCommand AddNewMedicineButtonCommand { get; set; }
        public RunInputCommand EditMedicineButtonCommand { get; set; }
        public RunInputCommand DeleteMedicineButtonCommand { get; set; }
        public RunInputCommand PromoMedicineButtonCommand { get; set; }
        public RunInputCommand FilterMedicineTypeCommand { get; set; }
        public EventHandleCommand ShowMedicineInfoCommand { get; set; }
        public EventHandleCommand SearchTextChangedCommand { get; set; }
        public string FilterText { get; set; } = "";

        private TimeSpan DELAY_TIME_TO_UPDATE_FILTER = TimeSpan.FromMilliseconds(500);
        private DispatcherTimer _timerUpdateFilter;
        private IActionListener _keyActionListener = KeyActionListener.Instance;
        private List<int> _lstMedicineTypeFilter = new List<int>();
        private List<tblMedicineType> _lstMedicineType = new List<tblMedicineType>();

        protected override void InitPropertiesRegistry()
        {
        }

        public MedicineManagementPageViewModel()
        {
            MedicineItemSource = new ObservableCollection<tblMedicine>();
            ExcelImportButtonCommand = new RunInputCommand(ExcelImportButtonClickEvent);
            PrintMedicineListButtonCommand = new RunInputCommand(PrintMedicineListButtonClickEvent);
            AddNewMedicineButtonCommand = new RunInputCommand(AddNewMedicineButtonClickEvent);
            EditMedicineButtonCommand = new RunInputCommand(EditMedicineButtonClickEvent);
            DeleteMedicineButtonCommand = new RunInputCommand(DeleteMedicineButtonClickEvent);
            PromoMedicineButtonCommand = new RunInputCommand(PromoMedicineButtonClickEvent);
            FilterMedicineTypeCommand = new RunInputCommand(FilterMedicineTypeClickEvent);
            SearchTextChangedCommand = new EventHandleCommand(SearchTextChangedEvent);
            ShowMedicineInfoCommand = new EventHandleCommand(ShowMedicineInfoEvent);
            InstantiateItems();
        }

        private void InstantiateItems()
        {
            AddAllMedicineTypeToFilterList();
            GetMedicineList();
        }

        private void GetMedicineList()
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

        private void AddAllMedicineTypeToFilterList()
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
            _timerUpdateFilter.Tick += (sender,e)=>
            {
                _timerUpdateFilter.Stop();
                dataGrid.Items.Filter = new Predicate<object>(medicine => FilterMedicineList(medicine as tblMedicine, FilterText));
            };

            _timerUpdateFilter.Start();
        }

        private bool FilterMedicineList(tblMedicine medicine, string filterText)
        {
            return (SearchByID(medicine, filterText) || SearchByName(medicine, filterText)) && FilterByType(medicine, filterText);
        }

        private bool SearchByID(tblMedicine medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_MEDICINE_BY_ID ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(medicine.MedicineID, filterText, CompareOptions.IgnoreCase) >= 0) : false;
        }

        private bool SearchByName(tblMedicine medicine, string filterText)
        {
            return RUNE.IS_SUPPORT_SEARCH_MEDICINE_BY_NAME ? (CultureInfo.CurrentCulture.CompareInfo.IndexOf(medicine.MedicineName, filterText, CompareOptions.IgnoreCase) >= 0) : false;
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

        private void ExcelImportButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_EXCEL_IMPORT_BUTTON
                , dataTransfer);
        }

        private void PrintMedicineListButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_PRINT_MEDICINE_BUTTON
                , dataTransfer);
        }

        private void AddNewMedicineButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON
                , dataTransfer);
        }

        private void PromoMedicineButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON
                , dataTransfer);
        }

        private void DeleteMedicineButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON
                , dataTransfer);
        }

        private void EditMedicineButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON
                , dataTransfer);
        }
    }
}
