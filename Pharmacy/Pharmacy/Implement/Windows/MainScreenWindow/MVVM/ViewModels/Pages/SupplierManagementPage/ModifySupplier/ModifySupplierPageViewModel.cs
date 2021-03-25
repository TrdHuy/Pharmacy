using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.ModifySupplier.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.ModifySupplier
{
    internal class ModifySupplierPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ModifySupplierPageViewModel");

        public MSW_SMP_MSP_ButtonCommandOV ButtonCommandOV { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public int SupplierNameCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int SupplierPhoneCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public bool IsSaveButtonCanPerform
        {
            get
            {
                return SupplierNameCheckingStatus == 1
                    && SupplierPhoneCheckingStatus == 1;
            }
        }
        public string SupplierName
        {
            get
            {
                return _supplierName;
            }
            set
            {
                _supplierName = value;
                CheckSupplierName();
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                CheckSupplierPhone();
            }
        }
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string Description { get; set; } = "";
        public tblSupplier SupplierInfo { get; set; }

        private string _supplierName = "";
        private string _phone = "";
        private bool _isSaveButtonRunning;
        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private List<tblSupplier> _lstActiveSuppliers;


        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_SMP_MSP_ButtonCommandOV(this);
            GetActiveSuppliers();
            UpdateModifyData();
        }

        protected override void OnInitialized()
        {
        }

        private void UpdateModifyData()
        {
            SupplierInfo = MSW_DataFlowHost.Current.CurrentModifiedSupplier;
            SupplierName = SupplierInfo.SupplierName;
            Phone = SupplierInfo.Phone;
            Email = SupplierInfo.Email;
            Address = SupplierInfo.Address;
            Description = SupplierInfo.SupplierDescription;
        }

        private void GetActiveSuppliers()
        {
            SQLQueryCustodian sqlCmdObserver = new SQLQueryCustodian(queryResult =>
            {
                if (queryResult.MesResult == MessageQueryResult.Done)
                {
                    _lstActiveSuppliers = queryResult.Result as List<tblSupplier>;
                }
                else
                {
                    _lstActiveSuppliers = new List<tblSupplier>();
                }
            });
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_SUPPLIER_DATA_CMD_KEY,
                sqlCmdObserver);
        }

        private void CheckSupplierName()
        {
            if (SupplierName.Trim().Length > 0
                && (SupplierName.Trim() == SupplierInfo.SupplierName
                || _lstActiveSuppliers.Where(o => o.SupplierName.Equals(SupplierName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() == null))
                SupplierNameCheckingStatus = 1;
            else
                SupplierNameCheckingStatus = -1;
            Invalidate("SupplierNameCheckingStatus");
        }

        private void CheckSupplierPhone()
        {
            if (Phone.Trim().Length > 0
                && (Phone.Trim() == SupplierInfo.Phone
                || _lstActiveSuppliers.Where(o => o.Phone.Equals(Phone, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() == null))
                SupplierPhoneCheckingStatus = 1;
            else
                SupplierPhoneCheckingStatus = -1;
            Invalidate("SupplierPhoneCheckingStatus");
        }
    }

}
