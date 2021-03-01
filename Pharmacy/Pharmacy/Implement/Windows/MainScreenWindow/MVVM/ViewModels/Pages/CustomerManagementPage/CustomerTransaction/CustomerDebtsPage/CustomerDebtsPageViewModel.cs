using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerDebtsPage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerDebtsPage
{
    public class CustomerDebtsPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("CustomerDebtsPageViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;

        public tblCustomer CurrentModifiedCustomer { get; set; }
        public ObservableCollection<tblOrder> OrderItemSource { get; set; }
        public tblOrder CurrentSelectedOrderDetail { get; set; }
        public MSW_CMP_CTP_CDP_ButtonCommandOV ButtonCommandOV { get; set; }

        public RunInputCommand PrintCustomerDebtButtonCommand { get; set; }
        public RunInputCommand ReturnButtonCommand { get; set; }
        public RunInputCommand BillDisplayButtonCommand { get; set; }

        public decimal PaidAmount
        {
            get
            {
                var pA = OrderItemSource.Sum((oD) => oD.PurchasePrice);
                return pA;
            }
        }
        public decimal RestAmount
        {
            get
            {
                return DebtAmount - PaidAmount;
            }
        }
        public decimal DebtAmount
        {
            get
            {
                var dA = OrderItemSource.Sum((oD) => oD.TotalPrice);
                return dA;
            }
        }

        public string PaymentClassification { get; set; }

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

        private void InstantiateItems()
        {
            OrderItemSource = new ObservableCollection<tblOrder>(
                CurrentModifiedCustomer
                .tblOrders
                .Where((o) => o.IsActive));
        }

    }
}
