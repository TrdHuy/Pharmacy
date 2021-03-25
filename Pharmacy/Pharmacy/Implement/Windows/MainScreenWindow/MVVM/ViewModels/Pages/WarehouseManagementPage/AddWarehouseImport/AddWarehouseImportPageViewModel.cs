using Pharmacy.Base.MVVM.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler.Listener;
using System.Windows.Controls;
using System.Linq;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.AddWarehouseImport.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.AddWarehouseImport
{
    internal class AddWarehouseImportPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("AddWarehouseImportPageViewModel");

        public ObservableCollection<tblSupplier> LstSupplier { get; set; }
        public ObservableCollection<tblMedicine> LstMedicine { get; set; }
        public ObservableCollection<MSW_WHMP_WarehouseImportDetailOV> LstWarehouseImportDetail { get; set; }
        public tblSupplier SelectedSupplier
        {
            get { return _selectedSupplier; }
            set
            {
                if (CheckResetAllWhenSupplierChanged(value))
                {
                    _selectedSupplier = value;
                    if (value != null)
                    {
                        SelectedSupplierCheckingStatus = 1;
                        UpdateMedicineListBySupplier();
                    }
                    else
                    {
                        SelectedSupplierCheckingStatus = -1;
                    }
                    Invalidate("SelectedSupplierCheckingStatus");
                }
                InvalidateOwn();
            }
        }
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
        public MSW_WHMP_AWIP_ButtonCommandOV ButtonCommandOV { get; set; }
        public int SelectedSupplierCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
      
        public bool IsSaveButtonCanPerform
        {
            get
            {
                if (SelectedSupplier != null
                        && LstWarehouseImportDetail.Count > 0)
                    return true;
                return false;
            }
        }

        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private List<tblMedicine> _lstMedicineFull;
        private tblSupplier _selectedSupplier;
        private tblMedicine _selectedMedicine;
        private decimal _purchasedPrice;
        private bool _isAddImportDetailButtonRunning;
        private bool _isAddWarehouseImportButtonRunning;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_WHMP_AWIP_ButtonCommandOV(this);
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
            GetSupplierList();
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

        private void GetSupplierList()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstSupplier = new ObservableCollection<tblSupplier>(queryResult.Result as List<tblSupplier>);
                    Invalidate("LstSupplier");
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_SUPPLIER_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void UpdateMedicineListBySupplier()
        {
            if (SelectedSupplier != null)
            {
                LstMedicine = new ObservableCollection<tblMedicine>(_lstMedicineFull.Where(o => o.SupplierID == SelectedSupplier.SupplierID));
                Invalidate("LstMedicine");
                SelectedMedicine = null;
            }
        }

        private void UpdateNetPrice()
        {
            NetPrice = TotalPrice - PurchasedPrice;
            Invalidate("NetPrice");
        }

        private bool CheckResetAllWhenSupplierChanged(tblSupplier newValue)
        {
            if (LstWarehouseImportDetail.Count > 0 && _selectedSupplier != newValue)
            {
                var result = App.Current.ShowApplicationMessageBox("Thay đổi nhà cung cấp sẽ xóa toàn bộ dữ liệu nhập kho trong danh sách bên dưới.\nBạn có muốn tiếp tục?",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                   OwnerWindow.MainScreen,
                   "Cảnh báo!");
                if (result == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
                {
                    InitImportDetail();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (LstMedicine != null)
                InitImportDetail();
            return true;
        }

        private void InitImportDetail()
        {
            LstWarehouseImportDetail = new ObservableCollection<MSW_WHMP_WarehouseImportDetailOV>();
            LstMedicine = new ObservableCollection<tblMedicine>();
            MedicinePrice = 0;
            MedicineQuantity = 0;
            SelectedMedicine = null;
            TotalPrice = 0;
            PurchasedPrice = 0;
            NetPrice = 0;
            Invalidate("LstWarehouseImportDetail");
            Invalidate("LstMedicine");
            Invalidate("MedicinePrice");
            Invalidate("MedicineQuantity");
            Invalidate("TotalPrice");
            Invalidate("NetPrice");
        }
    }
}
