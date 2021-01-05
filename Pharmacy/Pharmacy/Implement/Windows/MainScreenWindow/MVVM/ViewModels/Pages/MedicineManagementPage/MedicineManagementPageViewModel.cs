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
        public RunInputCommand FilterMedicineCommand { get; set; }
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
                UpdateFilter();
            }
        }

        private TimeSpan DELAY_TIME_TO_UPDATE_FILTER = TimeSpan.FromMilliseconds(500);
        private string _filterText;
        private IActionListener _keyActionListener = KeyActionListener.Instance;
        private List<int> _lstMedicineTypeFilter = new List<int>();
        private List<tblMedicineType> _lstMedicineType = new List<tblMedicineType>();
        private DispatcherTimer _timerUpdateFilter;

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
            FilterMedicineCommand = new RunInputCommand(FilterMedicineClickEvent);
            FilterText = "";
            InstantiateItems();
        }

        private void InstantiateItems()
        {
            AddAllMedicineTypeToFilterList();
            GetMedicineList("");
        }

        private void GetMedicineList(string keyword)
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian(GetActiveMedicineCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_DATA_CMD_KEY
                    , _sqlCmdObserver
                    , keyword
                    , _lstMedicineTypeFilter);
        }

        private void AddAllMedicineTypeToFilterList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian(GetActiveMedicineTypeCallback);
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_TYPE_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void GetActiveMedicineTypeCallback(SQLQueryResult queryResult)
        {
            foreach (var type in queryResult.Result as List<tblMedicineType>)
            {
                _lstMedicineTypeFilter.Add(type.MedicineTypeID);
                _lstMedicineType.Add(type);
            }
        }

        private void GetActiveMedicineCallback(SQLQueryResult queryResult)
        {
            MedicineItemSource = new ObservableCollection<tblMedicine>(queryResult.Result as List<tblMedicine>);
            Invalidate("MedicineItemSource");
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
        private void FilterMedicineTypeClickEvent(object obj)
        {
            CheckBox cbx = obj as CheckBox;
            if (cbx.IsChecked == false)
            {
                _lstMedicineTypeFilter.Remove(_lstMedicineType.Where(o => o.MedicineTypeName == (string)cbx.Content).FirstOrDefault().MedicineTypeID);
            }
            else
            {
                _lstMedicineTypeFilter.Add(_lstMedicineType.Where(o => o.MedicineTypeName == (string)cbx.Content).FirstOrDefault().MedicineTypeID);
            }

            GetMedicineList(FilterText);
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

        private void FilterMedicineClickEvent(object paramaters)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            if (_timerUpdateFilter != null && _timerUpdateFilter.IsEnabled)
            {
                _timerUpdateFilter.Stop();
            }
            _timerUpdateFilter = new DispatcherTimer();
            _timerUpdateFilter.Interval = DELAY_TIME_TO_UPDATE_FILTER;
            _timerUpdateFilter.Tick += _timerUpdateFilter_Tick;

            _timerUpdateFilter.Start();
        }

        private void _timerUpdateFilter_Tick(object sender, EventArgs e)
        {
            _timerUpdateFilter.Stop();
            GetMedicineList(FilterText);
        }
    }

}
