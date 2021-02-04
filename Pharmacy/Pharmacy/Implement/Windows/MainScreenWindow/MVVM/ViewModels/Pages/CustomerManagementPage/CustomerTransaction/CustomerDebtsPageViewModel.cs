using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction
{
    public class CustomerDebtsPageViewModel : AbstractViewModel
    {
        private static Logger logger = new Logger("CustomerDebtsPageViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;

        public tblCustomer CurrentModifiedCustomer { get; set; }
        public ObservableCollection<tblOrder> OrderItemSource { get; set; }
        public tblOrder CurrentSelectedOrderDetail { get; set; }
        public RunInputCommand PrintCustomerDebtButtonCommand { get; set; }
        public RunInputCommand ReturnButtonCommand { get; set; }

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

        public string PaymentClassification
        {
            get;
            set;
        }

        protected override void InitPropertiesRegistry()
        {
            logger.I("Instantinating CustomerDebtsPageViewModel");

            CurrentModifiedCustomer = MSW_DataFlowHost.Current.CurrentModifiedCustomer;
            InstantiateItems();
            PrintCustomerDebtButtonCommand = new RunInputCommand(PrintCustomerDebtButtonClickEvent);
            ReturnButtonCommand = new RunInputCommand(ReturnButtonClickEvent);

            logger.I("Instantinated CustomerDebtsPageViewModel");
        }

        private void InstantiateItems()
        {
            OrderItemSource = new ObservableCollection<tblOrder>();
            foreach (tblOrder order in CurrentModifiedCustomer.tblOrders)
            {
                OrderItemSource.Add(order);
            }

        }

        private void ReturnButtonClickEvent(object paramaters)
        {
            logger.V("OnReturnButtonClickEvent");

            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_RETURN_BUTTON
                , dataTransfer);
        }

        private void PrintCustomerDebtButtonClickEvent(object paramaters)
        {
            logger.V("OnPrintCustomerDebtButtonClickEvent");

            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_PRINT_DEBTS_BUTTON
                , dataTransfer);
        }
    }
}
