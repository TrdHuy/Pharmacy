using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.MedicineManagement.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ModifyMedicine
{
    internal class ModifyMedicinePageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ModifyMedicinePageViewModel");

        public MSW_MMP_MMoP_ButtonCommandOV ButtonCommandOV { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }
        public ObservableCollection<tblMedicineType> LstMedicineType { get; set; }
        public ObservableCollection<tblMedicineUnit> LstMedicineUnit { get; set; }
        public ObservableCollection<tblSupplier> LstSupplier { get; set; }
        public int MedicineNameCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int MedicineTypeCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int MedicineUnitCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int SupplierCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int BidPriceCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int AskingPriceCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        
        public bool IsSaveButtonCanPerform
        {
            get
            {
                return MedicineNameCheckingStatus == 1
                    && MedicineTypeCheckingStatus == 1
                    && MedicineUnitCheckingStatus == 1
                    && SupplierCheckingStatus == 1
                    && BidPriceCheckingStatus == 1
                    && AskingPriceCheckingStatus == 1;
            }
        }
        public string MedicineID
        {
            get
            {
                return _medicineID;
            }
            set
            {
                _medicineID = value;
                InvalidateOwn();
            }
        }
        public string MedicineName
        {
            get
            {
                return _medicineName;
            }
            set
            {
                _medicineName = value;
                CheckMedicineName();
                InvalidateOwn();
            }
        }
        public int MedicineTypeID
        {
            get
            {
                return _medicineTypeID;
            }
            set
            {
                _medicineTypeID = value;
                CheckMedicineType();
                InvalidateOwn();
            }
        }
        public int MedicineUnitID
        {
            get
            {
                return _medicineUnitID;
            }
            set
            {
                _medicineUnitID = value;
                CheckMedicineUnit();
                InvalidateOwn();
            }
        }
        public int SupplierID
        {
            get
            {
                return _supplierID;
            }
            set
            {
                _supplierID = value;
                CheckSupplier();
                InvalidateOwn();
            }
        }
        public decimal BidPrice
        {
            get
            {
                return _bidPrice;
            }
            set
            {
                _bidPrice = value;
                CheckBidPrice();
                InvalidateOwn();
            }
        }
        public decimal AskingPrice
        {
            get
            {
                return _askingPrice;
            }
            set
            {
                _askingPrice = value;
                CheckAskingPrice();
                InvalidateOwn();
            }
        }
        public string MedicineDescription { get; set; } = "";
        public ObservableCollection<tblWarehouseImportDetail> LstWarehouseImportDetail { get; set; }
        public ImageSource MedicineImageSource { get; set; }
        public string MedicineImageFileName { get; set; }

        private string _medicineID = "";
        private string _medicineName = "";
        private int _medicineTypeID = -1;
        private int _medicineUnitID = -1;
        private int _supplierID = -1;
        private decimal _bidPrice = 0;
        private decimal _askingPrice = 0;
        private bool _isSaveButtonRunning;
        private tblMedicine _modifiedMedicine;
        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_MMP_MMoP_ButtonCommandOV(this);
            InitData();
            UpdateModifyData();
        }

        protected override void OnInitialized()
        {
        }

        private void UpdateModifyData()
        {
            _modifiedMedicine = MSW_DataFlowHost.Current.CurrentModifiedMedicine;
            MedicineID = _modifiedMedicine.MedicineID;
            MedicineName = _modifiedMedicine.MedicineName;
            MedicineTypeID = LstMedicineType.IndexOf(_modifiedMedicine.tblMedicineType);
            MedicineUnitID = LstMedicineUnit.IndexOf(_modifiedMedicine.tblMedicineUnit);
            SupplierID = LstSupplier.IndexOf(_modifiedMedicine.tblSupplier);
            BidPrice = _modifiedMedicine.BidPrice;
            AskingPrice = _modifiedMedicine.AskingPrice;
            MedicineDescription = _modifiedMedicine.MedicineDescription;

            GetWarehouseImportDetail();
            MedicineImageSource = FileIOUtil.
                GetBitmapFromName(_modifiedMedicine.MedicineID.ToString(), FileIOUtil.MEDICINE_IMAGE_FOLDER_NAME).
                ToImageSource();
        }

        private void GetWarehouseImportDetail()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstWarehouseImportDetail = new ObservableCollection<tblWarehouseImportDetail>(queryResult.Result as List<tblWarehouseImportDetail>);
                }
                else
                {
                    LstWarehouseImportDetail = new ObservableCollection<tblWarehouseImportDetail>();
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_MEDICINE_STOCK_IN_WAREHOUSE_DATA_CMD_KEY
                    , _sqlCmdObserver
                    , MedicineID);
        }

        private void CheckMedicineName()
        {
            if (MedicineName.Trim().Length > 0)
                MedicineNameCheckingStatus = 1;
            else
                MedicineNameCheckingStatus = -1;
            Invalidate("MedicineNameCheckingStatus");
        }

        private void CheckMedicineType()
        {
            if (MedicineTypeID != -1)
                MedicineTypeCheckingStatus = 1;
            else
                MedicineTypeCheckingStatus = -1;
            Invalidate("MedicineTypeCheckingStatus");
        }

        private void CheckMedicineUnit()
        {
            if (MedicineUnitID != -1)
                MedicineUnitCheckingStatus = 1;
            else
                MedicineUnitCheckingStatus = -1;
            Invalidate("MedicineUnitCheckingStatus");
        }

        private void CheckSupplier()
        {
            if (SupplierID != -1)
                SupplierCheckingStatus = 1;
            else
                SupplierCheckingStatus = -1;
            Invalidate("SupplierCheckingStatus");
        }

        private void CheckBidPrice()
        {
            if (BidPrice >= 0)
                BidPriceCheckingStatus = 1;
            else
                BidPriceCheckingStatus = -1;
            Invalidate("BidPriceCheckingStatus");
        }

        private void CheckAskingPrice()
        {
            if (AskingPrice >= 0)
                AskingPriceCheckingStatus = 1;
            else
                AskingPriceCheckingStatus = -1;
            Invalidate("AskingPriceCheckingStatus");
        }

        private void InitData()
        {
            GetListOfMedicineType();
            GetListOfMedicineUnit();
            GetListOfSupplier();
        }

        private void GetListOfSupplier()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstSupplier = new ObservableCollection<tblSupplier>(queryResult.Result as List<tblSupplier>);
                }
                else
                {
                    LstSupplier = new ObservableCollection<tblSupplier>();
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_SUPPLIER_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void GetListOfMedicineUnit()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstMedicineUnit = new ObservableCollection<tblMedicineUnit>(queryResult.Result as List<tblMedicineUnit>);
                }
                else
                {
                    LstMedicineUnit = new ObservableCollection<tblMedicineUnit>();
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_MEDICINE_UNIT_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }

        private void GetListOfMedicineType()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstMedicineType = new ObservableCollection<tblMedicineType>(queryResult.Result as List<tblMedicineType>);
                }
                else
                {
                    LstMedicineType = new ObservableCollection<tblMedicineType>();
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_MEDICINE_TYPE_DATA_CMD_KEY
                    , _sqlCmdObserver);
        }
    }

}
