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
        public string[] MedicineFilterPathList { get; set; } = new string[] { "MedicineName", "MedicineID" };
        public tblSupplier SelectedSupplier
        {
            get { return _selectedSupplier; }
            set
            {
                _selectedSupplier = value;
                if (value == null)
                {
                    SelectedSupplierCheckingStatus = -1;
                }
                else
                {
                    SelectedSupplierCheckingStatus = 1;
                }
                Invalidate("SelectedSupplierCheckingStatus");
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
                    MedicineQuantity = "";
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
        public string MedicineQuantity { get; set; }
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
                if (SelectedSupplierCheckingStatus == 1
                        && (LstWarehouseImportDetail.Count > 0 || PurchasedPrice > 0))
                    return true;
                return false;
            }
        }

        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private tblSupplier _selectedSupplier;
        private tblMedicine _selectedMedicine;
        private decimal _purchasedPrice;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_WHMP_AWIP_ButtonCommandOV(this);
        }

        protected override void OnInitialized()
        {
            InitImportDetail();
            InstantiateItems();
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
                    LstMedicine = new ObservableCollection<tblMedicine>((queryResult.Result as List<tblMedicine>).OrderBy(o => o.MedicineName));
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
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_SUPPLIER_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void UpdateNetPrice()
        {
            NetPrice = TotalPrice - PurchasedPrice;
            Invalidate("NetPrice");
        }

        private void InitImportDetail()
        {
            LstWarehouseImportDetail = new ObservableCollection<MSW_WHMP_WarehouseImportDetailOV>();
            LstMedicine = new ObservableCollection<tblMedicine>();
            MedicinePrice = 0;
            MedicineQuantity = "";
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
