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
    public class SupplierImportHistoryPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("SupplierImportHistoryPageViewModel");

        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand ShowDebtButtonCommand { get; set; }
        public RunInputCommand ShowInvoiceButtonCommand { get; set; }
        public tblSupplier SupplierInfo { get; set; }
        public tblWarehouseImport ImportInfo
        {
            get { return _importInfo; }
            set
            {
                _importInfo = value;
                InvalidateOwn();
                UpdateWarehouseImportDetail();
            }
        }
        public ObservableCollection<tblWarehouseImport> LstWarehouseImport { get; set; }
        public ObservableCollection<string> LstWarehouseImportDetail { get; set; }

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private tblWarehouseImport _importInfo;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CancelButtonCommand = new RunInputCommand(CancelButtonClickEvent);
            ShowDebtButtonCommand = new RunInputCommand(ShowDebtButtonClickEvent);
            ShowInvoiceButtonCommand = new RunInputCommand(ShowInvoiceButtonClickEvent);
            UpdateData();
        }

        protected override void OnInitialized()
        {
        }

        private void UpdateData()
        {
            SupplierInfo = MSW_DataFlowHost.Current.CurrentModifiedSupplier;
            LstWarehouseImport = new ObservableCollection<tblWarehouseImport>(SupplierInfo.tblWarehouseImports.Where(o => o.IsActive));
        }
        private void UpdateWarehouseImportDetail()
        {
            LstWarehouseImportDetail = new ObservableCollection<string>();
            foreach (var item in ImportInfo.tblWarehouseImportDetails.Where(o=>o.IsActive))
            {
                LstWarehouseImportDetail.Add(item.tblMedicine.MedicineName + " - " + item.Quantity
                    + " " + item.tblMedicine.tblMedicineUnit.MedicineUnitName + ": " + ((decimal)item.Quantity * item.Price).ToString(@"#\,##0 VND"));
            }
            Invalidate("LstWarehouseImportDetail");
        }

        private void ShowInvoiceButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_SHOW_INVOICE_BUTTON
                , dataTransfer);
        }

        private void ShowDebtButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_SHOW_DEBT_BUTTON
                , dataTransfer);
        }

        private void CancelButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_CANCEL_BUTTON
                , dataTransfer);
        }
    }

}
