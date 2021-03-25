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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.ShowWarehouseImportInfo.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.ShowWarehouseImportInfo
{
    internal class ShowWarehouseImportInfoPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ShowWarehouseImportInfoPageViewModel");

        public ObservableCollection<MSW_WHMP_WarehouseImportDetailOV> LstWarehouseImportDetail { get; set; }
        public tblWarehouseImport ImportInfo { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PurchasedPrice { get; set; }
        public decimal NetPrice { get; set; }
        public MSW_WHMP_SWIIP_ButtonCommandOV ButtonCommandOV { get; set; }

        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_WHMP_SWIIP_ButtonCommandOV(this);
            InitImportDetail();
        }

        protected override void OnInitialized()
        {
        }

        private void InitImportDetail()
        {
            ImportInfo = MSW_DataFlowHost.Current.CurrentModifiedWarehouseImport;
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
        }
    }
}
