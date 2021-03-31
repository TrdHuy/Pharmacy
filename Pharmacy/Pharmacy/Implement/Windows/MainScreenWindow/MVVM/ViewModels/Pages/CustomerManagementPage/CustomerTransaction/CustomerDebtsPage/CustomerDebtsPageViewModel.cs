using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerDebtsPage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerDebtsPage
{
    internal class CustomerDebtsPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("CustomerDebtsPageViewModel");

        [Bindable(true)]
        public tblCustomer CurrentModifiedCustomer { get; set; }

        [Bindable(true)]
        public ObservableCollection<tblOrder> OrderItemSource { get; set; }

        [Bindable(true)]
        public ObservableCollection<MSW_CMP_CTP_CDP_CustomerDebtOV> DebtItemSource { get; set; }
        public tblOrder CurrentSelectedOrderDetail { get; set; }

        [Bindable(true)]
        public MSW_CMP_CTP_CDP_ButtonCommandOV ButtonCommandOV { get; set; }

        [Bindable(true)]
        public decimal PaidAmount
        {
            get
            {
                var pA = DebtItemSource.Where(o => o.DebtType == "Trả").Sum(o => o.PurchasedDebt);
                return pA;
            }
        }

        [Bindable(true)]
        public decimal RestAmount
        {
            get
            {
                return DebtAmount + PaidAmount;
            }
        }

        [Bindable(true)]
        public decimal DebtAmount
        {
            get
            {
                var dA = DebtItemSource.Where(o => o.DebtType == "Nợ").Sum(o => o.PurchasedDebt);
                return dA;
            }
        }


        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CurrentModifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;
            ButtonCommandOV = new MSW_CMP_CTP_CDP_ButtonCommandOV(this);
            InstantiateItems();
        }

        protected override void OnInitialized()
        {
        }

        public override void OnLoaded()
        {
            base.OnLoaded();
            CurrentModifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;
            InstantiateItems();
            RefreshViewModel();
        }

        private void InstantiateItems()
        {

            OrderItemSource = new ObservableCollection<tblOrder>(
                CurrentModifiedCustomer
                .tblOrders
                .Where((o) => o.IsActive));

            DebtItemSource = new ObservableCollection<MSW_CMP_CTP_CDP_CustomerDebtOV>();

            foreach (var item in OrderItemSource)
            {
                MSW_CMP_CTP_CDP_CustomerDebtOV debt = new MSW_CMP_CTP_CDP_CustomerDebtOV();
                debt.OrderID = item.OrderID;
                debt.OrderTime = item.OrderTime;
                debt.PurchasedDebt = item.PurchasePrice - item.TotalPrice;
                debt.DebtType = debt.PurchasedDebt > 0 ? "Trả" : "Nợ";
                debt.Description = item.OrderDescription;
                DebtItemSource.Add(debt);
            }
        }

    }
}
