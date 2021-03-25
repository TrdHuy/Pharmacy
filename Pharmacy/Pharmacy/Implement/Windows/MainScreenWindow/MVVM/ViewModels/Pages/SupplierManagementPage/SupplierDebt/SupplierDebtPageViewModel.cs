using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.SupplierDebt.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.SupplierDebt
{
    internal class SupplierDebtPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("SupplierDebtPageViewModel");

        public MSW_SMP_SDP_ButtonCommandOV ButtonCommandOV { get; set; }
        public RunInputCommand PrintDebtButtonCommand { get; set; }
        public RunInputCommand ShowInvoiceButtonCommand { get; set; }
        public tblSupplier SupplierInfo { get; set; }
        public ObservableCollection<MSW_SMP_SDP_SupplierDebtOV> LstDebt { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal PurchasedDebt { get; set; }
        public decimal GrossDebt { get; set; }

        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            UpdateData();
            ButtonCommandOV = new MSW_SMP_SDP_ButtonCommandOV(this);
        }

        protected override void OnInitialized()
        {
        }

        private void UpdateData()
        {
            SupplierInfo = MSW_DataFlowHost.Current.CurrentModifiedSupplier;

            LstDebt = new ObservableCollection<MSW_SMP_SDP_SupplierDebtOV>();
            foreach (var item in SupplierInfo.tblWarehouseImports.Where(o => o.IsActive))
            {
                if (item.PurchasePrice - item.TotalPrice != 0)
                {
                    MSW_SMP_SDP_SupplierDebtOV debt = new MSW_SMP_SDP_SupplierDebtOV();
                    debt.ImportID = item.ImportID;
                    debt.ImportTime = item.ImportTime;
                    debt.PurchasedDebt = item.PurchasePrice - item.TotalPrice;
                    debt.DebtType = debt.PurchasedDebt > 0 ? "Trả" : "Nợ";
                    debt.Description = debt.Description;
                    LstDebt.Add(debt);
                }
            }

            TotalDebt = LstDebt.Where(o => o.DebtType == "Nợ").Sum(o => o.PurchasedDebt);
            PurchasedDebt = LstDebt.Where(o => o.DebtType == "Trả").Sum(o => o.PurchasedDebt);
            GrossDebt = TotalDebt + PurchasedDebt;
        }
    }

}
