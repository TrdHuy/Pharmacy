using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.SupplierImportHistory.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.SupplierImportHistory
{
    internal class SupplierImportHistoryPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("SupplierImportHistoryPageViewModel");

        public MSW_SMP_SIHP_ButtonCommandOV ButtonCommandOV { get; set; }
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

        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private tblWarehouseImport _importInfo;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_SMP_SIHP_ButtonCommandOV(this);
            UpdateData();
        }

        protected override void OnInitialized()
        {
        }

        private void UpdateData()
        {
            SupplierInfo = MSW_DataFlowHost.Current.CurrentModifiedSupplier;
            LstWarehouseImport = new ObservableCollection<tblWarehouseImport>(SupplierInfo.tblWarehouseImports.Where(o => o.IsActive).OrderByDescending(o=>o.ImportTime));
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

    }

}
