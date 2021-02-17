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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage
{
    public class ShowWarehouseImportInfoPageViewModel : AbstractViewModel
    {
        public ObservableCollection<MSW_WHMP_WarehouseImportDetailOV> LstWarehouseImportDetail { get; set; }
        public tblWarehouseImport ImportInfo { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PurchasedPrice { get; set; }
        public decimal NetPrice { get; set; }
        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand BrowseInvoiceImageButtonCommand { get; set; }

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;

        protected override void InitPropertiesRegistry()
        {
        }

        public ShowWarehouseImportInfoPageViewModel()
        {
            BrowseInvoiceImageButtonCommand = new RunInputCommand(BrowseInvoiceImageButtonClickEvent);
            CancelButtonCommand = new RunInputCommand(CancelButtonClickEvent);
            InitImportDetail();
        }

        private void CancelButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_CANCEL_BUTTON
                , dataTransfer);
        }

        private void BrowseInvoiceImageButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_SHOW_INVOICE_BUTTON
                , dataTransfer);
        }

        private void InitImportDetail()
        {
            ImportInfo = MSW_DataFlowHost.Current.CurrentModifiedWarehouseImport;
            TotalPrice = ImportInfo.TotalPrice;
            PurchasedPrice = ImportInfo.PurchasePrice;
            NetPrice = TotalPrice - PurchasedPrice;
            LstWarehouseImportDetail = new ObservableCollection<MSW_WHMP_WarehouseImportDetailOV>();
            foreach (var item in ImportInfo.tblWarehouseImportDetails.Where(o=>o.IsActive))
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
        }
    }
}
