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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage
{
    public class ModifySupplierPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ModifySupplierPageViewModel");
        
        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public int SupplierNameCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public int SupplierPhoneCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public bool IsSaveButtonRunning
        {
            get
            {
                return _isSaveButtonRunning;
            }
            set
            {
                _isSaveButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }
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
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private List<tblSupplier> _lstActiveSuppliers;


        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CancelButtonCommand = new RunInputCommand(CancelButtonClickEvent);
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
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

        private void SaveButtonClickEvent(object paramaters)
        {
            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_SAVE_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }

        private void CancelButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_CANCEL_BUTTON
                , dataTransfer);
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
