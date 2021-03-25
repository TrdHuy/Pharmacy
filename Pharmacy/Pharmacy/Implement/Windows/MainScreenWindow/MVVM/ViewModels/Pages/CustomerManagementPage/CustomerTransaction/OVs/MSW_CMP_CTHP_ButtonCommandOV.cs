using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.OVs
{
    internal class MSW_CMP_CTHP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_CMP_CTHP_ButtonCommandOV");

        protected override Logger logger => L;

        public CommandModel DebtsDisplayButtonCommand { get; set; }
        public CommandModel BillDisplayButtonCommand { get; set; }
        public CommandModel ReturnButtonCommand { get; set; }

        public MSW_CMP_CTHP_ButtonCommandOV(BaseViewModel parentsVM) : base(parentsVM) {

            DebtsDisplayButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_DEBTS_BUTTON
                    , paramaters);
            });
            BillDisplayButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_BILL_BUTTON
                    , paramaters);
            });
            ReturnButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_RETURN_BUTTON
                    , paramaters);
            });
        }
    }
}
