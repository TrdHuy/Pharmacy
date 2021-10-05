using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ModifyMedicine.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.ModifyMedicine
{
    internal class ModifyMedicinePageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ModifyMedicinePageViewModel");

        public MSW_MMP_MMoP_ButtonCommandOV ButtonCommandOV { get; set; }
        public ObservableCollection<tblMedicineType> LstMedicineType { get; set; }
        public ObservableCollection<tblMedicineUnit> LstMedicineUnit { get; set; }
        public ObservableCollection<tblMedicineSupplier> LstSupplier { get; set; }
        public int MedicineNameCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int MedicineTypeCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int MedicineUnitCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int BidPriceCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int AskingPriceCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid

        public bool IsSaveButtonCanPerform
        {
            get
            {
                return MedicineNameCheckingStatus == 1
                    && MedicineTypeCheckingStatus == 1
                    && MedicineUnitCheckingStatus == 1
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
        private decimal _bidPrice = 0;
        private decimal _askingPrice = 0;
        private tblMedicine _modifiedMedicine;

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
            BidPrice = _modifiedMedicine.BidPrice;
            AskingPrice = _modifiedMedicine.AskingPrice;
            MedicineDescription = _modifiedMedicine.MedicineDescription;

            GetWarehouseImportDetail();
            GetListSupplier();
            MedicineImageSource = FileIOUtil.
                GetBitmapFromName(_modifiedMedicine.MedicineID.ToString(), FileIOUtil.MEDICINE_IMAGE_FOLDER_NAME).
                ToImageSource();
        }

        private void GetListSupplier()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    LstSupplier = new ObservableCollection<tblMedicineSupplier>(queryResult.Result as List<tblMedicineSupplier>);
                }
                else
                {
                    LstSupplier = new ObservableCollection<tblMedicineSupplier>();
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_SUPPLIERS_OF_MEDICINE
                    , _sqlCmdObserver
                    , MedicineID);
        }

        private void GetWarehouseImportDetail()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    var result = queryResult.Result as List<tblWarehouseImportDetail>;
                    LstWarehouseImportDetail = new ObservableCollection<tblWarehouseImportDetail>(result.GetRange(0, result.Count <= 10 ? result.Count : 10));
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
