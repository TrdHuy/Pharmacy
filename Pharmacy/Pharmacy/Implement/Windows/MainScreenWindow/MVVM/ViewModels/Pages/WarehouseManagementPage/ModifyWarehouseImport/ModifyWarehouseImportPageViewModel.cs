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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.ModifyWarehouseImport.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.ModifyWarehouseImport
{
    internal class ModifyWarehouseImportPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ModifyWarehouseImportPageViewModel");

        public ObservableCollection<tblMedicine> LstMedicine { get; set; }
        public ObservableCollection<MSW_WHMP_WarehouseImportDetailOV> LstWarehouseImportDetail { get; set; }
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
        public MSW_WHMP_MWIP_ButtonCommandOV ButtonCommandOV { get; set; }

        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private List<tblMedicine> _lstMedicineFull;
        private tblMedicine _selectedMedicine;
        private decimal _purchasedPrice;
        private bool _isAddImportDetailButtonRunning;
        private bool _isAddWarehouseImportButtonRunning;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_WHMP_MWIP_ButtonCommandOV(this);
            InstantiateItems();
            InitImportDetail();
        }

        protected override void OnInitialized()
        {
        }

        public void UpdateTotalPriceAndNewPrice()
        {
            TotalPrice = LstWarehouseImportDetail.Sum(o => o.TotalPrice);
            NetPrice = TotalPrice - PurchasedPrice;
            Invalidate("TotalPrice");
            Invalidate("NetPrice");
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
            LstWarehouseImportDetail = new ObservableCollection<MSW_WHMP_WarehouseImportDetailOV>();
            foreach (var item in ImportInfo.tblWarehouseImportDetails.Where(o => o.IsActive))
            {
                var detail = new MSW_WHMP_WarehouseImportDetailOV();
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
