using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerDebtsPage.OVs
{
    public class MSW_CMP_CTP_CDP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_CMP_CTP_CDB_ButtonCommandOV");

        public RunInputCommand PrintCustomerDebtButtonCommand { get; set; }
        public RunInputCommand ReturnButtonCommand { get; set; }
        public RunInputCommand BillDisplayButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_CMP_CTP_CDP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            PrintCustomerDebtButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_PRINT_DEBTS_BUTTON
                    , paramaters);
            });
            BillDisplayButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_BILL_DISPLAY_BUTTON
                    , paramaters);
            });
            ReturnButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_RETURN_BUTTON
                    , paramaters);
            });
        }
    }
}
