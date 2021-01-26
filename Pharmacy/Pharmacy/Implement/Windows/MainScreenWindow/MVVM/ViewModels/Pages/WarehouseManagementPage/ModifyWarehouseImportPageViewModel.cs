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
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage
{
    public class ModifyWarehouseImportPageViewModel : AbstractViewModel
    {
        public ObservableCollection<tblMedicine> LstMedicine { get; set; }
        public ObservableCollection<WarehouseImportDetailVO> LstWarehouseImportDetail { get; set; }
        public tblWarehouseImport ImportInfo { get; set; }
        public tblMedicine SelectedMedicine
        {
            get { return _selectedMedicine; }
            set
            {
                if (_selectedMedicine != value)
                {
                    MedicineQuantity = 0;
                    MedicinePrice = 0;
                    Invalidate("MedicineQuantity");
                    Invalidate("MedicinePrice");
                }
                _selectedMedicine = value;
                InvalidateOwn();
            }
        }
        public string InvoiceImageURL { get; set; } = "";
        public string NoteString { get; set; } = "";
        public int MedicineQuantity { get; set; }
        public decimal MedicinePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PurchasedPrice
        {
            get { return _purchasedPrice; }
            set
            {
                _purchasedPrice = value;
                InvalidateOwn();
                UpdateNetPrice();
            }
        }
        public decimal NetPrice { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand BrowseInvoiceImageButtonCommand { get; set; }
        public RunInputCommand AddMedicineToListButtonCommand { get; set; }
        public RunInputCommand DeleteMedicineFromListButtonCommand { get; set; }
        public bool IsAddImportDetailButtonRunning
        {
            get { return _isAddImportDetailButtonRunning; }
            set
            {
                _isAddImportDetailButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }
        public bool IsAddWarehouseImportButtonRunning
        {
            get { return _isAddWarehouseImportButtonRunning; }
            set
            {
                _isAddWarehouseImportButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private List<tblMedicine> _lstMedicineFull;
        private tblMedicine _selectedMedicine;
        private decimal _purchasedPrice;
        private bool _isAddImportDetailButtonRunning;
        private bool _isAddWarehouseImportButtonRunning;

        protected override void InitPropertiesRegistry()
        {
        }

        public ModifyWarehouseImportPageViewModel()
        {
            BrowseInvoiceImageButtonCommand = new RunInputCommand(BrowseInvoiceImageButtonClickEvent);
            AddMedicineToListButtonCommand = new RunInputCommand(AddMedicineToListButtonClickEvent);
            CancelButtonCommand = new RunInputCommand(CancelButtonClickEvent);
            DeleteMedicineFromListButtonCommand = new RunInputCommand(DeleteMedicineFromListButtonClickEvent);
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            InstantiateItems();
            InitImportDetail();
        }

        public void UpdateTotalPriceAndNewPrice()
        {
            TotalPrice = LstWarehouseImportDetail.Sum(o => o.TotalPrice);
            NetPrice = TotalPrice - PurchasedPrice;
            Invalidate("TotalPrice");
            Invalidate("NetPrice");
        }

        private void SaveButtonClickEvent(object paramaters)
        {
            IsAddWarehouseImportButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_SAVE_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }

        private void DeleteMedicineFromListButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON
                , dataTransfer);
        }

        private void CancelButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_CANCEL_BUTTON
                , dataTransfer);
        }

        private void AddMedicineToListButtonClickEvent(object paramaters)
        {
            IsAddImportDetailButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }

        private void BrowseInvoiceImageButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_BROWSE_INVOICE_IMAGE_BUTTON
                , dataTransfer);
        }

        private void InstantiateItems()
        {
            GetMedicineList();
        }

        private void GetMedicineList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    _lstMedicineFull = queryResult.Result as List<tblMedicine>;
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void UpdateMedicineListBySupplier()
        {
            if (ImportInfo != null)
            {
                LstMedicine = new ObservableCollection<tblMedicine>(_lstMedicineFull.Where(o => o.SupplierID == ImportInfo.SupplierID));
                Invalidate("LstMedicine");
                SelectedMedicine = null;
            }
        }

        private void UpdateNetPrice()
        {
            NetPrice = TotalPrice - PurchasedPrice;
            Invalidate("NetPrice");
        }

        private void InitImportDetail()
        {
            ImportInfo = MSW_DataFlowHost.Current.CurrentModifiedWarehouseImport;
            UpdateMedicineListBySupplier();
            NoteString = ImportInfo.ImportDescription;
            MedicinePrice = 0;
            MedicineQuantity = 0;
            SelectedMedicine = null;
            TotalPrice = ImportInfo.TotalPrice;
            PurchasedPrice = ImportInfo.PurchasePrice;
            NetPrice = TotalPrice - PurchasedPrice;
            LstWarehouseImportDetail = new ObservableCollection<WarehouseImportDetailVO>();
            foreach (var item in ImportInfo.tblWarehouseImportDetails.Where(o=>o.IsActive))
            {
                var detail = new WarehouseImportDetailVO();
                detail.MedicineID = item.MedicineID;
                detail.MedicineName = item.tblMedicine.MedicineName;
                detail.MedicineUnitName = item.tblMedicine.tblMedicineUnit.MedicineUnitName;
                detail.Quantity = item.Quantity;
                detail.UnitPrice = item.Price;
                detail.TotalPrice = (decimal)detail.Quantity * detail.UnitPrice;
                LstWarehouseImportDetail.Add(detail);
            }
            Invalidate("LstWarehouseImportDetail");
            Invalidate("LstMedicine");
            Invalidate("MedicinePrice");
            Invalidate("MedicineQuantity");
            Invalidate("TotalPrice");
            Invalidate("NetPrice");
        }
    }
}
