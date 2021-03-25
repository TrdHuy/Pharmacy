using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InvoiceManagementPage.OVs
{
    internal class MSW_IMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_IMP_ButtonCommandOV");


        public CommandModel EditOrderButtonCommand { get; set; }
        public CommandModel DeleteOrderButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_IMP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            EditOrderButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_IMP_EDIT_BUTTON
                    , paramaters);
            });
            DeleteOrderButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_IMP_DELETE_BUTTON
                    , paramaters);
            });
        }
    }
}
