using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.AddMedicine.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.AddMedicine
{
    internal class AddMedicinePageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("AddMedicinePageViewModel");

        public ObservableCollection<tblMedicineType> LstMedicineType { get; set; }
        public ObservableCollection<tblMedicineUnit> LstMedicineUnit { get; set; }
        public ObservableCollection<tblSupplier> LstSupplier { get; set; }

        public MSW_MMP_AMP_ButtonCommandOV ButtonCommandOV { get; set; }
        public string MedicineIDCheckingStatusText { get; set; } = MedicineIDCheckingStatusMessage.Empty.GetStringValue();
        public int MedicineIDCheckingStatus { get; set; } = 1; //based on MedicineIDCheckingStatusMessage
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
                return MedicineIDCheckingStatus == (int)MedicineIDCheckingStatusMessage.MedicineIDAccepted
                    && MedicineNameCheckingStatus == 1
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
                CheckMedicineID();
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
            }
        }
        public string MedicineDescription { get; set; } = "";
        public ImageSource MedicineImageSource { get; set; }
        public string MedicineImageFileName { get; set; }

        private string _medicineID = "";
        private string _medicineName = "";
        private int _medicineTypeID = -1;
        private int _medicineUnitID = -1;
        private int _supplierID = -1;
        private decimal _bidPrice;
        private decimal _askingPrice;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_MMP_AMP_ButtonCommandOV(this);
            BidPrice = 0;
            AskingPrice = 0;
            InitData();
        }

        protected override void OnInitialized()
        {
        }

        private void CheckMedicineID()
        {
            if (MedicineID.Trim().Length == 0)
            {
                MedicineIDCheckingStatusText = MedicineIDCheckingStatusMessage.Empty.GetStringValue();
                MedicineIDCheckingStatus = (int)MedicineIDCheckingStatusMessage.Empty;
            }
            else if (MedicineID.Trim().Contains(" "))
            {
                MedicineIDCheckingStatusText = MedicineIDCheckingStatusMessage.WhiteSpaceAware.GetStringValue();
                MedicineIDCheckingStatus = (int)MedicineIDCheckingStatusMessage.WhiteSpaceAware;
            }
            else
            {
                IsMedicineIDExisted();
            }
            Invalidate("MedicineIDCheckingStatusText");
            Invalidate("MedicineIDCheckingStatus");
        }

        private void IsMedicineIDExisted()
        {
            SQLQueryCustodian _sqlCmdObserver = new SQLQueryCustodian((queryResult) =>
            {
                if ((bool)(queryResult.Result) == true)
                {
                    MedicineIDCheckingStatusText = MedicineIDCheckingStatusMessage.MedicineIDExisted.GetStringValue();
                    MedicineIDCheckingStatus = (int)MedicineIDCheckingStatusMessage.MedicineIDExisted;
                }
                else
                {
                    MedicineIDCheckingStatusText = MedicineIDCheckingStatusMessage.MedicineIDAccepted.GetStringValue();
                    MedicineIDCheckingStatus = (int)MedicineIDCheckingStatusMessage.MedicineIDAccepted;
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.CHECK_MEDICINEID_EXISTED_CMD_KEY
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
