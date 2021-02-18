using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerDebtsPage.OVs
{
    public class MSW_CMP_CTP_CDP_ButtonCommandOV : AbstractViewModel
    {
        private static Logger logger = new Logger("MSW_CMP_CTP_CDB_ButtonCommandOV");
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        public RunInputCommand PrintCustomerDebtButtonCommand { get; set; }
        public RunInputCommand ReturnButtonCommand { get; set; }
        public RunInputCommand BillDisplayButtonCommand { get; set; }

        public MSW_CMP_CTP_CDP_ButtonCommandOV(AbstractViewModel parentVM) : base(parentVM)
        {
            PrintCustomerDebtButtonCommand = new RunInputCommand(PrintCustomerDebtButtonClickEvent);
            ReturnButtonCommand = new RunInputCommand(ReturnButtonClickEvent);
            BillDisplayButtonCommand = new RunInputCommand(CustomerBillDisplayButtonClickEvent);
        }

        private void ReturnButtonClickEvent(object paramaters)
        {
            logger.V("OnReturnButtonClickEvent");

            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_RETURN_BUTTON
                , dataTransfer);
        }

        private void PrintCustomerDebtButtonClickEvent(object paramaters)
        {
            logger.V("OnPrintCustomerDebtButtonClickEvent");

            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_PRINT_DEBTS_BUTTON
                , dataTransfer);
        }

        private void CustomerBillDisplayButtonClickEvent(object paramaters)
        {
            logger.V("CustomerBillDisplayButtonClickEvent");

            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_BILL_DISPLAY_BUTTON
                , dataTransfer);
        }
    }
}
