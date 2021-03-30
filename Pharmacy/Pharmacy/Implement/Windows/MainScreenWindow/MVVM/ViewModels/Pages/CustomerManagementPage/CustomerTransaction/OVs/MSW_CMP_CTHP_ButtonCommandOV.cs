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

        public CommandExecuterModel DebtsDisplayButtonCommand { get; set; }
        public CommandExecuterModel BillDisplayButtonCommand { get; set; }
        public CommandExecuterModel ReturnButtonCommand { get; set; }

        public MSW_CMP_CTHP_ButtonCommandOV(BaseViewModel parentsVM) : base(parentsVM) {

            DebtsDisplayButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_DEBTS_BUTTON
                    , paramaters);
            });
            BillDisplayButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_BILL_BUTTON
                    , paramaters);
            });
            ReturnButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_RETURN_BUTTON
                    , paramaters);
            });
        }
    }
}
